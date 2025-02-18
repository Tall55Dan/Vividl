using System;
using System.Diagnostics;
using System.Linq;
using Avalonia.Controls;

namespace Vividl.Services;

public interface IFileService
{
    string SelectOpenFile(string? filter = null, string? selected = null);

    string SelectSaveFile(string? filter = null);

    string SelectFolder(string? description = null, string? selected = null);

    void ShowInExplorer(string path, bool isFolder = false);

    void ShowInExplorer(string[] paths);
}

class FileService : IFileService
{
    [Obsolete("Obsolete")]
    public string SelectOpenFile(string? filters = null, string? selected = null)
    {
        var ofd = new OpenFileDialog
        {
            Title = null,
            Directory = null,
            Filters = null,
            InitialFileName = null,
            AllowMultiple = false
        };
        ofd.Filter = filter;
        ofd.FileName = selected;
        ofd.ShowDialog();
        return ofd.FileName;
    }

    [Obsolete("Obsolete")]
    public string SelectSaveFile(string? filter = null)
    {
        var sfd = new SaveFileDialog
        {
            Title = null,
            Directory = null,
            Filters = null,
            InitialFileName = null,
            DefaultExtension = null,
            ShowOverwritePrompt = null
        };
        sfd.Filter = filter;
        sfd.ShowDialog();
        return sfd.FileName;
    }

    public string SelectFolder(string? description = null, string? selected = null)
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        fbd.Description = description;
        fbd.SelectedPath = selected;
        fbd.ShowDialog();
        return fbd.SelectedPath;
    }

    public void ShowInExplorer(string path, bool isFolder = false)
    {
        var command = isFolder ? "/open" : "/select";
        Process.Start("explorer.exe", $"{command},\"{path}\"");
    }

    public void ShowInExplorer(string[] paths)
    {
        var args = string.Join(" ", paths.Select(p => $"/select,{p}"));
        Process.Start("explorer.exe", args);
    }
}