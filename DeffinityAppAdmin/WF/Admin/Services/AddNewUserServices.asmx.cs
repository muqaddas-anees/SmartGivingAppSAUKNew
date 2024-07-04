using DeffinityManager.PortfolioMgt.BAL;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
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
    /// Summary description for AddNewUserServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AddNewUserServices : System.Web.Services.WebService
    {
        #region User
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> GetPateners(string typeid)
        {
            var rlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll();
            var result = (from p in rlist
                          orderby p.PartnerName
                          select new ListItem { Value = p.ID.ToString(), Text = p.PartnerName.ToString() }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem AddUser(string DisplayName, string Username, string Password,int PartnerID)
        {
            ListItem li = new ListItem();
            try
            {
                var r = new PortfolioMgt.Entity.PortfolioTrackerLogin();               
                r.DisplayName = DisplayName;
                r.Username = Username;
                 r.Password= Password;
                 r.BaseURL = "us.123smartpro.com";
                r.IsActive = true;
                r.PartnerID = PartnerID;

                var p = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_AddNew(r);
                if (r != null)
                {
                    li.Text = r.DisplayName.ToString();
                    li.Text = r.Username.ToString();
                   // li.Text = Deffinity.Users.Login.GeneratePasswordString(r.Password.ToString());
                    li.Text = r.Password.ToString();
                    li.Text = r.BaseURL.ToString();
                    li.Value = r.PartnerID.ToString();
                    li.Value = r.ID.ToString();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;
        }

        //edit form data

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem UserUpdate(string id, string DisplayName, string Username,string Password,string PartnerName, int PartnerID)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o=>o.ID == (Convert.ToInt32(id))).FirstOrDefault();
                var e = PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32(id));

                s.DisplayName = DisplayName;
                s.Username = Username;
                s.Password = Password;
                s.PartnerID = PartnerID;
               // e.PartnerName = PartnerName;

                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_UpdateNew(s);
                var q = PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32(id));
                if (r != null & q!=null)
                {
                    li.Text = r.DisplayName.ToString();
                    li.Text = r.Username.ToString();
                    li.Text = r.Password.ToString();
                    li.Value = r.PartnerID.ToString();
                  //  li.Value = q.PartnerName.ToString();
                    li.Value = r.ID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;

        }

        //get data for edit data table

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromDataByID(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).ToList();

                var resultList = PartnerDetailBAL.PartnerDetailBAL_SelectAll().ToList();


                var rlist = (from e in result
                             join ex in resultList on e.PartnerID equals ex.ID
                             select new
                             {
                                 ID = e.ID,
                                 DisplayName = e.DisplayName,                                 
                                 Username = e.Username,
                                 Password = e.Password,
                                 PartnerName = ex.PartnerName,
                                 partnerID = e.PartnerID
                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        //bind data in table
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromData()
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_SelectAll().ToList();
                var resultList = PartnerDetailBAL.PartnerDetailBAL_SelectAll().ToList();


                //var rlist = (from e in result.AsEnumerable()
                //             from ex in resultList.Where(x => x.ID != null && e.PartnerID == x.ID|e.PartnerID==0).DefaultIfEmpty()
                //             select new 
                //             {
                //                 ID = e.ID,
                //                 DisplayName = e.DisplayName,
                //                 Username = e.Username,
                //                 PartnerName = ex.PartnerName,

                //             }).ToList();
                var rlist = (from e in result
                             join ex in resultList on e.PartnerID equals ex.ID
                             orderby e.ID descending
                             select new
                             {
                                 ID = e.ID,
                                 DisplayName = e.DisplayName,
                                 Username = e.Username,
                                 PartnerName = ex.PartnerName,

                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }
        //delete form data

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
                    PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(Convert.ToInt32(id));
                    //var flsCustomField = CustomFormDesignerBAL.GetFieldByID(Convert.ToInt32(id));
                    //  PortfolioTrackerLoginBAL.del(Convert.ToInt32(id));
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;
        }



        #endregion
    }
}
