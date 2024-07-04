using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.BLL;
using Deffinity.BE;
using Deffinity.Bindings;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class RFI_VendorSites_page : System.Web.UI.Page
{
    DisBindings getdata = new DisBindings();
    RFI_VendorSites _vendorsites = new RFI_VendorSites();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
       // Master.PageHead = "Vendor Account: Sites";
        
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIVendorContacts.aspx?VendorID=" + QueryStringValues.Vendor.ToString(), true);
    }

    protected void imgbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIVendorOverview.aspx?VendorID=" + QueryStringValues.Vendor.ToString(), false);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int i = 0;
        GridView gvtemp = new GridView();
        if (e.CommandName == "AddSite")
        {
            try
            {
                TextBox txtsitename = (TextBox)GridView1.FooterRow.FindControl("txtSitename1");
                TextBox txtaddress = (TextBox)GridView1.FooterRow.FindControl("txtAddress1");
                TextBox txtpostcode = (TextBox)GridView1.FooterRow.FindControl("txtPostcode1");
                TextBox txtswitchno = (TextBox)GridView1.FooterRow.FindControl("txtSwitchno1");

                _vendorsites.VENDORID = QueryStringValues.Vendor;
                if (txtswitchno.Text == "")
                {
                    _vendorsites.SWITCHBOARDNUMBER = "0";
                }
                else
                {
                    _vendorsites.SWITCHBOARDNUMBER = txtswitchno.Text.Trim();
                }
                _vendorsites.VENDORSITEID = 0;
                _vendorsites.SITEID = 0;// Convert.ToInt32(ddlsite.SelectedValue);
                _vendorsites.SITENAME = txtsitename.Text.Trim();
                _vendorsites.POSTCODE = txtpostcode.Text.Trim();
                _vendorsites.ADDRESS = txtaddress.Text.Trim();
                //_vendorsites.CITYID = Convert.ToInt32(ddlcity.SelectedValue); //Convert.ToInt32(HIDCity.Value);

                i = RFI_VendorSites_Base_SVC.Insert(_vendorsites);
                if (i == 2)
                {

                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Site added successfully";
                    objds_grid.DataBind();
                    GridView1.DataBind();
                }
                else if (i == 1)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Site already exists";
                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
         
        else if (e.CommandName == "Delete")
        {
            int vendorsiteid = Convert.ToInt32(e.CommandArgument);
            if (RFI_VendorSites_Base_SVC.Delete(vendorsiteid) == -1)
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Site Deleted";                
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Cannot delete site";
            }
           
        }
        else if (e.CommandName == "Cancel")
        {
            TextBox txtsitename = (TextBox)GridView1.FooterRow.FindControl("txtSitename1");
            TextBox txtaddress = (TextBox)GridView1.FooterRow.FindControl("txtAddress1");
            TextBox txtpostcode = (TextBox)GridView1.FooterRow.FindControl("txtPostcode1");
            TextBox txtswitchno = (TextBox)GridView1.FooterRow.FindControl("txtSwitchno1");

            txtsitename.Text = "";
            txtaddress.Text = "";
            txtpostcode.Text = "";
            txtswitchno.Text = "";

        }

        //objds_grid.DataBind();
        //GridView1.DataBind();
    }   
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            Label lblvendorid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblvendorid");
            TextBox txtsitename = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSitename");
            TextBox txtaddress = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAddress");
            TextBox txtpostcode = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPostcode");
            TextBox txtswno = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSwitchno");

            _vendorsites.VENDORID = QueryStringValues.Vendor;
            _vendorsites.SWITCHBOARDNUMBER = (txtswno.Text == "" ? string.Empty: txtswno.Text).ToString();
            _vendorsites.VENDORSITEID = Convert.ToInt32(e.Keys["VENDORSITEID"]);
            _vendorsites.SITEID = 0;// Convert.ToInt32(ddlsite.SelectedValue);
            _vendorsites.SITENAME = txtsitename.Text.Trim();
            _vendorsites.POSTCODE = txtpostcode.Text.Trim();
            _vendorsites.ADDRESS = txtaddress.Text.Trim();
            RFI_VendorSites_Base_SVC.Update(_vendorsites);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //Check if there is any exception while deleting
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
        }
    }
}
