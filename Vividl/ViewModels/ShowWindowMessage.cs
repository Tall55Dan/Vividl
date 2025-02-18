﻿using System;

namespace Vividl.ViewModels;

public enum WindowType
{
  VideoDataWindow,
  PlaylistDataWindow,
  SettingsWindow,
  FetchWindow,
  DownloadOutputWindow,
  FormatSelectionWindow,
  CustomArgsWindow,
  NotificationLogWindow,
  NameEditWindow,
}

internal class ShowWindowMessage
{
  public WindowType Window { get; }
  public object Parameter { get; }
  public Action<bool?, object> Callback { get; }

  public ShowWindowMessage(WindowType window, object parameter = null,
    Action<bool?, object> callback = null)
  {
    this.Window = window;
    this.Parameter = parameter;
    this.Callback = callback;
  }
}