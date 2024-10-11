using Microsoft.Owin;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class LiveEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                // var tRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var tList = cRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if (tList != null)
                    sessionKeys.PortfolioID = tList.OrganizationID;
                SetLiveStreamData();
                BindSpeakerDetails();



            }
        }
        private void BindSpeakerDetails()
        {


            var uEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(QueryStringValues.UNID);

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.Speaker_Detail>();


            var list = cRep.GetAll().Where(o => o.Event_ID == uEntity.ID).ToList();
            string speakerHtml = "";
            foreach(var speaker in list)
            {
                speakerHtml += $@" <div class=""row mb-3 no-gutters"">
    <div class=""col-md-4"">
      <img src=""{GetImageUrl(speaker.Speaker_ID)}"" class=""img-fluid rounded Speakerimg"" alt=""Speaker Image"">
    </div>
    <div class=""col-md-8"">
      <div class="""">
        <h5 class=""card-title video-title"">{speaker.Speaker_Name}</h5>
<div class=""video-description"">
        <p class=>{speaker.Speaker_Bio}</p></div>
      </div>

    </div>
  </div>";
            }

            speakers.Text = speakerHtml;

          



        }

        protected static string GetImageUrl(object contactsId)
        {


            return "/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_speaker;

        }
        public void SetLiveStreamData()
        {
            using (var context = new PortfolioDataContext())
            {
                var ev = context.ActivityDetails.FirstOrDefault(o => o.unid == QueryStringValues.UNID);
                if (ev != null && (!ev.isInPerson ?? false))
                {
                    // Check if YouTube link is available
                    var youtubeLiveLink = ev.YouTubeLink ?? "";

                    // Embed YouTube only if the link exists
                    if (!string.IsNullOrEmpty(youtubeLiveLink))
                    {
                        // Create YouTube embed HTML code
                        var embedHtml = $@"
    <div class='w-500 yt-video' >
        <iframe 
            src='{(ev.YouTubeLink.StartsWith("https://") ? ev.YouTubeLink : "https://" + ev.YouTubeLink)
    .Replace("watch?v=", "embed/")
    .Replace("live", "embed/")}' 
            style='position: absolute; top: 0; left: 0; width: 100%; height: 100%;'
            frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' 
            allowfullscreen>
        </iframe>
    </div>";

                        // Insert the generated HTML into the Literal control
                        ytLiveStream.Text = embedHtml;

                        var descHTML = $@"        <div class=""video-title"">
    {ev.Title}
</div>
<div class=""video-description"">
   {ev.Description}

</div>";
                        videoDesc.Text = descHTML;
                    }
                }
            }
        }

    }
}