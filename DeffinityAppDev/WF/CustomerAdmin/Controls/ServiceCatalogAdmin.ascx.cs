using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Deffinity.ServiceCatalogManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Drawing;
using System.IO;
using PortfolioMgt.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Data.OleDb;
using PortfolioMgt.Entity;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

public partial class controls_ServiceCatalogAdmin_new_1 : System.Web.UI.UserControl
{

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (sessionKeys.Cataloguetype > 0)
                    ddlType.SelectedValue = "2";//sessionKeys.Cataloguetype.ToString();
                    ServiceCatalog_Display();
                }

                iframeMpp.Attributes.Add("src", string.Format("ServiceCatelogAdminFileUpload.aspx?type={0}&page=admin", ddlType.SelectedItem.Text));
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #region Image

        public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);

            ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
            if (a_oThumbSize.HasValue)
            {
                switch (a_oThumbSize.Value)
                {
                    case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
                }
            }
            else
            {
                eImageType = ImageManager.ImageType.OriginalData;
            }

            return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
           
        }

        public bool CheckImageVisibility(Guid a_guid)
        {
            bool _visible = false;
            if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                _visible = true;
            }
            return _visible;
        }

    #endregion

        #region Category
        protected void btnSubCategoryAdd_Click(object sender, ImageClickEventArgs e)
        {
            h_categoryID.Value = ddlCategory.SelectedValue;
            model_subcategory.Show();
        }
        protected void btnCategoryAdd_Click(object sender, ImageClickEventArgs e)
        {
            model_category.Show();
        }
        protected void btnSubcategoryedit_Click(object sender, ImageClickEventArgs e)
        {
            txtSubcategoryName.Text = ddlSubCategory.SelectedItem.Text;
            h_categoryID.Value = ddlSubCategory.SelectedValue;
            model_subcategory.Show();
        }

        protected void btnCategoryedit_Click(object sender, ImageClickEventArgs e)
        {
            txtCategoryName.Text = ddlCategory.SelectedItem.Text;
            model_category.Show();
        }
        protected void btnAddCategory_Click1(object sender, ImageClickEventArgs e)
        {
            try{
            ServiceCatalog_category sc = new ServiceCatalog_category();
            if (Convert.ToInt32(h_edit_categoryID.Value) == 0)
            {
                sc.CategoryName = txtCategoryName.Text.Trim();
                ServiceCatalog_Admin.ServiceCatalog_CategoryInsertByAdmin(sc);
                //clear data
                txtCategoryName.Text = string.Empty;
            }
            else
            {
                sc.CategoryName = txtCategoryName.Text.Trim();
                sc.ID = Convert.ToInt32(h_edit_categoryID.Value);
                ServiceCatalog_Admin.ServiceCatalog_Category_category_UpdateByAdmin(sc);
                //clear data
                h_edit_categoryID.Value ="0";
                txtCategoryName.Text = string.Empty;
            }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnAddSubCategory_Click1(object sender, ImageClickEventArgs e)
        {
            try{
           
             ServiceCatalog_category sc = new ServiceCatalog_category();
             if (Convert.ToInt32(h_edit_subcategoryID.Value) == 0)
             {
                 sc.CategoryName = txtSubcategoryName.Text.Trim();
                 h_categoryID.Value = ddlCategory.SelectedValue;
                 sc.MasterID = Convert.ToInt32(h_categoryID.Value);
                 ServiceCatalog_Admin.ServiceCatalog_SubCategoryInsertByAdmin(sc);

                 //clear data
                 txtSubcategoryName.Text = string.Empty;
                 h_categoryID.Value = "0";
                 
             }
             else
             {
                 sc.CategoryName = txtSubcategoryName.Text.Trim();
                 sc.ID = Convert.ToInt32(h_edit_subcategoryID.Value);
                 ServiceCatalog_Admin.ServiceCatalog_Category_subcategory_UpdateByAdmin(sc);

                 //clear data
                 h_edit_subcategoryID.Value = "0";
                 txtSubcategoryName.Text = string.Empty;
             }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion

        #region labour
        protected void Grid_Labour_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try{
            if (e.CommandName == "edit_labour")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                h_labourID.Value = ID.ToString();
                model_labour.Show();
                Labour_Select(ID);
            }
            else if (e.CommandName == "Delete_labour")
            {
                int Del_ID = Convert.ToInt32(e.CommandArgument.ToString());
                ServiceCatalog_Admin.ServiceCatelog_Labour_DeleteByPortfolioAdmin(Del_ID);
                ServiceCatalog_Display();
            }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void Labour_Select(int LabourID)
        {
            ServiceCatelog_Labour sl = ServiceCatalog_Admin.ServiceCatelog_Labour_SelectByID(LabourID);
            txt_labour_Description.Text=sl.EngineerDescription ;
            txt_labour_Notes.Text=sl.Notes;
            txt_labour_SellingPrice.Text= !sl.SellingPrice.HasValue?"0.00":sl.SellingPrice.Value.ToString();
            txt_labour_BuyingPrice.Text=!sl.BuyingPrice.HasValue?"0.00":sl.BuyingPrice.Value.ToString();
            ccdRatetype.SelectedValue =sl.RateType.ToString();
            txt_labour_LDPrice.Text=!sl.DiscountPrice.HasValue?"0.00":sl.DiscountPrice.Value.ToString();
           // ddl_labour_Team.SelectedValue =sl.RouteToServiceTeam.HasValue?"0":sl.RouteToServiceTeam.Value.ToString();
            txt_labour_unitconsumption.Text=!sl.UnitConsumption.HasValue?"0.00":sl.UnitConsumption.Value.ToString();
            txt_labour_LPercent.Text = string.IsNullOrEmpty(sl.MrkUp) ? "0.00" : string.Format("{0:F2}", Convert.ToDouble(sl.MrkUp));
            ccdCategory.SelectedValue = sl.Category.Value.ToString();
            ccdSubCategory.SelectedValue = sl.SubCategory.Value.ToString();

        }
        private void Labour_ClearData()
        {
            
            txt_labour_Description.Text = string.Empty;
            txt_labour_Notes.Text = string.Empty;
            txt_labour_SellingPrice.Text = string.Empty;
            txt_labour_BuyingPrice.Text = string.Empty;
            ddl_labour_RateType.SelectedIndex =0;
            txt_labour_LDPrice.Text = string.Empty;
            txt_labour_unitconsumption.Text = string.Empty;
            h_labourID.Value = "0";
            txt_labour_LPercent.Text = string.Empty;

        }
        private void Labour_InsertUpdate()
        {
            ServiceCatelog_Labour sl = new ServiceCatelog_Labour();
            if (Convert.ToInt32(h_labourID.Value) != 0)
            {
                sl = ServiceCatalog_Admin.ServiceCatelog_Labour_SelectByID(Convert.ToInt32(h_labourID.Value));
            }
            Guid _guid = Guid.NewGuid();
            sl.Category = Convert.ToInt32(ddlCategory.SelectedValue);
                sl.SubCategory = Convert.ToInt32(ddlSubCategory.SelectedValue);
                sl.EngineerDescription = txt_labour_Description.Text.Trim();
                sl.Notes = txt_labour_Notes.Text.Trim();
                sl.SellingPrice = Convert.ToDouble( string.IsNullOrEmpty(txt_labour_SellingPrice.Text.Trim())? "0.00": txt_labour_SellingPrice.Text.Trim() );
                sl.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_labour_BuyingPrice.Text.Trim()) ? "0.00" : txt_labour_BuyingPrice.Text.Trim());
                sl.RateType = Convert.ToInt32(ddl_labour_RateType.SelectedValue);
                sl.QTY = 0;
                sl.DiscountPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_labour_LDPrice.Text.Trim()) ? "0.00" : txt_labour_LDPrice.Text.Trim());
                sl.RouteToServiceTeam = 0;//Convert.ToInt32(ddl_labour_Team.SelectedValue);
                sl.UnitConsumption = Convert.ToDouble(string.IsNullOrEmpty(txt_labour_unitconsumption.Text.Trim()) ? "0.00" : txt_labour_unitconsumption.Text.Trim());
                sl.MrkUp = GetMarkup(sl.SellingPrice.Value.ToString(), sl.BuyingPrice.Value.ToString());
            if (Convert.ToInt32(h_labourID.Value) == 0)
            {
                if (FileUploadLabour.HasFile)
                {
                    sl.Image = _guid;
                }
                else
                {
                    sl.Image = Guid.Empty;
                }

                ServiceCatalog_Admin.ServiceCatelog_Labour_InsertByPortfolioAdmin(sl);
                
            }
            else
            {
                if (FileUploadLabour.HasFile)
                {
                    sl.Image = _guid;
                }
               
                //sl.ID = Convert.ToInt32(h_labourID.Value);
                ServiceCatalog_Admin.ServiceCatelog_Labour_UpdateByPortfolioAdmin(sl);
                h_labourID.Value = "0";
            }

            if (FileUploadLabour.HasFile)
            {
                ImageManager.SaveImage(_guid, FileUploadLabour.FileBytes);
            }

            ServiceCatalog_Display();
        }
        protected void btn_labour_SaveRecord_Click(object sender, ImageClickEventArgs e)
        {
            try{
            Labour_InsertUpdate();

            //clear data
            Labour_ClearData();
            model_labour.Hide();
            // bind data
            ServiceCatalog_Display();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    #endregion

        #region Material
        protected void Grid_Material_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try{
            if (e.CommandName == "edit_material")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                h_materialID.Value = ID.ToString();
                model_material.Show();
                Material_Select(ID);
            }
            else if (e.CommandName == "Delete_material")
            {
                int Del_ID = Convert.ToInt32(e.CommandArgument.ToString());
                ServiceCatalog_Admin.ServiceCatelog_Material_DeleteByPortfolioAdmin(Del_ID);
                ServiceCatalog_Display();
            }
             }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void Material_Select(int MaterialID)
        {
            ServiceCatelog_Material sm = ServiceCatalog_Admin.ServiceCatelog_Material_SelectByID(MaterialID);
           
            txt_material_ItemDesc.Text=sm.ItemDescription ;
            //ccdSupplier.DataBind();
            ccdSupplier.SelectedValue=!sm.Supplier.HasValue?"0":sm.Supplier.Value.ToString();
            txt_material_PartNumber.Text=sm.PartNumber;
            txt_material_Unit.Text = sm.UnitPrice;
            txt_material_MSellingPrice.Text = !sm.SellingPrice.HasValue? "0.00":sm.SellingPrice.Value.ToString();
            txt_material_MBuyingPrice.Text=!sm.BuyingPrice.HasValue?"0.00":sm.BuyingPrice.Value.ToString();
            txt_material_StockLevel.Text=!sm.UnitsinStock.HasValue?"0":sm.UnitsinStock.Value.ToString() ;
            txt_material_MNotes.Text=sm.Notes;
            txt_material_PDPrice.Text =!sm.DiscountPrice.HasValue?"0.00":sm.DiscountPrice.Value.ToString();
            txt_material_ucproduct.Text = !sm.UnitConsumption.HasValue ? "0.00" : sm.UnitConsumption.Value.ToString();
            txt_material_PPercent.Text = string.IsNullOrEmpty(sm.MrkUp) ? "0.00" : string.Format("{0:F2}", Convert.ToDouble(sm.MrkUp));
            ccdCategory.SelectedValue = sm.Category.Value.ToString();
            ccdSubCategory.SelectedValue = sm.SubCategory.Value.ToString();

        }
        private void Material_ClearData()
        {
            txt_material_ItemDesc.Text = string.Empty;
            ddl_material_Supplier.SelectedIndex= 0;
            txt_material_PartNumber.Text = string.Empty;
            txt_material_Unit.Text = string.Empty;
            txt_material_MSellingPrice.Text = string.Empty;
            txt_material_MBuyingPrice.Text = string.Empty;
            txt_material_StockLevel.Text = string.Empty;
            txt_material_MNotes.Text = string.Empty;
            txt_material_PDPrice.Text = string.Empty;
            txt_material_ucproduct.Text = string.Empty;
            h_materialID.Value = "0";
            txt_material_PPercent.Text = string.Empty;

        }
        private void Material_InsertUpdate()
        {
            
            Guid _guid = Guid.NewGuid();

            ServiceCatelog_Material sm = new ServiceCatelog_Material();
            if (Convert.ToInt32(h_materialID.Value) != 0)
            {
                sm = ServiceCatalog_Admin.ServiceCatelog_Material_SelectByID(Convert.ToInt32(h_materialID.Value));
            }
            sm.Category = Convert.ToInt32(ddlCategory.SelectedValue);
            sm.SubCategory = Convert.ToInt32(ddlSubCategory.SelectedValue);
            sm.ItemDescription = txt_material_ItemDesc.Text.Trim();
            sm.Supplier = Convert.ToInt32(string.IsNullOrEmpty(ddl_material_Supplier.SelectedValue) ? "0" : ddl_material_Supplier.SelectedValue);
            sm.PartNumber = txt_material_PartNumber.Text.Trim();
            sm.UnitPrice = txt_material_Unit.Text;
            sm.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_material_MSellingPrice.Text.Trim()) ? "0.00" : txt_material_MSellingPrice.Text.Trim());
            sm.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_material_MBuyingPrice.Text.Trim()) ? "0.00" : txt_material_MBuyingPrice.Text.Trim());
            sm.UnitsinStock = Convert.ToInt32(string.IsNullOrEmpty(txt_material_StockLevel.Text.Trim()) ? "0" : txt_material_StockLevel.Text.Trim());
            sm.ReorderLevel = 0;
            sm.QTY = 0;
            sm.Notes = txt_material_MNotes.Text.Trim();
            sm.DiscountPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_material_PDPrice.Text.Trim()) ? "0.00" : txt_material_PDPrice.Text.Trim());
            sm.UnitConsumption = Convert.ToDouble(string.IsNullOrEmpty(txt_material_ucproduct.Text.Trim()) ? "0.00" : txt_material_ucproduct.Text.Trim());
            sm.MrkUp = GetMarkup(sm.SellingPrice.Value.ToString(), sm.BuyingPrice.Value.ToString());
            if (Convert.ToInt32(h_materialID.Value) == 0)
            {
                if (FileUploadMaterial.HasFile)
                {
                    sm.Image = _guid;
                }
                else
                {

                    sm.Image = Guid.Empty;
                }
                ServiceCatalog_Admin.ServiceCatelog_Material_InsertByPortfolioAdmin(sm);
            }
            else 
            {
                if (FileUploadMaterial.HasFile)
                {
                    sm.Image = _guid;
                }
                //sm.ID = Convert.ToInt32(h_materialID.Value);
                ServiceCatalog_Admin.ServiceCatelog_Material_UpdateByPortfolioAdmin(sm);
                h_materialID.Value = "0";
            }

            if (FileUploadMaterial.HasFile)
            {
                ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
            }

            ServiceCatalog_Display();
        
        }
        protected void btnSaveMaterial_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Material_InsertUpdate();
                //clear data
                Material_ClearData();
                model_material.Hide();
                // bind data
                ServiceCatalog_Display();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #endregion

        #region Service
        protected void Grid_Service_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try{
            if (e.CommandName == "edit_service")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                h_serviceid.Value = ID.ToString();
                model_service.Show();
                Service_select(ID);
            }
            else if (e.CommandName == "Delete_service")
            {
                int Del_ID = Convert.ToInt32(e.CommandArgument.ToString());
                ServiceCatalog_Admin.ServiceCatelog_Service_DeleteByPortfolioAdmin(Del_ID);
                ServiceCatalog_Display();
            }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void Service_select(int ServiceID)
        {
            ServiceCatelog_Service ss = ServiceCatalog_Admin.ServiceCatelog_Service_SelectByID(ServiceID);
            txt_service_SDescription.Text = ss.ServiceDescription;
            txt_service_SetupBuy.Text=!ss.SetupBuy.HasValue? "0.00": ss.SetupBuy.Value.ToString();
            txt_service_SetupSell.Text = !ss.SetupSell.HasValue ? "0.00" : ss.SetupSell.Value.ToString();
            txt_service_MaterialsBuy.Text = !ss.MaterialsBuy.HasValue ? "0.00" : ss.MaterialsBuy.Value.ToString();
            txt_service_MaterialsSell.Text = !ss.MaterialsSell.HasValue ? "0.00" : ss.MaterialsSell.Value.ToString();
            txt_service_LabourBuy.Text = !ss.LabourBuy.HasValue ? "0.00" : ss.LabourBuy.Value.ToString();
            txt_service_LabourSell.Text = !ss.LabourSell.HasValue ? "0.00" : ss.LabourSell.Value.ToString();
            txt_service_SDprice.Text = !ss.DiscountPrice.HasValue ? "0.00" : ss.DiscountPrice.Value.ToString();
            txt_service_ucservice.Text = !ss.UnitConsumption.HasValue ? "0.00" : ss.UnitConsumption.Value.ToString();
            txt_service_TotBuy.Text = !ss.TotalServiceBuy.HasValue ? "0.00" : ss.TotalServiceBuy.Value.ToString();
            txt_service_TotSell.Text = !ss.TotalServiceSell.HasValue ? "0.00" : ss.TotalServiceSell.Value.ToString();
            txt_service_SPercent.Text = string.IsNullOrEmpty(ss.MrkUp) ? "0.00" : string.Format("{0:F2}", Convert.ToDouble(ss.MrkUp));
            ccdCategory.SelectedValue = ss.Category.Value.ToString();
            ccdSubCategory.SelectedValue = ss.SubCategory.Value.ToString();
          
        }
        private void Service_ClearData()
        {
            txt_service_SDescription.Text = string.Empty;
            txt_service_SetupBuy.Text = string.Empty;
            txt_service_SetupSell.Text = string.Empty;
            txt_service_MaterialsBuy.Text = string.Empty;
            txt_service_MaterialsSell.Text = string.Empty;
            txt_service_LabourBuy.Text = string.Empty;
            txt_service_LabourSell.Text = string.Empty;
            txt_service_SDprice.Text = string.Empty;
            txt_service_ucservice.Text = string.Empty;
            txt_service_SPercent.Text = string.Empty;
            h_serviceid.Value="0";
        }
        private void Service_InsertUpdate()
        {
            Guid _guid = Guid.NewGuid();
            ServiceCatelog_Service ss = new ServiceCatelog_Service();
            if (Convert.ToInt32(h_serviceid.Value) != 0)
            {
                ss = ServiceCatalog_Admin.ServiceCatelog_Service_SelectByID(Convert.ToInt32(h_serviceid.Value));
            }
            ss.Category = Convert.ToInt32(ddlCategory.SelectedValue);
            ss.SubCategory = Convert.ToInt32(ddlSubCategory.SelectedValue);
            ss.ServiceDescription = txt_service_SDescription.Text.Trim();
            ss.QTY = 0;
            ss.SetupBuy = Convert.ToDouble(string.IsNullOrEmpty(txt_service_SetupBuy.Text.Trim()) ? "0.00" : txt_service_SetupBuy.Text.Trim());
            ss.SetupSell = Convert.ToDouble(string.IsNullOrEmpty(txt_service_SetupSell.Text.Trim()) ? "0.00" : txt_service_SetupSell.Text.Trim());
            ss.MaterialsBuy = Convert.ToDouble(string.IsNullOrEmpty(txt_service_MaterialsBuy.Text.Trim()) ? "0.00" : txt_service_MaterialsBuy.Text.Trim());
            ss.MaterialsSell = Convert.ToDouble(string.IsNullOrEmpty(txt_service_MaterialsSell.Text.Trim()) ? "0.00" : txt_service_MaterialsSell.Text.Trim());
            ss.LabourBuy = Convert.ToDouble(string.IsNullOrEmpty(txt_service_LabourBuy.Text.Trim()) ? "0.00" : txt_service_LabourBuy.Text.Trim());
            ss.LabourSell = Convert.ToDouble(string.IsNullOrEmpty(txt_service_LabourSell.Text.Trim()) ? "0.00" : txt_service_LabourSell.Text.Trim());
            ss.DiscountPrice = Convert.ToDouble(string.IsNullOrEmpty(txt_service_SDprice.Text.Trim()) ? "0.00" : txt_service_SDprice.Text.Trim());
            ss.UnitConsumption = Convert.ToDouble(string.IsNullOrEmpty(txt_service_ucservice.Text.Trim()) ? "0.00" : txt_service_ucservice.Text.Trim());
            ss.TotalServiceBuy = ss.SetupBuy + ss.MaterialsBuy + ss.LabourBuy;
            ss.TotalServiceSell = ss.SetupSell + ss.MaterialsSell + ss.LabourSell;
            ss.MrkUp = GetMarkup(ss.TotalServiceSell.Value.ToString(), ss.TotalServiceBuy.Value.ToString());
            if (Convert.ToInt32(h_serviceid.Value) == 0)
            {
                if (FileUploadService.HasFile)
                {
                    ss.Image = _guid;
                }
                else
                {
                    ss.Image = Guid.Empty;
                }
                ServiceCatalog_Admin.ServiceCatelog_Service_Insert_ByPortfolioAdmin(ss);
            }
            else
            {
                if (FileUploadService.HasFile)
                {
                    ss.Image = _guid;
                }
                //ss.ID = Convert.ToInt32(h_serviceid.Value);
                ServiceCatalog_Admin.ServiceCatelog_Service_Update_ByPortfolioAdmin(ss);
                h_serviceid.Value = "0";
            }
            if (FileUploadMaterial.HasFile)
            {
                ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
            }
            ServiceCatalog_Display();
        }
        protected void btnSaveServices_Click(object sender, ImageClickEventArgs e)
        {
            try{
            Service_InsertUpdate();
            //clear data
            Service_ClearData();
            model_service.Hide();
            // bind data
            ServiceCatalog_Display();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion

        #region Display
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            sessionKeys.Cataloguetype = 2;
            ServiceCatalog_Display();
        }
        protected void btnView_Click(object sender, ImageClickEventArgs e)
        {
            ServiceCatalog_Display();
        }

        private void ServiceCatalog_Display()
        {
            try
            {
                int CategoryID = Convert.ToInt32(string.IsNullOrEmpty(ddlCategory.SelectedValue) ? "0" : ddlCategory.SelectedValue);
                int SubCategoryID = Convert.ToInt32(string.IsNullOrEmpty(ddlSubCategory.SelectedValue) ? "0" : ddlSubCategory.SelectedValue);
                if (ddlType.SelectedValue == "1")
                {
                    Grid_Visiblility(true, false, false);
                    Labour_GridBinding(CategoryID, SubCategoryID);
                }
                else if (ddlType.SelectedValue == "2")
                {
                    Grid_Visiblility(false, true, false);
                    Material_GridBinding(CategoryID, SubCategoryID);
                }
                else if (ddlType.SelectedValue == "3")
                {
                    Grid_Visiblility(false, false, true);
                    Service_GridBinding(CategoryID, SubCategoryID);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void Service_GridBinding(int CategoryID, int SubCategoryID)
        {
            List<ServiceCatelog_Service> ss = new List<ServiceCatelog_Service>();
            if (CategoryID > 0)
                ss = ServiceCatalog_Admin.ServiceCatelog_Service_SelectByPortfolioAdmin(CategoryID, SubCategoryID).ToList();
            else
                ss = ServiceCatalog_Admin.ServiceCatelog_Service_SelectByPortfolioAdmin().ToList();
            Grid_Service.DataSource = ss;
            Grid_Service.DataBind();
        }

        private void Labour_GridBinding(int CategoryID, int SubCategoryID)
        {
            List<RateType> rt = new List<RateType>();
            using (PortfolioMgt.DAL.PortfolioDataContext pdContext = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                rt = (from r in pdContext.RateTypes
                      select r).ToList();
            }
            List<ServiceCatelog_Labour> sl = new List<ServiceCatelog_Labour>();
            if (CategoryID > 0)
            {
                sl = (ServiceCatalog_Admin.ServiceCatelog_Labour_SelectByPortfolioAdmin(CategoryID, SubCategoryID)).ToList();
            }
            else
            {
                sl = (ServiceCatalog_Admin.ServiceCatelog_Labour_SelectByPortfolioAdmin()).ToList();
            }

            Grid_Labour.DataSource = from p in sl
                                     select new {
                                         p.ID,
                                         p.Image,
                                         p.Notes,
                                         p.QTY,
                                         p.RateType,
                                         p.Rate,
                                         p.SellingPrice,
                                         p.SubCategory,
                                         p.UnitConsumption,
                                         p.BuyingPrice,
                                         p.Category,
                                         p.DiscountPrice,
                                         p.EngineerDescription,
                                         p.currency,
                                      RateTypeName = rt.Where(i=>i.ID == p.RateType).FirstOrDefault().RateType1
                                     };
            Grid_Labour.DataBind();
        }

        private void Material_GridBinding(int CategoryID, int SubCategoryID)
        {
            List<RFI.Entity.VendorDetails> rf = new List<RFI.Entity.VendorDetails>();
           

            List<ServiceCatelog_Material> sm_list = new List<ServiceCatelog_Material>();
            if (CategoryID > 0)

                sm_list = ServiceCatalog_Admin.ServiceCatelog_Material_SelectByPortfolioAdmin(CategoryID, SubCategoryID) as List<ServiceCatelog_Material>;
            else
                sm_list = ServiceCatalog_Admin.ServiceCatelog_Material_SelectByPortfolioAdmin() as List<ServiceCatelog_Material>;

            var v_array = sm_list.Select(p => p.Supplier.HasValue ? p.Supplier.Value : 0).ToArray();
            using (RFI.DAL.RFIDataContext rfContext = new RFI.DAL.RFIDataContext())
            {
                rf = (from r in rfContext.VendorDetails
                      where v_array.Contains(r.VendorID)
                      select r).ToList();
            }

            Grid_Material.DataSource = (from p in sm_list
                                       select new {
                                       p.ID,
                                       p.BuyingPrice,
                                       p.Category,
                                       p.Image,
                                       p.ItemDescription,
                                       p.Notes,
                                       p.NSPPrice,
                                       p.PartNumber,
                                       p.QTY,
                                       p.QtyOnOrder,
                                       p.ReorderLevel,
                                       p.SellingPrice,
                                       p.SubCategory,
                                       p.Supplier,
                                       p.Unit,
                                       p.UnitConsumption,
                                       p.UnitPrice,
                                       p.UnitsinStock,
                                       p.DiscountPrice,
                                       SupplierName = (p.Supplier.HasValue ? p.Supplier.Value : 0) > 0 ? rf.Where(i => i.VendorID == p.Supplier.Value).FirstOrDefault().ContractorName : string.Empty
                                       
                                       }).ToList();
            Grid_Material.DataBind();
        }

        private void Grid_Visiblility(bool labour,bool material,bool service)
        {
            Grid_Labour.Visible = labour;
            btnAddLabour.Visible = labour;
            Grid_Material.Visible = material;
            btnAddMaterial.Visible = material;
            Grid_Service.Visible = service;
            btnAddService.Visible = service;
        }
        #endregion
 protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            BindToExcel(ddlType.SelectedItem.Text,0);
        }

 #region Download Export
 private void BindToExcel(string filetype, int PortfolioID)
 {

     string filename_template = @Server.MapPath(string.Format("uploaddata\\templates\\servicecatelog_{0}.xlsx", filetype));
     string filename = @Server.MapPath("upload") + string.Format("\\servicecatelog_{0}_{1}.xlsx", filetype, string.Format("{0:ddMMyyyyHHmmss}", DateTime.Now));
     System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
     if (fileInfo.Exists)
     {
         File.Delete(filename);
         File.Copy(filename_template, filename);
     }
     else
     {
         File.Copy(filename_template, filename);
     }

     if (ddlType.SelectedValue == "1")
         ExportToExcel_Labour(filename, PortfolioID);
     else if (ddlType.SelectedValue == "2")
         ExportToExcel_Material(filename, PortfolioID);
     else if (ddlType.SelectedValue == "3")
         ExportToExcel_Service(filename, PortfolioID);
     System.IO.FileInfo fileInfo1 = new System.IO.FileInfo(filename);
     if (fileInfo1.Exists)
     {


         Response.Clear();
         Response.ClearHeaders();
         Response.ClearContent();
         Response.AddHeader("content-disposition", "attachment; filename=" + fileInfo1.Name);
         Response.AddHeader("Content-Type", "application/Excel");
         Response.ContentType = "application/vnd.xls";
         Response.AddHeader("Content-Length", fileInfo1.Length.ToString());
         Response.WriteFile(fileInfo1.FullName);
         Response.Flush();
         Response.End();


     }



 }

 public static int ExportToExcel(DataTable dt, string excelFile, string sheetName)
 {
     // Create the connection string
     string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
         excelFile + ";Extended Properties=Excel 12.0 Xml;";

     int rNumb = 0;
     try
     {
         using (OleDbConnection con = new OleDbConnection(connString))
         {
             con.Open();

             // Build the field names string
             StringBuilder strField = new StringBuilder();
             for (int i = 0; i < dt.Columns.Count - 1; i++)
             {
                 strField.Append("[" + dt.Columns[i].ColumnName + "],");
             }
             strField = strField.Remove(strField.Length - 1, 1);

             // Create Excel sheet
             var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
             OleDbCommand cmd = new OleDbCommand(sqlCmd, con);
             cmd.ExecuteNonQuery();

             for (int i = 0; i < dt.Rows.Count; i++)
             {

                 // Insert data into Excel sheet
                 StringBuilder strValue = new StringBuilder();
                 for (int j = 0; j < dt.Columns.Count - 1; j++)
                 {
                     if (dt.Columns[j].ColumnName == "Date Added")
                     {
                         strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString().Remove(11)) + "',");
                     }
                     else
                     {
                         strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");
                     }
                     ////if (j < 4)
                     ////{
                     //strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");
                     ////}
                     ////else
                     ////{
                     ////    strValue.Append(AddSingleQuotes(dt.Rows[i][j].ToString()) + ",");
                     ////}

                 }
                 strValue = strValue.Remove(strValue.Length - 1, 1);

                 cmd.CommandText = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" +
                         strValue.ToString() + ")";
                 cmd.ExecuteNonQuery();
                 rNumb = i + 1;
             }


             con.Close();
         }
         return rNumb;
     }
     catch (Exception ex)
     {
         return -1;
     }
 }
 public static int ExportToExcel_Material(string excelFile, int PortfolioID)
 {
     // Create the connection string
     string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
         excelFile + ";Extended Properties=Excel 12.0 Xml;";
     string sheetName = string.Empty;
     string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "PART NUMBER", "DESCRIPTION", "NSP PRICE", "ACTUAL BUY", "% MARK UP", "SELL PRICE", "DISCOUNT PRICE" };

     int rNumb = 0;
     try
     {
         using (OleDbConnection con = new OleDbConnection(connString))
         {


             // Build the field names string
             StringBuilder strField = new StringBuilder();
             foreach (string m in MaterialHeader)
             {
                 strField.Append("[" + m + "],");

             }

             strField = strField.Remove(strField.Length - 1, 1);

             List<RFI.Entity.VendorDetails> v = new List<RFI.Entity.VendorDetails>();
             using (RFI.DAL.RFIDataContext rd = new RFI.DAL.RFIDataContext())
             {
                 v = rd.VendorDetails.ToList();
             }

             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                              where p.PortfolioID == PortfolioID
                                                                                              select p).ToList();

                 List<ServiceCatelog_Material> p_material = (from pm in pd.ServiceCatelog_Materials
                                                             where pm.PortfolioID == PortfolioID && pm.ItemDelete != '1' && pm.VendorID == 0
                                                             select pm).ToList();


                 int table_val = 0;
                 try
                 {
                     con.Open();
                     foreach (ServiceCatelog_Material s in p_material)
                     {

                         // Insert data into Excel sheet
                         StringBuilder strValue = new StringBuilder();
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.PartNumber) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.ItemDescription) + "',");
                         //strValue.Append("'" + AddSingleQuotes(string.IsNullOrEmpty(s.Unit) ? string.Empty : s.Unit) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.NSPPrice.HasValue ? s.NSPPrice.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.BuyingPrice.HasValue ? s.BuyingPrice.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.SellingPrice.HasValue ? s.SellingPrice.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.DiscountPrice.HasValue ? s.DiscountPrice.Value.ToString() : "") + "'");
                         //strValue.Append("'" + AddSingleQuotes(s.DateModified.HasValue ? s.DateModified.Value.ToString() : "") + "'");

                         // if (dt.Columns[j].ColumnName == "Date Added")
                         //    {
                         //        strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString().Remove(11)) + "',");
                         //    }

                         //for (int j = 0; j < dt.Columns.Count - 1; j++)
                         //{
                         //    if (dt.Columns[j].ColumnName == "Date Added")
                         //    {
                         //        strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString().Remove(11)) + "',");
                         //    }
                         //    else
                         //    {
                         //        strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");
                         //    }


                         //}
                         //strValue = strValue.Remove(strValue.Length - 1, 1);
                         // Create Excel sheet


                         OleDbCommand cmd = null;

                         var distinctCategories = p_material.Select(m => m.Supplier).Distinct();
                         if (table_val == 0)
                         {
                             try
                             {
                                 foreach (int sup in distinctCategories)
                                 {
                                     try
                                     {
                                         var sname = v.Where(p => p.VendorID == sup).FirstOrDefault().ContractorName.Trim();
                                         var sqlCmd = "CREATE TABLE [" + sname + "] (" + strField.ToString().Replace("]", "] text") + ")";
                                         cmd = new OleDbCommand(sqlCmd, con);
                                         cmd.ExecuteNonQuery();
                                     }
                                     catch (Exception ex)
                                     { string s1 = ex.Message; }
                                 }
                                 table_val = 1;
                             }
                             catch (Exception ex)
                             {
                                 string er = ex.Message;
                             }

                         }
                         sheetName = v.Where(p => p.VendorID == s.Supplier).FirstOrDefault().ContractorName.Trim() +"$";
                         //string s1 = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")";


                         cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                         cmd.ExecuteNonQuery();

                         //rNumb = i + 1;
                     }
                 }
                 catch (Exception ex)
                 { string er = ex.Message; }
                 finally { con.Close(); }

             }




             //con.Close();
         }
         return rNumb;
     }
     catch (Exception ex)
     {
         return -1;
     }
 }

 public static int ExportToExcel_Service(string excelFile, int PortfolioID)
 {
     // Create the connection string
     string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
         excelFile + ";Extended Properties=\"Excel 12.0;ReadOnly=False;HDR=Yes;\"";
     string sheetName = string.Empty;
     string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "DESCRIPTION", "SETUP BUY", "SETUP SELL", "MATERIAL BUY", "MATERIAL SELL", "LABOUR BUY", "LABOUR SELL","MARK UP" };


     int rNumb = 0;
     try
     {
         using (OleDbConnection con = new OleDbConnection(connString))
         {


             // Build the field names string
             StringBuilder strField = new StringBuilder();
             foreach (string m in MaterialHeader)
             {
                 strField.Append("[" + m + "],");

             }

             strField = strField.Remove(strField.Length - 1, 1);



             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                              where p.PortfolioID == PortfolioID
                                                                                              select p).ToList();

                 List<ServiceCatelog_Service> p_service = (from pm in pd.ServiceCatelog_Services
                                                           where pm.PortfolioID == PortfolioID && pm.ItemDelete != 1 && pm.VendorID == 0
                                                           select pm).ToList();
                 int table_cnt = 0;
                 try
                 {
                     con.Open();
                     foreach (ServiceCatelog_Service s in p_service)
                     {

                         // Insert data into Excel sheet
                         StringBuilder strValue = new StringBuilder();
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.ServiceDescription) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.SetupBuy.HasValue ? s.SetupBuy.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.SetupSell.HasValue ? s.SetupSell.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.MaterialsBuy.HasValue ? s.MaterialsBuy.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.MaterialsSell.HasValue ? s.MaterialsSell.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.LabourBuy.HasValue ? s.LabourBuy.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.LabourSell.HasValue ? s.LabourSell.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes((string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp)) + "'");


                         // Create Excel sheet

                         OleDbCommand cmd = null;
                         sheetName = "Sheet1$";
                         //if (table_cnt == 0 && sheetName != "Sheet1$")
                         //{

                         //    var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                         //    cmd = new OleDbCommand(sqlCmd, con);
                         //    cmd.ExecuteNonQuery();
                         //    table_cnt = 1;
                         //}
                         //else { table_cnt = 1; }

                         cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                         cmd.ExecuteNonQuery();

                     }
                 }
                 catch (Exception ex)
                 { string er = ex.Message; }
                 finally { con.Close(); }

             }

             //con.Close();
         }
         return rNumb;
     }
     catch (Exception ex)
     {
         return -1;
     }
 }
 public static int ExportToExcel_Labour(string excelFile, int PortfolioID)
 {

    
    
     // Create the connection string
     string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
         excelFile + ";Extended Properties=Excel 12.0 Xml;";
     string sheetName = string.Empty;
     string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "DESCRIPTION", "BUYING PRICE", "SELLING PRICE", "MARK UP" };


     int rNumb = 0;
     try
     {
         using (OleDbConnection con = new OleDbConnection(connString))
         {


             // Build the field names string
             StringBuilder strField = new StringBuilder();
             foreach (string m in MaterialHeader)
             {
                 strField.Append("[" + m + "],");

             }

             strField = strField.Remove(strField.Length - 1, 1);



             using (PortfolioDataContext pd = new PortfolioDataContext())
             {
                 List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                              where p.PortfolioID == PortfolioID
                                                                                              select p).ToList();

                 List<ServiceCatelog_Labour> p_service = (from pm in pd.ServiceCatelog_Labours
                                                          where pm.PortfolioID == PortfolioID && pm.ItemDelete != '1' && pm.VendorID == 0
                                                          select pm).ToList();
                 int table_cnt = 0;
                 try
                 {
                     con.Open();
                     foreach (ServiceCatelog_Labour s in p_service)
                     {

                         // Insert data into Excel sheet
                         StringBuilder strValue = new StringBuilder();
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.EngineerDescription) + "',");
                         strValue.Append("'" + AddSingleQuotes(s.BuyingPrice.HasValue ? s.BuyingPrice.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes(s.SellingPrice.HasValue ? s.SellingPrice.Value.ToString() : "0.00") + "',");
                         strValue.Append("'" + AddSingleQuotes((string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp)) + "'");



                         // Create Excel sheet

                         OleDbCommand cmd = null;
                         sheetName = "Sheet1$";
                         //if (table_cnt == 0 && sheetName != "Sheet1$")
                         //{
                         //    //cmd = new OleDbCommand("Drop Table "+ sheetName +"$" , con);
                         //    //cmd.ExecuteNonQuery();

                         //    var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                         //    cmd = new OleDbCommand(sqlCmd, con);
                         //    cmd.ExecuteNonQuery();
                         //    table_cnt = 1;
                         //}
                         //else { table_cnt = 1; }

                         cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                         cmd.ExecuteNonQuery();



                     }
                 }
                 catch (Exception ex)
                 { string er = ex.Message; }
                 finally { con.Close(); }

             }

             //con.Close();
         }
         return rNumb;
     }
     catch (Exception ex)
     {
         return -1;
     }
 }
 public static string AddSingleQuotes(string origText)
 {
     string s = origText;
     int i = 0;

     while ((i = s.IndexOf("'", i)) != -1)
     {
         // Add single quote after existing
         s = s.Substring(0, i) + "'" + s.Substring(i);

         // Increment the index.
         i += 2;
     }
     return s;
 }
 private static string GetMarkup(string SellingPrice, string BuyingPrice)
 {
     string retval = string.Empty;
     double sellprice = 0;
     double buypirce = 0;

     if (!string.IsNullOrEmpty(SellingPrice))
     {
         sellprice = Convert.ToDouble(SellingPrice);
     }

     if (!string.IsNullOrEmpty(BuyingPrice))
     {
         buypirce = Convert.ToDouble(BuyingPrice);
     }

     //formula

     if (buypirce > 0 && sellprice > 0)
     {
         retval = string.Format("{0:F2}", 100 * ((sellprice - buypirce) / buypirce));
     }
     else
         retval = string.Empty;


     return retval;
 }

 #endregion
        protected void btnCopyCatalogue_Click(object sender, EventArgs e)
        {
           
        }

        private int Get_CategoryID_ByAdmin(int portfolioid, int Admin_CategoryID)
        {
            int categoryID = 0;
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {

                var c_name = (from p in pd.ServiceCatalog_categories
                         where p.ID== Admin_CategoryID
                         select p).FirstOrDefault().CategoryName;
                categoryID = (from p in pd.ServiceCatalog_categories
                              where p.PortfolioID == portfolioid && p.VendorID == 0 && p.CategoryName.ToLower() == c_name.ToLower()
                              select p).FirstOrDefault().ID;
            }

            return categoryID;
        }

        private int Get_SubCategoryID_ByAdmin(int portfolioid, int Admin_SubCategoryID)
        {
            int categoryID = 0;
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {

                var subname  = (from p in pd.ServiceCatalog_categories
                         where p.ID == Admin_SubCategoryID  
                         select p).FirstOrDefault().CategoryName;
                var catID = (from p in pd.ServiceCatalog_categories
                           where p.PortfolioID == portfolioid && p.VendorID == 0 && p.CategoryName.ToLower() == subname.ToLower()
                           select p).FirstOrDefault().MasterID;
                categoryID = (from p in pd.ServiceCatalog_categories
                              where p.PortfolioID == portfolioid && p.VendorID == 0 && p.CategoryName.ToLower() == subname.ToLower() && p.MasterID == catID
                              select p).FirstOrDefault().ID;
            }

            return categoryID;
        }

        protected void btnCopyCatalogue_Click1(object sender, EventArgs e)
        {
            
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                string master_category = string.Empty;
                List<ProjectPortfolio> pp = new List<ProjectPortfolio>();
                List<PortfolioMgt.Entity.ServiceCatalog_category> admin_Category = new List<PortfolioMgt.Entity.ServiceCatalog_category>();
                List<PortfolioMgt.Entity.ServiceCatalog_category> admin_SubCategory = new List<PortfolioMgt.Entity.ServiceCatalog_category>();
                List<PortfolioMgt.Entity.ServiceCatelog_Labour> admin_labour = new List<PortfolioMgt.Entity.ServiceCatelog_Labour>();
                List<PortfolioMgt.Entity.ServiceCatelog_Material> admin_material = new List<PortfolioMgt.Entity.ServiceCatelog_Material>();
                List<PortfolioMgt.Entity.ServiceCatelog_Service> admin_service = new List<PortfolioMgt.Entity.ServiceCatelog_Service>();


                List<PortfolioMgt.Entity.ServiceCatalog_category> portfolio_exist_Category = new List<PortfolioMgt.Entity.ServiceCatalog_category>();
                List<PortfolioMgt.Entity.ServiceCatalog_category> portfolio_exist_SubCategory = new List<PortfolioMgt.Entity.ServiceCatalog_category>();


                ServiceCatalog_category p_category = new PortfolioMgt.Entity.ServiceCatalog_category();
                ServiceCatalog_category p_subCategory = new PortfolioMgt.Entity.ServiceCatalog_category();
                ServiceCatelog_Labour p_labour = new PortfolioMgt.Entity.ServiceCatelog_Labour();
                ServiceCatelog_Material p_material = new PortfolioMgt.Entity.ServiceCatelog_Material();
                ServiceCatelog_Service p_service = new PortfolioMgt.Entity.ServiceCatelog_Service();

                List<PortfolioMgt.Entity.ServiceCatalog_category> portfolio_Category = new List<PortfolioMgt.Entity.ServiceCatalog_category>();

                admin_Category = (from c in pd.ServiceCatalog_categories
                                  where c.PortfolioID == 0 && c.MasterID == 0 && (c.VendorID.HasValue ? c.VendorID.Value : 0) == 0
                                  select c).ToList();
                admin_SubCategory = (from c in pd.ServiceCatalog_categories
                                     where c.PortfolioID == 0 && c.MasterID > 0 && (c.VendorID.HasValue?c.VendorID.Value:0) == 0
                                     select c).ToList();
                admin_labour = (from l in pd.ServiceCatelog_Labours
                                where l.PortfolioID == 0 && l.ItemDelete != '1' && (l.VendorID.HasValue ? l.VendorID.Value : 0) == 0 && (l.Category.HasValue ? l.Category.Value : 0) > 0
                                select l).ToList();
                admin_material = (from l in pd.ServiceCatelog_Materials
                                  where l.PortfolioID == 0 && l.ItemDelete != '1' && (l.VendorID.HasValue ? l.VendorID.Value : 0) == 0 && (l.Category.HasValue ? l.Category.Value : 0) > 0
                                  select l).ToList();
                admin_service = (from l in pd.ServiceCatelog_Services
                                 where l.PortfolioID == 0 && l.ItemDelete != 1 && (l.VendorID.HasValue ? l.VendorID.Value : 0) == 0 && (l.Category.HasValue ? l.Category.Value : 0) > 0
                                 select l).ToList();

                pp = (from p in pd.ProjectPortfolios
                      select p).ToList();

                foreach (ProjectPortfolio p in pp)
                {
                  
                     //insert category which not exists
                    foreach (ServiceCatalog_category s in admin_Category)
                    {
                        try
                        {
                            //check category exits for this protfolio
                            p_category = (from c in pd.ServiceCatalog_categories
                                          where c.PageType == 1 && c.PortfolioID == p.ID && c.MasterID == 0 && c.VendorID == 0 && c.CategoryName.ToLower().ToString() == s.CategoryName.ToLower()
                                          select c).FirstOrDefault();

                            if (p_category == null)
                            {
                                p_category = new PortfolioMgt.Entity.ServiceCatalog_category();
                                p_category.CategoryName = s.CategoryName;
                                p_category.MasterID = s.MasterID;
                                p_category.PageType = 1;
                                p_category.PortfolioID = p.ID;
                                p_category.Type = s.Type;
                                p_category.VendorID = s.VendorID;

                                //s.ID = 0;
                                //s.PortfolioID = p.ID;
                                pd.ServiceCatalog_categories.InsertOnSubmit(p_category);
                                pd.SubmitChanges();
                            }
                        }

                        catch (Exception ex)
                        {
                            LogExceptions.LogException("category id - " + s.ID.ToString() + "Portfolio id - " + p.PortFolio.ToString());
                        }

                    }

                    
                    //insert sub category
                    foreach (ServiceCatalog_category sb in admin_SubCategory)
                    {
                        //check category exits for this protfolio
                        p_subCategory = (from c in pd.ServiceCatalog_categories
                                         where c.PageType == 1 && c.PortfolioID == p.ID && c.MasterID > 0 && c.VendorID == 0 && c.CategoryName.ToLower().ToString() == sb.CategoryName.ToLower()
                                         select c).FirstOrDefault();

                        if (p_subCategory == null)
                        {
                            try
                            {

                                //var categoryname = (from pc in pd.ServiceCatalog_categories
                                //                    where pc.PortfolioID == 0 && pc.ID == sb.MasterID 
                                //                    select pc).FirstOrDefault().CategoryName;
                                var categoryname = (from pc in pd.ServiceCatalog_categories
                                                    where  pc.PageType == 1 && pc.PortfolioID == 0 && pc.ID == sb.MasterID
                                                    select pc).FirstOrDefault().CategoryName;
                                var categoryID = (from pc in pd.ServiceCatalog_categories
                                                  where pc.PageType == 1 && pc.PortfolioID == p.ID && pc.CategoryName.ToLower() == categoryname.ToLower()
                                                  select pc).FirstOrDefault().ID;


                                p_subCategory = new PortfolioMgt.Entity.ServiceCatalog_category();
                                p_subCategory.PortfolioID = p.ID;
                                p_subCategory.MasterID = categoryID;
                                p_subCategory.CategoryName = sb.CategoryName;
                                p_subCategory.PageType = 1;
                                p_subCategory.PortfolioID = p.ID;
                                p_subCategory.Type = sb.Type;
                                p_subCategory.VendorID = sb.VendorID;

                                pd.ServiceCatalog_categories.InsertOnSubmit(p_subCategory);
                                pd.SubmitChanges();
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.LogException("Subcategory id - " + sb.ID.ToString() + "Portfolio id - " + p.PortFolio.ToString());
                            }
                        }
                    }

                    //insert sub category
                    foreach (ServiceCatalog_category sb in admin_SubCategory)
                    {
                        //check category exits for this protfolio
                        p_subCategory = (from c in pd.ServiceCatalog_categories
                                         where c.PortfolioID == p.ID && c.MasterID > 0 && c.VendorID == 0 && c.CategoryName.ToLower().ToString() == sb.CategoryName.ToLower()
                                         select c).FirstOrDefault();

                        if (p_subCategory == null)
                        {
                            var categoryname = (from pc in pd.ServiceCatalog_categories
                                                where pc.PortfolioID == 0 && pc.ID == sb.MasterID
                                                select pc).FirstOrDefault().CategoryName;
                            var categoryID = (from pc in pd.ServiceCatalog_categories
                                              where pc.PortfolioID == p.ID && pc.CategoryName.ToLower() == categoryname.ToLower()
                                              select pc).FirstOrDefault().ID;


                            p_subCategory = new PortfolioMgt.Entity.ServiceCatalog_category();
                            p_subCategory.PortfolioID = p.ID;
                            p_subCategory.MasterID = categoryID;
                            p_subCategory.CategoryName = sb.CategoryName;
                            p_subCategory.PageType = 1;
                            p_subCategory.PortfolioID = p.ID;
                            p_subCategory.Type = sb.Type;
                            p_subCategory.VendorID = sb.VendorID;

                            pd.ServiceCatalog_categories.InsertOnSubmit(p_subCategory);
                            pd.SubmitChanges();


                        }

                    }



                    try
                    {
                      
                            //insert material
                        foreach (ServiceCatelog_Labour sl in admin_labour)
                        {
                            var cat_l = Get_CategoryID_ByAdmin(p.ID, sl.Category.Value);
                            var sub_l = Get_SubCategoryID_ByAdmin(p.ID, sl.SubCategory.Value);

                            p_labour = (from s in pd.ServiceCatelog_Labours
                                        where s.PortfolioID == p.ID && s.ItemDelete != '1' && s.VendorID == 0 && s.EngineerDescription.ToLower() == sl.EngineerDescription.ToLower()
                                        && s.Category == cat_l && s.SubCategory == sub_l
                                        select s).FirstOrDefault();
                            if (p_labour == null)
                            {


                                p_labour = new PortfolioMgt.Entity.ServiceCatelog_Labour();
                                p_labour.PortfolioID = p.ID;
                                p_labour.Category = cat_l;
                                p_labour.SubCategory = sub_l;
                                p_labour.BuyingPrice = sl.BuyingPrice;
                                p_labour.DiscountPrice = sl.DiscountPrice;
                                p_labour.EngineerDescription = sl.EngineerDescription;
                                p_labour.Image = sl.Image;
                                p_labour.ItemDelete = sl.ItemDelete;
                                p_labour.ItemLock = sl.ItemLock;
                                p_labour.Notes = sl.Notes;
                                p_labour.PageType = sl.PageType;
                                p_labour.QTY = sl.QTY;
                                p_labour.Rate = sl.Rate;
                                p_labour.RateType = sl.RateType;
                                p_labour.RouteToServiceTeam = sl.RouteToServiceTeam;
                                p_labour.SellingPrice = sl.SellingPrice;
                                p_labour.Type = sl.Type;
                                p_labour.UnitConsumption = sl.UnitConsumption;
                                p_labour.VendorID = sl.VendorID;


                                pd.ServiceCatelog_Labours.InsertOnSubmit(p_labour);
                                pd.SubmitChanges();

                                ServiceCatalog_Admin.ServiceCatalog_Associate_insert(1, sl.ID, p_labour.ID);
                            }
                        }
                        
                        //insert material
                        foreach (ServiceCatelog_Material sm in admin_material)
                        {
                            var cat_m = Get_CategoryID_ByAdmin(p.ID, sm.Category.Value);
                            var sub_m = Get_SubCategoryID_ByAdmin(p.ID, sm.SubCategory.Value);
                            p_material = (from s in pd.ServiceCatelog_Materials
                                          where s.PortfolioID == p.ID && s.ItemDelete != '1' && s.VendorID == 0 && s.ItemDescription.ToLower() == sm.ItemDescription.ToLower()
                                          && s.Category == cat_m && s.SubCategory == sub_m
                                          select s).FirstOrDefault();
                            if (p_material == null)
                            {
                                p_material = new PortfolioMgt.Entity.ServiceCatelog_Material();
                                p_material.PortfolioID = p.ID;
                                p_material.Category = cat_m;
                                p_material.SubCategory = sub_m;
                                p_material.BuyingPrice = sm.BuyingPrice;
                                p_material.DiscountPrice = sm.DiscountPrice;
                                p_material.Image = sm.Image;
                                p_material.ItemDelete = sm.ItemDelete;
                                p_material.ItemDescription = sm.ItemDescription;
                                p_material.ItemLock = sm.ItemLock;
                                p_material.Manufacturer = sm.Manufacturer;
                                p_material.LeadTime = sm.LeadTime;
                                p_material.Length = sm.Length;
                                p_material.MrkUp = sm.MrkUp;
                                p_material.Notes = sm.Notes;
                                p_material.NSPPrice = sm.NSPPrice;
                                p_material.PageType = sm.PageType;
                                p_material.PartNumber = sm.PartNumber;
                                p_material.QTY = sm.QTY;
                                p_material.QtyOnOrder = sm.QtyOnOrder;
                                p_material.ReorderLevel = sm.ReorderLevel;
                                p_material.Replenish = sm.Replenish;
                                p_material.SellingPrice = sm.SellingPrice;
                                p_material.Supplier = sm.Supplier;
                                p_material.Type = sm.Type;
                                p_material.Unit = sm.Unit;
                                p_material.UnitConsumption = sm.UnitConsumption;
                                p_material.UnitPrice = sm.UnitPrice;
                                p_material.UnitsinStock = sm.UnitsinStock;
                                p_material.VendorID = sm.VendorID;

                                pd.ServiceCatelog_Materials.InsertOnSubmit(p_material);
                                pd.SubmitChanges();

                                ServiceCatalog_Admin.ServiceCatalog_Associate_insert(2, sm.ID, p_material.ID);
                            }
                        }

                        //insert services
                        foreach (ServiceCatelog_Service ss in admin_service)
                        {

                            var cat_s = Get_CategoryID_ByAdmin(p.ID, ss.Category.Value);
                            var sub_s = Get_SubCategoryID_ByAdmin(p.ID, ss.SubCategory.Value);
                            p_service = (from s in pd.ServiceCatelog_Services
                                         where s.PortfolioID == p.ID && s.ItemDelete != 1 && s.VendorID == 0 && s.ServiceDescription.ToLower() == ss.ServiceDescription.ToLower()
                                         && s.Category == cat_s && s.SubCategory == sub_s
                                         select s).FirstOrDefault();
                            if (p_service == null)
                            {
                                p_service = new PortfolioMgt.Entity.ServiceCatelog_Service();
                                p_service.PortfolioID = p.ID;
                                p_service.Category = cat_s;
                                p_service.SubCategory = sub_s;
                                p_service.BuyingPrice = ss.BuyingPrice;
                                p_service.DetailedDesc = ss.DetailedDesc;
                                p_service.DiscountPrice = ss.DiscountPrice;
                                p_service.GP = ss.GP;
                                p_service.Image = ss.Image;
                                p_service.ItemDelete = ss.ItemDelete;
                                p_service.LabourBuy = ss.LabourBuy;
                                p_service.LabourSell = ss.LabourSell;
                                p_service.MaterialsBuy = ss.MaterialsBuy;
                                p_service.MaterialsSell = ss.MaterialsSell;
                                p_service.PageType = ss.PageType;
                                p_service.QTY = ss.QTY;
                                p_service.SellingPrice = ss.SellingPrice;
                                p_service.ServiceDescription = ss.ServiceDescription;
                                p_service.ServiceType = ss.ServiceType;
                                p_service.SetupBuy = ss.SetupBuy;
                                p_service.SetupSell = ss.SetupSell;
                                p_service.TotalServiceBuy = ss.TotalServiceBuy;
                                p_service.TotalServiceSell = ss.TotalServiceSell;
                                p_service.Type = ss.Type;
                                p_service.UnitConsumption = ss.UnitConsumption;
                                p_service.VendorID = ss.VendorID;
                                p_service.Visible = ss.Visible;

                                pd.ServiceCatelog_Services.InsertOnSubmit(p_service);
                                pd.SubmitChanges();

                                ServiceCatalog_Admin.ServiceCatalog_Associate_insert(3, ss.ID, p_service.ID);
                            }
                        }
                        lblMsgDisplay.ForeColor = System.Drawing.Color.Green;
                        lblMsgDisplay.Text = "Catalogue items copied successfully.";
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }


                }

            }
        }
}