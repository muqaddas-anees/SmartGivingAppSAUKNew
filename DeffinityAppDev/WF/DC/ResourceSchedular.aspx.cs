using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using PortfolioMgt.DAL;
using System.Data;
using System.Reflection;
using System.Xml;
using DC.BLL;
using DC.SRV;
using DC.BAL;
using DC.Entity;

namespace DeffinityAppDev.WF.DC
{
    public partial class ResourceSchedular : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Response.Redirect("~/WF/DC/DashboardV2.aspx", false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleAccess(PortfolioModuleNames.DispatchBoard))
                    {
                        if (sessionKeys.SID == 1)
                        {
                            Response.Redirect("~/WF/DC/FLSJlist.aspx?type=FLS", false);
                            return;
                        }
                    }

                    if (Session["msg"] != null)
                    {
                        lblmsg.Text = Session["msg"].ToString();
                        Session["msg"] = null;
                    }

                    
                    DC_controls_FLSListCtrl.AddDefaultFields();
                    //check for default data

                    DC_controls_FLSListCtrl.InsertDefaultData();
                    DC_controls_FLSListCtrl.InsertDefaultColumn();


                    BindTown();
                    BindPostcode();
                    SetPopuplablevalues();
                    BindTypeDropdown();

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindStatusDropdown()
        {
            IDCRespository<TypeOfRequest> dRep = new DCRepository<TypeOfRequest>();
            ddlStatus.DataSource = dRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o => o.Name).ToList();
            ddltype.DataTextField = "Name";
            ddltype.DataValueField = "ID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("All", "0"));
        }
        public void BindTypeDropdown()
        {
            IDCRespository<TypeOfRequest> dRep = new DCRepository<TypeOfRequest>();
            ddltype.DataSource = dRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o => o.Name).ToList();
            ddltype.DataTextField = "Name";
            ddltype.DataValueField = "ID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("All", "0"));
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
                IDCRespository<CallDetail> dReporsitory = new DCRepository<CallDetail>();
                IDCRespository<FLSDetail> fReporsitory = new DCRepository<FLSDetail>();
                IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
                IUserRepository<UserMgt.Entity.UserDetail> udrep = new UserRepository<UserMgt.Entity.UserDetail>();

                //IDCRespository<UserLocation> ulReporsitory = new DCRepository<UserLocation>();
                IPortfolioRepository<PortfolioMgt.Entity.UserTracking> ulReporsitory = new PortfolioRepository<PortfolioMgt.Entity.UserTracking>();
                IDCRespository<CallResourceSchedule> crReporsitory = new DCRepository<CallResourceSchedule>();
                //22	New
