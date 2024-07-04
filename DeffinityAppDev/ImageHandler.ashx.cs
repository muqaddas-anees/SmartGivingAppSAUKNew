using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DeffinityAppDev
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            // Retrieve the image ID from the query string
            string imageId = "";
            string section = "";
            string type = "";
            if (context.Request.QueryString["id"]!= null)
            imageId = context.Request.QueryString["id"];
            if(context.Request.QueryString["s"]!= null)
             section = context.Request.QueryString["s"];
            if (context.Request.QueryString["t"] != null)
                type = context.Request.QueryString["t"];

            string ctype = "";
            string filename = "";
            // Retrieve the image data from the database based on the image ID
            byte[] imageData = GetImageDataFromDatabase(imageId, section,type, out ctype,out filename);

            // Set the appropriate content type for the response,
            if (ctype.Length > 0)
            {
                context.Response.ContentType = ctype;
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

            }
            //"application/octet-stream";
            else
                context.Response.ContentType = "image/png";

            // Write the image data to the response output stream
            //Encoding.UTF8.GetString(record.Data.ToArray());
            context.Response.BinaryWrite(imageData);
        }

        private byte[] GetImageDataFromDatabase(string imageId, string section,string type, out string ctype,out string filename)
        {
            byte[] imageData = null;
            ctype = "";
            filename = "";
            IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            var f = frep.GetAll().Where(o => o.FileID == imageId && o.Section == section).FirstOrDefault();
            if (f != null)
            {
                imageData = f.FileData1.ToArray();
                ctype = f.FileType ?? "";
                filename = f.FileName ?? "";
            }
            else
            {
                if (section == ImageManager.file_section_fundriser_top)
                    f = frep.GetAll().Where(o => o.FileID == imageId && o.Section == ImageManager.file_section_fundriser).FirstOrDefault();
                else if (section == ImageManager.file_section_portfolio)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == section).FirstOrDefault();
                else if (section == ImageManager.file_section_landing)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_landing).FirstOrDefault();
                else if (section == ImageManager.file_section_user)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == section).FirstOrDefault();
                else if (section == ImageManager.file_section_spnsor)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_user).FirstOrDefault();
                else if (section == ImageManager.file_section_speaker)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_user).FirstOrDefault();
                else if (section == ImageManager.file_section_banner)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_banner).FirstOrDefault();
                else if (section == ImageManager.file_section_landing_top)
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_landing_top).FirstOrDefault();

                else
                    f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_event).FirstOrDefault();

                if (f != null)
                {
                    if (type.Length > 0)
                        {
                        imageData = f.FileData2.ToArray();
                    }
                    else
                    imageData = f.FileData1.ToArray();
                }
            }

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    string query = "SELECT ImageData FROM Images WHERE ImageID = @ImageID";
            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@ImageID", imageId);
            //        imageData = (byte[])command.ExecuteScalar();
            //    }
            //}

            return imageData;
        }

        //private byte[] GetImageDataFromDatabase(string imageId,string section)
        //{
        //    byte[] imageData = null;


        //    IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

        //    var f = frep.GetAll().Where(o => o.FileID == imageId && o.Section== section).FirstOrDefault();
        //    if(f != null)
        //    {
        //        imageData = f.FileData1.ToArray();
        //    }
        //    else
        //    {
        //        if (section == ImageManager.file_section_fundriser_top)
        //            f = frep.GetAll().Where(o => o.FileID == imageId && o.Section == ImageManager.file_section_fundriser).FirstOrDefault();
        //        else if (section == ImageManager.file_section_portfolio)
        //            f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == section).FirstOrDefault();

        //        else if (section == ImageManager.file_section_user)
        //            f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == section).FirstOrDefault();
        //        else if (section == ImageManager.file_section_spnsor)
        //            f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_user).FirstOrDefault();
        //        else if (section == ImageManager.file_section_speaker)
        //            f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_user).FirstOrDefault();

        //        else 
        //            f = frep.GetAll().Where(o => o.FileID == "0" && o.Section == ImageManager.file_section_event).FirstOrDefault();

        //        if(f != null)
        //        {
        //            imageData = f.FileData1.ToArray();
        //        }
        //    }

        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    connection.Open();
        //    //    string query = "SELECT ImageData FROM Images WHERE ImageID = @ImageID";
        //    //    using (SqlCommand command = new SqlCommand(query, connection))
        //    //    {
        //    //        command.Parameters.AddWithValue("@ImageID", imageId);
        //    //        imageData = (byte[])command.ExecuteScalar();
        //    //    }
        //    //}

        //    return imageData;
        //}


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}