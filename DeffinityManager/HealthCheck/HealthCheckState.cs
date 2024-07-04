using System.Web;
using Health.Entity;
using Health.DAL;

/// <summary>
/// Summary description for HealthCheckState
/// </summary>
namespace Health.StateManager
{
    public class HealthCheckListState
    {
        public static HealthCheckList HealthCheckItemsSaver
        {
            get
            {
                if (HttpContext.Current.Session["HealthCheckList"] != null)
                    return (HealthCheckList)HttpContext.Current.Session["HealthCheckList"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["HealthCheckList"] = value;
            }
        }

        public static HealthCheckListCollection HealthCheckItemsCache
        {
            get
            {
                if (HttpContext.Current.Cache["HealthCheckItemsCollection"] != null)
                    return (HealthCheckListCollection)HttpContext.Current.Cache["HealthCheckItemsCollection"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["HealthCheckItemsCollection"] = value;
            }
        }

        public static void ClearHealthCheckItemsCache()
        {
            if (HttpContext.Current.Cache["HealthCheckItemsCollection"] != null)
                HttpContext.Current.Cache.Remove("HealthCheckItemsCollection");
        }

    }

    public class PortfolioHealthCheckState
    {
        public static PortfolioHealthCheck PortfolioHealthCheckStateSaver
        {
            get
            {
                if (HttpContext.Current.Session["PortfolioHealthCheck"] != null)
                    return (PortfolioHealthCheck)HttpContext.Current.Session["PortfolioHealthCheck"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["PortfolioHealthCheck"] = value;
            }
        }

        public static PortfolioHealthCheckCollection PortfolioHealthCheckCache
        {
            get
            {
                if (HttpContext.Current.Cache["PortfolioHealthCheckCollection"] != null)
                    return (PortfolioHealthCheckCollection)HttpContext.Current.Cache["PortfolioHealthCheckCollection"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["PortfolioHealthCheckCollection"] = value;
            }
        }

        public static void ClearPortfolioHealthCheckCache()
        {
            if (HttpContext.Current.Cache["PortfolioHealthCheckCollection"] != null)
                HttpContext.Current.Cache.Remove("PortfolioHealthCheckCollection");
        }

    }

    public class HealthCheckListItemsState
    {
        public static HealthCheckListItems HealthCheckListItemsStateSaver
        {
            get
            {
                if (HttpContext.Current.Session["HealthCheckListItems"] != null)
                    return (HealthCheckListItems)HttpContext.Current.Session["HealthCheckListItems"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["HealthCheckListItems"] = value;
            }
        }

        public static HealthCheckListItemsCollection HealthCheckListItemsCache
        {
            get
            {
                if (HttpContext.Current.Cache["HealthCheckListItemsCollection"] != null)
                    return (HealthCheckListItemsCollection)HttpContext.Current.Cache["HealthCheckListItemsCollection"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Cache["HealthCheckListItemsCollection"] = value;
            }
        }

        public static void ClearHealthCheckListItemsCache()
        {
            if (HttpContext.Current.Cache["HealthCheckListItemsCollection"] != null)
                HttpContext.Current.Cache.Remove("HealthCheckListItemsCollection");
        }

    }
}