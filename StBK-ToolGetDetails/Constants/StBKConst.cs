namespace StBK_ToolGetDetails.Constants;

public class StBKConst
{
  // Adelstitel
  public static readonly string[] ArrayAdelsTitel = { "de", "di", "du", "gr.", "la", "le", "mc", "van", "vom", "von", "vor dem", "zu", "zum", "zur" };

  // Alert container DOM
  public const string TxtDetailsPageNone = "Diese Detailseite konnte nicht gefunden werden";
  public const string TxtDetailsPageCompany = "firmenname";
  public const string TxtDetailsPagePerson = "vorname-and-nachname";
  public const string TxtDetailsPageNotFound = "Detail page for RID: { 0 } was not found.";
  public const string TxtDetailsPageWasFound = "Detail page for RID: { 0 } was found. ({ 1 })";

  // Status Detailspage Spalte auf der DB
  public const string TxtNone = "N.A.";
  public const string TxtCompany = "Firma";
  public const string TxtPerson = "Personengesellschaft";
  public const string TxtNoAddessFound = "Keine Adresse vorhanden.";
  public const string TxtNotFound = "Nicht vorhanden.";

  // XPath
  public const string XPathDetailsPageAll = "//*[contains(text(), 'Diese Detailseite konnte nicht')] | //*[@id='firmenname']/@id | //*[@id='vorname-and-nachname']/@id";
  // All details
  public const string XPathAllDetailsCompany = "//*[contains(@class, 'window-middle-setting')][1]";
  // FIRMA
  public const string XPathNameCompany = "//*[@id='firmenname']";
  //public const string XPathAdresseCompany = "//*[@id='adresse']/descendant::text()";
  // PERSON
  public const string XPathNamePerson = "//*[@id='vorname-and-nachname']";
  //public const string XPathNamePerson = "//*[@id='vorname-and-nachname']/text()";
  //public const string XPathAdressePerson = "//*[@id = 'adresse']/descendant::text()";

  // ADRESSE - VORLETZTES UND LETZTES ELEMENT
  public const string XPathAdresseAll = "//*[@id = 'adresse']/text()";
  //public const string XPathAdresseScndLastAndLastElm = "//*[@id = 'adresse']/text()[last() - 1] | //*[@id = 'adresse']/text()[last()]"; // ALT, Jetz Take(^2..)

  // KAMMER
  public const string XPathKammerName = "//*[@id='regionalkammerSection']/p/descendant::span";
  // EXPERIMENTE
  // HIER LANG

  // SQL QUERIES
  public const string QueryGetNextRid = "SELECT Rid FROM dbo.Results WHERE CheckedTs IS NULL";
  public const string QueryInsertRidsFromFile = "INSERT INTO dbo.Results(Rid, InsertedTs) VALUES(@rid, @insertedTs)";
  public const string QueryCountCheckedTsIsNull = "SELECT COUNT(*) FROM dbo.Results WHERE CheckedTs IS NULL";
  public const string QueryUpdateNoDetailsPageStatus = "UPDATE Results SET CheckedTs = @CheckedTs, StatusDetailsPage = @StatusDetailsPage WHERE Rid = ";
  public const string QueryUpdateCompanyDetailsPageStatus = "UPDATE Results SET CheckedTs = @CheckedTs, StatusDetailsPage = @StatusDetailsPage, Name = @Name, Kammer = @Kammer, Adresse = @Adresse WHERE Rid = ";
  public const string QueryUpdatePersonDetailsPageStatus = "UPDATE Results SET CheckedTs = @CheckedTs, StatusDetailsPage = @StatusDetailsPage, Name = @Name, Vorname = @Vorname, Kammer = @Kammer, Adresse = @Adresse WHERE Rid = ";
}
