using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
//using System.Text;

public partial class DC_controls_CustomFormCtrl1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void CustomFormBuilder()
    {
        var fieldList = CustomFormDesignerBAL.GetFieldList(11,0);


        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table>");
        sb.Append("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>");
        sb.Append("<tr><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>");
        sb.Append("<tr><td>{10}</td><td>{11}</td><td>{12}</td><td>{13}</td><td>{14}</td></tr>");
        sb.Append("<tr><td>{15}</td><td>{16}</td><td>{17}</td><td>{18}</td><td>{19}</td></tr>");
        sb.Append("<tr><td>{20}</td><td>{21}</td><td>{22}</td><td>{23}</td><td>{24}</td></tr>");
        sb.Append("</table>");
        ph.Controls.Add(new LiteralControl(sb.ToString()));

       
    }

}