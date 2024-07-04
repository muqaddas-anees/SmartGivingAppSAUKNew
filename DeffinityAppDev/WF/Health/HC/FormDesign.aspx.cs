using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using DC.DAL;

public partial class HC_FormDesign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
           
           
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
            try
            {
            if (!IsPostBack)
            {
                ccdCompny.DataBind();
                ccdCompny.SelectedValue = sessionKeys.PortfolioID.ToString();
                hcustomerid.Value = sessionKeys.PortfolioID.ToString();
                BindTypeOfField();
                SetAssignedTypeOfRequest();
            }
            if (Request.QueryString["fid"] != null)
            {
                int Formid = int.Parse(Request.QueryString["fid"]);
                using (HealthCheckDataContext Dc = new HealthCheckDataContext())
                {
                    var p = (from a in Dc.HealthCheck_FormPanels
                             where a.FormID == Formid && a.PanelName != "Signature Panel" && a.PanelName != "Header"
                             orderby a.PnlPosition ascending
                             select new
                             {
                                 a.PanelID,
                                 a.PnlPosition,
                                 a.PanelName
                             }).ToList();
                    gridlist.DataSource = p;
                    gridlist.DataValueField = "PanelID";
                    gridlist.DataTextField = "PanelName";
                    gridlist.DataBind();
                }
            }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
    }
   
    private void SetAssignedTypeOfRequest()
    {
        try
        {
            using (DCDataContext DC = new DCDataContext())
            {
                int Formid = 0;
                if (Request.QueryString["fid"] != null)
                {
                     Formid = int.Parse(Request.QueryString["fid"]);
                }
                    var dEntity = DC.FLSForms.Where(o => o.FormID == Formid).FirstOrDefault();
                if (dEntity != null)
                {
                    ddlTypeofRequest.SelectedValue = (dEntity.AssignedTypeOfRequestID.HasValue ? dEntity.AssignedTypeOfRequestID.Value : 0).ToString();

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTypeOfField()
    {
        try
        {
            using (DCDataContext dc = new DCDataContext())
            {
                var flist = dc.TypeOfRequests.Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o => o.Name).ToList();
                ddlTypeofRequest.DataSource = flist;
                ddlTypeofRequest.DataTextField = "Name";
                ddlTypeofRequest.DataValueField = "ID";
                ddlTypeofRequest.DataBind();
                ddlTypeofRequest.Items.Insert(0, new ListItem("Please select...", "0"));
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateAssignedTypeOfRequest();
    }

    private void UpdateAssignedTypeOfRequest()
    {
        try
        {

            using (DCDataContext DC = new DCDataContext())
            {
                int Formid = 0;
                if (Request.QueryString["fid"] != null)
                {
                    Formid = int.Parse(Request.QueryString["fid"]);
                }

                var dEntity = DC.FLSForms.Where(o => o.FormID == Formid).FirstOrDefault();
                if (dEntity != null)
                {
                    dEntity.AssignedTypeOfRequestID = Convert.ToInt32(ddlTypeofRequest.SelectedValue);
                    DC.SubmitChanges();
                    lblMsg1.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
                else
                {
                    dEntity = new DC.Entity.FLSForm();
                    dEntity.FormID = Formid;
                    dEntity.FormName = txtFormName.Text;
                    dEntity.PortfolioID = sessionKeys.PortfolioID;
                    dEntity.LoggedBy = sessionKeys.UID;
                    dEntity.LoggedDate = DateTime.Now;
                    dEntity.AssignedTypeOfRequestID = Convert.ToInt32(ddlTypeofRequest.SelectedValue);
                    DC.FLSForms.InsertOnSubmit(dEntity);
                    DC.SubmitChanges();
                    lblMsg1.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["fid"] != null)
        Response.Redirect("~/WF/Health/HC/FormPreview.aspx?fid="+ Request.QueryString["fid"].ToString());
    }
    protected void BtnPosition_Click(object sender, EventArgs e)
    {
        Ajaxpopup.Show();
    }
}