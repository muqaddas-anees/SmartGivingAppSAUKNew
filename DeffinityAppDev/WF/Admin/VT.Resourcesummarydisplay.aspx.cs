using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Resourcesummarydisplay : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int Resourceid = 0;
        string cdate = string.Empty;
        if (QueryStringValues.ContractorID > 0)
            Resourceid = QueryStringValues.ContractorID;

        if (Request.QueryString["date"] != null)
        {
            cdate = Request.QueryString["date"].ToString();
        }
        SummaryBinding(Resourceid,cdate);
    }
    private void SummaryBinding(int Resourceid,string cdate)
    {
        try
        {

            DataTable Dt_allowance;
            if(!string.IsNullOrEmpty(cdate))
                Dt_allowance = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_UserDate", new SqlParameter("@ResourceID", Resourceid), new SqlParameter("@cdate", Convert.ToDateTime(cdate))).Tables[0];
            else 
                Dt_allowance = VT.DAL.LeaveRequestHelper.DisplayResourceSummary(Resourceid);
            dlist_summary.DataSource = Dt_allowance;
            dlist_summary.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
}
