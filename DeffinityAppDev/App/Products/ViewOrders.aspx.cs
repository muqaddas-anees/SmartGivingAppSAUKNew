using Deffinity.CustomerManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Products
{
    public partial class ViewOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindCategory();
                BingGrid();
            }
        }
        private void BingGrid()
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();

                var pd = pdRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                IPortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> psRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();

                var ps = psRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o=>(o.IsPaid??false) == true).ToList();

                IPortfolioRepository<PortfolioMgt.Entity.ProductCategory> pcRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();

                var pc = pcRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                var rlist = (from p in ps
                             select new
                             {
                                 p.ID,
                                 p.ProductGuid,
                                 Status = p.Status?? "Pending Dispatch",
                                 ProductName = pd.Where(o => o.ProductGuid == p.ProductGuid).FirstOrDefault().ProductName,
                                 ProductDetails = StringTrimLength(pd.Where(o => o.ProductGuid == p.ProductGuid).FirstOrDefault().ProductDetails),

                                 ProductPriceDisplay = string.Format("{1}{0:N2}", p.ProductPrice??0.00, Deffinity.Utility.GetCurrencySymbol()),
                                 p.ProductPrice ,
                                 UnitsSold = 0,
                                 QTY = (p.ProductQTY ?? 1),
                                 Address = p.CuseromerAddress,
                                 Town = p.CuseromerTown,
                                 State = p.CuseromerState,
                                 Zipcode = p.CuseromerZipCode,
                                 DateSold = p.OrderDate,
                                 Category = GetCategory(p.ProductGuid, pc, pd.Where(o => o.ProductGuid == p.ProductGuid).FirstOrDefault()),
                                 Year = p.OrderDate.Value.Year,
                                 Month = p.OrderDate.Value.Month,
                                 CustomerName = p.CustomerFirstName + " " + p.CustomerLastName
                             }).ToList();
                GridProducts.DataSource = rlist;
                GridProducts.DataBind();

                lbltotal_thismonth.InnerText = string.Format("{1}{0:N2}", rlist.Where(o => (o.Month == DateTime.Now.Month) && (o.Year == DateTime.Now.Year)).Sum(s => s.ProductPrice ?? 0.00), Deffinity.Utility.GetCurrencySymbol());
                lbltotal_thisyear.InnerText = string.Format("{1}{0:N2}", rlist.Where(o => (o.Year == DateTime.Now.Year)).Sum(s => s.ProductPrice ?? 0.00), Deffinity.Utility.GetCurrencySymbol()); ;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetCategory(string ProductGuid, List< PortfolioMgt.Entity.ProductCategory> c, PortfolioMgt.Entity.ProductDetail p)
        {

            string retval = "";

            var pd = p.CategoryID;
            var cd = c.Where(o => o.ID == pd).FirstOrDefault();
            if (cd != null)
                retval = cd.Name;

            return retval;

        }
        private string StringTrimLength(string val)
        {

            string retval = Deffinity.Utility.RemoveHTMLTags(val).ToString();

            if (retval.Length > 200)
            {
                retval = retval.Substring(0, 199) + "...";
            }


            return retval;
        }

        protected void GridProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();

                    var pd = pdRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();

                    Response.Redirect("~/App/Products/AddProduct.aspx?unid=" + pd.ProductGuid, false);
                }
                else if (e.CommandName == "del")
                {
                    IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();

                    var pd = pdRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();

                    pdRep.Delete(pd);
                    BingGrid();
                }

                else if (e.CommandName == "save")
                {

                    Button btn = (Button)e.CommandSource;
                    GridViewRow row = (GridViewRow)btn.NamingContainer;

                    // Find the DropDownList in the row.
                    DropDownList ddl = (DropDownList)row.FindControl("ddlStatus");


                    IPortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();

                    var pd = pdRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (pd != null)
                    {
                        pd.Status = ddl.SelectedValue;
                        pd.StatusUpdateDate = DateTime.Now;
                        pdRep.Edit(pd);
                        BingGrid();

                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page,Resources.DeffinityRes.UpdatedSuccessfully,"Ok");
                    }
                }

                //save
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BingGrid();
        }
        protected static string GetImageUrl(string contactsId)
        {
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Products/") + "Product_org_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt=" + DateTime.Now.TimeOfDay, contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Products/Product_org_{0}.png?dt=" + DateTime.Now.TimeOfDay, contactsId.ToString());
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            return "~/ImageHandler.ashx?id=" + contactsId.ToString() + "&s=" + ImageManager.file_section_online;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void GridProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Assuming your data source has a column named "SelectedValue" that contains the value to be selected in the DropDownList
                    string valueToSelect = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                    // Find the DropDownList and set its value
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlStatus");
                    if (ddl != null)
                    {
                        ddl.SelectedValue = valueToSelect;
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}