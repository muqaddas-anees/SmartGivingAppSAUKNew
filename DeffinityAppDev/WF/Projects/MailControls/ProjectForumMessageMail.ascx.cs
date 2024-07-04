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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectTasksManagers;


public partial class MailControls_ProjectForumMessageMail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void setdata(int ProjectReference, string Message, string ReciverName,int ForumID,string ForumTitle,string RaisedBy)
    {        
        BindData(ProjectReference, Message, ReciverName,ForumID,ForumTitle,RaisedBy);
    }
    private void BindData(int project, string message, string reciverName,int ForumID,string ForumTitle,string RasideBy)
    {
        ArrayList ac2pid_list = new ArrayList();
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetAC2PID", new SqlParameter("@ProjectReference", project));
        while (dr.Read())
        {
            ac2pid_list.Add(new AC2PID_details(message, int.Parse(dr["ContractorID"].ToString()), dr["ContractorName"].ToString()));
        }
        dr.Close();

        foreach (AC2PID_details details in ac2pid_list)
        {
            lblMessage.InnerText = message;
            //lblSender.InnerText = sessionKeys.UName;
            lblReciver.InnerText = reciverName;
        }
        lblForumTheadTitle.InnerText = ForumTitle;
        lblRasidedBy.InnerText = RasideBy;
        lblProjectRef.InnerText = sessionKeys.Prefix + project.ToString();
        //footer url
        //linkWebsite.Text = "Please click";//System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

        string MainString = Request.Url.ToString();
        string[] Split = MainString.Split(new Char[] { '/', '.' });

        linkReplay.HRef = "mailto:info@deffinity.com?subject=" + Split[2]+ "/" + sessionKeys.Prefix + project.ToString() + "/" + ForumID.ToString() ;
        //lblProjectReference.InnerText = sessionKeys.Prefix + project.ToString();
    }
    #region AC2PID class
    public class AC2PID_details
    {
        string _Message;
        int _ContractorID;
        string _ContractorName;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public int ContractorID
        {
            get { return _ContractorID; }
            set { _ContractorID = value; }
        }
        public string ContractorName
        {
            get { return _ContractorName; }
            set { _ContractorName = value; }
        }

        public AC2PID_details(string  a_Message, int a_ContractorID, string a_ContractorName)
        {
            Message = a_Message;
            ContractorID = a_ContractorID;
            ContractorName = a_ContractorName;
        }

        //public override string ToString()
        //{
        //    return
        //      String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
        //      itemDescription, startdate, enddate);
        //}

    }
    #endregion
}
