using System.Web;
using Incidents.Entity;

/// <summary>
/// Summary description for stateClass
/// </summary>
namespace Incidents.StateManager
{
    public class IncidentState
    {
        public IncidentState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Incident IncidentSaver
        {
            get
            {
                if (HttpContext.Current.Session["Incident"] != null)
                    return (Incident)HttpContext.Current.Session["Incident"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Incident"] = value;
            }
        }

        /// <summary>
        /// Adds the incident details into the cache
        /// </summary>
        public static IncidentCollection IncidentCache
        {
            get
            {
                if (HttpContext.Current.Cache["Incident"] != null)
                    return (IncidentCollection)HttpContext.Current.Cache["Incident"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Incident"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearIncidentCache()
        {
            if (HttpContext.Current.Cache["Incident"] != null)
                HttpContext.Current.Cache.Remove("Incident");
        }
    }

    public class ResourceState
    { 
        public ResourceState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Resource ResourceSaver
        {
            get
            {
                if (HttpContext.Current.Session["Resource"] != null)
                    return (Resource)HttpContext.Current.Session["Resource"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Resource"] = value;
            }
        }

        /// <summary>
        /// Adds the incident details into the cache
        /// </summary>
        public static ResourceCollection ResourceCache
        {
            get
            {
                if (HttpContext.Current.Cache["Resource"] != null)
                    return (ResourceCollection)HttpContext.Current.Cache["Resource"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Resource"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearResourceCache()
        {
            if (HttpContext.Current.Cache["Resource"] != null)
                HttpContext.Current.Cache.Remove("Resource");
        }
    }

    public class JournalState
    {
        public JournalState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Journal JournalSaver
        {
            get
            {
                if (HttpContext.Current.Session["Journal"] != null)
                    return (Journal)HttpContext.Current.Session["Journal"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Journal"] = value;
            }
        }

        /// <summary>
        /// Adds the Journal details into the cache
        /// </summary>
        public static JournalCollection JournalCache
        {
            get
            {
                if (HttpContext.Current.Cache["Journal"] != null)
                    return (JournalCollection)HttpContext.Current.Cache["Journal"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Journal"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearJournalCache()
        {
            if (HttpContext.Current.Cache["Journal"] != null)
                HttpContext.Current.Cache.Remove("Journal");
        }
    }

    public class ServiceState
    {
        public ServiceState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Service ServiceSaver
        {
            get
            {
                if (HttpContext.Current.Session["Service"] != null)
                    return (Service)HttpContext.Current.Session["Service"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Service"] = value;
            }
        }

        /// <summary>
        /// Adds the Service details into the cache
        /// </summary>
        public static ServiceCollection ServiceCache
        {
            get
            {
                if (HttpContext.Current.Cache["Service"] != null)
                    return (ServiceCollection)HttpContext.Current.Cache["Service"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Service"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearServiceCache()
        {
            if (HttpContext.Current.Cache["Service"] != null)
                HttpContext.Current.Cache.Remove("Service");
        }
    }

    public class AssetState
    {
        public AssetState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Asset AssetSaver
        {
            get
            {
                if (HttpContext.Current.Session["Asset"] != null)
                    return (Asset)HttpContext.Current.Session["Asset"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Asset"] = value;
            }
        }

        /// <summary>
        /// Adds the Asset details into the cache
        /// </summary>
        public static AssetCollection AssetCache
        {
            get
            {
                if (HttpContext.Current.Cache["Asset"] != null)
                    return (AssetCollection)HttpContext.Current.Cache["Asset"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Asset"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearAssetCache()
        {
            if (HttpContext.Current.Cache["Asset"] != null)
                HttpContext.Current.Cache.Remove("Asset");
        }
    }

    public class ChangeState
    {
        public ChangeState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Change ChangeSaver
        {
            get
            {
                if (HttpContext.Current.Session["Change"] != null)
                    return (Change)HttpContext.Current.Session["Change"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Change"] = value;
            }
        }

        /// <summary>
        /// Adds the Change details into the cache
        /// </summary>
        public static ChangeCollection ChangeCache
        {
            get
            {
                if (HttpContext.Current.Cache["Change"] != null)
                    return (ChangeCollection)HttpContext.Current.Cache["Change"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Change"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearChangeCache()
        {
            if (HttpContext.Current.Cache["Change"] != null)
                HttpContext.Current.Cache.Remove("Change");
        }
    }

    public class RiskState
    {
        public RiskState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Risk RiskSaver
        {
            get
            {
                if (HttpContext.Current.Session["Risk"] != null)
                    return (Risk)HttpContext.Current.Session["Risk"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Risk"] = value;
            }
        }

        /// <summary>
        /// Adds the Change details into the cache
        /// </summary>
        public static RiskCollection RiskCache
        {
            get
            {
                if (HttpContext.Current.Cache["Risk"] != null)
                    return (RiskCollection)HttpContext.Current.Cache["Risk"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Risk"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearRiskCache()
        {
            if (HttpContext.Current.Cache["Risk"] != null)
                HttpContext.Current.Cache.Remove("Risk");
        }
    }

    public class TaskState
    {
        public TaskState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Task TaskSaver
        {
            get
            {
                if (HttpContext.Current.Session["Task"] != null)
                    return (Task)HttpContext.Current.Session["Task"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Task"] = value;
            }
        }

        /// <summary>
        /// Adds the Change details into the cache
        /// </summary>
        public static TaskCollection TaskCache
        {
            get
            {
                if (HttpContext.Current.Cache["Task"] != null)
                    return (TaskCollection)HttpContext.Current.Cache["Task"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Task"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearTaskCache()
        {
            if (HttpContext.Current.Cache["Task"] != null)
                HttpContext.Current.Cache.Remove("Task");
        }
    }

    public class ApprovalState
    {
        public ApprovalState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Saves the data into the session, which could be retrieved later for filling the form
        /// </summary>
        public static Approval ApprovalSaver
        {
            get
            {
                if (HttpContext.Current.Session["Approval"] != null)
                    return (Approval)HttpContext.Current.Session["Approval"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Approval"] = value;
            }
        }

        /// <summary>
        /// Adds the Change details into the cache
        /// </summary>
        public static ApprovalCollection ApprovalCache
        {
            get
            {
                if (HttpContext.Current.Cache["Approval"] != null)
                    return (ApprovalCollection)HttpContext.Current.Cache["Approval"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["Approval"] = value;
            }
        }

        /// <summary>
        /// Clears the Cached IncidentsCollection
        /// </summary>
        public static void ClearApprovalCache()
        {
            if (HttpContext.Current.Cache["Approval"] != null)
                HttpContext.Current.Cache.Remove("Approval");
        }
    }  
}    

