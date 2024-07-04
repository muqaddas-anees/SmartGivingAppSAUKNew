using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Deffinity.ProgrammeManagers;
using Deffinity.ProgrammeEntitys;

public partial class PortfolioAssessment : System.Web.UI.Page
{
    Admin ad = new Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ProgrammeAssessmentCL Assessment = new ProgrammeAssessmentCL();
            Assessment.Benefits = txtBenefits.Text.Trim();
            Assessment.EmergentOpportunities = txtOpportunities.Text.Trim();
            Assessment.PaceOfProgress = txtPaceOfProgress.Text.Trim();
            Assessment.ProgressToDate = txtProgress.Text.Trim();
            Assessment.ProgrammeID = 0;
            Assessment.PortfolioID = sessionKeys.PortfolioID;
            if (!string.IsNullOrEmpty(id.Value))
            {
                Assessment.ID = Convert.ToInt32(id.Value);
                Admin.InsertUpdateProgrammeAssessment(false, Assessment);
            }
            else
            {
                Admin.InsertUpdateProgrammeAssessment(true, Assessment);
            }


            ClearFields();
            GridView1.DataBind();
            //clear hideen field
            id.Value = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }
    private void ClearFields()
    {
        txtBenefits.Text = "";
        txtOpportunities.Text = "";
        txtPaceOfProgress.Text = "";
        txtProgress.Text = "";
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int i = e.NewSelectedIndex;
        HiddenField HID = (HiddenField)GridView1.Rows[i].FindControl("HID");
        getAssement(ad.SelectProgrammeAssessment(Convert.ToInt32(HID.Value)));
    }
    private void getAssement(ProgrammeAssessmentCL assessment)
    {
        txtBenefits.Text = assessment.Benefits;
        txtOpportunities.Text = assessment.EmergentOpportunities;
        txtPaceOfProgress.Text = assessment.PaceOfProgress;
        txtProgress.Text = assessment.ProgressToDate;
        id.Value = assessment.ID.ToString();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
           
            
        }
    }
}
