using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using ProjectMgt.Entity;

public partial class controls_CreditGridCntl : System.Web.UI.UserControl
{

    public delegate void ButtonClciked(object sender, EventArgs e);
    public event ButtonClciked OnbuttonClicked;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridInCriditNote();     
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    public void BindGridInCriditNote()
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var CreditNotesList = Pdc.Project_CreditNotes.ToList();
                    if (QueryStringValues.Project != 0)
                    {
                        CreditNotesList = Pdc.Project_CreditNotes.Where(a => a.ProjectRef == QueryStringValues.Project).ToList();
                    }
                    var clist = Udc.Contractors.Where(a => CreditNotesList.Select(b => b.Appliedby.HasValue ? b.Appliedby.Value : 0).ToArray().Contains(a.ID)).ToList();

                    var x = (from a in CreditNotesList
                             join b in clist on a.Appliedby equals b.ID
                             orderby a.Id descending
                             select new
                             {
                                 Id = a.Id,
                                 CreditValue = a.CreditValue,
                                 Description = a.Description,
                                 DateandTime = a.DateandTime,
                                 Appliedby = b.ContractorName
                             }).ToList();
                    gridCreditRecord.DataSource = x;
                    gridCreditRecord.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridCreditRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int i = gridCreditRecord.EditIndex;
                GridViewRow row = gridCreditRecord.Rows[i];
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                TextBox txtcName = (TextBox)row.FindControl("txtCreditValue");
                if (txtcName.Text.Trim() != string.Empty)
                {
                    using (projectTaskDataContext Pdc = new projectTaskDataContext())
                    {

                        Project_CreditNote In_c = Pdc.Project_CreditNotes.Where(a => a.Id == id).FirstOrDefault();
                        In_c.CreditValue = Convert.ToDouble(txtcName.Text.Trim());
                        In_c.DateandTime = DateTime.Now;
                        In_c.Appliedby = sessionKeys.UID;


                        Pdc.SubmitChanges();
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Updated successfully";
                        BindGridInCriditNote();

                        if (OnbuttonClicked != null)
                        {
                            OnbuttonClicked(sender, e);
                        }

                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter credit value";
                }

            }
            else if (e.CommandName == "Delete1")
            {
                int Creditid = Convert.ToInt32(e.CommandArgument.ToString());
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    Project_CreditNote In_c = Pdc.Project_CreditNotes.Where(a => a.Id == Creditid).FirstOrDefault();
                    Pdc.Project_CreditNotes.DeleteOnSubmit(In_c);
                    Pdc.SubmitChanges();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Deleted successfully";
                    BindGridInCriditNote();
                    if (OnbuttonClicked != null)
                    {
                        OnbuttonClicked(sender, e);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gridCreditRecord_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridCreditRecord.EditIndex = e.NewEditIndex;
        BindGridInCriditNote();
    }
    protected void gridCreditRecord_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gridCreditRecord.EditIndex = -1;
        BindGridInCriditNote();
    }
    protected void gridCreditRecord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridCreditRecord.EditIndex = -1;
        BindGridInCriditNote();
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridInCriditNote();
    }
    protected void gridCreditRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridCreditRecord.PageIndex = e.NewPageIndex;
        BindGridInCriditNote();
    }
}