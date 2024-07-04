using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.DAL;

public partial class DC_controls_StorageLocation : System.Web.UI.UserControl
{
    public string Section
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Section))
        {
            if (Section.Contains("accesscontrol"))
            {
                lblSectionHeader.Text = "Area";
                //lblTitle.Text = "Area";
                rfv_ddl_validation.ErrorMessage = "Please select area";
                rfv_text_validation.ErrorMessage = "Please enter area";
            }
        }

        if (!IsPostBack)
        {
            HideLocation();
        }
    }
    #region Location

    #region Hide Location Controls
    private void HideLocation()
    {
        ddl_Location.Visible = true;
        txt_Location.Visible = false;
        imb_SubmitLocation.Visible = false;
        imb_CancelLocation.Visible = false;
        imb_AddLocation.Visible = true;
        imb_DeleteLocation.Visible = true;
        imb_EditLocation.Visible = true;
    }
    #endregion
    #region Show Location Controls
    private void ShowLocation()
    {
        ddl_Location.Visible = false;
        txt_Location.Visible = true;
        imb_SubmitLocation.Visible = true;
        imb_CancelLocation.Visible = true;
        imb_AddLocation.Visible = false;
        imb_DeleteLocation.Visible = false;
        imb_EditLocation.Visible = false;
        lbllmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_AddLocation_Click(object sender, EventArgs e)
    {
        ShowLocation();
        txt_Location.Text = string.Empty;
    }
    protected void imb_CancelLocation_Click(object sender, EventArgs e)
    {
        HideLocation();

        lbllmsg.Text = string.Empty;
    }
    protected void imb_SubmitLocation_Click(object sender, EventArgs e)
    {
        try
        {
            StorageLocation location = new StorageLocation();
            location.Name = txt_Location.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(h_lID.Value) ? "0" : h_lID.Value);
            if (id > 0)
            {
                bool exists = StorageLocationBAL.CheckbyIdUpdate(id, txt_Location.Text.Trim());
                if (!exists)
                {
                    location.ID = id;
                    StorageLocationBAL.LocationUpdate(location);
                    lbllmsg.Text = "Item Updated Successfully.";
                    lbllmsg.ForeColor = System.Drawing.Color.Green;
                    HideLocation();
                    ccdLocation.SelectedValue = id.ToString();
                    h_lID.Value = "0";
                    txt_Location.Text = string.Empty;
                }
                else
                {
                    lbllmsg.Text = "Item Already Exists.";
                    lbllmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = StorageLocationBAL.CheckExists(txt_Location.Text.Trim());
                if (!exists)
                {
                    StorageLocationBAL.AddLocations(location);
                    lbllmsg.Text = "Item Added Successfully.";
                    lbllmsg.ForeColor = System.Drawing.Color.Green;
                    HideLocation();
                    ccdLocation.SelectedValue = location.ID.ToString();
                    h_lID.Value = "0";
                    txt_Location.Text = string.Empty;
                }
                else
                {
                    lbllmsg.Text = "Item Already Exists.";
                    lbllmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditLocation_Click(object sender, EventArgs e)
    {
        try
        {
            StorageLocation location = StorageLocationBAL.SelectbyId(int.Parse(ddl_Location.SelectedValue));
            if (location != null)
            {
                txt_Location.Text = location.Name;
                h_lID.Value = location.ID.ToString();
                ShowLocation();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteLocation_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Location.SelectedValue != "0")
            {
                if (Check_StorageLocation_Association(int.Parse(ddl_Location.SelectedValue)) == 0)
                {
                    StorageLocationBAL.LocationDelete(int.Parse(ddl_Location.SelectedValue));
                    lbllmsg.Text = "Item Deleted Successfully.";
                    lbllmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbllmsg.Text = "Data is assigned to request(s). Please check and try again.";
                    lbllmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private int Check_StorageLocation_Association(int id)
    {
        int count =0;
        using (DCDataContext dc = new DCDataContext())
        {
            //check in Access Control
            count = dc.AccessControls.Where(p => p.AreaID == id).Count(); 
            //check in Delivery section
            if(count == 0)
                count = dc.RecievedInformations.Where(p => p.StorageLocationID == id).Count(); 

        }
        
        return count;
    }
    #endregion
}