using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class controls_CheckpointFeedback : System.Web.UI.UserControl 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblmsg.Visible = false;
        //add css dynamically
        //HtmlLink css = new HtmlLink();
        //css.Href = ResolveClientUrl("~/App_Themes/Default/Ratings.css");
        //css.Attributes["rel"] = "stylesheet";
        //css.Attributes["type"] = "text/css";
        ////css.Attributes["media"] = "all";
        //Page.Header.Controls.Add(css);
    }

    protected void btnSubmitHours_Click(object sender, EventArgs e)
    {
        insertFeedback();

        //DEFFINITY_FEEDBACK
        //int i = RatingTime.CurrentRating;
        //Response.Write(i.ToString());
    }
    protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlResource.SelectedValue) != 0)
            GetFeedback();
        else
        {
            RatingTime.CurrentRating = 0;
            RatingQuality.CurrentRating = 0;
            RatingMoney.CurrentRating = 0;
            RatingCommunication.CurrentRating = 0;
        }
    }
    private void GetFeedback()
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_GETFEEDBACK");
        db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, QueryStringValues.Project);
        db.AddInParameter(cmd, "@CONTRACTORID", DbType.Int32, Convert.ToInt32(ddlResource.SelectedValue));
        db.AddInParameter(cmd, "@PMID", DbType.Int32, sessionKeys.UID);
        db.AddOutParameter(cmd, "@TIMELINESS", DbType.Int32, RatingTime.CurrentRating);
        db.AddOutParameter(cmd, "@QUALITYOFWORK", DbType.Int32, RatingQuality.CurrentRating);
        db.AddOutParameter(cmd, "@VALUEFORMONEY", DbType.Int32, RatingMoney.CurrentRating);
        db.AddOutParameter(cmd, "@COMMUNICATION", DbType.Int32, RatingCommunication.CurrentRating);
        db.ExecuteNonQuery(cmd);
        RatingTime.CurrentRating = (int)db.GetParameterValue(cmd, "TIMELINESS");
        RatingQuality.CurrentRating = (int)db.GetParameterValue(cmd, "QUALITYOFWORK");
        RatingMoney.CurrentRating = (int)db.GetParameterValue(cmd, "VALUEFORMONEY");
        RatingCommunication.CurrentRating = (int)db.GetParameterValue(cmd, "COMMUNICATION");
        cmd.Dispose();
    }
    private void insertFeedback()
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_FEEDBACK");
            //add parameters

            db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, QueryStringValues.Project);
            db.AddInParameter(cmd, "@CONTRACTORID", DbType.Int32, Convert.ToInt32(ddlResource.SelectedValue));
            db.AddInParameter(cmd, "@PMID", DbType.Int32, sessionKeys.UID);
            db.AddInParameter(cmd, "@TIMELINESS", DbType.Int32, RatingTime.CurrentRating);
            db.AddInParameter(cmd, "@QUALITYOFWORK", DbType.String, RatingQuality.CurrentRating);
            db.AddInParameter(cmd, "@VALUEFORMONEY", DbType.Int32, RatingMoney.CurrentRating);
            db.AddInParameter(cmd, "@COMMUNICATION", DbType.Int32, RatingCommunication.CurrentRating);

            db.ExecuteNonQuery(cmd);
            lblmsg.Text = "Feedback saved";
            lblmsg.Visible = true;
            cmd.Dispose();

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.Visible = true;
            //error = ex.Message.ToString();
        }



    }

    

}

