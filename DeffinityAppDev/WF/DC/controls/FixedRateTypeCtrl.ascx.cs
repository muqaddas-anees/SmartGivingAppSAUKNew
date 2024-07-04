using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.DAL;
using PortfolioMgt.DAL;
using UserMgt.DAL;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class FixedRateTypeCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindControls();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #region Bind Subject
        public void BindControls()
        {
            try
            {
                ddlSubject.DataSource = FixedRateTypeBAL.Bind().ToList();
                ddlSubject.DataTextField = "FixedRateTypeName";
                ddlSubject.DataValueField = "FixedRateTypeID";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion


        private void HideControls(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
        {
            _ddlItems.Visible = true;
            _txtEdit.Visible = false;
            _imgSubmit.Visible = false;
            _imgCancel.Visible = false;
            _imgAdd.Visible = true;
            _imgDelete.Visible = true;
            _imgEdit.Visible = true;


        }

        private void VisibleControl(TextBox _txtEdit, DropDownList _ddlItems, LinkButton _imgSubmit, LinkButton _imgAdd, LinkButton _imgEdit, LinkButton _imgCancel, LinkButton _imgDelete)
        {
            _ddlItems.Visible = false;
            _txtEdit.Visible = true;
            _imgSubmit.Visible = true;
            _imgCancel.Visible = true;
            _imgAdd.Visible = false;
            _imgDelete.Visible = false;
            _imgEdit.Visible = false;
            lblSuccessServicetype.Text = string.Empty;
            lblErrorServicetype.Text = string.Empty;

        }

        protected void btndeleteSubject_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubject.SelectedValue != "0")
                {
                    int subjectid = int.Parse(ddlSubject.SelectedValue);
                    //check the suject is assigned already
                    if (!Subject_IsAssined(subjectid))
                    {
                        FixedRateTypeBAL.DeleteById(subjectid);
                        lblSuccessServicetype.Text = "Item deleted successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindControls();
                    }
                    else
                    {
                        lblErrorServicetype.Text = "Item assigned to request(s).Please check and try again";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblErrorServicetype.Text = "Please select service type";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private bool Subject_IsAssined(int subjectid)
        {
            //int retval = 0;
            //using (DCDataContext dcontext = new DCDataContext())
            //{
            //    retval = (from p in dcontext.FLSDetails
            //              where p.SubjectID == subjectid
            //              select p).Count();
            //}
            //return retval > 0 ? true : false;
            return false;
        }


        protected void btnaddSubject_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubject.Text = string.Empty;
                VisibleControl(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                //Panel_portfolio.Visible = false;

                //Panel2.Visible = false;
                txtSubject.Focus();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btneditSubject_Click(object sender, EventArgs e)
        {
            try
            {
                var subject = FixedRateTypeBAL.SelectById(int.Parse(ddlSubject.SelectedValue));

                if (subject != null)
                {
                    txtSubject.Text = subject.FixedRateTypeName;
                    hid.Value = subject.FixedRateTypeID.ToString();
                    VisibleControl(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                    //Panel_portfolio.Visible = false;

                    //Panel2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnsubmitSubject_Click(object sender, EventArgs e)
        {
            try
            {
                int customerID = sessionKeys.PortfolioID;
                FixedRateType subject = new FixedRateType();
                subject.FixedRateTypeName = txtSubject.Text.Trim();
                //ubject.CustomerID = customerID;

                int id = int.Parse(string.IsNullOrEmpty(hid.Value) ? "0" : hid.Value);
                if (id > 0)
                {
                    bool exists = FixedRateTypeBAL.CheckByIdUpdate(id, txtSubject.Text.Trim(), customerID);
                    if (!exists)
                    {
                        subject.FixedRateTypeID = id;
                        FixedRateTypeBAL.Update(subject);
                        lblSuccessServicetype.Text = "Item updated successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;

                        HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                        BindControls();
                        hid.Value = "0";
                        txtSubject.Text = string.Empty;
                    }
                    else
                    {
                        lblErrorServicetype.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    bool exists = FixedRateTypeBAL.CheckExists(txtSubject.Text.Trim(), customerID);

                    if (!exists)
                    {
                        FixedRateTypeBAL.Add(subject);
                        lblSuccessServicetype.Text = "Item added successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                        BindControls();
                        hid.Value = "0";
                        txtSubject.Text = string.Empty;
                    }
                    else
                    {
                        lblErrorServicetype.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btncancelSubject_Click(object sender, EventArgs e)
        {
            try
            {
                HideControls(txtSubject, ddlSubject, btnsubmitSubject, btnaddSubject, btneditSubject, btncancelSubject, btndeleteSubject);
                BindControls();
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
                if (ddlSubject.SelectedValue != "0")
                {
                    int subjectid = int.Parse(ddlSubject.SelectedValue);
                    //check the suject is assigned already
                    if (!Subject_IsAssined(subjectid))
                    {
                        FixedRateTypeBAL.DeleteById(subjectid);
                        lblSuccessServicetype.Text = "Item deleted successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindControls();
                    }
                    else
                    {
                        lblErrorServicetype.Text = "Item assigned to request(s).Please check and try again";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblErrorServicetype.Text = "Please select service type";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


    }
   
}