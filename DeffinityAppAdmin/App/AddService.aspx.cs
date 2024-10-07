using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class AddService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load any existing services if needed
                if(QueryStringValues.MID>0)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
                LoadServices();
                PopulateCurrencyDropDowns();
               BindService();
            }
        }

        private void BindService()
        {
            // Check if the query string contains MID, which indicates we're in edit mode
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                // Parse the MID value to get the service ID
                int serviceId = int.Parse(Request.QueryString["MID"]);

                using (var context = new PortfolioDataContext())
                {
                    // Retrieve the service with the given ID
                    var service = context.MarketplaceProducts.FirstOrDefault(s => s.Id == serviceId);

                    if (service != null)
                    {
                        // Populate form fields with the service data
                        txtModuleTitle.Text = service.Title;
                        txtModuleDescription.Text = service.Description;
                        txtVideoExplainerURL.Text = service.VideoDescriptionUrl;

                        ddlCurrencySmallCharity.SelectedValue = service.CurrencyForSmallCharities;
                        txtPriceSmallCharity.Text = service.PriceForSmallCharities.ToString();

                        ddlCurrencyMediumCharity.SelectedValue = service.CurrencyForMediumCharities;
                        txtPriceMediumCharity.Text = service.PriceForMediumCharities.ToString();

                        ddlCurrencyLargeCharity.SelectedValue = service.CurrencyForLargeCharities;
                        txtPriceLargeCharity.Text = service.PriceForLargeCharities.ToString();


                        if (service.IsLivestream??false)
                        {
                            ddlServices.SelectedValue = "Livestream";
                        }
                        else if (service.IsOnlineShop ?? false)
                        {
                            ddlServices.SelectedValue = "OnlineShop";
                        }
                        else if (service.IsPeerToPeerFundraising ?? false)
                        {
                            ddlServices.SelectedValue = "PeerToPeerFundraising";
                        }
                        else if (service.IsBeneficiaryManagement ?? false)
                        {
                            ddlServices.SelectedValue = "BeneficiaryManagement";
                        }
                        else if (service.IsProjectManagement ?? false)
                        {
                            ddlServices.SelectedValue = "ProjectManagement";
                        }
                        else if (service.IsAI ?? false)
                        {
                            ddlServices.SelectedValue = "AI";
                        }
                        else if (service.IsAcademy ?? false)
                        {
                            ddlServices.SelectedValue = "Academy";
                        }
                        else if (service.IsOtherServices ?? false)
                        {
                            ddlServices.SelectedValue = "OtherServices";
                        }



                        txtDiscountAnnualPrice.Text = service.AnnualDiscount.ToString();
                        TxtBuyNow.Text= service.textforbuynowbutton;
                        txtVideoBtn.Text = service.textforvideobutton;

                        txtBuyNowSmallCharity.Text = service.UrlForSmall;
                        txtBuyNowLargeCharity.Text = service.UrlForLarge;
                        txtBuyNowMediumCharity.Text = service.UrlForMedium;


                        txtTrialPeriod.Text = service.TrialPeriod.ToString();
                        chkModuleAvailable.Checked = service.IsModuleAvailable??false;

                        // Optionally bind the image if needed
                        var image = context.FileDatas.FirstOrDefault(f => f.FileID == service.Id.ToString());
                        if (image != null)
                        {
                            // Bind image data if necessary, e.g., display image or set image path
                            imgPreview.ImageUrl = "/imagehandler.ashx?id=" + image.FileID + "&s=" + ImageManager.file_section_marketplace;  // Example
                        }
                    }
                }
            }
        }

        private void PopulateCurrencyDropDowns()
        {
            // Define currency options
            var currencies = new[]
            {
        new { Text = "GBP", Value = "GBP" },
        new { Text = "USD", Value = "USD" },
        new { Text = "ZAR", Value = "ZAR" }
    };

            // Bind currency options to each dropdown
            ddlCurrencySmallCharity.DataSource = currencies;
            ddlCurrencySmallCharity.DataTextField = "Text";
            ddlCurrencySmallCharity.DataValueField = "Value";
            ddlCurrencySmallCharity.DataBind();

            ddlCurrencyMediumCharity.DataSource = currencies;
            ddlCurrencyMediumCharity.DataTextField = "Text";
            ddlCurrencyMediumCharity.DataValueField = "Value";
            ddlCurrencyMediumCharity.DataBind();

            ddlCurrencyLargeCharity.DataSource = currencies;
            ddlCurrencyLargeCharity.DataTextField = "Text";
            ddlCurrencyLargeCharity.DataValueField = "Value";
            ddlCurrencyLargeCharity.DataBind();

        }

        private void LoadServices()
        {
            using (var context = new PortfolioDataContext())
            {
                // Fetch data from MarketplaceProducts table
                var services = context.MarketplaceProducts.ToList();

                // Bind the data to existing DropDownLists
                ddlCurrencySmallCharity.DataSource = services;
                ddlCurrencySmallCharity.DataTextField = "Title";   // Display the service title
                ddlCurrencySmallCharity.DataValueField = "Id";     // Use the Id field as the value
                ddlCurrencySmallCharity.DataBind();

                // Optionally, do the same for other dropdowns if you need
                ddlCurrencyMediumCharity.DataSource = services;
                ddlCurrencyMediumCharity.DataTextField = "Title";
                ddlCurrencyMediumCharity.DataValueField = "Id";
                ddlCurrencyMediumCharity.DataBind();

                ddlCurrencyLargeCharity.DataSource = services;
                ddlCurrencyLargeCharity.DataTextField = "Title";
                ddlCurrencyLargeCharity.DataValueField = "Id";
                ddlCurrencyLargeCharity.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Create a new service and populate from form fields
        
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Implement delete functionality here, e.g., using a service ID
        }

        private byte[] SaveBannerImage()
        {
            if (fuBannerImage.HasFile)
            {
                // Convert uploaded file to byte array to store in the database
                using (BinaryReader reader = new BinaryReader(fuBannerImage.PostedFile.InputStream))
                {
                    return reader.ReadBytes(fuBannerImage.PostedFile.ContentLength);
                }
            }
            return null; // Return null if no file was uploaded
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new PortfolioDataContext())
                {
                    MarketplaceProduct service;

                    // Check if MID exists in the query string to determine if we're in edit mode
                    if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
                    {
                        // Parse MID as an integer
                        int serviceId = int.Parse(Request.QueryString["MID"]);

                        // Retrieve the existing service
                        service = context.MarketplaceProducts.FirstOrDefault(s => s.Id == serviceId);

                        if (service != null)
                        {
                            // Update the existing service
                            service.Title = txtModuleTitle.Text;
                            service.Description = txtModuleDescription.Text;
                            service.VideoDescriptionUrl = txtVideoExplainerURL.Text;

                            service.CurrencyForSmallCharities = ddlCurrencySmallCharity.SelectedValue;
                            service.PriceForSmallCharities = decimal.Parse(txtPriceSmallCharity.Text);

                            service.CurrencyForMediumCharities = ddlCurrencyMediumCharity.SelectedValue;
                            service.PriceForMediumCharities = decimal.Parse(txtPriceMediumCharity.Text);

                            service.CurrencyForLargeCharities = ddlCurrencyLargeCharity.SelectedValue;
                            service.PriceForLargeCharities = decimal.Parse(txtPriceLargeCharity.Text);

                            service.textforbuynowbutton = TxtBuyNow.Text;
                            service.textforvideobutton = txtVideoBtn.Text;

                            service.AnnualDiscount = decimal.Parse(txtDiscountAnnualPrice.Text);

                            service.UrlForSmall = txtBuyNowSmallCharity.Text;
                            service.UrlForLarge = txtBuyNowLargeCharity.Text;
                            service.UrlForMedium = txtBuyNowMediumCharity.Text;

                            service.TrialPeriod = int.Parse(txtTrialPeriod.Text);
                            service.IsModuleAvailable = chkModuleAvailable.Checked;

                            service.IsLivestream = false;
                            service.IsOnlineShop = false;
                            service.IsPeerToPeerFundraising = false;
                            service.IsBeneficiaryManagement = false;
                            service.IsProjectManagement = false;
                            service.IsAI = false;
                            service.IsAcademy = false;
                            service.IsOtherServices = false;

                            // Set the selected service to true based on the dropdown value
                            switch (ddlServices.SelectedValue)
                            {
                                case "Livestream":
                                    service.IsLivestream = true;
                                    break;
                                case "OnlineShop":
                                    service.IsOnlineShop = true;
                                    break;
                                case "PeerToPeerFundraising":
                                    service.IsPeerToPeerFundraising = true;
                                    break;
                                case "BeneficiaryManagement":
                                    service.IsBeneficiaryManagement = true;
                                    break;
                                case "ProjectManagement":
                                    service.IsProjectManagement = true;
                                    break;
                                case "AI":
                                    service.IsAI = true;
                                    break;
                                case "Academy":
                                    service.IsAcademy = true;
                                    break;
                                case "OtherServices":
                                    service.IsOtherServices = true;
                                    break;
                                default:
                                    // No valid service selected
                                    break;
                            }


                            // Handle updating images, if necessary
                            var existingImage = context.FileDatas.FirstOrDefault(f => f.FileID == service.Id.ToString() && f.Section==ImageManager.file_section_marketplace);
                            byte[] img = SaveBannerImage();
                            if (existingImage != null && img != null)
                            {
                                existingImage.FileData1 = img;
                            }
                            else
                            {
                                if (img != null)
                                {
                                    var newimg = new FileData
                                    {
                                        FileID = service.Id.ToString(),
                                        FileData1 = img,
                                        Section = ImageManager.file_section_marketplace
                                    };
                                    context.FileDatas.InsertOnSubmit(newimg);
                                }
                            }
                            context.SubmitChanges();
                        }
                    }
                    else
                    {
                        // Insert a new service
                        service = new MarketplaceProduct
                        {
                            Title = txtModuleTitle.Text,
                            Description = txtModuleDescription.Text,
                            VideoDescriptionUrl = txtVideoExplainerURL.Text,

                            CurrencyForSmallCharities = ddlCurrencySmallCharity.SelectedValue,
                            PriceForSmallCharities = decimal.Parse(txtPriceSmallCharity.Text),

                            CurrencyForMediumCharities = ddlCurrencyMediumCharity.SelectedValue,
                            PriceForMediumCharities = decimal.Parse(txtPriceMediumCharity.Text),

                            CurrencyForLargeCharities = ddlCurrencyLargeCharity.SelectedValue,
                            PriceForLargeCharities = decimal.Parse(txtPriceLargeCharity.Text),

                            AnnualDiscount = decimal.Parse(txtDiscountAnnualPrice.Text),

                            TrialPeriod = int.Parse(txtTrialPeriod.Text),
                            IsModuleAvailable = chkModuleAvailable.Checked,




                            UrlForSmall = txtBuyNowSmallCharity.Text,
                            UrlForLarge = txtBuyNowLargeCharity.Text,
                            UrlForMedium = txtBuyNowMediumCharity.Text,
                            textforbuynowbutton = TxtBuyNow.Text,
                            textforvideobutton = txtVideoBtn.Text

                        };
                        service.IsLivestream = false;
                        service.IsOnlineShop = false;
                        service.IsPeerToPeerFundraising = false;
                        service.IsBeneficiaryManagement = false;
                        service.IsProjectManagement = false;
                        service.IsAI = false;
                        service.IsAcademy = false;
                        service.IsOtherServices = false;

                        // Set the selected service to true based on the dropdown value
                        switch (ddlServices.SelectedValue)
                        {
                            case "Livestream":
                                service.IsLivestream = true;
                                break;
                            case "OnlineShop":
                                service.IsOnlineShop = true;
                                break;
                            case "PeerToPeerFundraising":
                                service.IsPeerToPeerFundraising = true;
                                break;
                            case "BeneficiaryManagement":
                                service.IsBeneficiaryManagement = true;
                                break;
                            case "ProjectManagement":
                                service.IsProjectManagement = true;
                                break;
                            case "AI":
                                service.IsAI = true;
                                break;
                            case "Academy":
                                service.IsAcademy = true;
                                break;
                            case "OtherServices":
                                service.IsOtherServices = true;
                                break;
                            default:
                                // No valid service selected
                                break;
                        }

                        // Insert new service into the database
                        context.MarketplaceProducts.InsertOnSubmit(service);
                        context.SubmitChanges();  // Commit here to generate the service ID

                        // Now that the service ID is available, save the image
                        byte[] img = SaveBannerImage();
                        if (img != null)
                        {
                            var newimg = new FileData
                            {
                                FileID = service.Id.ToString(),  // Now service.Id is valid
                                FileData1 = img,
                                Section = ImageManager.file_section_marketplace
                            };
                            context.FileDatas.InsertOnSubmit(newimg);
                        }
                    }

                    // Commit any remaining changes to the database
                    context.SubmitChanges();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Saved Successfuly");
                    if (QueryStringValues.MID == 0)
                    {
                        Response.Redirect(Request.RawUrl + "?MID=" + service.Id);
                    }
                    // Redirect or show success message
                }
            }
            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            using (var context = new PortfolioDataContext())
            {
                var a = context.MarketplaceProducts.FirstOrDefault(o => o.Id == QueryStringValues.MID);
                if(a!=null)
                {
                    context.MarketplaceProducts.DeleteOnSubmit(a);
                }
            }
            Response.Redirect("Marketplace.aspx");
        }
    }
}