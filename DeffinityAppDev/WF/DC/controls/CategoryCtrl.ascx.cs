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
public partial class DC_controls_CategoryCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ccdTypeOfRequest.DataBind();
            //ccdTypeOfRequest.SelectedValue = "2";
            //ddlRequestType.SelectedValue = "2";
            Hide();
            HideTypeOfRequest();
            HideSubCategory();
            HideModel();
        }
    }

    #region Hide Controls
    private void Hide()
    {
        ddlCategory.Visible = true;
        txtCategory.Visible = false;
        imb_Submit.Visible = false;
        imb_Cancel.Visible = false;
        imb_Add.Visible = true;
        imb_Delete.Visible = true;
        imb_Edit.Visible = true;
    }
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
    private void HideSubCategory()
    {
        txtSubCategory.Visible = false;
        btnCancelSubCategory.Visible = false;
        btnSubmitSubCategory.Visible = false;

        ddlSubCategory.Visible = true;
        btnAddSubCategory.Visible = true;
        btnEditSubCategory.Visible = true;
        btnDeleteSubCategory.Visible = true;
    }
    private void HideModel()
    {
        txtModel.Visible = false;
        btnCancelModel.Visible = false;
        btnSubmitModel.Visible = false;

        ddlModel.Visible = true;
        btnAddModel.Visible = true;
        btnEditModel.Visible = true;
        btnDeleteModel.Visible = true;
    }
    #endregion

    #region Show Controls
    private void Show()
    {
        ddlCategory.Visible = false;
        txtCategory.Visible = true;
        imb_Submit.Visible = true;
        imb_Cancel.Visible = true;
        imb_Add.Visible = false;
        imb_Delete.Visible = false;
        imb_Edit.Visible = false;
        lblMsg.Text = string.Empty;
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
    private void ShowSubCategory()
    {
        txtSubCategory.Visible = true;
        btnCancelSubCategory.Visible = true;
        btnSubmitSubCategory.Visible = true;

        ddlSubCategory.Visible = false;
        btnAddSubCategory.Visible = false;
        btnEditSubCategory.Visible = false;
        btnDeleteSubCategory.Visible = false;
    }
    private void ShowModel()
    {
        txtModel.Visible = true;
        btnCancelModel.Visible = true;
        btnSubmitModel.Visible = true;

        ddlModel.Visible = false;
        btnAddModel.Visible = false;
        btnEditModel.Visible = false;
        btnDeleteModel.Visible = false;
    }
    #endregion

    #region "Category"
    protected void imb_Add_Click(object sender, EventArgs e)
    {
        Show();
        txtCategory.Text = string.Empty;
    }

    protected void imb_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt32(ddlRequestType.SelectedValue) > 0)
            //{
                Category category = new Category();
                category.Name = txtCategory.Text.Trim();
                //category.TypeOfRequestID = Convert.ToInt32(ddlRequestType.SelectedValue);
                int id = int.Parse(string.IsNullOrEmpty(hfId.Value) ? "0" : hfId.Value);
                if (id > 0)
                {
                    bool exists = CategoryBAL.CheckCategory(id, txtCategory.Text.Trim());
                    if (!exists)
                    {
                        category.ID = id;
                        CategoryBAL.UpdateCategory(category);
                        lblMsg.Text = "Updated successfully";
                       // lblMsg.ForeColor = System.Drawing.Color.Green;
                        Hide();
                        ddlCategory.SelectedValue = id.ToString();
                        hfId.Value = "0";
                        txtCategory.Text = string.Empty;
                    }
                    else
                    {
                        lblMsg.Text = "Category already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    bool exists = CategoryBAL.CheckCategory(txtCategory.Text.Trim());
                    if (!exists)
                    {
                        CategoryBAL.AddCategory(category);
                        lblMsg.Text = "Added successfully";
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        Hide();
                        ddlCategory.SelectedValue = category.ID.ToString();
                        hfId.Value = "0";
                        txtCategory.Text = string.Empty;


                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            //}
            //else
            //{
            //    lblError.Text = "Please select type of request.";
            //    //lblMsg.ForeColor = System.Drawing.Color.Red;
            //}
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
            Category category = CategoryBAL.SelectByID(int.Parse(ddlCategory.SelectedValue));
            if (category != null)
            {
                txtCategory.Text = category.Name;
                hfId.Value = category.ID.ToString();
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
            if (ddlCategory.SelectedValue != "0")
            {
                CategoryBAL.DeleteByID(int.Parse(ddlCategory.SelectedValue));
                lblMsg.Text = "Category deleted successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

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

    #region "Sub Category"
    protected void btnAddSubCategory_Click(object sender, EventArgs e)
    {
        ShowSubCategory();
        txtSubCategory.Text = string.Empty;
    }
    protected void btnEditSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            SubCategory subCategory = SubCategoryBAL.SelectByID(int.Parse(ddlSubCategory.SelectedValue));
            if (subCategory != null)
            {
                txtSubCategory.Text = subCategory.Name;
                hfSubCategoryId.Value = subCategory.ID.ToString();
                ShowSubCategory();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnDeleteSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubCategory.SelectedValue != "")
            {
                SubCategoryBAL.DeleteByID(int.Parse(ddlSubCategory.SelectedValue));
                lblMsg.Text = "Sub Category deleted successfully";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnSubmitSubCategory_Click(object sender, EventArgs e)
    {
        try
        {

            SubCategory subCategory = new SubCategory();
            subCategory.Name = txtSubCategory.Text.Trim();
            subCategory.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            int id = int.Parse(string.IsNullOrEmpty(hfSubCategoryId.Value) ? "0" : hfSubCategoryId.Value);
            if (id > 0)
            {
                bool exists = SubCategoryBAL.CheckSubCategory(id, Convert.ToInt32(subCategory.CategoryID), txtSubCategory.Text.Trim());
                if (!exists)
                {
                    subCategory.ID = id;
                    SubCategoryBAL.UpdateSubCategory(subCategory);
                    lblMsg.Text = "Updated successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    HideSubCategory();
                    hfSubCategoryId.Value = "0";
                   
                }
                else
                {
                    lblError.Text = "Item already exists";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = SubCategoryBAL.CheckSubCategory(txtSubCategory.Text.Trim(), Convert.ToInt32(subCategory.CategoryID));
                if (!exists)
                {
                    SubCategoryBAL.AddSubCategory(subCategory);
                    lblMsg.Text = "Added successfully";
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    HideSubCategory();
                    hfSubCategoryId.Value = "0";
                 


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
    protected void btnCancelSubCategory_Click(object sender, EventArgs e)
    {
        HideSubCategory();
        lblMsg.Text = string.Empty;
        hfSubCategoryId.Value = "0";
    }
    #endregion
    protected void btnCopyToAllCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            int typeOfRequestId = Convert.ToInt32(ddlRequestType.SelectedValue);
            string requestType = ddlRequestType.SelectedItem.Text;
            
            using (DCDataContext dc = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    int rId = 0;
                    int categoryId = 0;
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    List<TypeOfRequest> trList = new List<TypeOfRequest>();
                    List<Category> cList = new List<Category>();
                    List<SubCategory> sList = new List<SubCategory>();
                    foreach (var c in customerList)
                    {
                        // Type of Request
                        bool exists = TypeOfRequestBAL.CheckTypeOfRequest(requestType, c.ID);
                        var rType = dc.TypeOfRequests.Where(r => r.Name == requestType && r.CustomerID == c.ID).FirstOrDefault();
                        if (rType == null)
                        {
                            TypeOfRequest tRequest = new TypeOfRequest();
                            tRequest.CustomerID = c.ID;
                            tRequest.Name = requestType;
                            dc.TypeOfRequests.InsertOnSubmit(tRequest);
                            dc.SubmitChanges();
                            rId = tRequest.ID;
                            //trList.Add(tRequest);
                        }
                        else
                        {
                            rId = rType.ID;
                        }

                        // Category
                        var categoryList = CategoryBAL.GetCategoryList().Where(ct => ct.TypeOfRequestID == typeOfRequestId).ToList();
                        foreach (var item in categoryList)
                        {
                            //bool checkCategory = CategoryBAL.CheckCategory(item.Name, rId);
                            var checkCategory = dc.Categories.Where(r => r.Name == item.Name && r.TypeOfRequestID == rId).FirstOrDefault();
                            if (checkCategory == null)
                            {
                                Category category = new Category();
                                category.Name = item.Name;
                                category.TypeOfRequestID = rId;
                                dc.Categories.InsertOnSubmit(category);
                                dc.SubmitChanges();
                                categoryId = category.ID;
                                //cList.Add(category);
                            }
                            else
                            {
                                categoryId = checkCategory.ID;
                            }

                            // Sub Category
                            var subCategoryList = SubCategoryBAL.GetSubCategoryList().Where(s => s.CategoryID == item.ID).ToList();
                            foreach (var sc in subCategoryList)
                            {
                                //bool checkSubCategory = SubCategoryBAL.CheckSubCategory(sc.Name,item.ID);
                                var checkSubCategory = dc.SubCategories.Where(s => s.Name == sc.Name && s.CategoryID == categoryId).FirstOrDefault();
                                if (checkSubCategory == null)
                                {
                                    SubCategory subCategory = new SubCategory();
                                    subCategory.Name = sc.Name;
                                    subCategory.CategoryID = categoryId;
                                    dc.SubCategories.InsertOnSubmit(subCategory);
                                    dc.SubmitChanges();
                                }
                            }
                        }
                       
                    }
                }
            }
            lblMsg.Text = "Successfully copied...";
        }
        catch (Exception ex)
        {

        }
    }
    protected void btndeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRequestType.SelectedValue != "")
            {
                TypeOfRequestBAL.DeteleSitetoAllCustomers(ddlRequestType.SelectedItem.ToString());
                lblMsg.Text = "Type of Request deleted to all customers successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;

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
            if (ddlRequestType.SelectedValue != "")
            {
                TypeOfRequestBAL.DeleteByID(int.Parse(ddlRequestType.SelectedValue));
                lblMsg.Text = "Deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void lbtnAddModel_Click(object sender, EventArgs e)
    {
        ShowModel();
        txtModel.Text = string.Empty;
    }

    protected void lbtnEditModel_Click(object sender, EventArgs e)
    {
        try
        {
            IDCRespository<DC.Entity.ProductModel> mRepository = new DCRepository<DC.Entity.ProductModel>();
            var mData = mRepository.GetAll().Where(o=>o.ModelID == int.Parse(ddlModel.SelectedValue)).FirstOrDefault();
            if (mData != null)
            {
                txtModel.Text = mData.ModelName;
                hfModelid.Value = mData.ModelID.ToString();
                ShowModel();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void lbtnDeleteModel_Click(object sender, EventArgs e)
    {
        if (ddlModel.SelectedValue != "")
        {
            IDCRespository<DC.Entity.ProductModel> mRepository = new DCRepository<DC.Entity.ProductModel>();
            var mData = mRepository.GetAll().Where(o => o.ModelID == int.Parse(ddlModel.SelectedValue)).FirstOrDefault();
            if (mData != null)
            {
                mRepository.Delete(mData);
                lblMsg.Text = "Deleted successfully";
               // lblMsg.ForeColor = System.Drawing.Color.Green;
            }

        }
    }

    protected void lbtnUpdateModel_Click(object sender, EventArgs e)
    {
        try
        {
            IDCRespository<DC.Entity.ProductModel> mRepository = new DCRepository<DC.Entity.ProductModel>();
            DC.Entity.ProductModel mEntity = new DC.Entity.ProductModel();
            
           
            int id = int.Parse(string.IsNullOrEmpty(hfModelid.Value) ? "0" : hfModelid.Value);
            if (id > 0)
            {
                mEntity = mRepository.GetAll().Where(r => r.ModelID == id).FirstOrDefault();
                mEntity.ModelName = txtModel.Text.Trim();
                mEntity.SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                var entity = mRepository.GetAll().Where(r => r.ModelID != id && r.ModelName.ToLower() == mEntity.ModelName.ToLower() && r.SubCategoryID == mEntity.SubCategoryID).FirstOrDefault();
                if (entity == null)
                {
                    mRepository.Edit(mEntity);
                    lblMsg.Text = "Updated successfully";
                    HideModel();
                    hfModelid.Value = "0";
                }
                else
                {
                    lblError.Text = "Item already exists";
                }
            }
            else
            {
                mEntity.ModelName = txtModel.Text.Trim();
                mEntity.SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                var entity = mRepository.GetAll().Where(r => r.ModelName.ToLower() == mEntity.ModelName.ToLower() && r.SubCategoryID == mEntity.SubCategoryID).FirstOrDefault();
                if (entity == null)
                {
                    mRepository.Add(mEntity);
                    lblMsg.Text = "Added successfully";
                    HideModel();
                    hfModelid.Value = "0";
                }
                else
                {
                    lblError.Text = "Item already exists";
                }
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {

    }
}