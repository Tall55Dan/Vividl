﻿using System;
using YoutubeDLSharp.Options;

namespace Vividl.Models;

public static class ExtProvider
{
  public static string GetExtForAudio(AudioConversionFormat audioConversionFormat, string defaultValue = "")
  {
    switch (audioConversionFormat)
    {
      case AudioConversionFormat.Mp3:
        return "mp3";
      case AudioConversionFormat.M4a:
        return "m4a";
      case AudioConversionFormat.Vorbis:
        return "ogg";
      case AudioConversionFormat.Wav:
        return "wav";
      case AudioConversionFormat.Opus:
        return "opus";
      case AudioConversionFormat.Aac:
        return "aac";
      case AudioConversionFormat.Flac:
        return "flac";
      case AudioConversionFormat.Best:
      default:
        // Don't support 'best' because we don't know the extension in advance!
        if (defaultValue == "")
          throw new InvalidOperationException("AudioConversionFormat.Best is not supported.");
        else return defaultValue;
    }
  }

  public static string GetExtForVideo(VideoRecodeFormat videoRecodeFormat, string defaultValue = "")
  {
    switch (videoRecodeFormat)
    {
      case VideoRecodeFormat.Avi:
        return "avi";
      case VideoRecodeFormat.Mp4:
        return "mp4";
      case VideoRecodeFormat.Ogg:
        return "ogg";
      case VideoRecodeFormat.Flv:
        return "flv";
      case VideoRecodeFormat.Webm:
        return "webm";
      case VideoRecodeFormat.Mkv:
        return "mkv";
      case VideoRecodeFormat.None:
      default:
        // Don't support 'None' because we don't know the extension in advance!
        if (defaultValue == "")
          throw new InvalidOperationException("VideoRecodeFormat.None is not supported.");
        else return defaultValue;
    }
  }
}