using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Deffinity;
using System.IO;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using Deffinity.ProgrammeManagers;

public partial class ProjectBenfitBudget : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            
           // Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
            if (!IsPostBack)
            {
                grdBenefitItems.Visible = true;
                lblMsg.Visible = false;
                BindType();
                BindGrid();
                ShowInDashBoard();
                CheckUserRole();
               // BindItems();
                if (sessionKeys.SID == 1)
                {
                    imgDelete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void BindDefaultItems(int ID)
    {
        string rptCycle = "";
        ProjectBenefitItem pb = new ProjectBenefitItem();
        List<ProjectBenefitItem> list = new List<ProjectBenefitItem>();
        projectTaskDataContext task = new projectTaskDataContext();
        var list1 = (from r in task.Projects
                     where r.ProjectReference == QueryStringValues.Project
                     select r).ToList().FirstOrDefault();


        var listBenefit = (from r in task.ProjectBenefits
                           where r.ID == ID
                           select r).ToList().FirstOrDefault();
        if (listBenefit != null)
        {
            rptCycle = listBenefit.ReportCycle;
        }

        DateTime date1 = Convert.ToDateTime(list1.StartDate.ToString());
        DateTime date2 = Convert.ToDateTime(list1.ProjectEndDate.ToString());
        //TimeSpan ts = date2.Subtract(date1);
       ArrayList ary = Deffinity.Utility.MonthlyDiff(date1, date2);
       ArrayList qtr = Deffinity.Utility.QtrlyDiff(date1, date2);
        int M=Deffinity.Utility.MonthDifference(date1, date2);
        if (rptCycle == "Monthly")
        {
            if (ary.Count== 0)
            {
                pb = new ProjectBenefitItem();
                pb.Planned = 0;
                pb.Actual = 0;
                pb.Period = date1;
                pb.Achievement = 0.0;
                pb.CumulativeTotal = 0.0;
                pb.TargetPeriod = 0.0;
                pb.ID = 0;
                list.Add(pb);
            }
            else
            {
                for (int i = 0; i <ary.Count; i++)
                {
                    pb = new ProjectBenefitItem();
                    pb.Planned = 0;
                    pb.Actual = 0;
                    pb.Period =Convert.ToDateTime(ary[i]);
                    pb.Achievement = 0.0;
                    pb.CumulativeTotal = 0.0;
                    pb.TargetPeriod = 0.0;
                    pb.ID = 0;
                    list.Add(pb);
                }
            }
        }
        else
        {
            if (qtr.Count == 0)
            {
                pb = new ProjectBenefitItem();
                pb.Planned = 0;
                pb.Actual = 0;
                pb.Period = date1;
                pb.Achievement = 0.0;
                pb.CumulativeTotal = 0.0;
                pb.TargetPeriod = 0.0;
                pb.ID = 0;
                list.Add(pb);
            }
            else
            {
                for (int i = 0; i < qtr.Count; i++)
                {
                    pb = new ProjectBenefitItem();
                    pb.Planned = 0;
                    pb.Actual = 0;
                    pb.Period = Convert.ToDateTime(qtr[i]);
                    pb.Achievement = 0.0;
                    pb.CumulativeTotal = 0.0;
                    pb.TargetPeriod = 0.0;
                    pb.ID = 0;
                    list.Add(pb);
                }
            }
        }
        grdBenefitItems.DataSource = list;
        grdBenefitItems.DataBind();
    }

    private void BindItems(int ID)
    {
        string rptCycle = "";
        ProjectBenefitItem pb = new ProjectBenefitItem();
        ProjectBenefitItem pb1 = new ProjectBenefitItem();
        projectTaskDataContext task = new projectTaskDataContext();
        List<ProjectBenefitItem> list = new List<ProjectBenefitItem>();
        List<ProjectBenefitItem> list2 = new List<ProjectBenefitItem>();
        List<ProjectBenefitItem> list3 = new List<ProjectBenefitItem>();
        var list1 = (from r in task.Projects
                     where r.ProjectReference == QueryStringValues.Project
                     select r).ToList().FirstOrDefault();
        var listItems = (from r in task.ProjectBenefitItems
                         where r.BenefitID==ID
                         select r).ToList();

        
        var listBenefit = (from r in task.ProjectBenefits
                           where r.ID == ID
                           select r).ToList().FirstOrDefault();
        if (listBenefit != null)
        {
            rptCycle = listBenefit.ReportCycle;
        }
       
        DateTime date1 = Convert.ToDateTime(list1.StartDate.ToString());
        DateTime date2 = Convert.ToDateTime(list1.ProjectEndDate.ToString());
        //TimeSpan ts = date2.Subtract(date1);
        ArrayList qtr = Deffinity.Utility.QtrlyDiff(date1, date2);
        ArrayList ary = Deffinity.Utility.MonthlyDiff(date1, date2);
        int M=Deffinity.Utility.MonthDifference(date1, date2);
        if (rptCycle == "Monthly")
        {
            if (ary.Count != 0)
            {
                for (int i = 0; i < ary.Count; i++)
                {
                    pb = new ProjectBenefitItem();
                    pb.Planned = 0;
                    pb.Actual = 0;
                    pb.Period = Convert.ToDateTime(ary[i]);
                    pb.Achievement = 0.0;
                    pb.CumulativeTotal = 0.0;
                    pb.TargetPeriod = 0.0;
                    pb.ID = 0;
                    pb.Notes = string.Empty;
                    list.Add(pb);
                }
            }
        }
        else
        {
            if (qtr.Count != 0)
            {
                for (int i = 0; i < qtr.Count; i++)
                {
                    pb = new ProjectBenefitItem();
                    pb.Planned = 0;
                    pb.Actual = 0;
                    pb.Period = Convert.ToDateTime(qtr[i]);
                    pb.Achievement = 0.0;
                    pb.CumulativeTotal = 0.0;
                    pb.TargetPeriod = 0.0;
                    pb.ID = 0;
                    pb.Notes = string.Empty;
                    list.Add(pb);
                }
            }
        }



        double ct = 0.0;
        double actual = 0.0;
        foreach (var c in list)
        {


            var list4 = (from r in listItems
                         where r.Period == c.Period
                         select r).ToList().FirstOrDefault();

            if (list4 == null)
            {
                actual = 0.0;
            }
            else
            {
                actual = Convert.ToDouble(list4.Actual.Value);
            }
            var list5 = (from r in listItems
                         where r.Period == c.Period
                         select r).ToList();

            ct = ct + actual;

            if (list5.Count()!=0)
                {
                    pb1 = new ProjectBenefitItem();
                    pb1.Planned = list4.Planned;
                    pb1.Actual = list4.Actual;
                    pb1.Period = list4.Period;
                    pb1.Achievement = list4.Achievement;
                    pb1.CumulativeTotal = ct; //list4.Achievement + c.CumulativeTotal;
                    pb1.TargetPeriod = list4.TargetPeriod;
                    pb1.ID = list4.ID;
                    pb1.Notes = list4.Notes;
                    list3.Add(pb1);
                }
                else
                {
                    pb1 = new ProjectBenefitItem();
                    pb1.Planned =c.Planned;
                    pb1.Actual = c.Actual;
                    pb1.Period = c.Period;
                    pb1.Achievement = 0.0;
                    pb1.CumulativeTotal = ct;
                    pb1.TargetPeriod =0.0;
                    pb1.ID = 0;
                    pb1.Notes = c.Notes;
                    list3.Add(pb1);

                }
            }
            

        
       


        grdBenefitItems.DataSource = list3.ToList().Distinct();
        grdBenefitItems.DataBind();
    }

    
    protected void imgApply_Click(object sender, EventArgs e)
    {
        try
        {

            projectTaskDataContext project = new projectTaskDataContext();

            var isExist = from r in project.ProjectBenefits
                          where r.BenfitID == int.Parse(ddlBenefitType.SelectedValue) && r.Projectreference == QueryStringValues.Project
                          select r;
            if (isExist.Count() == 0)
            {

                ProjectBenefit insert = new ProjectBenefit();
                insert.BenfitID = int.Parse(ddlBenefitType.SelectedValue);
                insert.Description = ddlBenefitType.SelectedItem.ToString();
                insert.Projectreference = QueryStringValues.Project;
                insert.ReportCycle = ddlRpeortingCycle.SelectedValue;
                insert.Target = Convert.ToDouble(string.IsNullOrEmpty(txtTarget.Text) ? "0" : txtTarget.Text.Trim());
                project.ProjectBenefits.InsertOnSubmit(insert);
                project.SubmitChanges();
               
            }
            BindGrid();
            grdBenefitItems.Visible = false;
            lblMsg.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region BindGrids and dropdown

    private void BindGrid()
    {
        try
        {
            projectTaskDataContext project = new projectTaskDataContext();
            var projectBenfits = (from r in project.ProjectBenefits
                                  where r.Projectreference == QueryStringValues.Project
                                  select r).ToList();
            if (projectBenfits != null)
            {
                grdBenefitBudget.DataSource = projectBenfits;
                grdBenefitBudget.DataBind();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    private void BindType()
    {
        try
        {
            projectTaskDataContext type = new projectTaskDataContext();

            var types = from r in type.ProjectBenefitTypes
                        orderby r.Description
                        select r;
            if (types != null)
            {
                ddlBenefitType.DataSource = types;
                ddlBenefitType.DataTextField = "Description";
                ddlBenefitType.DataValueField = "ID";
                ddlBenefitType.DataBind();
            }
            ddlBenefitType.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }






    #endregion
    protected void grdBenefitBudget_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update1")
            {
                projectTaskDataContext project = new projectTaskDataContext();
                lblMsg.Visible = false;
                decimal tval = 0;
                int targetcount = (from r in project.ProjectBenefitItems
                                   where r.BenefitID == int.Parse(e.CommandArgument.ToString())
                                   select r.Planned).Count();

                if(targetcount >0)
                tval=(from r in project.ProjectBenefitItems
                             where r.BenefitID==int.Parse(e.CommandArgument.ToString())
                                 select r.Planned).Sum().Value;

                int i = grdBenefitBudget.EditIndex;
                GridViewRow row = grdBenefitBudget.Rows[i];

                TextBox txtTarget = (TextBox)row.FindControl("txtEditTarget");
                if (Convert.ToDecimal(txtTarget.Text) >= tval)
                {
                    ProjectBenefit pv = project.ProjectBenefits.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                    pv.Target = Convert.ToDouble(txtTarget.Text.Trim());
                    project.SubmitChanges();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Enter target value greater then total planned value";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                BindGrid();
            }
            if (e.CommandName == "Delete")
            {
                projectTaskDataContext project = new projectTaskDataContext();
                ProjectBenefit pv = project.ProjectBenefits.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                project.ProjectBenefits.DeleteOnSubmit(pv);
                project.SubmitChanges();
                BindGrid();
                projectTaskDataContext task = new projectTaskDataContext();
                var listItems = (from r in task.ProjectBenefitItems
                                 where r.BenefitID == int.Parse(e.CommandArgument.ToString())
                                 select r).ToList();
                int cnt = listItems.Count();
                if (cnt != 0)
                {
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "delete projectbenefititems where BenefitID=@ID",
                        new SqlParameter("@ID", int.Parse(e.CommandArgument.ToString())));
            //        List<ProjectBenefitItem> users = (List<ProjectBenefitItem>)from u in project.ProjectBenefitItems

            //                                                                   where u.BenefitID ==int.Parse(e.CommandArgument.ToString())

            //            select u;

            //        project.ProjectBenefitItems.DeleteAllOnSubmit(users);

            //project.SubmitChanges();
                    //ProjectBenefitItem pvi = project.ProjectBenefitItems.Single(P => P.BenefitID == int.Parse(e.CommandArgument.ToString()));
                    //project.ProjectBenefitItems.DeleteOnSubmit(pvi);
                    //project.SubmitChanges();
                }
                lblMsg.Visible = false;
                grdBenefitItems.Visible = false;
                Reset();

            }

            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void UpdateSupplyLed(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        grdBenefitItems.Visible = true;
        CheckBox chkSelect = sender as CheckBox;

        bool sample = chkSelect.Checked;

        GridViewRow row = chkSelect.NamingContainer as GridViewRow;
        Label lblBenefitID = (Label)row.FindControl("lblBenefitID");

         int id = Int32.Parse(lblBenefitID.Text);
        
        projectTaskDataContext task = new projectTaskDataContext();
        var listItems = (from r in task.ProjectBenefitItems
                         where r.BenefitID==id
                         select r).ToList();
        int cnt = listItems.Count();
        if (chkSelect.Checked == true)
        {
            hdnID.Value = id.ToString();
            if (cnt == 0)
            {
                BindDefaultItems(int.Parse(lblBenefitID.Text));
            }
            else
            {
                BindItems(int.Parse(lblBenefitID.Text));
            }
            //BindInvoiceJournal(int.Parse(lblValutionID.Text));
        }
    }
    protected void grdBenfitBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdBenefitBudget.EditIndex = -1;
        BindGrid();
    }
   
    //protected void imgAdd_Click(object sender, EventArgs e)
    //{
    //    //modelPopupAddSoftware.Show();
    //}
    protected void btnAddNenefitType_Click(object sender, EventArgs e)
    {
        try
        {

            projectTaskDataContext project = new projectTaskDataContext();

            var types = (from r in project.ProjectBenefitTypes
                         where r.Description == txtAddBenefit.Text
                         select r).ToList();
            if (types.Count() == 0)
            {
                lblError.Visible = false;

                ProjectBenefitType insert = new ProjectBenefitType();
                insert.Description = txtAddBenefit.Text;
                project.ProjectBenefitTypes.InsertOnSubmit(insert);
                project.SubmitChanges();
                BindType();
                ddlBenefitType.SelectedValue = insert.ID.ToString();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Entered Type Exist";
                modelPopupAddSoftware.Show();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        } 
    }


    protected void grdBenefitBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdBenefitBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string strScript = "uncheckOthers(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).ClientID + ");";
                ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).Attributes.Add("onclick", strScript);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }    
    }
   
    protected void grdBenefitItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            lblError.Visible = false;
            projectTaskDataContext project = new projectTaskDataContext();
            int CountRow = grdBenefitItems.Rows.Count;
            var targetVal = (from r in project.ProjectBenefits
                             where r.ID == int.Parse(hdnID.Value) && r.Projectreference == QueryStringValues.Project
                             select r).ToList().FirstOrDefault();
            double mainTarget = 0;
            if (targetVal != null)
            {
                mainTarget = Convert.ToDouble(targetVal.Target);
            }

            double totalPlanned = 0;
            double totalActual = 0;
            for (int j = 0; j < CountRow; j++)
            {

                GridViewRow Row1 = grdBenefitItems.Rows[j];
                //txtPlannedDate txtDateRaised txtTargetPeriod txtAchievement Cumulative Total
                Label lblID1 = (Label)Row1.FindControl("lblID");
                Label lblPeriod1 = (Label)Row1.FindControl("lblPeriod");
                TextBox txtPlannedDate1 = (TextBox)Row1.FindControl("txtPlannedDate");
                TextBox txtDateRaised1 = (TextBox)Row1.FindControl("txtDateRaised");
                totalPlanned = totalPlanned + Convert.ToDouble(string.IsNullOrEmpty(txtPlannedDate1.Text.Trim()) ? "0" : txtPlannedDate1.Text.Trim());
                totalActual = totalActual + Convert.ToDouble(string.IsNullOrEmpty(txtDateRaised1.Text.Trim()) ? "0" : txtDateRaised1.Text.Trim());
            }

            if (totalPlanned <= mainTarget)
            {
                for (int i = 0; i < CountRow; i++)
                {

                    GridViewRow Row = grdBenefitItems.Rows[i];
                    //txtPlannedDate txtDateRaised txtTargetPeriod txtAchievement Cumulative Total
                    Label lblID = (Label)Row.FindControl("lblID");
                    Label lblPeriod = (Label)Row.FindControl("lblPeriod");
                    TextBox txtPlannedDate = (TextBox)Row.FindControl("txtPlannedDate");
                    TextBox txtDateRaised = (TextBox)Row.FindControl("txtDateRaised");
                    TextBox txtnotes = (TextBox)Row.FindControl("txtnotes");
                    // TextBox txtAchievement = (TextBox)Row.FindControl("txtAchievement");
                    ProjectBenefitItem insert = new ProjectBenefitItem();
                    if (int.Parse(lblID.Text) == 0)
                    {

                        insert.Achievement = Convert.ToDouble("0");
                        insert.Actual = Convert.ToDecimal(string.IsNullOrEmpty(txtDateRaised.Text.Trim()) ? "0" : txtDateRaised.Text.Trim());
                        insert.BenefitID = int.Parse(hdnID.Value);
                        insert.CumulativeTotal = 0.0;
                        insert.Period = Convert.ToDateTime(lblPeriod.Text);
                        insert.Planned = Convert.ToDecimal(string.IsNullOrEmpty(txtPlannedDate.Text.Trim()) ? "0" : txtPlannedDate.Text.Trim());
                        insert.TargetPeriod = 0.0;// Convert.ToDouble(txtTargetPeriod.Text.Trim());
                        insert.Notes = txtnotes.Text.Trim();
                        project.ProjectBenefitItems.InsertOnSubmit(insert);
                        project.SubmitChanges();
                    }

                    else
                    {
                        ProjectBenefitItem Update =
                           project.ProjectBenefitItems.Single(P => P.ID == int.Parse(lblID.Text));

                        Update.Planned = Convert.ToDecimal(string.IsNullOrEmpty(txtPlannedDate.Text.Trim()) ? "0" : txtPlannedDate.Text.Trim());
                        //Update.TargetPeriod = Convert.ToDouble(txtTargetPeriod.Text.Trim());
                        //Update.BenefitID = int.Parse(hdnID.Value);
                        // Update.Achievement = Convert.ToDouble(txtAchievement.Text.Trim());
                        Update.Actual = Convert.ToDecimal(string.IsNullOrEmpty(txtDateRaised.Text.Trim()) ? "0" : txtDateRaised.Text.Trim());
                        Update.Notes = txtnotes.Text.Trim();
                        project.SubmitChanges();
                    }
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Total planned value is more then target"; 
            }
            BindItems(int.Parse(hdnID.Value));
        }
    }
    protected void grdBenefitItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdBenefitItems.EditIndex = -1;
        //BindItems();
    }

    private void Reset()
    {
        ddlBenefitType.SelectedValue ="0";
        txtTarget.Text = "";

    }
    protected void imgDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBenefitType.SelectedValue != "0")
            {
                projectTaskDataContext project = new projectTaskDataContext();
                ProjectBenefitType pv = project.ProjectBenefitTypes.Single(P => P.ID == int.Parse(ddlBenefitType.SelectedValue));
                project.ProjectBenefitTypes.DeleteOnSubmit(pv);
                project.SubmitChanges();
            }
            BindType();
            lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdBenefitBudget_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdBenefitBudget.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void grdBenefitBudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBenefitBudget.EditIndex = -1;
        BindGrid();
    }
    protected void grdBenefitBudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdBenefitBudget.EditIndex = -1;
        BindGrid();
    }
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

                    Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }

            }
        }

    }
    private void Disable()
    {
        chkShowBenefit.Enabled = false;
        imgApply.Enabled = false;
        imgAdd.Enabled = false;
        imgDelete.Enabled = false;
       // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";


    }
    #endregion 
    #region "Show Benefit-17thOct11"
    protected void chkShowBenefit_CheckedChanged(object sender, EventArgs e)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        Project update = projectDB.Projects.Single(P => P.ProjectReference == sessionKeys.Project);
        update.showbenefit = chkShowBenefit.Checked ? true : false;
        projectDB.SubmitChanges();
    }
    private void ShowInDashBoard()
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var getVal = (from r in projectDB.Projects
                      where r.ProjectReference == sessionKeys.Project
                      select r).ToList().FirstOrDefault();
        if (getVal.showbenefit == null)
        {
            Project update = projectDB.Projects.Single(P => P.ProjectReference == sessionKeys.Project);
            update.showbenefit = chkShowBenefit.Checked ? true : false;
            projectDB.SubmitChanges();
        }
        chkShowBenefit.Checked = getVal.showbenefit.Value;
    }
    #endregion
 
}
