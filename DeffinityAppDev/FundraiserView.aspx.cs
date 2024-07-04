using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class FundriserView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {



                    // tList = tList.Where(o => o.FundriserUNID.ToLower().Trim() == txtEmailAddress.Text.Trim().ToLower()).ToList();

                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var tEntity = pRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                    if(tEntity != null)
                    {
                        sessionKeys.PortfolioID = tEntity.OrganizationID??0;
                        htitle.Value = tEntity.Title;
                        hurl.Value = Deffinity.systemdefaults.GetWebUrl() + "/Fundraiser/" + tEntity.QRcode;
                        // himage.Value = Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Tithing/Fund_" + tEntity.unid + "_top.png";
                        himage.Value = Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + tEntity.ID + "&s=" + ImageManager.file_section_fundriser_top;
                        // "~/ImageHandler.ashx?id=" + eventEntity.ID + "&s=" + ImageManager.file_section_fundriser_top;
                        hdescription.Value = tEntity.SocialDescription;
                        hkey.Value = tEntity.SocialKeywords;
                    }



                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string sitetitle()
        {
            return htitle.Value;
        }
        protected string siteurl()
        {
            return hurl.Value;
        }
        protected string siteimage()
        {
            return himage.Value;
        }

        protected string sitedesciption()
        {
            return hdescription.Value;
        }
        protected string sitekey()
        {
            return hkey.Value;
        }
    }
}