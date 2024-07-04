using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Deffinity.Utils.Printing
{
    /// <summary>
    /// Summary description for EmailTemplate.
    /// </summary>
    public class EmailTemplate
    {
        public EmailTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Prints any Control i.e. User Control, DataGrid, Page , Panel etc.
        /// </summary>
        /// <param name="ctrl">Control to be printed</param>
        /// <param name="StyleText">Style to be added in the Head Section</param>
        public static void PrintWebControl(Control ctrl)
        {
            PrintWebControl(ctrl, string.Empty);
        }
        /// <summary>
        /// Prints any Control i.e. User Control, DataGrid, Page , Panel etc.
        /// </summary>
        /// <param name="ctrl">Control to be printed</param>
        public static void PrintWebControl(Control ctrl, string Script)
        {
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(50, UnitType.Pixel);
                ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();

            if (Script != string.Empty)
            {
                pg.RegisterStartupScript("PrintJavaScript", Script);
            }
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            string scr = "<script>function window.onafterprint(){history.back(1);}</script>";
            htmlWrite.Write(scr);


            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);

            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);
            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();
        }
    }
}

