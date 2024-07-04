using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if(!IsPostBack)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
                    var f = fRep.GetAll().Where(o => o.ShortCode == QueryStringValues.UNID).FirstOrDefault();
                    var eList =  PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == f.MainFundUNID).FirstOrDefault();

                    if(f != null)
                    {
                        lblFunriaser_title.Text = eList.Title ;
                        lblInstance.Text = Deffinity.systemdefaults.GetInstanceTitle();
                        lblInstance1.Text = lblInstance.Text;

                        txtemail.Text = f.Email;
                        txtname.Text = f.FirstName + " " + f.LastName;


                        var userdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().Where(o => o.EmailAddress.ToLower().Trim() == f.Email.ToLower().Trim()).Where(o=>o.SID != 2).FirstOrDefault();

                        if (userdetails != null)
                        {
                            pnl_confirmpassword.Visible = false;
                            pnl_password.Visible = false;
                            btnsubmit.Text = "Continue";
                            btnsubmit.Visible = false;
                            btnSubmit1.Text = "Continue";
                            btnSubmit1.Visible = true;
                        }
                        else
                        {
                            btnsubmit.Visible = true;
                            btnSubmit1.Visible = false;
                        }


                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
                var f = fRep.GetAll().Where(o => o.ShortCode == QueryStringValues.UNID).FirstOrDefault();
                var eList = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == f.MainFundUNID).FirstOrDefault();

                if (f != null)
                {
                    var userdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().Where(o => o.EmailAddress.ToLower().Trim() == f.Email.ToLower().Trim()).Where(o => o.SID != 2).FirstOrDefault();

                    if( userdetails == null)
                    {
                        //add user to databse
                        var v = UserMgt.BAL.UserMgtBAL.AddOrUpdateFundriaser(f.Email, f.FirstName, f.LastName ?? "", f.ContactNo ?? "", txtpwd.Text.Trim(), "", "", "", "", "", "", eList.OrganizationID.Value);
                        //Create user

                        f.IsAddMember = true;

                        // f.Status = "Invitation Sent";
                        fRep.Edit(f);

                        int retval = Deffinity.Users.Login.LoginUser(f.Email, txtpwd.Text.Trim());

                        if (retval > 0)
                        {

                            Session["fund_user"] = f.Email.Trim().ToLower();
                            Session["fund_pwd"] = txtpwd.Text.Trim();
                            Session["fund_type"] = "";
                           
                            if (sessionKeys.SID == 3)
                            {
                                Session["fund_camp"] = "~/App/FundraiserListView.aspx";
                                Response.Redirect("~/Default.aspx", false);
                                // Response.Redirect("~/App/FundraiserListView.aspx", false);
                            }
                            else if (sessionKeys.SID == 1)
                            {
                                Session["fund_camp"] = "~/App/FundraiserListView.aspx?type=camp";
                                // Response.Redirect("~/App/FundraiserListView.aspx?type=camp", false);
                                Response.Redirect("~/Default.aspx", false);
                            }
                        }


                    }
                    else
                    {
                        int retval = Deffinity.Users.Login.LoginUser_withDecript(f.Email.Trim().ToLower(), userdetails.Password);

                        if (retval > 0)
                        {

                            Session["fund_user"] = f.Email.Trim().ToLower();
                            Session["fund_pwd"] = userdetails.Password;

                            Session["fund_type"] = "d";
                            if (sessionKeys.SID == 3)
                            {


                                Session["fund_camp"] = "~/App/FundraiserListView.aspx";
                                Response.Redirect("~/Default.aspx", false);
                            }
                            else if (sessionKeys.SID == 1)
                            {
                                Session["fund_camp"] = "~/App/FundraiserListView.aspx?type=camp";
                                // Response.Redirect("~/App/FundraiserListView.aspx?type=camp", false);
                                Response.Redirect("~/Default.aspx", false);
                            }
                        }
                    }
                   

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}