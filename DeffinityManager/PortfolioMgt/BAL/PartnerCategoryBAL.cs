using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class PartnerCategoryBAL
    {
        public static string Section_Diagnostic = "Diagnostic";
        public static string Section_Maintenance  = "Maintenance";

        //copy items to current customer
        public static bool PartnerCategoryBAL_Copy()
        {
            bool retval = false;
            var plist = PartnerCategoryBAL_SelectAll().Where(o => o.Section == Section_Maintenance && o.PartnerID == 2 && (o.PortfolioID.HasValue?o.PortfolioID.Value:0) ==0).ToList();

            foreach(var p in plist)
            {
                var catid_old = p.ID;
                var c= PartnerCategoryBAL_Add(p);

                //copy subcategory

                var slist= PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_SelectByCategoryID(catid_old);
                foreach (var s in slist)
                {
                    var subid_old = s.ID;
                    var ret_s=  PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Add(new PartnerSubCategory() { PartnerCategoryID = c.ID, SubCategoryName=s.SubCategoryName, IsDeleted=false });

                    //Copy services

                    var servicelist = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(subid_old);

                    foreach(var ser in servicelist)
                    {
                        var serviceid_old = ser.ID;

                        var ret_service = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Add(new PartnerService() { PartnerSubCategoryID = ret_s.ID, ServiceName = ser.ServiceName,TimeInMinutes = ser.TimeInMinutes, IsDeleted = false });


                        //get items

                        var itemslist = DC.BLL.CustomFormDesignerBAL.GetFieldByServiceID(serviceid_old);
                        foreach(var item in itemslist)
                        {


                            DC.BLL.CustomFormDesignerBAL.AddFields(new DC.Entity.FLSCustomField()
                            {
                                PartnerServiceID = ret_service.ID,
                                CustomerID = item.CustomerID,
                                DefaultText = item.DefaultText,
                                FieldPosition = item.FieldPosition,
                                FormID = item.FormID,
                                LabelName = item.LabelName,
                                ListValue = item.ListValue,
                                ListValueExt = item.ListValueExt,
                                Mandatory = item.Mandatory,
                                MaximumValue = item.MaximumValue,
                                MinimumValue = item.MinimumValue,
                                PartnerSubcategoryID = subid_old,
                                Position = item.Position,
                                TypeOfField = item.TypeOfField,

                            });
                            ;
                        }

                    }

                }


                //Copy services


                //copy checked items

                retval = true;
            }
          
            return retval;
        }
        public static PartnerCategory PartnerCategoryBAL_Add(PartnerCategory cat)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            cat.PortfolioID = sessionKeys.PortfolioID;
            pRep.Add(cat);
            return cat;
        }

        public static PartnerCategory PartnerCategoryBAL_Update(PartnerCategory cat)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            var s = pRep.GetAll().Where(o=>o.ID == cat.ID).FirstOrDefault();
            if (s != null)
            {
                s.CategoryName = cat.CategoryName;
                s.IsDeleted = cat.IsDeleted;
            }

            pRep.Edit(s);
            return s;
        }
        public static bool PartnerCategoryBAL_IsExists(int categoryID, string categoryName)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            var retEntity = pRep.GetAll().Where(o => o.CategoryName.ToLower().Trim() == categoryName.ToLower().Trim()).FirstOrDefault();
            if (retEntity != null)
                return true;
            else
                return false;

        }
        public static List<PartnerCategory> PartnerCategoryBAL_SelectByPartnerID(int partnerID,string section)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            return pRep.GetAll().Where(o => o.PartnerID == partnerID && o.Section == section).ToList();

        }
        public static List<PartnerCategory> PartnerCategoryBAL_SelectByPartnerID(int partnerID)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            return pRep.GetAll().Where(o => o.PartnerID == partnerID).ToList();

        }
        public static PartnerCategory PartnerCategoryBAL_Select(int id)
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            return pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

        }
        //public static List<PartnerCategory> PartnerCategoryBAL_SelectAll()
        //{
        //    IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
        //    return pRep.GetAll().ToList();

        //}

        public static IQueryable<PartnerCategory> PartnerCategoryBAL_SelectAll()
        {
            IPortfolioRepository<PartnerCategory> pRep = new PortfolioRepository<PartnerCategory>();
            return pRep.GetAll();

        }
    }
}
