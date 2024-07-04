using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using POMgt.DAL;
using POMgt.Entity;
using UserMgt.DAL;

public partial class BoMSupplierPayments : System.Web.UI.Page
{
    ReportDocument rpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                AllVendorNames();
                //Master.PageHead = "Supplier Invoices";
                lblMessage.Visible = false;
                setProjectPrefix();
                BindProjects();
                Bind_worksheet(int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue));
                //SetWorksheet();
                int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
                BindVendor();
                BindGridInCriditNote();
                lblProject.Text = "<b>Project:</b> " + ddlProjects.SelectedItem.Text;
                if (int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue) != 0)
                {
                    lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(ddlProjects.SelectedValue) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                }
                if (int.Parse(ddlWorkSheet.SelectedValue) == 0)
                {
                    lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(ddlProjects.SelectedValue) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //if (ddlProjects.SelectedValue != "0")
        //{
        //    btnCreditNote.Enabled = true;
        //}
        //else
        //{
        //    btnCreditNote.Enabled = false;
        //}
    }
    #region Bind Data
    private void setProjectPrefix()
    {
        txtPrefix.Text = sessionKeys.Prefix;
    }
    private void Bind_worksheet(int ProjectRef)
    {
        try
        {
            ddlWorkSheet.DataSource = Deffinity.Worksheet.Worksheet_SelectAll(ProjectRef); //Deffinity.Worksheet.Worksheet_SelectAll(int.Parse(ddlProjects.SelectedValue));
            ddlWorkSheet.DataTextField = "TypeName";
            ddlWorkSheet.DataValueField = "ID";
            ddlWorkSheet.DataBind();
            ddlWorkSheet.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void AllVendorNames()
    {
        try
        {
            PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();
            List<VerdorNamesClass> v = (from r in Vendors.v_Vendors
                     select new VerdorNamesClass
                     {
                         VendorID = r.VendorID,
                         ContractorName = r.ContractorName
                     }).ToList();
            ViewState["Names"] = v.ToList();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string VendorName(string id)
    {
        string name = string.Empty;
        List<VerdorNamesClass> v = new List<VerdorNamesClass>();
        try
        {
            if (!string.IsNullOrEmpty(id.Trim()) && id.Trim() != "0")
            {
               
                    if (ViewState["Names"] != null)
                    {
                        v = (List<VerdorNamesClass>)ViewState["Names"];
                        name = v.Where(a => a.VendorID == int.Parse(id)).FirstOrDefault().ContractorName;
                    }
                    else
                    {
                        AllVendorNames();
                        v = (List<VerdorNamesClass>)ViewState["Names"];
                        name = v.Where(a => a.VendorID == int.Parse(id)).FirstOrDefault().ContractorName;
                    }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException("BOMSupplierpayments.aspx - " + "VendorName " + "id:"+id);
        }
        return name;
    }
    private void SetWorksheet()
    {
        try
        {
            if (ddlWorkSheet.Items.Count > 1)
            {
                //set the starting value
                ddlWorkSheet.SelectedIndex = 1;
                // HD_Type.Value = ddlWorksheet.SelectedValue;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProjects()
    {
        projectTaskDataContext portFolio = new projectTaskDataContext();
      

        DataSet ds = new DataSet();
        try
        {


           var GetAllProjectRef = (from r in portFolio.ProjectDetails
                                //where r.Portfolio == Portfolio
                                   where r.ProjectTitle != "" && r.ProjectStatusID != 4 && r.ProjectStatusID != 5
                                orderby r.ProjectReference
                                select new { name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle, value = r.ProjectReference.ToString() }).ToList();
           ddlProjects.DataSource = GetAllProjectRef;
           ddlProjects.DataTextField = "name";
           ddlProjects.DataValueField = "value";
           ddlProjects.DataBind();
           ddlProjects.Items.Insert(0, new ListItem("Please select...", "0"));
           if (ddlProjects.Items.Count > 1)
           {
               //set the starting value
               ddlProjects.SelectedIndex = 1;
               // HD_Type.Value = ddlWorksheet.SelectedValue;

           }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void BindVendor()
    {
        try
        {
            List<VerdorNamesClass> v = new List<VerdorNamesClass>();
            if (ViewState["Names"] != null)
            {
                v = (List<VerdorNamesClass>)ViewState["Names"];
            }
            else
            {
                AllVendorNames();
                v = (List<VerdorNamesClass>)ViewState["Names"];
            }
            //PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();
            //var vendorsList = from r in Vendors.v_Vendors
            //                  orderby r.ContractorName
            //                  select r;
            ddlVendor.DataSource = v.OrderBy(a => a.ContractorName);
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "ContractorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));


           


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindVendorInCreditNote()
    {
        try
        {
            List<VerdorNamesClass> v = new List<VerdorNamesClass>();
            if (ViewState["Names"] != null)
            {
                v = (List<VerdorNamesClass>)ViewState["Names"];
            }
            else
            {
                AllVendorNames();
                v = (List<VerdorNamesClass>)ViewState["Names"];
            }

            projectTaskDataContext Pdc = new projectTaskDataContext();

            var projectref = 0;
            if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
            {
                projectref = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
            }
            else if (int.Parse(ddlProjects.SelectedValue) > 0)
            {
                projectref = int.Parse(ddlProjects.SelectedValue);
            }

            var BomVendorIds = Pdc.ProjectBOMs.Where(a => a.ProjectReference == projectref).ToList();

            v = (from a in v join b in BomVendorIds on a.VendorID equals b.Supplier select a).Distinct().ToList();

            ddlVendorsIncredit.DataSource = v.OrderBy(a => a.ContractorName);
            ddlVendorsIncredit.DataValueField = "VendorID";
            ddlVendorsIncredit.DataTextField = "ContractorName";
            ddlVendorsIncredit.DataBind();
            ddlVendorsIncredit.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGrid( int project,int ddlproject,int workSheetID,int VendorId)
    {
        try
        {
            projectTaskDataContext goods = new projectTaskDataContext();

            var goodsList =goods.ExecuteQuery<ProjectBOMDetils>(Query(project,ddlproject,workSheetID,VendorId));

            //var goodsList = (from p in goods.ProjectBOMDetils
                            
            //                 join g in goods.GoodsReceipts on p.ID equals g.BOMID
            //                  into e1_et
            //                 from e2 in e1_et.DefaultIfEmpty()
            //                 where p.WorkSheetID == workSheetID &&
            //                  p.ProjectReference == project
            //                 select new
            //                 {
            //                     p.Description,
            //                     p.PartNumber,
            //                     p.ID,
            //                     QtyOrdered = p.Qty,
            //                     e2.BOMID,
            //                      e2.InvoiceNumber,
            //                     e2.QtyReceived,
            //                     e2.ExpectedShipmentDate,
            //                     e2.NextShipmentDate,
            //                     e2.Notes,
            //                     p.Total                              

            //                 }).ToList();


            gridGoodReceipt.DataSource = goodsList.ToList();
            gridGoodReceipt.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region "Query"

    private string Query(int ProjectRef, int ddlprojectRef, int worksheet,int VenderID)
    {
        string sql = "";
        try
        {

            sql = "select Total,ID,NextShipmentDate,ExpectedShipmentDate,Notes,Description,PartNumber,Qty,QtyReceived,Supplier from v_ProjectBOM  where ID<>-99 and IsDeleted=0 ";
            if (ProjectRef != 0)
            {
                sql += "  and ProjectReference=" + ProjectRef.ToString();
            }
            if (ddlprojectRef != 0)
            {
                sql += "  and ProjectReference=" + ddlprojectRef.ToString();
            }
            if (worksheet != 0)
            {
                sql += "  and WorkSheetID=" + worksheet.ToString();
            }
            if (VenderID != 0)
            {
                sql += "  and Supplier=" + VenderID.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
         return sql;
    }
 


    #endregion
    protected string BlankDate(string ID)
    {
        string blank = "";
        try
        {
            projectTaskDataContext goods = new projectTaskDataContext();
            var getDate = (from r in goods.ProjectBOMDetils
                           where r.ID == int.Parse(ID)
                           select r).ToList().FirstOrDefault();
          
            if (getDate != null)
            {
                if (Convert.ToDateTime(getDate.NextShipmentDate) == Convert.ToDateTime("01/01/1900"))
                {
                    blank = "";

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
        return blank;
    }
    protected string TotalInvoiced(string ID)
    {
        string totalVal = "";
        try
        {
            projectTaskDataContext goods = new projectTaskDataContext();
           
            var totalVal1 = (from p in goods.GoodsRecieptBOMs
                             where p.BOMID == int.Parse(ID)
                             group p by p.BOMID into g
                             select new { TotalSum = g.Sum(p => p.Amount) }).ToList().FirstOrDefault();

            if (totalVal1 != null)
            {
                totalVal = string.Format("{0:f2}", totalVal1.TotalSum);

            }
            else
            {
                totalVal = "0.00";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      return totalVal;
    }
    protected string TotalOutstanding(string ID)
    {
        double total = 0;
        try
        {
           
            projectTaskDataContext goods = new projectTaskDataContext();
            var goodsList = (from p in goods.ProjectBOMDetils

                             join g in goods.GoodsReceipts on p.ID equals g.BOMID
                              into e1_et
                             from e2 in e1_et.DefaultIfEmpty()
                             where p.ID == int.Parse(ID)
                             select new
                             {
                                 p.Description,
                                 p.PartNumber,
                                 p.ID,
                                 QtyOrdered = p.Qty,
                                 e2.BOMID,
                                 e2.InvoiceNumber,
                                 e2.QtyReceived,
                                 e2.ExpectedShipmentDate,
                                 e2.NextShipmentDate,
                                 e2.Notes,
                                 p.Total

                             }).ToList().FirstOrDefault();
            string totalVal = "";
            var totalVal1 = (from p in goods.GoodsRecieptBOMs
                             where p.BOMID == int.Parse(ID)
                             group p by p.BOMID into g
                             select new { TotalSum = g.Sum(p => p.Amount) }).ToList().FirstOrDefault();

            if (totalVal1 != null)
            {
                totalVal = string.Format("{0:f2}", totalVal1.TotalSum);

            }
            else
            {
                totalVal = "0.00";
            }
            if (goodsList != null)
            {
                total = Convert.ToDouble(goodsList.Total) - Convert.ToDouble(totalVal);
            }
            else
            {
                total = 0;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return string.Format("{0:f2}", total);
    }

    private void BindPOPGrid(int ID)
    {
        try
        {
            projectTaskDataContext goods = new projectTaskDataContext();


            var goodsList = (from p in goods.GoodsReceiptBOMDetails

                             where p.BOMID == ID || p.ID == -99
                             select p).ToList();
                            

            gridPOP.DataSource = goodsList;
            gridPOP.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindMouseHoverGrid(GridView grdView,int ID)
    {
        try
        {
            projectTaskDataContext goods = new projectTaskDataContext();


            var goodsList = (from p in goods.GoodsReceiptBOMDetails

                             where p.BOMID == ID 
                             select p).ToList();


            grdView.DataSource = goodsList;
            grdView.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #endregion
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
           //int.Parse(.SelectedValue);
            if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
            {
                Bind_worksheet(int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text));
                //ddlProjects.SelectedIndex = 0;

                if (ddlProjects.Items.FindByValue(txtProjectRef.Text) != null)
                {
                    ddlProjects.SelectedValue = txtProjectRef.Text;
                }
                else
                {
                    ddlProjects.SelectedValue = "0";
                }
            }
            else
            {
                Bind_worksheet(int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue));
                txtProjectRef.Text = "0";
                ddlProjects.SelectedValue = "0";
            }
            int workID = int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue);
            ddlWorkSheet.SelectedValue = workID.ToString();
               // SetWorksheet();
            int projectRef=int.Parse(string.IsNullOrEmpty(txtProjectRef.Text)?"0":txtProjectRef.Text);
            BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
            BindGridInCriditNote();
            lblProject.Text = "<b>Project:</b> " + ddlProjects.SelectedItem.Text;
            if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
            {
                lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(txtProjectRef.Text) + "&wrkshtId=" + int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue);
            }
            if (int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue) != 0)
            {
                lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(txtProjectRef.Text) + "&wrkshtId=" + int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue);
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridGoodReceipt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {





    }
    protected void gridGoodReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                BindPOPGrid(int.Parse(e.CommandArgument.ToString()));
                hdnID.Value = e.CommandArgument.ToString();
                projectTaskDataContext goodsReceipt = new projectTaskDataContext();

                var journalsData = (from r in goodsReceipt.ProjectBOMs
                                    where r.ID == int.Parse(e.CommandArgument.ToString()) 
                                    select r).ToList().FirstOrDefault();
                if (journalsData != null)
                {
                    lblTaskName.Text = journalsData.Description;
                }
                mpopBOM.Show();
                lblErr.Visible = false;
            }

            if (e.CommandName == "Update")
            {
                lblMessage.Visible = false;
                projectTaskDataContext project = new projectTaskDataContext();
                int CountRow = gridGoodReceipt.Rows.Count;
                for (int i = 0; i < CountRow; i++)
                {

                    GridViewRow Row = gridGoodReceipt.Rows[i];
                    TextBox txtDateReceived = (TextBox)Row.FindControl("txtDateReceived");
                    TextBox txtQtyOrdered = (TextBox)Row.FindControl("txtQtyOrdered");
                    TextBox txtQtyReceived = (TextBox)Row.FindControl("txtQtyReceived");
                    TextBox txtNextExpectedDate = (TextBox)Row.FindControl("txtNextExpectedDate");
                    TextBox Comments = (TextBox)Row.FindControl("txtComments");
                      TextBox txtInvoiceNumber = (TextBox)Row.FindControl("txtInvoiceNumber");
                    Label lblID = (Label)Row.FindControl("lblID");
                    ProjectMgt.Entity.GoodsReceipt gr = new ProjectMgt.Entity.GoodsReceipt();
                    var IsExist = (from r in project.GoodsReceipts
                                   where r.BOMID == int.Parse(lblID.Text)
                                   select r).ToList();
                    if (IsExist != null)
                    {
                        if (IsExist.Count == 0)
                        {
                            if (int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text) >= int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text))
                            {

                               
                                projectTaskDataContext goodsReceipt = new projectTaskDataContext();

                                var journalsData = (from r in goodsReceipt.GoodsReceiptJournals
                                                    where r.BOMID == int.Parse(lblID.Text) && r.InvoiceNumber == txtInvoiceNumber.Text.Trim()
                                                    select r).ToList();
                                double remain = 0;
                                var getRemainedQty = (from r in goodsReceipt.GoodsReceipts
                                                      where r.BOMID == int.Parse(lblID.Text)
                                                      select r).ToList().FirstOrDefault();
                                if (getRemainedQty != null)
                                {
                                    remain = getRemainedQty.OutStandingQty.Value;
                                    // QtyRec = getRemainedQty.QtyReceived.Value;
                                }

                                if (journalsData != null)
                                {
                                    if (journalsData.Count == 0 )
                                    { 



                                ProjectMgt.Entity.ProjectBOM Update =
                                 project.ProjectBOMs.Single(P => P.ID == int.Parse(lblID.Text));
                                Update.Qty = int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                project.SubmitChanges();

                                gr.BOMID = int.Parse(lblID.Text);
                                gr.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                    DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                gr.NextShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextExpectedDate.Text) ?
                                    DateTime.Now.ToShortDateString() : txtNextExpectedDate.Text);
                                gr.QtyOrdered = int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                gr.QtyReceived = int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text);
                                gr.Notes = Comments.Text;
                                gr.OutStandingQty = gr.QtyOrdered - gr.QtyReceived;
                                gr.InvoiceNumber = txtInvoiceNumber.Text.Trim();
                                project.GoodsReceipts.InsertOnSubmit(gr);
                                project.SubmitChanges();
                                if (txtInvoiceNumber.Text != "")
                                {
                                //    AddToGoodsReceiptJouranl(int.Parse(lblID.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                //        DateTime.Now.ToShortDateString() : txtDateReceived.Text), int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text),
                                //        int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text),
                                //      remain,
                                //        txtInvoiceNumber.Text);
                                }
                                    }
                                }


                            }
                            else
                            {
                                lblMessage.Text = "Please enter Ordered Quantity more then Received";
                                lblMessage.Visible = true;
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                        else
                        {
                            if (int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text) >= int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text))
                            {

                                

                                 projectTaskDataContext goodsReceipt = new projectTaskDataContext();

                                var journalsData = (from r in goodsReceipt.GoodsReceiptJournals
                                                    where r.BOMID == int.Parse(lblID.Text) && r.InvoiceNumber == txtInvoiceNumber.Text.Trim()
                                                    select r).ToList();
                                double remain = 0;
                                var getRemainedQty = (from r in goodsReceipt.GoodsReceipts
                                                      where r.BOMID == int.Parse(lblID.Text)
                                                      select r).ToList().FirstOrDefault();
                                if (getRemainedQty != null)
                                {
                                    remain = getRemainedQty.OutStandingQty.Value;
                                    // QtyRec = getRemainedQty.QtyReceived.Value;
                                }
                                if (journalsData != null)
                                {
                                    if (journalsData.Count == 0)
                                    {
                                        ProjectMgt.Entity.ProjectBOM UpdatePO =
                                        project.ProjectBOMs.Single(P => P.ID == int.Parse(lblID.Text));
                                        UpdatePO.Qty = int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                        project.SubmitChanges();



                                        ProjectMgt.Entity.GoodsReceipt Update =
                                         project.GoodsReceipts.Single(P => P.BOMID == int.Parse(lblID.Text));

                                        Update.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                             DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                        Update.NextShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextExpectedDate.Text) ?
                                            DateTime.Now.ToShortDateString() : txtNextExpectedDate.Text);
                                        Update.QtyOrdered = int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                        Update.QtyReceived = int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text);
                                        Update.Notes = Comments.Text;
                                        Update.InvoiceNumber = txtInvoiceNumber.Text.Trim();
                                        Update.OutStandingQty = Update.QtyOrdered - Update.QtyReceived;
                                        project.SubmitChanges();

                                        if (txtInvoiceNumber.Text != "")
                                        {
                                            //AddToGoodsReceiptJouranl(int.Parse(lblID.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                            //    DateTime.Now.ToShortDateString() : txtDateReceived.Text), int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text),
                                            //    int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text),
                                            //    int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text) -
                                            //    int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text),
                                            //    txtInvoiceNumber.Text);

                                            //AddToGoodsReceiptJouranl(int.Parse(lblID.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                            //   DateTime.Now.ToShortDateString() : txtDateReceived.Text), int.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text),
                                            //   int.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text),
                                            //   remain,
                                            //   txtInvoiceNumber.Text);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Please enter Ordered Quantity more then Received";
                                lblMessage.Visible = true;
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                    }


                }
                //BindGrid(int.Parse(ddlWorkSheet.SelectedValue), int.Parse(ddlSupplier.SelectedValue));
                //BindGrid(int.Parse(ddlWorkSheet.SelectedValue), int.Parse(ddlProjects.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AddToGoodsReceiptJouranl(int BOMID,DateTime dateReceived,double QtyOrd,double QtyRec,double QtyRemain,
        string InvoiceNum)
    {
        projectTaskDataContext goodsReceipt = new projectTaskDataContext();
        double QtyRemaindGR = 0;
        double QtyRecGR = 0;
        var journalsData = (from r in goodsReceipt.GoodsReceiptJournals
                            where r.BOMID == BOMID && r.InvoiceNumber == InvoiceNum
                            select r).ToList();

        var getRemainedQty = (from r in goodsReceipt.GoodsReceipts
                              where r.BOMID == BOMID
                              select r).ToList().FirstOrDefault();
        if (getRemainedQty != null)
        {
            QtyRemaindGR = getRemainedQty.OutStandingQty.Value;
           // QtyRec = getRemainedQty.QtyReceived.Value;
        }


        if (journalsData != null)
        {
            if (journalsData.Count == 0 && QtyRemain > 0)
            {
                GoodsReceiptJournal insert = new GoodsReceiptJournal();
                insert.InvoiceNumber = InvoiceNum;
                insert.QtyOrdered = QtyOrd;
                insert.QtyRec = QtyRec;
                insert.QtyRemain = QtyRemain-QtyRec;
                insert.DateRecevied = dateReceived;
                insert.BOMID = BOMID;

                goodsReceipt.GoodsReceiptJournals.InsertOnSubmit(insert);
                goodsReceipt.SubmitChanges();
            }
        }


    }

    
   
    protected void gridGoodReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //grdViewBOM
                GridView grdViewBOM = (GridView)e.Row.FindControl("grdViewBOM");
                Label lblIDHover = (Label)e.Row.FindControl("lblIDHover");
                BindMouseHoverGrid(grdViewBOM, int.Parse(lblIDHover.Text));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (int.Parse(txtProjectRef.Text) != 0)
            //{ Bind_worksheet(int.Parse(txtProjectRef.Text)); }
            //else
            //{
            //    Bind_worksheet(int.Parse(ddlProjects.SelectedValue));
            //}
            chkWorksheet.Checked = false;
            Bind_worksheet(int.Parse(ddlProjects.SelectedValue));
            SetWorksheet();
            txtProjectRef.Text = "";
            int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
           // BindGrid(projectRef,int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
            BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
            BindGridInCriditNote();
            if (int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue) != 0)
            {
                lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(ddlProjects.SelectedValue) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
            }

            lblProject.Text = "<b>Project:</b> " + ddlProjects.SelectedItem.Text;
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPOP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        projectTaskDataContext BOM = new projectTaskDataContext();
        try
        {
           
            if (e.CommandName == "Add")
            {

                TextBox txtNextDatef = (TextBox)gridPOP.FooterRow.FindControl("txtNextDatef");
                TextBox txtInvoiceNumber = (TextBox)gridPOP.FooterRow.FindControl("txtInvoiceNumberf");
                TextBox txtAmountrf = (TextBox)gridPOP.FooterRow.FindControl("txtAmountrf");

                var getEx = (from r in BOM.GoodsRecieptBOMs
                             where r.BOMID == int.Parse(hdnID.Value) &&
                             r.InvoiceNumber == (txtInvoiceNumber.Text.Trim())
                             select r).ToList();
                if (getEx != null)
                {
                    if (getEx.Count == 0)
                    {
                        GoodsRecieptBOM gd = new GoodsRecieptBOM();
                        if (txtInvoiceNumber.Text != "")
                        {
                            gd.BOMID = int.Parse(hdnID.Value);
                            gd.Amount = Convert.ToDouble(string.IsNullOrEmpty(txtAmountrf.Text) ? "0" : txtAmountrf.Text);
                            gd.InvoiceNumber = txtInvoiceNumber.Text;
                            gd.NextDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextDatef.Text) ? DateTime.Now.ToShortDateString() : txtNextDatef.Text);
                            BOM.GoodsRecieptBOMs.InsertOnSubmit(gd);
                            BOM.SubmitChanges();
                            int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                            //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                            BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
                            BindPOPGrid(int.Parse(hdnID.Value));

                        }

                        else
                        {
                            lblErrPop.Visible = true;
                            lblErrPop.Text = "Please enter Invoice Number";
                        }
                    }
                    else
                    {
                        lblErrPop.Visible = true;
                        lblErrPop.Text = "Invoice already exist";
                    }
                }

            }
        }
         catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        try
        {
            if (e.CommandName == "Update")
            {
                int i = gridPOP.EditIndex;
                GridViewRow row = gridPOP.Rows[i];
                TextBox txtNextDate = (TextBox)row.FindControl("txtNextDate");
                TextBox txtInvoiceNumbe = (TextBox)row.FindControl("txtInvoiceNumber");
                TextBox txtAmountr = (TextBox)row.FindControl("txtAmountr");
                var getEx = (from r in BOM.GoodsRecieptBOMs
                             where r.BOMID == int.Parse(hdnID.Value)
                             select r).ToList();
                if (getEx != null)
                {
                    if (getEx.Count > 0)
                    {
                        if (txtInvoiceNumbe.Text != "")
                        {
                            var mygetEx = (from r in BOM.GoodsRecieptBOMs
                                           where r.InvoiceNumber == (txtInvoiceNumbe.Text.Trim())
                                           select r).ToList();
                            //if (mygetEx.Count > 0)
                            //{

                            //    lblErrPop.Visible = true;
                            //    lblErrPop.Text = "Invoice already exist";
                            //}
                            //else
                            //{
                            GoodsRecieptBOM update =
                             BOM.GoodsRecieptBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                            update.InvoiceNumber = txtInvoiceNumbe.Text.ToString();
                            update.NextDate = Convert.ToDateTime(txtNextDate.Text);
                            update.Amount = Convert.ToDouble(txtAmountr.Text);
                            BOM.SubmitChanges();
                            //}
                        }
                        else
                        {
                            lblErrPop.Visible = true;
                            lblErrPop.Text = "Please enter Invoice number";
                        }

                    }
                    else
                    {


                    }
                }







                int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue),int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue)?"0":ddlVendor.SelectedValue));
            }
        }
         catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        try
        {
            if (e.CommandName == "Delete")
            {

                int i = gridPOP.EditIndex;
                GoodsRecieptBOM update =
                BOM.GoodsRecieptBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                BOM.GoodsRecieptBOMs.DeleteOnSubmit(update);
                BOM.SubmitChanges();
                int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
            }
            mpopBOM.Show();
        }
         
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPOP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GoodsReceiptBOMDetails de = (GoodsReceiptBOMDetails)e.Row.DataItem;
                if (de.ID == -99)
                {
                    e.Row.Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPOP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
         try
        {
        lblErr.Visible = false;
        gridPOP.EditIndex = -1;
        BindPOPGrid(int.Parse(hdnID.Value));
        }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
    }
    protected void gridPOP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
        gridPOP.EditIndex = -1;
        BindPOPGrid(int.Parse(hdnID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPOP_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridPOP.EditIndex = e.NewEditIndex;
            BindPOPGrid(int.Parse(hdnID.Value));
        }
         
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridPOP_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
        lblErr.Visible = false;
        gridPOP.EditIndex = -1;
        BindPOPGrid(int.Parse(hdnID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    private void  BindReport(int ProjectRef)
    {

        try
        {
            //linkRpt.NavigateUrl = "~/Reports/ProjectBOMReport.aspx?id=" + HD_Type.Value + "&project=" + QueryStringValues.Project;


            string str = "~/WF/Reports/ProjectBOMSRInv.rpt";
            rpt.Load(Server.MapPath(str));

            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            SqlConnection MyCon = new SqlConnection(strConn);
            SqlCommand MyCommand = new SqlCommand("ProjectBOMReport_Mainrpt", MyCon);
            MyCommand.CommandType = CommandType.StoredProcedure;

            MyCommand.Parameters.AddWithValue("@ProjectReference", ProjectRef);
            SqlDataAdapter myAdapter = new SqlDataAdapter(MyCommand);
            myAdapter.Fill(dt);

            DataTable dt1 = new DataTable();
            SqlCommand MyCommand1 = new SqlCommand("ProjectBOMReport_Subrpt", MyCon);
            MyCommand1.CommandType = CommandType.StoredProcedure;

            MyCommand1.Parameters.AddWithValue("@ProjectReference", ProjectRef);
            SqlDataAdapter myAdapter1 = new SqlDataAdapter(MyCommand1);
            myAdapter1.Fill(dt1);



            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);

            rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports[0].SetDataSource(dt1);
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Supplier Invoice Report");
        }
         
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    protected void chkWorksheet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkWorksheet.Checked == true)
            {
                if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
                {
                    ddlProjects.SelectedValue = "0";
                    Bind_worksheet(int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text));
                    SetWorksheet();
                    int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                    //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                    BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
                    if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
                    {
                        lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(txtProjectRef.Text) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                           
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlWorkSheet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                      
                lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(ddlProjects.SelectedValue) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                //txtProjectRef.Text = "";
                int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
                if (projectRef != 0)
                {
                    lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + projectRef + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                }
                else
                {
                    lnkRpt.NavigateUrl = "~/WF/Reports/ProjectBOMSupplierInv.aspx?project=" + int.Parse(ddlProjects.SelectedValue) + "&wrkshtId=" + int.Parse(ddlWorkSheet.SelectedValue);
                }
            

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgApply_Click(object sender, EventArgs e)
    {
        try
        {
            
            using (projectTaskDataContext BOM = new projectTaskDataContext())
            {
                 bool checkboxChecked = false;
                for (int i = 0; i < gridGoodReceipt.Rows.Count; i++)
                {
                    GridViewRow row = gridGoodReceipt.Rows[i];
                    bool ischecked = ((CheckBox)row.FindControl("chkRecords")).Checked;
                    
                    
                   
                    if (ischecked)
                    {
                        int id = Convert.ToInt32(((HiddenField)row.FindControl("hfId")).Value);
                        double bomTotal  = Convert.ToDouble(((Label)row.FindControl("lblTotal")).Text);

                        checkboxChecked = true;
                        var getEx = (from r in BOM.GoodsRecieptBOMs
                                     where r.BOMID == id &&
                                     r.InvoiceNumber == (txtApplyInvoiceNumber.Text.Trim())
                                     select r).ToList();
                        if (getEx != null)
                        {
                            if (getEx.Count == 0)
                            {
                                GoodsRecieptBOM gd = new GoodsRecieptBOM();
                                if (txtApplyInvoiceNumber.Text != "")
                                {
                                    gd.BOMID = id;
                                    gd.Amount = bomTotal;
                                    gd.InvoiceNumber = txtApplyInvoiceNumber.Text;
                                    gd.NextDate = Convert.ToDateTime(string.IsNullOrEmpty(txtApplyInvoiceDate.Text) ? DateTime.Now.ToShortDateString() : txtApplyInvoiceDate.Text);
                                    BOM.GoodsRecieptBOMs.InsertOnSubmit(gd);
                                    BOM.SubmitChanges();

                                    //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));



                                }

                            }
                            
                        }
                    }
                }
                if (!checkboxChecked)
                {
                    lblApplyMsg.Text = "Please select record(s) to apply";
                    lblApplyMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblApplyMsg.Text = "Invoice Number applied successfully";
                    lblApplyMsg.ForeColor = System.Drawing.Color.Green;
                }
               

            }
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            
            int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
            BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
        }
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVendor.SelectedValue != "0")
            {
                int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
                BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCreditNote_Click(object sender, EventArgs e)
    {
        try
        {
            BindVendorInCreditNote();
            BindGridInCriditNote();
            mdlpopUpCreditNote.Show();
            ddlVendorsIncredit.SelectedValue = "0";
            txtCreditValue.Text = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindGridInCriditNote()
    {
        try
        {
            using(projectTaskDataContext Pdc=new projectTaskDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var projectref = 0;
                    if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
                    {
                        projectref = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                    }
                    else if (int.Parse(ddlProjects.SelectedValue) > 0)
                    {
                        projectref = int.Parse(ddlProjects.SelectedValue);
                    }

                    if (projectref > 0)
                    {
                        var CreditNotesList = Pdc.Project_CreditNotes.Where(a => a.ProjectRef == projectref).ToList();
                        var clist = Udc.Contractors.Where(a => CreditNotesList.Select(b => b.Appliedby.HasValue ? b.Appliedby.Value : 0).ToArray().Contains(a.ID)).ToList();

                        var x = (from a in CreditNotesList
                                 join b in clist on a.Appliedby equals b.ID
                                 orderby a.Id descending
                                 select new
                                 {
                                     Id = a.Id,
                                     CreditValue = a.CreditValue,
                                     Description = a.Description,
                                     DateandTime = a.DateandTime,
                                     Appliedby = b.ContractorName
                                 }).ToList();
                        gridCreditRecord.DataSource = x;
                        gridCreditRecord.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCreditApply_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                Project_CreditNote P_cn = new Project_CreditNote();
                P_cn.CreditValue = Convert.ToDouble(txtCreditValue.Text);
                P_cn.Description = "Credit Note from " + ddlVendorsIncredit.SelectedItem.Text.ToString();
                P_cn.VId = int.Parse(ddlVendorsIncredit.SelectedValue);
                if (int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text) != 0)
                {
                    P_cn.ProjectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
                }
                else if (int.Parse(ddlProjects.SelectedValue) > 0)
                {
                    P_cn.ProjectRef = int.Parse(ddlProjects.SelectedValue);
                }
                P_cn.DateandTime = DateTime.Now;
                P_cn.Appliedby = sessionKeys.UID;
                Pdc.Project_CreditNotes.InsertOnSubmit(P_cn);
                if (P_cn.ProjectRef > 0)
                {
                    Pdc.SubmitChanges();
                    lblCreditMsg.ForeColor = System.Drawing.Color.Green;
                    lblCreditMsg.Text = "Credit added successfully";
                    mdlpopUpCreditNote.Show();
                    
                    ddlVendorsIncredit.SelectedValue = "0";
                    txtCreditValue.Text = string.Empty;
                }
                else
                {
                    lblCreditMsg.ForeColor = System.Drawing.Color.Red;
                    lblCreditMsg.Text = "Please select project";
                    mdlpopUpCreditNote.Show();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        BindGridInCriditNote();
    }
}
[Serializable]
public class VerdorNamesClass
{
    public int VendorID { set; get; }
    public string ContractorName { set; get; }
}
