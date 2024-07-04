using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioPaymentSettingsBAL
    {
        public static bool IsPaymentActive()
        {
            bool retval = false;
            try
            {
                if (System.Web.HttpContext.Current.Session["PayStatus"] == null)
                {
                    var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                    if (payDetials != null)
                    {
                        var mid = payDetials.Vendor.Trim();
                        if (mid.Length == 0)
                        {
                            retval = false;
                            sessionKeys.PayStatus = retval; //System.Web.HttpContext.Current.Session["PayStatus"] = retval;
                        }
                        else
                        {
                            retval = true;
                            sessionKeys.PayStatus = retval;
                        }
                    }
                }
                else
                {
                    retval = sessionKeys.PayStatus;
                }
              
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }

        public static void Clear_IsPaymentActive_Session()
        {
            System.Web.HttpContext.Current.Session["PayStatus"] = null;
        }

        public static bool PortfolioPaymentSettingsBAL_AddUpdate(PortfolioMgt.Entity.PortfolioPaymentSetting ps)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
            var p = pRep.GetAll().Where(o => o.PortfolioID == ps.PortfolioID ).FirstOrDefault();
            if(p== null)
            {
                p = new Entity.PortfolioPaymentSetting();

                p.consumerKey = ps.consumerKey;
                p.consumerSecret = ps.consumerSecret;
                p.Host = ps.Host;
                p.Username = ps.Username;
                p.Vendor = ps.Vendor;
                p.PortfolioID = ps.PortfolioID;
                p.PayType = "cardconnect";
                p.Password = ps.Password;
                p.Partner = ps.Partner;
                p.Notes = ps.Notes;
                p.LoggedBy = ps.LoggedBy;
                p.LoggedDatetime = ps.LoggedDatetime;
                p.IsActive = true;


                p.LoggedBy = sessionKeys.UID;
                p.LoggedDatetime = DateTime.Now;
                //ps.CardFee = 0;
                //ps.TransactionFee = 0;
                p.Host = "https://www.payfast.co.za/eng/process?";
                p.CardFee = ps.CardFee;
                p.FixedPrice = ps.FixedPrice;
                p.TransactionFee = ps.TransactionFee;
                p.IsActive = true;
                pRep.Add(ps);
                if(ps.ID >0)
                retval = true;
            }
            else
            {
                p.Host = ps.Host;
                p.Notes = ps.Notes;
                p.Partner = ps.Partner;
                p.Password = ps.Password;
                p.PayType = "cardconnect";
                p.Username = ps.Username;
                p.Vendor = ps.Vendor;
               
                p.CardFee = ps.CardFee;
                p.FixedPrice = ps.FixedPrice;
                p.TransactionFee = ps.TransactionFee;
               
                p.consumerSecret = ps.consumerSecret;
                p.consumerKey = ps.consumerKey;
                p.IsActive = true;
                p.MonthlyPriceID = ps.MonthlyPriceID;
                p.WeekPriceID = ps.WeekPriceID;
                
                pRep.Edit(p);
                retval = true;
            }
            return retval;
        }
        public static bool PortfolioPaymentSettingsBAL_UpdatePlatformFee(int portfolioID, double platformfee)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
            var plist = pRep.GetAll().Where(o => o.PortfolioID == portfolioID ).ToList();
            foreach(var p in plist)
            {
               
                p.TransactionFee = platformfee;
              
                pRep.Edit(p);
                retval = true;
            }
            return retval;
        }

        public static PortfolioMgt.Entity.PortfolioPaymentSetting PortfolioPaymentSettingsBAL_SelectByCompany()
        {
            
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
            return pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
           
        }
        public static List<PortfolioMgt.Entity.PortfolioPaymentSetting> PortfolioPaymentSettingsBAL_SelectAll()
        {
            
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
            return pRep.GetAll().ToList();

        }
        public static PortfolioMgt.Entity.PortfolioPaymentSetting PortfolioPaymentSettingsBAL_SelectByCompany(int portfolioid,string paytype= "cardconnect")
        {
            
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
            return pRep.GetAll().Where(o => o.PortfolioID == portfolioid).FirstOrDefault();

        }
    }
}
