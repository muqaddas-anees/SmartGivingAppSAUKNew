﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class controls_InventoryManagerTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sessionKeys.SID != 1)
        {
            //AdminAccessOnly.Visible = false;
        }
    }
    protected string GetCssClass(int t)
    {
        string CssClass = string.Empty;

        if (t == int.Parse(Request.QueryString["status"].ToString()))
        {
            CssClass = "current1";
        }
        return CssClass;
    }
}
