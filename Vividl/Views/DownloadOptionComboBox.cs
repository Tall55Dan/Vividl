using System;
using System.Windows.Input;
using Avalonia.Controls;
using Vividl.Models;

namespace Vividl.Views;

internal class DownloadOptionComboBox : ComboBox
{
  private static readonly DependencyProperty CustomDownloadCommandProperty =
    DependencyProperty.Register("CustomDownloadCommand",
      typeof(ICommand), typeof(DownloadOptionComboBox)
    );

  public static ICommand CustomDownloadCommand
  {
    get => (ICommand)GetValue(CustomDownloadCommandProperty);
    set => SetValue(CustomDownloadCommandProperty, value);
  }

  public event EventHandler? CustomDownloadSelected;

  protected void OnSelectionChanged(SelectionChangedEventArgs e)
  {
    base.OnSelectionChanged(e);
    // Only trigger event & command if selection change was done by user.
    // We try to ensure this via "IsDropDownOpen || IsKeyboardFocused".
    if (ItemsSource is not DownloadOptionCollection downloadOptions
        || (!IsDropDownOpen && !IsKeyboardFocused)) return;
    
    if (e.AddedItems[0] is not CustomDownload) return;
    
    CustomDownloadSelected?.Invoke(this, EventArgs.Empty);
        CustomDownloadCommand?.Execute(
      new Action<bool>((applied) =>
        {
          // Reset selection if custom download was not configured.
          // This avoids invalid empty custom downloads.
          if (!applied) SelectedItem = e.RemovedItems[0];
        }
      )
    );
  }
}