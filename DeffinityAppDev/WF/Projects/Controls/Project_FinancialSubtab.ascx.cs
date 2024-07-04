using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class controls_Project_FinancialSubtab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["project"] != null)
        {
            string pref = Request.QueryString["project"].ToString();
            lbtnGeneral.NavigateUrl = "~/WF/Projects/ProjectTracker_General.aspx?project=" + pref;
            lbtnActuals.NavigateUrl = "~/WF/Projects/ProjectTracker_Actuals.aspx?project=" + pref;
            lbtnLaborTracker.NavigateUrl = "~/WF/Projects/ProjectLabourTracker.aspx?project=" + pref;
            lbtnMaterialsTracker.NavigateUrl = "~/WF/Projects/ProjectMaterialTracker.aspx?project=" + pref;
            lbtnMiscTracker.NavigateUrl = "~/WF/Projects/ProjectMiscTracker.aspx?project=" + pref;
            lbtnExpenseTracker.NavigateUrl = "~/WF/Projects/ProjectExpenseTracker.aspx?project=" + pref;
            lbtnPMHours.NavigateUrl = "~/WF/Projects/ProjectPMHours.aspx?project=" + pref;
            lbtnInvoicing.NavigateUrl = "~/WF/Projects/ProjectTracker_Invoicing.aspx?project=" + pref;
            lbtnVariation.NavigateUrl = "~/WF/Projects/ProjectTracker_Variations.aspx?project=" + pref;
            //lbtnPO.NavigateUrl = "~/WF/Projects/ProjectFinancial_CustomerPO.aspx?project=" + pref;
            lbtnStaffHours.NavigateUrl = "~/WF/Projects/ProjectStaffHours.aspx?project=" + pref;
            SetTabColor();
            ProjectClass_Visibility();
        }


    }
    #region DLT
    private void ProjectClass_Visibility()
    {
        try
        {
            //Check Project invoice feature is enabled
            string[] str = PermissionManager.GetFeatures();
            if (!Page.IsPostBack)
            {
                lbtnInvoicing.Visible = Convert.ToBoolean(str[98]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
    private void SetTabColor()
    {
        //if ((Request.Url.ToString().ToLower()).Contains("projecttracker_general.aspx") == true)
        //{
        //    lbtnGeneral.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projecttracker_actuals.aspx") == true)
        //{
        //    lbtnActuals.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projecttracker_invoicing.aspx") == true)
        //{
        //    lbtnInvoicing.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projecttracker_variations.aspx") == true)
        //{
        //    lbtnVariation.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectfinancial_customerpo.aspx") == true)
        //{
        //    lbtnPO.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectfinancial_internalpo.aspx") == true)
        //{
        //    lbtnPO.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectlabourtracker.aspx") == true)
        //{
        //    lbtnLaborTracker.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectmaterialtracker.aspx") == true)
        //{
        //    lbtnMaterialsTracker.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectmisctracker.aspx") == true)
        //{
        //    lbtnMiscTracker.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectexpensetracker.aspx") == true)
        //{
        //    lbtnExpenseTracker.BackColor = System.Drawing.Color.White;
        //}
            
        //else if ((Request.Url.ToString().ToLower()).Contains("projectpmhours.aspx") == true)
        //{
        //    lbtnPMHours.BackColor = System.Drawing.Color.White;
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("projectstaffhours.aspx") == true)
        //{
        //    lbtnStaffHours.BackColor = System.Drawing.Color.White;
        //}
        //else
        //{
        //    lbtnPO.BackColor = System.Drawing.Color.White;
        //}
    }
}
