using StBK_ToolGetDetails.Constants;
using System.Text.RegularExpressions;

namespace StBK_ToolGetDetails.Helper;

public class RegExHelper
{
  // Gibt alles ab dem übergebenen Suchstring zurück, inkl. dem Suchstring
  // Pattern (Case insensitive)
  // @$"(?i)\s+{ toSearchFor }.*$"
  // @"\s\+{ toSearchFor }.*$", RegexOptions.IgnoreCase

  private static readonly string[] tmpArr = StBKConst.ArrayAdelsTitel;

  public static void IfAdelsTitelExists(string fullName, string xPath)
  {
    foreach (var prefix in tmpArr)
    {
      System.Console.WriteLine(prefix);
    }

    //return "";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="stringToCheck"></param>
  /// <param name="subStringToSearchFor"></param>
  /// <returns></returns>
  public static bool CheckIfStringContainsSubstring(string stringToCheck, string subStringToSearchFor)
  {
    var tmp = Regex.Match(stringToCheck, $"\\b{subStringToSearchFor}\\b").ToString().Trim();

    if (tmp == subStringToSearchFor)
    {
      return true;
    }

    return false;
  }
}
