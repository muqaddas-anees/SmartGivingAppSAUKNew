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
using Incidents.Entity;
using Incidents.DAL;
using Incidents.StateManager;
using Deffinity.IncidentService_Price_Manager;
using DC.DAL;
using DC.Entity;
using DC.BLL;

 
public partial class MailControls_SDTeamToCustomer_Complete : System.Web.UI.UserControl
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
            hfType.Value = Type.ToString();
        }
    }
    public string BindControls(string ReqName,string ReqEmail)
    {
        string ticketPrefix = string.Empty;
        if (hfType.Value == "FLS")
        {
            lblbType.Text = "FLS Request";
            ticketPrefix = "";
        }
        else
        {
            lblbType.Text = "Service Request";
            ticketPrefix = "SR:";
        }
      
        Gridview_services.DataBind();
        //Bind prices
        string EmailID = string.Empty;
        try
        {
           
            EmailID = string.Empty;
            SqlDataReader dr = IncidentService_Price.IncidentService_Price_Select(sessionKeys.IncidentID, hfType.Value,QueryStringValues.IVREF);
            while (dr.Read())
            {
              
                lblRevisedPric.InnerText = string.Format("{0:#.00}",dr["RevicedPrice"]);
               
            }
            dr.Close();
            dr.Dispose();

            //get the raised date
            if (hfType.Value == "FLS")
            {
                CallDetail cd = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                lblOrderLoggedDate.InnerText = cd.LoggedDate.ToString().Remove(10);
            }
            else
            {
                Incident ins = IncidentHelper.SelectById(sessionKeys.IncidentID);
                lblOrderLoggedDate.InnerText = ins.DateLogged.ToShortDateString();
            }
           
     
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        lblReciver.InnerText = ReqName;
        EmailID = ReqEmail;
        lblIncidentID.InnerText = ticketPrefix + sessionKeys.IncidentID.ToString();
        //linkPending.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + string.Format("/IncidentServiceEmail.aspx?incidentid={0}&statusid={1}", sessionKeys.IncidentID, 1);
        //linkPendingApproval.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + string.Format("/Default.aspx");
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

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
