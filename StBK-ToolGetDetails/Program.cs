using NLog;
using static StBK_ToolGetDetails.Constants.StBKConst;
using StBK_ToolGetDetails.DAL;
using StBK_ToolGetDetails.Enums;
using StBK_ToolGetDetails.Helper;
using System;
using System.Threading.Tasks;
using static StBK_ToolGetDetails.Helper.HttpHelper;

namespace StBK_ToolGetDetails;

internal class Program
{
  private static readonly TimeSpan _delay = TimeSpan.FromSeconds(2);
  private static Logger _logger = LogManager.GetCurrentClassLogger();

  //// DEV SECTION
  private static string _companyRid = "10-05-8E-D4-7D-56-83-2E-D5-F9-55-60-E0-93-D2-79";
  //private static string _personRid = "BB-C5-DF-FF-5D-B0-F5-62-F3-1B-EC-11-37-28-CD-75";
  //private static string _personRidLangeAdresse = "00-CC-7F-B4-D1-8A-09-68-72-A7-76-7F-6E-C5-B0-C0"; // LANGE Adresse
  //private static string _personRidKeineAdresse = "06-4D-3C-41-CB-F9-72-04-57-C4-5C-1B-0B-2B-FE-7F"; // KEINE Adresse PROBLEM
  //private static string _personRidZweiVornamen = "5D-86-BB-8E-9E-27-5F-02-BD-10-D8-8E-87-EC-AB-11"; // ZWEI Vornamen
  //private static string _personRidAdelsTitel = "B4-01-26-87-2D-FE-18-4E-0E-BD-41-C3-DD-EC-38-AE"; // Adelstitel

  static async Task Main(string[] args)
  {
    await RunApplication();
    //await WriteFileToDb();
  }

  // ToDo:    Finale Implementierung NLog (Konsole, Datei, Email Alerting)
  // ToDo:    Implementierung CommandLineParser Package
  // ToDo:    Adelstitel handling (von, zu, zum) mit dem Nachname in die DB schreiben, aktuell beim Vornamen
  public static async Task RunApplication()
  {
    var url = AppSettingsHelper.ReadSettings("HttpSettings", "HostUrl");
    var nextRidNotChecked = String.Empty;

    // NLog Formatter - Remove quotes from structured logging
    var oldFormatter = NLog.Config.ConfigurationItemFactory.Default.ValueFormatter;
    var newFormatter = new NLogExtensions.OverrideValueFormatter(oldFormatter);
    newFormatter.StringWithQuotes = false;
    NLog.Config.ConfigurationItemFactory.Default.ValueFormatter = newFormatter;

    await using var connection = await StbkConnection.CreateAsync();

    try
    {
      // OLD
      // Get next rid from db
      //var ds = GetNextRidFromDb(connection, StBKConst.QueryGetNextRid);
      //var reader = await connection.GetDataSet(StBKConst.QueryGetNextRid);

      //await foreach (var entry in connection.GetDataSet(StBKConst.QueryGetNextRid))
      //{
      // DEV SECTION
      var responseBody = await GetRidDetailsPageContent(url, _companyRid, _delay);
      //var responseBody = await GetRidDetailsPageContent(url, _personRid, _delay);
      //var responseBody = await GetRidDetailsPageContent(url, _personRidLangeAdresse, _delay);
      //var responseBody = await GetRidDetailsPageContent(url, _personRidKeineAdresse, _delay);
      //var responseBody = await GetRidDetailsPageContent(url, _personRidZweiVornamen, _delay);
      //var responseBody = await GetRidDetailsPageContent(url, _personRidAdelsTitel, _delay);

      //nextRidNotChecked = entry["Rid"].ToString();
      //var responseBody = await GetRidDetailsPageContent(url, nextRidNotChecked, _delay);
      var detailPageContent = ExamineResponseContentType(responseBody, XPathDetailsPageAll, "id");

      switch (detailPageContent)
      {
        // No details page method
        case SearchResultEnum.DetailsPageNone:
          _logger.Info(TxtDetailsPageNotFound, _companyRid);
          DbHelper.UpdateNoDetailsPageStatusToDb(connection, nextRidNotChecked, TxtNone);
          break;
        // Company details page method
        case SearchResultEnum.DetailsPageCompany:
          _logger.Info(TxtDetailsPageWasFound, _companyRid, TxtCompany);
          var companyData = XpathHelper.GetCompanyDataFromResponse(responseBody);
          DbHelper.UpdateCompanyDetailsPageStatusToDb(connection, nextRidNotChecked, TxtCompany, companyData);
          break;
        // Person details page method
        default:
          _logger.Info(TxtDetailsPageWasFound, _companyRid, TxtPerson);
          var personData = XpathHelper.GetPersonDataResponse(responseBody);
          DbHelper.UpdatePersonDetailsPageStatusToDb(connection, nextRidNotChecked, TxtPerson, personData);
          break;
      }
      //}
    }
    catch (Exception ex) { _logger.Fatal(ex, "Stopped program because of exception"); }
    finally
    {
      LogManager.Shutdown();
    }

    return;
  }

  /// <summary>
  /// Schreibt alle Rids aus der Datei in die Datenbank
  /// </summary>
  /// <returns></returns>
  public static async Task WriteFileToDb()
  {
    await using var connection = await StbkConnection.CreateAsync();

    var line = FileHelper.ReadFileLineByLineStreamReader(@"C:\Users\ChristianKuleszaWAIT\source\repos\BEWERBUNG\DataScraping\StBK-ToolGetDetails\ResultsRIDs.txt");

    _logger.Info("Write file contents to the database.");
    foreach (var rid in line)
    {
      DbHelper.InsertRidToDb(connection, rid);
    }

    return;
  }
}
