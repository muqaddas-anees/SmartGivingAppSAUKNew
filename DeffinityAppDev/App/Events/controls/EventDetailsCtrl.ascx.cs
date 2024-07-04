using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DeffinityAppDev.App.Events.controls
{
    public partial class EventDetailsCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["unid"] != null)
                    {
                        var id = Request.QueryString["unid"].ToString();



                        //  IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();


                        // var ActivityDetail = pRep.GetAll().Where(o => o.ID == Aid).FirstOrDefault();


                        BindSpeakerDetails(id);


                        BindSpeakerGridDetails(id);
                        BingSponsorsGrid();
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void BindSpeakerDetails(string eid)
        {
            try
            {
                var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var tList = tRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).ToList();


                var list = cRep.GetAll().Where(o => o.unid == eid).ToList();

                var rlist = (from r in list
                             select new
                             {
                                 r.ActivityCategoryID,
                                 r.ActivitySubCategoryID,
                                 r.Address1,
                                 r.Address2,
                                 r.BookingEndDate,
                                 r.BookingStartDate,
                                 r.City,
                                 r.Country,
                                 r.CreatedDate,
                                 r.Description,
                                 r.EndDateTime,
                                 r.Event_Capacity,
                                 r.Event_Cost,
                                 r.Event_Type,
                                 r.ID,
                                 r.ImageID,
                                 r.IsActive,
                                 r.IsPublicEvent,
                                 r.KeyWords,
                                 r.LoggedBy,
                                 r.ModifiedDate,
                                 r.Notes,
                                 r.OrganizationID,
                                 r.Postalcode,
                                 Price = GetPrices(tList.Where(o => o.unid == r.unid).ToList()),
                                 r.PublishDate,
                                 r.Publisher,
                                 r.QRcode,
                                 r.RefundPolicy,
                                 r.Slots,
                                 r.SocialDescription,
                                 r.StartDateTime,
                                 r.state_Province,
                                 r.SupplierID,
                                 r.Title,
                                 r.unid,
                                 r.VenueLocation_Link,
                                 r.Venue_Name,
                                 r.Venue_Type,


                             }).ToList();

                ListEventDetails.DataSource = rlist;
                ListEventDetails.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }




        }

        private string GetPrices(List<PortfolioMgt.Entity.ActivityTicketSetting> tList)
        {
            string retval = string.Empty;
            var t = tList.OrderBy(o => o.Price).FirstOrDefault();
            if (t != null)
            {
                if (t.Price == 0.00)
                {
                    retval = "Free";
                }
                else
                {
                    if (retval.Length > 0)
                    {
                        retval = retval + " - " + string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                    }
                    else
                    {
                        retval = string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                    }
                }
            }

            if (tList.Count > 1)
            {
                t = tList.OrderBy(o => o.Price).LastOrDefault();
                if (t != null)
                {
                    if (t.Price == 0.00)
                    {
                        retval = retval = retval + " - " + "Free";
                    }
                    else
                    {
                        if (retval.Length > 0)
                        {
                            retval = retval + " - " + string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                        }
                        else
                        {
                            retval = string.Format("{1}{0:N2}", t.Price, Deffinity.Utility.GetCurrencySymbol()); ;
                        }
                    }
                }
            }



            return retval;
        }


        public void BingSponsorsGrid()
        {
            try
            {


                //var cRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();


                //var value = cRep.GetAll().Where(o => o.SponsorId >=0).FirstOrDefault();
                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> aRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var eventDetils = aRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

                IPortfolioRepository<PortfolioMgt.Entity.SponsorTable> pRep = new PortfolioRepository<PortfolioMgt.Entity.SponsorTable>();

                var Value = pRep.GetAll().Where(o => o.EventID == eventDetils.ID).ToList();


                gridSponsors.DataSource = Value;
                gridSponsors.DataBind();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void BindSpeakerGridDetails(string unid)
        {


            var uEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(unid);

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


            var list = cRep.GetAll().Where(o => o.Event_ID == uEntity.ID).ToList();
            //hEventid.Value = uEntity.ID.ToString();


            BannerList.DataSource = list;
            BannerList.DataBind();



        }
        protected string getPrice(string price)
        {
            if (Convert.ToDouble(price) > 0)
            {
                return string.Format("{0:F2}", Convert.ToDouble(price));
            }
            else
            {
                return "Free";
            }
        }

        protected static string GetAddress(object description)
        {
            string retval = "";
            if (description != null)
            {
                if (description.ToString().Length > 500)
                {
                    retval = description.ToString().Substring(0, 490) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        protected static string GetAddress(object Venue_Name, object address1, object address2, object city, object state, object postcode, object country)
        {
            string retval = "";

            if (Venue_Name != null)
                retval = " " + retval + Venue_Name.ToString() + "<br>";

            if (address1 != null)
                retval = " " + retval + address1.ToString();

            if (address2 != null)
                retval = " " + retval + address2.ToString() + "<br>";

            if (city != null)
                retval = " " + retval + city.ToString();

            if (state != null)
                retval = " " + retval + state.ToString() + "<br>";

            if (postcode != null)
                retval = " " + retval + postcode.ToString() + "<br>";

            if (country != null)
                retval = " " + retval + country.ToString();



            return retval;
        }
        //protected static string GetImageUrl(string contactsId)
        //{
        //    //return GetImageUrl(a_gId, a_oThumbSize, true);
        //    bool isOriginal = false;

        //    string img = string.Empty;

        //    string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + contactsId;

        //    if (System.IO.File.Exists(filepath))
        //    {
        //        if (isOriginal)
        //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
        //        else
        //            img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
        //        // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
        //        //img = string.Format("<img src='{0}' />", imgurl);
        //    }
        //    else
        //    {
        //        img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
        //        //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
        //    }
        //    return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
        //    // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        //}


        protected static string GetSponsorImageUrl(object contactsId)
        {
            return "~/ImageHandler.ashx?id=" + contactsId.ToString() + "&s=" + ImageManager.file_section_spnsor;
        }
        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Speakers_Imgages/") + contactsId;

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
            //    else
            //        img = string.Format("~/WF/UploadData/Speakers_Imgages/{0}", contactsId);
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
            //    //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            return "~/ImageHandler.ashx?id=" + contactsId.ToString() + "&s=" + ImageManager.file_section_speaker; //
        }



        protected string GetImmage(object activityid)
        {
            //string retval = string.Empty;
            //if (activityid != null)
            //{
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
            //    {
            //        //retval = "../../WF/UploadData/Events/" + activityid + "/0.png";
            //        retval = "~/WF/UploadData/Events/" + activityid + "/0.png?v="+DateTime.Now.Ticks;
            //    }
            //    else
            //    {

            //        retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
            //}
            //else
            //    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //return retval;
            return "~/ImageHandler.ashx?id=" + activityid + "&s=" + ImageManager.file_section_event; //
        }

        protected string GetImmageString(string activityid)
        {
            string retval = string.Empty;
            if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid + "/0.png")))
            {
                retval = "<div class='bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px' style='background-image:url('" + "../../WF/UploadData/Events/" + activityid + "/0.png"+ "?v=" + DateTime.Now.Ticks + "')' ></div> ";
            }
            else
            {

                retval = "";
            }
            return retval;

        }



    

        protected void btnBookTickets_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["unid"] != null)
            {
                var id = Request.QueryString["unid"].ToString();

                Response.Redirect("~/ManageTicketsNew.aspx?unid=" + id, false);
            }
        }
    }





}