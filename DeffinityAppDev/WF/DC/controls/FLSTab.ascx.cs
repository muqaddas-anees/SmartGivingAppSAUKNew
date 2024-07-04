using DeffinityManager.Portfolio.BAL;
using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_controls_FLSTab : System.Web.UI.UserControl
{
    string[] array_path = {"FLSForm.aspx","SDdetails_frm.aspx","DCAssignResource.aspx","DCInvoiceList.aspx",
                              "DCQuotationCompare.aspx","DCFeedback.aspx","SDdocuments_frm.aspx",
                              "SDnotification_frm.aspx","DCInventory.aspx","DCServiceUnits.aspx","DCFormNew.aspx","DCChat.aspx",
        "DCBOM.aspx","DCEquipment.aspx","HVACDiagnostics.aspx","DCMaintenance.aspx","DCAssignSalesRep.aspx","DCDonation.aspx","DCCost.aspx","JobTargets.aspx","ProgressView.aspx"};
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            hidPortfolioID.Value = sessionKeys.PortfolioID.ToString();
            if (!IsPostBack)
            {
                //set the session customer valuew
                sessionKeys.IncidentID = QueryStringValues.CallID;
                hsdid.Value = sessionKeys.IncidentID.ToString();
                checkDlt();
                Check_Visibility();

                //if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                //{
                //    link_return.HRef = "~/WF/DC/FLSResourceList.aspx?type=FLS";
                //}
                //else
                //{
                //    link_return.HRef = "~/WF/DC/FLSJlist.aspx?type=FLS";
                //    ////}
                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Check_Visibility()
    {
        //link_BOM.Visible = false;
        //link_equipment.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Equipment);
        //link_assigntechnician.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.SchedulinginJobs);
        //link_Inventory.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Inventory);
        //link_Forms.Visible = PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.Forms);
        //link_assigntechnician.Visible = false;
        //if (sessionKeys.SID == 4)
        //{
        //    //link_assigntechnician.Visible = false;
        //    link_BOM.Visible = false;
        //}

       // link_equipment.Visible = sessionKeys.PayStatus;
       

        //var p = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
        //if(p != null)
        //{
        //    link_BOM.Visible = p.EnableInternalCost.HasValue ? p.EnableInternalCost.Value : false;
        //}
            
    }
  //DLT Database list
        //136	DC - FLS - FLS 	1
        //137	DC - FLS - FLS Details	1
        //138	DC - FLS - Assign Team	1
        //139	DC - FLS - Services	1
        //140	DC - FLS - Moves	1
        //141	DC - FLS - Service Units	1
        //142	DC - FLS - Inventory	1
    //End list
    private void checkDlt()
    {
        try
        {
            string[] str = PermissionManager.GetFeatures();
            if (!Page.IsPostBack)
            {
                //34 ToolboxDataAttribute 43
                //tab_Fls_overview.Visible = Convert.ToBoolean(str[136]);
                //tab_assigneteam.Visible = Convert.ToBoolean(str[138]);
                //tab_services.Visible = Convert.ToBoolean(str[139]);
                //tab_moves.Visible = false;// Convert.ToBoolean(str[140]);
                //tab_service_units.Visible = Convert.ToBoolean(str[141]);
                //tab_inventory.Visible = false;// Convert.ToBoolean(str[142]);
                //tab_form.Visible = false;
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected string getUrl(int i)
    {
        string[] array_path = {"FLSForm.aspx","SDdetails_frm.aspx","DCAssignResource.aspx","DCInvoiceList.aspx",
                              "DCQuotationCompare.aspx","DCFeedback.aspx","SDdocuments_frm.aspx",
                              "SDnotification_frm.aspx","DCInventory.aspx","WF/DC/DCServiceUnits.aspx","DCFormNew.aspx",
            "DCChat.aspx","DCBOM.aspx","DCEquipment.aspx","HVACDiagnostics.aspx",
            "DCMaintenance.aspx","DCAssignSalesRep.aspx","DCAssignSalesRep.aspx","DCDonation.aspx","DCCost.aspx","JobTargets.aspx","ProgressView.aspx"};

        if (Request.QueryString["callid"] != null)
        {
            if (i == 4)
                return array_path[i] + "?CCID=" + QueryStringValues.CCID + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID+ "&Option=0&tab=quote";
            else
            return array_path[i] + "?CCID=" + QueryStringValues.CCID + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID;
        }
        else
            if (i == 0)
        {
            return array_path[i];
        }
        else
            return "#";
      
    }
    protected string GetCssClass(int i)
    {

        string rtValue = string.Empty;
        if (i < array_path.Length)
        {
            if ((Request.Url.ToString().ToLower()).Contains(array_path[i].ToLower()) == true)
            {
                rtValue = "current1";

            }
            string strTemp = string.Empty;
            if (i == 5)
            {
                strTemp = "DCAssets.aspx";
                if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                {
                    rtValue = "current1";
                }
                strTemp = "DCDashboard.aspx";
                if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                {
                    rtValue = "current1";
                }
                strTemp = "DCDealerVoice.aspx";
                if ((Request.Url.ToString().ToLower()).Contains(strTemp.ToLower()) == true)
                {
                    rtValue = "current1";
                }
                strTemp = "SDdatacentre.aspx";
               
            }
        }
        return rtValue;
    }
}