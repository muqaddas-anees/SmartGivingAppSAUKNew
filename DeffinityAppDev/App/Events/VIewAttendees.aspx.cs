using ClosedXML.Excel;
using DeffinityAppDev.api;
using DocumentFormat.OpenXml.ExtendedProperties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using TuesPechkin;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App.Events
{
    public partial class VIewAttendees : System.Web.UI.Page
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
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private class TicketsDisplay
        {
            public string Title { set; get; }
            public string Value { set; get; }
        }

        private void BindGrid()
        {
            try
            {
                var booking_unid = QueryStringValues.UNID;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> eRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var eventdetails = eRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                lblEvent.Text = eventdetails.Title;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting> tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var tlist = tRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();


                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> bRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                var bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).ToList();


                var sdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var edate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (txtStartDate.Text.Trim().Length > 0)
                {
                    sdate = Convert.ToDateTime(txtStartDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        sdate = bookinglist.Select(o => o.BookingDate.Value).Min();
                }

                if (txtEndDate.Text.Trim().Length > 0)
                {
                    edate = Convert.ToDateTime(txtEndDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        edate = bookinglist.Select(o => o.BookingDate.Value).Max();
                }

                bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).Where(o => o.BookingDate >= sdate && o.BookingDate <= edate.AddDays(1).AddMinutes(-1)).ToList();



                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                var pblist = pbRep.GetAll().Where(o => bookinglist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").Where(o=>(o.BookingPrice.HasValue? o.BookingPrice.Value:0) == 0).ToList();

                var bNewlist = bookinglist.Where(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0) > 0).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                if (bNewlist.Count() > 0)
                {
                    pblist.AddRange(pbRep.GetAll().Where(o => bNewlist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").ToList());
                }


                // booking_unid = pblist.BookingUNID;
                //IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();

                //var blist = pRep.GetAll().Where(o => o. == booking_unid).ToList();




                var rlist = (from p in pblist

                             select new AttendessDisplay(
                                 p.ID,
                                 p.TicketType,
                                 p.UserEmail,
                                 p.UserName,
                               GetFirstName(p.UserName.Trim()),
                                GetLastName(p.UserName.Trim()),
                                 p.ValidatedDateTime,
                                 p.UserContact,
    p.EventStatus == null ? "Pending" : p.EventStatus,
    string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist.Where(o => o.unid == p.BookingUNID).Select(o => o.BookingDate).FirstOrDefault()),
    (p.ValidatedDateTime.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", p.ValidatedDateTime.Value) : ""),
     bookinglist.Where(o => o.unid == p.BookingUNID).Select(o=>o.PaymentDatetime).FirstOrDefault(), 
     p.BookingPrice.HasValue?p.BookingPrice.Value:0.00, p.BookingUNID,
    p.EventStatus == null ? "<span class='badge badge-danger'>Pending</span>" : (p.EventStatus == "Pending" ? "<span class='badge badge-danger'>Pending</span>" : "<span class='badge badge-success'>Attended</span>")
                             )).ToList();

                //foreach(var u in rlist)
                //{
                //    AddOrUpdateMembers(u.UserEmail, u.UserName, "", u.UserContact, "", "", "", "", eventdetails.Title, u.Status);
                //}
                // var eventdetails = pblist
                var searchtext = txtSearch.Text.Trim();
                if (searchtext.Length > 0)
                {
                    rlist = (from p in rlist
                             where ((p.UserName != null ? p.UserName.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserEmail != null ? p.UserEmail.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserContact != null ? p.UserContact.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false)||
                              (p.TicketType != null ? p.TicketType.ToLower().Contains(searchtext.ToLower()) : false)
                             )

                             select p
                             ).ToList();

                }

                GridViewAttendece.DataSource = rlist.OrderByDescending(o=>o.BookingDate).ToList();
                GridViewAttendece.DataBind();

                lblAttended.InnerText = rlist.Where(o => o.Status == "Attended").Count().ToString();
                lblPending.InnerText = rlist.Where(o => o.Status == "Pending").Count().ToString();

                List<TicketsDisplay> tdList = new List<TicketsDisplay>();
                foreach (var p in tlist)
                {
                   
                 //   tdList.Add(new TicketsDisplay() { Title =  p.TypeOfTicket + "<br> No of Bookings", Value = rlist.Where(o=>o.TicketType == p.TypeOfTicket).Count().ToString() });

                    
                    var amt = p.Price;
                    if (amt > 0)
                    {
                        var cnt = rlist.Where(o => o.TicketType == p.TypeOfTicket).Where(o => o.BookingStatus != null).Count();
                        tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> No of Bookings", Value = cnt.ToString() });
                        tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> Ticket Sales", Value = string.Format("{0:F2}", cnt * amt) });
                    }
                    else
                    {
                        tdList.Add(new TicketsDisplay() { Title = p.TypeOfTicket + "<br> No of Bookings", Value = rlist.Where(o => o.TicketType == p.TypeOfTicket).Count().ToString() });
                    }
                    // tdList.Add(new TicketsDisplay() { Title =  p.TypeOfTicket + "<br> Booking Amount", Value = "0" });
                }
                listdata.DataSource = tdList;
                listdata.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public void AddOrUpdateMembers(string email,string firstname,string lastname,string contactno,string address,string town,string state,string zipcode,string eventname,string eventstatus)
        {

            try
            {
                int userid = 0;

                if (email.Length>0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.Status == "Active").FirstOrDefault();
                   if( cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();
                      
                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = firstname.Trim() + " " + lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
                        var pw = "SMG@2022";
                        value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber =contactno;

                        cRep.Add(value);
                       

                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();

                       
                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = 190;
                        cdEntity.PostCode =zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);

                        userid = value.ID;
                    }

                    

                   //update company
                    var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }

                    var tags = "";
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "";
                        if (eventstatus == "Pending")
                        {
                             toadd = eventname + " - Not Attended";
                        }
                        else if (eventstatus == "Attended")
                        {
                            toadd = eventname + " - Attended";
                        }
                        var notes =  toadd + ",";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if(exitingNotes.Contains(","))
{
                            string toadd = "";
                            if (eventstatus == "Pending")
                            {
                                toadd = eventname + " - Not Attended";
                            }
                            else if (eventstatus == "Attended")
                            {
                                toadd = eventname + " - Attended";
                            }

                            exitingNotes = exitingNotes.Replace(",", toadd + ",");
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridViewAttendece_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName == "updatedstats")
                {
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    int i = GridViewAttendece.EditIndex;
                    GridViewRow Row = GridViewAttendece.Rows[i];

                    string status = ((DropDownList)Row.FindControl("ddlStatus")).SelectedValue;


                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                    var pbentity = pbRep.GetAll().Where(o => o.ID == ID ).FirstOrDefault();
                    if(pbentity != null)
                    {
                        pbentity.EventStatus = status;
                        if(status == "Attended")
                        pbentity.ValidatedDateTime = DateTime.Now;

                        pbRep.Edit(pbentity);

                        GridViewAttendece.EditIndex = -1;
                        BindGrid();
                    }


                }
                else if(e.CommandName == "updatedcancel")
                {
                    GridViewAttendece.EditIndex = -1;
                    BindGrid();
                }
                else if (e.CommandName == "del")
                {
                    //GridViewAttendece.EditIndex = -1;
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                    var pbentity = pbRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                    if (pbentity != null)
                    {
                        pbRep.Delete(pbentity);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    }
                        BindGrid();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridViewAttendece_DataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void GridViewAttendece_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    //check the empty row containg '-99' or not 
                    //if yes then hide that row
                   AttendessDisplay objList = (e.Row.DataItem as AttendessDisplay);
                    if (objList != null)
                    {
                        if (ddlStatus != null)
                        {
                            ddlStatus.SelectedValue = objList.Status;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void GridViewAttendece_RowEditing(object sender, GridViewEditEventArgs e)
{
            GridViewAttendece.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private string GetFirstName(string name)
        {
            string firstName = string.Empty;

            firstName = name.Split(' ').First();


            return firstName.Trim();
        }

        public string GetLastName(string name)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            firstName = name.Split(' ').First();

            if (firstName.Length > 0)
                lastName = name.Replace(firstName, "");

            return lastName.Trim();
        }

        protected bool dvisible()
        {
            if (QueryStringValues.Type.ToLower() == "all")
            {
                return true;
            }
            else
                return false;
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //Export to excel

            try
            {
               

                var booking_unid = QueryStringValues.UNID;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> eRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var eventdetails = eRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                lblEvent.Text = eventdetails.Title;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting> tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var tlist = tRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();


                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> bRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                var bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).ToList();


                var sdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var edate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (txtStartDate.Text.Trim().Length > 0)
                {
                    sdate = Convert.ToDateTime(txtStartDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        sdate = bookinglist.Select(o => o.BookingDate.Value).Min();
                }

                if (txtEndDate.Text.Trim().Length > 0)
                {
                    edate = Convert.ToDateTime(txtEndDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        edate = bookinglist.Select(o => o.BookingDate.Value).Max();
                }

                bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).Where(o => o.BookingDate >= sdate && o.BookingDate <= edate.AddDays(1).AddMinutes(-1)).ToList();



                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                var pblist = pbRep.GetAll().Where(o => bookinglist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "")
                    .Where(o => (o.BookingPrice.HasValue ? o.BookingPrice.Value : 0) == 0).ToList();

                var bNewlist = bookinglist.Where(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0) > 0).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                if (bNewlist.Count() > 0)
                {
                    pblist.AddRange(pbRep.GetAll().Where(o => bNewlist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").ToList());
                }


               

               
                // booking_unid = pblist.BookingUNID;
                //IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();

                //var blist = pRep.GetAll().Where(o => o. == booking_unid).ToList();




                var rlist = (from p in pblist

                             select new AttendessDisplay(
                                 p.ID,
                                 p.TicketType,
                                 p.UserEmail,
                                 p.UserName,
                               GetFirstName(p.UserName.Trim()),
                                GetLastName(p.UserName.Trim()),
                                 p.ValidatedDateTime,
                                 p.UserContact,
    p.EventStatus == null ? "Pending" : p.EventStatus,
    string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist.Where(o => o.unid == p.BookingUNID).Select(o => o.BookingDate).FirstOrDefault()),
    (p.ValidatedDateTime.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", p.ValidatedDateTime.Value) : ""),
     bookinglist.Where(o => o.unid == p.BookingUNID).Select(o => o.PaymentDatetime).FirstOrDefault(),
     p.BookingPrice.HasValue ? p.BookingPrice.Value : 0.00, p.BookingUNID,
     p.EventStatus == null ? "<span class='badge badge-danger'>Pending</span>" : (p.EventStatus =="Pending"? "<span class='badge badge-danger'>Pending</span>": "<span class='badge badge-success'>Attended</span>")
                             )).ToList();

                //foreach(var u in rlist)
                //{
                //    AddOrUpdateMembers(u.UserEmail, u.UserName, "", u.UserContact, "", "", "", "", eventdetails.Title, u.Status);
                //}
                // var eventdetails = pblist
                var searchtext = txtSearch.Text.Trim();
                if (searchtext.Length > 0)
                {
                    rlist = (from p in rlist
                             where ((p.UserName != null ? p.UserName.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserEmail != null ? p.UserEmail.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserContact != null ? p.UserContact.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false) ||
                              (p.TicketType != null ? p.TicketType.ToLower().Contains(searchtext.ToLower()) : false)

                             )
                             select p
                             ).ToList();

                }

                rlist = rlist.OrderByDescending(o => o.BookingDate).ToList();
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Attendees Report");


                int i = 1;
                // Title
                //ws.Cell("A"+i.ToString()).Value = "Event: "+ lblEvent.Text;
                //i++;
                ws.Cell("A" + i.ToString()).Value = "Event: " + lblEvent.Text +" - Attendees Report"; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                i++;
                ws.Cell("A" + i.ToString()).Value = "Ticket Type";
                //ws.Cell("B2").Value = "Comapany";
                ws.Cell("B" + i.ToString()).Value = "First Name";
                ws.Cell("C" + i.ToString()).Value = "Last Name";
                ws.Cell("D" + i.ToString()).Value = "Email";
                ws.Cell("E" + i.ToString()).Value = "CellNumber";
                //ws.Cell("G2").Value = "Site";
                //ws.Cell("H2").Value = "Department";
                ws.Cell("F" + i.ToString()).Value = "Status";
                ws.Cell("G" + i.ToString()).Value = "Booking Date & Time";
                ws.Cell("H" + i.ToString()).Value = "Validated Date & Time";
                i++;
                foreach (var item in rlist)
                {
                    ws.Cell("A" + i.ToString()).Value = item.TicketType; //"" + item.PaidDate.Value.ToShortDateString() + item.PaidDate.Value.ToShortTimeString();
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value =  item.Firstname;
                    ws.Cell("C" + i.ToString()).Value = item.Lastname;
                    ws.Cell("D" + i.ToString()).Value = item.UserEmail; //string.Format("{0:F2}", item.Amount);

                    ws.Cell("E" + i.ToString()).Value = item.UserContact; //item.PaymentType;

                    ws.Cell("F" + i.ToString()).Value = item.Status; //item.Status;
                    ws.Cell("G" + i.ToString()).Value = "" + item.BookingDate;
                    ws.Cell("H" + i.ToString()).Value = "" + item.ValidatedDateTime1;

                    i = i + 1;
                }

                // From worksheet
                var rngTable = ws.Range("A1:H2");

                var rngHeaders = rngTable.Range("A2:H2");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                rngTable.Cell(1, 1).Style.Font.FontSize = 15;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Row(1).Merge();

                ws.Columns(1, 9).AdjustToContents();

                string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/SAMReports");

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                wb.SaveAs(path + "\\" + "AttendeesReport.xlsx");
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "AttendeesReport.xlsx");
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename="+ lblEvent.Text.Trim().Replace(" ","-")  +"-AttendeesReport.xlsx");
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                //get the booking ids

                var booking_unid = QueryStringValues.UNID;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> eRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var eventdetails = eRep.GetAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                lblEvent.Text = eventdetails.Title;

                IPortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting> tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var tlist = tRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();


                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> bRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                var bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).ToList();


                var sdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var edate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (txtStartDate.Text.Trim().Length > 0)
                {
                    sdate = Convert.ToDateTime(txtStartDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        sdate = bookinglist.Select(o => o.BookingDate.Value).Min();
                }

                if (txtEndDate.Text.Trim().Length > 0)
                {
                    edate = Convert.ToDateTime(txtEndDate.Text.Trim());
                }
                else
                {
                    if (bookinglist.Count > 0)
                        edate = bookinglist.Select(o => o.BookingDate.Value).Max();
                }

                bookinglist = bRep.GetAll().Where(o => o.ActivityID == eventdetails.ID).Where(o => o.BookingDate >= sdate && o.BookingDate <= edate.AddDays(1).AddMinutes(-1)).ToList();



                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                var pblist = pbRep.GetAll().Where(o => bookinglist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "")
                    .Where(o => (o.BookingPrice.HasValue ? o.BookingPrice.Value : 0) == 0).ToList();

                var bNewlist = bookinglist.Where(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0) > 0).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                if (bNewlist.Count() > 0)
                {
                    pblist.AddRange(pbRep.GetAll().Where(o => bNewlist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").ToList());
                }





                // booking_unid = pblist.BookingUNID;
                //IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();

                //var blist = pRep.GetAll().Where(o => o. == booking_unid).ToList();




                var rlist = (from p in pblist

                             select new AttendessDisplay(
                                 p.ID,
                                 p.TicketType,
                                 p.UserEmail,
                                 p.UserName,
                               GetFirstName(p.UserName.Trim()),
                                GetLastName(p.UserName.Trim()),
                                 p.ValidatedDateTime,
                                 p.UserContact,
    p.EventStatus == null ? "Pending" : p.EventStatus,
    string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist.Where(o => o.unid == p.BookingUNID).Select(o => o.BookingDate).FirstOrDefault()),
    (p.ValidatedDateTime.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", p.ValidatedDateTime.Value) : ""),
     bookinglist.Where(o => o.unid == p.BookingUNID).Select(o => o.PaymentDatetime).FirstOrDefault(),
     p.BookingPrice.HasValue ? p.BookingPrice.Value : 0.00, p.BookingUNID,
     p.EventStatus == null ? "<span class='badge badge-danger'>Pending</span>" : (p.EventStatus == "Pending" ? "<span class='badge badge-danger'>Pending</span>" : "<span class='badge badge-success'>Attended</span>")


                             )).ToList();

                //foreach(var u in rlist)
                //{
                //    AddOrUpdateMembers(u.UserEmail, u.UserName, "", u.UserContact, "", "", "", "", eventdetails.Title, u.Status);
                //}
                // var eventdetails = pblist
                var searchtext = txtSearch.Text.Trim();
                if (searchtext.Length > 0)
                {
                    rlist = (from p in rlist
                             where ((p.UserName != null ? p.UserName.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserEmail != null ? p.UserEmail.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.UserContact != null ? p.UserContact.ToLower().Contains(searchtext.ToLower()) : false) ||
                             (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false) ||
                              (p.TicketType != null ? p.TicketType.ToLower().Contains(searchtext.ToLower()) : false)

                             )
                             select p
                             ).ToList();

                }


                var bookingids = rlist.Select(o => o.BookingUnid).ToList();

                foreach(var b in bookingids)
                {
                    SendEmailTOClient(b);
                }
                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Booking Mail(s) sent successfully", "");


            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SendEmailTOClient(string unid)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> auRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var ulist = puRep.GetAll().Where(o => o.BookingUNID == unid).ToList();

                //get booking details
                var bEntity = pRep.GetAll().Where(o => o.unid == ulist.FirstOrDefault().BookingUNID).FirstOrDefault();

                //get activity or event detsils
                var aEntity = auRep.GetAll().Where(o => o.ID == bEntity.ActivityID).FirstOrDefault();
                sessionKeys.PortfolioID = aEntity.OrganizationID;
                //get customer details 
                var portfolioDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == aEntity.OrganizationID).FirstOrDefault();

                // int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(sessionKeys.PortfolioID);
                string subject = "Here's your tickets";
                Emailer em = new Emailer();
                string bodyNew = em.ReadFile("~/WF/DC/EmailTemplates/eventtickets.htm");

                foreach (var u in ulist)
                {
                    string body = bodyNew;

                    body = body.Replace("[mail_head]", " Ticket(s)");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID));
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                    body = body.Replace("[noteslist]", GetQuoteItemList(u.UserBookingUNID));

                    body = body.Replace("[eventname]", aEntity.Title);
                    body = body.Replace("[eventdatetime]", string.Format("{0:dd/MM/yyyy hh:mm tt}", aEntity.StartDateTime));
                    body = body.Replace("[eventvenue]", string.Format("{0} {1} {2} {3} {4} {5}", aEntity.Venue_Name, aEntity.Address1, aEntity.Address2, aEntity.City, aEntity.state_Province, aEntity.Postalcode));
                    body = body.Replace("[orgname]", portfolioDetails.PortFolio);

                    body = body.Replace("[eventimage]", Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Events/" + aEntity.unid.ToString() + "/0.png");

                    //[date]
                    string Dis_body = body;
                    bool ismailsent = false;
                    // mail to requester
                    //if help desk or assign users are changed then mail should go to requester
                    body = body.Replace("[user]", u.UserName);



                    //var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                    //string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                    //if (File.Exists(pname))
                    //{
                    //    var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                    //    Email ToEmail = new Email();
                    //    Attachment attachment1 = new Attachment(pname);
                    //    attachment1.Name = q.CurrentTemplateName + ".pdf";

                    //    ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                    //}
                    //else
                    //{
                    em.SendingMail(fromemailid, subject, body, u.UserEmail);
                    //}
                    ismailsent = true;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetQuoteItemList(string unid)
        {
            StringBuilder sbuild = new StringBuilder();

            sbuild.Append("<table style='width:70%'>");
            var b = Deffinity.systemdefaults.GetWebUrl() + string.Format("/App/Events/TicketDetails.aspx?unid={0}", unid);
            sbuild.Append(string.Format("<tr><td><b> {1} </b><br></td><td>{0}</td></tr>", getButton(b, "View Ticket"), ""));
            sbuild.Append("</table>");




            return sbuild.ToString();
        }

        private string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#7239EA'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #7239EA; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //  / App / Events / EventDetails.aspx ? unid = ead44ca4 - e8cd - 48e5 - bdf6 - 5ad478b5d7d7

            Response.Redirect("~/App/Events/EventDetails.aspx?unid=" + QueryStringValues.UNID, false);
        }
    }

    internal class AttendessDisplay
    {
        public int ID { get; }
        public string TicketType { get; }
        public string UserEmail { get; }
        public string UserName { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public DateTime? ValidatedDateTime { get; }
        public string UserContact { get; }
        public string Status { get; }

        public string DStatus { get; }
        public string BookingDate { get; }
       
        public string ValidatedDateTime1 { get; }
        public string BookingStatus { get; }
        public double BookingAmount { get; }
        public string BookingUnid { get; }

        public AttendessDisplay(int iD, string ticketType, string userEmail, string userName, string firstName, string lastName, DateTime? validatedDateTime, 
            string userContact, string status, string bookingDate, string validatedDateTime1,string bookingStatus,double bookingAmount,string bookingUnid,string dstatus)
        {
            ID = iD;
            TicketType = ticketType;
            UserEmail = userEmail;
            UserName = userName;
            Firstname = firstName;
            Lastname = lastName;
            ValidatedDateTime = validatedDateTime;
            UserContact = userContact;
            Status = status;
            BookingDate = bookingDate;
            ValidatedDateTime1 = validatedDateTime1;
            BookingStatus = bookingStatus;
            BookingAmount = bookingAmount;
            BookingUnid = bookingUnid;
            DStatus = dstatus;
            
        }

      
        //public override bool Equals(object obj)
        //{
        //    return obj is AttendessDisplay other &&
        //           ID == other.ID &&
        //           TicketType == other.TicketType &&
        //           UserEmail == other.UserEmail &&
        //           UserName == other.UserName &&
        //             Firstname == other.Firstname &&
        //               Lastname == other.Lastname &&
        //           ValidatedDateTime == other.ValidatedDateTime &&
        //           UserContact == other.UserContact &&
        //           Status == other.Status &&
        //           BookingDate == other.BookingDate &&
        //           ValidatedDateTime1 == other.ValidatedDateTime1;
        //}

        //public override int GetHashCode()
        //{
        //    int hashCode = -916408844;
        //    hashCode = hashCode * -1521134295 + ID.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TicketType);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserEmail);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Firstname);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lastname);
        //    hashCode = hashCode * -1521134295 + ValidatedDateTime.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserContact);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BookingDate);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ValidatedDateTime1);
        //    return hashCode;
        //}
    }
}