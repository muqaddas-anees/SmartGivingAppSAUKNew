using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using Deffinity.ProgrammeManagers;
public partial class controls_PTLabourTracker : System.Web.UI.UserControl
{
    double total = 0;
    double spentToDate = 0;
    double totalBudgetRemaining = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckUserRole();
            BindWorksheet();
            BindGrid();
            CommandField();
           
           
        }
    }

    private void BindGrid()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var labourList = (from r in db.ProjectBOMDetils
                                  join b in db.BOM_Types on r.WorkSheetID equals b.ID
                                  where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == QueryStringValues.Project && r.Labour != 0 && r.ID != -99
                                  select new
                                  {
                                      ID = r.ID,
                                      r.Worksheet,
                                      r.WorkSheetID,
                                      r.Labour,
                                      r.Qty,
                                      r.Description,
                                     r.ExpectedShipmentDate,
                                      Total = r.Qty * r.Labour, // QTY * Price
                                      NumberComplete = (r.QtyReceived == null ? 0 : r.QtyReceived),

                                      SpentToDate = (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived)), // Price * NumberComplete
                                      TotalBudgetRemaining = ((r.Qty * r.Labour) - (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived))) // Total - SpentToDate
                                  }).ToList();

                if (ddlWorksheet.SelectedValue != "0")
                {
                    labourList = labourList.Where(l => l.WorkSheetID == Convert.ToInt32(ddlWorksheet.SelectedValue)).ToList();
                }
                if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                {
                    labourList = labourList.Where(l => l.Description.ToLower().Contains(txtDescription.Text.ToLower())).ToList();
                }

                total = labourList.Select(l => l.Total).Sum();
                spentToDate = Convert.ToDouble(labourList.Select(l => l.SpentToDate).Sum());
                totalBudgetRemaining = Convert.ToDouble(labourList.Select(l => l.TotalBudgetRemaining).Sum());

                gvLabour.DataSource = labourList;
                gvLabour.DataBind();

                //Top section binding
                lblOriginalLabourCost.Text = total.ToString("C");
                lblSpentToDate.Text = spentToDate.ToString("C");
                lblCostRemaining.Text = totalBudgetRemaining.ToString("C");
               



            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void imgNumberComplete_Click(object sender, EventArgs e)
    {

    }

    private void CheckUrl()
    {
        
    }
    private void BindWorksheet()
    {
        ddlWorksheet.DataSource = Deffinity.Worksheet.Worksheet_SelectAll(QueryStringValues.Project);
        ddlWorksheet.DataTextField = "TypeName";
        ddlWorksheet.DataValueField = "ID";
        ddlWorksheet.DataBind();
        ddlWorksheet.Items.Insert(0, new ListItem("Select All...", "0"));
    }
    protected void gvLabour_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "History")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                mdlpopupHisstory.Show();
                BindHistory(id);
            }
            if (e.CommandName == "NumberComplete")
            {

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvLabour_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //total += double.Parse(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
            //spentToDate += double.Parse(DataBinder.Eval(e.Row.DataItem, "SpentToDate").ToString());
            //totalBudgetRemaining += double.Parse(DataBinder.Eval(e.Row.DataItem, "TotalBudgetRemaining").ToString());
            LinkButton imgApplyDate = (LinkButton)e.Row.FindControl("imgApplyDate");
            if (e.Row.RowIndex == 0)
            {
                imgApplyDate.Visible = true;
            }
            else
            {
                imgApplyDate.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            //Footer row binding
            Label lblfTotal = (Label)e.Row.FindControl("lblfTotal");
            lblfTotal.Text = total.ToString("C");
            Label lblfSpentToDate = (Label)e.Row.FindControl("lblfSpentToDate");
            lblfSpentToDate.Text = spentToDate.ToString("C");
            Label lblfTotalBudgetRemaining = (Label)e.Row.FindControl("lblfTotalBudgetRemaining");
            lblfTotalBudgetRemaining.Text = totalBudgetRemaining.ToString("C");


            

        }
    }
    #region "Permission Code Here"
    private void CommandField()
    {
      
        try
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    imgUpdate.Visible = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    imgUpdate.Visible = false;

                    // Disable();

                }

            }
            if (Request.Url.ToString().ToLower().Contains("projecttracker_actuals.aspx"))
            {
                imgUpdate.Visible = false;
            }
        

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       

    }
    private void CheckUserRole()
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

    private void Disable()
    {
        //.Enabled = false;
        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

    }
    #endregion
    private void JournalInsert(int id, int worksheetId, string description, double valueNow)
    {

        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            double priviousValue = 0;
            ProjectTrackerJournal journal = db.ProjectTrackerJournals.Where(j => j.BOMID == id).OrderByDescending(j => j.ModifiedDate).FirstOrDefault();
            if (journal != null)
            {
                priviousValue = double.Parse(journal.ValueNow.ToString());
            }

            //journal 
            ProjectTrackerJournal projectTrackerJournal = new ProjectTrackerJournal();
            projectTrackerJournal.BOMID = id;
            projectTrackerJournal.WorksheetID = worksheetId;
            projectTrackerJournal.Description = description;
            projectTrackerJournal.PreviousValue = priviousValue;
            projectTrackerJournal.ValueNow = valueNow;
            projectTrackerJournal.SectionType = "Labour";
            projectTrackerJournal.ModifiedBy = sessionKeys.UID;
            projectTrackerJournal.ModifiedDate = DateTime.Now;
            db.ProjectTrackerJournals.InsertOnSubmit(projectTrackerJournal);
            db.SubmitChanges();
        }
    }
    private void BindHistory(int id)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    var jList = db.ProjectTrackerJournals.Where(j => j.BOMID == id && j.SectionType.ToLower() == "labour").Select(j => j).ToList();
                    var bomList = db.ProjectBOMDetils.Where(b => b.ID == id).ToList();
                    var journalList = (from p in jList
                                       join c in contractorList on p.ModifiedBy equals c.ID
                                       join b in bomList on p.BOMID equals b.ID
                                       orderby p.ModifiedDate descending
                                       select new { p.ID, p.WorksheetID, b.Worksheet, p.PreviousValue, p.ValueNow, UserName = c.ContractorName, p.Description, p.ModifiedDate }).ToList();

                    GvHistory.DataSource = journalList;
                    GvHistory.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        try
        {
            using (projectTaskDataContext project = new projectTaskDataContext())
            {
                int countRow = gvLabour.Rows.Count;
                bool dateError = false;
                bool qtyError = false;
                for (int i = 0; i < countRow; i++)
                {
                   
                    GridViewRow row = gvLabour.Rows[i];
                    Label lblID = (Label)row.FindControl("lblID");
                    int id = Convert.ToInt32(lblID.Text);
                    TextBox txtDateReceived = (TextBox)row.FindControl("txtDateReceived");
                    Label lblWorksheetID = (Label)row.FindControl("lblWorksheetID");
                    Label lblQTY = (Label)row.FindControl("lblQTY");
                    TextBox numberComplete = (TextBox)row.FindControl("txtNumberComplete");
                    double number = double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text);
                    Label lblDescription = (Label)row.FindControl("lblDescription");
                    ProjectMgt.Entity.GoodsReceipt gr = new ProjectMgt.Entity.GoodsReceipt();
                    var IsExist = (from r in project.GoodsReceipts
                                   where r.BOMID == int.Parse(lblID.Text)
                                   select r).ToList();
                    if (IsExist != null)
                    {
                        if (IsExist.Count == 0)
                        {
                            if (number > 0 && txtDateReceived.Text == "")
                            {
                                dateError = true;
                            }
                            else
                            {
                                if (txtDateReceived.Text != "" && number>0)
                                {
                                    if (double.Parse(string.IsNullOrEmpty(lblQTY.Text) ? "0" : lblQTY.Text) < number)
                                    {
                                        qtyError = true;
                                        //lblResult.Text = "You have entered number complete value greater than the QTY. Please check and try again.";
                                        //lblResult.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        gr.BOMID = int.Parse(lblID.Text);
                                        gr.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                        gr.QtyOrdered = double.Parse(string.IsNullOrEmpty(lblQTY.Text) ? "0" : lblQTY.Text);
                                        gr.QtyReceived = number;

                                        gr.OutStandingQty = gr.QtyOrdered - gr.QtyReceived;
                                        project.GoodsReceipts.InsertOnSubmit(gr);
                                        project.SubmitChanges();
                                        if (CheckDetailsChanged(id, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text)))
                                        {
                                            JournalInsert(id, Convert.ToInt32(lblWorksheetID.Text), lblDescription.Text, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text));
                                        }
                                        // lblResult.Text = "Updated successfully.";
                                        // lblResult.ForeColor = System.Drawing.Color.Green;
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (number > 0 && txtDateReceived.Text == "")
                            {
                                dateError = true;
                            }
                            else
                            {
                                if (txtDateReceived.Text != "" && number>0)
                                {
                                    if (double.Parse(string.IsNullOrEmpty(lblQTY.Text) ? "0" : lblQTY.Text) < number)
                                    {
                                        qtyError = true;
                                    }
                                    else
                                    {
                                        ProjectMgt.Entity.GoodsReceipt Update =
                                                 project.GoodsReceipts.Single(P => P.BOMID == int.Parse(lblID.Text));

                                        Update.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                               DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                        Update.QtyOrdered = double.Parse(string.IsNullOrEmpty(lblQTY.Text) ? "0" : lblQTY.Text);
                                        Update.QtyReceived = number;
                                        Update.OutStandingQty = Update.QtyOrdered - Update.QtyReceived;
                                        project.SubmitChanges();
                                        if (CheckDetailsChanged(id, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text)))
                                        {
                                            JournalInsert(id, Convert.ToInt32(lblWorksheetID.Text), lblDescription.Text, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text));
                                        }
                                        // lblResult.Text = "Updated successfully.";
                                        //lblResult.ForeColor = System.Drawing.Color.Green;
                                    }
                                }
                            }
                        }


                    }

                    //using (projectTaskDataContext db = new projectTaskDataContext())
                    //{
                    //    ProjectBOM projectBOM = db.ProjectBOMs.Where(p => p.ID == id).Select(p => p).FirstOrDefault();
                    //    if (projectBOM != null)
                    //    {
                    //        if (double.Parse(string.IsNullOrEmpty(lblQTY.Text) ? "0" : lblQTY.Text) < number)
                    //        {

                    //            lblResult.Text = "You have entered number complete value greater than the QTY. Please check and try again.";
                    //            lblResult.ForeColor = System.Drawing.Color.Red;
                    //        }
                    //        else
                    //        {
                    //            projectBOM.NumberComplete = number;
                    //            db.SubmitChanges();

                    //            if (CheckDetailsChanged(id, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text)))
                    //            {
                    //                JournalInsert(id, Convert.ToInt32(lblWorksheetID.Text), lblDescription.Text, double.Parse(string.IsNullOrEmpty(numberComplete.Text) ? "0" : numberComplete.Text));
                    //            }
                    //            lblResult.Text = "Updated successfully.";
                    //            lblResult.ForeColor = System.Drawing.Color.Green;
                    //        }

                    //    }


                    //}

                }
                if (qtyError || dateError)
                {
                    if (qtyError)
                    {
                        lblResult.Text = "You have entered number complete value greater than the QTY. Please check and try again.";
                        lblResult.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblResult.Text = "Please enter date received";
                        lblResult.ForeColor = System.Drawing.Color.Red;
                    }
                  
                }
                else
                {
                    lblResult.Text = "Updated successfully.";
                    lblResult.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }

               // BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private bool CheckDetailsChanged(int id, double value)
    {
        bool changed = false;
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {

                ProjectTrackerJournal journal = db.ProjectTrackerJournals.Where(j => j.BOMID == id && j.SectionType == "Labour").OrderByDescending(j => j.ModifiedDate).FirstOrDefault();
                if (journal != null)
                {
                    if (journal.ValueNow != value)
                        changed = true;

                }
                else
                {
                    //first time
                    changed = true;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return changed;
    }
    protected void gvLabour_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLabour.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ddlWorksheet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void imgViewAll_Click(object sender, EventArgs e)
    {
        ddlWorksheet.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        gvLabour.AllowPaging = false;
        BindGrid();
      
    }
    protected void btn_ApplyDate_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
            //HIndent
             DateTime dateReceived = Convert.ToDateTime(((TextBox)row.FindControl("txtDateReceived")).Text);

            int CountRow = gvLabour.Rows.Count;
            for (int i = 0; i < CountRow; i++)
            {

                GridViewRow Row = gvLabour.Rows[i];
                TextBox txtDateReceived = (TextBox)Row.FindControl("txtDateReceived");

                txtDateReceived.Text = string.Format("{0:d}", dateReceived);
                
            }


        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }



    }
}