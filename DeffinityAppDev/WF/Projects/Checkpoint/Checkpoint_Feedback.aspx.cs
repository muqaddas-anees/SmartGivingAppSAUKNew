using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class Checkpoint_Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //Master.PageHead = "Feed back";
        }
    }

    [System.Web.Services.WebMethod]
    public static object DataOfFeedBackGraph(string Cid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            List<FeedbackCls> FBDataClsList = new List<FeedbackCls>();
            FeedbackCls D_Cls = null;
            DataTable dt = new DataTable();
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand myCommand = new SqlCommand("DEFFINITY_RES_FEEDBACKGRAPH", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@ContractorID", int.Parse(Cid));
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                D_Cls = new FeedbackCls();
                D_Cls.state = dr["date"].ToString();
                D_Cls.Timeliness = Convert.ToDouble(dr["Timeliness"].ToString());
                D_Cls.QualityofWork = Convert.ToDouble(dr["QualityofWork"].ToString());
                D_Cls.ValueforMoney = Convert.ToDouble(dr["ValueforMoney"].ToString());
                D_Cls.Communication = Convert.ToDouble(dr["Communication"].ToString());
                FBDataClsList.Add(D_Cls);
            }
            return jsonSerializer.Serialize(FBDataClsList).ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
}
public class FeedbackCls
{
    public string state { get; set; }
    public double Timeliness { get; set; }
    public double QualityofWork { get; set; }
    public double ValueforMoney { get; set; }
    public double Communication { get; set; }
}
