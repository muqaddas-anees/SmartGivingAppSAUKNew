using InventoryMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Inventory
{
    public partial class NewInventoryManagerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetAllInventoryNames(string pre)
        {
            List<string> allCompanyName = new List<string>();
            using (InventoryDataContext Idc = new InventoryDataContext())
            {
                allCompanyName = (from a in Idc.InventoryManagers
                                  where a.ItemDescription.ToLower().StartsWith(pre.ToLower())
                                  select a.ItemDescription).Take(7).ToList();
            }
            return allCompanyName;
        }
    }
}