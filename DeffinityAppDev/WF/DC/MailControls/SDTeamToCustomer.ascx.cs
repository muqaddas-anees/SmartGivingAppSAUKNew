using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using DC.BAL;
using DC.Entity;
using Deffinity.IncidentService_Price_Manager;
using DC.BLL;
using System.Linq;

public partial class MailControls_SDTeamToCustomer : System.Web.UI.UserControl
{
    public string Type
    {
        set;
        get;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.QueryString["callid"] != null)
            {
                hfType.Value = "FLS";
            }
            else
            {
                hfType.Value = "SD";
            }

        }
    }
    public string BindControls()
    {
        string weburl = Deffinity.systemdefaults.GetWebUrl();
        string websitename = Deffinity.systemdefaults.GetWebSiteName();
        var jEntity = FLSDetailsBAL.Jqgridlist(sessionKeys.IncidentID).FirstOrDefault();
        string ticketPrefix = string.Empty;
        if (hfType.Value == "FLS")
        {
            lblType.Text = "JOB Request";
            ticketPrefix = "";
            lblPrefix.Text = "Job reference";

            CallDetail cd = new CallDetail();
            cd = DC.BLL.CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
            string vc_code = FormsAuthentication.HashPasswordForStoringInConfigFile(sessionKeys.IncidentID.ToString() + cd.LoggedBy.Value.ToString(), "SHA1");
            linkPending.NavigateUrl = weburl + string.Format("/WF/Default.aspx?callid={0}&ref=mail&vc={1}", sessionKeys.IncidentID, vc_code);
        }
        else
        {
            lblType.Text = "Service Request";
            ticketPrefix = "SR:";
            lblPrefix.Text = "Service Desk";
            linkPending.NavigateUrl = weburl + string.Format("WF/DC/IncidentServiceEmail.aspx?incidentid={0}&statusid={1}&type={2}", sessionKeys.IncidentID, 1, hfType.Value);
        }
      
        Gridview_services.DataBind();
        //Bind prices
        string EmailID = string.Empty;
        try
        {
            EmailID = string.Empty;
            SqlDataReader dr = IncidentService_Price.Quotation_Price_Select(sessionKeys.IncidentID, hfType.Value,QueryStringValues.OPTION);
            //SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "IncidentServicePriceByRequester", new SqlParameter("@IncidentID", sessionKeys.IncidentID), new SqlParameter("@Type", hfType.Value));
            while (dr.Read())
            {
                lblReciver.InnerText = dr["RequesterName"].ToString();
                //lblRevisedPric.InnerText = string.Format("{0:F2}", dr["RevicedPrice"]);
                //lblNotes.InnerText = dr["Notes"].ToString();
                lblTotalValue.InnerText = string.Format("{0:F2}", dr["OriginalPrice"]) ;
                lblDiscount.InnerText = dr["DiscountPercent"].ToString() + "%";
                lblRevisedPrice.InnerHtml = string.Format("{0:F2}", dr["RevicedPrice"]);
                lblNotes1.InnerText = dr["Notes"].ToString();
                EmailID = dr["RequesterEmail"].ToString();

            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        

        lblInstnaceTitle.Text = Deffinity.systemdefaults.GetInstanceTitle();
        lblIncidentID.InnerText = ticketPrefix+ jEntity.CCID;
       
        linkPendingApproval.NavigateUrl = weburl + string.Format("/WF/Default.aspx");
        linkWebsiteFooter.Text = websitename;
        linkWebsiteFooter.NavigateUrl = weburl;
        imgLogo.ImageUrl = weburl + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = weburl + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

        return EmailID;

    }
    //private void GetPrices(out string EmailID)
    //{
    //    try
    //    {
    //        EmailID = string.Empty;
    //        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "IncidentServicePriceByRequester", new SqlParameter("@IncidentID", sessionKeys.IncidentID));
    //        while (dr.Read())
    //        {
    //            lblReciver.InnerText = dr["RequesterName"].ToString();
    //            lblRevisedPric.InnerText = dr["RevicedPrice"].ToString();
    //            lblNotes.InnerText = dr["Notes"].ToString();
    //            EmailID = dr["RequesterEmail"].ToString();
    //        }
    //        dr.Close();
    //        dr.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
}
