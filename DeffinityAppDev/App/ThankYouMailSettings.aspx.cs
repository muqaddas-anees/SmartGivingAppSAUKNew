using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class ThankYouMailSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    //set default value
                    var pDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                    chk.Checked = pDetails.EnableThankyouMail.HasValue ? pDetails.EnableThankyouMail.Value : false;

                    ddlType.DataSource = DeffinityManager.TagsBAL.GetDonationTags().OrderBy(o=>o.Value).ToList();
                    ddlType.DataTextField = "Value";
                    ddlType.DataValueField = "ID";
                    ddlType.DataBind();
                    ddlType.Items.Insert(0, new ListItem("Select...", ""));

                    //update 


                    IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                   // var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();


                    BindDropDown();

                    SetSelectedLetter();
                    var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();

                    if(p != null)
                    {
                        //chk.Checked = p.EnableAutoMative.HasValue ? p.EnableAutoMative.Value : false;
                        //CKEditor1.Text = p.EmailContent;
                        //ddlType.SelectedValue = p.Type;
                    }
                    else
                    {
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/App/EmailTemplates/ThankyouMail.html");

                        body = body.Replace("[mail_head]",  " Donation");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    }
                }

                BindGrid();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                var result = (from t in tList
                              select new
                              {
                                  Title = t.Type,
                                  FromAmount = t.AmountGrater ?? 0.00,
                                  ToAmount = t.AmountLesser ?? 0.00

                              }).ToList();

                gridList.DataSource = result;
                gridList.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindDropDown()
        {
            try
            {

                AddDefaultData(sessionKeys.PortfolioID);

                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

               

                ddlTemplate.DataValueField = "ID";
                ddlTemplate.DataTextField = "Type";
                ddlTemplate.DataSource = tList;
                ddlTemplate.DataBind();
                ddlTemplate.Items.Insert(0, new ListItem("Please select...","0"));

                if(tList.ToList().Count >0)
                {
                    ddlTemplate.SelectedValue = tList.FirstOrDefault().ID.ToString();
                }
            } catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void AddDefaultData(int portfolioID)
        {

            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var tList = tk.GetAll().Where(o => o.PortfolioID == portfolioID).ToList();
                var tpLfist = tList.ToList();
                if (tpLfist.Count() == 0)
                {
                    tList = tk.GetAll().ToList();
                    var pE = tList.Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                    if (pE != null)
                    {
                        var dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Default Thank You Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;

                            tk.Add(dEntity);

                        }

                        pE = tList.Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        dEntity = tk.GetAll().Where(o => o.PortfolioID == portfolioID).Where(o => o.Type == "Recurring Email").FirstOrDefault();
                        if (dEntity == null)
                        {
                            dEntity = new PortfolioMgt.Entity.TithingThankyouSetting();
                            dEntity.Type = pE.Type;
                            dEntity.AmountGrater = pE.AmountGrater;
                            dEntity.EmailContent = pE.EmailContent;
                            dEntity.EnableAutoMative = pE.EnableAutoMative;
                            dEntity.IsAmountGraterThan = pE.IsAmountGraterThan;
                            dEntity.IsRecurring = pE.IsRecurring;
                            dEntity.Notes = pE.Notes;
                            dEntity.PortfolioID = portfolioID;
                            dEntity.SetAsDefault = pE.SetAsDefault;

                            tk.Add(dEntity);

                        }
                    }
                }
                BindGrid();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                PortfolioMgt.BAL.ProjectPortfolioBAL.UpdateThankyouMail(sessionKeys.PortfolioID,chk.Checked);
                
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID == Convert.ToInt32( ddlTemplate.SelectedValue)).FirstOrDefault();

                if (p != null)
                {
                    //p.Type = ddlTemplate.SelectedValue;
                    p.EmailContent = CKEditor1.Text;
                    p.EnableAutoMative = true;
                    p.SetAsDefault = chksetdefault.Checked;
                    p.IsRecurring = ChkRecurring.Checked;
                    p.IsAmountGraterThan = ChkIsGrater.Checked;
                    //if(txtAmount.Text.Trim().Length >0)
                    p.AmountGrater = Convert.ToDouble(txtMin.Text.Trim().Length ==0 ?"0": txtMin.Text.Trim());

                    p.AmountLesser = Convert.ToDouble(txtMax.Text.Trim() == "0"?"0": txtMax.Text.Trim());
                    tk.Edit(p);
                    if (chksetdefault.Checked == true)
                    {
                        var elist = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID != p.ID).ToList();
                        if ((p.SetAsDefault.HasValue ? p.SetAsDefault.Value : false) == true)
                        {
                            //remove old 
                            foreach (var d in elist)
                            {
                                d.SetAsDefault = false;
                                tk.Edit(p);
                            }
                        }
                    }
                }
                //else
                //{
                //    p = new PortfolioMgt.Entity.TithingThankyouSetting();
                //    p.PortfolioID = sessionKeys.PortfolioID;
                //   // p.Type = ddlTemplate.select;
                //    p.EmailContent = CKEditor1.Text;
                //    p.EnableAutoMative = chk.Checked;
                //    p.SetAsDefault = chksetdefault.Checked;
                //    tk.Add(p);

                //    if (chksetdefault.Checked == true)
                //    {
                //        var elist = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID != p.ID).ToList();
                //        if ((p.SetAsDefault.HasValue ? p.SetAsDefault.Value : false) == true)
                //        {
                //            //remove old 
                //            foreach (var d in elist)
                //            {
                //                d.SetAsDefault = false;
                //                tk.Edit(p);
                //            }
                //        }
                //    }
                //}
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            hid.Value = "0";
            ddlTemplate.SelectedValue = "0";
            mdlVideo.Show();
        }

        protected void btnSavetemplate_Click(object sender, EventArgs e)
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
               
                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o=>o.ID == Convert.ToInt32(ddlTemplate.SelectedValue)).FirstOrDefault();

                if (p != null)
                {
                    p.Type = txtTemplate.Text;
                    p.SetAsDefault = chksetdefault.Checked;
                    // p.EmailContent = CKEditor1.Text;
                    // p.EnableAutoMative = chk.Checked;
                    tk.Edit(p);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    if (chksetdefault.Checked == true)
                    {
                        var elist = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID != p.ID).ToList();
                        if ((p.SetAsDefault.HasValue ? p.SetAsDefault.Value : false) == true)
                        {
                            //remove old 
                            foreach (var d in elist)
                            {
                                d.SetAsDefault = false;
                                tk.Edit(p);
                            }
                        }
                    }
                }
                else
                {
                    p = new PortfolioMgt.Entity.TithingThankyouSetting();
                    p.PortfolioID = sessionKeys.PortfolioID;
                    p.Type = txtTemplate.Text;
                    p.SetAsDefault = chksetdefault.Checked;

                   // p.EmailContent = CKEditor1.Text;
                    //p.EnableAutoMative = chk.Checked;
                    tk.Add(p);
                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    if (chksetdefault.Checked == true)
                    {
                        var elist = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID != p.ID).ToList();
                        if ((p.SetAsDefault.HasValue ? p.SetAsDefault.Value : false) == true)
                        {
                            //remove old 
                            foreach (var d in elist)
                            {
                                d.SetAsDefault = false;
                                tk.Edit(p);
                            }
                        }
                    }
                }

                BindDropDown();
                BindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            mdlVideo.Hide();
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedLetter();
        }

        private void SetSelectedLetter()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID == Convert.ToInt32(ddlTemplate.SelectedValue)).FirstOrDefault();

                if (p != null)
                {
                    CKEditor1.Text = p.EmailContent;
                    hid.Value = p.ID.ToString();
                    chksetdefault.Checked = p.SetAsDefault.HasValue ? p.SetAsDefault.Value : false;
                    ChkIsGrater.Checked = p.IsAmountGraterThan.HasValue ? p.IsAmountGraterThan.Value : false;
                    ChkRecurring.Checked = p.IsRecurring.HasValue ? p.IsRecurring.Value : false;
                    txtAmount.Text = string.Format("{0:F2}", p.AmountGrater.HasValue ? p.AmountGrater.Value : 0);
                    txtMin.Text = string.Format("{0:F2}", p.AmountGrater.HasValue ? p.AmountGrater.Value : 0);
                    txtMax.Text = string.Format("{0:F2}", p.AmountLesser.HasValue ? p.AmountLesser.Value : 0);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                hid.Value = ddlTemplate.SelectedValue;
                txtTemplate.Text = ddlTemplate.SelectedItem.Text;
                mdlVideo.Show();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
          
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> tk = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();

                var p = tk.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.ID == Convert.ToInt32(ddlTemplate.SelectedValue)).FirstOrDefault();
                if(p != null)
                {
                    tk.Delete(p);
                }
                sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                Response.Redirect(Request.RawUrl, false);
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}