using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BAL;

public partial class DC_controls_PurposeOfVisit : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindNames();
            pnladd.Visible = false;

        }
        lblmsg.Text = string.Empty;
    }

    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlnames.Visible = false;
            pnladd.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Bind records
    private void BindNames()
    {
        try
        {
            ddlNames.DataSource = VisitingPurpose.Names_selectAll();
            ddlNames.DataTextField = "Name";
            ddlNames.DataValueField = "ID";
            ddlNames.DataBind();
            ddlNames.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Add records
    private void AddNames()
    {
        try
        {
            PurposeToVisit p = new PurposeToVisit();
            p.Name = txtadd.Text;
            int id = int.Parse(string.IsNullOrEmpty(h_nameid.Value) ? "0" : h_nameid.Value);

            if (id > 0)
            {
                bool blncheck = VisitingPurpose.Name_ExistUpdate(txtadd.Text, id);
                p.ID = id;
                if (!blncheck)
                {


                    VisitingPurpose.Name_update(p);
                    lblmsg.Text = "Updated successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    // lblerror.Text = string.Empty;
                    BindNames();
                    ddlNames.SelectedValue = id.ToString();
                }
                else
                {
                    lblmsg.Text = "Record already exist.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {

                bool blncheck = VisitingPurpose.Name_Exist(txtadd.Text);
                if (!blncheck)
                {
                    VisitingPurpose.Names_Insert(p);
                    lblmsg.Text = "Inserted successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    BindNames();
                    ddlNames.SelectedValue = p.ID.ToString();
                }
                else
                {
                    lblmsg.Text = "Record already exist.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }
            }

            pnladd.Visible = false;
            BindNames();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AddNames();
            txtadd.Text = string.Empty;
            h_nameid.Value = "0";
            pnlnames.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNames.SelectedValue == "0")
            {
                lblmsg.Text = "Please select record";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                pnlnames.Visible = true;
                pnladd.Visible = false;

            }
            else
            {
                h_nameid.Value = ddlNames.SelectedValue;
                int id = int.Parse(string.IsNullOrEmpty(ddlNames.SelectedValue) ? "0" : ddlNames.SelectedValue);
                if (id > 0)
                {
                    PurposeToVisit p = VisitingPurpose.Name_selectByID(id);
                    txtadd.Text = p.Name;
                    h_nameid.Value = p.ID.ToString();

                }
                pnladd.Visible = true;
                pnlnames.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void imgbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNames.SelectedIndex != 0)
            {

                int id = int.Parse(ddlNames.SelectedValue);

                VisitingPurpose.Name_Delete(id);
                lblmsg.Text = "Record deleted successfully.";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                BindNames();

            }
            else
            {
                lblmsg.Text = "Please select record.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgbtncnl_Click(object sender, EventArgs e)
    {
        try
        {
            pnladd.Visible = false;
            pnlnames.Visible = true;
            txtadd.Text = string.Empty;
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}