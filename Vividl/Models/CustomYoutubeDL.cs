using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace Vividl.Models;

public class CustomYoutubeDl(
  byte maxNumberOfProcesses,
  string downloadArchive,
  string proxy,
  string formatSort,
  OptionSet customDownloadOptions)
  : YoutubeDL(maxNumberOfProcesses)
{
  private string DownloadArchive { get; set; } = downloadArchive;

  public bool AddMetadata { get; set; }

  private string Proxy { get; set; } = proxy;

  private string FormatSort { get; set; } = formatSort;

  private OptionSet CustomDownloadOptions { get; set; } = customDownloadOptions;

  protected override OptionSet GetDownloadOptions()
  {
    var options = base.GetDownloadOptions();
#if LegacyYoutubeDLSharp
            // Workaround to suppress the warning in yt-dlp
            if (UsingYtDlp)
            {
                options.ExternalDownloaderArgs = "ffmpeg:" + options.ExternalDownloaderArgs;
            }
#endif
    options.DownloadArchive = this.DownloadArchive;
#if LegacyYoutubeDLSharp
    options.AddMetadata = this.AddMetadata;
#else
    options.EmbedMetadata = this.AddMetadata;
#endif
    options.Proxy = this.Proxy;
    options.FormatSort = this.FormatSort;
    options = options.OverrideOptions(this.CustomDownloadOptions);
    return options;
  }
}