using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class controls_CustomerPODetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                

                lblMsg.Visible = false;
                if (Request.QueryString["POID"] != null)
                {
                    hdnID.Value = Request.QueryString["POID"].ToString();
                }
                txtDateRaised.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now.ToShortDateString());
                //BindProjects(ddlProjects, 0);
                BindCustomers();
                BindUsers();
                BindData();
                BindPODetails();
                Visibility();

            }

            for (int i = 0; i < chkPaymentMode.Items.Count; i++)
            {
                chkPaymentMode.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    ViewState["update"] = Session["update"];
    //    // hiddenSession.Value = hidden;
    //}

    private void Visibility()
    {
        if (Request.QueryString["project"] != null)
        {
            if (sessionKeys.SID != 1)
            {
                imgSave.Visible = false;
            }
        }
      
    }

    #region "Bind DropdownList And Grid"
    private void BindData()
    {
        if (int.Parse(hdnID.Value) != 0)
        {
            PurchaseOrderMgtDataContext POMgt = new PurchaseOrderMgtDataContext();
            var details = (from r in POMgt.v_CustomerPO_Details
                           where r.ID == int.Parse(hdnID.Value)

                           select r).FirstOrDefault();

            if (details != null)
            {
                txtDateRaised.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), details.Date);
                txtDetails.Text = details.DetailsOfPO;
                txtPOExpDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), details.POExpiryDate);
                txtPONumber.Text = details.PONumber;
                txtRelatedToPO.Text = details.RelatedToPO;
                ddlCustomers.SelectedValue = details.CustomerID.ToString();
                ddlRaisedBy.SelectedValue = details.RaisedBy.ToString();
                chkPaymentMode.SelectedValue = details.PaymentMode.ToString();
                radPaymentType.SelectedValue = details.PaymentMethod.ToString();
                txtValue.Text = details.POValue.ToString();
                txtDurationDays.Text = details.DDays.ToString();
                CascadingDropDown1.SelectedValue = details.Project.ToString();
            }
        }

    }
    private void BindCustomers()
    {
        try
        {
            PortfolioDataContext portfolioDB = new PortfolioDataContext();



            var portfolio = from r in portfolioDB.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomers.DataSource = portfolio;
            ddlCustomers.DataTextField = "PortFolio";
            ddlCustomers.DataValueField = "ID";
            ddlCustomers.DataBind();
            ddlCustomers.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindUsers() //Raised By...
    {
        try
        {
            UserDataContext users = new UserDataContext();
            var sid = new int[] { 10, 8, 6 };
            var raisedBy = from r in users.Contractors
                           where !sid.Contains(r.SID.Value)
                           orderby r.ContractorName
                           select r;
            ddlRaisedBy.DataSource = raisedBy;
            ddlRaisedBy.DataValueField = "ID";
            ddlRaisedBy.DataTextField = "ContractorName";
            ddlRaisedBy.DataBind();
            ddlRaisedBy.Items.Insert(0, new ListItem("Please select...", "0"));
            if (Request.QueryString["New"] != null)
            {
                ddlRaisedBy.SelectedValue = sessionKeys.UID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindProjects(DropDownList ddlProjects, int setValue)
    {
        try
        {
            projectTaskDataContext portfolioDB = new projectTaskDataContext();



            var portfolio = from r in portfolioDB.ProjectDetails

                            orderby r.ProjectReferenceWithPrefix
                            select new { r.ProjectReference, Name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle };
            ddlProjects.DataSource = portfolio;
            ddlProjects.DataTextField = "Name";
            ddlProjects.DataValueField = "ProjectReference";
            ddlProjects.DataBind();
            ddlProjects.SelectedValue = setValue.ToString();
            ddlProjects.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindPODetails()
    {
        try
        {
            PurchaseOrderMgtDataContext POMgt = new PurchaseOrderMgtDataContext();

            //if (int.Parse(hdnID.Value) != 0)
            //{


            var details = from r in POMgt.v_CustomerPO_Details
                          where r.CustomerPOID == int.Parse(hdnID.Value)
                          || r.ID == -99
                          select r;
            if (details != null)
            {
                grdPODetails.DataSource = details;

                grdPODetails.DataBind();
            }
            //}

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    protected void grdPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                v_CustomerPO_Detail de = (v_CustomerPO_Detail)e.Row.DataItem;
                if (de.ID == -99)
                {
                    e.Row.Visible = false;
                }
                DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
               
                BindProjects(ddlProjectTitle, de.ProjectRef.Value);
                if (Request.QueryString["project"] != null)
                {
                    if (sessionKeys.SID != 1)
                    {
                        LinkButton linkButtonEdit = (LinkButton)e.Row.FindControl("LinkButtonEdit");
                        linkButtonEdit.Visible = false;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DropDownList ddlProjectTitlef = (DropDownList)e.Row.FindControl("ddlProjectTitlef");
                BindProjects(ddlProjectTitlef, 0);
                if (Request.QueryString["project"] != null)
                {
                    if (sessionKeys.SID != 1)
                    {
                        LinkButton linkAdd = (LinkButton)e.Row.FindControl("ImageButton5a");
                        LinkButton linkCancel = (LinkButton)e.Row.FindControl("ImageButton22");
                        linkAdd.Visible = false;
                        linkCancel.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {



        if (e.CommandName == "Add")
        {
            lblMsg.Visible = false;
            TextBox txtInvoiceNumf = (TextBox)grdPODetails.FooterRow.FindControl("txtInvoiceNumf");
            TextBox txtDateRaisedF = (TextBox)grdPODetails.FooterRow.FindControl("txtDateRaisedF");
            DropDownList ddlProjectTitlef = (DropDownList)grdPODetails.FooterRow.FindControl("ddlProjectTitlef");
            TextBox txtTotalAmtf = (TextBox)grdPODetails.FooterRow.FindControl("txtTotalAmtf");
            TextBox txtCurrenceyf = (TextBox)grdPODetails.FooterRow.FindControl("txtCurrenceyf");
            TextBox txtTotalPaidf = (TextBox)grdPODetails.FooterRow.FindControl("txtTotalPaidf");
            TextBox txtNotesf = (TextBox)grdPODetails.FooterRow.FindControl("txtNotesf");
            TextBox txtRetentionValuef = (TextBox)grdPODetails.FooterRow.FindControl("txtRetentionValuef");
            if (int.Parse(hdnID.Value) != 0)
            {
                PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
                Customer_PODatabseDetail insert = new Customer_PODatabseDetail();
                insert.Balance = 0;//C (Convert.ToDecimal(txtTotalAmtf.Text) - Convert.ToDecimal(txtTotalPaidf.Text));
                insert.Currencey = txtCurrenceyf.Text;
                insert.CustomerPOID = int.Parse(hdnID.Value);
                insert.DateRaised = Convert.ToDateTime(txtDateRaisedF.Text);
                insert.InvoiceNumber = txtInvoiceNumf.Text;
                insert.Notes = txtNotesf.Text;
                insert.ProjectRef = Convert.ToInt32(ddlProjectTitlef.SelectedValue);
                insert.RetentionValue = Convert.ToInt32(txtRetentionValuef.Text);
                insert.TotalAmount = Convert.ToDecimal(txtTotalAmtf.Text);
                insert.TotalPaid = Convert.ToDecimal(txtTotalPaidf.Text);
                POInsert.Customer_PODatabseDetails.InsertOnSubmit(insert);
                POInsert.SubmitChanges();
                BindPODetails();


                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Successfully Added New Record";
                //if (ID != 0)
                //{


                //}
                //else
                //{
                //    lblMsg.Visible = true;
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    lblMsg.Text = "Failed to Add New Record";
                //  //BindPODetails();
                //}

            }




        }


        if (e.CommandName == "Update")
        {
            lblMsg.Visible = false;
            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
            int index = grdPODetails.EditIndex;
            GridViewRow row = grdPODetails.Rows[index];
            TextBox txtInvoiceNum = (TextBox)row.FindControl("txtInvoiceNum");
            TextBox txtDateRaisedG = (TextBox)row.FindControl("txtDateRaisedG");
            DropDownList ddlProjectTitle = (DropDownList)row.FindControl("ddlProjectTitle");
            TextBox txtTotalAmt = (TextBox)row.FindControl("txtTotalAmt");
            TextBox txtCurrencey = (TextBox)row.FindControl("txtCurrencey");
            TextBox txtTotalPaid = (TextBox)row.FindControl("txtTotalPaid");
            TextBox txtNotes = (TextBox)row.FindControl("txtNotes");
            TextBox txtRetentionValue = (TextBox)row.FindControl("txtRetentionValue");

            Customer_PODatabseDetail POUpdate =
                POInsert.Customer_PODatabseDetails.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
            POUpdate.DateRaised = Convert.ToDateTime(txtDateRaisedG.Text);
            POUpdate.InvoiceNumber = txtInvoiceNum.Text;
            POUpdate.Notes = txtNotes.Text;
            POUpdate.ProjectRef = int.Parse(ddlProjectTitle.SelectedValue);
            // POUpdate.RetentionReminder=0;
            POUpdate.RetentionValue = int.Parse(txtRetentionValue.Text);
            POUpdate.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
            POUpdate.TotalPaid = Convert.ToDecimal(txtTotalPaid.Text);
            POInsert.SubmitChanges();
            lblMsg.Visible = true;
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Updated Successfully";
            grdPODetails.EditIndex = -1;
            BindPODetails();


        }
    }


    protected void grdPODetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdPODetails.EditIndex = e.NewEditIndex;
        BindPODetails();

    }
    protected void grdPODetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdPODetails.EditIndex = -1;
        BindPODetails();
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {

            projectTaskDataContext project = new projectTaskDataContext();

            project.SubmitChanges();
            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
            var chk = (from r in POInsert.Customer_PODatabases
                       where r.PONumber == txtPONumber.Text.Trim() && r.ProjectRef == (int.Parse(ddlProjects.SelectedValue))
                       select r).ToList();
            var chkPO = (from r in project.Projects
                         where r.ProjectReference == int.Parse(ddlProjects.SelectedValue)
                         select r).ToList().FirstOrDefault();
            if (chkPO != null)
            {
                if ((chkPO.CustomerReference == null && chkPO.Custom1 == null) || (chkPO.CustomerReference == "" && chkPO.Custom1 == ""))
                {
                    Project update = project.Projects.Single(P => P.ProjectReference ==
                                                Convert.ToInt32(ddlProjects.SelectedValue));
                    update.CustomerReference = txtPONumber.Text.Trim();
                    update.Custom1 = txtPONumber.Text.Trim();
                    project.SubmitChanges();
                }
            }

            if (chk.Count == 0)
            {

                Customer_PODatabase insert = new Customer_PODatabase();
                insert.CustomerID = int.Parse(ddlCustomers.SelectedValue);
                insert.DateRaised = Convert.ToDateTime(txtDateRaised.Text);
                insert.DetailsOfPO = txtDetails.Text;
                insert.PaymentMethod = int.Parse(radPaymentType.SelectedValue);
                insert.PaymentMode = int.Parse(chkPaymentMode.SelectedValue);
                insert.POExpiryDate = Convert.ToDateTime(txtPOExpDate.Text);
                insert.PONumber = txtPONumber.Text.Trim();
                insert.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
                insert.RelatedToPO = txtRelatedToPO.Text;
                insert.Value = int.Parse(txtValue.Text);
                insert.DDays = int.Parse(txtDurationDays.Text);
                insert.ProjectRef = int.Parse(ddlProjects.SelectedValue);
                POInsert.Customer_PODatabases.InsertOnSubmit(insert);
                POInsert.SubmitChanges();
                Customer_PODatabase poID = new Customer_PODatabase();
                int ID = insert.ID;
                hdnID.Value = ID.ToString();
                if (ID != 0)
                {
                    //lblMsg.Visible = true;
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    //lblMsg.Text = "Successfully Saved";
                    //BindPODetails();
                }
                else
                {
                    //lblMsg.Visible = true;
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "Failed to save";
                    //BindPODetails();
                }



            }
            else
            {
                Customer_PODatabase insert =
                                       POInsert.Customer_PODatabases.Single(P => P.ID == int.Parse(hdnID.Value.ToString()));
                insert.CustomerID = int.Parse(ddlCustomers.SelectedValue);
                insert.DateRaised = Convert.ToDateTime(txtDateRaised.Text);
                insert.DetailsOfPO = txtDetails.Text;
                insert.PaymentMethod = int.Parse(radPaymentType.SelectedValue);
                insert.PaymentMode = int.Parse(chkPaymentMode.SelectedValue);
                insert.POExpiryDate = Convert.ToDateTime(txtPOExpDate.Text);
                // insert.PONumber = txtPONumber.Text;
                insert.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
                insert.RelatedToPO = txtRelatedToPO.Text;
                insert.Value = int.Parse(txtValue.Text);
                insert.DDays = int.Parse(txtDurationDays.Text);

                POInsert.SubmitChanges();
            }
            if (Request.QueryString["project"] != null)
            {
                Response.Redirect("ProjectFinancial_CustomerPO.aspx?project="+Request.QueryString["project"].ToString());
            }
            else
            {
                Response.Redirect("POJournal.aspx");
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdPODetails.EditIndex = -1;
        BindPODetails();
    }
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            PurchaseOrderMgtDataContext POInsert = new PurchaseOrderMgtDataContext();
            //Customer_PODatabase insert = new Customer_PODatabase();

            Customer_PODatabase insert =
                        POInsert.Customer_PODatabases.Single(P => P.ID == int.Parse(hdnID.Value.ToString()));
            insert.CustomerID = int.Parse(ddlCustomers.SelectedValue);
            insert.DateRaised = Convert.ToDateTime(txtDateRaised.Text);
            insert.DetailsOfPO = txtDetails.Text;
            insert.PaymentMethod = int.Parse(radPaymentType.SelectedValue);
            insert.PaymentMode = int.Parse(chkPaymentMode.SelectedValue);
            insert.POExpiryDate = Convert.ToDateTime(txtPOExpDate.Text);
            // insert.PONumber = txtPONumber.Text;
            insert.RaisedBy = int.Parse(ddlRaisedBy.SelectedValue);
            insert.RelatedToPO = txtRelatedToPO.Text;
            insert.Value = int.Parse(txtValue.Text);
            insert.DDays = int.Parse(txtDurationDays.Text);

            POInsert.SubmitChanges();
            //Customer_PODatabase poID = new Customer_PODatabase();
            //int ID = insert.ID;
            //hdnID.Value = ID.ToString();



            Response.Redirect("POJournal.aspx");

            //BindPODetails();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}