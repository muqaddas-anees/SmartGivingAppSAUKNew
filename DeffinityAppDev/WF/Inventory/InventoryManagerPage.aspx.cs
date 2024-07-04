using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using System.Linq;

public partial class InventoryManagerPage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
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

