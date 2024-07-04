using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class OrgHomeNewV2 : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //int[] sids = { 4, 9 };
            //if (sids.Contains(sessionKeys.SID))
            //{
            //    this.Page.MasterPageFile = "~/DeffinityResourceTab.master";

            //}
            try
            {
                if (Request.QueryString["orgguid"] != null)
                {

                    try
                    {
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(Request.QueryString["orgguid"].ToString());
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgguid"].ToString()).FirstOrDefault();
                    if (orgEntity != null)
                    {
                        //if orgid is empty update the orgname

                        sessionKeys.PortfolioID = orgEntity.ID;
                        sessionKeys.PortfolioName = orgEntity.PortFolio;
                        // lblOrgnizationname.Text = orgEntity.PortFolio;
                        //if (System.IO.File.Exists(Server.MapPath("~/WF/UploadData/OrgTemplate/org_new_" + sessionKeys.PortfolioID + ".html")))
                        //    hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_new_{0}.html?id=" + DateTime.Now.Millisecond.ToString(), sessionKeys.PortfolioID);
                        //else
                        //    hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_new_{0}.html", 0);

                        //Encoding.UTF8.GetString(record.Data.ToArray());
                        hpath.Value = "FileDataHandler.ashx?id=" + sessionKeys.PortfolioID +"&s="+ImageManager.file_section_landing+"&t=1";
                        //Emailer em = new Emailer();
                        //var eContent = em.ReadFile(string.Format("~/WF/UploadData/OrgTemplate/org_new_{0}.html", sessionKeys.PortfolioID)));
                        //lit_html.Text = eContent.Trim();

                        //this.Title = orgEntity.PortFolio;
                        // Page.Header.Title = orgEntity.PortFolio;
                        lblTitle.Text = orgEntity.PortFolio;

                        hurl.Value = Deffinity.systemdefaults.GetWebUrl() + "/web/" + orgEntity.OrgUniqID;
                        // himage.Value = Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_portfolio;
                        himage.Value = Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_landing_top;
                        hdescription.Value = orgEntity.Description;
                        hkey.Value = orgEntity.StrategicFit;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (Request.QueryString["orgguid"] != null)
                    //{
                    //    //update the org name
                    //    try
                    //    {
                    //        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(Request.QueryString["orgguid"].ToString());
                    //    }
                    //    catch(Exception ex)
                    //    {
                    //        LogExceptions.WriteExceptionLog(ex);
                    //    }
                    //    var orgEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == Request.QueryString["orgguid"].ToString()).FirstOrDefault();
                    //    if (orgEntity != null)
                    //    {
                    //        //if orgid is empty update the orgname

                    //        sessionKeys.PortfolioID = orgEntity.ID;
                    //        sessionKeys.PortfolioName = orgEntity.PortFolio;
                    //       // lblOrgnizationname.Text = orgEntity.PortFolio;
                    //       if(System.IO.File.Exists(Server.MapPath( "~/WF/UploadData/OrgTemplate/org_new_"+ sessionKeys.PortfolioID + ".html")))
                    //        hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_new_{0}.html?id=" + DateTime.Now.Millisecond.ToString(), sessionKeys.PortfolioID);
                    //       else
                    //            hpath.Value = string.Format("../../WF/UploadData/OrgTemplate/org_new_{0}.html", 0);
                    //        //Emailer em = new Emailer();
                    //        //var eContent = em.ReadFile(string.Format("~/WF/UploadData/OrgTemplate/org_new_{0}.html", sessionKeys.PortfolioID)));
                    //        //lit_html.Text = eContent.Trim();

                    //        //this.Title = orgEntity.PortFolio;
                    //        // Page.Header.Title = orgEntity.PortFolio;
                    //        lblTitle.Text = orgEntity.PortFolio;
                    //    }
                    //    //imglogo.Src = Deffinity.PortfolioManager.Portfilio.setLogo();
                    //}

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }

                    else if (sessionKeys.ErrorMessage.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string ReplaceTithing(string rdata)
        {
            //var str = "<img src=\"snippets/img/tithing.jpg\" width=\"\" height=\"\" style=\"display: inline-block;\">";
            var str = "&lt;imgsrc=&quot;snippets/img/faitheducation.jpg&quot;width=&quot;&quot;height=&quot;&quot;style=&quot;display:inline-block;&quot;class=&quot;&quot;&gt;";

            var retval = rdata.Replace(str, "[tithing]");

            return retval;
        }
        protected string sitetitle()
        {
            return lblTitle.Text;
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