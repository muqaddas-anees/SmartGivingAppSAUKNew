using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class VideosConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindVideosData();
            }
        }

        protected void BindVideosData()
        {
            using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                var mainurl = Deffinity.systemdefaults.GetWebUrl();
                var section = ImageManager.file_section_video;
                var videos = context.Videos.ToList();
                foreach (var video in videos)
                {
                    string row = $@"
                        <tr>
                            <td>
                                <button type=""button"" class=""btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1 edit-btn""
                                    onclick=""openEditModal('{video.Title}','{video.URL}','{video.VideoOrder}','{video.Id}')"">
                                    <i class='bi bi-pencil fs-2'></i>
                                </button>
                            </td>
                            <td>
                                <div class='d-flex align-items-center'>
                                    <div class=''>
                                        <img src='{mainurl}/imagehandler.ashx?s={section}&id={video.VideoOrder}' alt='Avatar' class='img-flex mx-10' />
                                    </div>
                                    <div class='d-flex justify-content-start flex-column'>
                                        <a href='/WatchVideo/order/{video.VideoOrder}' class='text-gray-900 fw-bold text-hover-primary mb-1 fs-6'>{video.Title}</a>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <a href='{video.URL}' class='text-gray-900 fw-bold text-hover-primary d-block mb-1 fs-6'>{video.URL}</a>
                            </td>
                            <td class='text-muted fw-semibold'>{video.VideoOrder}</td>
                            <td class='text-end'>
                                <button type='button' onclick=""openEditModal('{video.Title}','{video.URL}','{video.VideoOrder}')"" class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm'>
                                    <i class='bi bi-trash fs-2'></i>
                                </button>
                            </td>
                        </tr>";

                    videoList.Controls.Add(new LiteralControl(row));
                }
            }
        }

        protected void videoForm_ServerSubmit(object sender, EventArgs e)
        {
            try
            {
                string thumbnailPath = "/assets/dummy.jpg";
                string title = videoTitle.Text;
                string url = videoURL.Text;

                //if (thumbnailUpload.HasFile)
                //{
                //    string fileName = Path.GetFileName(thumbnailUpload.PostedFile.FileName);
                //    string filePath = Server.MapPath("~/assets/thumbnails/") + fileName;
                //    thumbnailUpload.PostedFile.SaveAs(filePath);
                //    thumbnailPath = "/assets/thumbnails/" + fileName;


                //}
                int order = Convert.ToInt32(videoOrder.Text);
                using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    if (context.Videos.Any(v => v.URL == url))
                    {
                        Response.Write("<div class=\"alert alert-danger\" role=\"alert\">\r\n  Video with same URL already exists\r\n</div>");

                        BindVideosData();
                        return;
                    }
                    if (context.Videos.Any(v => v.VideoOrder == order))
                    {
                        Response.Write("<div class=\"alert alert-danger\" role=\"alert\">\r\n  Video with same Order already exists\r\n</div>");
                        BindVideosData();
                        return;
                    }

                    var video = new PortfolioMgt.Entity.Video();

                    video = new PortfolioMgt.Entity.Video
                    {
                        Title = title,
                        URL = url,
                        VideoOrder = order,
                        ThumbnailPath = ""
                    };
                    //Deffinity.systemdefaults.GetWebUrl()+ "/ImageHandler.ashx?s="+ImageManager.file_section_video+ "&id="+
                    context.Videos.InsertOnSubmit(video);
                    context.SubmitChanges();

                    try
                    {
                        if (thumbnailUpload.HasFile)
                        {
                            Bitmap upBmp = (Bitmap)Bitmap.FromStream(thumbnailUpload.PostedFile.InputStream);
                            ImageManager.SaveVideoImage(thumbnailUpload.FileBytes, order.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                            // DisplayLogo();
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                    BindVideosData();
                    ClearFormInputs();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                //Response.Write($"An error occurred: {ex.Message}<br/>");
                //Response.Write($"Stack Trace: {ex.StackTrace}<br/>");
            }
        }

        protected void ClearFormInputs()
        {
            videoOrder.Text = "";
            videoTitle.Text = "";
            videoURL.Text = "";
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string url = editVideoURL.Value.Trim();

            if (string.IsNullOrEmpty(url))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Video URL cannot be empty.');", true);
                return;
            }

            using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                var video = context.Videos.FirstOrDefault(v => v.URL == url);
                if (video != null)
                {
                    context.Videos.DeleteOnSubmit(video);
                    context.SubmitChanges();
                    BindVideosData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Video deleted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Video not found or failed to delete.');", true);
                }
            }
        }

        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string id = edittextid.Value;
                string title = editVideoTitle.Value;
                string url = editVideoURL.Value;
                int order = Convert.ToInt32(editVideoOrder.Value);

                using (var context = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    var video = context.Videos.FirstOrDefault(v => v.Id == Convert.ToInt32( id));
                    if (video != null)
                    {
                        video.Title = title;
                        video.VideoOrder = order;
                        video.URL = url;
                        context.SubmitChanges();

                        try
                        {
                            if (thumbnailUpload1.HasFile)
                            {
                                Bitmap upBmp = (Bitmap)Bitmap.FromStream(thumbnailUpload1.PostedFile.InputStream);
                                ImageManager.SaveVideoImage(thumbnailUpload1.FileBytes, order.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                                // DisplayLogo();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        Response.Redirect(Request.RawUrl, false);
                      //  BindVideosData();
                    }
                    else
                    {
                        LogExceptions.LogException("error");
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}
