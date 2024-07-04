using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for HealthCommands
/// </summary>
public class HealthCheckListCommands
{
    public static string cmdInsert = "HealthCheckListInsertion";
    public static string cmdUpdate = "HealthCheckListUpdation";
    public static string cmdDelete = "HealthCheckListDeletion";
    public static string cmdSelectAll = "HealthCheckListSelectAll";
    public static string cmdSelectByTeamID = "HealthCheckListSelectByTeam";
}

public class PortfolioHealthCheckCommands
{
    public static string cmdInsert = "PortfolioHealthCheckListInsertion";
    public static string cmdUpdate = "PortfolioHealthCheckListUpdation";
    public static string cmdDelete = "PortfolioHealthCheckListDeletion";
    public static string cmdSelectAll = "PortfolioHealthCheckListSelectAll";
}

public class HealthCheckListItemsCommands
{
    public static string cmdInsert = string.Empty;
    public static string cmdUpdate = "HealthCheckListItemsUpdation";
    public static string cmdDelete = "HealthCheckListItemsDeletion";
    public static string cmdSelectAll = "HealthCheckListItemsSelectAll";
    public static string cmdSelectAllWithoutID = "HealthCheckListItemsSelectAllWithOutID";
    public static string cmdSelectAllByTitle = "HealthCheckListItemsByTitle";
}

public class HealthCheckRecurrence
{
    public static string cmdInsertUpdate = "HealthCheck_RecurInsertUpdate";
    public static string cmdSelectByID = "HealthCheck_RecurSelectByID";
    public static string cmdIsExist = "HealthCheck_IsExist";
}