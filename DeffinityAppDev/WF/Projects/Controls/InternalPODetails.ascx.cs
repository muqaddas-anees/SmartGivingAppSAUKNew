using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public partial class controls_InternalPODetails : System.Web.UI.UserControl
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                if (Request.QueryString["POID"] != null)
                {
                    hdnID.Value = Request.QueryString["POID"].ToString();
                }
                lblMsg.Visible = false;
                BindContractors(ddlRequestedBy);
                BindContractors(ddlApprover);
                BindContractors(ddlPurchaser);
                BindVendors();
                BindDetails();
                BindVendorsDate(int.Parse(ddlVendor.SelectedValue));
                BindProjects();
                BindGrid();
                Visibility();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void Visibility()
    {
        if (Request.QueryString["project"] != null)
        {
            if (sessionKeys.SID != 1)
            {
                imgSaveAndSubmit.Visible = false;
            }
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["update"] = Session["update"];
        // hiddenSession.Value = hidden;
    }
    #region Bind Dropdownlist Control and Grid

    private void BindDetails()
    {
        PurchaseOrderMgtDataContext PurchaseDetails = new PurchaseOrderMgtDataContext();
        if (int.Parse(hdnID.Value) != 0)
        {
            var details = (from r in PurchaseDetails.v_PurchaseDetails
                           where r.GenInfID == int.Parse(hdnID.Value)
                           select r).ToList().FirstOrDefault();
            if (details != null)
            {
                txtAddress1.Text = details.Address;
                txtAddress2.Text = details.HQAddress;
                txtContactName.Text = details.ContractorName;
                txtContactTel.Text = details.ContactNumber;
                txtEmail.Text = details.EmailAddress;
                txtNotes.Text = details.GNNotes;
                txtPODate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), details.date);
                txtPONumber.Text = details.PONumber;
                txtPostcode.Text = details.PostCode;
                ddlApprover.SelectedValue = details.PurchasedBy.Value.ToString();
                ddlProject.SelectedValue = details.ProjectRef.ToString();
                ddlPurchaser.SelectedValue = details.PurchasedBy.Value.ToString();
                ddlRequestedBy.SelectedValue = details.RequestedBy.Value.ToString();
                ddlVendor.SelectedValue = details.VendorID.Value.ToString();
            }
        }
    }



    private void BindContractors(DropDownList ddlUsers)
    {


        try
        {
            UserDataContext users = new UserDataContext();
            var sid = new int[] { 10, 8, 6 };
            var raisedBy = from r in users.Contractors
                           where !sid.Contains(r.SID.Value)
                           orderby r.ContractorName
                           select r;
            ddlUsers.DataSource = raisedBy;
            ddlUsers.DataValueField = "ID";
            ddlUsers.DataTextField = "ContractorName";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
            //ddlUsers.SelectedValue = setvalue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindVendors()
    {


        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

            var vendorsList = from r in Vendors.v_Vendors

                              orderby r.ContractorName
                              select r;
            ddlVendor.DataSource = vendorsList;
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "ContractorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
            //ddlUsers.SelectedValue = setvalue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindProjects()
    {
        try
        {
            projectTaskDataContext portfolioDB = new projectTaskDataContext();



            var portfolio = from r in portfolioDB.ProjectDetails

                            orderby r.ProjectReferenceWithPrefix
                            select r;
            ddlProject.DataSource = portfolio;
            ddlProject.DataTextField = "ProjectReferenceWithPrefix";
            ddlProject.DataValueField = "ProjectReference";
            ddlProject.DataBind();

            ddlProject.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindGrid()
    {
        try
        {
            //PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();

            //var details = (from r in PODetails.PO_GenInformations
            //               where r.ID == int.Parse(Request.QueryString["POID"]) 
            //               select r).ToList().FirstOrDefault();
            if (Request.QueryString["POID"] != null)
            {
                DataTable dt = new DataTable();

                //if (details != null)
                //{

                //}

                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "PO_Purchase",
                    new SqlParameter("@ID", int.Parse(Request.QueryString["POID"]))).Tables[0];
                grdPODetails.DataSource = dt;
                grdPODetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion




    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVendorsDate(int.Parse(ddlVendor.SelectedValue));

    }
    private void BindVendorsDate(int VendorID)
    {
        PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();
        var vendorDetails = (from r in PODetails.v_Vendors
                             where r.VendorID == VendorID
                             select r).ToList().FirstOrDefault();
        if (vendorDetails != null)
        {
            txtAddress1.Text = vendorDetails.HQAddress;
            txtAddress2.Text = vendorDetails.Address;
            txtContactName.Text = vendorDetails.ContractorName;
            txtContactTel.Text = vendorDetails.ContactNumber;
            txtEmail.Text = vendorDetails.EmailAddress;
            txtPostcode.Text = vendorDetails.PostCode;
            txtSupplierMail.Text = vendorDetails.EmailAddress;
        }
    }
    protected void grdPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblID = (Label)e.Row.FindControl("lblID");
                //v_PurchaseDetail de = (v_PurchaseDetail)e.Row.DataItem;
                //if (de.ID == -99)
                //{
                //    e.Row.Visible = false;
                //}
                if (lblID != null)
                {
                    if (int.Parse(lblID.Text) == -99)
                    {
                        e.Row.Visible = false;
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
        try
        {
            if (e.CommandName == "Add")
            {
                lblMsg.Visible = false;
                if (ViewState["update"].ToString() == Session["update"].ToString())
                {
                    if (int.Parse(hdnID.Value) != 0)
                    {
                        TextBox txtItemNumberf = (TextBox)grdPODetails.FooterRow.FindControl("txtItemNumberf");
                        TextBox txtDescriptionf = (TextBox)grdPODetails.FooterRow.FindControl("txtDescriptionf");
                        TextBox txtPartNumberf = (TextBox)grdPODetails.FooterRow.FindControl("txtPartNumberf");
                        TextBox txtUnitf = (TextBox)grdPODetails.FooterRow.FindControl("txtUnitf");
                        TextBox txtQtyf = (TextBox)grdPODetails.FooterRow.FindControl("txtQtyf");
                        TextBox txtPricef = (TextBox)grdPODetails.FooterRow.FindControl("txtPricef");
                        TextBox txtQtyRecf = (TextBox)grdPODetails.FooterRow.FindControl("txtQtyRecf");
                        //TextBox txtQtyOutf = (TextBox)grdPODetails.FooterRow.FindControl("txtQtyOutf");
                        TextBox txtNotesf = (TextBox)grdPODetails.FooterRow.FindControl("txtNotesf");
                        if (int.Parse(txtQtyf.Text) >= int.Parse(txtQtyRecf.Text))
                        {
                            PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();
                            PO_GoodsDetail insert = new PO_GoodsDetail();
                            insert.Description = txtDescriptionf.Text;
                            insert.GenInfID = int.Parse(hdnID.Value);
                            insert.ItemNumber = txtItemNumberf.Text;
                            insert.Notes = txtNotesf.Text;
                            insert.PartNumber = txtPartNumberf.Text;
                            insert.Price = Convert.ToDouble(txtPricef.Text);
                            insert.QtyOrderd = int.Parse(txtQtyf.Text);
                            insert.QtyOut = int.Parse(txtQtyf.Text) - int.Parse(txtQtyRecf.Text);//int.Parse(txtQtyOutf.Text);
                            insert.QtyRec = int.Parse(txtQtyRecf.Text);
                            insert.Unit = int.Parse(txtUnitf.Text);

                            PODetails.PO_GoodsDetails.InsertOnSubmit(insert);
                            PODetails.SubmitChanges();
                            lblMsg.Visible = true;
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            lblMsg.Text = "Successfully Added New Record";
                            BindGrid();
                            //int ID = insert.ID;
                            //hdnID.Value = ID.ToString();
                            //if (ID != 0)
                            //{
                            //    lblMsg.Visible = true;
                            //    lblMsg.ForeColor = System.Drawing.Color.Green;
                            //    lblMsg.Text = "Successfully Added New Record";
                            //    BindGrid();
                            //}
                            //else
                            //{
                            //    lblMsg.Visible = true;
                            //    lblMsg.ForeColor = System.Drawing.Color.Red;
                            //    lblMsg.Text = "Failed to Add New Record";
                            //    BindGrid();
                            //}
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Text = "Error:Please enter Qty more than received Qty";
                            BindGrid();
                        }
                    }
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }//to check session
            }

            if (e.CommandName == "Update")
            {
                lblMsg.Visible = false;
                if (ViewState["update"].ToString() == Session["update"].ToString())
                {
                    PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();
                    int index = grdPODetails.EditIndex;
                    GridViewRow row = grdPODetails.Rows[index];
                    TextBox txtItemNumber = (TextBox)row.FindControl("txtItemNumber");
                    TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                    TextBox txtPartNumber = (TextBox)row.FindControl("txtPartNumber");
                    TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                    TextBox txtQty = (TextBox)row.FindControl("txtQty");
                    TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
                    TextBox txtQtyRec = (TextBox)row.FindControl("txtQtyRec");
                    //TextBox txtQtyOut = (TextBox)row.FindControl("txtQtyOut");
                    TextBox txtNotes = (TextBox)row.FindControl("txtNotes");
                    if (int.Parse(txtQty.Text) >= int.Parse(txtQtyRec.Text))
                    {
                        PO_GoodsDetail POUpdate =
                            PODetails.PO_GoodsDetails.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                        POUpdate.Description = txtDescription.Text;
                        POUpdate.ItemNumber = txtItemNumber.Text;
                        POUpdate.Notes = txtNotes.Text;
                        POUpdate.PartNumber = txtPartNumber.Text;
                        POUpdate.Price = Convert.ToDouble(txtPrice.Text);
                        POUpdate.QtyOrderd = int.Parse(txtQty.Text);
                        POUpdate.QtyOut = int.Parse(txtQty.Text) - int.Parse(txtQtyRec.Text);// int.Parse(txtQtyOut.Text);
                        POUpdate.QtyRec = int.Parse(txtQtyRec.Text);
                        POUpdate.Unit = int.Parse(txtUnit.Text);
                        PODetails.SubmitChanges();
                        grdPODetails.EditIndex = -1;

                        BindGrid();
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Successfully Updated";
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Error:Please enter Qty more than received Qty";
                        grdPODetails.EditIndex = -1;

                        BindGrid();
                    }
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["update"].ToString() == Session["update"].ToString())
            {
                PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();

                var myPOGenInfo = (from r in PODetails.PO_GenInformations
                                   where r.PONumber == txtPONumber.Text
                                   select r).ToList();
                int ID = 0;

                if (myPOGenInfo.Count > 0)
                {

                    PO_GenInformation POUpdate =
                               PODetails.PO_GenInformations.Single(P => P.PONumber == txtPONumber.Text);

                    PO_GenInformation insert = new PO_GenInformation();
                    insert.ApprovedBy = int.Parse(ddlApprover.SelectedValue);
                    insert.date = Convert.ToDateTime(txtPODate.Text);
                    insert.Notes = txtNotes.Text;
                    insert.PONumber = txtPONumber.Text;
                    insert.ProjectRef = int.Parse(ddlProject.SelectedValue);
                    insert.PurchasedBy = int.Parse(ddlPurchaser.SelectedValue);
                    insert.RequestedBy = int.Parse(ddlRequestedBy.SelectedValue);
                    insert.VendorID = int.Parse(ddlVendor.SelectedValue);
                    //PODetails.PO_GenInformations.InsertOnSubmit(insert);
                    PODetails.SubmitChanges();
                    ID = POUpdate.ID;
                    hdnID.Value = POUpdate.ID.ToString();
                }

                else
                {
                    PO_GenInformation insert = new PO_GenInformation();
                    insert.ApprovedBy = int.Parse(ddlApprover.SelectedValue);
                    insert.date = Convert.ToDateTime(txtPODate.Text);
                    insert.Notes = txtNotes.Text;
                    insert.PONumber = txtPONumber.Text;
                    insert.ProjectRef = int.Parse(ddlProject.SelectedValue);
                    insert.PurchasedBy = int.Parse(ddlPurchaser.SelectedValue);
                    insert.RequestedBy = int.Parse(ddlRequestedBy.SelectedValue);
                    insert.VendorID = int.Parse(ddlVendor.SelectedValue);
                    PODetails.PO_GenInformations.InsertOnSubmit(insert);
                    PODetails.SubmitChanges();
                    ID = insert.ID;
                    hdnID.Value = ID.ToString();
                }
                if (ID != 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Successfully Saved";
                    BindGrid();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Failed to save";
                    BindGrid();
                }
                BindGrid();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdPODetails.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void grdPODetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            grdPODetails.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/WF/Vendors/RFIVendorOverview.aspx");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region "Mail functionality Add"


    private void Mailer()
    {
        try
        {
            string RecieveraName, RecieverEmail, FromEmail, ManagerName, ManagerEmail, ProjectReference, instance, Contactno, OwnerMail, ownerNo;
            projectTaskDataContext portfolioDB = new projectTaskDataContext();
            var qoutes = (from r in portfolioDB.ProjectBOMSupRequisitions
                          where r.PONumber == txtPONumber.Text.Trim()
                          select r).ToList().FirstOrDefault();
            DataTable dt;
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Quote_projectDetails", new SqlParameter("@ProjectReference", int.Parse(ddlProject.SelectedValue))).Tables[0];
            ProjectReference = dt.Rows[0]["ProjectReference"].ToString() + " [" + dt.Rows[0]["Projecttitle"].ToString() + "]";
            ManagerName = dt.Rows[0]["ManagerName"].ToString();
            ManagerEmail = dt.Rows[0]["ManagerEmail"].ToString().Trim();
            Contactno = dt.Rows[0]["Contactno"].ToString().Trim();
            FromEmail = dt.Rows[0]["FromEmail"].ToString().Trim();
            OwnerMail = dt.Rows[0]["OwnerMail"].ToString().Trim();
            ownerNo = dt.Rows[0]["OwnerNo"].ToString().Trim();
            instance = dt.Rows[0]["instance"].ToString().Trim();
            RecieveraName = txtContactName.Text.Trim();
            RecieverEmail = txtSupplierMail.Text.Trim();
            SupplierMail.Visible = true;
            
            SupplierMail.BindControls1(RecieveraName, ProjectReference, ManagerName, ManagerEmail, Contactno, instance, OwnerMail, ownerNo, ManagerName);
            ArrayList ToEmailIds = new ArrayList(0);
            ToEmailIds.Add(ManagerEmail);
            ToEmailIds.Add(txtSupplierMail.Text.Trim());

            string htmlText = string.Empty;
            StringWriter sw = new StringWriter();
            Html32TextWriter htmlTW = new Html32TextWriter(sw);
            SupplierMail.RenderControl(htmlTW);
            htmlText = htmlTW.InnerWriter.ToString();
            string errorString = string.Empty;
            Email eMail = new Email();
            Attachment attachment1 = BindReport(int.Parse(ddlVendor.SelectedValue), int.Parse(ddlProject.SelectedValue), qoutes.QuoteID.Value);





            if (!string.IsNullOrEmpty(RecieverEmail))
            {
                eMail.SendingMail(ToEmailIds, "Purchase order For " + ProjectReference, htmlText, FromEmail, attachment1);
                lblMsg.Text = "Purchase order form has been sent successfully.";

            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            SupplierMail.Visible = false;
            //Response.Redirect(string.Format("~/ProjectBOM.aspx?project={0}", QueryStringValues.Project), false);
        }



    }

    private Attachment BindReport(int SupplierID, int ProjectReference, int QuoteID)
    {
        Attachment at = null;
        try
        {
            rpt = new ReportDocument();
            string projectRef = QueryStringValues.Project.ToString();
            projectTaskDataContext project = new projectTaskDataContext();
            var projects = (from r in project.ProjectDetails
                            where r.ProjectReference == ProjectReference
                            select r).ToList().FirstOrDefault();
            if (projects != null)
            {
                projectRef = projects.ProjectReferenceWithPrefix;
            }


            string str = "~/WF/Reports/ProjectBOMSR.rpt";
            rpt.Load(Server.MapPath(str));

            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            SqlConnection MyCon = new SqlConnection(strConn);
            SqlCommand MyCommand = new SqlCommand("ProjectBOM_SupplierRequistion", MyCon);
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.Parameters.AddWithValue("@SupplierID", SupplierID);
            MyCommand.Parameters.AddWithValue("@ProjectReference", ProjectReference);
            MyCommand.Parameters.AddWithValue("@QuoteID", QuoteID);
            SqlDataAdapter myAdapter = new SqlDataAdapter(MyCommand);
            myAdapter.Fill(dt);

            DataTable dt1 = new DataTable();
            SqlCommand MyCommand1 = new SqlCommand("ProjectBOM_SupplierRequistionGrid", MyCon);
            MyCommand1.CommandType = CommandType.StoredProcedure;
            MyCommand1.Parameters.AddWithValue("@SupplierID", SupplierID);
            MyCommand1.Parameters.AddWithValue("@ProjectReference", ProjectReference);
            MyCommand1.Parameters.AddWithValue("@QuoteID", QuoteID);
            SqlDataAdapter myAdapter1 = new SqlDataAdapter(MyCommand1);
            myAdapter1.Fill(dt1);



            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);

            rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports[0].SetDataSource(dt1);

            string path = "~/WF/UploadData/Invoices/" + "PO_" + projectRef + "_" + HttpContext.Current.User.Identity.Name + ".pdf";
            //string path = "~//SupplierRequisitions//" +projectRef + "_" + User.Identity.Name + ".pdf";
            //Stream rptstream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);

            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(path));

            at = new Attachment(Server.MapPath(path));

            //rpt.ExportToDisk(ExportFormatType.PortableDocFormat,"Quote"+QuoteID);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return at;

    }

    protected void imgSaveAndSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["update"].ToString() == Session["update"].ToString())
            {
                PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();

                var myPOGenInfo = (from r in PODetails.PO_GenInformations
                                   where r.PONumber == txtPONumber.Text
                                   select r).ToList();
                int ID = 0;

                if (myPOGenInfo.Count > 0)
                {

                    PO_GenInformation POUpdate =
                               PODetails.PO_GenInformations.Single(P => P.PONumber == txtPONumber.Text);

                    PO_GenInformation insert = new PO_GenInformation();
                    insert.ApprovedBy = int.Parse(ddlApprover.SelectedValue);
                    insert.date = Convert.ToDateTime(txtPODate.Text);
                    insert.Notes = txtNotes.Text;
                    insert.PONumber = txtPONumber.Text;
                    insert.ProjectRef = int.Parse(ddlProject.SelectedValue);
                    insert.PurchasedBy = int.Parse(ddlPurchaser.SelectedValue);
                    insert.RequestedBy = int.Parse(ddlRequestedBy.SelectedValue);
                    insert.VendorID = int.Parse(ddlVendor.SelectedValue);
                    //PODetails.PO_GenInformations.InsertOnSubmit(insert);
                    PODetails.SubmitChanges();
                    ID = POUpdate.ID;
                    hdnID.Value = POUpdate.ID.ToString();
                }

                else
                {
                    PO_GenInformation insert = new PO_GenInformation();
                    insert.ApprovedBy = int.Parse(ddlApprover.SelectedValue);
                    insert.date = Convert.ToDateTime(txtPODate.Text);
                    insert.Notes = txtNotes.Text;
                    insert.PONumber = txtPONumber.Text;
                    insert.ProjectRef = int.Parse(ddlProject.SelectedValue);
                    insert.PurchasedBy = int.Parse(ddlPurchaser.SelectedValue);
                    insert.RequestedBy = int.Parse(ddlRequestedBy.SelectedValue);
                    insert.VendorID = int.Parse(ddlVendor.SelectedValue);
                    PODetails.PO_GenInformations.InsertOnSubmit(insert);
                    PODetails.SubmitChanges();
                    ID = insert.ID;
                    hdnID.Value = ID.ToString();
                }
                if (ID != 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Successfully Saved";
                    BindGrid();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Failed to save";
                    BindGrid();
                }
                BindGrid();
                Mailer();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
}