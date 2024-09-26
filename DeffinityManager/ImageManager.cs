using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;


/// <summary>
/// Summary description for ImageHandler
/// </summary>
public class ImageManager
{
    public ImageManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static void SaveImage_setfullpath(string s_filename, byte[] a_arrRawData, string setFullPath)
    {

        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image

        //orignal size
        string sOriginalDataPath = GetFullPath(s_filename, setFullPath);
        WriteToFile(sOriginalDataPath, a_arrRawData);

        //tumbnail large
        //arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        //string sThumbLargePath = GetImagePath(s_InstanceName, ImageType.ThumbNails, setPath);
        //WriteToFile(sThumbLargePath, arrMediumSmallerThumb);
        ////thumbnail medium
        //arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Medium);
        //string sThumbMediumSmallerPath = GetImagePath(s_InstanceName, ImageType.ThumbNailsMedium, setPath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    private static string GetFullPath(String a_gName, string setPath)
    {
        string sFolderPath = string.Empty;

        sFolderPath = setPath;

        string sPath = sFolderPath + "\\" + a_gName + ".png";
        return sPath;
    }
    public static void Save_FlsCustomerFiles(byte[] a_arrRawData, string filename, string fdpath)
    {
        //create if directiory is not exists
        bool folderExists = Directory.Exists(fdpath); ;
        if (!folderExists)
            Directory.CreateDirectory(fdpath);
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = fdpath + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = fdpath + filename + "_thumb.png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Medium);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    public static void Save_FlsCustomerFiles(byte[] a_arrRawData, string filename)
    {
        //create if directiory is not exists
        bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC"));
        if (!folderExists)
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC"));
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/DC") + "\\" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/DC") + "\\" + filename + "_thumb.png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Medium);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    public static void SaveHC_file(byte[] a_arrRawData, string filename)
    {
        //create if directiory is not exists
        bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/HC"));
        if (!folderExists)
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/HC"));
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/HC") + "\\original_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/HC") + "\\" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    public static void SaveProtfolioImage_setpath(byte[] a_arrRawData, string filename)
    {
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_org_" + filename + ".png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath( ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);


        //SaveProtfolioImage_setpath_copylocation( a_arrRawData,  filename);

        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_portfolio, ".png", filename);

    }


    public static void SaveEmailBannerImage_setpath(byte[] a_arrRawData, string filename)
    {
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_org_" + filename + ".png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath( ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.FitScreen);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);


        //SaveProtfolioImage_setpath_copylocation( a_arrRawData,  filename);

        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_email_banner, ".png", filename);

    }