//                33  Cancelled
//35  Closed
//43  Scheduled
//44  Awaiting Schedule
//45  Arrived
//46  Customer Not Responding
//47  Feedback Submitted
//48  Feedback Received
//49  Quote Rejected
//50  Quote Accepted
//51  Awaiting Information
//52  Waiting On Parts
//53  Authorised
//54  Request Feeback
                var sids = new int[] { 22, 43, 44, 45, 46, 47, 48,49, 50, 51,52,53, 54 };
                var dList = dReporsitory.GetAll().Where(o => sids.Contains(o.StatusID.Value) && o.CompanyID == sessionKeys.PortfolioID).OrderByDescending(o=>o.ID).ToList();
                var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                var crList = crReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                //var ulList = ulReporsitory.GetAll().Where(o => crList.Select(p => p.ResourceID).Contains(o.UserID)).ToList();
               

                var uidlist = UserMgt.BAL.ContractorsBAL.GetUserListByCompany();
                // Result = Result.Where(o => uidlist.Contains(o.Uid)).OrderBy(a => a.Username).ToList();
                //ulist = urep.GetAll().Where(o => fList.Select(u => u.UserID).ToArray().Contains(o.ID)).ToList();
                int[] spids = { 1, 4 };
                var ulist = urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && spids.Contains(o.SID.Value) && o.Status == "Active").ToList(); 
                //var ulist = urep.GetAll().Where(o => uidlist.Select(u => u).ToArray().Contains(o.ID) && o.SID == 4).ToList();
                var uaddresslist = udrep.GetAll().Where(o => uidlist.Contains(o.UserId.HasValue?o.UserId.Value:0)).ToList();
                
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
                if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
                {
                    LocationPinCodeResult = (from a in requesterList
                                             join a1 in requesterAddressList on a.ID equals a1.ContactID
                                             //join b in dList on a.ID equals b.RequesterID
                                             join f in fList on a1.ID equals f.ContactAddressID

                                             orderby a.ID descending
                                             //where a.PortfolioID == sessionKeys.PortfolioID
                                             select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                             {
                                                 LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                                                 address = "<br>" + "" + getCCIDs.Where(o => o.CallID == f.CallID).FirstOrDefault().CompanyCallID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                                                 name = a.Name,
                                                 jobref = "" + getCCIDs.Where(o=>o.CallID == f.CallID).FirstOrDefault().CompanyCallID,
                                                 Id = a1.ID, cid=a.ID
                                             }).Take(30).ToList();

                    var assignUserlist = (from a in ulist
                                          join a1 in uaddresslist on a.ID equals a1.UserId
                                          //join b in fList on a.ID equals b.UserID

                                          //orderby b.ID descending
                                          //where a.PortfolioID == sessionKeys.PortfolioID
                                          select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                          {
                                              lat = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o=>o.UserID == a1.UserId).Count() ==0? a1.PostCode:""),"lat"),
                                              lng = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : "", "lng"),
                                              LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : ""),
                                              address = string.IsNullOrEmpty(ulList.Where(o => o.UserID == a1.UserId).Count() == 0 ? a1.PostCode : "") ? "<br>" + "Service Tech Detials:<br>" + a.ContractorName : "<br>" + "Service Tech Detials:<br>" + a.ContractorName + "<br>" + a1.Address1 + a1.Address2 + "<br>" + a1.Town + "<br>" + a1.PostCode,
                                              
                                              name = a.ContractorName,
                                              jobref = "",
                                              Id = a.ID, cid=a.ID
                                          }).Take(30).ToList();
                    if (assignUserlist.Count >0)
                    LocationPinCodeResult.AddRange(assignUserlist);

                }

                else if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                    LocationPinCodeResult = (from a in requesterList
                                             join a1 in requesterAddressList on a.ID equals a1.ContactID
                                             join b in dList on a.ID equals b.RequesterID
                                             join f in fList on b.ID equals f.CallID
                                             where f.UserID == sessionKeys.UID
                                             orderby b.ID descending

                                             //where a.PortfolioID == sessionKeys.PortfolioID
                                             select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                             {
                                                 lat = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o => o.UserID == f.UserID).Count() == 0 ? a1.PostCode : ""), "lat"),
                                                 lng = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : ulList.Where(o => o.UserID == f.UserID).Count() == 0 ? a1.PostCode : "", "lng"),

                                                 //lat = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode, "lat"),
                                                 //lng = GetCurrentLocation(ulList, a.ID, string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode, "lng"),
                                                 LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : (ulList.Where(o => o.UserID == f.UserID).Count() == 0 ? a1.PostCode : ""),
                                                 address = "<br>" + "" + getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                                                 name = a.Name,
                                                 jobref = ""+ getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID,
                                                 Id = a1.ID,cid= a.ID
                                             }).Take(40).ToList();

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
        public decimal GetCurrentLocation(List<PortfolioMgt.Entity.UserTracking> ul,int userid,string postcode,string type)
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
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }
        private void BindTown()
        {
            try
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var result = (from p in pd.PortfolioContacts
                                  where p.PortfolioID == sessionKeys.PortfolioID && p.Town != null
                                  orderby p.Town
                                  select new { value = p.Town, name = p.Town }).ToList();
                    chkTowns.DataSource = result.Where(o=>o.value.Length >0).ToList();
                    chkTowns.DataValueField = "value";
                    chkTowns.DataTextField = "name";
                    chkTowns.DataBind();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindPostcode()
        {
            try
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var result = (from p in pd.PortfolioContacts
                                  where p.PortfolioID == sessionKeys.PortfolioID && p.Postcode != null
                                  orderby p.Postcode
                                  select new { value = p.Postcode, name = p.Postcode }).ToList();
                    chkPostcode.DataSource = result.Where(o=>o.value.Length >0).ToList();
                    chkPostcode.DataValueField = "value";
                    chkPostcode.DataTextField = "name";
                    chkPostcode.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnEditDate_Click(object sender, EventArgs e)
        {
            try
            {

                if (Hrefid.Value.Length > 0)
                {
                    lblJobRef.Text = Hrefid.Value;
                    var id = Hrefid.Value;
                    var cid = FLSDetailsBAL.GetCallID(Convert.ToInt32(id));
                    FLSDetail fd = FLSDetailsBAL.SelectbyId(Convert.ToInt32(cid));
                    if (fd != null)
                    {
                        txtScheduledEndDateTime1.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                        txtScheduledEndTime.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat());
                        txtSeheduledDateTime1.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.ScheduledDate);
                        txtScheduledTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), fd.ScheduledDate);

                        mdlExnter.Show();
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }
        protected void btnUpdateDates_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblJobRef.Text.Length > 0)
                {
                    var id = lblJobRef.Text.Replace("TN:", "");
                    var cid = FLSDetailsBAL.GetCallID(Convert.ToInt32(id));

                    FLSDetail fd = FLSDetailsBAL.SelectbyId(Convert.ToInt32(cid));

                    if (fd != null)
                    {
                        fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
                        fd.ScheduledEndDateTime = Convert.ToDateTime(txtScheduledEndDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
                        FLSDetailsBAL.FLSDetailsUpdate(fd);

                        try
                        {
                            IDCRespository<CallResourceSchedule> aRes = new DCRepository<CallResourceSchedule>();
                            var aList = aRes.GetAll().Where(o => o.CallID == Convert.ToInt32(cid) && o.IsAssigned == true).ToList();
                            if (aList.Count > 0)
                            {
                                foreach (var a in aList)
                                {
                                    a.StartDate = Convert.ToDateTime(txtSeheduledDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
                                    a.EndDate = Convert.ToDateTime(txtScheduledEndDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
                                    aRes.Edit(a);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                        Session["msg"] = Resources.DeffinityRes.UpdatedSuccessfully;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SetPopuplablevalues()
        {
            try
            {
                var fieldsList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).OrderBy(g => g.Position).ToList();

                if (fieldsList.Count > 0)
                {
                    WebService wb = new WebService();
                    foreach (var item in fieldsList)
                    {
                        if (item.DefaultName.ToLower().Contains("scheduled date/time"))
                        {
                            lblScheduledDateTime.Text = item.InstanceName;
                        }

                        else if (item.DefaultName.ToLower().Contains("scheduled end date/time"))
                        {
                            lblScheduledEndDateTime.Text = item.InstanceName;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}