using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BAL;

public partial class DC_controls_AccessNo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindAccessNo();

            }
            lblsuccessmsg.Text = string.Empty;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindAccessNo()
    {
        try
        {
            AccessNumber acsno = DefaultsOfAccessControl.AccessNo_select();
            if (acsno != null)
            {
                txtaccessno.Text = acsno.AccessNo;
                imgbtnaddno.Visible = false;
                imgbtnupdateno.Visible = true;
                imgbtndel.Visible = true;
            }
            else
            {
                txtaccessno.Text = string.Empty;
                imgbtnaddno.Visible = true;
                imgbtnupdateno.Visible = false;
                imgbtndel.Visible = false;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgbtnaddno_Click(object sender, EventArgs e)
    {
        try
        {

            AccessNumber acsno = DefaultsOfAccessControl.AccessNo_select();

            if (acsno == null)
            {
                AccessNumber ano = new AccessNumber();
                ano.AccessNo = txtaccessno.Text;
                DefaultsOfAccessControl.AccessNo_Insert(ano);
                lblsuccessmsg.Text = "Access number inserted successfully.";
                lblsuccessmsg.ForeColor = System.Drawing.Color.Green;
            }

            BindAccessNo();
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imgbtnupdateno_Click(object sender, EventArgs e)
    {
        try
        {
            AccessNumber acsno = DefaultsOfAccessControl.AccessNo_select();
            if (acsno != null)
            {
                acsno.AccessNo = txtaccessno.Text;
                DefaultsOfAccessControl.AccessNo_update(acsno);
                lblsuccessmsg.Text = "Access number updated successfully.";
                lblsuccessmsg.ForeColor = System.Drawing.Color.Green;

            }
            BindAccessNo();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void imgbtndel_Click(object sender, EventArgs e)
    {
        try
        {
            AccessNumber acsno = DefaultsOfAccessControl.AccessNo_select();
            if (acsno != null)
            {
                int id = acsno.ID;
                DefaultsOfAccessControl.AccessNo_Delete(id);
                lblsuccessmsg.Text = "Access no. deleted successfully.";
                lblsuccessmsg.ForeColor = System.Drawing.Color.Green;
                BindAccessNo();
            }

        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}