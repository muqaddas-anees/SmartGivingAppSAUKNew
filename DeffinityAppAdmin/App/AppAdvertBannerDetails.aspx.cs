using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class AppAdvertBannerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindReligion();
                    BindDenomination(0);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindReligion()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

                ddlReligion.DataSource = rlist;
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataValueField = "ID";
                ddlReligion.DataBind();

                ddlReligion.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int religionID)
        {
            try
            {
                if (religionID > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    ddlDenimination.DataSource = rlist;
                    ddlDenimination.DataTextField = "Name";
                    ddlDenimination.DataValueField = "ID";
                    ddlDenimination.DataBind();
                }

                ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}