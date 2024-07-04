using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class BlogPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["BlogRef"] != null)
                {
                    var bEntity = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().Where(o => o.BlogRef == Request.QueryString["BlogRef"].ToString()).FirstOrDefault();
                    if (bEntity != null)
                    {
                        lblName.Text =  bEntity.BlogTitle;
                       
                        lblContent.Text = Server.HtmlDecode(bEntity.BlogContent);
                    }
                }
              
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}