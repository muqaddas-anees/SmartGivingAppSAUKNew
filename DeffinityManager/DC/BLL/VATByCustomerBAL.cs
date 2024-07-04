using DC.DAL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BAL
{
    public class VATByCustomerBAL
    {

        static double defaultTax = 12;
        #region Update VATByCustomer
        public static void VATByCustomer_Update(double vatval)
        {
            VATByCustomer_Update(vatval, sessionKeys.PortfolioID);

        }
        public static void VATByCustomer_Update(double vatval,int portfolioID)
        {
            using (DCDataContext dc = new DCDataContext())
            {
                VATByCustomer acdetails = (from a in dc.VATByCustomers
                                           where a.CustomerID == portfolioID
                                           select a).FirstOrDefault();
                if (acdetails == null)
                {
                    var v = new VATByCustomer();
                    v.CustomerID = portfolioID;
                    v.VATVal = vatval;
                    dc.VATByCustomers.InsertOnSubmit(v);
                    dc.SubmitChanges();
                }
                else
                {
                    acdetails.VATVal = vatval;
                    dc.SubmitChanges();
                }

            }

        }

        public static void VATByCustomer_SetDefault()
        {
            using (DCDataContext dc = new DCDataContext())
            {
                VATByCustomer acdetails = (from a in dc.VATByCustomers
                                           where a.CustomerID == sessionKeys.PortfolioID
                                           select a).FirstOrDefault();
                if (acdetails == null)
                {
                    var v = new VATByCustomer();
                    v.CustomerID = sessionKeys.PortfolioID;
                    v.VATVal = defaultTax;
                    dc.VATByCustomers.InsertOnSubmit(v);
                    dc.SubmitChanges();
                }
              

            }

        }
        #endregion

        #region Select Vat
        public static double VATByCustomer_select()
        {
            double retval = VATByCustomer_select(sessionKeys.PortfolioID);
            return retval;
        }

        public static double VATByCustomer_select(int PortfolioID)
        {
            double retval = 0.0;
            using (DCDataContext dc = new DCDataContext())
            {
                var v = dc.VATByCustomers.Where(o => o.CustomerID == PortfolioID).FirstOrDefault();
                if (v == null)
                {
                    //set defautl tax 12 %
                    VATByCustomer_Update(defaultTax, PortfolioID);

                    v = dc.VATByCustomers.Where(o => o.CustomerID == PortfolioID).FirstOrDefault();
                }
                if (v != null)
                    retval = v.VATVal;
                else
                    retval = 0.0;
            }
            return retval;
        }
        #endregion
    }
}
