using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class BlogDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["blogref"] != null)
                {
                    var pEntity = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().Where(o => o.BlogRef == Request.QueryString["blogref"].ToString()).FirstOrDefault();

                    if (pEntity != null)
                    {
                        lblContent.Text = Server.HtmlDecode(pEntity.BlogContent);
                        link_nav.Text = pEntity.Button1Title;
                        link_nav.NavigateUrl = pEntity.Button1Link;
                        lit_title.Text = pEntity.BlogTitle;
                        img.Src = GetBlogImageUrl(pEntity.BlogRef.ToString() );
                        Page.Title = pEntity.BlogTitle;
                    }

                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetBlogImageUrl(string bref)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers/") + "Blog_org_" + bref.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                //if (isOriginal)
                //    img = string.Format("~/WF/UploadData/Tithing/Tithing_{0}.png", contactsId.ToString());
                //else
                img = string.Format("../../WF/UploadData/Customers/Blog_org_{0}.png", bref.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "../../WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
    }
}