using DeffinityManager.PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class SMSsettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if(sessionKeys.Message.Length >0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "Ok");
                    sessionKeys.Message = "";
                }

                try
                {
                    hid.Value = "0";
                    IPortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting> pRef = new PortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting>();
                    var pEntity = pRef.GetAll().FirstOrDefault();

                    if (pEntity != null)
                    {
                        txtAPISecret.Text = pEntity.APISecret;
                        txtClientID.Text = pEntity.ClientID;
                        txtSMSSell.Text = string.Format("{0:F2}", pEntity.SellingPrice);

                    }


                    BindGrid();
                    BindPurchaseHistory();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting> pRef = new PortfolioRepository<PortfolioMgt.Entity.SMSDefaultSetting>();
                var pEntity = pRef.GetAll().FirstOrDefault();
                if (pEntity == null)
                {
                    pEntity = new PortfolioMgt.Entity.SMSDefaultSetting();
                    pEntity.APISecret = txtAPISecret.Text;
                    pEntity.ClientID = txtClientID.Text;
                    pEntity.SellingPrice = Convert.ToDouble(txtSMSSell.Text);

                    pRef.Add(pEntity);
                }
                else
                {
                    pEntity.APISecret= txtAPISecret.Text ;
                    pEntity.ClientID = txtClientID.Text;
                    pEntity.SellingPrice = Convert.ToDouble( txtSMSSell.Text);

                    pRef.Edit(pEntity);

                }

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                Response.Redirect(Request.RawUrl, false);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        void BindGrid()
        {
            IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();

            
            grid_display.DataSource = pRep.GetAll().ToList();
            grid_display.DataBind();
        }


        void BindPurchaseHistory()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pcRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                var pcList = pcRep.GetAll().ToList();
                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pkRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();

                var pkList = pkRep.GetAll().ToList();

                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageDetail>();

                var sList = pRep.GetAll().ToList();

                IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();

                var uList = uRep.GetAll().Where(o => o.SID != 2).ToList();


                var rList = (from s in sList
                             join p in pkList on s.PackageID equals p.ID
                             where s.IsPaid == true
                             select new
                             {
                                 s.ID,
                                 s.PackageID,
                                 DateTime = s.PaidOn.Value.ToShortDateString() + " " + s.PaidOn.Value.ToShortTimeString(),
                                 Organization = pcList.Where(o => o.ID == s.PortfolioID).FirstOrDefault().PortFolio,
                                 Pacakge = p.PackageName,
                                 Volume = p.SMSCount,
                                 SellPrice = string.Format("{0:F2}", p.SellingPrice),
                                 PurchasedBy = uList.Where(o => o.ID == s.PaidBy).FirstOrDefault().ContractorName

                             }).ToList();

                GridPackageHistory.DataSource = rList.ToList();
                GridPackageHistory.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_onclick(object sender, EventArgs e)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();
                if (txtPackageTitle.Text.Length > 0)
                {
                    if (hid.Value == "0")
                    {
                        var p = new PortfolioMgt.Entity.SMSPackageSetting();
                        p.DateAdded = DateTime.Now;
                        p.DateModified = DateTime.Now;
                        p.PackageName = txtPackageTitle.Text.Trim();
                        p.SubText = txtSubText.Text.Trim();
                        p.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "0" : txtPrice.Text.Trim());
                        p.SMSCount = Convert.ToInt32(txtSMSVolume.Text.Trim());
                        //p.SMSCount = 
                        pRep.Add(p);

                        sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                        Response.Redirect(Request.RawUrl, false);
                    }
                    else
                    {
                        var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                        if(p != null)
                        {
                            p.DateModified = DateTime.Now;
                            p.PackageName = txtPackageTitle.Text.Trim();
                            p.SubText = txtSubText.Text.Trim();
                            p.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "0" : txtPrice.Text.Trim());
                            p.SMSCount = Convert.ToInt32(txtSMSVolume.Text.Trim());
                            pRep.Edit(p);

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

        protected void grid_display_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();
                        var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                        if(p!= null)
                        {
                            hid.Value = p.ID.ToString();
                            txtPackageTitle.Text =p.PackageName;
                            txtSubText.Text = p.SubText;
                            txtPrice.Text =(p.SellingPrice??0.00).ToString();
                            txtSMSVolume.Text = (p.SMSCount??0).ToString();

                            mdl.Show();
                        }

                    }
                }
                if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument);
                    if (id > 0)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.SMSPackageSetting>();
                        var p= pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                        if(p != null)
                        {
                            pRep.Delete(p);
                        }
                       // PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(id);
                        sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                       //BindGrid();
                        Response.Redirect(Request.RawUrl, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddBundle_Click(object sender, EventArgs e)
        {
            hid.Value = "0";
            txtPackageTitle.Text = "";
            txtSubText.Text = "";
            txtPrice.Text = "0.00";
            txtSMSVolume.Text = "0";
            mdl.Show();
        }

    }
}