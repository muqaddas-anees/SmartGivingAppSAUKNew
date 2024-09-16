using DC.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace DeffinityAppDev.App.Events
{
    public partial class BasicInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pnlURLs.Visible = false;
                    setMapKey();
                    IProjectRepository<ProjectMgt.Entity.ProjectDefault> pd = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                    if(pd.GetAll().FirstOrDefault() != null)
                    hkey.Value = pd.GetAll().FirstOrDefault().Chat_API_key;
                    BindCountry();
                    ddlCountry.SelectedValue = Deffinity.systemdefaults.GetCoutryID();// "37";
                    if (Request.QueryString["unid"] == null)
                    {
                        huid.Value = Guid.NewGuid().ToString();
                        cpStartDate.ValueToCompare = DateTime.Now.ToShortDateString();
                        cpEndDate.ValueToCompare = DateTime.Now.ToShortDateString();

                    }
                    else
                    {
                        huid.Value = Request.QueryString["unid"].ToString();
                        displaydata();
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void setMapKey()
        {
            try
            {
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> pd = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                if (pd.GetAll().FirstOrDefault() != null)
                    hkey.Value = pd.GetAll().FirstOrDefault().Chat_API_key;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "Country1";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", ""));
        }
        private void displaydata()
        {
            try
            {
                var uqid = huid.Value;
                var ActivityDetail = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (ActivityDetail != null)
                {
                    //compare validator values
                    cpStartDate.ValueToCompare = ActivityDetail.StartDateTime.ToShortDateString();
                    cpEndDate.ValueToCompare = ActivityDetail.EndDateTime.ToShortDateString();

                    txtEventName.Text = ActivityDetail.Title;
                   // txtDescription.Text = ActivityDetail.Description;

                    txtStartDate.Text = ActivityDetail.StartDateTime.ToShortDateString();
                    txtEndDate.Text = ActivityDetail.EndDateTime.ToShortDateString();
                    txtStartTime.Text = ActivityDetail.StartDateTime.ToString("hh:mm tt");
                    txtEndTime.Text = ActivityDetail.EndDateTime.ToString("hh:mm tt");



                    // txtBookingStartDate.Text = ActivityDetail.StartDateTime.ToShortDateString();
                    // txtBookingEndDate.Text = ActivityDetail.EndDateTime.ToShortDateString();
                    // txtBookingStartTime.Text = ActivityDetail.StartDateTime.ToShortTimeString().Substring(0, 5);
                    //  txtBookingEndTime.Text = ActivityDetail.EndDateTime.ToShortTimeString().Substring(0, 5);

                    //ckbIsActive.Checked = ActivityDetail.IsActive;
                    // txtSlot.Value = ActivityDetail.Slots.ToString();
                    // txtPrice.Value = string.Format("{0:F2}", ActivityDetail.Price);

                    txtvenuename.Value = ActivityDetail.Venue_Name;
                    txtAddress1.Value = ActivityDetail.Address1;
                    txtAddress2.Value = ActivityDetail.Address2;
                    txtCity.Value = ActivityDetail.City;
                    ddlCountry.SelectedValue= "1";
                    txtZipcode.Value = ActivityDetail.Postalcode;
                    txtState.Value = ActivityDetail.state_Province;
                    rdlTypeofEvent.SelectedValue = ActivityDetail.isInPerson??false ? "0" : "1";
                    txtInstagramLiveLink.Text = ActivityDetail.InstagramLink ?? "";
                    txtYouTubeLiveLink.Text = ActivityDetail.YouTubeLink ?? "";
                    txtTikTokLiveLink.Text = ActivityDetail.TikTokLink ?? "";
                    ddlCountry.SelectedValue = ActivityDetail.Country;

                    if(ActivityDetail.isInPerson ?? false)
                    {
                        pnlURLs.Visible = false;
                        pnlLocation.Visible = true;
                    }
                    else
                    {
                        pnlURLs.Visible = true;
                        pnlLocation.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public void saveData()
        {
            try
            {

                var uqid = huid.Value;

                var eEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (eEntity == null)
                {
                    eEntity = new PortfolioMgt.Entity.ActivityDetail();
                    eEntity.unid = uqid;
                    eEntity.OrganizationID = sessionKeys.PortfolioID;
                    eEntity.ActivityCategoryID = 0;
                    eEntity.ActivitySubCategoryID = 0;
                    eEntity.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : Convert.ToDateTime(txtStartTime.Text).ToShortTimeString()));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : Convert.ToDateTime(txtEndTime.Text).ToShortTimeString()));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                    //eEntity.StartDateTime = Convert.ToDateTime(txtBookingStartDate.Text + " " + (string.IsNullOrEmpty(txtBookingStartTime.Text) ? "00:00:00" : txtBookingStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    //eEntity.EndDateTime = Convert.ToDateTime(txtBookingEndDate.Text + " " + (string.IsNullOrEmpty(txtBookingEndTime.Text) ? "00:00:00" : txtBookingEndTime.Text + ":00"));
                    eEntity.Title = txtEventName.Text.Trim();
                    eEntity.Address1 = txtAddress1.Value.Trim();
                    eEntity.Address2 = txtAddress2.Value.Trim();
                    eEntity.City = txtCity.Value.Trim();
                    eEntity.Country = ddlCountry.SelectedItem.Text;
                    eEntity.state_Province = txtState.Value.Trim();
                    eEntity.Venue_Name = txtvenuename.Value.Trim();
                    eEntity.Postalcode = txtZipcode.Value.Trim();
                    eEntity.isInPerson = rdlTypeofEvent.SelectedValue == "0";
                    eEntity.YouTubeLink = txtYouTubeLiveLink.Text;
                    eEntity.TikTokLink = txtTikTokLiveLink.Text;
                    eEntity.InstagramLink = txtInstagramLiveLink.Text;

                    //eEntity.Description = txtDescription.Text;
                    // value.Notes = txtNotes.Text;
                    eEntity.ModifiedDate = DateTime.Now;
                    eEntity.IsActive = true;
                    eEntity.CreatedDate = DateTime.Now;
                    eEntity.LoggedBy = sessionKeys.UID;
                    eEntity.RefundPolicy = "There are no refunds, all sales are final.";
                    //eEntity.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Value.Trim()) ? txtSlot.Value.Trim() : "0");
                    //eEntity.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Value.Trim()) ? txtPrice.Value.Trim() : "0.00");
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Add(eEntity);

                    sessionKeys.Message = "Event added successfully";
                    //if (nav_Showspeckers)
                    //    Response.Redirect("~/App/Events/ManageSpeakers.aspx?unid=" + eEntity.unid, false);

                    //else
                    //    Response.Redirect("~/App/Events/EventList.aspx", false);

                    Response.Redirect("~/App/Events/BasicInfo.aspx?unid="+ eEntity.unid, false);

                }
                else
                {
                    eEntity.OrganizationID = sessionKeys.PortfolioID;
                    eEntity.ActivityCategoryID = 0;
                    eEntity.ActivitySubCategoryID = 0;
                    eEntity.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : Convert.ToDateTime( txtStartTime.Text).ToShortTimeString()));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : Convert.ToDateTime(txtEndTime.Text).ToShortTimeString()));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                    //eEntity.StartDateTime = Convert.ToDateTime(txtBookingStartDate.Text + " " + (string.IsNullOrEmpty(txtBookingStartTime.Text) ? "00:00:00" : txtBookingStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    //eEntity.EndDateTime = Convert.ToDateTime(txtBookingEndDate.Text + " " + (string.IsNullOrEmpty(txtBookingEndTime.Text) ? "00:00:00" : txtBookingEndTime.Text + ":00"));
                    eEntity.Title = txtEventName.Text.Trim();
                    eEntity.Address1 = txtAddress1.Value.Trim();
                    eEntity.Address2 = txtAddress2.Value.Trim();
                    eEntity.City = txtCity.Value.Trim();
                    eEntity.Country = ddlCountry.SelectedItem.Text;
                    eEntity.state_Province = txtState.Value.Trim();
                    eEntity.Venue_Name = txtvenuename.Value.Trim();
                    eEntity.Postalcode = txtZipcode.Value.Trim();
                    eEntity.isInPerson = rdlTypeofEvent.SelectedValue == "0";
                    eEntity.YouTubeLink = txtYouTubeLiveLink.Text;
                    eEntity.TikTokLink = txtTikTokLiveLink.Text;
                    eEntity.InstagramLink = txtInstagramLiveLink.Text;
                    // eEntity.Description = txtDescription.Text;
                    // value.Notes = txtNotes.Text;
                    eEntity.ModifiedDate = DateTime.Now;
                    eEntity.IsActive = true;
                    eEntity.CreatedDate = DateTime.Now;
                    eEntity.LoggedBy = sessionKeys.UID;
                    //eEntity.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Value.Trim()) ? txtSlot.Value.Trim() : "0");
                    //eEntity.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Value.Trim()) ? txtPrice.Value.Trim() : "0.00");
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eEntity);
                    sessionKeys.Message = "Event updated successfully";
                    Response.Redirect("~/App/Events/BasicInfo.aspx?unid=" + eEntity.unid, false);
                    //if (nav_Showspeckers)
                    //    Response.Redirect("~/App/Events/ManageSpeakers.aspx?unid=" + eEntity.unid, false);

                    //else
                    //    Response.Redirect("~/App/Events/EventList.aspx", false);
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveBasic_Click(object sender, EventArgs e)
        {
            saveData();
        }

        public string GetAllPincodesOfRequester()
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<MarkersData> MarkersDataList = new List<MarkersData>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                //IDCRespository<DC.Entity.CallDetail> dReporsitory = new DCRepository<DC.Entity.CallDetail>();
                //IDCRespository<DC.Entity.FLSDetail> fReporsitory = new DCRepository<DC.Entity.FLSDetail>();
                ////var sids = new int[] { 22, 43, 44 };
                //var dList = dReporsitory.GetAll().Where(o => o.ID == QueryStringValues.CallID).ToList();
                //var fList = fReporsitory.GetAll().Where(o => dList.Select(p => p.ID).Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                //IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                //IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paReporsitory = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                //var requesterList = pReporsitory.GetAll().Where(o => dList.Select(r => r.RequesterID).ToArray().Contains(o.ID)).ToList();
                //var requesterAddressList = paReporsitory.GetAll().Where(o => fList.Select(r => r.ContactAddressID).ToArray().Contains(o.ID)).ToList();
                //var getCCIDs = FLSDetailsBAL.GetCCIDS();
                Dictionary<string, object> row;
                //using (AssetsToSoftwareDataContext asset = new AssetsToSoftwareDataContext())
                //{
                List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass> LocationPinCodeResult = new List<DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass>();
                //if (sessionKeys.SID == 1 || sessionKeys.SID == 2)
                //LocationPinCodeResult = (from a in requesterList
                //                         join a1 in requesterAddressList on a.ID equals a1.ContactID
                //                         join b in dList on a.ID equals b.RequesterID
                //                         where b.ID == QueryStringValues.CallID
                //                         orderby b.ID descending
                //                         //where a.PortfolioID == sessionKeys.PortfolioID
                //                         select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                //                         {
                //                             LocationPinCode = string.IsNullOrEmpty(a1.PostCode) ? string.Empty : a1.PostCode,
                //                             address = "<br>" + "" + getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID + "<br>" + a.Name + "<br>" + a1.Address + "<br>" + a1.City + "<br>" + a1.State + "<br>" + a1.PostCode,
                //                             Id = getCCIDs.Where(o => o.CallID == b.ID).FirstOrDefault().CompanyCallID// a1.ID
                //                         }).Take(10).ToList();


                 var uqid = huid.Value;

                var eEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll().Where(o=>o.unid == uqid).ToList();


                LocationPinCodeResult = (from a in eEntity

                                         orderby a.ID descending
                                         //where a.PortfolioID == sessionKeys.PortfolioID
                                         select new DeffinityAppDev.WF.DC1.GMap1.LocationDisplayClass
                                         {
                                             address_toset = a.Venue_Name.Replace(" ", "+") +a.Address1.Replace(" ", "+") + a.Address2.Replace(" ", "+") + ","+ a.City.Replace(" ", "+") + ","+ a.state_Province.Replace(" ", "+") ,
                                             LocationPinCode = string.IsNullOrEmpty(a.Postalcode) ? string.Empty : a.Postalcode,
                                             address = "<br>" + "" + a.Venue_Name + "<br>" + a.Address1 + "<br>" + a.Address2 + "<br>" + a.City + "<br>" + a.state_Province + "<br>" + a.Postalcode,
                                             Id = a.ID// a1.ID
                                         }).Take(10).ToList();


                MarkersData MarkersDataSingle;

                foreach (var x in LocationPinCodeResult)
                {
                    MarkersDataSingle = new MarkersData();
                    MarkersDataSingle.title = x.LocationPinCode;
                    string LL = getLatLong(x.LocationPinCode,x.address_toset);
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

        public string getLatLong(string Zip,string address_toset)
        {
            string latlong = "",// address = "";

            //IDCRespository<DC.Entity.GeoCode> gRep = new DCRepository<DC.Entity.GeoCode>();
            //var gEntity = gRep.GetAll().Where(o=>o.Zip == Zip.Trim()).FirstOrDefault();
            //if(gEntity!= null)
            //{
            //    latlong = Zip + "," + Convert.ToString(gEntity.Latitude) + "," + Convert.ToString(gEntity.Longitude) + "," + "" + Zip;
            //}

            //address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=" + hkey.Value;
            address = "https://maps.googleapis.com/maps/api/geocode/xml?components=country:ZA|postal_code:" + Zip.Trim() + "|address:"+ address_toset + "&sensor=false&key=" + hkey.Value;
           // address = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + Zip.Trim() + "&sensor=false&key=" + hkey.Value;
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

        protected void rdlTypeofEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rdlTypeofEvent.SelectedValue=="0")
            {
                pnlURLs.Visible = false;
                pnlLocation.Visible = true;
            }
            else
            {
                pnlLocation.Visible = false;
                pnlURLs.Visible = true;
            }
        }
    }
}