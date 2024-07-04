using Health.DAL;
using Health.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Controls
{
    public partial class MaintenanceScheduleFormCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMaintenanceType();
                BindGrid();
                txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);

            }
        }


        string inertnalUser = "Internal";
        string inertnalUserPassword = "Internal@!23";
        private void BindGrid()
        {
            try
            {
                List<PortfolioMgt.Entity.V_MaintenanceSchedule> rlist = MaintenanceScheduleList();
                GridList.DataSource = rlist;
                GridList.DataBind();

                if (rlist.Count > 0)
                {
                    //check Internal user existing
                    UserMgt.BAL.ContractorsBAL cbal = new UserMgt.BAL.ContractorsBAL();
                    var isUserExists =  cbal.UserExits(inertnalUser);
                    if(!isUserExists)
                    {
                        cbal.UserInsert(inertnalUser, inertnalUser, inertnalUserPassword);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private List<PortfolioMgt.Entity.V_MaintenanceSchedule> MaintenanceScheduleList()
        {
            string searchtext = txtSearch.Text.Trim();
            var rlist = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Select(sessionKeys.PortfolioID);
            if (!string.IsNullOrEmpty(txtFromdate.Text.Trim()))
                rlist = rlist.Where(o => o.DateOfReminder >= Convert.ToDateTime(txtFromdate.Text.Trim())).ToList();
            if (!string.IsNullOrEmpty(txtTodate.Text.Trim()))
                rlist = rlist.Where(o => o.DateOfReminder <= Convert.ToDateTime(txtTodate.Text.Trim())).ToList();
            if (!string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
            {
                rlist = rlist.Where(p =>
            (p.RequesterName != null ? p.RequesterName.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.EquipmentName != null ? p.EquipmentName.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.MaintenanceTypeName != null ? p.MaintenanceTypeName.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.ReminderDescription != null ? p.ReminderDescription.ToLower().Contains(searchtext.ToLower()) : false)

            ).OrderBy(p => p.DateOfReminder).Select(p => p).ToList();
            }
            if (Request.QueryString["ContactID"] != null)
            {
                rlist = rlist.Where(o => o.RequesterID == Convert.ToInt32(Request.QueryString["ContactID"].ToString())).ToList();
                btnRemainder.Visible = true;
                pnlSearch.Visible = false;
            }
            else
            {
                btnRemainder.Visible = false;
                pnlSearch.Visible = true;
            }
            return rlist;
        }

        protected void GridList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;
            var list = MaintenanceScheduleList();
            if (Sortdir == "ASC")
            {
                list = Sort<PortfolioMgt.Entity.V_MaintenanceSchedule>(list, SortExp, SortDirection.Ascending);
            }
            else
            {
                list = Sort<PortfolioMgt.Entity.V_MaintenanceSchedule>(list, SortExp, SortDirection.Descending);
            }
            this.GridList.DataSource = list;
            this.GridList.DataBind();
        }
        /// <summary>
        /// GEt Sorting direction
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PortfolioMgt.Entity.V_MaintenanceSchedule> Sort<TKey>(List<PortfolioMgt.Entity.V_MaintenanceSchedule> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.V_MaintenanceSchedule>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.V_MaintenanceSchedule>();
            }
        }
        protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "recurr")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    ShowRecurrencePopup(ID);
                } 
                 else   if (e.CommandName == "Edit1")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    var mData = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_SelectByID(Convert.ToInt32(hid.Value));
                    if (mData != null)
                    {
                        txtDateOfReminder.Text = mData.DateOfReminder.Value.ToShortDateString();
                        txtEquipment.Text = mData.EquipmentName;
                        txtReminderDescription.Text = mData.ReminderDescription;
                        txtRenewalAmount.Text = string.Format("{0:F2}", mData.RenewalAmount);
                        ddlMaintenanceType.SelectedValue = mData.MaintenanceTypeID.ToString();
                        ddlAssignedTo.SelectedValue = (mData.AssignTo.HasValue ? mData.AssignTo.Value : 0).ToString();
                        mdlExnter.Show();
                    }
                    else
                    {
                        ClearData();
                    }
                }
                else if (e.CommandName == "SendMail")
                {

                    int ID = Convert.ToInt32(e.CommandArgument);

                    var pBAL = new PortfolioMgt.BAL.PortfolioContactBAL();
                    var flsdata = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Select(sessionKeys.PortfolioID).Where(o => o.ID == ID).FirstOrDefault();
                    // var userBAL = new UserMgt.BAL.ContractorsBAL();
                    //userBAL.
                    var pdata = pBAL.PortfolioContact_SelectByID(flsdata.RequesterID).FirstOrDefault();

                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/CustomerAdmin/EmailTemplates/SendReminderEmail.htm");
                    string subject = string.Empty;
                    subject = "Maintenance alert";
                    body = body.Replace("[mail_head]", "Maintenance Schedule");

                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[user]", flsdata.RequesterName);
                    body = body.Replace("[MaintenanceType]", flsdata.MaintenanceTypeName);
                    body = body.Replace("[Equipment]", flsdata.EquipmentName);
                    body = body.Replace("[Instance]", flsdata.Portfolio);
                    body = body.Replace("[user]", flsdata.RequesterName);
                    body = body.Replace("[number]", string.Empty);


                    body = body.Replace("[img]", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/Admin/ImageHandler.ashx?type=customer&id={0}", flsdata.PortfolioID) + "'/>");


                    //foreach (var s in pdata.Split(','))
                    //{
                    if (!string.IsNullOrEmpty(pdata.Email))
                    {
                        em.SendingMail(fromemailid, subject, body, pdata.Email);
                        lblmsg.Text = "Mail has been sent successfully";
                    }
                    //}
                }
                else if (e.CommandName == "Delete1")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    var retval = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_DeleteByID(ID);
                    if (retval)
                    {
                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindMaintenanceType()
        {
            try
            {

                ddlMaintenanceType.DataSource = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_Select(sessionKeys.PortfolioID).OrderBy(o => o.Name);
                ddlMaintenanceType.DataTextField = "Name";
                ddlMaintenanceType.DataValueField = "ID";
                ddlMaintenanceType.DataBind();
                ddlMaintenanceType.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            try
            {

                var mData = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_SelectByID(Convert.ToInt32(hid.Value));
                if (mData != null)
                {
                    mData.DateOfReminder = Convert.ToDateTime(txtDateOfReminder.Text.Trim());
                    mData.EquipmentName = txtEquipment.Text.Trim();
                    mData.MaintenanceTypeID = Convert.ToInt32(ddlMaintenanceType.SelectedValue);
                    mData.PortfolioID = sessionKeys.PortfolioID;
                    mData.ReminderDescription = txtReminderDescription.Text.Trim();
                    mData.RenewalAmount = Convert.ToDouble(txtRenewalAmount.Text.Trim());
                    mData.AssignTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);
                    PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Update(mData);

                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    ClearData();
                    mdlExnter.Hide();
                    BindGrid();
                }
                else
                {
                    mData = new PortfolioMgt.Entity.MaintenanceSchedule();
                    mData.DateOfReminder = Convert.ToDateTime(txtDateOfReminder.Text.Trim());
                    mData.EquipmentName = txtEquipment.Text.Trim();
                    mData.MaintenanceTypeID = Convert.ToInt32(ddlMaintenanceType.SelectedValue);
                    mData.PortfolioID = sessionKeys.PortfolioID;
                    mData.ReminderDescription = txtReminderDescription.Text.Trim();
                    mData.RenewalAmount = Convert.ToDouble(txtRenewalAmount.Text.Trim());
                    mData.RequesterID = Convert.ToInt32(hcontactid.Value);
                    mData.AssignTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);
                    PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Add(mData);

                    lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                    txtDateOfReminder.Text = string.Empty;
                    txtEquipment.Text = string.Empty;
                    ddlMaintenanceType.SelectedValue = "0";
                    txtReminderDescription.Text = string.Empty;
                    txtRenewalAmount.Text = "0.00";
                    ddlAssignedTo.SelectedValue = "0";
                    //hcontactid.Value = "0";
                    mdlExnter.Hide();
                    BindGrid();
                }
                // Storage_AddUpdate(Convert.ToInt32(hbomid.Value), Convert.ToInt32(ddlsiteInSearch.SelectedValue), Convert.ToInt32(ddlWareshouse.SelectedValue), Convert.ToDouble(txtQtyReceived.Text));
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private void ClearData()
        {
            txtDateOfReminder.Text = string.Empty;
            txtEquipment.Text = string.Empty;
            ddlMaintenanceType.SelectedValue = "0";
            txtReminderDescription.Text = string.Empty;
            txtRenewalAmount.Text = "0.00";
            ddlAssignedTo.SelectedValue = "0";
            hid.Value = "0";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected string GetDateRemainderCheck(string dateremainder)
        {
            string st = string.Empty;
            try
            {
                //check the status is approve or submitted
                var d = Convert.ToDateTime(Convert.ToDateTime(dateremainder).ToShortDateString());
                var c = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now).ToShortDateString());
                if (c > d)
                    st = "Red";

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return st;

        }

        protected void btnRemainder_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
            {
                hid.Value = "0";
                hcontactid.Value = Request.QueryString["ContactID"].ToString();
                mdlExnter.Show();
            }
        }

        protected void ImgApply_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = 0;
                if (Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text) != 0 && txtEndDate.Text != "")
                {
                    chk = IsValidEndDate(int.Parse(txtRecur.Text));
                }
                if (chk == 0)
                {
                    var tdate = DateTime.Now;
                    var stime = txtStart.Text.Trim().Split(':');
                    var etime = txtEnd.Text.Trim().Split(':');

                    HealthCheckRecurr entity = new HealthCheckRecurr();
                    entity.StartTime = new DateTime(tdate.Year, tdate.Month, tdate.Day, Convert.ToInt32(stime.Length > 1 ? stime[0].ToString() : "0"), Convert.ToInt32(stime.Length > 1 ? stime[1].ToString() : "0"), 0); //Convert.ToDateTime("01/01/1900");
                    entity.EndTime = new DateTime(tdate.Year, tdate.Month, tdate.Day, Convert.ToInt32(etime.Length > 1 ? etime[0].ToString() : "0"), Convert.ToInt32(etime.Length > 1 ? etime[1].ToString() : "0"), 0); ;
                    entity.Duration = ddlDuration.SelectedValue;
                    entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? "01/01/1900" : txtStartDate.Text);
                    entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text);
                    entity.EndAfter = Convert.ToInt32(string.IsNullOrEmpty(txtEndOfOcurrences.Text) ? "0" : txtEndOfOcurrences.Text);
                    string days = "";
                    foreach (ListItem chkDay in chkDays.Items)
                    {
                        if (chkDay.Selected)
                        {
                            days += chkDay.Value + ",";
                        }
                    }
                    entity.WeekDayName = days;
                    entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
                    entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
                    entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
                    entity.HealthCheckID = Convert.ToInt32( hid.Value);
                     
                    HealthCheckListItemsHelper.InsertUpdateRecurr(entity);
                    hid.Value = "0";
                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

                else
                {
                    mpopRecurrence.Show();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ShowRecurrencePopup(int ID)
        {
            // imgRecurrence.Attributes.Add("onclick", "disableItem()");
            //string.Format("{0:t hh:mm}", healthCheckList.DateRaised.ToShortTimeString());
            try
            {
                int exist = HealthCheckListItemsHelper.IsExist(ID);
                if (exist != 0)
                {
                    HealthCheckRecurr entity = HealthCheckListItemsHelper.SelectById(ID);
                    //txtStartTime.Text = string.Format("{0:t hh:mm}", entity.StartTime.ToShortTimeString());
                    //txtEndTime.Text = string.Format("{0:t hh:mm}", entity.EndTime.ToShortTimeString());
                    if (string.Format("{0:dd/MM/yyyy}", entity.StartDate) == "01/01/1900")
                    {
                        txtStartDate.Text = "";
                    }
                    else
                    {
                        txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.StartDate);
                    }
                    if (string.Format("{0:dd/MM/yyyy}", entity.EndDate) == "01/01/1900")
                    {
                        txtEndDate.Text = "";
                    }
                    else
                    {
                        txtEndDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.EndDate);
                    }

                    rdPattern.SelectedValue = entity.ReCurrencePattern.ToString();
                    //if (rdPattern.SelectedValue != "1")
                    //{
                    //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "valid",
                    //    //    "<script language='javascript'> function disableItem() { var chkDays = document.getElementById('<%=chkDays.ClientID%>');chkDays.disabled = true;}</script>");

                    //    chkDays.Enabled = false;
                    //}
                    rdRangeOfRecurrence.SelectedValue = entity.ReCurrenceRange.ToString();

                    string[] days = entity.WeekDayName.Split(',');

                    foreach (ListItem chk in chkDays.Items)
                    {
                        if (days.Length > 0)
                        {
                            if (days.Contains(chk.Value))
                            {
                                chk.Selected = true;
                            }
                            else
                            {
                                chk.Selected = false;
                            }
                            //for (int i = 0; i < days.Length; i++)
                            //{
                            //    if (days[i].Length > 0)
                            //    {

                            //        if (days[i].Contains(chk.Value))
                            //        {
                            //            chk.Selected = true;
                            //        }
                            //        //else 
                            //        //{
                            //        //    chk.Selected = false;
                            //        //}
                            //    }
                                

                            //}

                        }
                        //else
                        //{
                        //    chk.Selected = false;
                        //}
                    }
                    if (entity.RecurWeekOn != 0)
                    {
                        txtRecur.Text = entity.RecurWeekOn.ToString();
                    }
                    else
                    {
                        txtRecur.Text = string.Empty;
                    }
                    txtEndOfOcurrences.Text = entity.EndAfter.ToString();

                    ddlDuration.SelectedValue = entity.Duration;
                    txtStart.Text = entity.StartTime.ToString("HH:mm");
                    txtEnd.Text = entity.EndTime.ToString("HH:mm");
                    rdRangeOfRecurrence.SelectedValue = entity.ReCurrenceRange.ToString();
                    //rdRangeOfRecurrence.SelectedValue = "1";
                    //rdPattern.SelectedValue = "1";

                    //foreach (ListItem chk in chkDays.Items)
                    //{
                    //    chk.Selected = true;
                    //}
                    mpopRecurrence.Show();
                }

                else
                {
                    txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                    txtEndDate.Text = string.Empty;
                    ddlDuration.SelectedValue = string.Empty;
                    txtStart.Text = "00:00";
                    txtEnd.Text = "00:00";
                    txtEndOfOcurrences.Text = string.Empty;
                    txtRecur.Text = "0";
                    rdRangeOfRecurrence.SelectedValue = "1";
                    rdPattern.SelectedValue = "1";
                    foreach (ListItem chk in chkDays.Items)
                    {
                        chk.Selected = true;
                    }
                    mpopRecurrence.Show();
                }
            }

            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private int IsValidEndDate(int weeks)
        {
            DateTime dt = Convert.ToDateTime(txtStartDate.Text);
            weeks = weeks * 7;
            DateTime dt1 = dt.AddDays(weeks);
            if (dt1 > Convert.ToDateTime(txtEndDate.Text) && weeks > 0)
            {
                lblRecurrMsg.Text = "Recur weeks exceeds end date";
                lblRecurrMsg.ForeColor = System.Drawing.Color.Red;
                return 1;
            }
            return 0;
        }
    }
}