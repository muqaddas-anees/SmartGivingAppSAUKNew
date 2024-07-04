using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using PortfolioMgt.BLL;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance.services
{
    /// <summary>
    /// Summary description for ChecklistServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class ChecklistServices : System.Web.Services.WebService
    {

        #region Section
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> SectionsGet()
        {
            var rlist = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
            var result = (from p in rlist
                          orderby p.Portfoliotype1
                          select new ListItem { Value = p.ID.ToString(), Text = p.Portfoliotype1 }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem SectionsAdd(string name)
        {
            ListItem li = new ListItem();
            try
            {
                var r = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Add(name);
                if (r != null)
                {
                    li.Text = r.Portfoliotype1;
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
        public ListItem SectionsUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {
                var r = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Update(name, Convert.ToInt32(id));
                if (r != null)
                {
                    li.Text = r.Portfoliotype1;
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
        public bool SectionsDelete(string id)
        {
            bool retval = false;
            try
            {
                if (PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Delete(Convert.ToInt32(id)))
                    retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion

        #region Category
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> CategoryGet(string typeid, string section)
        {
            var rlist = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_SelectByPartnerID(sessionKeys.PartnerID, section);
            if (sessionKeys.PortfolioID > 0)
                rlist = rlist.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
            var result = (from p in rlist
                          where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.CategoryName

                          select new ListItem { Value = p.ID.ToString(), Text = p.CategoryName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem CategoryAdd(string typeid, string name, string section)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.PartnerCategory();
                p.CategoryName = name;
                p.Section = section;
                p.IsDeleted = false;
                p.PartnerID = Convert.ToInt32(typeid);
               

                var r = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Add(p);
                if (r != null)
                {
                    li.Text = r.CategoryName;
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
        public ListItem CategoryUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {

                var p = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Select(Convert.ToInt32(id));
                p.CategoryName = name;

                var r = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Update(p);
                if (r != null)
                {
                    li.Text = r.CategoryName;
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
        public bool CategoryDelete(string id)
        {
            bool retval = false;
            try
            {
                var p = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Select(Convert.ToInt32(id));
                p.IsDeleted = true;
                PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Update(p);

                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion

        #region Sub Category
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> SubCategoryGet(string category)
        {
            var rlist = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_SelectByCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                          where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.SubCategoryName
                          select new ListItem { Value = p.ID.ToString(), Text = p.SubCategoryName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem SubCategoryAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var s = new PortfolioMgt.Entity.PartnerSubCategory();
                s.SubCategoryName = name;
                s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);
                s.IsDeleted = false;

                var r = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Add(s);
                if (r != null)
                {
                    li.Text = r.SubCategoryName;
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
        public ListItem SubCategoryUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Select(Convert.ToInt32(id));
                s.SubCategoryName = name;
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.SubCategoryName;
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
        public bool SubCategoryDelete(string id)
        {
            bool retval = false;
            try
            {
                var s = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Select(Convert.ToInt32(id));
                s.IsDeleted = true;
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Update(s);
                //DC.BLL.SubCategoryBAL.DeleteByID(Convert.ToInt32(id));
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion

        #region Service

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object ServiceGetByID(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            var serviceEnity = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
            return Jsonserializer.Serialize(serviceEnity).ToString();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> ServiceGet(string category)
        {
            var rlist = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                          where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.ServiceName
                          select new ListItem { Value = p.ID.ToString(), Text = p.ServiceName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object ServiceGetDetails(string serviceid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            var rlist = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(serviceid == null ? "0" : serviceid));

            if (rlist != null)

                return Jsonserializer.Serialize(rlist).ToString();
            else
                return Jsonserializer.Serialize(string.Empty).ToString();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem ServiceAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var s = new PortfolioMgt.Entity.PartnerService();
                s.ServiceName = name;
                s.PartnerSubCategoryID = Convert.ToInt32(category == null ? "0" : category);
                s.IsDeleted = false;

                var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Add(s);
                if (r != null)
                {
                    li.Text = r.ServiceName;
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
        public ListItem ServiceUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                s.ServiceName = name;
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.ServiceName;
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
        public ListItem ServiceUpdateTime(string id, string time)
        {
            ListItem li = new ListItem();
            try
            {
                var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                s.TimeInMinutes = Convert.ToInt32(time);
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.ServiceName;
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
        public bool ServiceDelete(string id)
        {
            bool retval = false;
            try
            {
                var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                s.IsDeleted = true;
                //s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                //DC.BLL.SubCategoryBAL.DeleteByID(Convert.ToInt32(id));
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem FromDataAdd(string id, string subid, string controltype, string cname, string cvalues)
        {
            ListItem li = new ListItem();
            try
            {
                if (id == "0")
                {
                    if (cname.Length > 0)
                    {
                        FLSCustomField flsCustomField = new FLSCustomField();
                        flsCustomField.PartnerSubcategoryID = 0;
                        flsCustomField.PartnerServiceID = Convert.ToInt32(subid);
                        flsCustomField.TypeOfField = controltype;
                        flsCustomField.LabelName = cname;
                        flsCustomField.DefaultText = string.Empty;
                        flsCustomField.MinimumValue = "";
                        flsCustomField.MaximumValue = "";
                        flsCustomField.Mandatory = false;
                        flsCustomField.FieldPosition = "";
                        flsCustomField.ListValue = cvalues;
                        //flsCustomField.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                        CustomFormDesignerBAL.AddFields(flsCustomField);
                        //var p = new PortfolioMgt.Entity.PartnerCategory();
                        //p.CategoryName = name;
                        //p.IsDeleted = false;
                        //p.PartnerID = Convert.ToInt32(typeid);

                        //var r = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_Add(p);
                        //if (r != null)
                        //{
                        //    li.Text = r.CategoryName;
                        //    li.Value = r.ID.ToString();
                        //}
                    }
                }
                else
                {
                    var flsCustomField = CustomFormDesignerBAL.GetFieldByID(Convert.ToInt32(id));
                    if (flsCustomField != null)
                    {
                        //flsCustomField.PartnerSubcategoryID = Convert.ToInt32(subid);
                        flsCustomField.TypeOfField = controltype;
                        flsCustomField.LabelName = cname;
                        flsCustomField.DefaultText = string.Empty;
                        flsCustomField.MinimumValue = "";
                        flsCustomField.MaximumValue = "";
                        flsCustomField.Mandatory = false;
                        flsCustomField.FieldPosition = "";
                        flsCustomField.ListValue = cvalues;
                        CustomFormDesignerBAL.UpdateFields(flsCustomField);
                    }

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
                    CustomFormDesignerBAL.DeleteField(Convert.ToInt32(id));
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
        public object BindFromData(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = CustomFormDesignerBAL.GetFieldByPartner(Convert.ToInt32(id));

                var rlist = (from r in result
                             orderby r.ID descending
                             select new
                             {
                                 ID = r.ID,
                                 LabelName = r.LabelName,
                                 ListValue = r.ListValue,
                                 PartnerSubcategoryID = r.PartnerSubcategoryID,
                                 TypeOfField = r.TypeOfField,
                                 MinimumValue = r.MinimumValue,
                                 MaximumValue = r.MaximumValue,
                                 Mandatory = r.Mandatory,
                                 FormID = r.FormID,
                                 FieldPosition = r.FieldPosition,
                                 DefaultText = r.DefaultText,
                                 CustomerID = r.CustomerID

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
        public object BindFromDataByID(string id)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                var result = CustomFormDesignerBAL.GetFieldListByID(Convert.ToInt32(id));

                var rlist = (from r in result
                             orderby r.ID descending
                             select new
                             {
                                 ID = r.ID,
                                 LabelName = r.LabelName,
                                 ListValue = r.ListValue,
                                 PartnerSubcategoryID = r.PartnerSubcategoryID,
                                 TypeOfField = r.TypeOfField,
                                 MinimumValue = r.MinimumValue,
                                 MaximumValue = r.MaximumValue,
                                 Mandatory = r.Mandatory,
                                 FormID = r.FormID,
                                 FieldPosition = r.FieldPosition,
                                 DefaultText = r.DefaultText,
                                 CustomerID = r.CustomerID

                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

    }
}
