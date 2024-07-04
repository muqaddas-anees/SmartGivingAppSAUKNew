using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.BLL;
using DC.DAL;
using PortfolioMgt.DAL;

public partial class DC_controls_SourceOfRequestCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hide();
        }
    }

    #region Hide Controls
    private void Hide()
    {
        ddlSourceOfRequest.Visible = true;
        txtSourceOfRequest.Visible = false;
        imb_Submit.Visible = false;
        imb_Cancel.Visible = false;
        imb_Add.Visible = true;
        imb_Delete.Visible = true;
        imb_Edit.Visible = true;

    }
    #endregion

    #region Show Controls
    private void Show()
    {
        ddlSourceOfRequest.Visible = false;
        txtSourceOfRequest.Visible = true;
        imb_Submit.Visible = true;
        imb_Cancel.Visible = true;
        imb_Add.Visible = false;
        imb_Delete.Visible = false;
        imb_Edit.Visible = false;
        lblMsg.Text = string.Empty;

    }
    #endregion

    protected void imb_Add_Click(object sender, EventArgs e)
    {
        Show();
        txtSourceOfRequest.Text = string.Empty;
    }



    protected void imb_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;
            FLSSourceOfRequest flsSourceOfRequest = new FLSSourceOfRequest();
            flsSourceOfRequest.Name = txtSourceOfRequest.Text.Trim();
            flsSourceOfRequest.CustomerID = customerId;
            int id = int.Parse(string.IsNullOrEmpty(hfId.Value) ? "0" : hfId.Value);
            if (id > 0)
            {
                bool exists = SourceOfRequestBAL.CheckSourceOfRequest(id, txtSourceOfRequest.Text.Trim(), customerId);
                if (!exists)
                {
                    flsSourceOfRequest.ID = id;
                    SourceOfRequestBAL.UpdateSourceOfRequest(flsSourceOfRequest);
                    lblMsg.Text = "Source of Request updated successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ddlSourceOfRequest.SelectedValue = id.ToString();
                    hfId.Value = "0";
                    txtSourceOfRequest.Text = string.Empty;
                }
                else
                {
                    lblError.Text = "Source of Request already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = SourceOfRequestBAL.CheckSourceOfRequest(txtSourceOfRequest.Text.Trim(), customerId);
                if (!exists)
                {
                    SourceOfRequestBAL.AddSourceOfRequest(flsSourceOfRequest);
                    lblMsg.Text = "Source of Request added successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ddlSourceOfRequest.SelectedValue = flsSourceOfRequest.ID.ToString();
                    hfId.Value = "0";
                    txtSourceOfRequest.Text = string.Empty;


                }
                else
                {
                    lblError.Text = "Source of Request already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            FLSSourceOfRequest flsSourceOfRequest = SourceOfRequestBAL.SelectByID(int.Parse(ddlSourceOfRequest.SelectedValue));
            if (flsSourceOfRequest != null)
            {
                txtSourceOfRequest.Text = flsSourceOfRequest.Name;
                hfId.Value = flsSourceOfRequest.ID.ToString();
                Show();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Cancel_Click(object sender, EventArgs e)
    {
        Hide();
        lblMsg.Text = string.Empty;
        hfId.Value = "0";
    }

    protected void imb_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSourceOfRequest.SelectedValue != "0")
            {
                SourceOfRequestBAL.DeleteByID(int.Parse(ddlSourceOfRequest.SelectedValue));
                lblMsg.Text = "Source of Request deleted successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
                    var sourceOfRequestList = dc.FLSSourceOfRequests.Where(c => c.CustomerID == customerID).ToList();
                    if (sourceOfRequestList.Count() > 0)
                    {
                        List<FLSSourceOfRequest> sList = new List<FLSSourceOfRequest>();
                        foreach (var c in customerList)
                        {
                            foreach (var s in sourceOfRequestList)
                            {
                                bool exists = SourceOfRequestBAL.CheckSourceOfRequest(s.Name, c.ID);
                                if (!exists)
                                {
                                    FLSSourceOfRequest flsSourceOfRequest = new FLSSourceOfRequest();
                                    flsSourceOfRequest.CustomerID = c.ID;
                                    flsSourceOfRequest.Name = s.Name;
                                    sList.Add(flsSourceOfRequest);
                                }
                            }
                        }
                        //Bulk insert
                        dc.FLSSourceOfRequests.InsertAllOnSubmit(sList);
                        dc.SubmitChanges();
                        lblMsg.Text = "Successfully copied";
                    }
                }
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
            if (ddlSourceOfRequest.SelectedValue != "0")
            {
                SourceOfRequestBAL.DeteleSRtoAllCustomers(ddlSourceOfRequest.SelectedItem.ToString());
                lblMsg.Text = "Source of Request deleted to all customers successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

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
            if (ddlSourceOfRequest.SelectedValue != "0")
            {
                SourceOfRequestBAL.DeleteByID(int.Parse(ddlSourceOfRequest.SelectedValue));
                lblMsg.Text = "Source of Request deleted successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}