using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProgrammeManagers;
using UserMgt.DAL;
using UserMgt.Entity;
public partial class controls_CustomerAlerts : System.Web.UI.UserControl
{
    PortfolioDataContext portfolioDB = new PortfolioDataContext();
    string type = "";
    int refID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Type"] != null)
                {
                    BindChecklistbox();
                    BindDefaultData();
                   // BindData();
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    private void BindDefaultData()
    {
        try
        {
            if (Request.QueryString["Type"] != null)
            {
                type = Request.QueryString["Type"].ToString();
                if (type == "Project")
                {
                    refID = sessionKeys.Project;
                }
                if (type == "Customer")
                {
                    refID = sessionKeys.PortfolioID;
                }
                if (type == "Vendor")
                {
                    refID = int.Parse(Request.QueryString["VendorID"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindGrid()
    {
        if (Request.QueryString["Type"] != null)
        {
            var IsExist = (from r in portfolioDB.CustomAlerts
                           where r.Refid == refID && r.CustomAlertType == type
                           select r).ToList();
            if (IsExist != null)
            {
                gridAlert.DataSource = IsExist;
                gridAlert.DataBind();
            }
        }

    }
    protected string lblResource(string Resource)
    {
        string res = "";
        if (Resource.Length > 0)
        {
            res = Resource.Substring(0, Resource.Length - 1);
        }


        return res;
    }
    private void BindChecklistbox()
    {
        try
        {
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetResourcesChecklist", new SqlParameter("@ProjectReference", QueryStringValues.Project)).Tables[0];
            //DataTable dt = new DataTable();
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetResourcesChecklist", new SqlParameter("@ProjectReference", QueryStringValues.Project)).Tables[0];
            DataTable dt = new DataTable();
            if (sessionKeys.Project != 0)
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo_ProjectPlan", new SqlParameter("@ProjectRefrence", QueryStringValues.Project)
                    , new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];

            }
            else
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ContractorName,ID from Contractors where SID not in (10) order by ContractorName ").Tables[0];

            }
            if (dt.Rows.Count > 0)
            {


                CheckBoxList2.DataSource = dt;
                CheckBoxList2.DataValueField = "ID";
                CheckBoxList2.DataTextField = "ContractorName";
                CheckBoxList2.DataBind();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ResourceIDs = string.Empty;
            BindDefaultData();
            if (Request.QueryString["Type"] != null)
            {
                //var IsExist = (from r in portfolioDB.CustomAlerts
                //               where r.Refid == refID && r.CustomAlertType == type
                //               select r).ToList();
                //if (IsExist != null)
                //{
                if (hdnID.Value == "0")
                {

                    CustomAlert customer = new CustomAlert();
                    customer.AddedBy = sessionKeys.UID;
                    customer.AddedTime = DateTime.Now;
                    customer.AlertDate = Convert.ToDateTime(string.IsNullOrEmpty(txtAlertDate.Text) ? DateTime.Now.ToShortDateString() : txtAlertDate.Text);
                    customer.AlertDescription = txtAlertDescription.Text;
                    customer.CustomAlertType = type;
                    customer.Refid = refID;
                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        if (CheckBoxList2.Items[i].Selected)
                        {
                            ResourceIDs = ResourceIDs + CheckBoxList2.Items[i].Value.ToString() + ",";
                        }
                    }
                    customer.DistributionList = ResourceIDs;
                    customer.Notes = txtNotes.Text;
                    customer.DueDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDueDate.Text) ? DateTime.Now.ToShortDateString() : txtDueDate.Text);
                    portfolioDB.CustomAlerts.InsertOnSubmit(customer);
                    portfolioDB.SubmitChanges();
                    BindGrid();
                    lblMessage.Text = "Successfully Added";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    CustomAlert update = portfolioDB.CustomAlerts.Single(P => P.AlertID == int.Parse(hdnID.Value));
                    // && P.Refid == refID);
                    update.AddedBy = sessionKeys.UID;
                    update.AddedTime = DateTime.Now;
                    update.AlertDate = Convert.ToDateTime(string.IsNullOrEmpty(txtAlertDate.Text) ? DateTime.Now.ToShortDateString() : txtAlertDate.Text);
                    update.AlertDescription = txtAlertDescription.Text;
                    //update.CustomAlertType = Request.QueryString["Type"].ToString();

                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        if (CheckBoxList2.Items[i].Selected)
                        {
                            ResourceIDs = ResourceIDs + CheckBoxList2.Items[i].Value.ToString() + ",";
                        }
                    }
                    update.DistributionList = ResourceIDs;
                    update.Notes = txtNotes.Text;
                    update.DueDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDueDate.Text) ? DateTime.Now.ToShortDateString() : txtDueDate.Text);
                    portfolioDB.SubmitChanges();
                    BindGrid();
                    lblMessage.Text = "Successfully updated";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }

                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void ClearField()
    {
        txtAlertDate.Text = "";
        txtAlertDescription.Text = "";
        txtDueDate.Text = "";
        txtNotes.Text = "";
        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
        {

            
                CheckBoxList2.Items[i].Selected = false;
            
        }
    }

    private void BindData(int alertID)
    {
        try
        {
            var IsExist = (from r in portfolioDB.CustomAlerts
                           where r.AlertID == alertID//r.Refid == refID && r.CustomAlertType == type
                           select r).ToList().FirstOrDefault();
            if (IsExist != null)
            {
                txtAlertDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), IsExist.AlertDate);
                txtAlertDescription.Text = IsExist.AlertDescription;
                txtDueDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), IsExist.DueDate);
                txtNotes.Text = IsExist.Notes;
                string[] ids = IsExist.DistributionList.Split(',');
                if (ids.Length > 0)
                {
                    for (int j = 0; j < ids.Length; j++)
                    {

                        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                        {

                            if (ids[j] == CheckBoxList2.Items[i].Value)
                            {
                                CheckBoxList2.Items[i].Selected = true;
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


    public string AlertType { get; set; }

    #region "Reccurence pop window"
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

                CustomAlertRecurr entity = new CustomAlertRecurr();
                entity.StartTime = Convert.ToDateTime("01/01/1900");
                entity.EndTime = Convert.ToDateTime("01/01/1900");
                entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
                entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
                entity.EndAfter = Convert.ToInt32(string.IsNullOrEmpty(txtEndOfOcurrences.Text) ? "0" : txtEndOfOcurrences.Text);
                string days = "";
                if (rdPattern.SelectedValue == "1")
                {
                    foreach (ListItem chkDay in chkDays.Items)
                    {
                        if (chkDay.Selected)
                        {
                            days += chkDay.Value + ",";
                        }
                    }
                }
                entity.WeekDayName = days;
                entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
                entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
                entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
                entity.AlertID = Convert.ToInt32(hdnAlertID.Value);
                InsertUpdateRecurr(entity);
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
    private static void InsertUpdateRecurr(CustomAlertRecurr CustomAlertRecurr)
    {
        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = new SqlCommand("Customalert_RecurInsertUpdate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartTime", CustomAlertRecurr.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", CustomAlertRecurr.EndTime);
                cmd.Parameters.AddWithValue("@RecurWeekOn", CustomAlertRecurr.RecurWeekOn);
                cmd.Parameters.AddWithValue("@WeekDayName", CustomAlertRecurr.WeekDayName);
                cmd.Parameters.AddWithValue("@StartDate", CustomAlertRecurr.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", CustomAlertRecurr.EndDate);
                cmd.Parameters.AddWithValue("@EndAfter", CustomAlertRecurr.EndAfter);
                cmd.Parameters.AddWithValue("@ReCurrencePattern", CustomAlertRecurr.ReCurrencePattern);
                cmd.Parameters.AddWithValue("@ReCurrenceRange", CustomAlertRecurr.ReCurrenceRange);
                cmd.Parameters.AddWithValue("@AlertID", CustomAlertRecurr.AlertID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    private void GetRecurrenceData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select * from CustomAlert_Recur where AlertID=@AlertID",
                new SqlParameter("@AlertID",int.Parse(hdnAlertID.Value))).Tables[0];
            if (dt.Rows.Count != 0)
            {
                //HealthCheckRecurr entity = HealthCheckListItemsHelper.SelectById(Convert.ToInt32(Request.QueryString["HealthCheckID"]));
                //txtStartTime.Text = string.Format("{0:t hh:mm}", entity.StartTime.ToShortTimeString());
                //txtEndTime.Text = string.Format("{0:t hh:mm}", entity.EndTime.ToShortTimeString());
                if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), dt.Rows[0]["StartDate"]) == "01/01/1900")
                {
                    txtStartDate.Text = "";
                }
                else
                {
                    txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dt.Rows[0]["StartDate"]);
                }
                if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), dt.Rows[0]["EndDate"]) == "01/01/1900")
                {
                    txtEndDate.Text = "";
                }
                else
                {
                    txtEndDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dt.Rows[0]["EndDate"]);
                }

                rdPattern.SelectedValue = dt.Rows[0]["ReCurrencePattern"].ToString();
                //if (rdPattern.SelectedValue != "1")
                //{
                //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "valid",
                //    //    "<script language='javascript'> function disableItem() { var chkDays = document.getElementById('<%=chkDays.ClientID%>');chkDays.disabled = true;}</script>");

                //    chkDays.Enabled = false;
                //}
                rdRangeOfRecurrence.SelectedValue = dt.Rows[0]["ReCurrenceRange"].ToString();
                if (rdPattern.SelectedValue == "1")
                {
                    string[] days = dt.Rows[0]["WeekDayName"].ToString().Split(',');
                    foreach (ListItem chk in chkDays.Items)
                    {
                        if (days.Length > 0)
                        {
                            for (int i = 0; i < days.Length; i++)
                            {
                                if (days[i] == chk.Value)
                                {
                                    chk.Selected = true;
                                }
                            }

                        }
                    }
                }
                else
                {
                    //chkDays.Enabled = false;
                }
                if (dt.Rows[0]["RecurWeekOn"].ToString() != "0")
                {
                    txtRecur.Text = dt.Rows[0]["RecurWeekOn"].ToString();
                }
                else
                {
                    txtRecur.Text = string.Empty;
                }
                txtEndOfOcurrences.Text = dt.Rows[0]["EndAfter"].ToString();

                mpopRecurrence.Show();
            }

