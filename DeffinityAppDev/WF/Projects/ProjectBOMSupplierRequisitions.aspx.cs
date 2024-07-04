using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using POMgt.DAL;
using POMgt.Entity;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.SqlClient;
using Deffinity.ServiceCatalogManager;
using System.Text;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using POMgt.DAL;
using POMgt.Entity;
using Quote.BLL;
public partial class ProjectBOMSupplierRequisitions : System.Web.UI.Page
{
    
    ReportDocument rpt;
    public double Total = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (rpt != null)
            {
                rpt.Close();
                rpt.Dispose();
                GC.Collect();
            }


            if (!IsPostBack)
            {
                imgSave.Visible = false;
                lblError.Visible = false;
               // Master.PageHead = "Project Management";
                BindPOnumber();
                BindVendors();
                BindSites();
                BindDataDetails();
                if (Request.QueryString["QuoteID"] != null)
                {
                    BindGrid(int.Parse(Request.QueryString["QuoteID"]));
                }
            }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Bind Dropdowns and Grid
    private void BindPOnumber()
    {
        try
        {
            PurchaseOrderMgtDataContext POMgt = new PurchaseOrderMgtDataContext();


          
                var sites = (from r in POMgt.Customer_PODatabases
                               where r.ProjectRef == QueryStringValues.Project
                              
                             select new {r.ID, r.PONumber }).ToList();
                ddlPonumber.DataSource = sites;
                ddlPonumber.DataBind();
                ddlPonumber.DataTextField = "PONumber";
                ddlPonumber.DataValueField = "ID";
                ddlPonumber.DataBind();
                ddlPonumber.Items.Insert(0, new ListItem("Please select", "0"));
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindSites()
    {
        try
        {
        Location.DAL.LocationDataContext location = new  Location.DAL.LocationDataContext();
            //Location.DAL.LocationDataContext location = new Location.DAL.LocationDataContext();
            //get portfolio from Project table..

            projectTaskDataContext project = new projectTaskDataContext();
            var portfolioID = (from r in project.Projects
                               where r.ProjectReference == QueryStringValues.Project
                               select r).ToList().FirstOrDefault();

            if (portfolioID != null)
            {
                var sites = (from r in location.AssignedSitesToPortfolios
                             join s in location.Sites on r.SiteID equals s.ID
                             where r.Portfolio == portfolioID.Portfolio
                             orderby s.Site1
                             select new { s.ID, s.Site1 }).ToList();
                ddlSites.DataSource = sites;
                ddlSites.DataBind();
                ddlSites.DataTextField = "Site1";
                ddlSites.DataValueField = "ID";
                ddlSites.DataBind();
                ddlSites.Items.Insert(0, new ListItem("Please select", "0"));
            }
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

            projectTaskDataContext BOM = new projectTaskDataContext();

            var getSuppliers = (from r in BOM.ProjectBOMDetils
                                where r.ProjectReference == QueryStringValues.Project
                                select r).ToList();


            var vendorsList =from r1 in Vendors.v_Vendors
                              orderby r1.ContractorName
                              select r1;

            var Suppliers = (from s in getSuppliers
                             join s1 in vendorsList on s.Supplier equals s1.VendorID
                             select new { s1.VendorID, s1.ContractorName }).ToList().Distinct();



            ddlSupplier.DataSource = Suppliers;
            ddlSupplier.DataValueField = "VendorID";
            ddlSupplier.DataTextField = "ContractorName";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("Please select...", "0"));
            //ddlSupplier.SelectedValue = setvalue.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindGrid1(int QouteID)
    {
        try
        {
            projectTaskDataContext BOM = new projectTaskDataContext();
            var bomList = (from r in BOM.ProjectBOMDetils
                           where r.ProjectReference == QueryStringValues.Project
                           select new ProjectBOMDetils{Supplier=r.Supplier, ProjectReference=r.ProjectReference, ID=r.ID,PartNumber=r.PartNumber,
                               Worksheet=r.Worksheet,Description=r.Description,
                               Unit=string.Format("{0:f2}",(r.Labour+r.Material+r.Mics)),
                               Qty=r.Qty,Total=r.Total}).ToList();
            var QouteList = (from r in BOM.SupplierQUOTE_ITEMs
                             where r.QuoteID == QouteID && r.ProjectReference == QueryStringValues.Project
                             select r).ToList();

            var GdList = (from g in BOM.GoodsReceipts
                                join p in BOM.ProjectBOMs on g.BOMID equals p.ID
                                select g).ToList();

            var SuppliersReq = (from s in bomList
                                join s1 in QouteList on s.ID equals s1.ItemNo
                                join g1 in GdList on s.ID equals g1.BOMID
                                
                        into e_e2
                                from gg1 in e_e2.DefaultIfEmpty()
                                where s.ID == null && (gg1.ID == null || gg1.BOMID == null)
                                select new
                                {
                                    ID = s.ID,
                                    PartNumber = s.PartNumber,
                                    Worksheet = s.Worksheet,
                                    Description = s.Description,
                                    Unit=Convert.ToDouble(string.IsNullOrEmpty(s.Unit)?"0":s.Unit) ,
                                     //Qty = s.Qty,
                                    Qty = (gg1.QtyOrdered ==null ? 0 : gg1.QtyOrdered),
                                    Total = s.Total,
                                    s.ProjectReference
                                }).ToList().Distinct();


           
           

            

            if (int.Parse(ddlSupplier.SelectedValue) != 0)
            {
                var SuppliersReq1 = (from s in bomList
                                     join s1 in QouteList on s.ID equals s1.ItemNo
                                     join g1 in GdList on s.ID equals g1.BOMID

                         into e_e2
                                     from gg1 in e_e2.DefaultIfEmpty()
                                     where s.Supplier == int.Parse(ddlSupplier.SelectedValue)
                                     && s.ID == null && (gg1.ID == null || gg1.BOMID == null)
                                     select new
                                     {
                                         ID = s.ID,
                                         PartNumber = s.PartNumber,
                                         Worksheet = s.Worksheet,
                                         Description = s.Description,
                                         s=  s.Material,
                                         Unit = Convert.ToDouble(string.IsNullOrEmpty(s.Unit) ? "0" : s.Unit),
                                         //Qty = s.Qty,
                                         Qty = (gg1.QtyOrdered == null ? 0 : gg1.QtyOrdered),
                                         Total = s.Total,
                                         s.ProjectReference
                                     }).ToList().Distinct();

                


                gridBOM.DataSource = SuppliersReq1;
                gridBOM.DataBind();
            }
            else
            {
                gridBOM.DataSource = SuppliersReq;
                gridBOM.DataBind();
            }

           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    #region "BindGrid"

    private void BindGridUpdate(int QouteID)
    {
        projectTaskDataContext BOM = new projectTaskDataContext();

        var SuppliersReq = BOM.ExecuteQuery<ProjectBOMDetils>(QueryUpdate(QouteID)).ToList();
        gridBOM.DataSource = SuppliersReq;
        gridBOM.DataBind();
        if (ddlSupplier != null)
        {
            foreach (ProjectBOMDetils b in SuppliersReq)
            {
                ddlSupplier.SelectedValue = b.Supplier.ToString();
            }
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();
            var vendorsList = (from r in Vendors.v_Vendors

                               where r.VendorID == int.Parse(ddlSupplier.SelectedValue)
                               select r).ToList().FirstOrDefault();
            if (vendorsList != null)
            {
                txtEmail.Text = vendorsList.EmailAddress.Trim();
                txtReceiverName.Text = vendorsList.ContractorName;
            }
        }

    }
     private void BindGrid(int QouteID)
    {
          projectTaskDataContext BOM = new projectTaskDataContext();

          var SuppliersReq = BOM.ExecuteQuery<ProjectBOMDetils>(Query1(QouteID)).ToList();
          gridBOM.DataSource = SuppliersReq;
          gridBOM.DataBind();
          if (ddlSupplier != null)
          {
              foreach (ProjectBOMDetils b in SuppliersReq)
              {
                  ddlSupplier.SelectedValue = b.Supplier.ToString();
              }
              PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();
              var vendorsList = (from r in Vendors.v_Vendors

                                 where r.VendorID == int.Parse(ddlSupplier.SelectedValue)
                                 select r).ToList().FirstOrDefault();
              if (vendorsList != null)
              {
                  txtEmail.Text = vendorsList.EmailAddress.Trim();
                  txtReceiverName.Text = vendorsList.ContractorName;
              }
          }


     }
     private string QueryUpdate(int quoteID)
     {
         string sql = "";
         try
         {
             sql = "select P.ID as ID,P.ProjectReference as ProjectReference,P.WorkSheetID as WorkSheetID,isnull(CAST (V.Qty as float),0) as Qty," +
                  " P.PartNumber as PartNumber,P.Supplier,P.Description,isnull(P.Unit,0) Unit, isnull(P.Material,0) Material," +
                  " isnull(P.Labour,0)Labour,isnull(P.Mics,0)Mics,isnull(CAST (v.Qty as float),0)*(isnull(P.Material,0)+isnull(P.Labour,0)+isnull(P.Mics,0)) As Total," +
                  "  (SELECT  TypeName FROM BOM_Types where ProjectReference=P.ProjectReference and ID=P.WorkSheetID) as Worksheet " +
                  " from ProjectBOM P left join SupplierQUOTE_ITEMS V on P.ID=V.ItemNo  left join GoodsReceipt G on P.ID=G.BOMID  " +
                 " where V.QuoteID =" + quoteID.ToString() + "  and V.ProjectReference =" + QueryStringValues.Project;

             if (int.Parse(ddlSupplier.SelectedValue) != 0)
             {
                 sql += " and P.Supplier=" + ddlSupplier.SelectedValue;
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
         return sql;
     }

    private string Query1(int quoteID)
    {
        string sql = "";


        sql = "select P.ID as ID,P.ProjectReference as ProjectReference,P.WorkSheetID as WorkSheetID,isnull(CAST(V.Qty as float),0) as Qty," +
                  " P.PartNumber as PartNumber,P.Supplier,P.Description,isnull(P.Unit,0) Unit, isnull(P.Material,0) Material," +
                  " isnull(P.Labour,0)Labour,isnull(P.Mics,0)Mics,isnull(CAST (v.Qty as float),0)*(isnull(P.Material,0)+isnull(P.Labour,0)+isnull(P.Mics,0)) As Total," +
                  "  (SELECT  TypeName FROM BOM_Types where ProjectReference=P.ProjectReference and ID=P.WorkSheetID) as Worksheet " +
                  " from ProjectBOM P left join SupplierQUOTE_ITEMS V on P.ID=V.ItemNo  left join GoodsReceipt G on P.ID=G.BOMID  " +
                 " where V.QuoteID =" + quoteID.ToString() + "  and V.ProjectReference =" + QueryStringValues.Project ;
       
      
            
        

        if (int.Parse(ddlSupplier.SelectedValue) != 0)
        {
            sql += "  and P.Supplier=" + ddlSupplier.SelectedValue;
        }
        sql += "  order by P.ID";
        return sql;
    }
   

    #endregion
    protected string CalTotal(string Unit, string Qty)
    {
        double total = 0;
        try
        {
            if (Unit != "")
            {
                total = Convert.ToDouble(Unit) * Convert.ToDouble(Qty);
                
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return string.Format("{0:F2}", total);
       
    }

    private void ResetAll()
    {
        txtAddress.Text = "";
        txtDateRequired.Text = "";
        txtEmail.Text = "";
        txtNotes.Text = "";
        txtNotes1.Text = "";
        txtNotes2.Text = "";
        txtPONumber.Text = "";
        //ddlPonumber.SelectedIndex = 0;
       
    }
    private void BindDataDetails()
    {
        projectTaskDataContext workSheets = new projectTaskDataContext();

        var SupplierReq = (from r in workSheets.ProjectBOMSupRequisitions
                           where r.SupplierID == int.Parse(ddlSupplier.SelectedValue)
                           && r.ProjectRef == QueryStringValues.Project
                           select r).ToList().FirstOrDefault();

        PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

        var vendorsList = (from r in Vendors.v_Vendors

                         where r.VendorID==int.Parse(ddlSupplier.SelectedValue)
                          select r).ToList().FirstOrDefault();

        if (SupplierReq != null)
        {
            if (SupplierReq.DateRequired != null)
            {
                txtDateRequired.Text = string.Format("{0:d}", SupplierReq.DateRequired);

            }
            if (SupplierReq.Email != null)
            {
                //txtEmail.Text = SupplierReq.Email.ToString();

            }
            if (SupplierReq.Notes != null)
            {
                txtNotes.Text = SupplierReq.Notes;
            }
            if (SupplierReq.Notes1 != null)
            {
                txtNotes1.Text = SupplierReq.Notes1;

            }
            if (SupplierReq.Notes2 != null)
            {
                txtNotes2.Text = SupplierReq.Notes2;

            }
            if ((SupplierReq.PONumber != null) && (SupplierReq.PONumber != "Please select"))
            {
                txtPONumber.Text = SupplierReq.PONumber;

            }
            //if (SupplierReq.DeliveryAddress != null)
            //{
            //    if (SupplierReq.DeliveryAddress != "")
            //    {
            //        txtAddress.Text = SupplierReq.DeliveryAddress;
            //    }


            //}
            //if (SupplierReq.Email != null)
            //{
            //    if (SupplierReq.Email != "")
            //    {
            //        txtEmail.Text = SupplierReq.Email;
            //    }
            //    else
            //    {
            //        if (vendorsList != null)
            //        {
            //            txtEmail.Text = vendorsList.EmailAddress.Trim();
            //        }
            //    }

            //}
        }
        else
        {
            ResetAll();
        }
    }
    #endregion


    protected void imgSave_Click(object sender, EventArgs e)
    {
        projectTaskDataContext insert = new projectTaskDataContext();

       // projectTaskDataContext workSheets = new projectTaskDataContext();
        int ID = 0;
        var SupplierReq = (from r in insert.ProjectBOMSupRequisitions
                           where r.SupplierID == int.Parse(ddlSupplier.SelectedValue)
                           && r.ProjectRef == QueryStringValues.Project
                           select r).ToList();
        var rowID = (from r in insert.ProjectBOMSupRequisitions
                  where r.SupplierID == int.Parse(ddlSupplier.SelectedValue)
                  && r.ProjectRef == QueryStringValues.Project
                  select r).ToList().FirstOrDefault();
        if (rowID != null)
        {
            ID = rowID.ID;
        }

        if (SupplierReq.Count == 0 && ID==0)
        {

            ProjectBOMSupRequisition add = new ProjectBOMSupRequisition();
            add.DateRequired = Convert.ToDateTime(string.IsNullOrEmpty(txtDateRequired.Text) ?
                DateTime.Now.ToShortDateString() : txtDateRequired.Text);
            add.DeliveryAddress = txtAddress.Text;
            add.Email = txtEmail.Text;
            add.Notes = txtNotes.Text;
            add.Notes1 = txtNotes1.Text;
            add.Notes2 = txtNotes2.Text;
            add.PONumber = txtPONumber.Text;
            //add.PONumber = ddlPonumber.SelectedItem.Text;
            add.ProjectRef = QueryStringValues.Project;
            add.SupplierID = int.Parse(ddlSupplier.SelectedValue);
            insert.ProjectBOMSupRequisitions.InsertOnSubmit(add);

            insert.SubmitChanges();
        }
        else
        {
            ProjectBOMSupRequisition update =insert.ProjectBOMSupRequisitions.Single(P => P.ID == int.Parse(ID.ToString()));
            update.DateRequired = Convert.ToDateTime(string.IsNullOrEmpty(txtDateRequired.Text) ?
                DateTime.Now.ToShortDateString() : txtDateRequired.Text);
            update.DeliveryAddress = txtAddress.Text;
            update.Email = txtEmail.Text;
            update.Notes = txtNotes.Text;
            update.Notes1 = txtNotes1.Text;
            update.Notes2 = txtNotes2.Text;
            update.PONumber = txtPONumber.Text;
            //update.PONumber = ddlPonumber.SelectedItem.Text;
            update.ProjectRef = QueryStringValues.Project;
            update.SupplierID = int.Parse(ddlSupplier.SelectedValue);
            //insert.ProjectBOMSupRequisitions.InsertOnSubmit(update);

            insert.SubmitChanges();
        }
        Response.Redirect(string.Format("~/WF/Projects/ProjectBOM.aspx?project={0}", QueryStringValues.Project));
    }
    protected void imgSaveEmail_Click(object sender, EventArgs e)
    {
        try
        {
            projectTaskDataContext insert = new projectTaskDataContext();
            

            // projectTaskDataContext workSheets = new projectTaskDataContext();
            int ID = 0;
            var SupplierReq = (from r in insert.ProjectBOMSupRequisitions
                               where r.SupplierID == int.Parse(ddlSupplier.SelectedValue)
                               && r.ProjectRef == QueryStringValues.Project
                               select r).ToList();
            var rowID = (from r in insert.ProjectBOMSupRequisitions
                         where r.SupplierID == int.Parse(ddlSupplier.SelectedValue)
                         && r.ProjectRef == QueryStringValues.Project
                         select r).ToList().FirstOrDefault();
            if (rowID != null)
            {
                ID = rowID.ID;
            }

            if (SupplierReq.Count == 0 && ID == 0)
            {

                ProjectBOMSupRequisition add = new ProjectBOMSupRequisition();
                add.DateRequired = Convert.ToDateTime(string.IsNullOrEmpty(txtDateRequired.Text) ?
                    DateTime.Now.ToShortDateString() : txtDateRequired.Text);
                add.DeliveryAddress = txtAddress.Text;
                add.Email = "";// txtEmail.Text;
                add.Notes = txtNotes.Text;
                add.Notes1 = txtNotes1.Text;
                add.Notes2 = txtNotes2.Text;
                add.PONumber = txtPONumber.Text;
                //add.PONumber = ddlPonumber.SelectedItem.Text;
                add.ProjectRef = QueryStringValues.Project;
                add.SupplierID = int.Parse(ddlSupplier.SelectedValue);
                insert.ProjectBOMSupRequisitions.InsertOnSubmit(add);

                insert.SubmitChanges();
            }
            else
            {
                ProjectBOMSupRequisition update = insert.ProjectBOMSupRequisitions.Single(P => P.ID == int.Parse(ID.ToString()));
                update.DateRequired = Convert.ToDateTime(string.IsNullOrEmpty(txtDateRequired.Text) ?
                    DateTime.Now.ToShortDateString() : txtDateRequired.Text);
                update.DeliveryAddress = txtAddress.Text;
                update.Email = "";// txtEmail.Text;
                update.Notes = txtNotes.Text;
                update.Notes1 = txtNotes1.Text;
                update.Notes2 = txtNotes2.Text;
                update.PONumber = txtPONumber.Text;
                //update.PONumber = ddlPonumber.SelectedItem.Text;
                update.ProjectRef = QueryStringValues.Project;
                update.SupplierID = int.Parse(ddlSupplier.SelectedValue);
                //insert.ProjectBOMSupRequisitions.InsertOnSubmit(update);

                insert.SubmitChanges();
            }
            // For inserting PO Number into the Internal PO Section

            PurchaseOrderMgtDataContext PODetails = new PurchaseOrderMgtDataContext();
            if (txtPONumber.Text.ToString() != "")
            {
                var myPOGenInfo = (from r in PODetails.PO_GenInformations
                                   where r.PONumber == txtPONumber.Text
                                   select r).ToList();


                if (myPOGenInfo.Count > 0)
                {

                    PO_GenInformation POUpdate =
                               PODetails.PO_GenInformations.Single(P => P.PONumber == txtPONumber.Text);

                    PO_GenInformation insert1 = new PO_GenInformation();
                    insert1.ApprovedBy = sessionKeys.UID;
                    insert1.date = DateTime.Now;
                    insert1.Notes = txtNotes.Text;
                    insert1.ProjectRef = QueryStringValues.Project;
                    //PODetails.PO_GenInformations.InsertOnSubmit(insert);
                    PODetails.SubmitChanges();

                }
                else
                {
                    PO_GenInformation insert2 = new PO_GenInformation();
                    insert2.ApprovedBy = sessionKeys.UID;
                    insert2.date = DateTime.Now;
                    insert2.Notes = txtNotes.Text;
                    insert2.PONumber = txtPONumber.Text;
                    insert2.ProjectRef = QueryStringValues.Project;
                    insert2.PurchasedBy = sessionKeys.UID;
                    insert2.RequestedBy = sessionKeys.UID;
                    insert2.VendorID = int.Parse(ddlSupplier.SelectedValue);
                    PODetails.PO_GenInformations.InsertOnSubmit(insert2);
                    PODetails.SubmitChanges();
                }

            }


            lblError.Visible = false;
            lblMsg.Visible = true;

           //need to update the QtyOrderd in GoodsReceipt table

            foreach (GridViewRow row in gridBOM.Rows)
            {
                Label lblid = (Label)row.FindControl("lblID");
                if (lblid != null)
                {
                    Label lblQty = (Label)row.FindControl("lblQty");
                    if (lblQty != null)
                    {
                        var myGoodRecpts = (from r in insert.GoodsReceipts
                                           where r.BOMID == int.Parse(lblid.Text.ToString())
                         
                                           select r).ToList();
                        if (myGoodRecpts != null)
                        {
                            if (myGoodRecpts.Count() != 0)
                            {
                                ProjectMgt.Entity.ProjectBOM Update =
                              insert.ProjectBOMs.Single(P => P.ID == int.Parse(lblid.Text.ToString()));
                                ProjectMgt.Entity.GoodsReceipt GRUpdate =

                                insert.GoodsReceipts.Single(G => G.BOMID == int.Parse(lblid.Text.ToString()));
                                if (myGoodRecpts.Count() > 0 && (GRUpdate.QtyOrdered + Convert.ToInt32(double.Parse(string.IsNullOrEmpty(lblQty.Text.ToString()) ? "0" : lblQty.Text.ToString()))) <= Update.Qty)
                                {
                                    //double.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text)
                                    GRUpdate.QtyOrdered = GRUpdate.QtyOrdered + Convert.ToInt32(double.Parse(string.IsNullOrEmpty(lblQty.Text.ToString()) ? "0" : lblQty.Text.ToString()));
                                    GRUpdate.OutStandingQty = Convert.ToInt32(Update.Qty - GRUpdate.QtyReceived);
                                    insert.SubmitChanges();
                                }
                            }
                        }
                    }
                }
                
            }

           
            Mailer();
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //Response.Redirect(string.Format("~/ProjectBOM.aspx?project={0}", QueryStringValues.Project));
       //Response.Redirect(string.Format("~/ProjectBOMSupplierRequisitions.aspx?project={0}", QueryStringValues.Project));
        //BindReport();
    }
    protected void imgView_Click(object sender, EventArgs e)
    {

        lblMsg.Visible = false;
        BindDataDetails();
        BindGrid(int.Parse(Request.QueryString["QuoteID"]));
        


    }
    private void Mailer()
    {
        try
        {
            string RecieveraName, RecieverEmail, FromEmail, ManagerName, ManagerEmail, ProjectReference,instance, Contactno, OwnerMail,ownerNo;

            //ArrayList ToEmailIds = new ArrayList();
            DataTable dt;
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Quote_projectDetails", new SqlParameter("@ProjectReference", QueryStringValues.Project)).Tables[0];
            ProjectReference = dt.Rows[0]["ProjectReference"].ToString() + " [" + dt.Rows[0]["Projecttitle"].ToString() + "]";
            ManagerName = dt.Rows[0]["ManagerName"].ToString();
            ManagerEmail = dt.Rows[0]["ManagerEmail"].ToString().Trim();
            Contactno = dt.Rows[0]["Contactno"].ToString().Trim();
            FromEmail = dt.Rows[0]["FromEmail"].ToString().Trim();
            OwnerMail = dt.Rows[0]["OwnerMail"].ToString().Trim();
            ownerNo = dt.Rows[0]["OwnerNo"].ToString().Trim();
            instance=dt.Rows[0]["instance"].ToString().Trim();
            RecieveraName = txtReceiverName.Text.Trim();//ddlSupplier.SelectedItem.Text;
            RecieverEmail = txtEmail.Text.Trim();
            SupplierMail1.Visible = true;
            SupplierMail1.BindControls1(RecieveraName, ProjectReference, ManagerName, ManagerEmail, Contactno,instance,OwnerMail,ownerNo,ManagerName);
            
            ArrayList ToEmailIds = new ArrayList(0);
            ToEmailIds.Add(ManagerEmail);
            ToEmailIds.Add(txtEmail.Text.Trim());
            //string[] EmailIds = txtemail.Text.Trim().Split(';');
            //foreach (string EmailId in EmailIds)
            //{
            //    ToEmailIds.Add(EmailId);
            //}

            //ArrayList al = new ArrayList(0); ;
            string htmlText = string.Empty;
            StringWriter sw = new StringWriter();
            Html32TextWriter htmlTW = new Html32TextWriter(sw);
            SupplierMail1.RenderControl(htmlTW);
            htmlText = htmlTW.InnerWriter.ToString();
            string errorString = string.Empty;
            Email eMail = new Email();
            Attachment attachment1 = BindReport();
            Attachment attachment = GetAttachment();

              
               //Attachment attachment2 = null;
               //if (attachment1 == null)
               //{
               //    attachment2 = attachment;
               //}
               //else
               //{
               //    attachment2 = attachment1;
               //}
            //BindReport()
           // List<Attachment> Atts = (List<Attachment>)GetAttachment();

            
            if (!string.IsNullOrEmpty(RecieverEmail))
            {
                eMail.SendingMail(ToEmailIds, "Supplier Requisition For " + ProjectReference, htmlText, FromEmail, attachment1);
                lblMsg.Text = "Supplier requisition form has been sent successfully.";
                imgSaveEmail.Enabled = false;
                //eMail.SendingMail(ToEmailIds, "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                //eMail.SendingMail(txtemail.Text.Trim(), "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                //ProjectBOM.aspx?project=71
            }

            Stream st = attachment.ContentStream;
            SaveQuote(st);


            //attachment.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            SupplierMail1.Visible = false;
            Response.Redirect(string.Format("~/WF/Projects/ProjectBOM.aspx?project={0}",QueryStringValues.Project),false);
        }


        //string htmlText = string.Empty;
        //HtmlHijacker HTML = new HtmlHijacker();
        //htmlText = HTML.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/NewIncident.aspx?IncidentID=" + incident.ID, ref errorString);
        //Email eMail = new Email();

    }
    static void DoSomething2(IAsyncResult ar)
    {


    }
    private byte[] GetByteFromStream(Stream Str)
    {

        AsyncCallback askc = new AsyncCallback(DoSomething2);
        object OBJ = new object();
        int imgLen = (int)Str.Length;
        byte[] imgBinaryData = new byte[imgLen];
        new Random().NextBytes(imgBinaryData);
        Str.BeginRead(imgBinaryData, 0, imgLen, askc, OBJ);


        return imgBinaryData;// (br.ReadBytes(Convert.ToInt32(br.BaseStream.Length)));
    }
    private void SaveQuote(Stream stream)
    {
        string projectRef = QueryStringValues.Project.ToString();
        projectTaskDataContext project = new projectTaskDataContext();
        var projects = (from r in project.ProjectDetails
                        where r.ProjectReference == QueryStringValues.Project
                        select r).ToList().FirstOrDefault();
        if (projects != null)
        {
            projectRef = projects.ProjectReferenceWithPrefix;
        }
        byte[] Content = GetByteFromStream(stream);
        string ContentType = "application/pdf";
        int Length = Content.Length;
        string strFilename = string.Format("{0:ddMMyyyymm}", DateTime.Now) + "_" + projectRef + "_" + User.Identity.Name + ".pdf";

        QuoteProjectSupplierReqInsert(QueryStringValues.Project, strFilename, ContentType, Content, Length);
        stream.Dispose();
    }
    private void QuoteProjectSupplierReqInsert(int ProjectReference, string DocName, string ContentType, byte[] Document, int Size)
    {
        QuoteItemManager.QuoteProjectSupplierReqInsert(ProjectReference, DocName, ContentType, Document, Size);
    }
    private Attachment  BindReport()
    {
        Attachment at=null;
        try
        {
             rpt = new ReportDocument();
            string projectRef = QueryStringValues.Project.ToString();
            projectTaskDataContext project = new projectTaskDataContext();
            var projects = (from r in project.ProjectDetails
                            where r.ProjectReference == QueryStringValues.Project
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
            MyCommand.Parameters.AddWithValue("@SupplierID", int.Parse(ddlSupplier.SelectedValue));
            MyCommand.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
            SqlDataAdapter myAdapter = new SqlDataAdapter(MyCommand);
            myAdapter.Fill(dt);

            DataTable dt1 = new DataTable();
            SqlCommand MyCommand1 = new SqlCommand("ProjectBOM_SupplierRequistionGrid", MyCon);
            MyCommand1.CommandType = CommandType.StoredProcedure;
            MyCommand1.Parameters.AddWithValue("@SupplierID", int.Parse(ddlSupplier.SelectedValue));
            MyCommand1.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
            MyCommand1.Parameters.AddWithValue("@QuoteID",int.Parse(string.IsNullOrEmpty(Request.QueryString["QuoteID"])?"0":Request.QueryString["QuoteID"].ToString()));
            SqlDataAdapter myAdapter1 = new SqlDataAdapter(MyCommand1);
            myAdapter1.Fill(dt1);



            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);

            rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports[0].SetDataSource(dt1);

            string path = "~/WF/UploadData/Invoices/" + string.Format("{0:ddMMyyyymm}", DateTime.Now) + "_" + projectRef + "_" + User.Identity.Name + ".pdf";
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

    private Attachment GetAttachment()
    { 
        Attachment at=null;
        try
        {
         rpt = new ReportDocument();
        string projectRef = QueryStringValues.Project.ToString();
        projectTaskDataContext project = new projectTaskDataContext();
        var projects = (from r in project.ProjectDetails
                        where r.ProjectReference == QueryStringValues.Project
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
        MyCommand.Parameters.AddWithValue("@SupplierID", int.Parse(ddlSupplier.SelectedValue));
        MyCommand.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
        MyCommand.Parameters.AddWithValue("@QuoteID", int.Parse(string.IsNullOrEmpty(Request.QueryString["QuoteID"]) ? "0" : Request.QueryString["QuoteID"].ToString()));
        SqlDataAdapter myAdapter = new SqlDataAdapter(MyCommand);
        myAdapter.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlCommand MyCommand1 = new SqlCommand("ProjectBOM_SupplierRequistionGrid", MyCon);
        MyCommand1.CommandType = CommandType.StoredProcedure;
        MyCommand1.Parameters.AddWithValue("@SupplierID", int.Parse(ddlSupplier.SelectedValue));
        MyCommand1.Parameters.AddWithValue("@ProjectReference", QueryStringValues.Project);
        MyCommand1.Parameters.AddWithValue("@QuoteID",int.Parse(string.IsNullOrEmpty(Request.QueryString["QuoteID"])?"0":Request.QueryString["QuoteID"].ToString()));
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(MyCommand1);
        myAdapter1.Fill(dt1);



        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        string path = "~/WF/UploadData/Invoices/" + string.Format("{0:ddMMyyyymmss}", DateTime.Now.AddSeconds(4)) + "_" + projectRef + "_" + User.Identity.Name + ".pdf";
        //string path = "~//SupplierRequisitions//" +projectRef + "_" + User.Identity.Name + ".pdf";
        //Stream rptstream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
       
        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(path));

        at = new Attachment(Server.MapPath(path));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return at;
    }
    //private void Mailer()
    //{
    //    Email eMail = new Email();
    //    eMail.SendingMail(txtEmail.Text, "Supplier Requistion", "", BindReport());
    //}
    protected void gridBOM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBOM.EditIndex = -1;
        //BindGrid(int.Parse(ddlSupplier.SelectedValue));
        BindGrid(int.Parse(Request.QueryString["QuoteID"]));
    }
    protected void gridBOM_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        projectTaskDataContext InsertBOM = new projectTaskDataContext();
            if (e.CommandName == "Update")
            {
                lblError.Visible = false;
                int i = gridBOM.EditIndex;
                GridViewRow row = gridBOM.Rows[i];

                TextBox txtDescription = (TextBox)row.FindControl("txtDescription1");
               // TextBox txtPartNumber = (TextBox)row.FindControl("txtPartNumber");
                //DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtPartNumber1 = (TextBox)row.FindControl("txtPartNumber1");

                ProjectMgt.Entity.ProjectBOM Update =
              InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));

                //Update.Description = txtDescription.Text;
                //Update.PartNumber = txtPartNumber1.Text;
                //Update.Qty = Convert.ToDouble(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);
                //Update.Supplier = int.Parse(ddlSupplier.SelectedValue);
                //Update.Unit = txtUnit.Text;//Convert.ToDouble(string.IsNullOrEmpty(txtUnit.Text) ? "0" : txtUnit.Text);// Convert.ToDouble(txtUnit.Text);
                //InsertBOM.SubmitChanges();

                var myGoodRecpts = from r in InsertBOM.GoodsReceipts
                                   where r.BOMID == int.Parse(e.CommandArgument.ToString())
                                   select r;
                if (myGoodRecpts.Count() > 0)
                {
                    ProjectMgt.Entity.GoodsReceipt GRUpdate =

                        InsertBOM.GoodsReceipts.Single(G => G.BOMID == int.Parse(e.CommandArgument.ToString()));
                    int QtyOrd = 0;
                    double qttt1 = double.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);
                    QtyOrd = Convert.ToInt32(GRUpdate.QtyOrdered + Convert.ToInt32(qttt1));
                    int avail = Convert.ToInt32(Update.Qty - GRUpdate.QtyOrdered);
                   
                    if (QtyOrd > Update.Qty)
                    {
                        if (GRUpdate.QtyOrdered != 0)
                        {
                            lblError.Text = "Sorry but you have only specified " + Update.Qty + " in the Bill of Materials of which you have ordered " + GRUpdate.QtyOrdered + " previously";
                        }
                        else
                        {
                            lblError.Text = "Sorry but you have only specified " + Update.Qty + " in the Bill of Materials of which you have ordering " + QtyOrd+" now";
                        }
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        GRUpdate.QtyOrdered = GRUpdate.QtyOrdered + Convert.ToInt32(qttt1);
                        GRUpdate.OutStandingQty =Convert.ToInt32(Update.Qty - GRUpdate.QtyReceived);
                        //InsertBOM.SubmitChanges();
                        lblError.Text = "Updated Successfully";
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Green;
                        // here we need to update the supplier_Quote Items
                        int myqty =Convert.ToInt32(qttt1);

                        QuoteItemManager.SupplierReqQuoteItemUpdate(Convert.ToInt32(Request.QueryString["QuoteID"]), int.Parse(e.CommandArgument.ToString()), QueryStringValues.Project, Convert.ToInt32(qttt1), sessionKeys.UID);
                    }
                }
                else
                {
                    //Inserting GoodsRecipts table entry

                    GoodsReceipt GDR = new GoodsReceipt();
                    GDR.ExpectedShipmentDate =DateTime.Now;
                    GDR.NextShipmentDate = DateTime.Now;
                    double qttt = double.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text);

                    GDR.QtyOrdered = 0;// Convert.ToInt32(qttt);
                    GDR.QtyReceived = 0;
                    GDR.OutStandingQty =Convert.ToInt32(Update.Qty - GDR.QtyReceived);
                    GDR.BOMID = int.Parse(e.CommandArgument.ToString());
                    InsertBOM.GoodsReceipts.InsertOnSubmit(GDR);
                    InsertBOM.SubmitChanges();
                    lblError.Text = "Updated Successfully";
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Green;
                    QuoteItemManager.SupplierReqQuoteItemUpdate(Convert.ToInt32(Request.QueryString["QuoteID"]), int.Parse(e.CommandArgument.ToString()), QueryStringValues.Project, Convert.ToInt32(qttt), sessionKeys.UID);
                }


                
                gridBOM.EditIndex = -1;
                BindGridUpdate(int.Parse(Request.QueryString["QuoteID"]));
               
            

        }
            if (e.CommandName == "Delete")
            {
                //ProjectMgt.Entity.ProjectBOM pvi = InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                //InsertBOM.ProjectBOMs.DeleteOnSubmit(pvi);
                //InsertBOM.SubmitChanges();
                //ProjectMgt.Entity.SupplierQUOTE_ITEM pvi = InsertBOM.SupplierQUOTE_ITEMs.Single(P => P.ItemNo == int.Parse(e.CommandArgument.ToString()));
                //InsertBOM.SupplierQUOTE_ITEMs.DeleteOnSubmit(pvi);
                //InsertBOM.SubmitChanges();
                int obj = QuoteItemManager.SuplierRequisitionDelete(int.Parse(e.CommandArgument.ToString()));
                gridBOM.EditIndex = -1;
                BindGrid(int.Parse(Request.QueryString["QuoteID"]));
            }

    }
    protected void gridBOM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProjectBOMDetils de = (ProjectBOMDetils)e.Row.DataItem;
               
                //Label lblID = (Label)e.Row.FindControl("lblID");
                //if (int.Parse(lblID.Text) == -99)
                if(de.ID == -99)
                {
                    e.Row.Visible = false;
                }

                //Label lblTotal =(Label)e.Row.FindControl("lblTotal");

                //if (lblTotal != null)
                if(de.Total != null)
                {
                    Total =Convert.ToDouble(de.Total) + Total;// Convert.ToDouble(lblTotal.Text) + Total;
                } 
                //DropDownList ddlSupplier = (DropDownList)e.Row.FindControl("ddlSupplier");
                //if (ddlSupplier != null)
                //{
                //    BindVendors(ddlSupplier, de.Supplier.Value);
                //}
                Label lblUnit = (Label)e.Row.FindControl("lblUnit");
                if(lblUnit != null)
                {
                    lblUnit.Text = string.Format("{0:F2}", de.Material + de.Labour + de.Mics);
                }

                TextBox txtUnit = (TextBox)e.Row.FindControl("txtUnit");
                if (txtUnit != null)
                {
                    txtUnit.Text = string.Format("{0:F2}", de.Material + de.Labour + de.Mics);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                projectTaskDataContext projectDefault = new projectTaskDataContext();
                var projectDef = (from r in projectDefault.ProjectDefaults
                                  select r).ToList().FirstOrDefault();
                Label lblVatText = (Label)e.Row.FindControl("lblVatText");
                if (projectDef != null)
                {
                    lblVatText.Text = "VAT(" + projectDef.VAT+"%)";
                   // lblVatText.Text = "VAT Payble(" + projectDef.VAT + "%)";
                }
              
                    Label lblVat = (Label)e.Row.FindControl("lblVat");
                Label lblSum = (Label)e.Row.FindControl("lblSum");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
               
                lblTotal.Text = "";
                lblSum.Text = string.Format("{0:#.00}", Total); //Total.ToString();

                double vat = ((Convert.ToDouble(projectDef.VAT)) / 100) * Total;
                lblVat.Text=string.Format("{0:#.00}", vat);//vat.ToString()
                double sum = Total + vat;
                lblTotal.Text = string.Format("{0:#.00}", sum);
                vat = 0;
                sum = 0;
                Total = 0;
            }

            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridBOM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gridBOM.EditIndex = -1;
        if (Request.QueryString["QuoteID"] != null)
        {
            BindGrid(int.Parse(Request.QueryString["QuoteID"]));
        }
    }
    protected void gridBOM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBOM.EditIndex = e.NewEditIndex;
        if (Request.QueryString["QuoteID"] != null)
        {
            BindGrid(int.Parse(Request.QueryString["QuoteID"]));
        }
    }
    protected void gridBOM_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridBOM.EditIndex = -1;
        if (Request.QueryString["QuoteID"] != null)
        {
            BindGridUpdate(int.Parse(Request.QueryString["QuoteID"]));
        }
    }
    protected void ddlSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        string address = "";
        lblMsg.Visible = false;
        try
        {

            Location.DAL.LocationDataContext location = new Location.DAL.LocationDataContext();
            //Location.DAL.LocationDataContext location = new Location.DAL.LocationDataContext();

            var getAdrress = (from r in location.Sites
                              where r.ID == int.Parse(ddlSites.SelectedValue)
                              select r).ToList().FirstOrDefault();
            if (ddlSites.SelectedValue != "")
            {
                if (getAdrress != null)
                {
                    address =getAdrress.Site1+" "+ getAdrress.Address1 + " " + getAdrress.Address2 + " " + getAdrress.Address3 + " "
                        + getAdrress.Postcode;

                }
            }


            txtAddress.Text = address;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();
            BindDataDetails();
            if (Request.QueryString["QuoteID"] != null)
            {
               BindGrid(int.Parse(Request.QueryString["QuoteID"]));
            }
            var vendorsList = (from r in Vendors.v_Vendors

                               where r.VendorID == int.Parse(ddlSupplier.SelectedValue)
                               select r).ToList().FirstOrDefault();
            if (vendorsList != null)
            {
                txtEmail.Text = vendorsList.EmailAddress.Trim();
                txtReceiverName.Text = vendorsList.ContractorName;
            }
            
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
