using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DC.Entity;

namespace DC.BLL
{
    public class QuoteTemplateBAL
    {
        public static QuotationTemplate AddUpdateQuoteTemplate(int ID, string Title, string htmlContent)
        {
            var qEntity = new QuotationTemplate();
            IDCRespository<QuotationTemplate> qtRep = new DCRepository<QuotationTemplate>();
            if (ID > 0)
            {
                qEntity = qtRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (qEntity != null)
                {
                    qEntity.Title = Title;
                    qtRep.Edit(qEntity);
                    GenerateQuoteHtml(qEntity.ID, htmlContent);
                }
            }
            else
            {
                qEntity = new QuotationTemplate();
                qEntity.Title = Title;
                qEntity.CustomerID = sessionKeys.PortfolioID;
                qEntity.LoggedBy = sessionKeys.UID;
                qEntity.LoggedDate = DateTime.Now;
                qtRep.Add(qEntity);
                GenerateQuoteHtml(qEntity.ID, htmlContent);
            }
            return qEntity;
        }
        //check title is already exists
        public static bool QuoteTemplateTitleExists(int ID, string Title)
        {
            bool retval = false;
            IDCRespository<QuotationTemplate> qtRep = new DCRepository<QuotationTemplate>();
            if (ID > 0)
            {
                if (qtRep.GetAll().Where(o => o.ID != ID).Where(o => o.CustomerID == sessionKeys.PortfolioID).Where(o => o.Title.ToLower() == Title.ToLower()).Count() > 0)
                    retval = true;
            }
            else
            {
                if (qtRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).Where(o => o.Title.ToLower() == Title.ToLower()).Count() > 0)
                    retval = true;
            }
            return retval;
        }
        public static QuotationTemplate QuoteTemplateSelect(int ID)
        {
            var qEntity = new QuotationTemplate();
            IDCRespository<QuotationTemplate> qtRep = new DCRepository<QuotationTemplate>();
            if (ID > 0)
            {
                qEntity = qtRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            }

            return qEntity;
        }
        public static List<QuotationTemplate> QuoteTemplateSelectAll()
        {
            List<QuotationTemplate> qEntity = new List<QuotationTemplate>();
            IDCRespository<QuotationTemplate> qtRep = new DCRepository<QuotationTemplate>();

            qEntity = qtRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o => o.Title).ToList();


            return qEntity;
        }
        public static string QuoteTemplateSelectHTML(int ID)
        {
            string htmlString = string.Empty;
            string fname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/QuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()));

            string fileID = string.Format("~/WF/UploadData/QuoteTemplate/{0}/Template.html", ID);
            if (File.Exists(fname))
            {
                Emailer em = new Emailer();
                htmlString = em.ReadFile(fileID);
            }
            return htmlString;
        }
        public static string JobQuoteTemplateSelectHTML(int ID)
        {
            string htmlString = string.Empty;
            string fname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()));

            string fileID = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.html", ID);
            if (File.Exists(fname))
            {
                Emailer em = new Emailer();
                htmlString = em.ReadFile(fileID);
            }
            return htmlString;
        }
        public static bool QuoteTemplateDelete(int ID)
        {
            bool retval = false;
            IDCRespository<QuotationTemplate> qtRep = new DCRepository<QuotationTemplate>();
            if (ID > 0)
            {
                var qEntity = qtRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (qEntity != null)
                {
                    qtRep.Delete(qEntity);
                    retval = true;
                }
            }
            return retval;
        }
        public static void GenerateQuoteHtml(int ID, string htmlContent)
        {
            try {
                //Send policy email
                string content = GenerateHTML(ID, htmlContent);
                string dname = HttpContext.Current.Server.MapPath("~/WF/UploadData/QuoteTemplate/" + ID);
                if (!Directory.Exists(dname))
                {
                    Directory.CreateDirectory(dname);
                }
                string fname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/QuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()));
                if (File.Exists(fname))
                {
                    try
                    {
                        File.Delete(fname);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.LogException("Delete file failed quote template :" + ID.ToString());
                    }
                }

                System.IO.File.WriteAllText(Path.Combine(dname, "Template.html"), content);


                string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/QuoteTemplate/{0}/Template.pdf", ID, Deffinity.systemdefaults.GetWebUrl()));
                if (File.Exists(pname))
                {
                    try
                    {
                        File.Delete(pname);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.LogException("Delete file failed quote template pdf:" + ID.ToString());
                    }
                }



                //Generate PDF
                PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/QuoteTemplate/" + ID, "Template", new string[] { string.Format(@"{1}/WF/UploadData/QuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()) }, new string[] { "--print-media-type" });
            }
            
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void GenerateJobQuoteHtml(int ID, string htmlContent)
        {
            try
            {
                //Send policy email
                string content = GenerateHTML(ID, htmlContent);
                string dname = HttpContext.Current.Server.MapPath("~/WF/UploadData/JobQuoteTemplate/" + ID);
                if (!Directory.Exists(dname))
                {
                    Directory.CreateDirectory(dname);
                }
                string fname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()));
                if (File.Exists(fname))
                {
                    try
                    {
                        File.Delete(fname);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.LogException("Delete file failed quote template :" + ID.ToString());
                    }
                }

                System.IO.File.WriteAllText(Path.Combine(dname, "Template.html"), content);

                string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", ID, Deffinity.systemdefaults.GetWebUrl()));
                if (File.Exists(pname))
                {
                    try
                    {
                        File.Delete(pname);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.LogException("Delete file failed job quote template pdf:" + ID.ToString());
                    }
                }

                //Generate PDF
                PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/JobQuoteTemplate/" + ID, "Template", new string[] { string.Format(@"{1}/WF/UploadData/JobQuoteTemplate/{0}/Template.html", ID, Deffinity.systemdefaults.GetWebUrl()) }, new string[] { "--print-media-type" });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static string GenerateHTML(int ID, string htmlContent)
        {
            string retval = string.Empty;
            try
            {
                retval = htmlContent;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        public static QuotationTemplatesAssignedToTicket QuotationTemplatesAssignedToTicket_AddUpdate(int callid, int templateID, string TemplateName)
        {
            IDCRespository<QuotationTemplatesAssignedToTicket> qtRep = new DCRepository<QuotationTemplatesAssignedToTicket>();
            var q = qtRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (q != null)
            {
                q.CurrentTemplateName = TemplateName;
                q.BaseTemplateID = templateID;
                qtRep.Edit(q);
            }
            else
            {
                q = new QuotationTemplatesAssignedToTicket();
                q.BaseTemplateID = templateID;
                q.CallID = callid;
                q.CurrentTemplateName = TemplateName;
                qtRep.Add(q);
            }
            return q;
        }
        public static QuotationTemplatesAssignedToTicket QuotationTemplatesAssignedToTicket_Select(int callid)
        {
            IDCRespository<QuotationTemplatesAssignedToTicket> qtRep = new DCRepository<QuotationTemplatesAssignedToTicket>();
            var q = qtRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            return q;
        }
    

        }
}
