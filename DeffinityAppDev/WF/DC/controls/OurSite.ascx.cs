using System;
using System.Web.UI;
using DC.Entity;
using DC.BLL;
using PortfolioMgt.DAL;
using DC.DAL;
using System.Linq;

using System.Collections.Generic;

public partial class DC_controls_OurSite : System.Web.DynamicData.FieldTemplateUserControl {

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hide();
            DefaultData d = DefaultDataBAL.SiteDefaultData_select();
            if (d != null)
            {
                ccdOurSite.SelectedValue = d.SiteID.ToString();
                chksite.Checked = true;
                //chksite.Text = " Default site data";
            }
        }
        //CopyToAllCustomer show only for fls or service desk requesters
        if (Request.QueryString["tab"] != null)
        {
            //if (Request.QueryString["tab"].ToString().ToLower() == "fls")
            //    btnCopyToAllCustomers.Visible = true;
            //else
            //    sessionKeys.PortfolioID = 0;
        }
    }
    #region Hide Controls
    private void Hide()
    {
        ddlOurSite.Visible = true;
        txtOurSite.Visible = false;
        imb_SubmitSite.Visible = false;
        imb_CancelSite.Visible = false;
        imb_AddSite.Visible = true;
        imb_DeleteSite.Visible = true;
        imb_EditSite.Visible = true;
    }
    #endregion
    #region Show Controls
    private void Show()
    {
        ddlOurSite.Visible = false;
        txtOurSite.Visible = true;
        imb_SubmitSite.Visible = true;
        imb_CancelSite.Visible = true;
        imb_AddSite.Visible = false;
        imb_DeleteSite.Visible = false;
        imb_EditSite.Visible = false;
        lblmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_DeleteSite_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlOurSite.SelectedValue != "0")
            {
                OurSiteBAL.DeleteOurSite(int.Parse(ddlOurSite.SelectedValue));
                lblmsg.Text = "Site deleted successfully";
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                Getstyles();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_AddSite_Click(object sender, EventArgs e)
    {
        Show();
        txtOurSite.Text = string.Empty;
        chksite.Visible = false;
    }
    protected void imb_SubmitSite_Click(object sender, EventArgs e)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;

            OurSite site = new OurSite();
            site.Name = txtOurSite.Text.Trim();
            site.CustomerID = customerId;
            int id = int.Parse(string.IsNullOrEmpty(h_sId.Value) ? "0" : h_sId.Value);
            if (id > 0)
            {
                bool exists = OurSiteBAL.CheckbyIdUpdate(id, txtOurSite.Text.Trim(), customerId);
                if (!exists)
                {
                    site.ID = id;
                    OurSiteBAL.UpdateOurSite(site);
                    lblmsg.Text = "Site updated successfully";
                    //lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ccdOurSite.SelectedValue = id.ToString();
                    h_sId.Value = "0";
                    txtOurSite.Text = string.Empty;
                }
                else
                {
                    lblerror.Text = "Site already exists";
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = OurSiteBAL.CheckExists(txtOurSite.Text.Trim(),customerId);
                if (!exists)
                {
                    OurSiteBAL.AddOurSite(site);
                    lblmsg.Text = "Site added successfully";
                    //lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ccdOurSite.SelectedValue = site.ID.ToString();
                    h_sId.Value = "0";
                    txtOurSite.Text = string.Empty;
                    chksite.Checked = false;
                    //chksite.Text = " Select checkbox for default selection";

                }
                else
                {
                    lblerror.Text = "Site already exists";
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            chksite.Visible = true;
            Getstyles();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditSite_Click(object sender, EventArgs e)
    {
        try
        {
            OurSite site = OurSiteBAL.SelectbyId(int.Parse(ddlOurSite.SelectedValue));
            if (site != null)
            {
                txtOurSite.Text = site.Name;
                h_sId.Value = site.ID.ToString();
                Show();
            }
            chksite.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_CancelSite_Click(object sender, EventArgs e)
    {
        Hide();
        lblmsg.Text = string.Empty;
        chksite.Visible = true;
    }

    private void Getstyles()
    {
        if (!string.IsNullOrEmpty(lblmsg.Text))
        {
            lblmsg.Visible = true;
        }
        else
        {
            lblmsg.Visible = false;
            lblmsg.Text = string.Empty;
        }
    }
    protected void chksite_CheckedChanged(object sender, EventArgs e)
    {
        DefaultData dd = new DefaultData();
        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
        if (chksite.Checked)
        {
            
            if (ddlOurSite.SelectedValue == "" || ddlOurSite.SelectedValue == "0")
            {
                lblerror.Text = "Please select site";
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                chksite.Checked = false;
            }
            else
            {
                
                if (d == null)
                {
                    dd.SiteID = int.Parse(ddlOurSite.SelectedValue);
                    DefaultDataBAL.SiteDefaultData_Insert(dd);
                }
                else
                {
                    d.SiteID = int.Parse(ddlOurSite.SelectedValue);
                    DefaultDataBAL.SiteDefaultData_update(d);
                }
                lblmsg.Text = "Site successfully set as default";
                //lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        else
        {
            if (d != null)
            {
                DefaultDataBAL.SiteDefaultData_Delete(d.ID);
            }
        }
        Getstyles();
    }
    protected void ddlOurSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultData d = DefaultDataBAL.SiteDefaultData_select();
        if (d != null)
        {
            if (ddlOurSite.SelectedValue == "" || ddlOurSite.SelectedValue == "0")
            {
                //chksite.Checked = false;
            }
            else
            {

                if (d.SiteID == int.Parse(ddlOurSite.SelectedValue))
                {
                    chksite.Checked = true;
                }
                else
                {
                    chksite.Checked = false;
                }
                lblmsg.Text = string.Empty;
            }
        }
        Getstyles();
    }
    protected void btnCopyToAllCustomers_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext dc = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    var siteList = dc.OurSites.Where(c => c.CustomerID == customerID).ToList();
                    if (siteList.Count() > 0)
                    {

                        List<OurSite> sList = new List<OurSite>();
                        foreach (var c in customerList)
                        {
                            foreach (var s in siteList)
                            {
                                bool exists = OurSiteBAL.CheckExists(s.Name, c.ID);
                                if (!exists)
                                {
                                    OurSite ourSite = new OurSite();
                                    ourSite.CustomerID = c.ID;
                                    ourSite.Name = s.Name;
                                    sList.Add(ourSite);
                                   
                                }
                            }
                        }
                        //Bulk insert
                        dc.OurSites.InsertAllOnSubmit(sList);
                        dc.SubmitChanges();
                        lblmsg.Text = "Successfully copied";
                    }
                }
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
            if (ddlOurSite.SelectedValue != "0")
            {
                OurSiteBAL.DeleteOurSite(int.Parse(ddlOurSite.SelectedValue));
                lblmsg.Text = "Site deleted successfully";
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                Getstyles();
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
            if (ddlOurSite.SelectedValue != "0")
            {
                OurSiteBAL O_BAL = new OurSiteBAL();
                O_BAL.DeteleSitetoAllCustomers(ddlOurSite.SelectedItem.ToString());
                lblmsg.Text = "Site deleted to all customers successfully";
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                Getstyles();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
