using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;


/// <summary>
/// Summary description for testEmail
/// </summary>
public class testEmail
{
   
    public static string SendTestMail(Control ctrl )
    {
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Page pg = new Page();
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        return stringWrite.ToString();

    }
  
}
