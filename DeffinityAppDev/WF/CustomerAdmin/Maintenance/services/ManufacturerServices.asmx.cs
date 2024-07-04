using DC.BLL;
using DC.Entity;
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
    /// Summary description for WebServiceAdmin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
   
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceAdmin : System.Web.Services.WebService
    {
        #region Category
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> CategoryGet(string typeid)
        {
            var rlist = PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_SelectByPartnerID().ToList();

            if (sessionKeys.PortfolioID > 0)
                rlist = rlist.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

            var result = (from p in rlist                         
                          orderby p.Name
                          select new ListItem { Value = p.id.ToString(), Text = p.Name }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem CategoryAdd(string typeid, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.Manufacturer();
                p.Name = name;
              //  p.IsDeleted = false;
                p.PartnerID = Convert.ToInt32(typeid);

                var r = PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_Add(p);
                if (r != null)
                {
                    li.Text = r.Name;
                    li.Value = r.id.ToString();
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
        public ListItem CategoryUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {

                var p = PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_Select(Convert.ToInt32(id));
                p.Name = name;

                var r = PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_Update(p);
                if (r != null)
                {
                    li.Text = r.Name;
                    li.Value = r.id.ToString();
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
        public bool CategoryDelete(string id1)
        {
            bool retval = false;
            try
            {
                //var p = PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_Select(Convert.ToInt32(id1));
                //p.id = id1;
                PortfolioMgt.BAL.ManufacturerBAL.ManufacturerBAL_delete(Convert.ToInt32(id1));

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

