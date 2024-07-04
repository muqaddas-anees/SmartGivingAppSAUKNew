using QRCoder;
using shortid.Configuration;
using shortid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;

namespace DeffinityAppDev.App.Products
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }
                PortfolioMgt.BAL.ProductCategoryBAL.ProductCategoryBAL_AddDefaults();
                BindCategory();
                if (QueryStringValues.UNID.Length>0)
                {
                    
                    SetProductDetails(QueryStringValues.UNID);
                }
               
               
            }
        }
        private void BindCategory()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProductCategory> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();

                var pd = pdRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                ddlCategory.DataSource = pd.OrderBy(o => o.Name).ToList();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SetProductDetails(string unid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();

                var pd = pdRep.GetAll().Where(o => o.ProductGuid == unid).FirstOrDefault();
                if(pd != null)
                {

                    txtTitle.Text = pd.ProductName;
                    txtDescriptionArea.Text = pd.ProductDetails;
                    txtAmount.Text = string.Format("{0:N2}", (pd.ProductPrice));
                    ddlCategory.SelectedValue = pd.CategoryID.ToString();
                    img.ImageUrl = GetImageUrl(pd.ProductGuid.ToString());
                    txtTotalUnits.Text = (pd.TotalUnits ?? 0).ToString();
                    chkActive.Checked = pd.IsActive.HasValue ? pd.IsActive.Value : false;
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Products/") + "Product_org_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt="+DateTime.Now.TimeOfDay, contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt=" + DateTime.Now.TimeOfDay, contactsId.ToString());
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();

            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_online;

        }
        private void BannerUpLoad(string tithingid)
        {


            if (imgBanner.PostedFile.FileName.Length > 0)
            {
                Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgBanner.PostedFile.InputStream);
                ImageManager.SaveProductImage_setpath(imgBanner.FileBytes, tithingid.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                // DisplayLogo();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();

               
                if (QueryStringValues.UNID.Length > 0)
                {
                    var pd = pdRep.GetAll().Where(o => o.ProductGuid == QueryStringValues.UNID).FirstOrDefault();

                    if(pd != null)
                    {
                        
                        pd.ProductName = txtTitle.Text.Trim();
                        pd.ProductDetails = txtDescriptionArea.Text;
                        pd.ProductPrice = Convert.ToDouble(txtAmount.Text.Trim());
                        // pd.ProductGuid = Guid.NewGuid().ToString();
                        pd.IsActive = chkActive.Checked;
                        pd.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                        pd.DateLogged = DateTime.Now;
                        pd.TotalUnits = Convert.ToInt32(txtTotalUnits.Text.Trim());
                        pdRep.Edit(pd);
                        BannerUpLoad(pd.ProductGuid.ToString());
                        sessionKeys.Message = "Product details updated successfully";
                        Response.Redirect("~/App/Products/AddProduct.aspx?unid="+ pd.ProductGuid,false);

                    }
                }
                else
                {
                   

                    var  pd = new PortfolioMgt.Entity.ProductDetail();
                    pd.PortfolioID = sessionKeys.PortfolioID;
                    pd.ProductName = txtTitle.Text.Trim();
                    pd.ProductDetails = txtDescriptionArea.Text;
                    pd.ProductPrice = Convert.ToDouble(txtAmount.Text.Trim());
                    pd.ProductGuid = Guid.NewGuid().ToString();
                    pd.IsActive = chkActive.Checked;
                    pd.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    pd.DateLogged = DateTime.Now;
                    pd.TotalUnits = Convert.ToInt32(txtTotalUnits.Text.Trim());

                    var options = new GenerationOptions(useSpecialCharacters: false);
                    string shortid = ShortId.Generate(options);
                    pd.ShortCode = shortid;
                    pdRep.Add(pd);
                    BannerUpLoad(pd.ProductGuid.ToString());
                    sessionKeys.Message = "Product details added successfully";
                    Response.Redirect("~/App/Products/AddProduct.aspx?unid=" + pd.ProductGuid, false);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}