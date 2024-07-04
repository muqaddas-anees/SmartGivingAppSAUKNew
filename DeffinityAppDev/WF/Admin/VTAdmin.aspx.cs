using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VT;

public partial class VTAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "VT Admin";
        //ConvertTime.ConvertToHours(585);
        if (!IsPostBack)
        {
            BindData();
        }
    }
  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //ConvertTime.ConvertToHours(585);
            object returnval;
            returnval = VT.AdminEntry.InsertUpdateAbsenseType(txtabsensetype.Text.Trim(), ddlColors.SelectedValue.Trim(), Convert.ToInt32(string.IsNullOrEmpty(HID.Value.ToString()) ? "0" : HID.Value));
            if (Convert.ToInt16(returnval) == 1)
            {
                lblmsg1.Text = "Absence Type inserted";
                txtabsensetype.Text = string.Empty;
            }
            else if (Convert.ToInt16(returnval) == 2)
            {
                lblmsg1.Text = "Absence Type updated";
                txtabsensetype.Text = string.Empty;
            }
            else if (Convert.ToInt16(returnval) == 3)
            {
                lblmsg1.Text = "Absence Type already exists";
                txtabsensetype.Text = string.Empty;
            }
            else if (Convert.ToInt16(returnval) == 4)
            {
                lblmsg1.Text = "Absence Type Cannot be Changed";
                txtabsensetype.Text = string.Empty;
            }
            GrdAbsenseTypes.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        txtabsensetype.Text = string.Empty;
        HID.Value = string.Empty;
    }
    //protected void btnsubmit1_Click(object sender, ImageClickEventArgs e)
    //{
    //    object returnval;
    //    int UserID = Convert.ToInt32(ddlUsers.SelectedValue);
    //    float Fulldayhours = getFloat(txtfulldayhours.Text.Trim());
    //    float Halfdayhours = getFloat(txthalfdayhours.Text.Trim());
    //    int Daysinlieu = Convert.ToInt32(txtdayinlieu.Text);
    //    returnval = VT.AdminEntry.InsertUpdateWorkHours(UserID,Fulldayhours,Halfdayhours,Daysinlieu);
    //    if (Convert.ToInt16(returnval) == 1)
    //    {
    //        lblMessage.Text = "Details inserted";
    //        txtdayinlieu.Text = "";
    //        txtfulldayhours.Text = "";
    //        txthalfdayhours.Text = "";

    //    }
    //    if (Convert.ToInt16(returnval) == 2)
    //    {
    //        lblMessage.Text = "Details updated";
    //        txtdayinlieu.Text = "";
    //        txtfulldayhours.Text = "";
    //        txthalfdayhours.Text = "";
    //    }
        
    //}

    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            object returnval;
            int UserID = 0;
            int starttime = ConvertTime.ConvertToMinuts(txtdaystart.Text.Trim());
            int endtime = ConvertTime.ConvertToMinuts(txtdayend.Text.Trim());
            int halfdaypoint = ConvertTime.ConvertToMinuts(txthdp.Text.Trim());
            int maxdaysinlieu = 0;
            returnval = VT.AdminEntry.InsertUpdateUSERTIMES(UserID, starttime, endtime, halfdaypoint, maxdaysinlieu);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //if (Convert.ToInt16(returnval) == 1)
        //{
        //    //lblMessage.Text = "Details inserted";
        //    //clearfields();
        //}
        //if (Convert.ToInt16(returnval) == 2)
        //{
        //    lblMessage.Text = "Details updated";
        //    //clearfields();
            
        //}

    }
    public string BindTImespan(int mins)
    {
        return ConvertTime.ConvertToHours(mins);
    }
    //private float getFloat(string st)
    //{
    //    string newval = "";
    //    char[] comm = { ':' };
    //    string[] getval = st.Split(comm);

    //    newval = (getval[0]) + "." + getval[1];
    //    float t = 0;
    //    try
    //    {
    //        if (newval != "")
    //        {
    //            t = float.Parse(newval);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    //    return t;
    //}
    //private double getDouble(string st)
    //{
    //    string newval = "";
    //    char[] comm = { ':' };
    //    string[] getval = st.Split(comm);

    //    newval = (getval[0]) + "." + getval[1];
    //    double t = 0;
    //    try
    //    {
    //        if (newval != "")
    //        {
    //            t = Convert.ToDouble(newval);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    //    return t;
    //}
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        clearfields();
        //txtdayinlieu.Text = "";
        //txtfulldayhours.Text = "";
        //txthalfdayhours.Text = "";
    }
    private void clearfields()
    {
        //gridUserMappings.DataBind();
        //ddlUsers.SelectedValue = "0";
        txtdaystart.Text = "00:00";
        txtdayend.Text = "00:00";
        txthdp.Text = "00:00";
        //txtdayinlieu.Text = "";
    }
   
    protected void GrdAbsenseTypes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GrdAbsenseTypes.Rows[e.NewSelectedIndex];
        
        txtabsensetype.Text = ((Label)row.FindControl("lblAbsense")).Text;
        ddlColors.SelectedValue = ((Label)row.FindControl("lblColorEdit")).Text;
        //Label lblcolor = ((Label)row.FindControl("lblColorEdit"));
        //string color = lblcolor.Text;
        //ddlColors.SelectedIndex = ddlColors.Items.IndexOf(ddlColors.Items.FindByText(color));
        //ddlColors.SelectedIndex = ddlColors.Items.IndexOf(ddlColors.Items.FindByText(((Label)row.FindControl("lblColor")).Text.Trim()));
        
    }
    protected void GrdAbsenseTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        HID.Value = GrdAbsenseTypes.SelectedDataKey.Value.ToString();
    }
    protected void GrdAbsenseTypes_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled == false)
        {
            try
            {
                
                VT.AdminEntry.DeleteAbsenseType(int.Parse(e.Keys["ID"].ToString()));
                //GrdAbsenseTypes.DataBind();
                e.ExceptionHandled = true;
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
   }

    private void BindData()
    {
        try
        {
            SqlDataReader dr = VT.AdminEntry.SelectUserTimes();
            while (dr.Read())
            {
                txtdayend.Text = ConvertTime.ConvertToHours(int.Parse(dr["ENDMIN"].ToString()));
                //txtdayinlieu.Text = dr["MAXDAYSINLIEU"].ToString();
                txtdaystart.Text = ConvertTime.ConvertToHours(int.Parse(dr["STARTMIN"].ToString()));
                txthdp.Text = ConvertTime.ConvertToHours(int.Parse(dr["HDPMIN"].ToString()));
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GrdAbsenseTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGridColor = ((Label)e.Row.FindControl("lblColor"));
            System.Drawing.KnownColor[] colors = (System.Drawing.KnownColor[])System.Enum.GetValues(typeof(System.Drawing.KnownColor));
            foreach (System.Drawing.KnownColor color in colors)
            {
                if (color.ToString().Equals(lblGridColor.Text))
                {
                    lblGridColor.Attributes.Add("style", "background:" + GetAlteredColor(lblGridColor.Text));
                    lblGridColor.Text = string.Empty;
                }
            }
        }
    }
    protected string GetAlteredColor(string color)
    {
        string alteredColor = string.Empty;
        switch (color)
        {
            case "Olive":
                alteredColor = "#667C26";
                break;
            case "Blue":
                alteredColor = "#6698FF";
                break;
            case "Green":
                alteredColor = "#4CC417";
                break;
            case "Teal":
                alteredColor = "#4C7D7E";
                break;
            case "Maroon":
                alteredColor = "#E3319D";
                break;
            case "Red":
                alteredColor = "#E55451";
                break;
            case "Gray":
                alteredColor = "#4C4646";
                break;
            case "Lime":
                alteredColor = "#41A317";
                break;
            case "Aqua":
                alteredColor = "#B6FFFF;color:black";
                break;
            case "Yellow":
                alteredColor = "#FFF380;color:black";
                break;
            case "Purple":
                alteredColor = "#8E35EF";
                break;
            default:
                alteredColor = color;
                break;
        }
        return alteredColor;
    }

    protected bool DeleteButtonVisibility(string Absenttypeid)
    {
        bool retval = false;
        // static absent data types should not delete
        if ((int.Parse(string.IsNullOrEmpty(Absenttypeid)? "0": Absenttypeid)) >6 )
        {
            retval = true;
        }

        return retval;
    }
   
}
