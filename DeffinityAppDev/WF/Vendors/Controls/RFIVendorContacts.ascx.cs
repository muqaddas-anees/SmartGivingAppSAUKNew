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
using Deffinity.BLL;
using Deffinity.BE;

public partial class RFIVendorContacts : System.Web.UI.UserControl
{
    ContractorContacts contacts = new ContractorContacts();
    int ContractorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";

    }
    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {

    //        int ContractorId = ContractorContacts_Base_SVC.GetContractorId(QueryStringValues.Vendor);
    //        contacts.CONTRACTORID = ContractorId;
    //        contacts.NAME = txtName.Text.Trim();
    //        contacts.JOBTITLE=(txtTitle.Text == "" ? null : txtTitle.Text.Trim());
    //        contacts.EMAIL = TxtEmail.Text.Trim();
    //        contacts.TELEPHONE = (txtTelephone.Text == "" ? null : txtTelephone.Text.Trim());
    //        contacts.MOBILE = (txtMobile.Text == "" ? null : txtMobile.Text.Trim());
    //        int i = ContractorContacts_Base_SVC.Insert(contacts);
    //        if (i > 0)
    //        {
    //            lblmsg.Text = "Contact added successfully";
    //        }
    //        else
    //        {
    //            lblmsg.Text = "Cannot add contact ";
    //        }
    //        GridContactsInfo.DataBind();
    //        Clear();
    //     }

    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    protected void GridContactsInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            contacts.ID = Convert.ToInt32(e.Keys["ID"]);
            contacts.CONTRACTORID = ContractorContacts_Base_SVC.GetContractorId(QueryStringValues.Vendor);
            contacts.NAME = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtname")).Text;
            contacts.JOBTITLE = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txttitle")).Text;
            contacts.EMAIL = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtemail")).Text;
            contacts.TELEPHONE = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtcontactnumber")).Text;
            contacts.MOBILE = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtmobilenumber")).Text;
            int i = ContractorContacts_Base_SVC.Update(contacts);
            //if (i > 0)
            //{
            //    lblmsg.Text = "Contact updated successfully";
            //}
            //else
            //{
            //    lblmsg.Text = "Cannot update contact ";
            //}
            GridContactsInfo.DataBind();
            //Clear();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridContactsInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       int i= ContractorContacts_Base_SVC.Delete(Convert.ToInt32(e.Keys["ID"]));
       //if (i > 0)
       //{
       //    lblmsg.Text = "Contact deleted";           
       //}
       //else
       //{
       //    lblmsg.Text = "Cannot delete contact ";
       //}
    }
    protected void GridContactsInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridContactsInfo.EditIndex = e.NewEditIndex;

    }
    protected void GridContactsInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridContactsInfo.EditIndex = -1;
    }
    //protected void btnCanel_Click(object sender, ImageClickEventArgs e)
    //{
    //    Clear();
    //}
    //private void Clear()
    //{
    //    txtName.Text = "";
    //    TxtEmail.Text = "";
    //    txtTitle.Text = "";
    //    txtTelephone.Text = "";
    //    txtMobile.Text = "";
    //}
    protected void GridContactsInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //Check if there is any exception while deleting
        if (e.Exception != null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
            e.ExceptionHandled = true;
        }
    }
    protected void GridContactsInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddContact")
        {
            try
            {
                TextBox txtname = (TextBox)GridContactsInfo.FooterRow.FindControl("txtname1");
                TextBox txtposition = (TextBox)GridContactsInfo.FooterRow.FindControl("txttitle1");
                TextBox txtemailid = (TextBox)GridContactsInfo.FooterRow.FindControl("txtemail1");
                TextBox txttelephone = (TextBox)GridContactsInfo.FooterRow.FindControl("txtcontactnumber1");
                TextBox txtmobile = (TextBox)GridContactsInfo.FooterRow.FindControl("txtmobilenumber1");

                contacts.ID = 0;
                contacts.CONTRACTORID = ContractorContacts_Base_SVC.GetContractorId(QueryStringValues.Vendor);
                contacts.NAME = txtname.Text.Trim();
                contacts.JOBTITLE = txtposition.Text.Trim();
                contacts.EMAIL = txtemailid.Text.Trim();
                contacts.TELEPHONE = txttelephone.Text.Trim();
                contacts.MOBILE = txtmobile.Text.Trim();
                int i = ContractorContacts_Base_SVC.Insert(contacts);
                
                GridContactsInfo.DataBind();               
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        else if (e.CommandName == "Cancel")
        {
            TextBox txtname = (TextBox)GridContactsInfo.FooterRow.FindControl("txtname1");
            TextBox txtposition = (TextBox)GridContactsInfo.FooterRow.FindControl("txttitle1");
            TextBox txtemailid = (TextBox)GridContactsInfo.FooterRow.FindControl("txtemail1");
            TextBox txttelephone = (TextBox)GridContactsInfo.FooterRow.FindControl("txtcontactnumber1");
            TextBox txtmobile = (TextBox)GridContactsInfo.FooterRow.FindControl("txtmobilenumber1");

            txtname.Text = "";
            txtposition.Text = "";
            txtemailid.Text = "";
            txttelephone.Text = "";
            txtmobile.Text = "";
        }

    }
    protected void GridContactsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //check the empty row containg '-99' or not 
            //if yes then hide that row
            object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
            if (objList != null)
            {
                if (objList[0].ToString() == "-99")
                {
                    e.Row.Visible = false;
                }
            }

        }
    }
}
