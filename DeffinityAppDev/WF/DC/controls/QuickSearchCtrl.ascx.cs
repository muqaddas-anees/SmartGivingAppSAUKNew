using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using UserMgt.DAL;
using UserMgt.Entity;
using DC.DAL;
using DC.Entity;
using ProjectMgt.BAL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using PortfolioMgt.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
public partial class DC_controls_QuickSearchCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["S"] != null)
            {
                string Sname = Request.QueryString["S"].ToString();
                Txtsearch.Text = Sname.ToString();
            }
        }
    }
   
    protected void BtnQuickSearch_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {

    }
}