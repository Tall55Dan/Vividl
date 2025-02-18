using System;
using YoutubeDLSharp;

namespace Vividl.Models;

public class ProgressEventArgs : EventArgs
{
  public DownloadProgress Info { get; }

  public ProgressEventArgs(DownloadProgress info)
  {
    this.Info = info;
  }
}