using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Incidents.Entity;
using Deffinity.EmailService;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using Incidents.DAL;
using Incidents.StateManager;
public partial class CCAssetsMove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Change change = new Change();
        if (sessionKeys.ChangeControlID != 0)
        {
            change = ChangeHelper.LoadChangesById(sessionKeys.ChangeControlID);

        }
        //if (!IsPostBack)
        //{
           // Master.PageHead = "Change Control";
            if (sessionKeys.ChangeControlID == 0)
            {
                lblPageTitle.InnerText = "Change Control Assets -  Log New Change Control Request";

            }
            else
            {
                lblPageTitle.InnerText = "Change Control Assets - Reference " + sessionKeys.ChangeControlID.ToString();

            }
     //   }
        AssetsId.Assign_Project_Incident = sessionKeys.ChangeControlID;
        AssetsId.AssestProject = 0;
        AssetsId.AssetType_Assign = 3;
      
        AssetsId.GetProtfoliyo = sessionKeys.PortfolioID;
    }
}
