using StBK_ToolGetDetails.Constants;
using StBK_ToolGetDetails.Models;
using System;
using System.Collections.Generic;

namespace StBK_ToolGetDetails.Helper;

internal class DbHelper
{
  /// <summary>
  /// Detailspage N.A.
  /// </summary>
  /// <param name="rid"></param>
  /// <param name="status"></param>
  public static void UpdateNoDetailsPageStatusToDb(DAL.StbkConnection connection, string rid, string status)
  {
    // Query
    var query = $"{ StBKConst.QueryUpdateNoDetailsPageStatus }'{ rid }'";

    // sql parameter
    Dictionary<string, object> parameters = new()
    {
      { "CheckedTs", DateTime.Now },
      { "StatusDetailsPage", status }
    };

    connection.ExecuteNonQueryFromQuery(query, parameters);
  }

  /// <summary>
  /// Company detailspage
  /// </summary>
  /// <param name="rid"></param>
  /// <param name="status"></param>
  public static void UpdateCompanyDetailsPageStatusToDb(DAL.StbkConnection connection, string rid, string status, Company company)
  {
    // Query
    var query = $"{ StBKConst.QueryUpdateCompanyDetailsPageStatus }'{ rid }'";

    // sql parameter
    Dictionary<string, object> parameters = new()
    {
      { "CheckedTs", DateTime.Now },
      { "StatusDetailsPage", status },
      { "Name", company.Name },
      { "Kammer", company.Kammer },
      { "Adresse", company.Adresse }
    };

    connection.ExecuteNonQueryFromQuery(query, parameters);
  }

  /// <summary>
  /// Person detailspage
  /// </summary>
  /// <param name="rid"></param>
  /// <param name="status"></param>
  public static void UpdatePersonDetailsPageStatusToDb(DAL.StbkConnection connection, string rid, string status, Person person)
  {
    // Query
    var query = $"{ StBKConst.QueryUpdatePersonDetailsPageStatus }'{ rid }'";

    // sql parameter
    Dictionary<string, object> parameters = new()
    {
      { "CheckedTs", DateTime.Now },
      { "StatusDetailsPage", status },
      { "Name", person.Nachname },
      { "Vorname", person.Vorname },
      { "Kammer", person.Kammer },
      { "Adresse", person.Adresse }
    };

    connection.ExecuteNonQueryFromQuery(query, parameters);
  }

  /// <summary>
  /// Writes all rids from a given file to the database.
  /// </summary>
  /// <param name="connection">The database connection of the DAL.</param>
  /// <param name="rid"></param>
  public static void InsertRidToDb(DAL.StbkConnection connection, string rid)
  {
    // query
    var query = StBKConst.QueryInsertRidsFromFile;

    // sql parameter
    Dictionary<string, object> paramter = new()
    {
      { "Rid", rid },
      { "InsertedTs", DateTime.Now }
    };

    connection.ExecuteNonQueryFromQuery(query, paramter);
  }
}
