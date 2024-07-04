using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.Entity;
using PortfolioMgt.BLL;

namespace DeffinityAppDev.WF.Health.HC.Services
{
    /// <summary>
    /// Summary description for FormsServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FormsServices : System.Web.Services.WebService
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
        public List<ListItem> CategoryGet(string typeid)
        {
            var rlist = PortfolioMgt.BAL.PartnerCategoryBAL.PartnerCategoryBAL_SelectByPartnerID(Convert.ToInt32(!string.IsNullOrEmpty(typeid) ? typeid : "0"));
            var result = (from p in rlist
                          where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.CategoryName

                          select new ListItem { Value = p.ID.ToString(), Text = p.CategoryName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem CategoryAdd(string typeid, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var p = new PortfolioMgt.Entity.PartnerCategory();
                p.CategoryName = name;
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

        public List<HealthCheck_Form> FormCollection1(int customerid)
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            if (customerid == 0)
                return hb.HealthCheck_Form_SelectAll();
            else
                return hb.HealthCheck_Form_SelectByCustomerID(customerid);
        }

        public List<HealthCheck_FormPanel> FormCollection(int formid)
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            
                return hb.HealthCheck_FormPanel_SelectByFormID(formid);
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> ServiceGet(string category)
        {
            var rlist = FormCollection1(sessionKeys.PortfolioID).OrderBy(o => o.FormName); //PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                              //where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.FormName
                          select new ListItem { Value = p.FormID.ToString(), Text = p.FormName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem ServiceAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var r = hb.HealthCheck_Form_Add(new HealthCheck_Form() { FormName = name, CustomerID = sessionKeys.PortfolioID });

                //var s = new PortfolioMgt.Entity.PartnerService();
                //s.ServiceName = name;
                //s.PartnerSubCategoryID = Convert.ToInt32(category == null ? "0" : category);
                //s.IsDeleted = false;

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Add(s);
                if (r != null)
                {
                    li.Text = r.FormName;
                    li.Value = r.FormID.ToString();
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
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var r = hb.HealthCheck_Form_update(new HealthCheck_Form() { FormName = name, CustomerID = sessionKeys.PortfolioID, FormID = Convert.ToInt32(id) });

                //var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                //s.ServiceName = name;
                ////s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.FormName;
                    li.Value = r.FormID.ToString();
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


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> FormGet(string category)
        {
            var rlist = FormCollection1(sessionKeys.PortfolioID).OrderBy(o => o.FormName); //PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                              //where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.FormName
                          select new ListItem { Value = p.FormID.ToString(), Text = p.FormName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem FormAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var r = hb.HealthCheck_Form_Add(new HealthCheck_Form() { FormName = name, CustomerID = sessionKeys.PortfolioID });

                //var s = new PortfolioMgt.Entity.PartnerService();
                //s.ServiceName = name;
                //s.PartnerSubCategoryID = Convert.ToInt32(category == null ? "0" : category);
                //s.IsDeleted = false;

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Add(s);
                if (r != null)
                {
                    li.Text = r.FormName;
                    li.Value = r.FormID.ToString();
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
        public ListItem FormUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var h = hb.HealthCheck_Form_SelectByID(Convert.ToInt32(id));
                h.FormName = name;
                var r = hb.HealthCheck_Form_update(h);

                //var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                //s.ServiceName = name;
                ////s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.FormName;
                    li.Value = r.FormID.ToString();
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
        public bool FormDelete(string id)
        {
            bool retval = false;
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                hb.HealthCheck_Form_Delete(id);
                //var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                //s.IsDeleted = true;
                ////s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                //DC.BLL.SubCategoryBAL.DeleteByID(Convert.ToInt32(id));
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> FormPanelGet(string category)
        {
            var rlist = FormCollection(Convert.ToInt32(category)).OrderBy(o => o.PanelName); //PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                              //where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.PanelID 
                          select new ListItem { Value = p.PanelID.ToString(), Text = p.PanelName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem FormPanelAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var r = hb.HealthCheck_FormPanel_Add(new HealthCheck_FormPanel() { PanelName = name, FormID = Convert.ToInt32(category) });

                //var s = new PortfolioMgt.Entity.PartnerService();
                //s.ServiceName = name;
                //s.PartnerSubCategoryID = Convert.ToInt32(category == null ? "0" : category);
                //s.IsDeleted = false;

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Add(s);
                if (r != null)
                {
                    li.Text = r.PanelName;
                    li.Value = r.PanelID.ToString();
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
        public ListItem FormPanelUpdate(string name, string id)
        {
            ListItem li = new ListItem();
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                //var h= new 
                var r = hb.HealthCheck_FormPanel_update(new HealthCheck_FormPanel() { PanelName = name, PanelID = Convert.ToInt32(id) });

                //var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                //s.ServiceName = name;
                ////s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
                if (r != null)
                {
                    li.Text = r.PanelName;
                    li.Value = r.PanelID.ToString();
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
        public bool FormPanelDelete(string id)
        {
            bool retval = false;
            try
            {
                HealthCheckBAL hb = new HealthCheckBAL();
                hb.HealthCheck_FormPanel_Delete(id);
                //var s = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(Convert.ToInt32(id));
                //s.IsDeleted = true;
                ////s.PartnerCategoryID = Convert.ToInt32(category == null ? "0" : category);

                //var r = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Update(s);
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
                        flsCustomField.CustomerID = sessionKeys.PortfolioID;
                        //if it is forms set 1
                        HealthCheckBAL hb = new HealthCheckBAL();
                        //get formid from panel id
                        var h = hb.HealthCheck_FormPanel_SelectByID(Convert.ToInt32(subid));
                        flsCustomField.PartnerSubcategoryID =  h.FormID.Value;
                        flsCustomField.PartnerServiceID = Convert.ToInt32(subid);
                        flsCustomField.TypeOfField = controltype;
                        flsCustomField.LabelName = cname;
                        flsCustomField.DefaultText = string.Empty;
                        flsCustomField.MinimumValue = "";
                        flsCustomField.MaximumValue = "";
                        flsCustomField.Mandatory = false;
                        flsCustomField.FieldPosition = "";
                        flsCustomField.ListValue = cvalues;
                        flsCustomField.Position = CustomFormDesignerBAL.GetFieldByPartner(Convert.ToInt32(subid)).Count()+1;
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
                        flsCustomField.CustomerID = sessionKeys.PortfolioID;
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

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem FromDataUpdatePosition(string id,string panelid, string newposition, string oldPosition)
        {
            ListItem li = new ListItem();
            try
            {
                 CustomFormDesignerBAL.UpdateRowPostion(Convert.ToInt32(id),Convert.ToInt32(panelid),Convert.ToInt32(newposition),Convert.ToInt32(oldPosition));
              

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
                             orderby r.Position 
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
                                 CustomerID = r.CustomerID,
                                 Position =r.Position
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
