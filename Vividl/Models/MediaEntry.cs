using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CrossPlatformLibrary.IoC;
using Vividl.Properties;
using Vividl.Services;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;
using YoutubeDLSharp.Options;

namespace Vividl.Models;

public abstract class MediaEntry(YoutubeDL youtubeDl, VideoData metadata, OptionSet overrideOptions)
  : IDownloadEntry
{
  public event EventHandler<ProgressEventArgs>? DownloadStateChanged;

  private YoutubeDL ytdl = youtubeDl;
  protected CancellationTokenSource cts = new();
  protected readonly IProgress<DownloadProgress> progress = new Progress<DownloadProgress>();
  protected OptionSet OverrideOptions { get; } = overrideOptions;
  public VideoData Metadata { get; } = metadata;
  public string Url => Metadata.WebpageUrl;
  public string Title => Metadata.Title;
  public string DownloadName { get; set; } = "";
  public abstract int TotalItems { get; }
  public abstract bool FileAvailable { get; }
  public DownloadOptionCollection DownloadOptions { get; } = [];
  public int SelectedDownloadOption { get; set; }
  public async Task<MediaEntry> Fetch(string url, OptionSet overrideOptions)
  {
    ytdl = SimpleIoc.Default.GetInstance<YoutubeDL>();
    var run = await ytdl.RunVideoDataFetch(url, overrideOptions: overrideOptions);
    if (!run.Success)
      throw new VideoEntryException(run.ErrorOutput);
    var metadata = run.Data;
    return metadata.ResultType switch
    {
      MetadataType.Playlist or MetadataType.MultiVideo => new PlaylistEntry(ytdl, metadata,
        overrideOptions: overrideOptions),
      _ => new VideoEntry(ytdl, metadata, overrideOptions: overrideOptions)
    };
  }

  protected abstract Task<DownloadResult> DoDownload(DownloadOption downloadOption);

  public async Task<DownloadResult> Download()
  {
    CancelDownload(); // Cancel ongoing download if existent
    cts = new CancellationTokenSource();
    Directory.CreateDirectory(Settings.Default.DownloadFolder);
    var result = await DoDownload((DownloadOption)DownloadOptions[SelectedDownloadOption]);
    cts.Dispose();
    return result;
  }

  public void CancelDownload()
  {
    cts.Cancel();
  }

  public void OpenInBrowser() => Process.Start(Url);

  public abstract void OpenFile();

  public abstract void ShowInFolder(IFileService fileService);

  private void RaiseDownloadStateChanged(DownloadProgress p)
    => DownloadStateChanged?.Invoke(this, new ProgressEventArgs(p));
}