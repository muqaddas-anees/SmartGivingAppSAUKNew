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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
public partial class MailControls_ProjectInvoiceMail : System.Web.UI.UserControl
{
    RiseValuation RiseVal = new RiseValuation();
    protected void Page_Load(object sender, EventArgs e)
    {

   }
    public void setdata(int pref,int invoiceid)
    {
        try{
        GetGridData( pref,invoiceid);
        BindLables(pref,invoiceid);
            BindInvoiceDetail(pref,invoiceid);
            projectTaskDataContext projectsDB=new projectTaskDataContext();
            var projectsTitle = (from r in projectsDB.ProjectDetails
                                 where r.ProjectReference == pref
                                 select r).ToList().FirstOrDefault();

        //footer url
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
            linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
            linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        lblProjectReference.InnerText = sessionKeys.Prefix + pref.ToString();
            if(projectsTitle!=null)
            {
                lblTitle.InnerText = projectsTitle.ProjectTitle;
            }
        lblInvoice.InnerText = getInvoiceReference(invoiceid);
        //getInvoiceReference(int invoiceid)
         }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
    private void GetGridData(int pref,int invoiceid)
    {
        try{
        GridView1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DisplayInvoice", new SqlParameter("@ProjectReference", pref), new SqlParameter("@ProjectValuationID", invoiceid), new SqlParameter("@Counter", 1));
        GridView1.DataBind();
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
   
    private void BindLables(int pref,int invoiceid)
    {
        try
        {
            using (IDataReader dr = RiseVal.GetInvoiceTotal(pref, invoiceid))
            {
                while (dr.Read())
                {
                    lblRevisedProjectValue.Text = string.Format("{0:F2}", dr["RevisedValue"]);
                    lblTotalInvoice.Text = string.Format("{0:F2}", dr["TotalValue"]);
                    lblTotalPV.InnerText = string.Format("{0:F2}", dr["RevisedValue"]);
                    //lblOutstandingVal.Text = string.Format("{0:F2}", dr["OutstandingValue"]);
                }
                dr.Close();
            }
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindInvoiceDetail(int ProjectRef, int invoiceid)
    {
         try
        {
       
        projectTaskDataContext invoice = new projectTaskDataContext();
        var invoiceJouranl = (from r in invoice.ProjectValuations
                              // join s in list on r.InvoiceStatus equals int.Parse(s.Value)
                              join p in invoice.ProjectDetails on r.ProjectReference equals p.ProjectReference
                              where r.ProjectReference == ProjectRef && r.ValuationID == invoiceid
                              select new
                              {
                                  r.InvoiceReference,
                                  r.DateRaised,
                                  r.Value,
                                  r.VATPercentage,
                                  VatTotal = ((r.VATPercentage * r.Value) / 100) ,
                                  SubTotal = ((r.VATPercentage * r.Value) / 100) + r.Value,
                                  p.ProjectReferenceWithPrefix,
                                  p.ProjectTitle,
                                  r.ValuationID,
                                  expectedate = r.DateRaised.Value.AddDays(30),
                                  r.Notes,
                                  r.ProjectReference,
                                  r.InvoiceStatus
                              }).ToList().FirstOrDefault();
        if (invoiceJouranl != null)
        {
            lblVat.Text = string.Format("{0:f2}", invoiceJouranl.VATPercentage)+"%"  ;
            lblVatcal.Text = string.Format("{0:f2}", invoiceJouranl.VatTotal);
            lblInvoiceTotal.Text = string.Format("{0:f2}", invoiceJouranl.SubTotal);

        }
        }
         catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
    private string getInvoiceReference(int invoiceid)
    {
        string invoiceref = string.Empty;
        try
        {
            invoiceref = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select InvoiceReference from ProjectValuations where ValuationID= @ValuationID", new SqlParameter("@ValuationID", invoiceid)).ToString();
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

        return invoiceref;
    }
    
}
