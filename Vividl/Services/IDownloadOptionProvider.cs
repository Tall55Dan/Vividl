using System.Collections.Generic;
using Vividl.Models;

namespace Vividl.Services;

public interface IDownloadOptionProvider
{
    List<IDownloadOption> CreateDownloadOptions(bool withCustomDownload = false);
}