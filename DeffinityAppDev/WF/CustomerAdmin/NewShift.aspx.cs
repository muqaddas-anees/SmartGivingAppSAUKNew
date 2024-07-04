using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class NewShift : System.Web.UI.Page
{
    SqlCommand _cmd = new SqlCommand();

    #region SQL Commands

    const string _insertShift = "INSERT INTO Shift(Shift,StartTime,EndTime,Colour,PortfolioID) Values(@Shift,@StartTime,@EndTime,@Colour,@PortfolioID)";
    const string _updateShift = "Update Shift Set Shift=@Shift,StartTime=@StartTime,EndTime=@EndTime,Colour=@Colour WHERE ID=@ID";
    const string _selectShiftByID = "Select * FROM Shift WHERE ID=@ID";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Customer Admin";
        }
    }

    protected void btnShift_Click(object sender, EventArgs e)
    {
        _cmd.Parameters.Clear();
        InsertOrUpdate(_insertShift);
    }

    protected void btnUpdateShift_Click(object sender, EventArgs e)
    {
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@ID",hiddenID.Value);
        InsertOrUpdate(_updateShift);
        hiddenID.Value = string.Empty;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hiddenID.Value))
            btnUpdateShift.Visible = false;
        else
            btnUpdateShift.Visible = true;
        btnShift.Visible = !btnUpdateShift.Visible;
    }

    private void InsertOrUpdate(string dmlStatement)
    {
        try
        {
            using (TeamHelper helper = new TeamHelper())
            {
                _cmd.CommandText = dmlStatement;
                addParametersForShift();
                helper.SqlCommand = _cmd;
                if (helper.UpdateOrInsertOrDeleteData())
                {
                    lblMessage.Text = "Shift added/updated successfully";
                    ClearFormFields();
                    gridShift.DataBind();
                }
                else
                    lblMessage.Text = "Insertion/Updation failed. Please try again.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Insertion/Updation Failed. Please try again.";
        }
    }

    void ClearFormFields()
    {
        txtEndTime.Text = string.Empty;
        txtShift.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        ddlColors.SelectedIndex = 0;
    }

    //Add the parameters (@Shift,@StartTime,@EndTime,@Colour)
    private void addParametersForShift()
    {
        _cmd.Parameters.AddWithValue("@Shift", txtShift.Text);
        _cmd.Parameters.AddWithValue("@StartTime", txtStartTime.Text);
        _cmd.Parameters.AddWithValue("@EndTime", txtEndTime.Text);
        _cmd.Parameters.AddWithValue("@Colour",ddlColors.SelectedItem.Value);
        _cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
    }
    protected void gridShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGridColor = ((Label)e.Row.FindControl("lblShift"));
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
                alteredColor=color;
                break;
        }
        return alteredColor;
    }

    protected void gridShift_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        lblMessage.Text = "Shift deleted successfully.";
    }
   
    protected void gridShift_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int selectedRow = e.NewSelectedIndex;
        int selectedID= Convert.ToInt32(((Label)gridShift.Rows[selectedRow].FindControl("lblID")).Text);
        _cmd.Parameters.Clear();
        _cmd.Parameters.AddWithValue("@ID", selectedID);
        DataTable shiftTable = new DataTable();
        using (TeamHelper helper = new TeamHelper())
        {
            helper.SqlCommand = _cmd;
            _cmd.CommandText = _selectShiftByID;
            shiftTable = helper.GetData();
        }
        FillFormControls(shiftTable);
    }

    void FillFormControls(DataTable shiftTable)
    {
        if (shiftTable.Rows.Count > 0)
        { 
            txtShift.Text=shiftTable.Rows[0]["Shift"].ToString();
            txtStartTime.Text = shiftTable.Rows[0]["StartTime"].ToString();
            txtEndTime.Text = shiftTable.Rows[0]["EndTime"].ToString();
            ddlColors.SelectedIndex = ddlColors.Items.IndexOf(ddlColors.Items.FindByValue(shiftTable.Rows[0]["Colour"].ToString()));
            hiddenID.Value = shiftTable.Rows[0]["ID"].ToString();
        }
    }
}
