using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DC.DAL;
using DC.Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using UserMgt.BAL;
using UserMgt.Entity;
using UserMgt.DAL;
using System.Data;
using AssetsMgr.DAL;
using System.Xml;
using System.Reflection;

namespace DeffinityAppDev.WF.DC1
{
    public partial class GMap1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetAllPincodesOfRequester(int callid)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<MarkersData> MarkersDataList = new List<MarkersData>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
                IDCRespository<FLSDetail> fReporsitory = new DCRepository<FLSDetail>();
                var sids = new int[] { 22, 43, 44 };
                var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value)).ToList();
                var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(callid)).ToList();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
                var requesterAddressList = paReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ContactID)).ToList();
                Dictionary<string, object> row;
                //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
                //{
                List<LocationDisplayClass> LocationPinCodeResult = new List<LocationDisplayClass>();
                if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
                    LocationPinCodeResult = (from a in requesterList
                                             join a1 in requesterAddressList on a.ID equals a1.ContactID
                                             join b in dList on a.ID equals b.RequesterID

                                             orderby b.ID descending
                                             //where a.PortfolioID == sessionKeys.PortfolioID
                                             select new LocationDisplayClass
                                             {

                                                 
                                                 LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                                                 address = "<br>" + "" +  b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                                                 Id = a1.ID
                                             }).Take(10).ToList();

                else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                    LocationPinCodeResult = (from a in requesterList
                                             join a1 in requesterAddressList on a.ID equals a1.ContactID
                                             join b in dList on a.ID equals b.RequesterID
                                             join f in fList on b.ID equals f.CallID
                                             where f.UserID == sessionKeys.UID
                                             orderby b.ID descending

                                             //where a.PortfolioID == sessionKeys.PortfolioID
                                             select new LocationDisplayClass
                                             {
                                                 LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                                                 address = "<br>" + "" + b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                                                 Id = a1.ID
                                             }).Take(10).ToList();

                //var LocationPinCodeResult = (from a in asset.V_Assets
                //                             where a.PortfolioID == sessionKeys.PortfolioID
                //                             select new
                //                             {
                //                                 LocationPinCode = a.FromLocation,
                //                                 Id = a.ID
                //                             }).ToList();
                //var AssetAssociatedToContactsList = asset.AssetAssociatedToContacts.
                //    Where(c => c.ContactId.Value == int.Parse(RequesterId)).Select(c => c.AssectId.Value.ToString()).ToArray();
                //LocationPinCodeResult = LocationPinCodeResult.
                //    Where(a => ContainsNew(a.Id.ToString(), AssetAssociatedToContactsList)).ToList();

                MarkersData MarkersDataSingle;

                foreach (var x in LocationPinCodeResult)
                {
                    MarkersDataSingle = new MarkersData();
                    MarkersDataSingle.title = x.LocationPinCode;
                    string LL = getLatLong(x.LocationPinCode);
                    string[] LLArray = LL.Split(',');
                    MarkersDataSingle.lat = LLArray[1];
                    MarkersDataSingle.lng = LLArray[2];
                    MarkersDataSingle.description = x.address; //LLArray[3];
                    MarkersDataSingle.Id = x.Id;
                    MarkersDataList.Add(MarkersDataSingle);
                }

                DataTable dt = ToDataTable(MarkersDataList);
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return serializer.Serialize(rows);
        }
        public string getLatLong(string Zip)
        {
            string latlong = "", address = "";
            address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=AIzaSyCZ15oQjmlmnkqQ5kVUgct_s1rOmARC0DI";
            var result = new System.Net.WebClient().DownloadString(address);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNodeList parentNode = doc.GetElementsByTagName("location");
            var lat = "";
            var lng = "";
            foreach (XmlNode childrenNode in parentNode)
            {
                lat = childrenNode.SelectSingleNode("lat").InnerText;
                lng = childrenNode.SelectSingleNode("lng").InnerText;
            }
            latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
            return latlong;
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public class LocationDisplayClass
        {
            public string LocationPinCode { set; get; }
            public string name { set; get; }
            public string jobref { set; get; }
            public string address { set; get; }
            public string address_toset { set; get; }
            public int Id { set; get; }
            public int cid { set; get; }
            public decimal lat { set; get; }
            public decimal lng { set; get; }
        }

        //public string GetAllPincodesOfRequester()
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<MarkersData> MarkersDataList = new List<MarkersData>();
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    try
        //    {
        //        IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
        //        IDCRespository<FLSDetail> fReporsitory = new DCRepository<FLSDetail>();
        //        var sids = new int[] { 22, 43, 44 };
        //        var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value)).ToList();
        //        var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
        //        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
        //        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
        //        var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
        //        var requesterAddressList = paReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ContactID)).ToList();
        //        Dictionary<string, object> row;
        //        //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
        //        //{
        //        List<LocationDisplayClass> LocationPinCodeResult = new List<LocationDisplayClass>();
        //        if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
        //            LocationPinCodeResult = (from a in requesterList
        //                                     join a1 in requesterAddressList on a.ID equals a1.ContactID
        //                                     join b in dList on a.ID equals b.RequesterID

        //                                     orderby b.ID descending
        //                                     //where a.PortfolioID == sessionKeys.PortfolioID
        //                                     select new LocationDisplayClass
        //              {
        //                  LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
        //                  address = "<br>" + "" + b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
        //                  Id = a1.ID
        //              }).Take(10).ToList();

        //        else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
        //            LocationPinCodeResult = (from a in requesterList
        //                                     join a1 in requesterAddressList on a.ID equals a1.ContactID
        //                                     join b in dList on a.ID equals b.RequesterID
        //                                     join f in fList on b.ID equals f.CallID
        //                                     where f.UserID == sessionKeys.UID
        //                                     orderby b.ID descending

        //                                     //where a.PortfolioID == sessionKeys.PortfolioID
        //                                     select new LocationDisplayClass
        //                                     {
        //                                         LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
        //                                         address = "<br>" + "" + b.ID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
        //                                         Id = a1.ID
        //                                     }).Take(10).ToList();

        //        //var LocationPinCodeResult = (from a in asset.V_Assets
        //        //                             where a.PortfolioID == sessionKeys.PortfolioID
        //        //                             select new
        //        //                             {
        //        //                                 LocationPinCode = a.FromLocation,
        //        //                                 Id = a.ID
        //        //                             }).ToList();
        //        //var AssetAssociatedToContactsList = asset.AssetAssociatedToContacts.
        //        //    Where(c => c.ContactId.Value == int.Parse(RequesterId)).Select(c => c.AssectId.Value.ToString()).ToArray();
        //        //LocationPinCodeResult = LocationPinCodeResult.
        //        //    Where(a => ContainsNew(a.Id.ToString(), AssetAssociatedToContactsList)).ToList();

        //        MarkersData MarkersDataSingle;

        //        foreach (var x in LocationPinCodeResult)
        //        {
        //            MarkersDataSingle = new MarkersData();
        //            MarkersDataSingle.title = x.LocationPinCode;
        //            string LL = getLatLong(x.LocationPinCode);
        //            string[] LLArray = LL.Split(',');
        //            MarkersDataSingle.lat = LLArray[1];
        //            MarkersDataSingle.lng = LLArray[2];
        //            MarkersDataSingle.description = x.address; //LLArray[3];
        //            MarkersDataSingle.Id = x.Id;
        //            MarkersDataList.Add(MarkersDataSingle);
        //        }

        //        DataTable dt = ToDataTable(MarkersDataList);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }

        //        // }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //    return serializer.Serialize(rows);
        //}
    }
    public class MarkersData
    {
        public string title { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int Id { get; set; }
        public string description { get; set; }
    }
    public class Column
    {
        public string FieldName { get; set; }
        public string ColumnName { get; set; }
    }
}
