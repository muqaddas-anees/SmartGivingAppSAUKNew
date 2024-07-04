using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Deffinity.Bindings;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for Portfilio
/// </summary>
namespace Deffinity.PortfolioManager
{
    public class Portfilio
    {
        public static SqlDataReader SelectPortfolio(int PortfilioID)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfilioSelect", new SqlParameter("@PortfolioID", PortfilioID));
        }
        public static DataTable Portfolio_display()
        {
            string key = CacheNames.DefaultNames.Portfolio.ToString();
            if (BaseCache.Cache_Select(key) == null)
            {
                BaseCache.Cache_Insert(key, DefaultDatabind.AddSelectRow("ID", "PortFolio", DefaultDatabind.b_Portfolio(), 3));
            }
            return BaseCache.Cache_Select(key) as DataTable;
        }
        public static DataTable Portfolio_display(int SelectType)
        {
            //string key = CacheNames.DefaultNames.Portfolio.ToString();
            //if (BaseCache.Cache_Select(key) == null)
            //{
            //    BaseCache.Cache_Insert(key, DefaultDatabind.AddSelectRow("ID", "PortFolio", DefaultDatabind.b_Portfolio(), SelectType));
            //}

            //return BaseCache.Cache_Select(key) as DataTable;
            return DefaultDatabind.AddSelectRow("ID", "PortFolio", DefaultDatabind.b_Portfolio(), SelectType);
        }
        #region assign to customer 
        /// <summary>
        /// if OutVal - 1-> associated to another portfolio 2->associated to selected portfolio 3-> inserted successfully
        /// </summary>
        /// <param name="PortfolioID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="OutVal"></param>
        public static void InsertAssociatedCustomers(int PortfolioID,int CustomerID,out int OutVal)
        { 
            SqlParameter _outval = new SqlParameter("@OutVal",SqlDbType.Int,4);
            _outval.Direction= ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_AssignedCustomerToPortfolio_insert", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@CustomerID", CustomerID), _outval);

            OutVal = int.Parse( _outval.Value.ToString());
        }

        #endregion


        public static string setLogo()
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID);
           // string pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", sessionKeys.PartnerID);
            string userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
            if (File.Exists(userImg))
            {
                defaultImg = cImg;
            }
            //else
            //{
            //    //userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
            //    //if (File.Exists(userImg))
            //        defaultImg = pImg;
            //}
            return defaultImg+"?d="+DateTime.Now.ToShortTimeString();
        }
        public static string setPartnerLogo()
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            //string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID);
            string pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", sessionKeys.PartnerID);
            string userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
            //if (File.Exists(userImg))
            //{
            //    defaultImg = cImg;
            //}
            //else
            //{
               // userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
                if (File.Exists(userImg))
                    defaultImg = pImg;
            //}
            return defaultImg + "?d=" + DateTime.Now.ToShortTimeString();
        }
        public static string setLogo(int portfolioid)
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", portfolioid);
            string userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
            if (File.Exists(userImg))
                defaultImg = cImg;
            return defaultImg + "?d=" + DateTime.Now.ToShortTimeString();
        }

        public static string setMailLogo()
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", sessionKeys.PortfolioID);
            string pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", sessionKeys.PartnerID);
            string userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
            if (File.Exists(userImg))
            {
                defaultImg = cImg;
            }
            else
            {
                userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
                if (File.Exists(userImg))
                    defaultImg = pImg;
            }
            return defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
        }

        public static string setMailLogo(int portfolioid)
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", portfolioid);
            string userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
            if (File.Exists(userImg))
                defaultImg = cImg;
            return defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
        }

        public static string setMailLogo(int portfolioid,int partnerid)
        {
           
                string defaultImg = "~/Content/assets/images/logo@2x.png";
                string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", portfolioid);
                string pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", partnerid);
                string userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
                if (File.Exists(userImg))
                {
                    defaultImg = cImg;
                }
                else
                {
                    userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
                    if (File.Exists(userImg))
                        defaultImg = pImg;
                }
                return defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
            

        }

        public static string UpdateLogoForAll()
        {
           
            string defaultImg = string.Empty;
            string cImg = string.Empty;
            string pImg = string.Empty;
            string userImg = string.Empty;


           var plist = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.Visible == true).ToList();
            var prlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.IsActive == true).ToList();

            foreach (var p in plist)
            {
                if (p.PartnerID.HasValue)
                {
                    var prEntity = prlist.Where(o => o.ID == p.PartnerID).FirstOrDefault();
                    if (prEntity != null)
                    {
                        var websiteurl = prEntity.ParnerPortal;
                        defaultImg = "~/Content/assets/images/logo@2x.png";
                        cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", p.ID);
                        pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", p.PartnerID);
                        userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
                        if (File.Exists(userImg))
                        {
                            defaultImg = cImg;
                        }
                        else
                        {
                            userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
                            if (File.Exists(userImg))
                                defaultImg = pImg;
                        }
                        defaultImg = websiteurl + defaultImg.Replace("~", "");
                        p.LogoPath = defaultImg;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(p);
                    }
                }
               // return websiteurl + defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
            }

            return defaultImg;
        }


        public static string UpdateLogo(int portfolioid)
        {

            string defaultImg = string.Empty;
            string cImg = string.Empty;
            string pImg = string.Empty;
            string userImg = string.Empty;


            var plist = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.Visible == true && o.ID == portfolioid).ToList();
            var prlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.IsActive == true).ToList();

            foreach (var p in plist)
            {
                if (p.PartnerID.HasValue)
                {
                    var prEntity = prlist.Where(o => o.ID == p.PartnerID).FirstOrDefault();
                    if (prEntity != null)
                    {
                        var websiteurl = prEntity.ParnerPortal;
                        defaultImg = "~/Content/assets/images/logo@2x.png";
                        cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", p.ID);
                        pImg = string.Format("~/WF/UploadData/Customers/partner_{0}.png", p.PartnerID);
                        userImg = System.Web.HttpContext.Current.Server.MapPath(cImg);
                        if (File.Exists(userImg))
                        {
                            defaultImg = cImg;
                        }
                        else
                        {
                            userImg = System.Web.HttpContext.Current.Server.MapPath(pImg);
                            if (File.Exists(userImg))
                                defaultImg = pImg;
                        }
                        defaultImg = websiteurl + defaultImg.Replace("~", "");
                        p.LogoPath = defaultImg;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(p);
                    }
                }
                // return websiteurl + defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
            }

            return defaultImg;
        }
        public static string setMailLogo(int portfolioid,string defaultFolder)
        {
            string defaultImg = "~/Content/assets/images/logo@2x.png";
            string cImg = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", portfolioid);
            string userImg =defaultFolder+ "Customers/portfolio_"+ portfolioid + ".png"; //System.Web.HttpContext.Current.Server.MapPath(cImg);
            if (File.Exists(userImg))
                defaultImg = cImg;
            return defaultImg.Replace("~", "");// + "?d=" + DateTime.Now.Second.ToString();
        }
    }
}
