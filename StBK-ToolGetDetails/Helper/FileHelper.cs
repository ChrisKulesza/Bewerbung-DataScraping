using System.Collections.Generic;
using System.IO;

namespace StBK_ToolGetDetails.Helper;

internal class FileHelper
{
  public static IEnumerable<string> ReadFileLineByLineStreamReader(string pathToFile)
  {
    using (var sr = new StreamReader(pathToFile))
    {
      string? line = string.Empty;
      while ((line = sr.ReadLine()) is not null)
      {
        yield return line;
      }
    }
  }
}
