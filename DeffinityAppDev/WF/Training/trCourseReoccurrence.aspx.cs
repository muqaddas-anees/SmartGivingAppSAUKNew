using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
public partial class Training_trCourseReoccurrence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.InnerText = "Course Reoccurrence";
       // Master.PageHead = "Training Management";
        if (!IsPostBack)
        {
            BindReoccurrenceFrequencey();
        }
        for (int i = 0; i < chkDays.Items.Count; i++)
        {
            chkDays.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
        }
    }
    private void BindReoccurrenceFrequencey()
    {
        ddlReoccursFrequencey.DataSource = ReoccurrenceFrequencey.SelectAll(true);
        ddlReoccursFrequencey.DataValueField = "value";
        ddlReoccursFrequencey.DataTextField = "text";
        ddlReoccursFrequencey.DataBind();
    }

    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {

    }
}
