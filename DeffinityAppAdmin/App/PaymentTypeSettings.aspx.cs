using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class PaymentTypeSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        void BindGrid()
        {
            IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();


            grid_display.DataSource = pRep.GetAll().OrderBy(p=>p.RowID).ToList();
            grid_display.DataBind();
        }

        protected void grid_display_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                        if (p != null)
                        {
                            hid.Value = p.ID.ToString();
                            txtPaymentMethod.Text = p.PaymentMethod;
                            txtPaytypeCode.Text = p.ShortCode;
                            txtFixedFee.Text = (p.FixedFee).ToString();
                            txtTransactionFee.Text = (p.TransactionPercent).ToString();
                            chk_Active.Checked = p.IsActive;
                            mdl.Show();
                        }

                    }
                }
                if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        if (p != null)
                        {
                            pRep.Delete(p);
                        }
                        // PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(id);
                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                        //BindGrid();
                        Response.Redirect(Request.RawUrl, false);
                    }
                }

                if (e.CommandName == "up")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        SwapSortOrder(p.RowID, p.RowID - 1);
                    }
                    BindGrid();
                    //var id = Convert.ToInt32(e.CommandArgument);
                    //if (id > 0)
                    //{
                    //    IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                    //    var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                    //    if (p != null)
                    //    {
                    //        int r = p.RowID;
                    //        int down = r - 1;
                    //        if (down > 0)
                    //        {
                    //            p.RowID = down;
                    //            pRep.Edit(p);

                    //            //update old record

                    //            p = pRep.GetAll().Where(o => o.RowID == down).FirstOrDefault();
                    //            if (p != null)
                    //            {
                    //                p.RowID = r;
                    //                pRep.Edit(p);
                    //            }

                    //        }


                    //        BindGrid();


                    //    }

                    //}
                }
                if (e.CommandName == "down")

                {

                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        SwapSortOrder(p.RowID, p.RowID + 1);
                    }
                    BindGrid();                    //if (id > 0)
                    //{
                    //    IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                    //    var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                    //    if (p != null)
                    //    {
                    //        int r = p.RowID;
                    //        int up = r + 1;
                    //        if (up > 0)
                    //        {
                    //            p.RowID = up;
                    //            pRep.Edit(p);

                    //            //update old record

                    //            p = pRep.GetAll().Where(o => o.RowID == r).FirstOrDefault();
                    //            if (p != null)
                    //            {
                    //                p.RowID = up-1;
                    //                pRep.Edit(p);
                    //            }

                    //        }


                    //        BindGrid();


                    //    }

                    //}
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void SwapSortOrder(int currentSortOrder, int targetSortOrder)
        {
            using (PortfolioMgt.DAL.PortfolioDataContext dataContext = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                // Fetch the two items with the specified sort orders
                var currentItem = dataContext.GetTable<PortfolioMgt.Entity.tblPaymentMethod>().SingleOrDefault(i => i.RowID == currentSortOrder);
                var targetItem = dataContext.GetTable<PortfolioMgt.Entity.tblPaymentMethod>().SingleOrDefault(i => i.RowID == targetSortOrder);

                if (currentItem != null && targetItem != null)
                {
                    // Swap their sort orders
                    int tempSortOrder = currentItem.RowID;
                    currentItem.RowID = targetItem.RowID;
                    targetItem.RowID = tempSortOrder;

                    // Submit the changes
                    dataContext.SubmitChanges();
                }
            }
        }

        protected void btnAddBundle_Click(object sender, EventArgs e)
        {
            hid.Value = "0";
            txtPaytypeCode.Text = "";
            txtPaymentMethod.Text = "";
            txtFixedFee.Text = "0.00";
            txtTransactionFee.Text = "0.00";
            chk_Active.Visible = true;
            mdl.Show();
        }

        protected void btnSubmit_onclick(object sender, EventArgs e)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                if (txtPaymentMethod.Text.Length > 0)
                {
                    if (hid.Value == "0")
                    {
                        var p = new PortfolioMgt.Entity.tblPaymentMethod();
                        p.CreatedOn = DateTime.Now;
                        p.UpdatedOn = DateTime.Now;
                        p.FixedFee = Convert.ToDouble(txtFixedFee.Text.Trim() == "" ?"0.00":txtFixedFee.Text.Trim()) ;
                        p.IsActive = chk_Active.Checked;
                        p.PaymentMethod = txtPaymentMethod.Text.Trim();
                        p.RowID = pRep.GetAll().Count() +1;
                        p.ShortCode = txtPaytypeCode.Text.Trim();
                        p.TransactionPercent = Convert.ToDouble(string.IsNullOrEmpty(txtTransactionFee.Text.Trim()) ? "0" : txtTransactionFee.Text.Trim());
                        
                        //p.SMSCount = 
                        pRep.Add(p);

                        uploadImage(p.ID);
                        sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                        Response.Redirect(Request.RawUrl, false);
                    }
                    else
                    {
                        var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if (p != null)
                        {
                            p.UpdatedOn = DateTime.Now;
                            p.FixedFee = Convert.ToDouble(txtFixedFee.Text.Trim() == "" ? "0.00" : txtFixedFee.Text.Trim());
                            p.IsActive = chk_Active.Checked;
                            p.PaymentMethod = txtPaymentMethod.Text.Trim();
                           // p.RowID = pRep.GetAll().Count() + 1;
                            p.ShortCode = txtPaytypeCode.Text.Trim();
                            p.TransactionPercent = Convert.ToDouble(string.IsNullOrEmpty(txtTransactionFee.Text.Trim()) ? "0" : txtTransactionFee.Text.Trim());

                            pRep.Edit(p);
                            uploadImage(p.ID);
                            sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                            Response.Redirect(Request.RawUrl, false);
                        }
                    }
                }
                //lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                //string script = "window.onload = function() { toastr.success('etetetetet', 'testet'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        private void uploadImage(int portfolioid)
        {
            try

            {

                if (imgFile.HasFile)
                {

                    using (Stream fs = imgFile.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            ImageManager.FileDBSave(bytes, null, portfolioid.ToString(), ImageManager.file_section_paymenttype, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);

                        }
                    }
                }
                //    if (imgFile.PostedFile.FileName.Length > 0)
                //{
                //    Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgFile.PostedFile.InputStream);
                //    ImageManager.FileDBSave(imgFile.FileBytes, null,portfolioid.ToString(),ImageManager.file_section_portfolio_doc, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);
                //    // DisplayLogo();

                //  //  Response.Redirect(Request.RawUrl + "&v=" + DateTime.Now.Ticks.ToString(), false);
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}