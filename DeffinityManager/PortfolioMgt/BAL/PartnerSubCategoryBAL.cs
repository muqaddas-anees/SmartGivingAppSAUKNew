using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class PartnerSubCategoryBAL
    {
       public static PartnerSubCategory PartnerSubCategoryBAL_Add(PartnerSubCategory sub)
        {
            IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
            pRep.Add(sub);
            return sub;
        }

       public static PartnerSubCategory PartnerSubCategoryBAL_Update(PartnerSubCategory sub)
       {
           IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
           var s = pRep.GetAll().Where(o=>o.ID == sub.ID).FirstOrDefault();
           if(s != null)
           {
               s.SubCategoryName = sub.SubCategoryName;
               s.IsDeleted = sub.IsDeleted;
           }

           pRep.Edit(s);
           return sub;
       }
       public static bool PartnerSubCategoryBAL_IsExists(int categoryID, string subcategoryName)
       {
           IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
           var retEntity = pRep.GetAll().Where(o => o.SubCategoryName.ToLower().Trim() == subcategoryName.ToLower().Trim() && o.PartnerCategoryID == categoryID).FirstOrDefault();
           if (retEntity != null)
               return true;
           else
               return false;

       }
       public static List<PartnerSubCategory> PartnerSubCategoryBAL_SelectByCategoryID(int categoryID)
       {
           IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
           return pRep.GetAll().Where(o => o.PartnerCategoryID == categoryID && o.IsDeleted == false).ToList();

       }
       public static PartnerSubCategory PartnerSubCategoryBAL_Select(int id)
       {
           IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
           return  pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
          
       }
       public static List<PartnerSubCategory> PartnerSubCategoryBAL_SelectAll()
       {
           IPortfolioRepository<PartnerSubCategory> pRep = new PortfolioRepository<PartnerSubCategory>();
           return pRep.GetAll().ToList();

       }

    }
}
