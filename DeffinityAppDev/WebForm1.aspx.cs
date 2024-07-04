using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace TurbProWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string getLatLong(string Zip)
        {
            string latlong = "", address = "";

            //IDCRespository<DC.Entity.GeoCode> gRep = new DCRepository<DC.Entity.GeoCode>();
            //var gEntity = gRep.GetAll().Where(o=>o.Zip == Zip.Trim()).FirstOrDefault();
            //if(gEntity!= null)
            //{
            //    latlong = Zip + "," + Convert.ToString(gEntity.Latitude) + "," + Convert.ToString(gEntity.Longitude) + "," + "" + Zip;
            //}

            address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=" + hkey.Value;
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
        //public string getLatLong(string Zip)
        //{
        //    string latlong = "", address = "";
        //    address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=AIzaSyCZ15oQjmlmnkqQ5kVUgct_s1rOmARC0DI";
        //    var result = new System.Net.WebClient().DownloadString(address);
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(result);
        //    XmlNodeList parentNode = doc.GetElementsByTagName("location");
        //    var lat = "";
        //    var lng = "";
        //    foreach (XmlNode childrenNode in parentNode)
        //    {
        //        lat = childrenNode.SelectSingleNode("lat").InnerText;
        //        lng = childrenNode.SelectSingleNode("lng").InnerText;
        //    }
        //    latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
        //    return latlong;
        //}
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
        public string GetAllPincodesOfRequester()
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<MarkersData> MarkersDataList = new List<MarkersData>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
  
                
                //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
                //{
                List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass> LocationPinCodeResult = new List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass>();

                LocationPinCodeResult.Add(new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass()
                {
                    address = "demo",
                    cid = 1,
                    Id=1,
                    jobref="11",
                    lat= Convert.ToDecimal("13.082680"),
                    lng = Convert.ToDecimal("80.270721"),
                    LocationPinCode="600090",
                    name = "myhome",
                    

                });
                ;
              

                MarkersData MarkersDataSingle;
                Dictionary<string, object> row;
                foreach (var x in LocationPinCodeResult)
                {
                    MarkersDataSingle = new MarkersData();
                    MarkersDataSingle.title = x.LocationPinCode;
                    MarkersDataSingle.name = x.name;

                    if (x.lat > 0)
                    {
                        MarkersDataSingle.lat = x.lat.ToString();
                        MarkersDataSingle.lng = x.lng.ToString();
                    }
                    else
                    {
                        string LL = getLatLong(x.LocationPinCode);
                        string[] LLArray = LL.Split(',');
                        MarkersDataSingle.lat = LLArray[1];
                        MarkersDataSingle.lng = LLArray[2];
                    }
                    MarkersDataSingle.description = x.address; //LLArray[3];
                    MarkersDataSingle.Id = x.Id;
                    MarkersDataSingle.cid = x.cid;
                    if (x.address.ToString().ToLower().Contains("service tech"))
                    {
                        MarkersDataSingle.color = "green";
                        MarkersDataSingle.imgtype = "user";
                    }
                    else
                    {
                        MarkersDataSingle.color = "red";
                        MarkersDataSingle.imgtype = "contact";
                        MarkersDataSingle.jobref = x.jobref;
                    }

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
        public decimal GetCurrentLocation(List<PortfolioMgt.Entity.UserTracking> ul, int userid, string postcode, string type)
        {
            decimal retval = 0;
            try
            {
                var ulocation = ul.Where(o => o.UserID == userid).OrderByDescending(o => o.ID).FirstOrDefault();
                if (ulocation != null)
                {
                    if (type == "lat")
                        retval = ulocation.Latitude;
                    else
                        retval = ulocation.Longitude;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }
    }
}