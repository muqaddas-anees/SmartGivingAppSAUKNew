using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class EventDdlCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();
                if (sessionKeys.PortfolioID == 0)
                {
                    mdlPopup.Show();
                }
                else
                { mdlPopup.Hide(); }
            }
        }

        private void BindData()
        {
            //dbind.DdlBindSelect(ddlPortfolio, "SELECT ID,PortFolio FROM ProjectPortfolio where visible=1 order by PortFolio", "ID", "PortFolio",false,true,false);
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> acRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            var aList = acRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).OrderByDescending(o=>o.ID).ToList();
            ddlPortfolio.DataSource = aList;
            ddlPortfolio.DataTextField = "Title";
            ddlPortfolio.DataValueField = "unid";
            ddlPortfolio.DataBind();
            if (ddlPortfolio.Items.Count > 0)
            {
                if (QueryStringValues.UNID.Length >0)
                {
                    ddlPortfolio.SelectedValue = QueryStringValues.UNID.ToString();

                    lblEventname.Text = ddlPortfolio.SelectedItem.Text;
                }
                else
                {
                    var a = aList.FirstOrDefault();
                    ddlPortfolio.SelectedValue = a.unid;
                    lblEventname.Text = ddlPortfolio.SelectedItem.Text;
                    Response.Redirect("~/App/Events/ViewAttendees.aspx?unid=" + ddlPortfolio.SelectedValue, false);
                }
              
            }
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
              
                mdlPopup.Hide();
                Response.Redirect("/App/Events/ViewAttendees.aspx?unid="+ ddlPortfolio.SelectedValue, false);
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        }
    }

}