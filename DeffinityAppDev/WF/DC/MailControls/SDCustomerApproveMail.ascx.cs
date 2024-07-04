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
using DC.BLL;
using DC.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class MailControls_SDCustomerApproveMail : System.Web.DynamicData.FieldTemplateUserControl {

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

    public void BindControls(string reciverName,string description,string details,string poNumber)
    {
        if (Type == "FLS")
        {
            lblTypeOfRequest.InnerText = "FLS Request";
            lblReciver.InnerText = "ALL";
            lblIncidentID.InnerText = string.Format("{0}", sessionKeys.IncidentID);
            FLSDetail flsDetail = FLSDetailsBAL.SelectbyId(sessionKeys.IncidentID);
            lblpono.InnerText = flsDetail.POnumber;
        }
        else
        {
            lblTypeOfRequest.InnerText = "Service Request";
            lblReciver.InnerText = reciverName;
            lbldiscription.InnerText = description;
            lblIncidentID.InnerText = string.Format("SR:{0}", sessionKeys.IncidentID);
            lblpono.InnerText = poNumber;
            lbldetails.InnerText = details;
        }
        lblrequster.InnerText = sessionKeys.UName;

        Gridview_services.DataBind();
        //Bind prices
        string EmailID = string.Empty;
        try
        {
           // EmailID = string.Empty;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "IncidentServicePriceByRequester", new SqlParameter("@IncidentID", sessionKeys.IncidentID), new SqlParameter("@Type", hfType.Value));
            while (dr.Read())
            {
                lblReciver.InnerText = dr["RequesterName"].ToString();
                //lblRevisedPric.InnerText = string.Format("{0:F2}", dr["RevicedPrice"]);
                //lblNotes.InnerText = dr["Notes"].ToString();
                lblTotalValue.InnerText = string.Format("{0:F2}", dr["OriginalPrice"]);
                lblDiscount.InnerText = dr["DiscountPercent"].ToString() + "%";
                lblRevisedPrice.InnerHtml = string.Format("{0:F2}", dr["RevicedPrice"]);
                lblNotes1.InnerText = dr["Notes"].ToString();
                //EmailID = dr["RequesterEmail"].ToString();

            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        string weburl = Deffinity.systemdefaults.GetWebUrl();
        string websitename = Deffinity.systemdefaults.GetWebSiteName();
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        //linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = websitename;
        linkWebsiteFooter.NavigateUrl = weburl;
        imgLogo.ImageUrl = weburl + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = weburl + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

    }

}
