using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
   public class ActivityDetailsBAL
    {
        public static PortfolioMgt.Entity.ActivityDetail ActivityDetailsBAL_Add(PortfolioMgt.Entity.ActivityDetail cat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

            cat.CreatedDate = DateTime.Now;
            cat.ModifiedDate = DateTime.Now;

            pRep.Add(cat);
            return cat;
        }

        public static PortfolioMgt.Entity.ActivityDetail ActivityDetailsBAL_Update(PortfolioMgt.Entity.ActivityDetail mat)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            var s = pRep.GetAll().Where(o => o.ID == mat.ID).FirstOrDefault();
            if (s != null)
            {
                s.ActivityCategoryID = mat.ActivityCategoryID;
                s.ActivitySubCategoryID = mat.ActivitySubCategoryID;
                s.Description = mat.Description;
                s.EndDateTime = mat.EndDateTime;
                s.ImageID = mat.ImageID;
                s.IsActive = mat.IsActive;
                s.ModifiedDate = DateTime.Now;
                s.Notes = mat.Notes;
                s.OrganizationID = mat.OrganizationID;
                s.StartDateTime = mat.StartDateTime;
                s.SupplierID = mat.SupplierID;
                s.Title = mat.Title;
                s.Slots = mat.Slots;
                s.Price = mat.Price;
                s.Address1 = mat.Address1;
                s.Address2 = mat.Address2;
                s.City = mat.City;
                s.Country = mat.Country;
                s.Postalcode = mat.Postalcode;
                s.Venue_Name = mat.Venue_Name;
                s.Event_Capacity = mat.Event_Capacity;
                s.Event_Cost = mat.Event_Cost;
                s.Event_Type = mat.Event_Type;
                s.Publisher = mat.Publisher;
                s.QRcode = mat.QRcode;
                s.state_Province = mat.state_Province;
                s.VenueLocation_Link = mat.VenueLocation_Link;
                s.Venue_Type = mat.Venue_Type;
                s.unid = mat.unid;
                s.QRcode = mat.QRcode;
                s.BookingEndDate = mat.BookingEndDate;
                s.BookingStartDate = mat.BookingStartDate;
                s.PublishDate = mat.PublishDate;
                s.IsPublicEvent = mat.IsPublicEvent;
                s.KeyWords = mat.KeyWords;
                s.SocialDescription = mat.SocialDescription;
                s.RefundPolicy = mat.RefundPolicy;
              
            }
            pRep.Edit(s);
            return s;
        }

        public static bool ActivityDetailsBAL_delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            var retEntity = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (retEntity != null)
            {
                pRep.Delete(retEntity);
                return true;
            }
            return retval;

        }
        public static PortfolioMgt.Entity.ActivityDetail ActivityDetailsBAL_SelectByID(int activityid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            return pRep.GetAll().Where(o=>o.ID == activityid).FirstOrDefault();

        }

        public static PortfolioMgt.Entity.ActivityDetail ActivityDetailsBAL_SelectByUNID(string unid)
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            return pRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();

        }
        public static IQueryable<PortfolioMgt.Entity.ActivityDetail> ActivityDetailsBAL_SelectAllNew()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();
            return pRep.GetAll();

        }
        public static IQueryable<PortfolioMgt.Entity.V_ActivityDetail> ActivityDetailsBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail>();
            return pRep.GetAll();

           

        }
    }
}
