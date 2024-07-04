using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class spSchedulingCtrl : System.Web.UI.UserControl
    {
        IDCRespository<ServiceProviderScheduling> spRepository = null;
        IDCRespository<CriteriaAndWeighting> cwRepository = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDefaultValues();
                BindListData();
            }
        }

        private void BindDefaultValues()
        {
            try
            {
                spRepository = new DCRepository<ServiceProviderScheduling>();
                var sentity = spRepository.GetAll().FirstOrDefault();
                if (sentity != null)
                {
                    if (sentity.SchedulType == "Blast")
                    {
                        rdBlast.Checked = true;
                        txtIntialBatchQty.Text = sentity.InitialBatchQty.HasValue ? sentity.InitialBatchQty.Value.ToString() : string.Empty;
                        txtMinBeforeNextBlast.Text = sentity.MinBeforeNextBlast.HasValue ? sentity.MinBeforeNextBlast.Value.ToString() : string.Empty;
                        txtSecondBatchQty.Text = sentity.SecondBatchQty.HasValue ? sentity.SecondBatchQty.Value.ToString() : string.Empty;
                    }
                    else
                    {
                        rdSequence.Checked = true;
                        txtSequenceInterval.Text = sentity.SequenceInterval.HasValue ? sentity.SequenceInterval.Value.ToString() : string.Empty;
                    }


                }
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                spRepository = new DCRepository<ServiceProviderScheduling>();
                var sentity = spRepository.GetAll().FirstOrDefault();
                if (sentity != null)
                {
                    if (rdBlast.Checked)
                    {
                        sentity.SchedulType = "Blast";
                        sentity.InitialBatchQty = Convert.ToInt32(!string.IsNullOrEmpty(txtIntialBatchQty.Text) ? txtIntialBatchQty.Text.Trim() : "0");
                        sentity.MinBeforeNextBlast = Convert.ToInt32(!string.IsNullOrEmpty(txtMinBeforeNextBlast.Text) ? txtMinBeforeNextBlast.Text.Trim() : "0");
                        sentity.SecondBatchQty = Convert.ToInt32(!string.IsNullOrEmpty(txtSecondBatchQty.Text) ? txtSecondBatchQty.Text.Trim() : "0");
                    }
                    else
                    {
                        sentity.SchedulType = "Sequence";
                        sentity.SequenceInterval = Convert.ToInt32(!string.IsNullOrEmpty(txtSequenceInterval.Text) ? txtSequenceInterval.Text.Trim() : "0");
                    }
                    spRepository.Edit(sentity);
                }
                else
                {
                    sentity = new ServiceProviderScheduling();
                    if (rdBlast.Checked)
                    {
                        sentity.SchedulType = "Blast";
                        sentity.InitialBatchQty = Convert.ToInt32(!string.IsNullOrEmpty(txtIntialBatchQty.Text) ? txtIntialBatchQty.Text.Trim() : "0");
                        sentity.MinBeforeNextBlast = Convert.ToInt32(!string.IsNullOrEmpty(txtMinBeforeNextBlast.Text) ? txtMinBeforeNextBlast.Text.Trim() : "0");
                        sentity.SecondBatchQty = Convert.ToInt32(!string.IsNullOrEmpty(txtSecondBatchQty.Text) ? txtSecondBatchQty.Text.Trim() : "0");
                    }
                    else
                    {
                        sentity.SchedulType = "Sequence";
                        sentity.SequenceInterval = Convert.ToInt32(!string.IsNullOrEmpty(txtSequenceInterval.Text) ? txtSequenceInterval.Text.Trim() : "0");
                    }
                    spRepository.Add(sentity);

                }
                lblsuccessmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                BindDefaultValues();
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        private void BindListData()
        {
            cwRepository = new DCRepository<CriteriaAndWeighting>();
            var rlist = cwRepository.GetAll().ToList();
            listCriteria.DataSource = rlist;
            listCriteria.DataBind();
        }
        protected void listCriteria_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                cwRepository = new DCRepository<CriteriaAndWeighting>();
                if (e.CommandName == "Edit")
                {
                    //var dc = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    //DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                    //DropDownList ddlmake = (DropDownList)e.Item.FindControl("ddlMake");
                    //ddltype.DataSource = BindType().OrderBy(o => o.Type).ToList();
                    //ddltype.DataTextField = "Type";
                    //ddltype.DataValueField = "TypeID";
                    //ddltype.DataBind();

                    //ddltype.Items.Insert(0, new ListItem("Please select...", "0"));

                    //ddltype.SelectedValue = dc.ProductTypeID.Value.ToString();

                    //ddlmake.DataSource = BindMake().OrderBy(o => o.Make).ToList();
                    //ddlmake.DataTextField = "Make";
                    //ddlmake.DataValueField = "MakeID";
                    //ddlmake.DataBind();
                    //ddlmake.Items.Insert(0, new ListItem("Please select...", "0"));
                    //ddlmake.SelectedValue = dc.MakeID.ToString();

                }
                else if (e.CommandName == "UpdateItem")
                {
                    var dc = cwRepository.GetAll().Where(o => o.CWID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    TextBox txtCriteria = (TextBox)e.Item.FindControl("txtCriteria_e");
                    TextBox txtWeighting = (TextBox)e.Item.FindControl("txtWeighting_e");
                    CheckBox ck = (CheckBox)e.Item.FindControl("chkSelected_e");

                    if (cwRepository.GetAll().Where(p => p.CWID != Convert.ToInt32(e.CommandArgument.ToString()) && p.Criteria.ToLower() == txtCriteria.Text.ToLower()).Count() == 0)
                    {
                        dc.Criteria = txtCriteria.Text.Trim();
                        dc.Weighting = Convert.ToDouble(txtWeighting.Text.Trim());
                        dc.IsEnable = ck.Checked;
                        cwRepository.Edit(dc);
                        lblMsg_a.Text = "Updated sucessfully";
                        listCriteria.EditIndex = -1;
                        BindListData();
                    }
                    else
                    {
                        lblError_a.Text = "Item already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else if (e.CommandName == "Add")
                {
                    //var dc = cwRepository.GetAll().Where(o => o.CWID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    TextBox txtCriteria = (TextBox)e.Item.FindControl("txtCriteria");
                    TextBox txtWeighting = (TextBox)e.Item.FindControl("txtWeighting");
                    CheckBox ck = (CheckBox)e.Item.FindControl("chkSelected");

                    if (cwRepository.GetAll().Where(p => p.Criteria.ToLower() == txtCriteria.Text.ToLower()).Count() == 0)
                    {

                        var cf = new CriteriaAndWeighting();
                        cf.Criteria = txtCriteria.Text.Trim();
                        cf.Weighting =  Convert.ToDouble( txtWeighting.Text.Trim());
                        cf.IsEnable = ck.Checked;
                        cwRepository.Add(cf);
                        lblMsg_a.Text = "Added sucessfully";
                        BindListData();
                    }
                    else
                    {
                        lblError_a.Text = "item already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else if (e.CommandName == "Del")
                {
                    //cb = new CustomFieldsBAL();
                    if (Convert.ToInt32(e.CommandArgument) > 0)
                    {
                        var dc = cwRepository.GetAll().Where(o => o.CWID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                        cwRepository.Delete(dc);
                        lblMsg_a.Text = "Deleted sucessfully";
                        BindListData();
                    }
                }
                else if (e.CommandName == "Cancel")
                {

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void listCriteria_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            listCriteria.EditIndex = -1;
            //BindCustomFields();
        }
        protected void listCriteria_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            listCriteria.EditIndex = e.NewEditIndex;
            BindListData();
        }
        protected void listCriteria_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //Label lblID = (Label)e.Item.FindControl("lblID");//lblID


                //DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                //DropDownList ddlmake = (DropDownList)e.Item.FindControl("ddlMake");
                //AjaxControlToolkit.CascadingDropDown ctype = (AjaxControlToolkit.CascadingDropDown)e.Item.FindControl("ccdCategoryNew");
                //AjaxControlToolkit.CascadingDropDown cmake = (AjaxControlToolkit.CascadingDropDown)e.Item.FindControl("ccdSubCategoryNew");
                //if (ddltype != null)
                //{
                //    IUserRepository<UserMgt.Entity.UserAssociateToType> pRepository = new UserRepository<UserMgt.Entity.UserAssociateToType>();
                //    var dc = pRepository.GetAll().Where(o => o.UATID == Convert.ToInt32(lblID.Text)).FirstOrDefault();

                //    //ddltype.DataSource = BindType().OrderBy(o => o.Type).ToList();
                //    //ddltype.DataTextField = "Type";
                //    //ddltype.DataValueField = "TypeID";
                //    //ddltype.DataBind();

                //    //ddltype.Items.Insert(0, new ListItem("Please select...", "0"));
                //    ctype.SelectedValue = dc.ProductTypeID.ToString();
                //    cmake.DataBind();
                //    cmake.SelectedValue = dc.MakeID.ToString();
                //    //ddltype.SelectedValue = dc.ProductTypeID.ToString();

                //    //ddlmake.DataSource = BindMake().OrderBy(o => o.Make).ToList();
                //    //ddlmake.DataTextField = "Make";
                //    //ddlmake.DataValueField = "MakeID";
                //    //ddlmake.DataBind();
                //    //ddlmake.Items.Insert(0, new ListItem("Please select...", "0"));
                //    //ddlmake.SelectedValue = dc.MakeID.ToString();
                //}
            }

        }
    }
}