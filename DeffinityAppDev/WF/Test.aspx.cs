using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;
using PdfGenerator;


using System.Threading.Tasks;

namespace DeffinityAppDev.WF
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // int d = Deffinity.Utility.MonthDifference(DateTime.Now, DateTime.Now.AddMonths(5));
            int noofcolumns = 1;
            int noofRows = 5;
            int totalwidth = 12;
            try
            {
                for (int row = 1; row <= noofRows; row++)
                {

                    Panel pnlRoot = new Panel();
                    pnlRoot.CssClass = "form-group row";

                   

                    for (int col = 1; col <= noofcolumns; col++)
                    {
                        //add lable
                        Panel pnlRootControl = new Panel();
                        pnlRootControl.CssClass = string.Format("col-md-{0}", 12 / noofcolumns);
                        Panel pnlLabel = new Panel();   
                        pnlLabel.CssClass = "col-sm-3 control-label";
                        Literal lblTitle = new Literal();
                        lblTitle.Text = "row:"+row.ToString()+ "col:" + col.ToString() + " Lable";
                        pnlLabel.Controls.Add(lblTitle);

                        Panel pnlControl = new Panel();
                        pnlControl.CssClass = "col-sm-9 control-label";
                        TextBox txt = new TextBox();
                        txt.CssClass = "form-control";
                        pnlControl.Controls.Add(txt);

                        pnlRootControl.Controls.Add(pnlLabel);
                        pnlRootControl.Controls.Add(pnlControl);
                        pnlRoot.Controls.Add(pnlRootControl);

                    }

                   
                    ph.Controls.Add(pnlRoot);
                }
               

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        #region Panel Methods

       //Root panle
       private Panel GetRootPanel()
        {
            Panel pnlRoot = new Panel();
            pnlRoot.CssClass = "form-group row";
            return pnlRoot;
        }

        private Panel GetSubRootPanel()
        {
            Panel pnlSubRoot = new Panel();
            pnlSubRoot.CssClass = "col-md-12";
            return pnlSubRoot;
        }


        #endregion

        protected void btngetid_Click(object sender, EventArgs e)
        {
            // var pdfSaveLocation = "\"" + Server.MapPath("~/WF/UploadData/") + "question.pdf\"";

            PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/", "first", new string[] { Server.MapPath("~/WF/DC/EmailTemplates/Policy.html") }, new string[] { "--header-html " + Server.MapPath("~/WF/DC/EmailTemplates/Header.html"), "--footer-html " + Server.MapPath("~/WF/DC/EmailTemplates/footer.html") });
            //PdfGenerator.PdfGenerator.HtmlToPdf("~/WF/UploadData/", "first", new string[] { @"http://localhost:55390/WF/DC/EmailTemplates/Policy.html" }, new string[] { "--header-html " + @"http://localhost:55390/WF/DC/EmailTemplates/Header.html", "--footer-html " + @"http://localhost:55390/WF/DC/EmailTemplates/Footer.html" });
                
            //var wkhtmltopdfLocation = Server.MapPath("~/bin/") + "wkhtmltopdf.exe";
            //var htmlUrl = @"http://localhost:55390/WF/DC/EmailTemplates/Policy.html";
           
            //var process = new Process();
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.CreateNoWindow = true;
            //process.StartInfo.FileName = wkhtmltopdfLocation;
            //process.StartInfo.Arguments = htmlUrl + " " + pdfSaveLocation;
            //process.Start();
            //process.WaitForExit();


            //IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            //var entity = urep.GetAll().Where(o => o.ID == Convert.ToInt32(txtid.Text)).FirstOrDefault();
            //if (entity != null)
            //    lblresult.Text = HashSHA1Decryption.HashSHA1Decryption1(entity.Password);
        }



        //public void generatePDF()
        //{
        //    // create a new document with your desired configuration
        //    var document = new HtmlToPdfDocument
        //    {
        //        GlobalSettings =
        //        {
        //            ProduceOutline = true,
        //            DocumentTitle = "Pretty Websites",
        //            //PaperSize = PaperKind.A4,
        //            Margins =
        //            {
        //                All = 1.375,
        //                Unit = TuesPechkin.Unit.Centimeters
        //            }
        //        },
        //        Objects = {
        //            new ObjectSettings { HtmlText = "<h1>Pretty Websites</h1><p>This might take a bit to convert!</p>" },
        //            new ObjectSettings { PageUrl = "www.google.com" },
        //            new ObjectSettings { PageUrl = "www.microsoft.com" },
        //            new ObjectSettings { PageUrl = "www.github.com" }
        //        }
        //    };

        //    IPechkin converter = Factory.Create();

        //    // subscribe to events
        //    /* converter.Begin += OnBegin;
        //    converter.Error += OnError;
        //    converter.Warning += OnWarning;
        //    converter.PhaseChanged += OnPhase;
        //    converter.ProgressChanged += OnProgress;
        //    converter.Finished += OnFinished;*/

        //    // convert document
        //    byte[] result = converter.Convert(document);

        //    ByteArrayToFile(result, "C:/temp/exmple.pdf");
        //}
        public static bool ByteArrayToFile(byte[] _ByteArray, string _FileName)
        {
            try
            {
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return false;
        }
    }

}


  
