namespace Vividl.Models;

public interface IDownloadOption
{
  string FormatSelection { get; }
  string Description { get; }
  bool IsAudio { get; }

  string GetExt(string defaultValue = "");
}