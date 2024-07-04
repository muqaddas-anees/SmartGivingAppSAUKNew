using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;

using System.Web.Script.Serialization;
namespace DeffinityAppDev.WF.AutoResponder
{
    /// <summary>
    /// Summary description for CalenderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CalenderService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public object ts_add(string tname, string sdate, string edate)
        {
            //   public static void WorkflowTemplateConfig_Add(int TemplateID, int UserID, int ContactID, string ContactType, DateTime StartDate, DateTime EndDate, TimeSpan ScheduledTime, DateTime ScheduledDate)
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var tData = CampaignTemplateBAL.CampaignTemplate_SelectAll(sessionKeys.PortfolioID).Where(o => o.TemplateName.ToLower() == tname.ToLower()).FirstOrDefault();
                if (tData != null)
                {
                    //check already exists for the date
                    var b = CampaignTemplateBAL.CampaignTemplate_Schedule_IsExists(tData.ID, DateTime.Parse(sdate));
                    if (!b)
                    {
                        var c = new CampaignTemplate_Schedule();
                        c.IsMailSent = false;
                        c.LoggedBy = sessionKeys.UID;
                        c.LoggedDate = DateTime.Now;
                        DateTime EventEndDate = DateTime.Parse(edate).AddDays(1); //DateTime.Parse(edate.ToString("yyyy-MM-dd HH:mm:ss"));
                        c.ScheduledEndDate = EventEndDate;
                        DateTime EventStartDate = DateTime.Parse(sdate);
                        c.ScheduledStartDate = EventStartDate;
                        c.TemplateID = tData.ID;


                        CampaignTemplateBAL.CampaignTemplate_Schedule_Add(c);

                        var gList = CampaignTemplateBAL.V_CampaignTemplate_Schedule_SelectAll(sessionKeys.PortfolioID).Where(o => o.ID == c.ID).ToList();


                        var s = from r in gList
                                select new
                                {
                                    id = r.ID,
                                    title = r.TemplateName,
                                    start = string.Format("{0:s}", r.ScheduledStartDate),
                                    end = string.Format("{0:s}", r.ScheduledEndDate),
                                    color = (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(Convert.ToDateTime(r.ScheduledStartDate.Value.ToShortDateString()))).Days > 0 ? "gray" : "green"
                                };

                        return Jsonserializer.Serialize(s).ToString();
                    }
                    else
                    {
                        return Jsonserializer.Serialize("Template already exists").ToString();
                    }
                }
                else
                {
                    return Jsonserializer.Serialize(string.Empty).ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }

        }


        [WebMethod(EnableSession = true)]
        //  public object WorkflowTemplateConfig_Update(int ID, string EndtDate, string Stime, string StartDate, string Etime)
        public object WorkflowTemplateConfig_UpdateTime(int ID, string StartDate)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                return Jsonserializer.Serialize(string.Empty).ToString();
                //using (DataClasses1DataContext dc = new DataClasses1DataContext())
                //{
                //    WorkflowTemplateConfig wtc = new WorkflowTemplateConfig();
                //    MailTemplatesTbl Mtl = new MailTemplatesTbl();
                //    WorkflowTemplateConfig UpdateEvent = (from a in dc.WorkflowTemplateConfigs where a.ID == ID select a).FirstOrDefault();

                //    DateTime sdt = DateTime.Parse(StartDate);
                //    DateTime EventStartDate = DateTime.Parse(sdt.ToString("yyyy-MM-dd HH:mm:ss"));
                //    //DateTime edt = DateTime.Parse(EndtDate);
                //    //DateTime EventEndDate = DateTime.Parse(edt.ToString("yyyy-MM-dd HH:mm:ss"));

                //    //UpdateEvent.EndDate = EventEndDate;
                //    //add one hour to end date
                //    UpdateEvent.EndDate = EventStartDate.AddHours(1);
                //    UpdateEvent.ScheduledTime = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                //    UpdateEvent.StartDate = EventStartDate;
                //    UpdateEvent.ScheduledDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                //    dc.SubmitChanges();

                //    var EventData = (from a in dc.WorkflowTemplateConfigs where a.ID == ID select a).ToList();
                //    var MailTempalteList = dc.MailTemplatesTbls.ToList();

                //    var Result = from e in EventData
                //                 join
                //                 m in MailTempalteList on e.TemplateID equals m.TemplateID
                //                 select new
                //                 {
                //                     Id = e.ID,
                //                     Title = m.TemplateName,
                //                     Sdate = e.StartDate,
                //                     Edate = e.EndDate
                //                 };
                //    var s = from r in Result
                //            select new
                //            {
                //                id = r.Id,
                //                title = r.Title,
                //                start = string.Format("{0:s}", r.Sdate),
                //                end = string.Format("{0:s}", r.Edate),
                //                color = (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(Convert.ToDateTime(r.Sdate.Value.ToShortDateString()))).Days > 0 ? "gray" : "green"

