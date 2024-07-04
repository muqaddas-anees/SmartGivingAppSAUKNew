using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class ProductCategoryBAL
    {

        public static void ProductCategoryBAL_AddDefaults()
        {

            PortfolioRepository<PortfolioMgt.Entity.ProductCategory> pRep = new PortfolioRepository<PortfolioMgt.Entity.ProductCategory>();
            var dList = pRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).OrderBy(o => o.Name).ToList();

            if(dList.Count ==0)
            {
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Books" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Merchandize" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Media" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Food and Drinks" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Health" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Clothing" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Hardware" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Digital Services" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Perfumes" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Childrens" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Greeting Cards" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Gifts" });
                pRep.Add(new Entity.ProductCategory() { PortfolioID = sessionKeys.PortfolioID, Name = "Artwork" });
               
            }
        }

    }
}
