using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin.webservices
{
    /// <summary>
    /// Summary description for ContactLeadsServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContactLeadsServices : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem AddContactLeads(string LeadID,string CreatedDate,string CustomerName,string Email,string Cell,string Address,string LeadDescription,string PriceQuoted,string ReminderDate)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.PortfolioContactLead();
                p.CreatedDate = Convert.ToDateTime(CreatedDate);
                p.CustomerName = CustomerName;
                p.Email = Email;
                p.Cell = Cell;
                p.Address = Address;
                p.LeadDescription = LeadDescription;
                if (!string.IsNullOrEmpty(PriceQuoted))
                    p.PriceQuoted = Convert.ToDouble(!string.IsNullOrEmpty(PriceQuoted) ? PriceQuoted : "0");

                if (!string.IsNullOrEmpty(ReminderDate))
                    p.ReminderDate = Convert.ToDateTime(ReminderDate);

                var r = PortfolioMgt.BAL.PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_Add(p);
                if (r != null)
                {
                    li.Text = r.CreatedDate.ToString();
                    li.Text = r.CustomerName.ToString();
                    li.Text = r.Email.ToString();
                    li.Text = r.Cell.ToString();
                    li.Text = r.Address.ToString();
                    li.Text = r.LeadDescription.ToString();
                    li.Text = r.PriceQuoted.ToString();
                    li.Text = r.ReminderDate.ToString();
                    li.Value = r.LeadID.ToString();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;
        }

        //jqury dataTable

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromData()
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_SelectByPortfolioID();

                var rlist = (from r in result.AsEnumerable()
                             select new
                             {
                                 LeadID = r.LeadID,                                
                                 CustomerName = r.CustomerName,
                                 Email = r.Email,
                                 CreatedDate = String.Format("{0:MM/dd/yyyy}", r.CreatedDate),                              
                                 Address = r.Address,
                                 Cell = r.Cell,
                                 LeadDescription = r.LeadDescription,
                                 PriceQuoted = string.Format("{0:F2}", r.PriceQuoted.HasValue ? r.PriceQuoted.Value : 0),
                                 ReminderDate = r.ReminderDate.HasValue ? r.ReminderDate.Value.ToShortDateString() : "",

                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        //Edit contact detalies 

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromDataByID(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_SelectAll().Where(o=>o.LeadID == Convert.ToInt32(id)).ToList();

                var rlist = (from r in result
                             orderby r.LeadID descending
                             select new
                             {
                                 LeadID = r.LeadID,
                                 CreatedDate= String.Format("{0:MM/dd/yyyy}", r.CreatedDate),
                                 CustomerName = r.CustomerName,
                                 Email = r.Email,
                                 Cell = r.Cell,
                                 Address = r.Address,
                                 LeadDescription = r.LeadDescription,
                                 PriceQuoted = string.Format("{0:F2}", r.PriceQuoted.HasValue?r.PriceQuoted.Value:0),
                                 ReminderDate = r.ReminderDate.HasValue?r.ReminderDate.Value.ToShortDateString():"",


                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

       
        //update contact
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem ContactUpdate(string LeadID, string CreatedDate, string CustomerName, string Email, string Cell, string Address,string LeadDescription, string PriceQuoted,string ReminderDate)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_Select(Convert.ToInt32(LeadID));
              

                s.CreatedDate = Convert.ToDateTime(CreatedDate);
                s.CustomerName = CustomerName;
                s.Email = Email;
                s.Cell = Cell;
                s.Address = Address;
                s.LeadDescription = LeadDescription;
                if (!string.IsNullOrEmpty(PriceQuoted))
                    s.PriceQuoted = Convert.ToDouble(!string.IsNullOrEmpty(PriceQuoted) ? PriceQuoted : "0");
               
                if (!string.IsNullOrEmpty( ReminderDate))
                s.ReminderDate = Convert.ToDateTime(ReminderDate);

               
                var r = PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_Update(s);
                if (r != null)
                {
                    li.Value = r.CreatedDate.ToString();
                    li.Text = r.CustomerName.ToString();
                    li.Text = r.Email.ToString();
                    li.Text = r.Cell.ToString();
                    li.Text = r.Address.ToString();
                    li.Text = r.LeadDescription.ToString();
                    li.Value = r.PriceQuoted.ToString();
                    li.Value = r.ReminderDate.ToString();
                    li.Value = r.LeadID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;

        }


        //FromDataDelete
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem FromDataDelete(string id)
        {
            ListItem li = new ListItem();
            try
            {
                if (id == "0")
                {

                }
                else
                {
                    //var flsCustomField = CustomFormDesignerBAL.GetFieldByID(Convert.ToInt32(id));
                    PortfolioContactLeadsBAL.PortfolioContactLeadsBAL_delete(Convert.ToInt32(id));
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;
        }

    }
}
