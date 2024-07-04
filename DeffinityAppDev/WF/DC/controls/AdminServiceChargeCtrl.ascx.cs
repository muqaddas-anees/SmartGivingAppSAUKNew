using DC.DAL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class AdminServiceChargeCtrl : System.Web.UI.UserControl
    {
        public string check_Password = "Aircon123#!";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["sadmin"] = null;
                pnlServiceCharge.Visible = false;
                pnlLogin.Visible = true;
            }
        }

        protected void btnAccess_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != check_Password)
                {
                    lblError.Text = "Invalid password. Please try again";
                    Session["sadmin"] = false;
                    pnlServiceCharge.Visible = false;
                    pnlLogin.Visible = true;
                }
                else
                {
                    Session["sadmin"] = true;
                    pnlServiceCharge.Visible = true;
                    pnlLogin.Visible = false;
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var s = dc.ServiceChargeDefaults.Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                        if (s != null)
                        {
                            txtAmount.Text = string.Format("{0:F2}", s.Amount.HasValue ? s.Amount.Value : 0);
                            txtDescription.Text = s.Name;
                            chkApply.Checked = s.ApplyVAT.HasValue ? s.ApplyVAT.Value : false;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["sadmin"] != null)
                {
                    if (Convert.ToBoolean(Session["sadmin"]))
                    {
                        using (DCDataContext dc = new DCDataContext())
                        {
                            var s = dc.ServiceChargeDefaults.Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                            if (s == null)
                            {
                                s = new ServiceChargeDefault();
                                s.Amount = Convert.ToDouble(txtAmount.Text);
                                s.ApplyVAT = chkApply.Checked;
                                s.PortfolioID = sessionKeys.PortfolioID;
                                s.Name = txtDescription.Text.Trim();
                                dc.ServiceChargeDefaults.InsertOnSubmit(s);
                                dc.SubmitChanges();
                            }
                            else
                            {
                                s.Amount = Convert.ToDouble(txtAmount.Text);
                                s.ApplyVAT = chkApply.Checked;
                                s.Name = txtDescription.Text.Trim();
                                dc.SubmitChanges();
                            }
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;

                        }
                    }
                    else
                    {
                        Session["sadmin"] = false;
                        pnlServiceCharge.Visible = false;
                        pnlLogin.Visible = true;
                    }

                }
                else
                {
                    Session["sadmin"] = false;
                    pnlServiceCharge.Visible = false;
                    pnlLogin.Visible = true;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}