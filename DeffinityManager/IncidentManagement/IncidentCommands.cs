
/// <summary>
/// Summary description for IncidentCommands
/// </summary>
public class IncidentCommands
{
	public IncidentCommands()
	{

	}

    public static string cmdInsert = "IncidentInsertion";
    public static string cmdUpdate = "IncidentUpdate";
    public static string cmdDelete = "IncidentDelete";
    public static string cmdSelectAll = "IncidentSelectALL";
    public static string cmdSelectByStatus = "IncidentSelectByStatus";
    public static string cmdSelectIncidentByID = "IncidentByID";
    public static string cmdAddToKB = "IncidentToKB";
    public static string cmdSelectVisibleIncidentsByStatus = "IncidentSelectByStatusVisibility";
    public static string cmdSelectIncidentsByStatusAndType = "IncidentSelectByStatusAndType";
    public static string cmdSelectIncidentByAssignee = "IncidentSelectByStatusCaseAssignee";
}

public class ResourceCommands
{
    public static string cmdInsert = "IncidentResourceInsertion";
    public static string cmdUpdate = "IncidentResourceUpdate";
    public static string cmdDelete = "IncidentResourceDeletion";
    public static string cmdSelectAll = "IncidentResourceSelectAll";
}

public class JournalCommands
{
    public static string cmdInsert = "IncidentJournalInsertion";
    public static string cmdUpdate = "IncidentJournalUpdation";
    public static string cmdDelete = "IncidentJournalDeletion";
    public static string cmdSelectAll = "IncidentJournalSelectAll";
}

public class ServiceCommands
{
    public static string cmdInsert = "IncidentServiceInsertion";
    public static string cmdUpdate = "IncidentServiceUpdation";
    public static string cmdDelete = "IncidentServiceDeletion";
    public static string cmdSelectAll = "IncidentServiceSelectAll";
}

public class AssetCommands
{
    public static string cmdInsert = "IncidentAssetInsertion";
    public static string cmdUpdate = "IncidentAssetUpdation";
    public static string cmdDelete = "IncidentAssetDeletion";
    public static string cmdSelectAll = "IncidentAssetSelectAll";
}

public class ChangeCommands
{
    public static string cmdInsert = "IncidentChangeInsertion";
    public static string cmdUpdate = "IncidentChangeUpdation";
    public static string cmdDelete = "IncidentChangeDeletion";
    public static string cmdSelectAll = "IncidentChangeSelectAll";
    public static string cmdSelectById = "IncidentChangeSelectById";
    public static string cmdSelectAllByPortfolio = "IncidentChangeSelectByPortfolio";
}

public class RiskCommands
{
    public static string cmdInsert = "IncidentRiskInsertion";
    public static string cmdUpdate = "IncidentRiskUpdation";
    public static string cmdDelete = "IncidentRiskDeletion";
    public static string cmdSelectAll = "IncidentRiskSelectAll";
    public static string cmdSelectById = "IncidentRiskSelectById";
}

public class TaskCommands
{
    public static string cmdInsert = "IncidentTaskInsertion";
    public static string cmdUpdate = "IncidentTaskUpdation";
    public static string cmdDelete = "IncidentTaskDeletion";
    public static string cmdSelectAll = "IncidentTaskSelectAll";
    public static string cmdSelectById = "IncidentTaskSelectById";
}

public class ApprovalCommands
{
    public static string cmdInsert = "IncidentApprovalInsertion";
    public static string cmdUpdate = "IncidentApprovalUpdation";
    public static string cmdDelete = "IncidentApprovalDeletion";
    public static string cmdSelectAll = "IncidentApprovalSelectAll";
    public static string cmdSelectById = "IncidentApprovalSelectById";
}