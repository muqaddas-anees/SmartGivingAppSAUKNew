using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev.App.controls
{
    public partial class VideoCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadVideos();
           
            }
        }
        private string CreateVideoHtml(dynamic video)
        {
            var thumbnailUrl = !string.IsNullOrEmpty(video.ThumbnailUrl)
                ? video.ThumbnailUrl
                : $"https://img.youtube.com/vi/{ExtractVideoId(video.VideoUrl)}/hqdefault.jpg";

            return $@"
    <div class='column'>
        <div class='video-item'>
            <a href='{video.VideoUrl}' class='video-link' data-video-url='{video.VideoUrl}'>
                <img src='{thumbnailUrl}' alt='Video Thumbnail' class='video-thumbnail' />
                <div class='play-button'></div>
            </a>
            <h1 class='video-description'>{video.VideoDescription}</h1>
        </div>
    </div>";
        }


        private bool WantsVideos()
        {

            using (var context = new UserDataContext())
            {
                var user = context.Contractors.FirstOrDefault(o => o.ID == sessionKeys.UID);
                if (user != null)
                {
                    return user.WantsTutorial ?? true; // Return true if WantsTutorial is null
                }
            }
            return true; // Default to true if the user is not found
        }
        private string GetThumbnailUrl(int? videoOrder)
        {
            using (var context = new PortfolioDataContext())
            {
                var fileData = context.FileDatas
                    .FirstOrDefault(f => f.FileID == videoOrder.ToString() && f.Section == "video");
                if (fileData != null)
                {
                    return "/imagehandler.ashx?id="+videoOrder+"&s=video"; // Adjust based on your actual data model
                }
            }
            return null; // No custom thumbnail found
        }

        private void LoadVideos()
        {
            if (WantsVideos())
            {
                using (var VideosContext = new PortfolioDataContext())
                {
                    var videos = VideosContext.Videos
                        .OrderBy(v => v.VideoOrder)
                        .Select(v => new
                        {
                            VideoUrl = TransformToEmbedUrl(v.URL),
                            VideoDescription = v.Title,
                            ThumbnailUrl = GetThumbnailUrl(v.VideoOrder) // Fetch thumbnail URL
                        })
                        .ToList();

                    var videoContainerHtml = new System.Text.StringBuilder();

                    for (int i = 0; i < videos.Count; i += 2)
                    {
                        videoContainerHtml.Append("<div class='video-div'>");

                        videoContainerHtml.Append(CreateVideoHtml(videos[i]));
                        if (i + 1 < videos.Count)
                        {
                            videoContainerHtml.Append(CreateVideoHtml(videos[i + 1]));
                        }

                        videoContainerHtml.Append("</div>");
                    }

                    VideoContainer.Controls.Add(new Literal { Text = videoContainerHtml.ToString() });
                }

                chkHideTab.Checked = false;
            }
            else
            {
                chkHideTab.Checked = true;

                if (!Request.Url.Query.Contains("type="))
                {
                    string currentUrl = Request.Url.AbsoluteUri;
                    Response.Redirect(string.Format("{0}?type=2", currentUrl));
                }
            }
        }



        // Method to transform YouTube URL to embed format
        private string TransformToEmbedUrl(string url)
        {
            var videoId = ExtractVideoId(url);
            return $"https://www.youtube.com/embed/{videoId}";
        }

        // Method to extract the video ID from a YouTube URL
        public static string ExtractVideoId(string url)
        {
            var regex = new Regex(@"(?:youtube\.com\/(?:[^\/\n\s]+\/\s*[^\/\n\s]+\/|(?:v|e(?:mbed)?)\/|\S*?watch(?:\.php)?\?v=|watch\?.+&v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})");
            Match match = regex.Match(url);
            return match.Success ? match.Groups[1].Value : null;
        }

        private void CheckUserPreference()
        {
            // Check user preference from database or session
            bool hideTab = GetUserPreference();

            if (hideTab)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void chkHideTab_CheckedChanged(object sender, EventArgs e)
        {
            bool hideTab = !chkHideTab.Checked;

            using (var context = new UserDataContext())
            {
                var user = context.Contractors.FirstOrDefault(o => o.ID == sessionKeys.UID);
                if (user != null)
                {
                    user.WantsTutorial = hideTab;
                    context.SubmitChanges();
                }
            }
            LoadVideos();
        }


        private bool GetUserPreference()
        {
            // Retrieve user preference from database or session
            // For demonstration, always return false
            return false;
        }

        private void SaveUserPreference(bool hideTab)
        {
            // Save user preference to database or session
            // Implementation depends on your storage method (e.g., database, session state)
        }
    }
}
