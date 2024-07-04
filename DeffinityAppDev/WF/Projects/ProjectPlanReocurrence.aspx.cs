using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class ProjectPlanReocurrence : System.Web.UI.Page
{
    projectTaskDataContext projectDB = new projectTaskDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindCheckBox();
                txtStartDate.Text = string.Format("{0:d}", DateTime.Now);
               // Master.PageHead = "Project Management";
                ReLoadData();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    protected void ImgApply_Click(object sender, EventArgs e)
    {
        try
        {
        AddRecurrence();
        ReLoadData();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void AddRecurrence()
    {
        try
        {
            var exist = (from r in projectDB.ProjectPlan_Recurs
                         where r.ProjectReference == sessionKeys.Project
                         select r).ToList();
            if (exist != null)
            {
                if (exist.Count == 0)
                {
                    ProjectPlan_Recur entity = new ProjectPlan_Recur();
                    entity.StartTime = Convert.ToDateTime("01/01/1900");
                    entity.EndTime = Convert.ToDateTime("01/01/1900");
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

                    entity.TempDate = Convert.ToDateTime(GetNextDates());
                    entity.WeekDayName = days;
                    entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
                    entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
                    entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
                    entity.ProjectReference = sessionKeys.Project;
                    entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString());
                    projectDB.ProjectPlan_Recurs.InsertOnSubmit(entity);
                    projectDB.SubmitChanges();
                    lblRecurrMsg.Text = "Added successfully";
                    lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    ProjectPlan_Recur entity = projectDB.ProjectPlan_Recurs.Single(P => P.ProjectReference == sessionKeys.Project);
                    entity.StartTime = Convert.ToDateTime("01/01/1900");
                    entity.EndTime = Convert.ToDateTime("01/01/1900");
                    entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? "01/01/1900" : txtStartDate.Text);
                    entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text);
                    entity.EndAfter = Convert.ToInt32(string.IsNullOrEmpty(txtEndOfOcurrences.Text) ? "0" : txtEndOfOcurrences.Text);
                    entity.CompletedRecurr = 0;
                    string days = "";
                    foreach (ListItem chkDay in chkDays.Items)
                    {
                        if (chkDay.Selected)
                        {
                            days += chkDay.Value + ",";
                        }
                    }
                    entity.TempDate = Convert.ToDateTime(GetNextDates());
                    entity.WeekDayName = days;
                    entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
                    entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
                    entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
                    entity.ProjectReference = sessionKeys.Project;
                    entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString());
                    //projectDB.ProjectPlan_Recurs.InsertOnSubmit(entity);
                    projectDB.SubmitChanges();
                    lblRecurrMsg.Text = "Updated successfully";
                    lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
    }

    private string GetNextDates()
    {
        DateTime strDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
        DateTime tempDate = DateTime.Now;
        if (strDate <= DateTime.Now)
        {
            tempDate = DateTime.Now.AddDays(1);
        }
        else
        {
            tempDate = strDate;
        }
        //DateTime tempDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
        //if (rdPatternVal == "1")
        //{
        //    tempDate =tempDate.AddDays(1);
        //}
        //if (rdPatternVal == "2")
        //{
        //    int NoOfDays = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "1" : txtRecur.Text);
        //    tempDate =tempDate.AddDays(7 * NoOfDays);
        //}
        //if (rdPatternVal == "3")
        //{
        //    tempDate = tempDate.AddMonths(1);
        //}
        //if (rdPatternVal == "4")
        //{
        //    tempDate = tempDate.AddYears(1);
        //}
        return tempDate.ToShortDateString();
    }
    private void ReLoadData()
    {
        try
        {
            lblMsg.Visible = false;
        var entity=(from r in projectDB.ProjectPlan_Recurs
                       where r.ProjectReference==sessionKeys.Project
                       select r).ToList().FirstOrDefault();
        if (entity != null)
        {


            //txtStartTime.Text = string.Format("{0:t hh:mm}", entity.StartTime.ToShortTimeString());
            //txtEndTime.Text = string.Format("{0:t hh:mm}", entity.EndTime.ToShortTimeString());
            if (string.Format("{0:d}", entity.StartDate) == "01/01/1900")
            {
                txtStartDate.Text = "";
            }
            else
            {
                txtStartDate.Text = string.Format("{0:d}", entity.StartDate);
            }
            if (string.Format("{0:d}", entity.EndDate) == "01/01/1900")
            {
                txtEndDate.Text = "";
            }
            else
            {
                txtEndDate.Text = string.Format("{0:d}", entity.EndDate);
            }

            rdPattern.SelectedValue = entity.ReCurrencePattern.ToString();
           
            rdRangeOfRecurrence.SelectedValue = entity.ReCurrenceRange.ToString();
            //Uncheck all
            foreach (ListItem chk in chkDays.Items)
            {

                chk.Selected = false;

            }
            string[] days = entity.WeekDayName.Split(',');
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
            if (entity.RecurWeekOn != 0)
            {
                txtRecur.Text = entity.RecurWeekOn.ToString();
            }
            else
            {
                txtRecur.Text = string.Empty;
            }
            txtEndOfOcurrences.Text = entity.EndAfter.ToString();

        }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Enable section
    protected void chkProjectUpdate_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Visible = true;
        var isExist = (from r in projectDB.ProjectPlan_Recurs
                       where r.ProjectReference == sessionKeys.Project
                       select r).ToList();
        if (isExist != null)
        {
            if (isExist.Count() > 0)
            {
                lblRecurrMsg.Visible = false;
                EnableProjectEmail();
                BindCheckBox();

                lblMsg.Text = "Project updates have been successfully configured";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                //chkProjectUpdate.Checked = false;
                //lblMsg.Text = "Failed to enable,please apply reoccurrence";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                AddRecurrence();
                lblRecurrMsg.Visible = false;
                EnableProjectEmail();
                BindCheckBox();
                lblRecurrMsg.Visible = false;
               lblMsg.Text = "Project updates have been successfully configured";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
    }

    private void BindCheckBox()
    {
        try
        {
        var getProject = (from r in projectDB.Projects
                          where r.ProjectReference == sessionKeys.Project
                          select r).ToList().FirstOrDefault();
        if (getProject != null)
        {
            if (getProject.EnableEmail == true)
            {
                chkProjectUpdate.Checked = true;
            }
            else
            {
                chkProjectUpdate.Checked = false;
            }
        }
         }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void EnableProjectEmail()
    {
        try
        {
            projectTaskDataContext projectsUpdate = new projectTaskDataContext();
            Project update = projectsUpdate.Projects.Single(P => P.ProjectReference == sessionKeys.Project);
            update.EnableEmail = chkProjectUpdate.Checked;
            projectsUpdate.SubmitChanges();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #endregion



}
