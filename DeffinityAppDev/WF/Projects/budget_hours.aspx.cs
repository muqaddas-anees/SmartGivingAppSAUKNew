using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;

public partial class budget_hours : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Project Management";
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                if (pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.ProjectStatusID).FirstOrDefault() == 2)
                {
                    ChangeControlStatus();
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectPipeline.aspx?Status=2");
    }
    public void ControlDisable(ControlCollection ptext, bool status)
    {
        try
        {
            foreach (Control ctrl in ptext)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Enabled = status;
                }
                else if (ctrl is Button)
                {
                    ((Button)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBoxList)
                {
                    ((CheckBoxList)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).Enabled = status;
                }
                else if (ctrl is Image)
                {
                    ((Image)ctrl).Enabled = status;
                }
                else if (ctrl is Calendar)
                {
                    ((Calendar)ctrl).Enabled = status;
                }
                else if (ctrl is ImageButton)
                {
                    ((ImageButton)ctrl).Enabled = status;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = status;
                }
                else if (ctrl is HyperLink)
                {
                    ((HyperLink)ctrl).Enabled = status;
                }
                else if (ctrl is Label)
                {
                    ((Label)ctrl).Enabled = status;
                }
                else if (ctrl is GridView)
                {
                    ((GridView)ctrl).Enabled = status;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ChangeControlStatus()
    {
        var cl = this.Form.Controls;
        var M1Content = this.Form.FindControl("MainContent") as ContentPlaceHolder;
        if (M1Content != null)
        {
            var M2Content = M1Content.FindControl("MainContent") as ContentPlaceHolder;
            var M3Content = M2Content.FindControl("StaffHours1") as UserControl;
            if (M3Content != null)
            {
                var Ccntrl = M3Content.Controls;
                ControlDisable(Ccntrl, false);
            }
        }
    }
}