using shortid.Configuration;
using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;

namespace DeffinityAppDev.App.Products
{
    public partial class ViewProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BingGrid();
            }
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
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_online;

        }
        private void BingGrid()
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ProductDetail> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProductDetail>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker> psRep = new PortfolioRepository<PortfolioMgt.Entity.ProductSalesTracker>();

                var alist = pdRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                try
                {
                    //update shotr url
                    foreach (var eventEntity in alist.Where(o => o.ShortCode == null).ToList())
                    {

                        if (eventEntity.ShortCode == null)
                        {
                            var options = new GenerationOptions(useSpecialCharacters: false);
                            string shortid = ShortId.Generate(options);
                            eventEntity.ShortCode = shortid;

                            pdRep.Edit(eventEntity);


                        }
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

                var pd = pdRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                var ps = psRep.GetAll().Where(o => pd.Select(p => p.ProductGuid).ToList().Contains(o.ProductGuid)).Where(o=>(o.IsPaid??false)== true).ToList();

                var rlist = (from p in pd
                             select new
                             {
                                 p.ID,
                                 p.ProductGuid,
                                 Status = (p.IsActive ?? true) ? "<span class='badge badge-success'>Active</span>" : "<span class='badge badge-danger'>Inactive</span>",
                                 p.ProductName,
                                 ProductDetails = StringTrimLength(p.ProductDetails),
                                 TotalUnits = p.TotalUnits - ps.Where(o => o.ProductGuid == p.ProductGuid).Count(),
                                 ProductPriceDisplay = string.Format("{1}{0:N2}", p.ProductPrice, Deffinity.Utility.GetCurrencySymbol()),
                                 p.ProductPrice,
                                 UnitsSold = ps.Where(o=>o.ProductGuid == p.ProductGuid).Count(),
                                 Url = Deffinity.systemdefaults.GetWebUrl()+ "/Product/"+p.ShortCode
                             }).ToList();
                GridProducts.DataSource = rlist;
                GridProducts.DataBind();

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       private string StringTrimLength(string val)
        {

            string retval = Deffinity.Utility.RemoveHTMLTags(val).ToString();

            if(retval.Length >200)
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
                else if (e.CommandName == "copy")
                {

                }
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

        protected void GridProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the CopyButton control in the current row
                Button copyButton = (Button)e.Row.FindControl("CopyButton");

                // Get the URL value from the data item
                string url = DataBinder.Eval(e.Row.DataItem, "Url").ToString();

                // Set the URL as a data attribute on the CopyButton
                copyButton.Attributes["data-url"] = url;
            }
        }
    }
}