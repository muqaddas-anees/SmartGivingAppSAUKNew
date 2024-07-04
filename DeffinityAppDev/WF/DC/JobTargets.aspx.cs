using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

namespace DC.BLL
{
    public static class JobTargetStatusBAL
    {
        public static string Pending = "Pending";
        public static string InProgress = "In Progress";
        public static string Completed = "Completed";

        public static List<string> GetStatusList()
        {

            List<string> Jlist = new List<string>();
            Jlist.Add(Pending);
            Jlist.Add(InProgress);
            Jlist.Add(Completed);

            return Jlist;

        }
    }
       

}

namespace DeffinityAppDev.WF.DC
{
    public partial class JobTargets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hcallid.Value = QueryStringValues.CallID.ToString();
                    txtStartDate.Text = DateTime.Now.ToShortDateString();
                    BindUsers();
                    BindListView();


                }


                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                //txtComments
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindListView()
        {
            try
            {
                var slist = JobTargetStatusBAL.GetStatusList();
                var rlist = JobTargetBAL.JobTargetBAL_SelectAll().Where(o=>o.CallID == Convert.ToInt32(hcallid.Value)).ToList();
                var aslist = JobTargetAssignedUserBAL.JobTargetAssignedUserBAL_SelectAll().ToList();
                var cList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                var dList = JobTargetsDocBAL.JobTargetsDocBAL_SelectAll().ToList();
                var commentsList = JobTargetCommentBAL.JobTargetCommentBAL_SelectAll().ToList();

                var result = (from r in rlist
                              select new
                              {
                                  r.ID,
                                  r.Title,
                                  r.ModifiedDate,
                                  r.LoggedDate,
                                  r.Notes,
                                  r.Status,
                                  r.Details,
                                  r.CallID,
                                  Users = aslist.Where(o => o.JobTargetID == r.ID).ToList(),
                                  DocsCount = dList.Where(o => o.JobTargetID == r.ID).Count(),
                                  CommentsCount = commentsList.Where(o=>o.JobTargetID == r.ID).Count()

                              });


                list_pending.DataSource = result.Where(o=>o.Status == JobTargetStatusBAL.Pending).OrderByDescending(o => o.ModifiedDate).ToList();
                list_pending.DataBind();
                lblPendingCount.Text = list_pending.Items.Count().ToString();

                list_inprogress.DataSource = result.Where(o => o.Status == JobTargetStatusBAL.InProgress).OrderByDescending(o => o.ModifiedDate).ToList();
                list_inprogress.DataBind();
                lblInProgressCount.Text = list_inprogress.Items.Count().ToString();

                list_complete.DataSource = result.Where(o => o.Status == JobTargetStatusBAL.Completed).OrderByDescending(o => o.ModifiedDate).ToList();
                list_complete.DataBind();
                lblCompletedCount.Text = list_complete.Items.Count().ToString();



            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void list_Customfields_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            list_pending.EditIndex = -1;
            //BindCustomFields();
        }
        protected void list_Customfields_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            list_pending.EditIndex = e.NewEditIndex;
            //BindCustomFields();
            BindListView();
        }
        protected void list_Customfields_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
               

                if(e.CommandName == "View")
                {
                    try
                    {
                        hid.Value = e.CommandArgument.ToString();
                        showTargetData();
                        mdlTargets.Show();
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.CommandName == "Docs")
                {
                    try
                    {
                        hTaskID.Value = e.CommandArgument.ToString();
                        Gridfilesbind();
                        //showTargetData();
                        mdlFile.Show();
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.CommandName == "Comments")
                {
                    try
                    {
                        hTaskIDComments.Value = e.CommandArgument.ToString();
                        BindListComments();
                        //showTargetData();
                        mdlCommentes.Show();
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.CommandName == "Item")
                {
                    var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                    Response.Redirect(string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                }
                else if (e.CommandName == "Del")
                {

                    QuotationOptionsBAL.QuotationOption_DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));

                    sessionKeys.Message = "Estimate deleted successfully";

                    Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID));

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                string[] bgcolor = { "bg-success", "bg-primary", "bg-warning" };
                Random rnd = new Random();
                //int index = rnd.Next(bgcolor.Length);
                var cList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {


                    Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;

                    Literal lblGetUsers = (Literal)e.Item.FindControl("lblUsers");

                    if(lblGetUsers != null)
                    {
                        string ret = "";
                        var ulist = d.Users;

                        if(ulist != null)
                        {
                            var userlist = ulist;
                            if (userlist != null)
                            {
                                List<JobTargetAssignedUser> utLIst = userlist as List<JobTargetAssignedUser>;
                              
                                foreach (var u in utLIst)
                                {
                                    var uDetails = cList.Where(o => o.ID == u.UserID).FirstOrDefault();
                                    if (uDetails != null)
                                    {
                                        int index = rnd.Next(bgcolor.Length);
                                        //GetImageUrl

                                        var imgurl = GetImageUrl(u.UserID.Value.ToString());
                                        if(!string.IsNullOrEmpty(imgurl))
                                            ret = ret + string.Format("<div class='symbol symbol-35px symbol-circle' data-bs-toggle='tooltip' title='' data-bs-original-title='{0}'><img src='{3}'/></div>", uDetails.ContractorName, uDetails.ContractorName.Substring(0, 1), bgcolor[index], imgurl);
                                        else
                                        ret = ret + string.Format("<div class='symbol symbol-35px symbol-circle' data-bs-toggle='tooltip' title='' data-bs-original-title='{0}'><span class='symbol-label {2} text-inverse-warning fw-bolder'>{1}</span></div>", uDetails.ContractorName, uDetails.ContractorName.Substring(0, 1), bgcolor[index]);
                                }
                                }

                            }
                        }
                        lblGetUsers.Text = ret;
                    }

                    if (lbl != null)
                    {

                        var r = d.IsAplied;
                        if (!r)
                        {
                            lbl.Visible = false;
                        }
                        // BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl);
                    }
                    HyperLink hlink = (HyperLink)e.Item.FindControl("hlinkItems");
                    if (hlink != null)
                    {
                        var optionID = d.ID;
                        hlink.NavigateUrl = string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

                    }
                    HyperLink hLinkPlan = (HyperLink)e.Item.FindControl("hLinkPlan");
                    if (hLinkPlan != null)
                    {
                        //WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID=10227&addid=3866
                        //var optionID = d.ID;
                        var j = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID);
                        
                        hLinkPlan.NavigateUrl = string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?CCID={0}&callid={1}&SDID={1}&ContactID={3}&addid={2}", QueryStringValues.CCID, QueryStringValues.CallID, j.FirstOrDefault().ContactAddressID, j.FirstOrDefault().RequesterID);

                    }

                    HyperLink hLinkFinnace = (HyperLink)e.Item.FindControl("hLinkFinnace");
                    if (hLinkFinnace != null)
                    {
                        var optionID = d.ID;
                        hLinkFinnace.NavigateUrl = "#"; //string.Format("~/WF/DC/DCFinancing.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

                    }
                    //DropDownList ddl = (DropDownList)e.Item.FindControl("ddlRatetype");
                    //if (ddl != null)
                    //{
                    //    BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl);
                    //}
                    //DropDownList ddl_e = (DropDownList)e.Item.FindControl("ddlRatetype_e");
                    //if (ddl_e != null)
                    //{
                    //    BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl_e);
                    //}
                    //CheckBoxList chkDays = (CheckBoxList)e.Item.FindControl("chkDays");
                    //if (chkDays != null)
                    //{
                    //    BindDays(chkDays, (e.Item.DataItem as v_TimesheetEntryCustom).Days.Split(',').ToList());
                    //}
                    //CheckBoxList chkDays_e = (CheckBoxList)e.Item.FindControl("chkDays_e");
                    //if (chkDays_e != null)
                    //{
                    //    BindDays(chkDays_e, (e.Item.DataItem as v_TimesheetEntryCustom).Days.Split(',').ToList());
                    //}
                    //Panel pnlTime = (Panel)e.Item.FindControl("pnlTime");
                    //Panel pnlHours = (Panel)e.Item.FindControl("pnlHours");
                    //if (pnlTime != null && pnlHours != null)
                    //{
                    //    SetPanleVisibility(pnlTime, pnlHours);
                    //}
                    //Panel pnlTime_e = (Panel)e.Item.FindControl("pnlTime_e");
                    //Panel pnlHours_e = (Panel)e.Item.FindControl("pnlHours_e");
                    //if (pnlTime_e != null && pnlHours_e != null)
                    //{
                    //    SetPanleVisibility(pnlTime_e, pnlHours_e);
                    //}

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public void BindUsers()
        {
            try
            {
                var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o=>o.SID == 1).Where(o=>o.Status == "Active").OrderBy(o=>o.ContractorName).ToList();
                ddlUsers.DataSource = ulist;
                ddlUsers.DataTextField = "ContractorName";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();
                //ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("../../WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("../../WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img;// + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
        protected void btnAddTarget_click(object sender, EventArgs e)
        {

            ddlUsers.ClearSelection();
            txtDetails.Value = string.Empty;
            hid.Value = "0";
            txttitle.Value = string.Empty;
            txtStartDate.Text = DateTime.Now.ToShortDateString();

            mdlTargets.Show();
        }
        private void showTargetData()
        {
            try
            {
                ddlUsers.ClearSelection();

                if (Convert.ToInt32(hid.Value) > 0)
                {
                    var jt = JobTargetBAL.JobTargetBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();

                    if(jt != null)
                    {
                        txttitle.Value = jt.Title;
                        txtDetails.Value = jt.Details;
                        txtStartDate.Text = jt.ModifiedDate.Value.ToShortDateString();
                        ddlStatus.SelectedValue = jt.Status;

                        var jAlist = JobTargetAssignedUserBAL.JobTargetAssignedUserBAL_SelectAll().Where(o => o.JobTargetID == Convert.ToInt32(hid.Value)).ToList();
                        if(jAlist.Count >0)
                        {
                            foreach (ListItem Item in ddlUsers.Items)
                            {
                                if(jAlist.Where(o=>o.UserID == Convert.ToInt32(Item.Value) ).FirstOrDefault() != null)
                                {
                                    Item.Selected = true;
                                }
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

        protected void btncloseCommnets_click(object sender,EventArgs e)
        {
            txtComments.Text = string.Empty;
            BindListView();
            mdlCommentes.Hide();
        }

        protected void btnclosedocs_click(object sender, EventArgs e)
        {
           
            BindListView();
            mdlCommentes.Hide();
        }
        protected void btnSaveTarget_click(object sender, EventArgs e)
        {
            try
            {

                if(Convert.ToInt32( hid.Value) >0)
                {
                    var title = txttitle.Value;
                    var details = txtDetails.Value;
                    var duedate = txtStartDate.Text;
                    var jt = JobTargetBAL.JobTargetBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(hid.Value)).FirstOrDefault();
                    jt.CallID = Convert.ToInt32(hcallid.Value);
                    jt.Title = title;
                    jt.Details = details;
                    // jt.Status = "Pending";
                    jt.LoggedDate = DateTime.Now;
                    jt.Status = ddlStatus.SelectedValue;
                    jt.ModifiedDate = Convert.ToDateTime(txtStartDate.Text);
                    JobTargetBAL.JobTargetBAL_Update(jt);
                    if (jt != null)
                    {

                        foreach (ListItem Item in ddlUsers.Items)
                        {
                            if (Item.Selected == true)
                            {
                                var ja = new JobTargetAssignedUser();
                                ja.CallID = Convert.ToInt32(hcallid.Value);
                                ja.JobTargetID = jt.ID;
                                ja.UserID = Convert.ToInt32(Item.Value);
                                JobTargetAssignedUserBAL.JobTargetAssignedUserBAL_Add(ja);
                            }
                        }
                        
                    }

                    BindListView();
                    mdlTargets.Hide();
                }
                else
                {
                    //add the task

                    var title = txttitle.Value;
                    var details = txtDetails.Value;
                    var duedate = txtStartDate.Text;
                    var jt = new JobTarget();
                    jt.CallID = Convert.ToInt32(hcallid.Value);
                    jt.Title = title;
                    jt.Details = details;
                   // jt.Status = "Pending";
                    jt.LoggedDate = DateTime.Now;
                    jt.Status = ddlStatus.SelectedValue;
                    jt.ModifiedDate = Convert.ToDateTime(txtStartDate.Text);
                    JobTargetBAL.JobTargetBAL_Add(jt);
                    if (jt != null)
                    {

                        foreach (ListItem Item in ddlUsers.Items)
                        {
                            if (Item.Selected == true)
                            {
                                var ja = new JobTargetAssignedUser();
                                ja.CallID = Convert.ToInt32(hcallid.Value);
                                ja.JobTargetID = jt.ID;
                                ja.UserID = Convert.ToInt32(Item.Value);
                                JobTargetAssignedUserBAL.JobTargetAssignedUserBAL_Add(ja);
                            }
                        }

                      
                    }
                    BindListView();
                    mdlTargets.Hide();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Download")
            {
                try
                {
                    mdlFile.Show();
                    var jobdoc = JobTargetsDocBAL.JobTargetsDocBAL_SelectAll().Where(o => o.ID == Convert.ToInt32( e.CommandArgument.ToString())).FirstOrDefault();
                    //var docid = e.CommandArgument.ToString();
                    //GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    // string contenttype = gridfiles.DataKeys[gvrow.RowIndex].Values[1].ToString();
                    //string filename = gridfiles.DataKeys[gvrow.RowIndex].Values[2].ToString();
                    //string[] ex = filename.Split('.');
                    //string ext = ex[ex.Length - 1];
                    //"~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString(
                    string filepath = string.Format("~/WF/UploadData/Tasks/{0}/{1}", jobdoc.JobTargetID, jobdoc.DocumentID);
                    //Response.ContentType = contenttype;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + jobdoc.FileName + "\"");
                    Context.Response.ContentType = "octet/stream";
                    Response.TransmitFile(filepath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                mdlFile.Show();
                string filePath = (sender as LinkButton).CommandArgument;

                var jobdoc = JobTargetsDocBAL.JobTargetsDocBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(filePath)).FirstOrDefault();
                JobTargetsDocBAL.JobTargetsDocBAL_delete(Convert.ToInt32(filePath));
                Gridfilesbind();
                BindListView();
                //File.Delete(filePath);
               // Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void Gridfilesbind()
        {
            try
            {
                if (Convert.ToInt32( hTaskID.Value) >0)
                {

                    var jobtaskFilesList = JobTargetsDocBAL.JobTargetsDocBAL_SelectAll().Where(o => o.JobTargetID == Convert.ToInt32(hTaskID.Value)).ToList();

                    //if(jobtaskFilesList.Count >0)
                    //{
                        var rList = (from f in jobtaskFilesList
                                     select new JobTargetsDoc
                                     {
                                         CallID = f.CallID,
                                         ContentType = f.ContentType,
                                         DocumentID = f.DocumentID,
                                         FileName = f.FileName,
                                         JobTargetID = f.JobTargetID,
                                         ID = f.ID
                                     }).ToList();
                        gridfiles.DataSource = rList;
                        gridfiles.DataBind();
                   // }
                  
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void BindListComments()
        {
            try
            {
                if (Convert.ToInt32(hTaskIDComments.Value) > 0)
                {

                    var jobtaskFilesList = JobTargetCommentBAL.JobTargetCommentBAL_SelectAll().Where(o => o.JobTargetID == Convert.ToInt32(hTaskIDComments.Value)).ToList();
                    var userlist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
                    //if (jobtaskFilesList.Count > 0)
                    //{
                        var rList = (from f in jobtaskFilesList
                                     orderby f.LoggedDate descending
                                     select new 
                                     {
                                         CallID = f.CallID,
                                         JobTargetID = f.JobTargetID,
                                         ID = f.ID,
                                         Details = f.Details,
                                         LoggedDate = f.LoggedDate,
                                         LoggedDateDisplay = f.LoggedDate.Value.ToShortDateString() + " " + f.LoggedDate.Value.ToShortTimeString(),
                                         UserID = f.UserID,
                                         UserName = userlist.Where(o=>o.ID == f.UserID).FirstOrDefault().ContractorName
                                     }).ToList();
                        list_comments.DataSource = rList;
                        list_comments.DataBind();
                   // }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Post_click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtComments.Text.Trim()))
                {
                    JobTargetCommentBAL.JobTargetCommentBAL_Add(new JobTargetComment()
                    {
                        JobTargetID = Convert.ToInt32(hTaskIDComments.Value),
                        Details = txtComments.Text.Trim(),
                        LoggedDate = DateTime.Now,
                        UserID = sessionKeys.UID

                    });
                    txtComments.Text = string.Empty;
                    BindListComments();
                    BindListView();
                    mdlCommentes.Show();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveTemplate_Click(object sender, EventArgs e)
        {

        }
    }
    }