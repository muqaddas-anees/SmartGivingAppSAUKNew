using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class BlogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindBloglist();
            }
        }

        private void BindBloglist()
        {
            try
            {
                var plist = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().ToList();

                if (plist != null)
                {
                    gridBlog.DataSource = plist.OrderByDescending(o => o.ID).ToList();
                    gridBlog.DataBind();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/Admin/BlogDetails.aspx", false);
        }
        
        protected void gridBlog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
              
                if (e.CommandName == "blogedit")
                {
                    //var id = Convert.ToInt32(e.CommandArgument.ToString());
                    Response.Redirect("~/WF/Admin/BlogDetails.aspx?BlogRef=" + e.CommandArgument.ToString(), false);
                }
                else if (e.CommandName == "preview")
                {
                    Response.Redirect("~/WF/Admin/BlogPreview.aspx?BlogRef="+ e.CommandArgument.ToString(), false);
                }
                else if (e.CommandName == "blogdelete")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());
                    PortfolioMgt.BAL.BlogBAL.BlogBAL_delete(id);
                    BindBloglist();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}