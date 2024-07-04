using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_OvertimeCutOffReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblError.Visible = false;
                BindChkBoxType();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    }
    private void BindChkBoxType()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "TimeSheet_ListOfEntryTypes").Tables[0];
        lstEntryType.DataSource = dt;
        lstEntryType.DataValueField = "ID";
        lstEntryType.DataTextField = "EntryType";
        lstEntryType.DataBind();

        if (lstEntryType.Items.Count > 0)
        {
            foreach (ListItem i in lstEntryType.Items)
            {
                i.Selected = true;
            }
        }
    }
    protected void btn_Submitt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string val = EntryType();
           if (val != "")
           {
               lblError.Visible = false;
               TimesheetSummary.Attributes.Add("src", string.Format("OvertimeCutOffReportfrm.aspx?etype={0}&sdate={1}&edate={2}&type=pdf&time={3}", val,
                Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
                , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), CutOffTime()));
           }
           else
           {
               lblError.Visible = true;
               lblError.Text = "Please select entry type";
           }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
             string val = EntryType();
             if (val != "")
             {
                 lblError.Visible = false;
                 TimesheetSummary.Attributes.Add("src", string.Format("OvertimeCutOffReportfrm.aspx?etype={0}&sdate={1}&edate={2}&type=xsl1&time={3}", val,
                   Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
                   , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), CutOffTime()));
             }
             else
             {
                 lblError.Visible = true;
                 lblError.Text = "Please select entry type";
             }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private  string EntryType()
    {
        string EntryTypeID = "";
        try
        {


            foreach (ListItem i in lstEntryType.Items)
            {
                if (i.Selected)
                {
                    EntryTypeID += i.Value + ",";

                }

            }
            // Course.Classification_InsertDelete(courseID, classificationID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return EntryTypeID;
    }
    protected string CutOffTime()
    {
        string val = txtCutOff.Text;
        char[] comm = { ':' };
        string[] getva = val.Split(comm);

        string newval = "";

        newval = getva[0] + "." + getva[1];
        return newval;
    }
}
