using System;
using System.Globalization;
using Avalonia.Data.Converters;
using IconPacks.Avalonia.Modern;
using Vividl.Models;
using static System.String;

namespace Vividl.Helpers;

class InverseBoolConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return (bool)value!;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return (bool)value!;
  }
}

internal class NullToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {

    return value == null ? Visibility.Collapsed : Visibility.Visible;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class InverseBooleanToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is bool b)
      return b ? Visibility.Collapsed : Visibility.Visible;
    else return Visibility.Visible;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is Visibility vis)
      return vis != Visibility.Visible;
    else return true;
  }
}

internal enum Visibility
{
  Collapsed,
  Visible
}

class IntToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return (int)value! > 0 ? Visibility.Visible : Visibility.Collapsed;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class TypeToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null || parameter == null) return Visibility.Collapsed;

    return (value.GetType() == (Type)parameter) ? Visibility.Visible : Visibility.Collapsed;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class StringToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return IsNullOrWhiteSpace(value as string) ? Visibility.Collapsed : Visibility.Visible;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class FloatToPercentConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return (float)value! > 0 ? $"{Math.Round(((float)value) * 100, 1)} %" : Empty;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class ArrayToStringConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is string[] strArray)
    {
      return Join(Environment.NewLine, strArray);
    }
    else if (value == null) return Empty;
    else throw new ArgumentException();
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return (((string)value!)!).Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
  }
}

class ResTextConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return value?.ToString()?.Replace("_", "");
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class DownloadOptionIconConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is IDownloadOption downloadOption)
    {
      if (downloadOption is CustomDownload) return PackIconModernKind.Tools;
      else if (downloadOption.IsAudio) return PackIconModernKind.Music;
      else return PackIconModernKind.Video;
    }
    else return null;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class ValueGreaterEqualConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var result = (double)value! >= double.Parse(parameter!.ToString()!);
    return result;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

class RemoveLineBreaksConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return value?.ToString()?.Replace(Environment.NewLine, " ");
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}