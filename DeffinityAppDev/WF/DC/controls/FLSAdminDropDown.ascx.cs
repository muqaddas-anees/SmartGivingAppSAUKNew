using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.DAL;
using PortfolioMgt.DAL;
using UserMgt.DAL;
using DC.DAL;
using DC.Entity;
using DC.BLL;

public partial class DC_FLSAdminDropDown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindControls();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    #region Bind Subject
    public void BindControls()
    {
        try
        {
            ddlSubject.DataSource = FLSSubject.Bind().Where(s => s.CustomerID == sessionKeys.PortfolioID).ToList();
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "ID";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion


    private void HideControls(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
    {
        _ddlItems.Visible = true;
        _txtEdit.Visible = false;
        _imgSubmit.Visible = false;
        _imgCancel.Visible = false;
        _imgAdd.Visible = true;
        _imgDelete.Visible = true;
        _imgEdit.Visible = true;


    }

    private void VisibleControl(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
    {
        _ddlItems.Visible = false;
        _txtEdit.Visible = true;
        _imgSubmit.Visible = true;
        _imgCancel.Visible = true;
        _imgAdd.Visible = false;
        _imgDelete.Visible = false;
        _imgEdit.Visible = false;
        lblMsg.Text = string.Empty;

    }

    protected void btndeleteSubject_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedValue != "0")
            {
                int subjectid = int.Parse(ddlSubject.SelectedValue);
                //check the suject is assigned already
                if (!Subject_IsAssined(subjectid))
                {
                    FLSSubject.DeleteById(subjectid);
                    lblMsg.Text = "Item deleted successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindControls();
                }
                else
                {
                    lblError.Text = "Item assigned to request(s).Please check and try again";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select one subject";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private bool Subject_IsAssined(int subjectid)
    {
        int retval = 0;
        using (DCDataContext dcontext = new DCDataContext())
        {
            retval = (from p in dcontext.FLSDetails
                      where p.SubjectID == subjectid
                      select p).Count();
        }
        return retval > 0 ? true : false;
    }

  
    protected void btnaddSubject_Click(object sender, EventArgs e)
    {
        try
        {
            txtSubject.Text = string.Empty;
            VisibleControl(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
            //Panel_portfolio.Visible = false;
            
            //Panel2.Visible = false;
            txtSubject.Focus();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btneditSubject_Click(object sender, EventArgs e)
    {
        try
        {
            Subject subject = FLSSubject.SelectById(int.Parse(ddlSubject.SelectedValue));

            if (subject != null)
            {
                txtSubject.Text = subject.SubjectName;
                hid.Value = subject.ID.ToString();
                VisibleControl(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                //Panel_portfolio.Visible = false;
                
                //Panel2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnsubmitSubject_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            Subject subject = new Subject();
            subject.SubjectName = txtSubject.Text.Trim();
            subject.CustomerID = customerID;

            int id = int.Parse(string.IsNullOrEmpty(hid.Value) ? "0" : hid.Value);
            if (id > 0)
            {
                bool exists = FLSSubject.CheckByIdUpdate(id, txtSubject.Text.Trim(), customerID);
                if (!exists)
                {
                    subject.ID = id;
                    FLSSubject.Update(subject);
                    lblMsg.Text = "Item updated successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;

                    HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                    BindControls();
                    hid.Value = "0";
                    txtSubject.Text = string.Empty;
                }
                else
                {
                    lblError.Text = "Item already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {

                bool exists = FLSSubject.CheckExists(txtSubject.Text.Trim(), customerID);

                if (!exists)
                {
                    FLSSubject.Add(subject);
                    lblMsg.Text = "Item added successfully.";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                    BindControls();
                    hid.Value = "0";
                    txtSubject.Text = string.Empty;
                }
                else
                {
                    lblError.Text = "Item already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btncancelSubject_Click(object sender, EventArgs e)
    {
        try
        {
            HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
            BindControls();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    protected void btnCopyToAllCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext dc = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    var subjectList = dc.Subjects.Where(c => c.CustomerID == customerID).ToList();
                    if (subjectList.Count() > 0)
                    {
                        List<Subject> sList = new List<Subject>();
                        foreach (var c in customerList)
                        {
                            foreach (var s in subjectList)
                            {
                                bool exists = FLSSubject.CheckExists(s.SubjectName, c.ID);
                                if (!exists)
                                {
                                    Subject subject = new Subject();
                                    subject.CustomerID = c.ID;
                                    subject.SubjectName = s.SubjectName;
                                    sList.Add(subject);
                                }
                            }
                        }
                        //Bulk insert
                        dc.Subjects.InsertAllOnSubmit(sList);
                        dc.SubmitChanges();

                        LblSubjectMsg.Text = "Successfully copied";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    
    protected void btndeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedValue != "0")
            {
                List<Subject> sList = new List<Subject>();
                string Sname = ddlSubject.SelectedItem.ToString();
                using (DCDataContext dc = new DCDataContext())
                {
                  sList = dc.Subjects.Where(c => c.SubjectName == Sname).ToList();
                  dc.Subjects.DeleteAllOnSubmit(sList);
                  dc.SubmitChanges();
                }
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Subject deleted to all customers successfully";
                BindControls();
            }
            else
            {
               //lblMsg.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select one subject";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedValue != "0")
            {
                int subjectid = int.Parse(ddlSubject.SelectedValue);
                //check the suject is assigned already
                if (!Subject_IsAssined(subjectid))
                {
                    FLSSubject.DeleteById(subjectid);
                    lblMsg.Text = "Item deleted successfully.";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindControls();
                }
                else
                {
                    lblError.Text = "Item assigned to request(s).Please check and try again";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select one subject";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
   
   
   
}