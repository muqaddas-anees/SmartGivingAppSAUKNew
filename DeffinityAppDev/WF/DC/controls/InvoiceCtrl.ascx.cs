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
    public partial class InvoiceCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetImageHandler();
                    BindInvoiceGrid();
                    BindCompanyInfo();
                    BindVATData();
                    SetInvoiceData();
                    //List<Jqgrid> flsList = new List<Jqgrid>();
                    //IDCRespository<FLSDetail> flsrepository = new DCRepository<FLSDetail>();
                    BindRequesterInfo();
                }
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private void SetInvoiceData()
        {
            IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
            var inDetails = inRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
            if (inDetails != null)
            {
                lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
            }
            else
            {
                inDetails = new CallInvoice();
                inDetails.CreatedDate = DateTime.Now;
                inDetails.CallID = QueryStringValues.CallID;
                inRepository.Add(inDetails);

                lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
            }
        }

        private void BindRequesterInfo()
        {
            var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
            lblClientInfo.Text = fls.RequesterName + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo;
        }

        private void BindVATData()
        {
            IProjectRepository<ProjectMgt.Entity.ProjectDefault> pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
            var pdetails = pRepository.GetAll().FirstOrDefault();
            if (pRepository != null)
            {
                lblVAT.Text = string.Format("{0:F2}", pdetails.VAT.HasValue ? pdetails.VAT.Value : 0.00);
            }
        }

        private void BindCompanyInfo()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var p = prep.GetAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
            if (p != null)
            {
                lblAccountName.Text = p.PortFolio;
                //txtAccountNumber.Text = p.AccountNumber;
                //txtIBAN.Text = p.IBAN;
                //txtSortCode.Text = p.SortCode;
                lblSWIFTCode.Text = p.SwiftCode;
                lblVATReg.Text = p.TaxReg;
            }
        }
        public void GetImageHandler()
        {
            try
            {
                IDCRespository<FLSDetail> flsrepository = new DCRepository<FLSDetail>();
                IUserRepository<UserMgt.Entity.UserDetail> crepository = new UserRepository<UserMgt.Entity.UserDetail>();
                var f = flsrepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                if (f != null)
                {
                    ImgContractor.Src = "~/WF/Admin/ImageHandler.ashx?type=user&id=" + (f.UserID.HasValue ? f.UserID.Value : 0).ToString();
                    var c = crepository.GetAll().Where(o => o.UserId == (f.UserID.HasValue ? f.UserID.Value : 0)).FirstOrDefault();
                    if (c != null)
                    {
                        // lbl
                        lblToDayDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), f.ScheduledDate.Value);
                        LblLocation.Text = c.Town + " ," + c.PostCode;
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindInvoiceGrid()
        {
            try
            {
                List<GridInvoiceFieldsCls> Glist = new List<GridInvoiceFieldsCls>();
                IDCRespository<Incident_Service> inrepository = new DCRepository<Incident_Service>();
                var f = inrepository.GetAll().Where(o => o.IncidentID == QueryStringValues.CallID).ToList();
                if (f != null)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> ivendor = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
                    var getlist = ivendor.GetAll().Where(o => f.Select(p => p.ServiceID).ToArray().Contains(o.ID)).ToList();
                    int i = 0;
                    foreach (var l in f)
                    {
                        i = i + 1;
                        Glist.Add(new GridInvoiceFieldsCls()
                        {
                            Id = i,
                            ProductName = l.ServiceID == 0? l.ServiceDescription: getlist.Where(o => o.ID == l.ServiceID).Select(p => p.Description).FirstOrDefault(),
                            Quantity = l.QTY.HasValue?l.QTY.Value:0.00,
                            Price = (l.QTY * l.SellingPrice)
                        });
                    }


                    //GridInvoiceFieldsCls g1 = new GridInvoiceFieldsCls()
                    //{
                    //    Id = 1,
                    //    ProductName = "Up do view time they shot",
                    //    Quantity = "1",
                    //    Price = "$400"
                    //};
                    //Glist.Add(g1);
                    //g1 = new GridInvoiceFieldsCls()
                    //{
                    //    Id = 2,
                    //    ProductName = "On am we offices expense thought",
                    //    Quantity = "1",
                    //    Price = "$1290"
                    //};
                    //Glist.Add(g1);
                    GridInvoiceEntries.DataSource = Glist;
                    GridInvoiceEntries.DataBind();
                    var sp = Glist.Select(o => o.Price).Sum();
                    var vat = 0.00;
                    lblSubTotalamount.Text = string.Format("{0:F2}", sp);
                    lblVAT.Text = vat.ToString() + "%";
                    var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));
                    lblGrandTotal.Text = string.Format("{0:F2}", sp + s1);

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    public class GridInvoiceFieldsCls
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
    }
}