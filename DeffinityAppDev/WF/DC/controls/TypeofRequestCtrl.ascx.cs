using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class TypeofRequestCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                HideTypeOfRequest();
            }
        }
       
       
        #region "Type of Request"
        protected void btnAddTypeOfRequest_Click(object sender, EventArgs e)
        {
            ShowTypeOfRequest();
            txtTypeOfRequest.Text = string.Empty;
        }
        protected void btnEditTypeOfRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestType.SelectedValue != "")
                {
                    TypeOfRequest typeOfRequest = TypeOfRequestBAL.SelectByID(int.Parse(ddlRequestType.SelectedValue));
                    if (typeOfRequest != null)
                    {
                        txtTypeOfRequest.Text = typeOfRequest.Name;
                        hfRequestTypeId.Value = typeOfRequest.ID.ToString();
                        ShowTypeOfRequest();
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnDeleteTypeOfRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestType.SelectedValue != "")
                {
                    TypeOfRequestBAL.DeleteByID(int.Parse(ddlRequestType.SelectedValue));
                    lblMsg.Text = "Type of Request deleted successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSaveTypeOfRequest_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = sessionKeys.PortfolioID;
                TypeOfRequest typeOfRequest = new TypeOfRequest();
                typeOfRequest.Name = txtTypeOfRequest.Text.Trim();
                typeOfRequest.CustomerID = customerId;
                int requestTypeId = int.Parse(string.IsNullOrEmpty(hfRequestTypeId.Value) ? "0" : hfRequestTypeId.Value);
                if (requestTypeId > 0)
                {
                    bool exists = TypeOfRequestBAL.CheckTypeOfRequest(requestTypeId, txtTypeOfRequest.Text.Trim(), customerId);
                    if (!exists)
                    {
                        typeOfRequest.ID = requestTypeId;
                        TypeOfRequestBAL.UpdateTypeOfRequest(typeOfRequest);
                        lblMsg.Text = "Updated successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        HideTypeOfRequest();

                        hfRequestTypeId.Value = "0";
                        txtTypeOfRequest.Text = string.Empty;
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    bool exists = TypeOfRequestBAL.CheckTypeOfRequest(txtTypeOfRequest.Text.Trim(), customerId);
                    if (!exists)
                    {
                        TypeOfRequestBAL.AddTypeOfRequest(typeOfRequest);
                        lblMsg.Text = "Added successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        HideTypeOfRequest();
                        hfRequestTypeId.Value = "0";
                        txtTypeOfRequest.Text = string.Empty;


                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCancelTypeOfRequest_Click(object sender, EventArgs e)
        {
            HideTypeOfRequest();
            lblMsg.Text = string.Empty;
            hfRequestTypeId.Value = "0";
        }
        #endregion

        private void HideTypeOfRequest()
        {
            txtTypeOfRequest.Visible = false;
            btnCancelTypeOfRequest.Visible = false;
            btnSaveTypeOfRequest.Visible = false;

            ddlRequestType.Visible = true;
            btnAddTypeOfRequest.Visible = true;
            btnEditTypeOfRequest.Visible = true;
            btnDeleteTypeOfRequest.Visible = true;
        }
        private void ShowTypeOfRequest()
        {
            txtTypeOfRequest.Visible = true;
            btnCancelTypeOfRequest.Visible = true;
            btnSaveTypeOfRequest.Visible = true;

            ddlRequestType.Visible = false;
            btnAddTypeOfRequest.Visible = false;
            btnEditTypeOfRequest.Visible = false;
            btnDeleteTypeOfRequest.Visible = false;
        }
    }
}