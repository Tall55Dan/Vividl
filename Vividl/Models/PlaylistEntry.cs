using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Vividl.Services;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;
using YoutubeDLSharp.Options;

namespace Vividl.Models;

public class PlaylistEntry(YoutubeDL ydl, VideoData metadata, OptionSet overrideOptions)
  : MediaEntry(ydl, metadata, overrideOptions)
{
  public override int TotalItems => Metadata.Entries?.Length ?? 1;

  public override bool FileAvailable
    => DownloadPaths is { Length: > 0 } && !string.IsNullOrEmpty(DownloadPaths[0]);

  private string[]? DownloadPaths { get; set; }

  protected override async Task<DownloadResult> DoDownload(DownloadOption downloadOption)
  {
    try
    {
      var run = await downloadOption.RunDownload(ydl, this, cts.Token, progress, overrideOptions: this.OverrideOptions);
      DownloadPaths = run.Data;
      // TODO When does playlist download count as 'failed'?
      if (!run.Success) return DownloadResult.Failed;
    }
    catch (Exception ex)
    {
      // TODO Clean up partially downloaded files?
      DownloadPaths = [];
      return ex is TaskCanceledException ? DownloadResult.Cancelled : DownloadResult.Failed;
    }

    return DownloadResult.Success;
  }

  public override void OpenFile()
  {
    if (FileAvailable)
      Process.Start(DownloadPaths![0]);
  }

  public override void ShowInFolder(IFileService fileService)
  {
    if (FileAvailable)
      fileService.ShowInExplorer(DownloadPaths!);
  }
}