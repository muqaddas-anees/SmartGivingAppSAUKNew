using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_RFIVendorMainTabNew : System.Web.UI.UserControl
{
    DisBindings getdata = new DisBindings();
    protected string[] Purl = { "RFIVendorOverview.aspx", "RFIVendorSites.aspx", "RFIVendorContacts.aspx", "RFIVendorCertifications.aspx", 
                                  "RFIVendorAttributes.aspx", "RFIVendorServiceCatalog.aspx", "MyTasks.aspx", "RFIMain.aspx", 
                                  "RFIVendorBBBEEPointsJournal.aspx", "RFIVendorsummary.aspx", "CustomerContracts.aspx?Vendor=2&", 
                                  "RFIVendorPerformance.aspx","VendorCustomEmail.aspx"};
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (sessionKeys.SID == 8)
        //{
        //    liMyTender.Visible = true;
        //    liTenderHome.Visible = false;
        //    liVendorPerformance.Visible = false;
        //}
        //else
        //{
        //    liMyTender.Visible = false;
        //    liTenderHome.Visible = true;
        //    liVendorPerformance.Visible = true;
        //}
        //if ((Request.Url.ToString().ToLower()).Contains(Purl[9].ToLower()) == false)
        //{
        //    liMyTender.Attributes.Remove("class");
        //}
        //if ((Request.Url.ToString().ToLower()).Contains(Purl[11].ToLower()) == false)
        //{
        //    liVendorPerformance.Attributes.Remove("class");
        //}

    }
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        string stemp = "";
        if (i <= Purl.Length)
        {
            stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current1";

            }     
        }
        return rtValue;

    }



    protected string getUrl(int i)
    {
        //if (i == 9)
        //{
        //    if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
        //    {
        //        liMyTender.Attributes.Add("class", "current1");
        //        //liMyTender.Attributes.Remove("class");
        //    }
        //}
        string url = "#";
        if (i <= Purl.Length)
        {
            if (Request.QueryString["VendorID"] != null)
            {
                if (i == 7)
                {
                    if (sessionKeys.SID == 8)
                    {
                        url = "#";
                    }
                    else
                        url = Purl[i];
                }
                //else if (i == 8)
                //{
                //    if (sessionKeys.SID == 8)
                //    {
                //        url = "#";
                //    }
                //    else
                //        url = Purl[i] + "?VendorID=" + Request.QueryString["VendorID"].ToString();
                //}
                else 
                {
                    if (i == 10)
                        url = Purl[i] + "VendorID=" + Request.QueryString["VendorID"].ToString();
                    else
                       url = Purl[i] + "?VendorID=" + Request.QueryString["VendorID"].ToString();
                }
               
            }
            else if (sessionKeys.SID == 8)
            {
                int VendorID = Convert.ToInt32(getdata.exeScalar("select VendorID from RFI_Vendor where ContractorID=" + Convert.ToInt32(sessionKeys.UID.ToString()), false));
                if (i == 10)
                    url = Purl[i] + "VendorID=" + VendorID.ToString();
                else
                    url = Purl[i] + "?VendorID=" + VendorID.ToString();
            }
            else
            {
                if (i == 7)
                {
                    url = Purl[i];
                }
            }
            i++;
        }

        return url;
    }
}
