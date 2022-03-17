using HtmlAgilityPack;
using StBK_ToolGetDetails.Enums;
using StBK_ToolGetDetails.Service;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static StBK_ToolGetDetails.Constants.StBKConst;

namespace StBK_ToolGetDetails.Helper;

public class HttpHelper
{
  // Getting retry settings
  private static readonly int _retryCount = int.Parse(AppSettingsHelper.ReadSettings("RetrySettings", "RetryCount"));
  private static readonly double _retryDelay = double.Parse(AppSettingsHelper.ReadSettings("RetrySettings", "RetryDelay"));

  // HttpClient including RetryHandler
  private static readonly HttpClient _client = new(new RetryHandler(new HttpClientHandler(), _retryCount, _retryDelay));

  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  /// <param name="rid"></param>
  /// <param name="delay"></param>
  /// <returns></returns>
  public static async Task<string> GetRidDetailsPageContent(string url, string rid, TimeSpan delay, CancellationToken cancellationToken = default)
  {
    await Task.Delay(delay, cancellationToken);

    try
    {
      var response = await _client.GetAsync($"{ url }/{ rid }");
      return await response.Content.ReadAsStringAsync();
    }
    catch (Exception ex) when (ex is HttpRequestException or AggregateException)
    {
      return null;
    }

  }

  /// <summary>
  /// Examines whether the response is a detail page of a company, an individual, or if the detail page no longer exists.
  /// </summary>
  /// <param name="data">Host response.</param>
  /// <param name="xPath">XML selector for the host response.</param>
  /// <param name="attribute">Attribute whose value is searched for.</param>
  /// <returns>An enum</returns>
  public static SearchResultEnum ExamineResponseContentType(string data, string xPath, string attribute)
  {
    string? requestBody = string.Empty;
    var doc = new HtmlDocument();
    doc.LoadHtml(data);

    try
    {
      requestBody = doc.DocumentNode.SelectSingleNode(xPath)?.Attributes[attribute]?.Value;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }

    return requestBody switch
    {
      TxtDetailsPageCompany => SearchResultEnum.DetailsPageCompany,
      TxtDetailsPagePerson => SearchResultEnum.DetailsPagePerson,
      _ => SearchResultEnum.DetailsPageNone
    };
  }
}
