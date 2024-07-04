using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
public partial class ProjectCheckPoints : System.Web.UI.Page
{
    projectTaskDataContext projectDB = new projectTaskDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProgramme();
            GetData();
        }
    }
    private void BindProgramme()
    {
        try
        {
            ProgrammeDataContext ProgrammeContext = new ProgrammeDataContext();
            var Owner = from c in ProgrammeContext.OperationsOwners
                        where c.Level == 1
                        orderby c.OperationsOwners
                        select c;
            ddlProgramme.DataSource = Owner;
            ddlProgramme.DataTextField = "OperationsOwners";
            ddlProgramme.DataValueField = "ID";
            ddlProgramme.DataBind();
            ddlProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
            if (ddlProgramme.Items.Count != 0)
            {
                ddlProgramme.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ImgApply_Click(object sender, EventArgs e)
    {
        AddRecurrence();
       // InsertSubProgrammes();
        GetData();
    }
    private void AddRecurrence()
    {
        var exist = projectDB.ExecuteQuery<ProgrammeManagement_Recur>(Query()).ToList();
         //if (exist != null)
         //{
             if(exist.Count==0)
             {
             ProgrammeManagement_Recur entity = new ProgrammeManagement_Recur();
             entity.ProgrammeID = int.Parse(ddlProgramme.SelectedValue);
                 entity.SubprogrammeID = int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue)?"0":ddlSubprogramme.SelectedValue);
             entity.StartTime = Convert.ToDateTime("01/01/1900");
             entity.EndTime = Convert.ToDateTime("01/01/1900");
             entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
             entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
             entity.ProjectReference = sessionKeys.Project;
             entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString());
             projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
             projectDB.SubmitChanges();
             Update_ProjectsRecurance();
             lblRecurrMsg.Text = "Successfully Added";
             lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
             }
             else
             {
                 var getID = (from r in exist select r).ToList().FirstOrDefault();
                 ProgrammeManagement_Recur entity =
                     projectDB.ProgrammeManagement_Recurs.Single(P => P.ID ==getID.ID);
             //entity.ProgrammeID = int.Parse(ddlProgramme.SelectedValue);
             //entity.SubprogrammeID = int.Parse(ddlSubprogramme.SelectedValue);
             entity.StartTime = Convert.ToDateTime("01/01/1900");
             entity.EndTime = Convert.ToDateTime("01/01/1900");
             entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
             entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
             entity.ProjectReference = sessionKeys.Project;
             entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
             //projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
             projectDB.SubmitChanges();
             Update_ProjectsRecurance();
             lblRecurrMsg.Text = "Successfully Updated";
             lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
             }
        // }
    }

  

    #region "Query"
    private void GetData()
    {
        var entity = projectDB.ExecuteQuery<ProgrammeManagement_Recur>(Query()).ToList().FirstOrDefault();
        if (entity != null)
        {

            if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.StartDate) == "01/01/1900")
            {
                txtStartDate.Text = "";
            }
            else
            {
                txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.StartDate);
            }
            if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.EndDate) == "01/01/1900")
            {
                txtEndDate.Text = "";
            }
            else
            {
                txtEndDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.EndDate);
            }

            rdPattern.SelectedValue = entity.ReCurrencePattern.ToString();
            rdRangeOfRecurrence.SelectedValue = entity.ReCurrenceRange.ToString();

            string[] days = entity.WeekDayName.Split(',');

            //Uncheck all
            foreach (ListItem chk in chkDays.Items)
            {
                chk.Selected = false;
            }


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
        else
        {
            ClearFields();
        }
    }
    private string Query()
    {
        string sql = "";
        sql = "select * from ProgrammeManagement_Recur where ID<>0 ";
        if (int.Parse(ddlProgramme.SelectedValue)!=0)
        {
            sql += " and  ProgrammeID=" + ddlProgramme.SelectedValue;
        }
        if (ddlSubprogramme.SelectedValue!="")
        {
            sql += " and  SubprogrammeID=" + ddlSubprogramme.SelectedValue;
        }


        return sql;
    }
    private string ProjectTable_Query()
    {
        string sql = "";
        sql = "select ID,ProjectReference from projects where ProjectReference<>0 ";
        if (int.Parse(ddlProgramme.SelectedValue) != 0)
        {
            sql += " and  OwnerGroupID=" + ddlProgramme.SelectedValue;
        }
        if (ddlSubprogramme.SelectedValue != "")
        {
            sql += " and  SubProgramme=" + ddlSubprogramme.SelectedValue;
        }


        return sql;
    }
    #endregion
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        GetData();
        lblRecurrMsg.Visible = false;
    }


    private void Update_ProjectsRecurance()
    {
         var entity = projectDB.ExecuteQuery<ProgrammeManagement_Recur>(Query()).ToList().FirstOrDefault();
         if (entity != null)
         {
             var projectList = projectDB.ExecuteQuery<Project>(ProjectTable_Query()).ToList();
             if (projectList != null)
             {
                 foreach (Project Projects in projectList)
                 {
                     AddRecurrence(Projects.ProjectReference);
                 }
             }
         }
        //ProgrammeDataContext programme = new ProgrammeDataContext();
        //var getSubProgramme = (from r in programme.OperationsOwners
        //                       where r.MasterProgramme == Convert.ToInt32(ddlProgramme.SelectedValue)
        //                       && r.Level == 2
        //                       select r).ToList();

        //var projectList1 = (from r in projectDB.Projects
        //                   where r.OwnerGroupID == Convert.ToInt32(string.IsNullOrEmpty(ddlProgramme.SelectedValue)?"0":ddlProgramme.SelectedValue)
        //                   && r.SubProgramme ==0
        //                   select r).ToList();
        //foreach (Project item in projectList1)
        //{
        //    AddRecurrence(item.ProjectReference);
        //}
        ////AddRecurrence_Programme(Convert.ToInt32(ddlProgramme.SelectedValue));

        //if (getSubProgramme != null)
        //{
        //    foreach (OperationsOwner ProgrammeID in getSubProgramme)
        //    {
        //        var projectList = (from r in projectDB.Projects
        //                           where r.OwnerGroupID == Convert.ToInt32(ddlProgramme.SelectedValue)
        //                           && r.SubProgramme == ProgrammeID.ID
        //                           select r).ToList();
        //        foreach (Project item in projectList)
        //        {
        //            AddRecurrence(item.ProjectReference);
        //        }

        //    }


        //}
    }
    private void AddRecurrence(int ProjectReference)
    {
        lblRecurrMsg.Visible = true;
        var exist = (from r in projectDB.ProjectPlan_Recurs
                     where r.ProjectReference == ProjectReference
                     select r).ToList();
        if (exist != null)
        {
            if (exist.Count == 0)
            {
                ProjectPlan_Recur entity = new ProjectPlan_Recur();
                entity.StartTime = Convert.ToDateTime("01/01/1900");
                entity.EndTime = Convert.ToDateTime("01/01/1900");
                entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
                entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
                entity.ProjectReference = ProjectReference;
                entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
                projectDB.ProjectPlan_Recurs.InsertOnSubmit(entity);
                projectDB.SubmitChanges();
                lblRecurrMsg.Text = "Successfully Added";
                lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ProjectPlan_Recur entity = projectDB.ProjectPlan_Recurs.Single(P => P.ProjectReference == ProjectReference);
                entity.StartTime = Convert.ToDateTime("01/01/1900");
                entity.EndTime = Convert.ToDateTime("01/01/1900");
                entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
                entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
                entity.ProjectReference = ProjectReference;
                entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
                //projectDB.ProjectPlan_Recurs.InsertOnSubmit(entity);
                projectDB.SubmitChanges();
                lblRecurrMsg.Text = "Successfully Updated";
                lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
            }
        }
    }

    private void ClearFields()
    {
        foreach (ListItem chk in chkDays.Items)
        {
            chk.Selected = true;
        }
        txtEndDate.Text = "";
        txtEndOfOcurrences.Text = "";
        txtRecur.Text = "";
        txtStartDate.Text = "";
        rdPattern.SelectedIndex = 0;
        rdRangeOfRecurrence.SelectedIndex = 0;
       
    }
    private void InsertSubProgrammes()
    {
        try
        {
            //FETCH  SUBPROGRAMMES ID
            ProgrammeDataContext programme = new ProgrammeDataContext();
            var getSubProgramme = (from r in programme.OperationsOwners
                                   where r.MasterProgramme == Convert.ToInt32(ddlProgramme.SelectedValue)
                                   && r.Level == 2
                                   select r).ToList();
            AddRecurrence_Programme(Convert.ToInt32(ddlProgramme.SelectedValue));

            if (getSubProgramme != null)
            {
                foreach (ProgrammeMgt.Entity.OperationsOwner ProgrammeID in getSubProgramme)
                {

                    AddRecurrence_Programme(Convert.ToInt32(ddlProgramme.SelectedValue), ProgrammeID.ID);

                }
               

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void AddRecurrence_Programme(int programme)
    {
        lblRecurrMsg.Visible = true;
        var exist = (from r in projectDB.ProgrammeManagement_Recurs
                     where r.ProgrammeID == programme
                     && r.SubprogrammeID==0
                     select r).ToList();
        //if (exist != null)
        //{
        if (exist.Count == 0)
        {
            ProgrammeManagement_Recur entity = new ProgrammeManagement_Recur();
            entity.ProgrammeID = programme;
            entity.SubprogrammeID = 0;// int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue);
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
            entity.WeekDayName = days;
            entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
            entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
            entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
            entity.ProjectReference = sessionKeys.Project;
            entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
            projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
            projectDB.SubmitChanges();
            Update_ProjectsRecurance();
        }
        else
        {
            var getID = (from r in exist select r).ToList().FirstOrDefault();
            ProgrammeManagement_Recur entity =
                projectDB.ProgrammeManagement_Recurs.Single(P => P.ID == getID.ID);
            //entity.ProgrammeID = int.Parse(ddlProgramme.SelectedValue);
            //entity.SubprogrammeID = int.Parse(ddlSubprogramme.SelectedValue);
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
            entity.WeekDayName = days;
            entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
            entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
            entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
            entity.ProjectReference = sessionKeys.Project;
            entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
            //projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
            projectDB.SubmitChanges();
            Update_ProjectsRecurance();
        }
        // }
    }
    private void AddRecurrence_Programme(int programme, int subprogramme)
    {
        lblRecurrMsg.Visible = true;
        var exist = (from r in projectDB.ProgrammeManagement_Recurs
                     where r.ProgrammeID == programme
                     && r.SubprogrammeID == subprogramme
                     select r).ToList();
        //if (exist != null)
        //{
        if (exist.Count == 0)
        {
            ProgrammeManagement_Recur entity = new ProgrammeManagement_Recur();
            entity.ProgrammeID = programme;
            entity.SubprogrammeID = subprogramme;// int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue);
            entity.StartTime = Convert.ToDateTime("01/01/1900");
            entity.EndTime = Convert.ToDateTime("01/01/1900");
            entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
            entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
            entity.ProjectReference = sessionKeys.Project;
            entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString());
            projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
            projectDB.SubmitChanges();
            Update_ProjectsRecurance();
            lblRecurrMsg.Text = "Successfully Added";
            lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            var getID = (from r in exist select r).ToList().FirstOrDefault();
            ProgrammeManagement_Recur entity =
                projectDB.ProgrammeManagement_Recurs.Single(P => P.ID == getID.ID);
            //entity.ProgrammeID = int.Parse(ddlProgramme.SelectedValue);
            //entity.SubprogrammeID = int.Parse(ddlSubprogramme.SelectedValue);
            entity.StartTime = Convert.ToDateTime("01/01/1900");
            entity.EndTime = Convert.ToDateTime("01/01/1900");
            entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToShortDateString() : txtStartDate.Text);
            entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? DateTime.Now.ToShortDateString() : txtEndDate.Text);
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
            entity.ProjectReference = sessionKeys.Project;
            entity.lastUpdated = Convert.ToDateTime(string.IsNullOrEmpty(DateTime.Now.ToShortDateString()) ? "01/01/1900" : DateTime.Now.ToShortDateString());
            //projectDB.ProgrammeManagement_Recurs.InsertOnSubmit(entity);
            projectDB.SubmitChanges();
            Update_ProjectsRecurance();
            lblRecurrMsg.Text = "Successfully Updated";
            lblRecurrMsg.ForeColor = System.Drawing.Color.Green;
        }
        // }
    }
}
