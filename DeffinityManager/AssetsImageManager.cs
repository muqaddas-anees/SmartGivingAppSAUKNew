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

/// <summary>
/// Summary description for AssetsImageManager
/// </summary>
namespace AssetsImage_Manager
{
    public class AssetsImageManager
    {
        public AssetsImageManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void Assets_SaveImage(Guid a_gMediaItemId, byte[] a_arrRawData)
        {

            byte[] arrMediumSmallerThumb = null;
            //generate the thumbs for the raw image
            arrMediumSmallerThumb = Assets_GenerateThumb(a_arrRawData, ThumbnailSize.MediumSmaller);

            string sOriginalDataPath = Assets_GetImagePath(a_gMediaItemId, ImageType.OriginalData);
            WriteToFile(sOriginalDataPath, a_arrRawData);

            string sThumbMediumSmallerPath = Assets_GetImagePath(a_gMediaItemId, ImageType.ThumbNails);
            WriteToFile(sThumbMediumSmallerPath, arrMediumSmallerThumb);
        }

        private static string Assets_GetImagePath(Guid a_gMediaItemId, ImageType a_eImageType)
        {
            string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Assets/");
            string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gMediaItemId.ToString() + ".png";
            return sPath;
        }
        private static string Assets_GetImagePath(String a_gName, ImageType a_eImageType)
        {
            string sFolderPath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Assets/");
            string sPath = sFolderPath + "\\" + a_eImageType.ToString() + "\\" + a_gName + ".png";
            return sPath;
        }
        public static byte[] Assets_GenerateThumb(byte[] a_arrImage, ThumbnailSize a_eSize)
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
            Instance
        }

        public enum ImageType
        {
            OriginalData,
            ThumbNails,
            Instance
        }
    }
}