using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
//using PrjTaskPlanner.DAL;

public partial class controls_ResourceSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMessage.Visible = false;
            fillResource();
            ddlProjectBind();
        }

    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            fillResource();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void fillResource()
    {
        try
        {

            using (projectTaskDataContext Resource = new projectTaskDataContext())
            {
                //grdResources.DataSource = Resource.ResourceSearch(txtCriteria.Text.ToString(), ddlLevel.SelectedItem.Text.ToString());
                grdResources.DataSource = Resource.ResourceSearch(txtCriteria.Text.ToString(), string.Empty);
                grdResources.DataBind();
                if (grdResources.Rows.Count > 1)
                {
                    pnlProject.Visible = true;
                    ddlProjectBind();
                    ddlTaskBind();
                }
                else
                {
                    pnlProject.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdResources_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdResources.PageIndex = e.NewPageIndex;
        fillResource();
        
    }

    private void ddlProjectBind()
    {
        try
        {
            using (projectTaskDataContext PrjTaskPlnr = new projectTaskDataContext())
            {
                int projectRef = int.Parse(Page.Request.QueryString["project"]);
                var data = (from p in PrjTaskPlnr.ProjectDetails
                            orderby p.ProjectReference
                            where p.ProjectStatusID == 2
                            select new
                            {
                                p.ProjectReferenceWithPrefix,
                                p.ProjectReference

                            });
                ddlProject.DataSource = data;// OrderBy(o => o.ItemDescription);
                ddlProject.DataValueField = "ProjectReference";
                ddlProject.DataTextField = "ProjectReferenceWithPrefix";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("Please select...", "0"));
                if (QueryStringValues.Project > 0)
                {
                    ddlProject.SelectedIndex = ddlProject.Items.IndexOf(ddlProject.Items.FindByValue(QueryStringValues.Project.ToString()));
                    //ddlProject.SelectedValue = QueryStringValues.Project.ToString();
                    ddlProject.Enabled = false;
                }
                else
                    ddlProject.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlTaskBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ddlTaskBind()
    {
        try
        {
            using (projectTaskDataContext PrjTaskPlnr = new projectTaskDataContext())
            {
                int projectitem = Convert.ToInt32(ddlProject.SelectedItem.Value.ToString());
                //int projectRef = int.Parse(Page.Request.QueryString["project"]);
                var data = (from p in PrjTaskPlnr.ProjectTaskItems
                            orderby p.ItemDescription
                            where p.ProjectReference == projectitem
                            select new
                            {
                                p.ItemDescription,
                                p.ID
                            });
                ddlTask.DataSource = data;//.OrderBy(o => o.ItemDescription);
                ddlTask.DataValueField = "ID";
                ddlTask.DataTextField = "ItemDescription";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {

            if (int.Parse(ddlProject.SelectedItem.Value.ToString()) != 0)
            {
                if (int.Parse(ddlTask.SelectedItem.Value.ToString()) != 0)
                {

                    foreach (GridViewRow row in grdResources.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkRsrce");
                        if (chk.Checked)
                        {
                            Label name = (Label)row.FindControl("lblRsrceName");
                            string itemDesc = ddlTask.SelectedItem.Text.ToString();
                            Label CID = (Label)row.FindControl("lblID");
                            int ContactorID = int.Parse(CID.Text.ToString());
                            projectTaskDataContext PrjTaskPlnr = new projectTaskDataContext();
                            PrjTaskPlnr.ProjectItems_Insert(int.Parse(ddlTask.SelectedValue), ContactorID, int.Parse(ddlProject.SelectedValue));
                            lblMessage.Visible = true;
                            lblMessage.Text = "Successfully Inserted";
                        }
                    }
                }
                else
                {
                    lblProjResErr.Visible = true;
                    lblProjResErr.Text = "Please select Project Task item";
                }
            }
            else
            {
                lblProjResErr.Visible = true;
                lblProjResErr.Text = "Please slect Project item";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //lnkView_Click
   protected void lnkView_Click(object sender, EventArgs e)
    {

        try
        {
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   protected void grdResources_RowCommand(object sender, GridViewCommandEventArgs e)
   {
       try
       {
           if (e.CommandName == "View")
           {
               int ID =Convert.ToInt32 (e.CommandArgument);
               Response.Redirect("projectresourceview.aspx?uid=" + ID);
           }
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

   protected string RetDate(string dval)
   {
       string retval = string.Empty;
       if (dval.Contains("01/01/1900"))
           retval = "Available";
       else
           retval = Convert.ToDateTime(dval).ToShortDateString();//string.Format("{0:d}", Convert.ToDateTime(dval).ToShortDateString());
       return retval;
   }
}