    public static void SaveLadndingPageTopImage_setpath(byte[] a_arrRawData, string filename)
    {

        byte[] arrMediumSmallerThumb = null;

        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);

        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_landing_top, ".png", filename);

    }

    public static void SaveProtfolioBannermage_setpath(byte[] a_arrRawData, string filename)
    {
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_org_" + filename + ".png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath( ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers") + "\\" + "portfolio_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.FitLargeScreen);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);


        //SaveProtfolioImage_setpath_copylocation( a_arrRawData,  filename);

        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_banner, ".png", filename);

    }

    public static string file_section_portfolio = "portfolio";
    public static string file_section_landing_top = "landingtop";

    public static string file_section_user = "user";
    public static string file_section_fundriser = "fundriser";
    public static string file_section_fundriser_top = "fundriser_top";
    public static string file_section_fundriser_logo = "fundriserlogo";
    public static string file_section_fundriser_qr = "fundriserqr";
    public static string file_section_fundriser_my_logo = "fundrisermylogo";
    public static string file_section_event = "event";
    public static string file_section_online = "online";
    public static string file_section_speaker = "speaker";
    public static string file_section_spnsor = "sponsor";
    public static string file_section_portfolio_doc = "portoliodoc";
    public static string file_section_user_doc = "userdoc";
    public static string file_section_donor_doc = "donordoc";
    public static string file_section_landing = "landing";
    public static string file_section_messages = "messages";
    public static string file_section_paymenttype = "paymenttype";
    public static string file_section_cost = "cost";

    public static string file_section_banner = "banner";

    public static string file_section_email_banner = "email_banner";

    public static string file_section_email_attach = "email_attach";

    public static string file_section_video = "video";
    public static string file_section_marketplace = "marketplace";



    public static bool FileIsExists(string fileid, string section)
    {
        //bool retval = false;
        IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

        var f = frep.GetAll().Where(o => o.FileID == fileid && o.Section == section).FirstOrDefault();

        if (f != null)
            return true;
        else
            return false;
    }
    public static string FileDBSave(byte[] filebytearray, byte[] Smallfilebytearray, string fileid, string section, string fileExtension, string filename = "", string contenttype = "", string folder = "", bool allowMutifile = false)
    {
        string retval = "";
        try
        {
            IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            var f = frep.GetAll().Where(o => o.FileID == fileid && o.Section == section).FirstOrDefault();
            if (allowMutifile == true)
            {
                f = null;
            }
            if (f == null)
            {
                f = new PortfolioMgt.Entity.FileData();
                f.FolderID = folder;
                f.ContentLength = filebytearray.Length;
                f.FileData1 = filebytearray;
                f.FileType = contenttype;
                if (filename.Length == 0)
                    f.FileName = fileid;
                else
                    f.FileName = filename;

                if (Smallfilebytearray != null)
                    f.FileData2 = Smallfilebytearray;
                f.Section = section;
                f.FileID = fileid;
                frep.Add(f);

            }
            else
            {
                f.FolderID = folder;
                f.ContentLength = filebytearray.Length;
                f.FileData1 = filebytearray;
                f.FileType = contenttype;
                if (filename.Length == 0)
                    f.FileName = fileid;
                else
                    f.FileName = filename;
                if (Smallfilebytearray != null)
                    f.FileData2 = Smallfilebytearray;
                // f.FileID = fileid;//
                frep.Edit(f);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return retval;
    }

    public static void SaveProtfolioImage_setpath_copylocation(byte[] a_arrRawData, string filename)
    {
        var npath = Deffinity.systemdefaults.GetCopyLocation() + "\\Customers";
        bool folderExists = Directory.Exists(npath);
        if (!folderExists)
            Directory.CreateDirectory(npath);
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = npath + "\\" + "portfolio_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = npath + "\\" + "portfolio_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }

    public static void SaveBlogImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        bool folderExists = Directory.Exists(path);
        if (!folderExists)
            Directory.CreateDirectory(path);
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "Blog_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = path + "\\" + "Blog_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }


    public static void SavePartnerImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        bool folderExists = Directory.Exists(path);
        if (!folderExists)
            Directory.CreateDirectory(path);
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "partner_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = path + "\\" + "partner_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }

    public static void SavePortfolioImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);

        bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        if (!folderExists)
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers"));
        //if (!folderExists)
        //    Directory.CreateDirectory(path);
        path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers");
        byte[] arrMediumSmallerThumb = null;
        var tfilepath = path + "\\" + "portfolio_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //save to database
        PortfolioMgt.BAL.ProjectPortfolioBAL.Portfolio_SaveImageData(Convert.ToInt32(filename), arrMediumSmallerThumb);
        //save the file to 
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "portfolio_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);



    }

    public static void SaveEventImage_setpath(byte[] a_arrRawData, string filename, string path, string foldername)
    {
        // bool folderExists = Directory.Exists(path);
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Events/" + foldername));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Events/" + foldername));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Events/" + foldername);
        byte[] arrMediumSmallerThumb_big = null;
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        // var filepath = path + "\\" + "event_org_" + filename + ".png";
        //orignal size
        // string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);

        //var tfilepath = path + "\\" + "" + filename + ".png";
        arrMediumSmallerThumb_big = GenerateThumb(a_arrRawData, ThumbnailSize.FitLargeScreen);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        //tfilepath = path + "\\" + "event_small_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        //string sThumbLargePath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbLargePath, arrMediumSmallerThumb);
        // SaveUserImage_setpath_copylocation(a_arrRawData, filename, path);


        FileDBSave(arrMediumSmallerThumb_big, arrMediumSmallerThumb, filename, file_section_event, ".png", filename);

    }

    public static void SaveProductImage_setpath(byte[] a_arrRawData, string filename, string path)
    {



        byte[] arrMediumSmallerThumb_big = null;
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        // var filepath = path + "\\" + "event_org_" + filename + ".png";
        //orignal size
        // string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);

        //var tfilepath = path + "\\" + "" + filename + ".png";
        arrMediumSmallerThumb_big = GenerateThumb(a_arrRawData, ThumbnailSize.OriginalData);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        //tfilepath = path + "\\" + "event_small_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Instance);
        //string sThumbLargePath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbLargePath, arrMediumSmallerThumb);
        // SaveUserImage_setpath_copylocation(a_arrRawData, filename, path);


        FileDBSave(arrMediumSmallerThumb_big, arrMediumSmallerThumb, filename, file_section_online, ".png", filename);
    }

    public static void SaveTithingImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);

        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(path);
        // path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing");
        byte[] arrMediumSmallerThumb_big = null;
        byte[] arrMediumSmallerThumb = null;
        //var tfilepath = path + "\\" + "" + filename + ".png";
        arrMediumSmallerThumb_big = GenerateThumb(a_arrRawData, ThumbnailSize.FitLargeScreen);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        //tfilepath = path + "\\" + "event_small_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        //generate the thumbs for the raw image
        //var filepath = path + "\\" + "tithing_org_" + filename + ".png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = path + "\\" + "tithing_" + filename + ".png";
        //arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        //SaveTithingImage_setpath_copyLocation(a_arrRawData, filename, path);

        FileDBSave(arrMediumSmallerThumb_big, arrMediumSmallerThumb, filename, file_section_fundriser, ".png", filename);
    }
    public static void SaveTithingTopImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);

        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(path);
        // path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing");
        byte[] arrMediumSmallerThumb_big = null;
        byte[] arrMediumSmallerThumb = null;
        //var tfilepath = path + "\\" + "" + filename + ".png";
        arrMediumSmallerThumb_big = GenerateThumb(a_arrRawData, ThumbnailSize.FitLargeScreen);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        //tfilepath = path + "\\" + "event_small_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        //generate the thumbs for the raw image
        //var filepath = path + "\\" + "tithing_org_" + filename + ".png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = path + "\\" + "tithing_" + filename + ".png";
        //arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        //SaveTithingImage_setpath_copyLocation(a_arrRawData, filename, path);

        FileDBSave(arrMediumSmallerThumb_big, arrMediumSmallerThumb, filename, file_section_fundriser_top, ".png", filename);
    }
    public static void SaveTithingLogoImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);

        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing");
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = path + "\\" + "tithing_org_" + filename + "_logo.png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = path + "\\" + "tithing_" + filename + "_logo.png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        //SaveTithingImage_setpath_copyLocation(a_arrRawData, filename, path);


        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_fundriser_logo, ".png", filename);
    }
    public static void SaveTithingMyLogoImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);

        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing");
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = path + "\\" + "tithing_org_" + filename + "_mylogo.png";
        ////orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //WriteToFile(sOriginalDataPath, a_arrRawData);
        //var tfilepath = path + "\\" + "tithing_" + filename + "_mylogo.png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        //SaveTithingImage_setpath_copyLocation(a_arrRawData, filename, path);



        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_fundriser_my_logo, ".png", filename);
    }
    public static void SaveTithingImage_setpath_copyLocation(byte[] a_arrRawData, string filename, string path)
    {
        //bool folderExists = Directory.Exists(path);
        var npath = Deffinity.systemdefaults.GetCopyLocation() + "\\Tithing";
        bool folderExists = Directory.Exists(npath);// Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing"));
        if (!folderExists)
            Directory.CreateDirectory(npath);
        //if (!folderExists)
        //    Directory.CreateDirectory(path);
        path = npath;
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "tithing_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = path + "\\" + "tithing_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }


    public static void SaveUserImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        // bool folderExists = Directory.Exists(path);
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users");
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = path + "\\" + "user_org_" + filename + ".png";
        //orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //  WriteToFile(sOriginalDataPath, a_arrRawData);
        // var tfilepath = path + "\\" + "user_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        // SaveUserImage_setpath_copylocation(a_arrRawData,  filename,  path);
        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_user, ".png", filename);
    }
    public static string FileDBSave(int Userid, DateTime date, byte[] filebytearray, byte[] Smallfilebytearray, string fileid, string section, string fileExtension, string filename = "", string contenttype = "", string folder = "", bool allowMutifile = false)
    {
        string retval = "";
        try
        {
            IPortfolioRepository<PortfolioMgt.Entity.FileData> frep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

            var f = frep.GetAll().Where(o => o.FileID == fileid && o.Section == section).FirstOrDefault();
            if (allowMutifile == true)
            {
                f = null;
            }
            if (f == null)
            {
                f = new PortfolioMgt.Entity.FileData();
                f.FolderID = folder;
                f.UserID = Userid;
                f.UploadedDate = date;
                f.ContentLength = filebytearray.Length;
                f.FileData1 = filebytearray;
                f.FileType = contenttype;
                if (filename.Length == 0)
                    f.FileName = fileid;
                else
                    f.FileName = filename;

                if (Smallfilebytearray != null)
                    f.FileData2 = Smallfilebytearray;
                f.Section = section;
                f.FileID = fileid;
                frep.Add(f);

            }
            else
            {
                f.FolderID = folder;
                f.ContentLength = filebytearray.Length;
                f.FileData1 = filebytearray;
                f.UserID = Userid;
                f.UploadedDate = date;
                f.FileType = contenttype;
                if (filename.Length == 0)
                    f.FileName = fileid;
                else
                    f.FileName = filename;
                if (Smallfilebytearray != null)
                    f.FileData2 = Smallfilebytearray;
                // f.FileID = fileid;//
                frep.Edit(f);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return retval;
    }

    public static void SaveVideoImage(byte[] a_arrRawData, string filename, string path)
    {

        byte[] arrMediumSmallerThumb = null;

        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.YoutubeThumb);

        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_video, ".png", filename);
    }
    public static void SaveSponserImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        // bool folderExists = Directory.Exists(path);
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users");
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = path + "\\" + "user_org_" + filename + ".png";
        //orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //  WriteToFile(sOriginalDataPath, a_arrRawData);
        // var tfilepath = path + "\\" + "user_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        // SaveUserImage_setpath_copylocation(a_arrRawData,  filename,  path);
        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_spnsor, ".png", filename);
    }
    public static void SaveSpeakerImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        // bool folderExists = Directory.Exists(path);
        //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        //if (!folderExists)
        //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/WF/UploadData/Users"));
        ////if (!folderExists)
        ////    Directory.CreateDirectory(path);
        //path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users");
        byte[] arrMediumSmallerThumb = null;
        ////generate the thumbs for the raw image
        //var filepath = path + "\\" + "user_org_" + filename + ".png";
        //orignal size
        //string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        //  WriteToFile(sOriginalDataPath, a_arrRawData);
        // var tfilepath = path + "\\" + "user_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        //string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        //WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

        // SaveUserImage_setpath_copylocation(a_arrRawData,  filename,  path);
        FileDBSave(arrMediumSmallerThumb, null, filename, file_section_speaker, ".png", filename);
    }

    public static void SaveUserImage_setpath_copylocation(byte[] a_arrRawData, string filename, string path)
    {
        // bool folderExists = Directory.Exists(path);
        var npath = Deffinity.systemdefaults.GetCopyLocation() + "\\Users";
        bool folderExists = Directory.Exists(npath);
        if (!folderExists)
            Directory.CreateDirectory(npath);
        //if (!folderExists)
        //    Directory.CreateDirectory(path);
        path = npath;// HttpContext.Current.Server.MapPath("~/WF/UploadData/Users");
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "user_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = path + "\\" + "user_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }

    public static void SavePartnerBackImage_setpath(byte[] a_arrRawData, string filename, string path)
    {
        bool folderExists = Directory.Exists(path);
        if (!folderExists)
            Directory.CreateDirectory(path);
        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        var filepath = path + "\\" + "partnerback_org_" + filename + ".png";
        //orignal size
        string sOriginalDataPath = GetImagePath(ImageType.OriginalData, filepath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
        var tfilepath = path + "\\" + "partnerback_" + filename + ".png";
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Poratal);
        string sThumbMediumSmallerPath = GetImagePath(ImageType.Instance, tfilepath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    public static void SaveImage_setpath(string s_InstanceName, byte[] a_arrRawData, string setPath)
    {

        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image

        //orignal size
        string sOriginalDataPath = GetImagePath(s_InstanceName, ImageType.OriginalData, setPath);
        WriteToFile(sOriginalDataPath, a_arrRawData);

        //tumbnail large
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        string sThumbLargePath = GetImagePath(s_InstanceName, ImageType.ThumbNails, setPath);
        WriteToFile(sThumbLargePath, arrMediumSmallerThumb);
        //thumbnail medium
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Medium);
        string sThumbMediumSmallerPath = GetImagePath(s_InstanceName, ImageType.ThumbNailsMedium, setPath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    //this created for Moves asstes...on 21/10/2010
    public static void SaveImage_setpath1(string s_InstanceName, byte[] a_arrRawData, string setPath)
    {

        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image

        //orignal size
        string sOriginalDataPath = GetImagePath2(s_InstanceName, ImageType.OriginalData, setPath);
        WriteToFile(sOriginalDataPath, a_arrRawData);

        //tumbnail large
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);
        string sThumbLargePath = GetImagePath1(s_InstanceName, ImageType.ThumbNails, setPath);
        WriteToFile(sThumbLargePath, arrMediumSmallerThumb);
        //thumbnail medium
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Medium);
        string sThumbMediumSmallerPath = GetImagePath1(s_InstanceName, ImageType.ThumbNailsMedium, setPath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);

    }
    public static void SaveImage_setpath_DeskImage(string s_InstanceName, byte[] a_arrRawData, string setPath)
    {
        //DeskImage Fit to screen
        byte[] arrDeskImage = null;
        byte[] arrFitScreen = null;

        arrFitScreen = GenerateThumb(a_arrRawData, ThumbnailSize.FitScreen);
        string sOriginalDataPath = GetImagePath2(s_InstanceName, ImageType.FitScreen, setPath);
        WriteToFile(sOriginalDataPath, arrFitScreen);


        //thumbnail DeskImage
        arrDeskImage = GenerateThumb(a_arrRawData, ThumbnailSize.DeskImage);
        string sThumbMediumSmallerPath = GetImagePath1(s_InstanceName, ImageType.DeskImage, setPath);
        WriteToFile(sThumbMediumSmallerPath, arrDeskImage);




    }
    public static void SaveInstanceImage(string s_InstanceName, byte[] a_arrRawData)
    {
        byte[] arrInstanceThumb = null;
        arrInstanceThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Instance);
        string sInstancePath = GetImagePath(s_InstanceName, ImageType.Instance);
        WriteToFile(sInstancePath, arrInstanceThumb);
    }
    public static void SaveImage(Guid a_gMediaItemId, byte[] a_arrRawData)
    {

        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);

        string sOriginalDataPath = GetImagePath(a_gMediaItemId, ImageType.OriginalData);
        WriteToFile(sOriginalDataPath, a_arrRawData);

        string sThumbMediumSmallerPath = GetImagePath(a_gMediaItemId, ImageType.ThumbNails);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
    }

    public static void SaveImage(Guid a_gMediaItemId, byte[] a_arrRawData, string folderpath)
    {

        byte[] arrMediumSmallerThumb = null;
        //generate the thumbs for the raw image
        arrMediumSmallerThumb = GenerateThumb(a_arrRawData, ThumbnailSize.Large);

        string sOriginalDataPath = GetImagePath(a_gMediaItemId, ImageType.OriginalData, folderpath);
        WriteToFile(sOriginalDataPath, a_arrRawData);

        string sThumbMediumSmallerPath = GetImagePath(a_gMediaItemId, ImageType.ThumbNails, folderpath);
        WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
    }

    private static string GetImagePath(Guid a_gMediaItemId, ImageType a_eImageType)
    {
        string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData");
        string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gMediaItemId.ToString() + ".png";
        return sPath;
    }
    private static string GetImagePath(Guid a_gMediaItemId, ImageType a_eImageType, string path)
    {
        string sFolderPath = path;
        string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gMediaItemId.ToString() + ".png";
        return sPath;
    }
    private static string GetImagePath(String a_gName, ImageType a_eImageType)
    {
        string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData");
        string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gName + ".png";
        return sPath;
    }
    private static string GetImagePath(String a_gName, ImageType a_eImageType, string setPath)
    {
        string sFolderPath = string.Empty;
        if (setPath.ToLower() == "assets")
            sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Assets");
        else
            sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users");

        string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gName + ".png";
        return sPath;
    }
    private static string GetImagePath(ImageType a_eImageType, string setPath)
    {
        //string sFolderPath = HttpContext.Current.Server.MapPath("~/UploadData/Users");
        string sPath = setPath;
        return sPath;
    }
    //private static string GetImagePath(String a_gName, ImageType a_eImageType,string setPath)
    //{
    //    string sFolderPath = HttpContext.Current.Server.MapPath("~/UploadData/Users");
    //    string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gName + ".png";
    //    return sPath;
    //}

    //this created for Moves asstes...on 21/10/2010
    private static string GetImagePath2(String a_gName, ImageType a_eImageType, string setPath)
    {
        string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Moves");
        string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gName + ".png";
        return sPath;
    }
    private static string GetImagePath1(String a_gName, ImageType a_eImageType, string setPath)
    {
        string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Moves");
        string sPath = sFolderPath + "\\" + a_gName + ".png";
        return sPath;
    }
    public static byte[] GenerateThumb(byte[] a_arrImage, ThumbnailSize a_eSize)
    {
        using (Bitmap oImage = new Bitmap(new MemoryStream(a_arrImage)))
        {
            //the thumbnail image should fit in a box with sizes nMaxWidth x nMaxHeight
            int nMaxWidth = 0;
            int nMaxHeight = 0;

            if (a_eSize == ThumbnailSize.Large)
            {
                nMaxWidth = 100;
                nMaxHeight = 100;
            }
            else if (a_eSize == ThumbnailSize.Medium)
            {
                nMaxWidth = 64;
                nMaxHeight = 64;
            }
            else if (a_eSize == ThumbnailSize.Small)
            {
                nMaxWidth = 30;
                nMaxHeight = 30;
            }
            else if (a_eSize == ThumbnailSize.Instance)
            {
                nMaxWidth = 200;
                nMaxHeight = 90;
            }
            else if (a_eSize == ThumbnailSize.Poratal)
            {
                var maxWidth = 187;
                var maxHeight = 65;
                // Get the image's original width and height
                int originalWidth = oImage.Width;
                int originalHeight = oImage.Height;
                float ratioX = (float)maxWidth / (float)originalWidth;
                float ratioY = (float)maxHeight / (float)originalHeight;
                float ratio = Math.Min(ratioX, ratioY);

                if (oImage.Height > 65 && oImage.Width > 187)
                {
                    nMaxHeight = (int)(originalHeight * ratio);
                    nMaxWidth = (int)(originalWidth * ratio);
                }
                else if (oImage.Height > 65)
                {
                    nMaxHeight = (int)(originalHeight * ratio);
                    nMaxWidth = (int)(originalWidth * ratio);
                }
                else if (oImage.Width > 187)
                {
                    nMaxHeight = (int)(originalHeight * ratio);
                    nMaxWidth = (int)(originalWidth * ratio);
                }
                else
                {
                    nMaxHeight = oImage.Height;
                    nMaxWidth = oImage.Width;
                }
            }
            else if (a_eSize == ThumbnailSize.DeskImage)
            {

                nMaxWidth = 600;
                nMaxHeight = 520;

            }
            else if (a_eSize == ThumbnailSize.YoutubeThumb)
            {
                if (oImage.Height > 180 || oImage.Width > 320)
                {
                    nMaxWidth = 320;
                    nMaxHeight = 180;
                }
                else
                {
                    nMaxWidth = oImage.Width;
                    nMaxHeight = oImage.Height;
                }
            }
            else if (a_eSize == ThumbnailSize.BannerImage)
            {
                if (oImage.Height > 180 || oImage.Width > 320)
                {
                    nMaxWidth = 320;
                    nMaxHeight = 180;
                }
                else
                {
                    nMaxWidth = oImage.Width;
                    nMaxHeight = oImage.Height;
                }
            }
            else if (a_eSize == ThumbnailSize.FitScreen)
            {
                if (oImage.Height > 700 || oImage.Width > 1000)
                {
                    nMaxWidth = 900;
                    nMaxHeight = 600;
                }
                else
                {
                    nMaxWidth = oImage.Width;
                    nMaxHeight = oImage.Height;
                }
            }
            else if (a_eSize == ThumbnailSize.FitLargeScreen)
            {
                if (oImage.Height > 720 || oImage.Width > 1280)
                {
                    nMaxWidth = 1280;
                    nMaxHeight = 720;
                }
                else
                {
                    nMaxWidth = oImage.Width;
                    nMaxHeight = oImage.Height;
                }
            }


            else if (a_eSize == ThumbnailSize.PoratalLarge)
            {
                if (oImage.Height > 250 || oImage.Width > 250)
                {
                    nMaxWidth = 250;
                    nMaxHeight = 250;
                }
                else
                {
                    nMaxWidth = oImage.Width;
                    nMaxHeight = oImage.Height;
                }
            }
            else //if eSize == MultiMediaFacade.ThumbnailSize.MediumSmaller
            {
                nMaxWidth = 52;
                nMaxHeight = 52;
            }

            int nNewWidth = 0;
            int nNewHeight = 0;

            if (oImage.Height < oImage.Width)
            {
                nNewWidth = nMaxWidth;
                nNewHeight = (int)((double)oImage.Height * ((double)nMaxWidth / (double)oImage.Width));
                if (nNewHeight == 0)//extremely narrow pictures will get their height as 0, just make it 1
                {
                    nNewHeight = 1;
                }
            }
            else
            {
                nNewHeight = nMaxHeight;
                nNewWidth = (int)((double)oImage.Width * ((double)nMaxWidth / (double)oImage.Height));
                if (nNewWidth == 0)//extremely narrow pictures will get their wodth as 0, just make it 1
                {
                    nNewWidth = 1;
                }
            }
            //make a white image for the background            
            using (Bitmap oCanvas = new Bitmap(nMaxWidth, nMaxHeight, PixelFormat.Format32bppArgb))
            {
                oCanvas.SetResolution(oImage.HorizontalResolution, oImage.VerticalResolution);
                using (Graphics oGraphics = Graphics.FromImage(oCanvas))
                {
                    oGraphics.Clear(Color.White);
                    //draw the thumb
                    oGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    oGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    if (oImage.Height < oImage.Width)
                    {
                        oGraphics.DrawImage(oImage, (float)0, ((float)nMaxHeight - (float)nNewHeight) / 2, (float)nNewWidth, (float)nNewHeight);
                    }
                    else
                    {
                        oGraphics.DrawImage(oImage, ((float)nMaxWidth - (float)nNewWidth) / 2, (float)0, (float)nNewWidth, (float)nNewHeight);
                    }

                    using (MemoryStream oResultStream = new MemoryStream())
                    {
                        oCanvas.Save(oResultStream, System.Drawing.Imaging.ImageFormat.Png);

                        return oResultStream.ToArray();
                    }
                }
            }
        }
    }
    private static void WriteToFile(string a_sPath, byte[] a_arrData)
    {
        using (FileStream oFileStream = new FileStream(a_sPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            oFileStream.Write(a_arrData, 0, a_arrData.Length);
        }
    }

    public enum ThumbnailSize
    {
        Large,
        Medium,
        MediumSmaller
       , Small,
        OriginalData,
        Instance,
        Poratal,
        DeskImage,
        FitScreen,
        FitLargeScreen,
        PoratalLarge,
        YoutubeThumb,
        BannerImage
    }

    public enum ImageType
    {
        OriginalData,
        ThumbNails,
        ThumbNailsMedium,
        Instance,
        Poratal,
        DeskImage,
        FitScreen,
        YoutubeThumb,
        BannerImage
    }

    #region image sting path



    #endregion

    #region Document Upload

    private static string GetDocPath(String a_gName, String f_extension, string setPath)
    {
        string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/" + setPath);
        string sPath = sFolderPath + "\\" + "\\" + a_gName + "." + f_extension;
        return sPath;
    }
    public static void SaveDocument_setpath(string s_InstanceName, string FileExtension, byte[] a_arrRawData, string setPath)
    {

        //orignal size
        string sOriginalDataPath = GetDocPath(s_InstanceName, FileExtension, setPath);
        WriteToFile(sOriginalDataPath, a_arrRawData);
    }
    #endregion
}
