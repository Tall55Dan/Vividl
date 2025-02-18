
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Vividl.Views;

public partial class CustomArgsWindow : UserControl
{
  public CustomArgsWindow(string args)
  {
    InitializeComponent();
    txtArgs.Text = args;
  }

  public object ReturnValue { get; set; }
  public string Cancel { get; set; }
  public string CustomArgsWindow_Description { get; }
  public string CustomArgsWindow_Warning { get; }

  private void Submit_Click(object sender, RoutedEventArgs e)
  {
    this.DialogResult = true;
    this.ReturnValue = txtArgs.Text;
    this.Close();
  }
}