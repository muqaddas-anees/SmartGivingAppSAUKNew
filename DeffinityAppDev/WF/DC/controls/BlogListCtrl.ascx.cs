using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class BlogListCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    IPortfolioRepository<PortfolioMgt.Entity.tblBlog> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblBlog>();
                    var tithingDetail = pRep.GetAll().FirstOrDefault();

                    var tithinglist = pRep.GetAll().ToList();

                    if (tithinglist != null)
                    {
                        var vValues = tithinglist.OrderByDescending(o => o.ID).ToList();

                        var retval =( from r in vValues
                                     select new
                                     {
                                         ID= r.ID,
                                         BlogContent =  r.BlogContent.Length >300? r.BlogContent.Substring(0,299): r.BlogContent,
                                         BlogRef= r.BlogRef,
                                         BlogTitle =r.BlogTitle,
                                         Button1Link = r.Button1Link,
                                         Button1Title = r.Button1Title,
                                         Button2Link = r.Button2Link,
                                         Button2Title = r.Button2Title
                                     }
                                     ).ToList();
                        ListFaithGiving.DataSource = vValues;
                        ListFaithGiving.DataBind();
                        //string curr = tithingDetail.Currency;
                    }
                }

                //if (sessionKeys.Message.Length > 0)
                //{
                //    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                //    sessionKeys.Message = string.Empty;
                //}

                // }
            }
            catch (Exception ex)
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


        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                    string v = e.CommandArgument.ToString();

                    Response.Redirect("~/App/AddFaithGiving.aspx?eid=" + v, false);
                }
                else
                {
                    string v = e.CommandArgument.ToString();

                    Response.Redirect("~/WF/BlogDisplay.aspx?blogref=" + v, false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      

        public bool showButton(object val)
        {
            string valstr = "";
            if(val != null)
            {
                valstr = val.ToString().Trim();
            }

            if (valstr.Length > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}