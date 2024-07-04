using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace PortfolioMgt.BAL
{
    public class PortfolioTypeBAL
    {
        public static void PortfolioTypeBAL_Copy(int fromid ,int toid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioType>();
            var plist= pRep.GetAll().ToList();

            var fromEntity = plist.Where(o => o.ID == fromid).FirstOrDefault();
            var toEntity = plist.Where(o => o.ID == toid).FirstOrDefault();

            //get list of categories
            IDCRespository<DC.Entity.Category> cRep = new DCRepository<DC.Entity.Category>();
            IDCRespository<DC.Entity.SubCategory> sRep = new DCRepository<DC.Entity.SubCategory>();

            var cList = cRep.GetAll().Where(o => o.TypeOfRequestID == fromEntity.ID).ToList();
            if (cList.Count > 0)
            {
                foreach (var c in cList)
                {
                    //check name already exists
                    if (cRep.GetAll().Where(o => o.TypeOfRequestID == toEntity.ID && o.Name.ToLower() == c.Name.ToLower()).Count() == 0)
                    {
                        //add 
                        var nc = new DC.Entity.Category();
                        nc.Name = c.Name;
                        nc.TypeOfRequestID = toEntity.ID;
                        cRep.Add(nc);
                        //get subcategorylist
                        var sList = sRep.GetAll().Where(o => o.CategoryID == c.ID).ToList();
                        if (sList.Count > 0)
                        {
                            foreach (var s in sList)
                            {
                                //check already exista or not
                                if(sRep.GetAll().Where(o => o.CategoryID == nc.ID && o.Name.ToLower() == s.Name.ToLower()).Count() ==0)
                                {
                                    //add new entiry
                                    var sc = new DC.Entity.SubCategory();
                                    sc.Name = s.Name;
                                    sc.CategoryID = nc.ID;
                                    sRep.Add(sc);
                                }
                            }
                        }
                    }
                }
            }


        }
        public static IQueryable<PortfolioMgt.Entity.PortfolioType> PortfolioTypeBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioType>();
            return pRep.GetAll();
        }

        public static PortfolioMgt.Entity.PortfolioType PortfolioTypeBAL_Add(string name)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioType>();
            var p = pRep.GetAll().Where(o => o.Portfoliotype1.ToLower() == name.ToLower()).FirstOrDefault();
            if(p == null)
            {
                p = new PortfolioMgt.Entity.PortfolioType();
                p.Portfoliotype1 = name;
                pRep.Add(p);
            }
            return p;
        }
        public static PortfolioMgt.Entity.PortfolioType PortfolioTypeBAL_Update(string name,int id)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioType>();
            var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (p != null)
            {
                if (pRep.GetAll().Where(o => o.ID != id && o.Portfoliotype1.ToLower() == name.ToLower()).Count() == 0)
                {
                    p.Portfoliotype1 = name;
                    pRep.Edit(p);
                }
            }
            return p;
        }

        public static bool PortfolioTypeBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioType> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioType>();
            var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (p != null)
            {
                pRep.Delete(p);
                retval = true;
            }
            return retval;
        }

    }
}
