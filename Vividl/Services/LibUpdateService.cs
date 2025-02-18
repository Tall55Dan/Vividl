using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vividl.Models;
using Vividl.Properties;

namespace Vividl.Services;

public interface ILibUpdateService
{
  string Version { get; }
  bool IsUpdating { get; }
  Task<bool> CheckForUpdates();
  Task<string> Update();
}

public abstract class YtdlUpdateService(CustomYoutubeDL ydl, IDialogService dialogService)
  : ILibUpdateService, INotifyPropertyChanged
{
  private const string YTDL_LATEST_VERSION_URL = "https://yt-dl.org/update/LATEST_VERSION";
  private const string YTDLP_LATEST_VERSION_URL = "https://api.github.com/repos/yt-dlp/yt-dlp/releases/latest";

  private CustomYoutubeDL ydl = ydl;
  private IDialogService dialogService = dialogService;
  private bool isUpdating;

  public string Version => ydl.Version;

  public bool IsUpdating
  {
    get => isUpdating;
    set
    {
      isUpdating = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUpdating)));
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  public async Task<bool> CheckForUpdates()
  {
    if (App.UsingYtDlp)
    {
      return await checkForUpdatesYtDlp();
    }
    else
    {
      return await checkForUpdatesYoutubeDL();
    }
  }

  private async Task<bool> checkForUpdatesYoutubeDL()
  {
    using (WebClient client = new WebClient())
    {
      try
      {
        string latestVersion = await client.DownloadStringTaskAsync(YTDL_LATEST_VERSION_URL);
        if (new Version(latestVersion) > new Version(this.Version))
        {
          return dialogService.ShowConfirmation(
            String.Format(Resources.YtdlUpdateService_NewUpdateMessage, latestVersion, this.Version),
            "Vividl - " + Resources.Info
          );
        }

        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }

  private async Task<bool> checkForUpdatesYtDlp()
  {
    using (WebClient client = new WebClient())
    {
      try
      {
        string jsonString = await client.DownloadStringTaskAsync(YTDLP_LATEST_VERSION_URL);
        Dictionary<string, string>
          versionInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        string latestVersion = versionInfo["tag_name"];
        if (new Version(latestVersion) > new Version(this.Version))
        {
          return dialogService.ShowConfirmation(
            String.Format(Resources.YtdlUpdateService_NewUpdateMessage, latestVersion, this.Version),
            "Vividl - " + Resources.Info
          );
        }

        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }

  public async Task<string> Update()
  {
    IsUpdating = true;
    var output = await ydl.RunUpdate();
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Version)));
    IsUpdating = false;
    return output;
  }
}