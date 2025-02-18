﻿using Avalonia.Controls;
using Avalonia.Media;

namespace Vividl.Models;

public class AccentColorScheme
{
  // these colors are mapped to the according Adonis UI colors
  public Color AccentColor { get; set; }
  public Color AccentHighlightColor { get; set; }
  public Color AccentIntenseHighlightColor { get; set; }
  public Color AccentInteractionColor { get; set; }

  public void ApplyColors(ResourceDictionary resourceDict)
  {
/*
    resourceDict[AColors.AccentColor] = AccentColor;
    resourceDict[AColors.AccentHighlightColor] = AccentHighlightColor;
    resourceDict[AColors.AccentIntenseHighlightColor] = AccentIntenseHighlightColor;
    resourceDict[AColors.AccentIntenseHighlightBorderColor] = AccentIntenseHighlightColor;
    resourceDict[AColors.AccentInteractionColor] = AccentInteractionColor;
    resourceDict[AColors.AccentInteractionBorderColor] = AccentInteractionColor;
  */
  }
}