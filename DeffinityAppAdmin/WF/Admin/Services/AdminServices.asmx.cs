using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

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
                if(r != null)
                {
                    li.Text = r.Portfoliotype1;
                    li.Value = r.ID.ToString();
                }
               
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            var rlist = DC.BLL.CategoryBAL.GetCategoryList(Convert.ToInt32(!string.IsNullOrEmpty( typeid)?typeid:"0"));
            var result = (from p in rlist
                          orderby p.Name
                          select new ListItem { Value = p.ID.ToString(), Text = p.Name }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem CategoryAdd(string typeid,string name)
        {
            ListItem li = new ListItem();
            try
            {
                var r  = DC.BLL.CategoryBAL.AddCategory(Convert.ToInt32(typeid),name);
                if (r != null)
                {
                    li.Text = r.Name;
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
                var r = DC.BLL.CategoryBAL.UpdateCategory(Convert.ToInt32(id),name);
                if (r != null)
                {
                    li.Text = r.Name;
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
                DC.BLL.CategoryBAL.DeleteByID(Convert.ToInt32(id));
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
            var rlist = DC.BLL.SubCategoryBAL.GetSubCategoryList(Convert.ToInt32( category== null? "0" :category));
            var result = (from p in rlist
                          orderby p.Name
                          select new ListItem { Value = p.ID.ToString(), Text = p.Name }).ToList();
            return result;

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public ListItem SubCategoryAdd(string category, string name)
        {
            ListItem li = new ListItem();
            try
            {
                var r = DC.BLL.SubCategoryBAL.AddSubCategory(Convert.ToInt32(category == null ? "0" : category), name);
                if (r != null)
                {
                    li.Text = r.Name;
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
                var r = DC.BLL.SubCategoryBAL.UpdateSubCategory(Convert.ToInt32(id),name);
                if (r != null)
                {
                    li.Text = r.Name;
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
                DC.BLL.SubCategoryBAL.DeleteByID(Convert.ToInt32(id));
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }

        #endregion


        //CopySection
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public bool CopySection(string fromid,string toid)
        {
            bool retval = false;
            try
            {
                var slist = toid.Split(',');
                foreach (var v in slist)
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        if (Convert.ToInt32(fromid) != Convert.ToInt32(v.Trim()))
                            PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Copy(Convert.ToInt32(fromid), Convert.ToInt32(v.Trim()));
                    }
                }
                retval = true;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;

        }
    }
}
