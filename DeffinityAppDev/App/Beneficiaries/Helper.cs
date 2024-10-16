using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgt.DAL;

namespace DeffinityAppDev.App.Beneficiaries
{
    public class Helper
    {
        public static string GetPersonNamebyID(string ID)
        {
            using(var context=new UserDataContext())
            {
                var contr=context.v_contractors.FirstOrDefault(o=>o.ID.ToString()==ID);
                if (contr == null)
                    return "Unknown";

                return contr.ContractorName + " " + contr.LastName;
            }
            
        }
        public static string GetFundraiserNamebyID(string ID)
        {
            using (var context = new PortfolioDataContext())
            {
                var contr = context.TithingDefaultDetails.FirstOrDefault(o => o.ID.ToString() == ID);
                if (contr == null)
                    return "Unknown";

                return contr.Title;
            }

        }


    }
}