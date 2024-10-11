using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class sidemenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DisplayandHideSerives();
            if (sessionKeys.SID == 3)
            {
                
            }

            if (sessionKeys.SID == 4)
            {

                link_settings.Visible = false;
                link_members.Visible = false;
               
                link_expenses.Visible = false;
                link_timesheets.Visible = false;//
                link_pagebuilder.Visible = false;
                a_tithing.HRef = "~/App/Donations.aspx";
                a_tithing.Title = "Tithing";
            }

            if(sessionKeys.IsService)
            {
                a_tithing.HRef = "~/App/Donations.aspx";
                a_tithing.Title = "Tithing";
            }
        }
  /*      private void DisplayandHideSerives()
        {
            using (var context = new PortfolioDataContext())
            {
                // Check if sessionKeys.PortfolioID is not null before querying the database
                if (sessionKeys.PortfolioID != null)
                {
                    var portolfiosettings = context.PortfolioActiveProducts.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);

                    // Check if portolfiosettings is not null before accessing its properties
                    if (portolfiosettings != null)
                    {
                        // Handle Project Management visibility
                        if (portolfiosettings.IsProjectManagement ?? false)
                        {
                            project.Text = @"
                        <div data-kt-menu-trigger='click' data-kt-menu-placement='right-start' data-kt-menu-flip='bottom' visible='false' class='menu-item py-2' id='link_eventmanagement' runat='server'>
                            <span class='menu-link' title='' data-bs-toggle='tooltip' data-bs-trigger='hover' data-bs-dismiss='click' data-bs-placement='right' data-bs-original-title='<%:sessionKeys.JobsDisplayName %>'>
                                <span class='menu-icon'>
                                    <i class='fas fa-project-diagram fs-2'></i>
                                </span>
                                <span class='menu-title'><%:sessionKeys.JobsDisplayName %></span>
                                <span class='menu-arrow'></span>
                            </span>
                            <div class='menu-sub menu-sub-accordion menu-active-bg'>
                                <div data-kt-menu-trigger='click' class='menu-item menu-accordion'>
                                    <div class='menu-item'>
                                        <a class='menu-link' href='<%:ResolveClientUrl(""~/WF/DC/Joblist.aspx"")%>'>
                                            <span class='menu-bullet'>
                                                <span class='bullet bullet-dot'></span>
                                            </span>
                                            <span class='menu-title'>View Current Projects</span>
                                        </a>
                                    </div>
                                    <div class='menu-item'>
                                        <a class='menu-link' href='<%:ResolveClientUrl(""~/WF/DC/FLSForm.aspx"")%>'>
                                            <span class='menu-bullet'>
                                                <span class='bullet bullet-dot'></span>
                                            </span>
                                            <span class='menu-title'>Add <%:sessionKeys.JobDisplayName %></span>
                                        </a>
                                    </div>
                                    <div class='menu-item' style='display: none; visibility: hidden;'>
                                        <a class='menu-link' href='<%:ResolveClientUrl(""~/WF/DC/FRPApprovals.aspx"")%>'>
                                            <span class='menu-bullet'>
                                                <span class='bullet bullet-dot'></span>
                                            </span>
                                            <span class='menu-title'>Invoice Journal</span>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>";
                        }

                        // Handle Peer-to-Peer Fundraising visibility
                        if (portolfiosettings.IsPeerToPeerFundraising ?? false)
                        {
                            p2p.Text = @"
                        <div class='menu-item py-2' id='pnlFundCamp' runat='server'>
                            <a href='<%:ResolveClientUrl(""~/App/FundraiserListView.aspx?type=camp"")%>' class='menu-link' title='Landing Page' data-bs-toggle='tooltip' data-bs-trigger='hover' data-bs-dismiss='click' data-bs-placement='right'>
                                <span class='menu-icon'>
                                    <i class='fas fa-hands fs-2'></i>
                                </span>
                                <span class='menu-title'>P2P Participation</span>
                            </a>
                        </div>";
                        }
                    }
                    else
                    {
                        // Handle case where portfolio settings are not found (optional)
                        // You can display a default message or log this for debugging
                    }
                }
                else
                {
                    // Handle case where PortfolioID is null (optional)
                    // You can display an error message or log this for debugging
                }
            }
        }

   */ }
}