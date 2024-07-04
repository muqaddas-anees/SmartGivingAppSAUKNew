using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class ResourceNewChitChat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Resources - Chit Chat";
        if (!IsPostBack)
        {
            BindCustomer();
            if (sessionKeys.PortfolioID > 0)
            {
                ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            if (int.Parse(ddlCustomer.SelectedValue) > 0)
            {
                sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedValue);
                sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
            }
           // ChitChatCtrlResource1.ChitchatBind();
        }
    }

    public void BindCustomer()
    {
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
           var customerList = (from p in pdt.ProjectDetails
                                join q in pdt.ProjectTaskItems on p.ProjectReference equals q.ProjectReference
                                join r in pdt.ProjectItems on q.ID equals r.ItemReference
                                where r.ContractorID == sessionKeys.UID 
                                select new {ID = p.Portfolio,Name= p.PortfolioName }).Distinct().OrderBy(p=>p.Name).ToList();

            ddlCustomer.DataSource = customerList;
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0,new ListItem("Please select...", "0"));
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedValue);
            sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
       
       // ChitChatCtrlResource1.ChitchatBind();
    }
}