            else
            {
                rdPattern.SelectedValue = "1";
                rdRangeOfRecurrence.SelectedValue = "1";
                foreach (ListItem chk in chkDays.Items)
                {
                   
                                chk.Selected = true;
                       
                }
                txtEndOfOcurrences.Text = "";
                txtRecur.Text = string.Empty;
                txtEndDate.Text = "";
                txtStartDate.Text = "";
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

    protected string GetResources(string ResourcesID)
    {
        string val = "";
        UserDataContext users = new UserDataContext();
        var contractors = (from r in users.Contractors
                           select r).ToList();
        if (contractors != null)
        {
           
            string[] ids = ResourcesID.Split(',');
            if (ids.Length > 0)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    foreach ( UserMgt.Entity.Contractor user in contractors)
                    {
                        if (ids[j] == user.ID.ToString())
                        {
                            val = val + user.ContractorName+ ",";
                        }
                    }

                }
            }
        }
        return val.TrimEnd(',');
    }

    #endregion



    #region "Permission Code Here"
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {
                    lblMessage.Text = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                   lblMessage.Text= "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }

            }
        }
    }
    private void Disable()
    {

        imgSave.Enabled = false;

    }
    #endregion 
    protected void imgAddAlert_Click(object sender, EventArgs e)
    {
        hdnID.Value = "0";
        ClearField();
        mpopAlert.Show();
    }
    protected void gridAlert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reccurrence")
        {
            lblMessage.Text = "";
            hdnAlertID.Value = e.CommandArgument.ToString();
            GetRecurrenceData();
            mpopRecurrence.Show();
        }
        if (e.CommandName == "AlertAdd")
        {
            ClearField();
            hdnID.Value = e.CommandArgument.ToString();
            BindData(int.Parse(e.CommandArgument.ToString()));
            mpopAlert.Show();
        }
        
    }
}
#region Custom alert Recurences

public class CustomAlertRecurr
{
    public int ID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int RecurWeekOn { get; set; }
    public String WeekDayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EndAfter { get; set; }
    public int ReCurrencePattern { get; set; }
    public int ReCurrenceRange { get; set; }
    public int AlertID { get; set; }
}
#endregion
