using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DC_AdminDropDown : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Admin Dropdown Lists";
                HideDelivery();
                HideCondition();

                BindDeliveryDefaults();
                HideDefault();
                sae1.RequestTypeID = 1;  //1=Delivery  
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region Delivery Type 
    #region Hide Delivery Type Controls
    private void HideDelivery()
    {
        ddl_Type.Visible = true;
        txt_Type.Visible = false;
        imb_SubmitType.Visible = false;
        imb_CancelType.Visible = false;
        imb_AddType.Visible = true;
        imb_DeleteType.Visible = true;
        imb_EditType.Visible = true;
    }
    #endregion
    #region Show Delivery Type Controls
    private void ShowDelivery()
    {
        ddl_Type.Visible = false;
        txt_Type.Visible = true;
        imb_SubmitType.Visible = true;
        imb_CancelType.Visible = true;
        imb_AddType.Visible = false;
        imb_DeleteType.Visible = false;
        imb_EditType.Visible = false;
        lbldtmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_AddType_Click(object sender, EventArgs e)
    {
        ShowDelivery();
        txt_Type.Text = string.Empty;
    }
    protected void imb_CancelType_Click(object sender, EventArgs e)
    {
        HideDelivery();       
        lbldtmsg.Text = string.Empty;
    }
    protected void imb_SubmitType_Click(object sender, EventArgs e)
    {
        try
        {
            DeliveryType type = new DeliveryType();
            type.Name = txt_Type.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(h_dtId.Value) ? "0" : h_dtId.Value);
            if (id > 0)
            {
                bool exists = DeliveryTypeBAL.CheckbyIdUpdate(id, txt_Type.Text.Trim());
                if (!exists)
                {
                    type.ID = id;
                    DeliveryTypeBAL.TypeUpdate(type);
                    lbldtmsg.Text = "Item Updated Successfully.";
                    lbldtmsg.ForeColor = System.Drawing.Color.Green;
                    HideDelivery();
                    ccdDeliveryType.SelectedValue = id.ToString();
                    h_dtId.Value = "0";
                    txt_Type.Text = string.Empty;
                }
                else
                {
                    lbldtmsg.Text = "Item already exists.";
                    lbldtmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = DeliveryTypeBAL.CheckExists(txt_Type.Text.Trim());
                if (!exists)
                {
                    DeliveryTypeBAL.AddTypes(type);
                    lbldtmsg.Text = "Item added successfully.";
                    lbldtmsg.ForeColor = System.Drawing.Color.Green;
                    HideDelivery();
                    ccdDeliveryType.SelectedValue = type.ID.ToString();
                    h_dtId.Value = "0";
                    txt_Type.Text = string.Empty;
                }
                else
                {
                    lbldtmsg.Text = "Item already exists.";
                    lbldtmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditType_Click(object sender, EventArgs e)
    {
        try
        {
            DeliveryType type = DeliveryTypeBAL.SelectbyId(int.Parse(ddl_Type.SelectedValue));
            if (type != null)
            {
                txt_Type.Text = type.Name;
                h_dtId.Value = type.ID.ToString();
                ShowDelivery();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteType_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Type.SelectedValue != "0")
            {
                DeliveryTypeBAL.TypeDelete(int.Parse(ddl_Type.SelectedValue));
                lbldtmsg.Text = "Item Deleted Successfully.";
                lbldtmsg.ForeColor = System.Drawing.Color.Green;               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region Condition
  
    #region Hide Condition Controls
    private void HideCondition()
    {
        ddl_Condition.Visible = true;
        txt_Condition.Visible = false;
        imb_SubmitCondition.Visible = false;
        imb_CancelCondition.Visible = false;
        imb_AddCondition.Visible = true;
        imb_DeleteCondition.Visible = true;
        imb_EditCondition.Visible = true;
    }
    #endregion
    #region Show Condition Controls
    private void ShowCondition()
    {
        ddl_Condition.Visible = false;
        txt_Condition.Visible = true;
        imb_SubmitCondition.Visible = true;
        imb_CancelCondition.Visible = true;
        imb_AddCondition.Visible = false;
        imb_DeleteCondition.Visible = false;
        imb_EditCondition.Visible = false;
        lblcmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_AddCondition_Click(object sender, EventArgs e)
    {
        ShowCondition();
        txt_Condition.Text = string.Empty;
    }
    protected void imb_CancelCondition_Click(object sender, EventArgs e)
    {
        HideCondition();       
        lblcmsg.Text = string.Empty;
    }
    protected void imb_SubmitCondition_Click(object sender, EventArgs e)
    {
        try
        {
            Condition c = new Condition();
            c.Name = txt_Condition.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(h_cID.Value) ? "0" : h_cID.Value);
            if (id > 0)
            {
                bool exists = ConditionBAL.CheckbyIdUpdate(id, txt_Condition.Text.Trim());
                if (!exists)
                {
                    c.ID = id;
                    ConditionBAL.ConditionUpdate(c);
                    lblcmsg.Text = "Item Updated Successfully.";
                    lblcmsg.ForeColor = System.Drawing.Color.Green;
                    HideCondition();
                    ccdCondition.SelectedValue = id.ToString();
                    h_cID.Value = "0";
                    txt_Condition.Text = string.Empty;
                }
                else
                {
                    lblcmsg.Text = "Item Already Exists.";
                    lblcmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = ConditionBAL.CheckExists(txt_Condition.Text.Trim());
                if (!exists)
                {
                    ConditionBAL.AddConditions(c);
                    lblcmsg.Text = "Item Added Successfully.";
                    lblcmsg.ForeColor = System.Drawing.Color.Green;
                    HideCondition();
                    ccdCondition.SelectedValue = c.ID.ToString();
                    h_cID.Value = "0";
                    txt_Condition.Text = string.Empty;
                }
                else
                {
                    lblcmsg.Text = "Item Already Exists.";
                    lblcmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditCondition_Click(object sender, EventArgs e)
    {
        try
        {
            Condition c = ConditionBAL.SelectbyId(int.Parse(ddl_Condition.SelectedValue));
            if (c != null)
            {
                txt_Condition.Text = c.Name;
                h_cID.Value = c.ID.ToString();
                ShowCondition();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteCondition_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Condition.SelectedValue != "0")
            {
                ConditionBAL.ConditionDelete(int.Parse(ddl_Condition.SelectedValue));
                lblcmsg.Text = "Item Deleted Successfully.";
                lblcmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
 
    #region Delivery Defaults
    #region Bind Delivery Defaults
    private void BindDeliveryDefaults()
    {
        try
        {
            DeliveryDefault d = DeliveryDefaultsBAL.BindDefaults();
            if (d != null)
            {
                txt_Weight.Text = Convert.ToString(d.Weight);
                txt_Date.Text = Convert.ToString(d.OverdueDays);
                txt_SCharges.Text = Convert.ToString(d.StorageCharges);
                txt_ACperiod.Text = Convert.ToString(d.AutoClosePeriod);
                txtNotice.Text = HttpUtility.HtmlDecode(d.HeavyItemNotice);
                imb_AddDefaults.Visible = false;
                imb_EditDefaults.Visible = true;
            }
            else
            {
                imb_EditDefaults.Visible = false;
                imb_AddDefaults.Visible = true;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    #region Hide Default Controls
    private void HideDefault()
    {
       txt_Date.Enabled = false;
        txt_Weight.Enabled = false;
        txt_SCharges.Enabled = false;
        txt_ACperiod.Enabled = false;
        txtNotice.Enabled = false;
        imb_SubmitDefaults.Visible = false;
        imb_CancelDefaults.Visible = false;
       // imb_AddDefaults.Visible = true;       
       //imb_EditDefaults.Visible = true;
    }
    #endregion
    #region Show Default Controls
    private void ShowDefaults()
    {
       txt_Date.Enabled = true;
        txt_Weight.Enabled = true;
        txt_SCharges.Enabled = true;
        txt_ACperiod.Enabled = true;
        txtNotice.Enabled = true;
        imb_SubmitDefaults.Visible = true;
        imb_CancelDefaults.Visible = true;
        imb_AddDefaults.Visible = false;        
        imb_EditDefaults.Visible = false;
        lbldmsg.Text = string.Empty;

    }
    #endregion       
    protected void imb_AddDefaults_Click(object sender, EventArgs e)
    {
        ShowDefaults();
    }
    protected void imb_SubmitDefaults_Click(object sender, EventArgs e)
    {
        try
        {
            DeliveryDefault d = new DeliveryDefault();
            d.Weight = int.Parse(txt_Weight.Text);
            d.OverdueDays = int.Parse(txt_Date.Text);
            d.StorageCharges = Convert.ToDecimal(txt_SCharges.Text);
            d.AutoClosePeriod = int.Parse(txt_ACperiod.Text);
            d.HeavyItemNotice = HttpUtility.HtmlEncode(txtNotice.Text);
            int count = DeliveryDefaultsBAL.CheckExists();
            if (count > 0)
            {
                DeliveryDefaultsBAL.DefaultsUpdate(d);
                lbldmsg.Text = "Details Updated Successfully.";
                lbldmsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                DeliveryDefaultsBAL.AddDefault(d);
                lbldmsg.Text = "Details Added Successfully.";
                lbldmsg.ForeColor = System.Drawing.Color.Green;
            }
            HideDefault();
            BindDeliveryDefaults();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditDefaults_Click(object sender, EventArgs e)
    {
        ShowDefaults();
    }
    protected void imb_CancelDefaults_Click(object sender, EventArgs e)
    {
        HideDefault();
        lbldmsg.Text = string.Empty;
        BindDeliveryDefaults();
    }
    #endregion
   
}