                //            };
                //    return Jsonserializer.Serialize(s).ToString();
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }

        }


        [WebMethod(EnableSession = true)]
      //  public object WorkflowTemplateConfig_Update(int ID, string EndtDate, string Stime, string StartDate, string Etime)
         public object ts_update(string id,string sdate, string edate)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var c = CampaignTemplateBAL.CampaignTemplate_Schedule_SelectByID(Convert.ToInt32(id));
                if (c != null)
                {

                    c.IsMailSent = false;
                    c.LoggedBy = sessionKeys.UID;
                    c.LoggedDate = DateTime.Now;
                    DateTime EventEndDate = DateTime.Parse(sdate); //DateTime.Parse(edate.ToString("yyyy-MM-dd HH:mm:ss"));
                    DateTime EventStartDate = DateTime.Parse(edate);
                    c.ScheduledStartDate = EventEndDate ;
                    c.ScheduledEndDate = EventStartDate;


                    CampaignTemplateBAL.CampaignTemplate_Schedule_Update(c);

                    var gList = CampaignTemplateBAL.CampaignTemplate_SelectAll(sessionKeys.PortfolioID);


                    var s = from r in gList
                            select new
                            {
                                id = r.ID,
                                title = r.TemplateName,
                                start = string.Format("{0:s}", r.ScheduledStartDate),
                                end = string.Format("{0:s}", r.ScheduledEndDate),
                                color = (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(Convert.ToDateTime(r.ScheduledStartDate.Value.ToShortDateString()))).Days > 0 ? "gray" : "green"
                            };

                    return Jsonserializer.Serialize(s).ToString();
                }
                else
                {
                    return Jsonserializer.Serialize(string.Empty).ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }

        }

        [WebMethod(EnableSession = true)]
        public void UpdateData(string startTime, string title, string endTime, int id)
        {
            try
            {

                //using (DataClasses1DataContext dc = new DataClasses1DataContext())               
                //{
                //    DateTime Startdate;
                //    DateTime Enddate;
                //    var t = startTime.Replace('T', ' ');
                //    var k = endTime.Replace('T', ' ');
                //    Startdate = DateTime.Parse(t);
                //    Enddate = DateTime.Parse(k);
                //    WorkflowTemplateConfig wtc = new WorkflowTemplateConfig();
                //    WorkflowTemplateConfig EventData = (from a in dc.WorkflowTemplateConfigs where a.ID == id select a).FirstOrDefault();
                //    if (EventData != null)
                //    {
                //       EventData.StartDate=Startdate;
                //       EventData.EndDate = Enddate;
                //       EventData.ScheduledTime = TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                //       EventData.ScheduledDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                //       dc.SubmitChanges();
                //    }

                  
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        [WebMethod(EnableSession = true)]
        public object GetAllEvents()
        {

            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var gList = CampaignTemplateBAL.V_CampaignTemplate_Schedule_SelectAll(sessionKeys.PortfolioID);


                var s = from r in gList
                        select new
                        {
                            id = r.ID,
                            title = r.TemplateName,
                            start = string.Format("{0:s}", r.ScheduledStartDate),
                            end = string.Format("{0:s}", r.ScheduledEndDate)//,
                            //color = (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(Convert.ToDateTime(r.ScheduledStartDate.Value.ToShortDateString()))).Days > 0 ? "gray" : "green"
                        };

                return Jsonserializer.Serialize(s).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
            //try
            //   {
            //     //using (DataClasses1DataContext dc = new DataClasses1DataContext())
            //     //{
            //     //    WorkflowTemplateConfig wtc = new WorkflowTemplateConfig();
            //     //    MailTemplatesTbl Mtl = new MailTemplatesTbl();
            //     //    //  var Exrecord = (from a in dc.BlogCategories where a.Name == CName select a).FirstOrDefault();
            //     //    var EventList = dc.WorkflowTemplateConfigs.ToList();
            //     //    var MailTempalteList = dc.MailTemplatesTbls.ToList();

            //     //    var Result = from e in EventList
            //     //                 join
            //     //                 m in MailTempalteList on e.TemplateID equals m.TemplateID
            //     //                 where e.ScheduledBy == sessionKeys.UID
            //     //                 select new
            //     //                 {
            //     //                     Id = e.ID,
            //     //                     Title = m.TemplateName,
            //     //                     Sdate = e.StartDate,
            //     //                     Edate = e.EndDate
            //     //                 };
            //     //    var s = from r in Result
            //     //            select new
            //     //            {
            //     //                id = r.Id,
            //     //                title = r.Title,
            //     //                start = string.Format("{0:s}", r.Sdate),
            //     //                end = string.Format("{0:s}", r.Edate),
            //     //                color = (Convert.ToDateTime(DateTime.Now.ToShortDateString()).Subtract(Convert.ToDateTime(r.Sdate.Value.ToShortDateString()))).Days > 0 ? "gray" : "green"
            //     //            };                  


            //     //    return Jsonserializer.Serialize(s).ToString();
            //     //}
            //     return Jsonserializer.Serialize(string.Empty).ToString();
            // }
            // catch (Exception ex)
            // {
            //     LogExceptions.WriteExceptionLog(ex);
            //     return Jsonserializer.Serialize(string.Empty).ToString();
            // }
        }

        [WebMethod(EnableSession = true)]
        public void DeleteEvent(int id)
        {
            try
            {
                if (id != 0)
                {
                    //using (DataClasses1DataContext dc = new DataClasses1DataContext())
                    //{
                    //    WorkflowTemplateConfig wtc = new WorkflowTemplateConfig();
                    //    var EventData = (from a in dc.WorkflowTemplateConfigs where a.ID == id select a).FirstOrDefault();
                    //    if (EventData != null)
                    //    {
                    //        dc.WorkflowTemplateConfigs.DeleteOnSubmit(EventData);
                    //        dc.SubmitChanges();
                    //    }
                    //}
                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public object GetAllTemplates()
        {

            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<string> tList = new List<string>();
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var rlist = cRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

                if(rlist.Count >0)
                {
                    tList = rlist.Select(o => o.TemplateName).ToList();
                }

                return Jsonserializer.Serialize(tList).ToString();
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
    }
}
