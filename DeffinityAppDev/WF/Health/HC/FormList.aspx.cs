using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.BAL;
using HealthCheckMgt.Entity;


public partial class HC_FormList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           // Bind_CustomerDropdown();
            BindGrid();
          //  BindListView();
        }
    }
    public List<PortfolioMgt.Entity.ProjectPortfolio> GetCustomers()
    {
        using(PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
        {
            return pd.ProjectPortfolios.ToList();
        }
    }
    private void BindGrid()
    {
        var customerlist = GetCustomers();
        List<HealthCheck_Form> HfCollection = FormCollection(sessionKeys.PortfolioID);
        GridFormList.DataSource = (from h in HfCollection
                                   select new { h.CustomerID, h.FormBackColor, h.FormID, h.FormName, CustomerName = customerlist.Where(o => o.ID == h.CustomerID).FirstOrDefault().PortFolio }).ToList();
        GridFormList.DataBind();

       

    }

    private List<HealthCheck_Form> FormCollection(int portfolioID)
    {
        HealthCheckBAL hb = new HealthCheckBAL();
        if (portfolioID == 0)
            return hb.HealthCheck_Form_SelectAll();
        else
            return hb.HealthCheck_Form_SelectByCustomerID(portfolioID);
    }

    //private List<HealthCheck_Form> FormCollection(int customerid)
    //{
    //    HealthCheckBAL hb = new HealthCheckBAL();
    //    if (customerid == 0)
    //        return hb.HealthCheck_Form_SelectAll();
    //    else
    //        return hb.HealthCheck_Form_SelectByCustomerID(customerid);
    //}                           
    #region dropdown bind
    //private void Bind_CustomerDropdown()
    //{
    //    try
    //    {
    //        ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
    //        ddlPortfolio.DataTextField = "PortFolio";
    //        ddlPortfolio.DataValueField = "ID";
    //        ddlPortfolio.DataBind();
    //        //ddlPortfolio.Items.Insert(0, new ListItem("All", "0"));
    //        ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
    //    }
    //    catch(Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    #endregion

    protected void btnCreateForm_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/WF/Health/HC/FormDesignNew.aspx?dv={0}",string.Format("{0:ddMMyyhhmmss}",DateTime.Now)));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        ddlPortfolio.SelectedValue = "0";
        BindGrid();
    }
    protected void GridFormList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "Print")
        {
            //  Response.Redirect(string.Format("~/WF/Health/HC/FormPreviewNew.aspx?fid={0}", e.CommandArgument.ToString()),false);
            Response.Redirect(string.Format("~/WF/DC/FormPreviewNew.aspx?fid={0}", e.CommandArgument.ToString()), false);
        }
        else if (e.CommandName == "Copy")
        {
            //try
            //{
            //    int formid = Convert.ToInt32(e.CommandArgument.ToString());
            //    HealthCheckBAL hb = new HealthCheckBAL();
            //    var retval = hb.FormCopyToCustomers(formid);
            //    if (retval)
            //    {
            //        lblMsg.Text = "Copied successfully.";
            //    }
            //    BindGrid();
            //}
            //catch(Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
        }
        else if (e.CommandName == "Del")
        {
            try
            {
                int formid = Convert.ToInt32(e.CommandArgument.ToString());
                HealthCheckBAL hb = new HealthCheckBAL();
                hb.HealthCheck_Form_Delete(formid.ToString());
                //lblMsg.Text = "Deleted successfully.";
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        else
        {

            Response.Redirect(string.Format("~/WF/Health/HC/FormDesignNew.aspx?fid={0}&dv={1}", e.CommandArgument.ToString(), string.Format("{0:ddMMyyhhmmss}", DateTime.Now)),false);
        }
    }
    protected void imgbtn_Click(object sender, EventArgs e)
    {

    }
    protected void imgbtn_Click1(object sender, EventArgs e)
    {
       // cdate.Text = hid.Value; 
        //string s = hid.Value;
    }
    //protected void upanel_Load(object sender, EventArgs e)
    //{
    //    cdate.Text = hid.Value;
    //    if (!string.IsNullOrEmpty(cdate.Text) && ViewState["isfirst"] == null)
    //    {
    //        ViewState["isfirst"] = "1";
    //        ViewState["cval"] = cdate.Text;
    //        BindListView(cdate.Text);
    //    }
    //    else if (ViewState["cval"] != null)
    //    {
    //        if (cdate.Text != ViewState["cval"].ToString())
    //        {
    //            ViewState["cval"] = cdate.Text;
    //            BindListView(cdate.Text);
    //        }
    //    }
    //}
    //public void BindListView(string Type)
    //{
    //    list_mails.Visible = false;
    //    HealthCheckBAL hb = new HealthCheckBAL();
    //    if (!string.IsNullOrEmpty(cdate.Text))
    //    {
    //        list_mails.DataSource = hb.HealthCheckMail_SelectByFormID(int.Parse(Type));
    //        list_mails.DataBind();
    //        list_mails.Visible = true;
    //    }
    //}
    //protected void list_mails_ItemCommand(object sender, ListViewCommandEventArgs e)
    //{
    //    HealthCheckBAL hb = null;
    //    HealthCheckNameMailID m = null;
    //    try
    //    {
    //        if (e.CommandName == "UpdateItem")
    //        {
    //            hb = new HealthCheckBAL();
    //            m = hb.HealthCheckMail_SelectByID(Convert.ToInt32(e.CommandArgument));
    //            if (m != null)
    //            {
    //                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
    //                TextBox txtEmail = (TextBox)e.Item.FindControl("txtEmail");
    //                m.Name = txtName.Text.Trim();
    //                m.EmailID = txtEmail.Text.Trim();
    //                m.PortfolioHealthCheckID = int.Parse(hid.Value);

    //                try
    //                {
    //                    hb.HealthCheckMail_Update(m);
    //                    lblMsg.Text = "Updated successfully";
    //                    list_mails.EditIndex = -1;
    //                    BindListView(hid.Value);
    //                }
    //                catch (Exception ex)
    //                {
    //                    if (ex.Message.Contains("UNIQUE KEY"))
    //                    {
    //                        lblMsg.ForeColor = System.Drawing.Color.Red;
    //                        lblMsg.Text = "Email already exists.";
    //                    }
    //                }
    //            }
    //        }
    //        else if (e.CommandName == "Add")
    //        {
    //            hb = new HealthCheckBAL();
    //             m = new HealthCheckNameMailID();
    //            TextBox txtName1 = (TextBox)e.Item.FindControl("txtIName");
    //            TextBox txtEmail1 = (TextBox)e.Item.FindControl("txtIEmail");
    //            m.Name = txtName1.Text.Trim();
    //            m.EmailID = txtEmail1.Text.Trim();
    //            m.PortfolioHealthCheckID = int.Parse(hid.Value); ;
    //            if (txtName1.Text!="")
    //            {
    //                try
    //                {
    //                    hb.HealthCheckMail_Add(m);
    //                     BindListView(hid.Value);
    //                        lblMsg.Text = "Added successfully.";
    //                }
    //                catch(Exception ex)
    //                {
    //                    if (ex.Message.Contains("UNIQUE KEY"))
    //                    {
    //                        lblMsg.ForeColor = System.Drawing.Color.Red;
    //                        lblMsg.Text = "Email already exists.";
    //                    }
    //                }
                
    //            }
    //            else
    //            {
    //                lblMsg.ForeColor = System.Drawing.Color.Red;
    //                lblMsg.Text = "Name already exists.";
    //            }
    //        }
    //        else if (e.CommandName == "Del")
    //        {
    //            if (Convert.ToInt32(e.CommandArgument) > 0)
    //            {
    //                hb = new HealthCheckBAL();
    //                hb.HealthCheckMail_DeleteByID(Convert.ToInt32(e.CommandArgument));
    //                lblMsg.Text = "Deleted successfully";
    //                BindListView(hid.Value);
    //            }
    //        }
    //        else if (e.CommandName == "Cancel")
    //        {
    //            TextBox txtName = (TextBox)e.Item.FindControl("txtName");
    //            TextBox txtEmail = (TextBox)e.Item.FindControl("txtEmail");
    //            txtEmail.Text = string.Empty;
    //            txtName.Text = string.Empty;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //protected void list_mails_ItemCanceling(object sender, ListViewCancelEventArgs e)
    //{
    //    list_mails.EditIndex = -1;
    //    BindListView(hid.Value);
    //}
    //protected void list_mails_ItemEditing(object sender, ListViewEditEventArgs e)
    //{
    //    list_mails.EditIndex = e.NewEditIndex;
    //    BindListView(hid.Value);
    //}
    //protected void btnApply_Click(object sender, EventArgs e)
    //{
    //    if(!string.IsNullOrEmpty(hid.Value))
    //    {
    //        //Copy to another customer
    //        //lblCopymsg.Text = hid.Value +"-" + ddlCustomer.SelectedValue;
    //        HealthCheckBAL hb = new HealthCheckBAL();
    //       var retval=  hb.FormCopyToCustomer(Convert.ToInt32(hid.Value), Convert.ToInt32(ddlCustomer.SelectedValue));
    //       if (retval) lblCopymsg.Text = "Copied successfully.";
    //       else
    //       {
    //           lblCopymsg.ForeColor = System.Drawing.Color.Green;
    //           lblCopymsg.Text = "Form name already exists.";
    //       }

    //    }
    //}
    //protected void upanelcopy_Load(object sender, EventArgs e)
    //{
    //    //lblCopymsg.Text = hid.Value;
    //}

    protected void GridFormList_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            //Response.Redirect(string.Format("~/WF/Health/HC/FormPreviewNew.aspx?fid={0}", e.CommandArgument.ToString()), false);
            Response.Redirect(string.Format("~/WF/DC/FormPreview.aspx?fid={0}", e.CommandArgument.ToString()), false);
        }
        else if (e.CommandName == "Copy")
        {
            //try
            //{
            //    int formid = Convert.ToInt32(e.CommandArgument.ToString());
            //    HealthCheckBAL hb = new HealthCheckBAL();
            //    var retval = hb.FormCopyToCustomers(formid);
            //    if (retval)
            //    {
            //        lblMsg.Text = "Copied successfully.";
            //    }
            //    BindGrid();
            //}
            //catch(Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
        }
        else if (e.CommandName == "Del")
        {
            try
            {
                int formid = Convert.ToInt32(e.CommandArgument.ToString());
                HealthCheckBAL hb = new HealthCheckBAL();
                hb.HealthCheck_Form_Delete(formid.ToString());
                //lblMsg.Text = "Deleted successfully.";
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        else
        {

            Response.Redirect(string.Format("~/WF/Health/HC/FormDesignNew.aspx?fid={0}&dv={1}", e.CommandArgument.ToString(), string.Format("{0:ddMMyyhhmmss}", DateTime.Now)), false);
        }
    }

  

    //public void CopyMailToAllForms()
    //{
    //   HealthCheckBAL hb = new HealthCheckBAL();
    //   HealthCheckNameMailID m = new HealthCheckNameMailID();
    //            TextBox txtName1 = (TextBox)e.Item.FindControl("txtIName");
    //            TextBox txtEmail1 = (TextBox)e.Item.FindControl("txtIEmail");
    //            m.Name = txtName1.Text.Trim();
    //            m.EmailID = txtEmail1.Text.Trim();
    //            m.PortfolioHealthCheckID = int.Parse(hid.Value); ;
    //            if (txtName1.Text!="")
    //            {
    //                try
    //                {
    //                    hb.HealthCheckMail_Add(m);
    //                    lblMsg.Text = "Added successfully.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    //if (ex.Message.Contains("UNIQUE KEY"))
    //                    //{
    //                    //    lblMsg.ForeColor = System.Drawing.Color.Red;
    //                    //    lblMsg.Text = "Email already exists.";
    //                    //}
    //                }

    //            }
    //            else
    //            {
    //                lblMsg.ForeColor = System.Drawing.Color.Red;
    //                lblMsg.Text = "Name already exists.";
    //            }
    //}
}