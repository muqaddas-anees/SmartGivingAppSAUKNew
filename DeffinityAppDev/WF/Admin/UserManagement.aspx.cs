using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev.WF.Admin
{
    public partial class Userswithrole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortOn"] = "Username";
                ViewState["SortBy"] = "ASC";

                if (Request.QueryString["back"] != null)
                {
                    if (Request.QueryString["pnl"] != null)
                        linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                    else
                        linkBack.NavigateUrl = Request.QueryString["back"];
                    linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                    linkBack.Visible = true;
                }

                //if (Request.QueryString["Type"] == null)
                //{

                //    GridBind("","", "", ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
                //}
                //else
                //{
                //    GridBind(Request.QueryString["Type"].ToString(), "", "", ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
                //}
                Check_Status(ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
                
            }
        }
        public string GetClassName(string T_Name)
        {
            string R_class = string.Empty;
            if (T_Name != string.Empty)
            {
                if (Request.QueryString["Type"] != null)
                {
                    string Q_Name = Request.QueryString["Type"].ToString();
                    if (Q_Name == T_Name)
                    {
                        R_class = "nav-item active";
                    }
                    else if (Q_Name == T_Name)
                    {
                        R_class = "nav-item active";
                    }
                    else if (Q_Name == T_Name)
                    {
                        R_class = "nav-item active";
                    }
                }
            }
            else
            {
                if (Request.QueryString["Type"] == null)
                {
                    R_class = "nav-item active";
                }
                else
                {
                    R_class = "";
                }
            }
            return R_class;
        }
        public void GridBind(string Type,string StatusType, string Search_Text, string ColName, string OrderType)
        {
            try
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var Userslist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllActiveUsers(sessionKeys.PortfolioID).ToList();
                    var Result = (from a in Userslist
                                  where (a.SID == 1 || a.SID == 2 || a.SID == 4 || a.SID == 11 || a.SID == 12)
                                  select new
                                  {
                                      Uid = a.ID,
                                      Username = a.ContractorName,
                                      Email = a.EmailAddress,
                                      SID = a.SID,
                                      UsernameRole =
                                      (a.SID == 1) ? "Smart Pros" :
                                      (a.SID == 2) ? "Service Managers" :
                                      (a.SID == 3) ? "Project Manager & QA" :
                                      (a.SID == 4 || a.SID == 9) ? "Smart Techs" :
                                      (a.SID == 5) ? "QA/UAT" :
                                      (a.SID == 6) ? "Dashboard" :
                                      (a.SID == 10) ? "Casual Labour":
                                       (a.SID == 11) ? "Sales" :
                                        (a.SID == 12) ? "Customer Service" : "",
                                      Status = a.Status
                                  }).ToList();
                    if (!string.IsNullOrEmpty(StatusType))
                    {
                        if (StatusType == "Active")
                        {
                            Result = Result.Where(a => a.Status.ToLower() == StatusType.ToLower()).ToList();
                        }
                        else if (StatusType == "Inactive")
                        {
                            Result = Result.Where(a => a.Status.ToLower() == StatusType.ToLower()).ToList();
                        }
                    }
                    if (!string.IsNullOrEmpty(Search_Text))
                    {
                        Result = Result.Where(a => a.Username.ToLower().Contains(Search_Text.ToLower()) || a.Email.ToLower().Contains(Search_Text.ToLower())).ToList();
                    }
                    else
                    {

                        if (Type == "SmartPros")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Smart Pros").ToList();
                        }
                        else if (Type == "ServiceManagers")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Service Managers").ToList();
                        }
                        else if (Type == "PM")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Project Manager & QA").ToList();
                        }
                        else if (Type == "SmartTechs")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Smart Techs").ToList();
                        }
                        else if (Type == "QA/UAT")
                        {
                            Result = Result.Where(a => a.UsernameRole == "QA/UAT").ToList();
                        }
                        else if (Type == "Dashboard")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Dashboard").ToList();
                        }
                        else if (Type == "CasualLabour")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Casual Labour").ToList();
                        }
                        else if (Type == "Sales")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Sales").ToList();
                        }
                        else if (Type == "CustomerService")
                        {
                            Result = Result.Where(a => a.UsernameRole == "Customer Service").ToList();
                        }
                        var ulist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
                        Result = Result.Where(o => ulist.Contains(o.Uid)).OrderBy(a => a.Username).ToList();
                    }
                    GridUsers.DataSource = Result.OrderBy(a => a.Username).ToList();
                    //GridUsers.DataBind();
                    if ((ColName == "Username") && (OrderType == "DESC"))
                    {
                        GridUsers.DataSource = Result.OrderByDescending(a => a.Username).ToList();
                    }
                    else if ((ColName == "Username") && (OrderType == "ASC"))
                    {
                        GridUsers.DataSource = Result.OrderBy(a => a.Username).ToList();

                    }
                    else if ((ColName == "Email") && (OrderType == "ASC"))
                    {
                        GridUsers.DataSource = Result.OrderBy(a => a.Email).ToList();

                    }
                    else if ((ColName == "Email") && (OrderType == "DESC"))
                    {
                        GridUsers.DataSource = Result.OrderByDescending(a => a.Email).ToList();
                    }
                    GridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private List<int> GetUserListByCompany()
        {
            List<int> retval = new List<int>();
            try
            {
                //if (Session["CompanyID"] != null)
                //{
                    IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    var uEntity = uRep.GetAll().Where(o => o.CompanyID ==sessionKeys.PortfolioID).ToList();
                    if (uEntity.Count > 0)
                        retval = uEntity.Select(o => o.UserID).ToList();
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        protected void GridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Url")
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                if (arg[1].ToString() == "10")
                {
                    Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + arg[0].ToString() + "&sid=10");
                }
                else
                {
                    Response.Redirect("~/WF/Admin/AdminUsers.aspx?uid=" + arg[0].ToString());
                }
            }
        }

        protected void GridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUsers.PageIndex = e.NewPageIndex;
            Check_Status(ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());

            //GridBind(Request.QueryString["Type"].ToString(), "", "", ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
        }

        protected void GridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label L_Status = (Label)e.Row.FindControl("lblStatus");
                    CheckBox C = (CheckBox)e.Row.FindControl("CheckUserStatus");
                    if (L_Status != null)
                    {
                        if (L_Status.Text == "Active")
                        {
                            if (C != null)
                            {
                                C.Checked = true;
                            }
                        }
                        else
                        {
                            if (C != null)
                            {
                                C.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private bool ShowUpgrade()
        {
            var retval = false;

            try
            {
                var p = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.IsPaid == true);

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }

        protected void BtnCreateNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                int Type = 0;
                if (Request.QueryString["Type"] != null)
                {
                    string Q_String = Request.QueryString["Type"].ToString();
                    if (Q_String == "SmartPros")
                    {
                        Type = 1;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "ServiceManagers")
                    {
                        Type = 2;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "PM")
                    {
                        Type = 3;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "SmartTechs")
                    {
                        Type = 4;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "CasualLabour")
                    {
                        Type = 10;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?SID=10&Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "Dashboard")
                    {
                        Type = 6;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "Sales")
                    {
                        Type = 11;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                    else if (Q_String == "CustomerService")
                    {
                        Type = 12;
                        Response.Redirect("~/WF/Admin/AdminUsers.aspx?Type=" + Type.ToString(), false);
                    }
                }
                else
                {
                    Response.Redirect("~/WF/Admin/AdminUsers.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Request.QueryString["Type"] != null)
            //    {
            //        string S_Text = TxtSearch.Text.Trim();
            //        string Status_Name = ddlStatus.SelectedItem.Text;


            //        GridBind(Request.QueryString["Type"].ToString(), Status_Name, S_Text, ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}

            Check_Status(ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
        }

        protected void Check_Status(string SortOn, string SortBy)
        {
            try
            {
                if (Request.QueryString["Type"] != null)
                {
                    string S_Text = TxtSearch.Text.Trim();
                    string Status_Name = ddlStatus.SelectedItem.Text;


                    GridBind(Request.QueryString["Type"].ToString(), Status_Name, S_Text,SortOn,SortBy);
                }
                else {
                    GridBind("", "Status_Name", "S_Text", ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string strcolumnname = e.SortExpression;
            ViewState["SortOn"] = strcolumnname;
            //  if (ViewState["SortBy"] != null)
            //  {


            if (ViewState["SortBy"].ToString() == "ASC")
                ViewState["SortBy"] = "DESC";
            else
                ViewState["SortBy"] = "ASC";
            // }

            //string CName = ViewState["SortOn"].ToString();
            //string OType = ViewState["SortBy"].ToString();

            Check_Status(ViewState["SortOn"].ToString(), ViewState["SortBy"].ToString());
            //else
            //{
            //    GridBind(Request.QueryString["Type"].ToString(), "", "", CName, OType);
            //}
        }
    }
}