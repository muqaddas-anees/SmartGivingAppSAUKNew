using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.Entity;
using PortfolioMgt.BLL;
using DeffinityAppDev.WF.Health.HC.Services;

namespace DeffinityAppDev.WF.Admin.Services
{
    /// <summary>
    /// Summary description for AdminServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdminServices : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> FormGet(string category)
        {
            HealthCheckBAL hb = new HealthCheckBAL();

            DeffinityAppDev.WF.Health.HC.Services.FormsServices f = new FormsServices();
            var rlist = hb.HealthCheck_Form_SelectByCustomerID(sessionKeys.PortfolioID).OrderBy(o => o.FormName); //PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                              //where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.FormName
                          select new ListItem { Value = p.FormID.ToString(), Text = p.FormName }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public List<ListItem> FormPanelGet(string category)
        {
            var rlist = FormPanelCollection(Convert.ToInt32(category)).OrderBy(o => o.PanelName); //PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(category == null ? "0" : category));
            var result = (from p in rlist
                              //where (p.IsDeleted.HasValue ? p.IsDeleted.Value : false) == false
                          orderby p.PanelID
                          select new ListItem { Value = p.PanelID.ToString(), Text = p.PanelName }).ToList();
            return result;

        }

        public List<HealthCheck_FormPanel> FormPanelCollection(int formid)
        {
            HealthCheckBAL hb = new HealthCheckBAL();

            return hb.HealthCheck_FormPanel_SelectByFormID(formid);
        }
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
                        flsCustomField.CustomerID = sessionKeys.PortfolioID;
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
        public object BindFromData(string id,string jobid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                
                var resultval = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(jobid)).ToList();
                if (resultval.Count() > 0)
                {
                    var rEntity = resultval.FirstOrDefault();
                   
                    //var subcategoryid = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(rEntity.ServiceID.Value).PartnerSubCategoryID;
                    //var categoryid = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Select(subcategoryid).PartnerCategoryID;

                    var result = CustomFormDesignerBAL.GetFieldByPartner(Convert.ToInt32(id));

                    var rlist = (from r in result
                                // join j in resultval on r.ID equals j.CustomFieldID
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
                                     PartnerServiceID = r.PartnerServiceID,
                                     ListValueExt = r.ListValueExt,
                                     ListValueValues = resultval.Count() > 0 ? (resultval.Where(o => o.CustomFieldID == r.ID).Count() > 0 ? resultval.Where(o => o.CustomFieldID == r.ID).FirstOrDefault().CustomFieldValue : string.Empty) : string.Empty,
                                     ListNotesValues = resultval.Count() > 0 ? (resultval.Where(o => o.CustomFieldID == r.ID).Count() > 0 ? resultval.Where(o => o.CustomFieldID == r.ID).FirstOrDefault().CustomFieldValueExt : string.Empty) : string.Empty,
                                     FileName = string.Empty, //(j.FileName == null? "":j.FileName),
                                     subcategoryid = string.Empty,
                                     categoryid = string.Empty,
                                     Position = r.Position,

                                 }).ToList();

                    return Jsonserializer.Serialize(rlist).ToString();
                }
                else if(id.Length >0)
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
                                     Position = r.Position,
                                     ListValueExt = r.ListValueExt,
                                     ListValueValues = resultval.Count()>0?( resultval.Where(o => o.CustomFieldID == r.ID).Count() > 0? resultval.Where(o=>o.CustomFieldID == r.ID).FirstOrDefault().CustomFieldValue:string.Empty):string.Empty,
                                     ListNotesValues = string.Empty
                                 }).ToList();

                    return Jsonserializer.Serialize(rlist).ToString();
                   
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

        public class FormDisplay
        {
           public int? ID { set; get; }
            public string LabelName { set; get; }
            public string ListValue { set; get; }
            public int? PartnerSubcategoryID { set; get; }
            public string TypeOfField { set; get; }
            public string MinimumValue { set; get; }
            public string MaximumValue { set; get; }
            public bool? Mandatory { set; get; }
            public int? FormID { set; get; }
            public string FieldPosition { set; get; }
            public string DefaultText { set; get; }
            public int? CustomerID { set; get; }
            public int? PartnerServiceID { set; get; }
            public string ListValueExt { set; get; }
            public string ListValueValues { set; get; }
            public string ListNotesValues { set; get; }
            public string FileName { set; get; }
            public int? subcategoryid { set; get; }
            public int? categoryid { set; get; }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindFromDataByFormAndJobID(string jobid, string fid = "0")
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<FormDisplay> rlist = new List<FormDisplay>();


                HealthCheckBAL hb = new HealthCheckBAL();

                if (jobid != "0")
                {
                    var resultval = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(jobid));
                    if (resultval.Count() > 0)
                    {
                        var rEntity = resultval.FirstOrDefault();
                        var pnlids = resultval.Select(o => o.ServiceID).Distinct().ToList();

                        //var categoryid = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Select(subcategoryid).PartnerCategoryID;

                        foreach (var p in pnlids)
                        {
                            //var pnlEntity = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(rEntity.ServiceID.Value);
                            var result = CustomFormDesignerBAL.GetFieldByPanel(p.Value);

                            //var panelid = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(rEntity.ServiceID.HasValue ? rEntity.ServiceID.Value : 0));
                            var pnlEntity = hb.HealthCheck_FormPanel_SelectByID(p.Value);
                            rlist.Add(new FormDisplay() { ID = pnlEntity.PanelID, LabelName = pnlEntity.PanelName, TypeOfField = "Subheading", PartnerServiceID = p, PartnerSubcategoryID = pnlEntity.FormID });

                            var rtemp = (from r in result
                                         join j in resultval on r.ID equals j.CustomFieldID
                                         orderby r.ID descending
                                         select new FormDisplay
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
                                             PartnerServiceID = r.PartnerServiceID,
                                             ListValueExt = r.ListValueExt,
                                             ListValueValues = j.CustomFieldValue,
                                             ListNotesValues = j.CustomFieldValueExt,
                                             FileName = (j.FileName == null ? "" : j.FileName),
                                             subcategoryid = 0,
                                             categoryid = 0

                                         }).ToList();

                            rlist.AddRange(rtemp);

                        }
                    }
                    return Jsonserializer.Serialize(rlist).ToString();
                }
                else if (jobid == "0")
                {
                    var resultval = CustomFormDesignerBAL.GetFieldByFormID(Convert.ToInt32(fid));
                    if (resultval.Count() > 0)
                    {
                        var rEntity = resultval.FirstOrDefault();
                        var pnlids = resultval.Select(o => o.PartnerServiceID).Distinct().ToList();

                        //var categoryid = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_Select(subcategoryid).PartnerCategoryID;

                        foreach (var p in pnlids)
                        {
                            //var pnlEntity = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_Select(rEntity.ServiceID.Value);
                            var result = CustomFormDesignerBAL.GetFieldByPanel(p.Value);

                            //var panelid = PortfolioMgt.BAL.PartnerServiceBAL.PartnerServiceBAL_SelectBySubCategoryID(Convert.ToInt32(rEntity.ServiceID.HasValue ? rEntity.ServiceID.Value : 0));
                            var pnlEntity = hb.HealthCheck_FormPanel_SelectByID(p.Value);
                            rlist.Add(new FormDisplay() { ID = pnlEntity.PanelID, LabelName = pnlEntity.PanelName, TypeOfField = "Subheading", PartnerServiceID = p, PartnerSubcategoryID = pnlEntity.FormID });

                            var rtemp = (from r in resultval
                                         where r.PartnerServiceID == p
                                             //join j in resultval on r.ID equals j.CustomFieldID
                                         orderby r.ID descending
                                         select new FormDisplay
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
                                             PartnerServiceID = r.PartnerServiceID,
                                             ListValueExt = r.ListValueExt,
                                             ListValueValues = "",//j.CustomFieldValue,
                                             ListNotesValues = "",// j.CustomFieldValueExt,
                                             FileName = "",// (j.FileName == null ? "" : j.FileName),
                                             subcategoryid = 0,
                                             categoryid = 0

                                         }).ToList();

                            rlist.AddRange(rtemp);

                        }
                    }

                    return Jsonserializer.Serialize(rlist).ToString();
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
                                 CustomerID = r.CustomerID,
                                 PartnerServiceID = r.PartnerServiceID,
                                 ListValueExt = r.ListValueExt
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
        public object ApplyServicetoJob(string serviceid,string jobid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                int sid = Convert.ToInt32(serviceid);
                int callid = Convert.ToInt32(jobid);
                //delete existing data
                var elist = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32( jobid));
                if(elist.Count >0)
                 FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID_Delete(Convert.ToInt32(jobid));


                var result = CustomFormDesignerBAL.GetFieldByPartner(Convert.ToInt32(serviceid));
                //insert all control ids
                foreach(var r in result)
                {

                    FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(new FLSAdditionalInfo() { CallID= callid,ServiceID = sid, CustomFieldID = r.ID,CustomFieldValue=string.Empty, CustomFieldValueExt=string.Empty,LoggedDatetime=DateTime.Now,Hours=0,Minutes=0 });
                }

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
                                 CustomerID = r.CustomerID,
                                 PartnerServiceID = r.PartnerServiceID,
                                 ListValueExt = r.ListValueExt,
                                
                             }).ToList();

                return Jsonserializer.Serialize(rlist).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        public class checklistidvalues
        {
            public int checkboxid { set; get; }

            public string checkboxvalue { set; get; }
            public int checkboxtextid { set; get; }
            public string checkboxtextvalue { set; get; }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object UpdateData(string[] d,object[] file, string jobid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
               
                int callid = Convert.ToInt32(jobid);
                var add_list = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(callid);
                List<checklistidvalues> cklist = new List<checklistidvalues>();

                //foreach(var s in add_list)
                //{
                //    //text box
                //    var tupdate = 

                //}

                foreach (var s in d)
                {
                    var rdata = s.Split(':');
                    if (rdata.Length > 0)
                    {
                        //ctl_chk_1_1
                        //ctl_txt_1
                        //ctl_ddl_1
                        var rawid = rdata[0].Split('_');
                        //check type 
                        if (rawid[1] == "txt" || rawid[1] == "ddl")
                        {
                            //control id
                            var ctrlid = rawid[2];
                            var ctrlval = rdata[1];
                            var u = add_list.Where(o => o.CustomFieldID == Convert.ToInt32(ctrlid)).FirstOrDefault();
                            u.CustomFieldValue = ctrlval;
                            FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);

                        }
                        else if (rawid[1] == "chk")
                        {
                            var ctrlid = rawid[2];
                            var ctrltxtid = rawid[3];
                            var ctrlval = rdata[1];
                            var ctrltextval = rdata[2];
                            cklist.Add(new checklistidvalues() { checkboxid = Convert.ToInt32(ctrlid), checkboxvalue = ctrlval, checkboxtextid = Convert.ToInt32(ctrltxtid), checkboxtextvalue = ctrltextval });
                        }

                    }
                   

                }
                //group by check box control id


                //update the check box data
                if (cklist.Count() > 0)
                {
                    var chkids = cklist.Select(o => o.checkboxid).Distinct().ToList();
                    foreach (var c in chkids)
                    {
                        var tlist = cklist.Where(o => o.checkboxid == c).ToList();
                        string tvals = string.Empty;
                        string tnotes = string.Empty;
                        foreach (var t in tlist)
                        {
                            tvals = tvals + t.checkboxvalue + ",";
                            tnotes = tnotes + t.checkboxtextvalue + ",";
                        }

                        var u = add_list.Where(o => o.CustomFieldID == c).FirstOrDefault();
                        u.CustomFieldValue = tvals.TrimEnd(',');
                        u.CustomFieldValueExt = tnotes.TrimEnd(',');
                        FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);
                    }

                }


                return Jsonserializer.Serialize("success").ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object JobUpdateData(string[] d, string jobid,string formid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {

                int callid = Convert.ToInt32(jobid);
                var add_list = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(callid);
                //update form id

                var fid = Convert.ToInt32(!string.IsNullOrEmpty(formid) ? formid : "0");
                if (fid > 0)
                {
                    // var controlEntity = CustomFormDesignerBAL.GetFieldByID(Convert.ToInt32(ctrlid));
                    var hb = new HealthCheckBAL();
                    var retval = hb.HealthCheck_FormAssignToCall_SelectByCallID(callid);
                    if (retval == null)
                    {
                        retval = new HealthCheck_FormAssignToCall();
                        retval.CallID = callid;
                        retval.FormID = fid;
                        hb.HealthCheck_FormAssignToCall_Add(retval);
                    }
                    else
                    {
                        hb.HealthCheck_FormAssignToCall_DeleteByCallID(callid);

                        retval = new HealthCheck_FormAssignToCall();
                        retval.CallID = callid;
                        retval.FormID = fid;
                        hb.HealthCheck_FormAssignToCall_Add(retval);
                    }
                }
                List<checklistidvalues> cklist = new List<checklistidvalues>();

                //foreach(var s in add_list)
                //{
                //    //text box
                //    var tupdate = 

                //}
                int inc = 1;

                foreach (var s in d)
                {
                    var rdata = s.Split(':');
                    if (rdata.Length > 0)
                    {
                        //ctl_chk_1_1
                        //ctl_txt_1
                        //ctl_ddl_1
                        var rawid = rdata[0].Split('_');
                        var ctrlid = rawid[2];
                        //check type 
                        if (rawid[1] == "txt" || rawid[1] == "ddl")
                        {
                            //control id
                          
                            var ctrlval = rdata[1];
                            var u = add_list.Where(o => o.CustomFieldID == Convert.ToInt32(ctrlid)).FirstOrDefault();
                            if (u != null)
                            {
                                u.CustomFieldValue = ctrlval;
                                FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);
                            }
                            else
                            {
                                u = new FLSAdditionalInfo();
                                u.CallID = callid;
                                u.CustomFieldID = Convert.ToInt32(ctrlid);
                                u.CustomFieldValue = ctrlval;
                                FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(u);
                            }

                        }
                        else if (rawid[1] == "chk")
                        {
                            //var ctrlid = rawid[2];
                            var ctrltxtid = rawid[3];
                            var ctrlval = rdata[1];
                            var ctrltextval = rdata[2];
                            cklist.Add(new checklistidvalues() { checkboxid = Convert.ToInt32(ctrlid), checkboxvalue = ctrlval, checkboxtextid = Convert.ToInt32(ctrltxtid), checkboxtextvalue = ctrltextval });
                        }

                       
                        inc = inc + 1;

                    }


                }
                //group by check box control id
              

                //update the check box data
                if (cklist.Count() > 0)
                {
                    var chkids = cklist.Select(o => o.checkboxid).Distinct().ToList();
                    foreach (var c in chkids)
                    {
                        var tlist = cklist.Where(o => o.checkboxid == c).ToList();
                        string tvals = string.Empty;
                        string tnotes = string.Empty;
                        foreach (var t in tlist)
                        {
                            tvals = tvals + t.checkboxvalue + ",";
                            tnotes = tnotes + t.checkboxtextvalue + ",";
                        }

                        var u = add_list.Where(o => o.CustomFieldID == c).FirstOrDefault();
                        if (u != null)
                        {
                            u.CustomFieldValue = tvals.TrimEnd(',');
                            u.CustomFieldValueExt = tnotes.TrimEnd(',');
                            FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);
                        }
                        else
                        {
                            u = new FLSAdditionalInfo();
                            u.CallID = callid;
                            u.CustomFieldID = c;
                            u.CustomFieldValue = tvals.TrimEnd(',');
                            u.CustomFieldValueExt = tnotes.TrimEnd(',');
                            FLSAdditionalInfoBAL.InsertFLSAdditionalInfo(u);
                        }
                    }

                }


                return Jsonserializer.Serialize("success").ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public object BindHVACData(string jobid)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {

                var result = HVACDiagnosticsTimeBAL.HVACDiagnosticsTimeBAL_SelectJobID(Convert.ToInt32(jobid));
                if (result.Count() > 0)
                {
                    

                    var rlist = (from r in result
                                 orderby r.ID descending
                                 select new
                                 {
                                     ID=r.ID,
                                     LoggedBy = r.LoggedBy,
                                     CallID = r.CallID,
                                     StartTime = r.StartTime,
                                     StopTime = r.StopTime,
                                     StartHour = r.StopTime.Value.Hour,
                                     StartMinute = r.StopTime.Value.Minute,
                                     StartSecond = r.StopTime.Value.Second,
                                     Status = r.Status,

                                 }).ToList();

                    return Jsonserializer.Serialize(rlist).ToString();
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
        [System.Web.Script.Services.ScriptMethod]
        public object StartTimeJob( string jobid,string hour,string minute, string second,string status)
        {
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                
                int callid = Convert.ToInt32(jobid);
                HVACDiagnosticsTime h = new HVACDiagnosticsTime();
                h.CallID = callid;
                h.LoggedBy = sessionKeys.UID;
                h.StartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                h.StopTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(hour), Convert.ToInt32(minute), Convert.ToInt32(second));
                h.Status = Convert.ToInt32(status);
               
                HVACDiagnosticsTimeBAL.HVACDiagnosticsTimeBAL_Update(h);

                return Jsonserializer.Serialize(string.Empty).ToString();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Jsonserializer.Serialize(string.Empty).ToString();
            }
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var controlid = HttpContext.Current.Request.Form["id"].ToString();
                var jobid = HttpContext.Current.Request.Form["jobid"].ToString();
                // Get the uploaded image from the Files collection
                var userPostedFile = HttpContext.Current.Request.Files[0];

                if (userPostedFile != null)
                {
                    // Validate the uploaded image(optional)

                    // Get the complete file path
                    // var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/WF/UploadedFiles/"), httpPostedFile.FileName);

                    // Save the uploaded file to "UploadedFiles" folder
                    //httpPostedFile.SaveAs(fileSavePath);
                    var folder = HttpContext.Current.Server.MapPath(string.Format( "~/WF/UploadData/DG/{0}/{1}", jobid.ToString(),controlid));
                    string fileName = Path.GetFileName(userPostedFile.FileName);

                    if (!System.IO.Directory.Exists(folder))
                    {
                        System.IO.Directory.CreateDirectory(folder);
                        userPostedFile.SaveAs(folder + "\\" + fileName);
                    }
                    else
                    {
                        userPostedFile.SaveAs(folder + "\\" + fileName);
                    }
                    var u = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32( jobid)).Where(o => o.CustomFieldID == Convert.ToInt32( controlid)).FirstOrDefault();
                    if (u != null)
                    {
                        u.FileName = fileName;
                        FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);
                    }
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void DeleteFile()
        {

            var controlid = HttpContext.Current.Request.Form["id"].ToString();
            var jobid = HttpContext.Current.Request.Form["jobid"].ToString();
            var u = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(jobid)).Where(o => o.CustomFieldID == Convert.ToInt32(controlid)).FirstOrDefault();

            var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/DG/{0}/{1}/{2}", jobid.ToString(), controlid,u.FileName));
            

            if (System.IO.File.Exists(file))
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
           
            
            if (u != null)
            {
                u.FileName = "";
                FLSAdditionalInfoBAL.UpdateFLSAddtionalInfo(u);
            }


        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void downloadFile()
        {
            WebClient req = new WebClient();
            var controlid = HttpContext.Current.Request.Form["id"].ToString();
            var jobid = HttpContext.Current.Request.Form["jobid"].ToString();
            
            
            var u = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(jobid)).Where(o => o.CustomFieldID == Convert.ToInt32(controlid)).FirstOrDefault();
            if (u != null)
            {
                var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/DG/{0}/{1}/{2}", jobid.ToString(), controlid,u.FileName));
                //byte[] ls1 = folder + ;
                HttpResponse response = Context.Response;

                response.Clear();
                //response.BufferOutput = true;
                response.ContentType = "application/octet-stream";
                //response.ContentEncoding = Encoding.UTF8;
                response.AppendHeader("content-disposition", "attachment; filename=" + u.FileName);
                response.TransmitFile(file);
                response.End();
                //response.BinaryWrite(ls1);

                response.Flush();
                response.Close();
            }
        }

    }
}
