using DC.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class ProductAddonPricesCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                BindProductAddons();

                //listAppliances.FindControl
                
            }
        }

        private void BingPolicyType()
        {
            DropDownList ddl = (DropDownList)listAppliances.InsertItem.FindControl("ddlTypeF");
            if (ddl != null)
            {
                ddl.DataSource = ProductPolicyData();
                ddl.DataValueField = "ID";
                ddl.DataTextField = "Title";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Add On", "0"));
            }
        }

        private void BindProductAddons()
        {
            PolicyTypeBAL pb = new PolicyTypeBAL();
            var policyList = pb.PolicyType_SelectAll().ToList();

            IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
           
            var sResult = pRepository.GetAll().Where(o=>o.CustomerID == sessionKeys.PortfolioID).ToList();

            var cd = (from s in sResult
                      select new
                      {
                          s.PAPID,
                          s.AddOnDetails,
                          MontlyCost = string.Format("{0:F2}", s.MontlyCost),
                          YearlyCost = string.Format("{0:F2}",s.YearlyCost),
                          Ptype = GetPolicyType(s.ProductPolicyTypeID,policyList),
                          PtypeID = s.ProductPolicyTypeID.HasValue?s.ProductPolicyTypeID.Value:0 
                      }).ToList();

            listAppliances.DataSource = cd;
            listAppliances.DataBind();

            BingPolicyType();

        }
        private string GetPolicyType(int? typeid,List<PortfolioMgt.Entity.ProductPolicyType> policylist)
        {
            string retval = string.Empty;
            if (typeid.HasValue)
            {
                var p = policylist.Where(o => o.ID == typeid.Value).FirstOrDefault();
                if(p!= null)
                retval = p.Title;
               
            }
            
            return retval;
        }
        protected void listAppliances_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice> pRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPrice>();
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
                    var dc = pRepository.GetAll().Where(o => o.PAPID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    TextBox txtAddOnDetails = (TextBox)e.Item.FindControl("txtAddOnDetails");
                    TextBox txtMontlyCost = (TextBox)e.Item.FindControl("txtMontlyCost");
                    TextBox txtYearlyCost = (TextBox)e.Item.FindControl("txtYearlyCost");
                    DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlType");
                    dc.AddOnDetails = txtAddOnDetails.Text.Trim();
                    dc.MontlyCost = Convert.ToDouble(!string.IsNullOrEmpty(txtMontlyCost.Text.Trim())? txtMontlyCost.Text.Trim(): "0");
                    dc.YearlyCost = Convert.ToDouble(!string.IsNullOrEmpty(txtYearlyCost.Text.Trim()) ? txtYearlyCost.Text.Trim() : "0");
                    dc.ProductPolicyTypeID = Convert.ToInt32(ddlType.SelectedValue);
                    if (pRepository.GetAll().Where(p => p.CustomerID == sessionKeys.PortfolioID && p.PAPID != Convert.ToInt32(e.CommandArgument.ToString()) && p.AddOnDetails == txtAddOnDetails.Text.Trim()).Count() == 0)
                    {
                        pRepository.Edit(dc);
                        lblMsg_a.Text = "Updated sucessfully";
                        listAppliances.EditIndex = -1;
                        BindProductAddons();
                    }
                    else
                    {
                        lblError_a.Text = "Item already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else if (e.CommandName == "Add")
                {
                    PortfolioMgt.Entity.ProductAddonPrice cf = new PortfolioMgt.Entity.ProductAddonPrice();

                    TextBox txtAddOnDetails = (TextBox)e.Item.FindControl("txtAddOnDetailsF");
                    TextBox txtMontlyCost = (TextBox)e.Item.FindControl("txtMontlyCostF");
                    TextBox txtYearlyCost = (TextBox)e.Item.FindControl("txtYearlyCostF");
                    DropDownList ddlTypeF = (DropDownList)e.Item.FindControl("ddlTypeF");
                    cf.AddOnDetails = txtAddOnDetails.Text.Trim();
                    cf.MontlyCost = Convert.ToDouble(!string.IsNullOrEmpty(txtMontlyCost.Text.Trim()) ? txtMontlyCost.Text.Trim() : "0");
                    cf.YearlyCost = Convert.ToDouble(!string.IsNullOrEmpty(txtYearlyCost.Text.Trim()) ? txtYearlyCost.Text.Trim() : "0");
                    cf.ProductPolicyTypeID = Convert.ToInt32(ddlTypeF.SelectedValue);
                    if (pRepository.GetAll().Where(p => p.AddOnDetails == txtAddOnDetails.Text.Trim() && p.CustomerID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        cf.CustomerID = sessionKeys.PortfolioID;
                        pRepository.Add(cf);
                        lblMsg_a.Text = "Added sucessfully";
                        BindProductAddons();
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
                        var entity = pRepository.GetAll().Where(o => o.PAPID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                        pRepository.Delete(entity);
                        lblMsg_a.Text = "Deleted sucessfully";
                        BindProductAddons();
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
        protected void listAppliances_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            listAppliances.EditIndex = -1;
            //BindCustomFields();
        }
        protected void listAppliances_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            listAppliances.EditIndex = e.NewEditIndex;
            BindProductAddons();
        }
        protected void listAppliances_ItemDataBound(object sender, ListViewItemEventArgs e)
        {


            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                int locale = (int)DataBinder.Eval(item.DataItem, "PtypeID");
                // not sure how to do this from here on. 

                var cell =
                    (DropDownList)e.Item.FindControl("ddlType");
                if (cell != null)
                {
                    cell.DataSource = ProductPolicyData();
                    cell.DataValueField = "ID";
                    cell.DataTextField = "Title";
                    cell.DataBind();
                    cell.Items.Insert(0,new ListItem("Add On", "0"));
                    cell.SelectedValue = locale.ToString();
                }

            }

            //if (e.Item.ItemType == ListViewItemType.InsertItem)
            //{
            //    var cell =
            //       (DropDownList)e.Item.FindControl("ddlTypeF");
            //    if (cell != null)
            //    {
            //        cell.DataSource = ProductPolicyData();
            //        cell.DataValueField = "ID";
            //        cell.DataTextField = "Title";
            //        cell.DataBind();
            //        cell.Items.Insert(0, new ListItem("Add On", "0"));
            //    }

            //}
        }

        private List< PortfolioMgt.Entity.ProductPolicyType> ProductPolicyData()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
            return pRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o=>o.Title ).ToList();
        }
    }
}