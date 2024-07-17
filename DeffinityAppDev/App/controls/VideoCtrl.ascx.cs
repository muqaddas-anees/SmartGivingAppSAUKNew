using Stripe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class VideoCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string videoOrderStr = Page.RouteData.Values["videoorder"] as string;
                //if (videoOrderStr == null)
                //    videoOrderStr = "1";

                //if (!string.IsNullOrEmpty(videoOrderStr) && int.TryParse(videoOrderStr, out int videoOrder))
                //{
                //    //LoadVideoDetails(videoOrder);
                //}
                //else
                //{
                //    Response.Write("Invalid video order: " + videoOrderStr);
                //}

                GenerateVideoThumbnails();
            }
        }

        private void GenerateVideoThumbnails()
        {
            StringBuilder html = new StringBuilder();
            var weburl = Deffinity.systemdefaults.GetWebUrl();
            using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                var vlist = context.Videos.ToList().OrderBy(o => o.VideoOrder).ToList();

             

                foreach (var v in vlist)
                {
                    var vid = ExtractVideoId(v.URL);
                    
                    //string thumbnailUrl = $"https://img.youtube.com/vi/{vid}/0.jpg";
                    string thumbnailUrl = $"{weburl}/imagehandler.ashx?s=video&id={v.VideoOrder}";
                    string youtubeLink = $"https://www.youtube.com/watch?v={vid}";

                    html.AppendFormat(@"<div class='col-3' data-fslightbox='gallery'>
                                    <a href='{0}' data-type='youtube' data-title='{2}' class='video-thumbnail'>
                                        <img src='{1}' class='img-fluid mx-3' alt='{2}'>
                                        <span class='play-icon bi bi-play-fill'></span>
                                    </a>
                                </div>", youtubeLink, thumbnailUrl, v.Title);
                }

                videoList.InnerHtml = html.ToString();


        //        var videos = new[]
        //    {
        //    new { Id = "dQw4w9WgXcQ", Title = "Rick Astley - Never Gonna Give You Up" },
        //    new { Id = "kJQP7kiw5Fk", Title = "Luis Fonsi - Despacito ft. Daddy Yankee" },
        //    // Add more videos here
        //    new { Id = "VIDEO_ID3", Title = "Video Title 3" },
        //    new { Id = "VIDEO_ID4", Title = "Video Title 4" },
        //    new { Id = "VIDEO_ID5", Title = "Video Title 5" },
        //    new { Id = "VIDEO_ID6", Title = "Video Title 6" },
        //    new { Id = "VIDEO_ID7", Title = "Video Title 7" },
        //    new { Id = "VIDEO_ID8", Title = "Video Title 8" },
        //    new { Id = "VIDEO_ID9", Title = "Video Title 9" },
        //    new { Id = "VIDEO_ID10", Title = "Video Title 10" }
        //};

               
               
            }
        }

        public static string ExtractVideoId(string url)
        {
            var regex = new Regex(@"(?:youtube\.com\/(?:[^\/\n\s]+\/\s*[^\/\n\s]+\/|(?:v|e(?:mbed)?)\/|\S*?watch(?:\.php)?\?v=|watch\?.+&v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})");
            Match match = regex.Match(url);

            return match.Success ? match.Groups[1].Value : null;
        }
        //private void LoadVideoDetails(int videoOrder)
        //{
        //    using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
        //    {
        //        var video = context.Videos.FirstOrDefault(v => v.VideoOrder == videoOrder);
        //        if (video != null)
        //        {
        //            videoTitle.InnerText = video.Title;
        //            videoSource.Attributes["src"] = video.URL;

        //            // Pass video URL to load steps
        //            LoadSteps(video.URL);
        //        }
        //        else
        //        {
        //            Response.Write("Video not found.");
        //        }

        //        LoadRelatedVideos(context, videoOrder);
        //    }
        //}

        private void LoadRelatedVideos(PortfolioMgt.DAL.PortfolioDataContext context, int currentOrder)
        {
            var mainurl = Deffinity.systemdefaults.GetWebUrl();
            var section = ImageManager.file_section_video;

            var prevVideo = context.Videos
                .Where(v => v.VideoOrder < currentOrder)
                .OrderByDescending(v => v.VideoOrder)
                .FirstOrDefault();

            var nextVideo = context.Videos
                .Where(v => v.VideoOrder > currentOrder)
                .OrderBy(v => v.VideoOrder)
                .FirstOrDefault();

            if (prevVideo != null)
            {
                string prevVideoHtml = $"<a href='/WatchVideo/order/{prevVideo.VideoOrder}'><img src='{prevVideo.Id}' alt='Previous Video'><div>{prevVideo.Title}</div></a>";
              //  PrevVideo.InnerHtml = prevVideoHtml;
            }

            if (nextVideo != null)
            {
                string nextVideoHtml = $"<a href='/WatchVideo/order/{nextVideo.VideoOrder}'><img src='{nextVideo.Id}' alt='Next Video'><div>{nextVideo.Title}</div></a>";
               // NextVideo.InnerHtml = nextVideoHtml;
            }
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {

        }

        //private void LoadSteps(string videoUrl)
        //{
        //    using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
        //    {
        //        var steps = context.Steps
        //            .Where(s => s.videourl == videoUrl)
        //            .OrderBy(s => s.step_number)
        //            .ToList();

        //        rptSteps.DataSource = steps;
        //        rptSteps.DataBind();
        //    }
        //}
    }
}