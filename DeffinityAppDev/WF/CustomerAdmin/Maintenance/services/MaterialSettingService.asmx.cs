using DC.BLL;
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
    /// Summary description for MaterialSettingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class MaterialSettingService : System.Web.Services.WebService
    {
        #region Meterial
       
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromData(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PartnerMaterialBAL.PartnerMaterialBAL_SelectByPortfolioID();

                var rlist = (from r in result
                             orderby r.ID descending
                             select new
                             {
                                 ID = r.ID,
                                 material = r.MaterialTitle,
                                 tcost = string.Format("{0:N2}", r.Cost),
                                 mark = r.Markup,
                                 pric = string.Format("{0:N2}", r.Price),
                              

                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem AddMeterial(string metrial, float tcost, float mark, float prices)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.PartnerMaterial();
                p.MaterialTitle = metrial;
                p.Cost = tcost;
                p.Markup = mark;
                p.Price = prices;

                var r = PortfolioMgt.BAL.PartnerMaterialBAL.PartnerMaterialBAL_Add(p);
                if (r != null)
                {
                    li.Text = r.MaterialTitle.ToString();
                    li.Text = string.Format("{0:N2}", r.Cost);
                    li.Text = r.Markup.ToString();
                    li.Text = string.Format("{0:N2}", r.Price);
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
        public object BindFromDataByID(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = PartnerMaterialBAL.PartnerMaterialBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(id)).ToList();

                var rlist = (from r in result
                             orderby r.ID descending
                             select new
                             {
                                 ID = r.ID,
                                 MaterialTitle = r.MaterialTitle,
                                 Cost = string.Format("{0:N2}", r.Cost),
                                 Markup = r.Markup,
                                 Price = string.Format("{0:N2}", r.Price),
                                 //MinimumValue = r.MinimumValue,
                                 //MaximumValue = r.MaximumValue,
                                 //Mandatory = r.Mandatory,
                                 //FormID = r.FormID,
                                 //FieldPosition = r.FieldPosition,
                                 //DefaultText = r.DefaultText,
                                 //CustomerID = r.CustomerID

                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        //edit form data

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem MeterialUpdate(string id,string metrial, float tcost, float mark, float prices)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioMgt.BAL.PartnerMaterialBAL.PartnerMaterialBAL_Select(Convert.ToInt32(id));
                s.MaterialTitle = metrial;
                s.Cost = tcost;
                s.Markup = mark;
                s.Price = prices;
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerMaterialBAL.PartnerMaterialBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.MaterialTitle.ToString();
                    li.Text = string.Format("{0:N2}", r.Cost);
                    li.Text = r.Markup.ToString();
                    li.Text = string.Format("{0:N2}", r.Price);
                    li.Value = r.ID.ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return li;

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
                    //var flsCustomField = CustomFormDesignerBAL.GetFieldByID(Convert.ToInt32(id));
                    PartnerMaterialBAL.PartnerMaterialBAL_delete(Convert.ToInt32(id));
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
