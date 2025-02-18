using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Avalonia.Markup.Xaml;

namespace Vividl.Helpers;

public abstract class EnumExtension(Type enumType) : MarkupExtension
{
  public int SkipCount { get; set; }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return Enum.GetValues(enumType).Cast<object>()
      .Skip(SkipCount)
      .Select(o => new { Value = o, Description = GetEnumDescription((Enum)o) });
  }

  private string GetEnumDescription(Enum value)
  {
    var attributes = value.GetType()
      .GetField(value.ToString())
      ?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

    Debug.Assert(attributes != null, nameof(attributes) + " != null");
    return attributes.Length != 0 ? attributes.First().Description : value.ToString();
  }
}