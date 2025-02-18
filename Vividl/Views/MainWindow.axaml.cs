using Avalonia.Controls;

namespace Vividl.Views;

public partial class MainWindow : Window
{
  public MainWindow(string greeting)
  {
    Greeting = greeting;
    InitializeComponent();
  }

  public string Greeting { get; }
}