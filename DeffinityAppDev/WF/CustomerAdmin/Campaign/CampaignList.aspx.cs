using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(sessionKeys.Message))
                {
                   DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message,"Ok");
                    sessionKeys.Message = string.Empty;
                }

                BindGrid();

            }
        }

        private void BindGrid()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var tmp = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                //if (tmp.Count > 0)
                //{
                    GridList.DataSource = (from t in tmp
                                           orderby t.ID descending
                                           select new
                                           {
                                               t.ID,
                                               t.Tags,
                                               t.Subject,
                                               t.TemplateName,
                                               Date = (t.ScheduledStartDate.HasValue ? string.Format("{0:dd/MM/yyyy}", t.ScheduledStartDate.Value) : string.Empty),

                                           }).ToList();
                    GridList.DataBind();
               // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public string SetTagCss(string tags)
        {
            string retval = string.Empty;
            if (tags != null)
            {
                if (tags.Trim().Length > 0)
                {
                    retval = "<div class='bootstrap-tagsinput'>";
                    var s = string.Empty;
                    string[] str = tags.Split(',');
                    foreach (var s1 in str)
                    {
                        if (!string.IsNullOrEmpty(s1))
                            s = s + "<span class='tag label label-black'>" + s1 + "</span> ";
                    }
                    retval = retval + s + "</div>";
                }
            }
            return retval;
        }
        protected void GridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                    Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID=" + Convert.ToInt32(e.CommandArgument.ToString()), false);
                }
                else if (e.CommandName == "del")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                    var tmp = cRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (tmp != null)
                    {
                        cRep.Delete(tmp);
                        lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        
                        BindGrid();
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}

