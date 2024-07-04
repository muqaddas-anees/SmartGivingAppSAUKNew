using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
using Deffinity.EmailService;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Collections;
using System.Net;
public partial class Training_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      //  CreateMyPDF();
        if (!IsPostBack)
        {
            //string ID = Request.QueryString["id"];

            //try
            //{
            //    BookingsEntity booking = Bookings.Bookings_Select(int.Parse(ID));

            //    string fileName = booking.FileName;
            //    if (fileName != "")
            //    {
            //        string name = Path.GetFileName(fileName);
            //        string extension = Path.GetExtension(fileName);
            //        string type = "";
            //        string ext = Path.GetExtension(name);
            //        switch (ext.ToLower())
            //        {
            //            case ".txt":
            //                type = "text/plain";
            //                break;
            //            case ".doc":
            //            case ".rtf":
            //                type = "Application/msword";
            //                break;
            //            case ".pdf":
            //                type = "Application/pdf";
            //                break;

            //            case ".jpeg":
            //                type = "image/pjpeg";
            //                break;
            //            case ".png":
            //                type = "image/png";
            //                break;
            //            case ".gif":
            //                type = "image/gif";
            //                break;
            //        }


            //        HttpContext.Current.Response.ContentType = type;

            //        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + name);

            //        HttpContext.Current.Response.WriteFile(MapPath(fileName));

            //        HttpContext.Current.Response.End();
            //    }
            //    else
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "erorr",
            //            "<script language='javascript'>alert('erorr');</script>");
            //    }
            //}

            //catch (Exception ex)
            //{
            //    LogExceptions.WriteExceptionLog(ex);
            //}
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string url = Server.MapPath("..\\Training\\viewcoursefeedback.aspx");
        //HtmlHijacker html = new HtmlHijacker();
        string se = RetrieveBodyFromAnotherPage(url);
        Document document = new Document(PageSize.A4, 80, 50, 30, 65);
        try
        {
           // Response.ContentType = "application/pdf";
           // Response.AddHeader("content-disposition", "attachment; filename=MypdfFile.pdf");

           // PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
           // //XmlTextReader reader = new XmlTextReader(new StringReader(strContent));
           // //reader.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
           // //HtmlParser.Parse(document, reader);
           // //System.Xml.XmlTextReader _xmlr = new System.Xml.XmlTextReader(new StringReader(@"<html><body>This is my <bold>test</bold> string</body></html>"));
           //// document.Add(new iTextSharp.text.html.HtmlParser());
           // ITextmyHtmlHandler handler = new ITextmyHtmlHandler(document);
           // System.Xml.XmlTextReader _xmlr = new System.Xml.XmlTextReader(new StringReader(se));
           // _xmlr.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
           // HtmlParser.Parse(document, _xmlr);
            
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public string RetrieveBodyFromAnotherPage(string url)
    {
        string markUpText = string.Empty;
        System.Net.WebClient Http = new System.Net.WebClient();

        // Download the Web resource and save it into a data buffer.
        try
        {
            byte[] Result = Http.DownloadData(url);
            markUpText = Encoding.Default.GetString(Result);
        }
        catch (Exception ex)
        {

            return null;
        }
        return markUpText;
    }
    private void CreateMyPDF()
    {
        Document doc = new Document(PageSize.A4);

        PdfWriter writer = PdfWriter.GetInstance(doc, new
        FileStream("C:\\lakhan4.pdf", FileMode.Create));

       // new iTextSharp.text.html.HtmlParser();
        doc.Open();

        //string strURL = Server.MapPath("..\\Training\\viewcoursefeedback.aspx");
        string strURL = "http://localhost:3640/Excel/Training/viewcoursefeedback.aspx?ID=2";
       Uri uri = new Uri(strURL);
        //string url = Server.MapPath("..\\Training\\trViewFeedback.aspx");
        ////HtmlHijacker html = new HtmlHijacker();
        //string se = RetrieveBodyFromAnotherPage(url);
        //Create the request object

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
        req.UserAgent = "Get Content";
        WebResponse resp = req.GetResponse();
        Stream stream = resp.GetResponseStream();
        StreamReader sr = new StreamReader(stream);
        string html = sr.ReadToEnd();
        iTextSharp.text.html.simpleparser.StyleSheet d = new iTextSharp.text.html.simpleparser.StyleSheet();

        //ArrayList lt = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(html), null);

        ColumnText ct = new ColumnText(writer.DirectContent);

        ct.SetSimpleColumn(50, 50, PageSize.A4.Width - 50, PageSize.A4.Height - 50);

        //for (int k = 0; k < lt.Count; ++k)
        //{

        //    ct.AddElement((IElement)lt[k]);

        //}

        ct.Go();

        doc.Close();

    }
}
