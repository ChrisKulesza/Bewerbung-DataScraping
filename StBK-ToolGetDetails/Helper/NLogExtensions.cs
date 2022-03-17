using NLog;
using System;

namespace StBK_ToolGetDetails.Helper;

internal class NLogExtensions
{
  public class OverrideValueFormatter : IValueFormatter
  {
    private IValueFormatter _originalFormatter;

    public bool StringWithQuotes { get; set; }

    public OverrideValueFormatter(IValueFormatter originalFormatter)
    {
      _originalFormatter = originalFormatter;
    }

    public bool FormatValue(object value, string format, NLog.MessageTemplates.CaptureType captureType, IFormatProvider formatProvider, System.Text.StringBuilder builder)
    {
      if (!StringWithQuotes && captureType == NLog.MessageTemplates.CaptureType.Normal)
      {
        switch (Convert.GetTypeCode(value))
        {
          case TypeCode.String:
            {
              builder.Append((string)value);
              return true;
            }
          case TypeCode.Char:
            {
              builder.Append((char)value);
              return true;
            }
          case TypeCode.Empty:
            return true;  // null becomes empty string
        }
      }

      return _originalFormatter.FormatValue(value, format, captureType, formatProvider, builder);
    }
  }
}
