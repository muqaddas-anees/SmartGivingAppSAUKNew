using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin.webservices
{
    /// <summary>
    /// Summary description for TimesPerYearSettingsServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
   
    public class TimesPerYearSettingsServices : System.Web.Services.WebService
    {
        #region Category

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> TimeYearGet(string typeid)
        {
            var rlist = PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_SelectByPartnerID();
            var result = (from p in rlist
                          orderby p.TimePerYear
                          select new ListItem { Value = p.ID.ToString(), Text = p.TimePerYear.ToString() }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem TimeYearAdd(string typeid, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.PartnerTimesPerYear();
                p.TimePerYear = Convert.ToInt32(name);

                p.PartnerID = Convert.ToInt32(typeid);

                var r = PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_Add(p);
                if (r != null)
                {
                    li.Text = r.TimePerYear.ToString();
                    li.Value = r.ID.ToString();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem TimeYearUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {

                var p = PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_Select(Convert.ToInt32(id));
                p.TimePerYear = Convert.ToInt32(name);

                var r = PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_Update(p);
                if (r != null)
                {
                    li.Text = r.TimePerYear.ToString();
                    li.Value = r.ID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;

        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public bool TimeYearDelete(string id)
        {
            //bool retval = false;
            //try
            //{
            //    var p = PortfolioMgt.BAL.PatnerTravelBAL.PartnerTravelTimeBAL_Select(Convert.ToInt32(id));
            //    p.ID = Convert.ToInt32(id);
            //    PortfolioMgt.BAL.PatnerTravelBAL.patnerTravelBAL_Update(p);

            //    retval = true;

            //}
            //catch (Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
            //return retval;
            bool retval = false;
            try
            {
                if (PortfolioMgt.BAL.PartnerTimesPerYearBAL.ManufacturerBAL_delete(Convert.ToInt32(id)))
                {


                    var p = PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_Select(Convert.ToInt32(id));
                    PortfolioMgt.BAL.PartnerTimesPerYearBAL.PartnerTimesPerYearBAL_Update(p);
                }
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion
        
    }
}
