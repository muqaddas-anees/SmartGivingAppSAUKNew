using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace PortfolioMgt.BAL
{
    public class CampaignTemplateBAL
    {
        public static List<CampaignTemplate> CampaignTemplate_SelectAll(int PortfolioID)
        {
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            var mlist = mRep.GetAll().Where(o => o.PortfolioID == PortfolioID).ToList();
            return mlist.ToList();
        }
        public static CampaignTemplate CampaignTemplate_SelectByID(int TemplateID)
        {
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            return mRep.GetAll().Where(o => o.ID == TemplateID).FirstOrDefault();
        }
        public static bool CampaignTemplate_IsExists(int PortfolioID,string TemplateName,int TemplateID=0)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            if (TemplateID == 0)
            {
                var mlist = mRep.GetAll().Where(o => o.PortfolioID == PortfolioID && o.TemplateName.ToLower() == o.TemplateName.ToLower()).Count();
                if (mlist > 0)
                    retval = true;
            }
            else
            {
                var mlist = mRep.GetAll().Where(o => o.PortfolioID == PortfolioID && o.TemplateName.ToLower() == o.TemplateName.ToLower() && o.ID != TemplateID).Count();
                if (mlist > 0)
                    retval = true;
            }
            return retval;
        }

        public static CampaignTemplate CampaignTemplate_Add(CampaignTemplate c)
        {
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            mRep.Add( c);

            return c;
        }

        public static CampaignTemplate CampaignTemplate_Update(CampaignTemplate c)
        {
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();

            var m = mRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if(m != null)
            {
                m.ModifiedBy = c.ModifiedBy;
                m.ModifiedDate = c.ModifiedDate;
                m.ScheduledEndDate = c.ScheduledEndDate;
                m.ScheduledEndDate = c.ScheduledEndDate;
                m.Subject = c.Subject;
                m.Tags = c.Tags;
                m.TemplateContent = c.TemplateContent;
                m.TemplateName = c.TemplateName;
                
                mRep.Edit(m);
            }


            return m;
        }

        public static bool CampaignTemplate_DeleteID(int TemplateID)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            var m =  mRep.GetAll().Where(o => o.ID == TemplateID).FirstOrDefault();
            if(m != null)
            {
                mRep.Delete(m);
                retval = true;
            }
            return retval;
        }

        public static List<V_CampaignTemplate_Schedule> V_CampaignTemplate_Schedule_SelectAll()
        {
            IPortfolioRepository<V_CampaignTemplate_Schedule> mRep = new PortfolioRepository<V_CampaignTemplate_Schedule>();
            var mlist = mRep.GetAll().ToList();
            return mlist.ToList();
        }
        public static List<V_CampaignTemplate_Schedule> V_CampaignTemplate_Schedule_SelectByCurrentDate(DateTime currentdate)
        {
            IPortfolioRepository<V_CampaignTemplate_Schedule> mRep = new PortfolioRepository<V_CampaignTemplate_Schedule>();
            var mlist = (from m in mRep.GetAll()
                         where (((m.ScheduledStartDate <= currentdate)
                         && (m.ScheduledEndDate > currentdate)))
                         select m
                         ).ToList();

            return mlist.ToList();
        }
        public static List<V_CampaignTemplate_Schedule> V_CampaignTemplate_Schedule_SelectAll(int PortfolioID)
        {
            IPortfolioRepository<V_CampaignTemplate_Schedule> mRep = new PortfolioRepository<V_CampaignTemplate_Schedule>();
            var mlist = mRep.GetAll().Where(o => o.PortfolioID == PortfolioID).ToList();
            return mlist.ToList();
        }

        public static CampaignTemplate_Schedule CampaignTemplate_Schedule_SelectByID(int ID)
        {
            IPortfolioRepository<CampaignTemplate_Schedule> mRep = new PortfolioRepository<CampaignTemplate_Schedule>();
            return mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }
       

        public static CampaignTemplate_Schedule CampaignTemplate_Schedule_Add(CampaignTemplate_Schedule c)
        {
            IPortfolioRepository<CampaignTemplate_Schedule> mRep = new PortfolioRepository<CampaignTemplate_Schedule>();
            mRep.Add(c);
            return c;
        }

        public static CampaignTemplate_Schedule CampaignTemplate_Schedule_Update(CampaignTemplate_Schedule c)
        {
            IPortfolioRepository<CampaignTemplate_Schedule> mRep = new PortfolioRepository<CampaignTemplate_Schedule>();

            var m = mRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if (m != null)
            {
                m.IsMailSent = c.IsMailSent;
                m.LoggedBy = c.LoggedBy;
                m.LoggedDate = c.LoggedDate;
                m.MailSentOn = c.MailSentOn;
                m.ScheduledEndDate = c.ScheduledEndDate;
                m.ScheduledStartDate = c.ScheduledStartDate;
                m.TemplateID = c.TemplateID;               

                mRep.Edit(m);
            }


            return m;
        }
        public static bool CampaignTemplate_Schedule_Delete(int ID)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate> mRep = new PortfolioRepository<CampaignTemplate>();
            var m = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (m != null)
            {
                mRep.Delete(m);
                retval = true;
            }
            return retval;

        }
        public static bool CampaignTemplate_Schedule_DeleteByTemplateID(int TemplateID)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate_Schedule> mRep = new PortfolioRepository<CampaignTemplate_Schedule>();
            var m = mRep.GetAll().Where(o => o.TemplateID == TemplateID).FirstOrDefault();
            if (m != null)
            {
                mRep.Delete(m);
                retval = true;
            }
            return retval;
        }

        public static bool CampaignTemplate_Schedule_IsExists(int TemplateID,DateTime currentdate)
        {
            bool retval = false;
           
            IPortfolioRepository<V_CampaignTemplate_Schedule> mRep = new PortfolioRepository<V_CampaignTemplate_Schedule>();
            //schedule data
            // var tEntity = mRep.GetAll().Where(o => o.ID == ScheduleID).FirstOrDefault();

            currentdate = Convert.ToDateTime(currentdate.ToShortDateString());
            var mEntity = mRep.GetAll().Where(m => m.TemplateID == TemplateID &&(( m.ScheduledStartDate <= currentdate)
                         && ( m.ScheduledEndDate > currentdate))
                         ).FirstOrDefault();
            //var mEntity = mRep.GetAll().Where(m => m.TemplateID == TemplateID && 
            //(m.StartDateYear >= currentdate.Year && m.StartDateMonth >= currentdate.Month && m.StartDateDate >= currentdate.Day)
            //             && (m.EndDateYear <= currentdate.Year && m.EndDateMonth <= currentdate.Month && m.EndDateDate <= currentdate.Day)
            //             ).FirstOrDefault();
            if (mEntity != null)
            {
               
                retval = true;
            }
            return retval;
        }



        public static List<V_CampaignTemplate_ScheduleHistory> V_CampaignTemplate_ScheduleHistory_SelectAll(int TemplateID)
        {
            IPortfolioRepository<V_CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<V_CampaignTemplate_ScheduleHistory>();
            var mlist = mRep.GetAll().Where(o => o.TemplateID == TemplateID).ToList();
            return mlist.ToList();
        }
        public static CampaignTemplate_ScheduleHistory CampaignTemplate_ScheduleHistory_SelectByID(int ID)
        {
            IPortfolioRepository<CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<CampaignTemplate_ScheduleHistory>();
            return mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        }

        public static bool CampaignTemplate_ScheduleHistory_IsExists(int contactID,int templateid,DateTime currentdate)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<CampaignTemplate_ScheduleHistory>();
            var rec = mRep.GetAll().Where(p=>p.TemplateID == templateid ).Where(m => m.ContactID == contactID && ((m.ScheduledStartDate <= currentdate)
                         && (m.ScheduledEndDate > currentdate))).FirstOrDefault();
            if (rec != null)
                retval = true;

            return retval;
        }

        public static CampaignTemplate_ScheduleHistory CampaignTemplate_ScheduleHistory_Add(CampaignTemplate_ScheduleHistory c)
        {
            IPortfolioRepository<CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<CampaignTemplate_ScheduleHistory>();
            mRep.Add(c);
            return c;
        }

        public static CampaignTemplate_ScheduleHistory CampaignTemplate_ScheduleHistory_Update(CampaignTemplate_ScheduleHistory c)
        {
            IPortfolioRepository<CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<CampaignTemplate_ScheduleHistory>();

            var m = mRep.GetAll().Where(o => o.ID == c.ID).FirstOrDefault();
            if (m != null)
            {
                m.IsMailSent = c.IsMailSent;
                m.ScheduledEndDate = c.ScheduledEndDate;
                m.ScheduledStartDate = c.ScheduledStartDate;
                m.MailSentOn = c.MailSentOn;               
                m.TemplateID = c.TemplateID;
                m.ContactID = c.ContactID;
                m.TemplateID = c.TemplateID;
                mRep.Edit(m);
            }


            return m;
        }
        public static bool CampaignTemplate_ScheduleHistory_Delete(int ID)
        {
            bool retval = false;
            IPortfolioRepository<CampaignTemplate_ScheduleHistory> mRep = new PortfolioRepository<CampaignTemplate_ScheduleHistory>();
            var m = mRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            if (m != null)
            {
                mRep.Delete(m);
                retval = true;
            }
            return retval;

        }

        public static List<UserMgt.Entity.v_contractor> PorfolioContact_SelectByTag(string tags, int portfolioid)
        {
            IUserRepository<UserMgt.Entity.v_contractor> pc = new UserRepository<UserMgt.Entity.v_contractor>();
            List<UserMgt.Entity.v_contractor> retList = new List<UserMgt.Entity.v_contractor>();
            foreach (var s in tags.Split(','))
            {
                if (s.Trim().Length > 0)
                {
                    var rContacts = pc.GetAll().Where(o => o.Tags.ToLower().Contains(s.ToLower().Trim())).Where(o => o.CompanyID == portfolioid).ToList();
                    foreach (var r in rContacts)
                    {
                        if (retList.Where(o => o.EmailAddress == r.EmailAddress).FirstOrDefault() == null)
                        {
                            retList.Add(r);
                        }

                    }
                }
            }

            return retList;
        }

        //update all donors 
        public static List<UserMgt.Entity.v_contractor> PorfolioContact_UpdateAllDonors(string tags, int portfolioid)
        {
            IUserRepository<UserMgt.Entity.v_contractor> pc = new UserRepository<UserMgt.Entity.v_contractor>();
            List<UserMgt.Entity.v_contractor> retList = new List<UserMgt.Entity.v_contractor>();

            var rContacts = pc.GetAll().Where(o => o.Tags.ToLower().Contains("all donors".ToLower().Trim()) && o.CompanyID == portfolioid && o.SID != 1).ToList();

            var rContacts_All = pc.GetAll().Where(o => !o.Tags.ToLower().Contains("all donors".ToLower().Trim()) && o.CompanyID == portfolioid && o.SID != 1).ToList();

           // if (rc)

            //foreach (var s in tags.Split(','))
            //{
            //    var rContacts = pc.GetAll().Where(o => o.Tags.ToLower().Contains(s.ToLower().Trim()) && o.CompanyID == portfolioid).ToList();
            //    foreach (var r in rContacts)
            //    {
            //        if (retList.Where(o => o.ID == r.ID).FirstOrDefault() == null)
            //        {
            //            retList.Add(r);
            //        }

            //    }
            //}

            return retList;
        }
    }
}
