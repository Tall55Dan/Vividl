using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Vividl.Helpers;

public class ButtonEx
{
  private static readonly DependencyProperty CloseWindowProperty =
    DependencyProperty.RegisterAttached(
      "CloseWindow", typeof(bool), typeof(ButtonEx),
      new PropertyMetadata(false, onPropertyChanged)
    );

  public static void SetCloseWindow(UIElement element, bool value)
    => element.SetValue(CloseWindowProperty, value);

  public static bool GetCloseWindow(UIElement element)
    => (bool)element.GetValue(CloseWindowProperty);

  protected virtual void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is not Button btn) return;
    bool oldVal = (bool)e.OldValue, newVal = (bool)e.NewValue;
    switch (oldVal)
    {
      case false when newVal:
        btn.Click += ButtonClick;
        break;
      case true when !newVal:
        btn.Click -= ButtonClick;
        break;
    }
  }

  private static void ButtonClick(object? sender, RoutedEventArgs e)
  {
    var button = (Button)sender!;
    Window window = Window.GetWindow(button);
    window.DialogResult = button!.IsDefault;
    window.Close();
  }
}