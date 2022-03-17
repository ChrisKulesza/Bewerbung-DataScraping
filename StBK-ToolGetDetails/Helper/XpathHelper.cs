using HtmlAgilityPack;
using StBK_ToolGetDetails.Constants;
using StBK_ToolGetDetails.Models;

namespace StBK_ToolGetDetails.Helper;

public class XpathHelper
{
  /// <summary>
  /// Extracting the address from an html document of the Berlin Chamber of Tax Consultants.
  /// </summary>
  /// <param name="response">The html document to be filtered.</param>
  /// <param name="xPath">XPath selector.</param>
  /// <param name="delimiter">A string that delimits the substrings in this string.</param>
  /// <returns><c><see cref="string"/></c> object if the address was found in the html document.; otherwise, <c>"Nicht vorhanden."</c></returns>
  private static string GetAddressFromResponse(string response, string xPath, string delimiter)
  {
    var doc = new HtmlDocument();
    doc.LoadHtml(response);

    var selectedNodes = doc.DocumentNode
        .SelectNodes(xPath);

    if (selectedNodes is null)
      return StBKConst.TxtNoAddessFound;

    return selectedNodes.ConcatAddressFromColletion(delimiter);
  }

  /// <summary>
  /// Extracting the address from an html document of the Berlin Chamber of Tax Consultants.
  /// </summary>
  /// <param name="response">The html document to be filtered.</param>
  /// <param name="xPath">XPath selector.</param>
  /// <param name="delimiter">A string that delimits the substrings in this string.</param>
  /// <returns><c><see cref="string"/></c> object if the address was found in the html document.; otherwise, <c>"Nicht vorhanden."</c></returns>
  private static string GetNameFromResponse(string response, string xPath, string delimiter)
  {
    var doc = new HtmlDocument();
    doc.LoadHtml(response);

    var selectedNodes = doc.DocumentNode
        .SelectNodes(xPath);

    if (selectedNodes is null)
    {
      return StBKConst.TxtNotFound;
    }

    return selectedNodes.SplitStringFromColletion(delimiter);
  }

  /// <summary>
  /// Extract the last last name from a html document.
  /// </summary>
  /// <param name="response">The html document to be filtered.</param>
  /// <param name="xPath">XPath selector.</param>
  /// <param name="delimiter">A string that delimits the substrings in this string.</param>
  /// <returns><c><see cref="string"/></c> object if the address was found in the html document.; otherwise, <c>"Nicht vorhanden."</c></returns>
  private static string GetNachnameFromResponse(string response, string xPath, string delimiter)
  {
    var doc = new HtmlDocument();
    doc.LoadHtml(response);

    var selectedNodes = doc.DocumentNode
        .SelectNodes(xPath);

    if (selectedNodes is null)
    {
      return StBKConst.TxtNotFound;
    }

    return selectedNodes.SplitNachnameFromCollection(delimiter);
  }

  /// <summary>
  /// Extract the first name from a html document.
  /// </summary>
  /// <param name="response">The html document to be filtered.</param>
  /// <param name="xPath">XPath selector.</param>
  /// <param name="delimiter">A string that delimits the substrings in this string.</param>
  /// <returns><c><see cref="string"/></c> object if the address was found in the html document.; otherwise, <c>"Nicht vorhanden."</c></returns>
  private static string GetVornameFromResponse(string response, string xPath, string delimiter)
  {
    var doc = new HtmlDocument();
    doc.LoadHtml(response);

    var selectedNodes = doc.DocumentNode
        .SelectNodes(xPath);

    if (selectedNodes is null)
    {
      return StBKConst.TxtNotFound;
    }

    return selectedNodes.SplitVornameFromCollection(delimiter);
  }

  /// <summary>
  /// Extracting a string from an html document.
  /// </summary>
  /// <param name="response">The html document to be filtered.</param>
  /// <param name="xPath">XPath selector.</param>
  /// <param name="delimiter">A string that delimits the substrings in this string.</param>
  /// <returns><c><see cref="string"/></c> object if the address was found in the html document.; otherwise, <c>"Nicht vorhanden."</c></returns>
  private static string GetBrTaggedNodeFromResponse(string response, string xPath, string delimiter)
  {
    var doc = new HtmlDocument();
    doc.LoadHtml(response);

    var selectedNodes = doc.DocumentNode
                .SelectNodes(xPath);

    if (selectedNodes is null)
    {
      return StBKConst.TxtNotFound;
    }

    return selectedNodes.SplitStringFromColletion(delimiter);
  }

  /// <summary>
  /// Reads all personal data from the server response.
  /// </summary>
  /// <param name="response">The answer of the server.</param>
  /// <returns>A <see cref="Person"/> object</returns>
  public static Person GetPersonDataResponse(string response)
  {
    var person = new Person
    {
      Vorname = GetVornameFromResponse(response, StBKConst.XPathNamePerson, " "),
      Nachname = GetNachnameFromResponse(response, StBKConst.XPathNamePerson, ""),
      Kammer = GetBrTaggedNodeFromResponse(response, StBKConst.XPathKammerName, " "),
      Adresse = GetAddressFromResponse(response, StBKConst.XPathAdresseAll, ", ")
    };

    return person;
  }

  /// <summary>
  /// Reads all company data from the server response.
  /// </summary>
  /// <param name="response">The answer of the server.</param>
  /// <returns>A <see cref="Company"/> object</returns>
  public static Company GetCompanyDataFromResponse(string responseBody)
  {
    return new Company
    {
      Name = GetNameFromResponse(responseBody, StBKConst.XPathNameCompany, " "),
      Kammer = GetBrTaggedNodeFromResponse(responseBody, StBKConst.XPathKammerName, " "),
      Adresse = GetAddressFromResponse(responseBody, StBKConst.XPathAdresseAll, ", ")
    };
  }
}
