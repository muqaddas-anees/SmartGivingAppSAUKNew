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

namespace DeffinityAppDev.WF.DC
{
    public partial class ShowMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblTitle.InnerText = "Smart tech Track - Job Reference " + QueryStringValues.CCID;
            }
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
        public string GetAllPincodesOfRequester()
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<MarkersData> MarkersDataList = new List<MarkersData>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
                IDCRespository<FLSDetail> fReporsitory = new DCRepository<FLSDetail>();
                IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
                IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();

                //IDCRespository<UserLocation> ulReporsitory = new DCRepository<UserLocation>();
                IPortfolioRepository<PortfolioMgt.Entity.UserTracking> ulReporsitory = new PortfolioRepository<PortfolioMgt.Entity.UserTracking>();
                IDCRespository<CallResourceSchedule> crReporsitory = new DCRepository<CallResourceSchedule>();
                var sids = new int[] { 22, 43, 44 };
                var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == sessionKeys.PortfolioID && o.ID == QueryStringValues.CallID).OrderByDescending(o => o.ID).ToList();
                var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                var crList = crReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                //var ulList = ulReporsitory.GetAll().Where(o => crList.Select(p => p.ResourceID).Contains(o.UserID)).ToList();


                var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany().Where(o=>o == (fList.FirstOrDefault().UserID.HasValue? fList.FirstOrDefault().UserID.Value:0)).ToList();
                // Result = Result.Where(o => uidlist.Contains(o.Uid)).OrderBy(a => a.Username).ToList();
                //ulist = urep.GetAll().Where(o => fList.Select(u => u.UserID).ToArray().Contains(o.ID)).ToList();
                int[] spids = { 1, 4 };
                var ulist = urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && spids.Contains(o.SID.Value) && o.Status == "Active").ToList();
                //var ulist = urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && o.SID == 4).ToList();
                var uaddresslist = udrep.GetAll().Where(o => uidlist.Contains(o.UserId.HasValue ? o.UserId.Value : 0)).ToList();

                var ulList = ulReporsitory.GetAll().Where(o => uidlist.Contains(o.UserID) && o.PortfolioID == sessionKeys.PortfolioID).ToList();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
                var requesterAddressList = paReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ContactID)).ToList();
                var getCCIDs = FLSDetailsBAL.GetCCIDS();
                Dictionary<string, object> row;
                //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
                //{
                List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass> LocationPinCodeResult = new List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass>();
                //if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
                //{
                    //LocationPinCodeResult = (from a in requesterList
                    //                         join a1 in requesterAddressList on a.ID equals a1.ContactID
                    //                         join b in dList on a.ID equals b.RequesterID

                    //                         orderby b.ID descending
                    //                         //where a.PortfolioID == sessionKeys.PortfolioID
                    //                         select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                    //                         {
                    //                             LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                    //                             address = "<br>" + "" + getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                    //                             name = a.Name,
                    //                             jobref = "" + getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID,
                    //                             Id = a1.ID,
                    //                             cid = a.ID
                    //                         }).Take(30).ToList();

                    var assignUserlist = (from a in ulist
                                          join a1 in uaddresslist on a.ID equals a1.UserId
                                          //join b in fList on a.ID equals b.UserID

                                          //orderby b.ID descending
                                          //where a.PortfolioID == sessionKeys.PortfolioID
                                          select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                          {
                                              lat = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : ""), "lat"),
                                              lng = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : "", "lng"),
                                              LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : ""),
                                              address = string.IsNullOrEmpty(ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : "") ? "<br>" + "Service Tech Detials:<br>" + a.ContractorName : "<br>" + "Service Tech Detials:<br>" + a.ContractorName + "<br>" + a1.Address1 + a1.Address2 + "<br>" + a1.Town + "<br>" + a1.PostCode,

                                              name = a.ContractorName,
                                              jobref = "",
                                              Id = a.ID,
                                              cid = a.ID
                                          }).Take(10).ToList();
                   // if (assignUserlist.Count > 0)
                        LocationPinCodeResult.AddRange(assignUserlist);

               // }

               
               

                MarkersData MarkersDataSingle;

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
    }
}