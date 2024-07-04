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


public partial class MailControls_ProjectTasklist : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
     
    }
    public void setdata(int ProjectReference,int AC2PID,string ReciverName)
    {
        ProjectDetails1.SetData(ProjectReference);
        //get AC2PID list for a project
        GridBind(ProjectReference,AC2PID,ReciverName);
    }
    private void GridBind(int project,int ac2pid,string reciverName)
    {
        ArrayList ac2pid_list = new ArrayList();
        SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetAC2PID", new SqlParameter("@ProjectReference", project));
        while (dr.Read())
        {
            ac2pid_list.Add(new AC2PID_details(int.Parse(dr["ID"].ToString()), int.Parse(dr["ContractorID"].ToString()), dr["ContractorName"].ToString()));
        }
        dr.Close();

       
        foreach (AC2PID_details details in ac2pid_list)
        {

            Gridview1.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectProjItmes", new SqlParameter("@AC2PID", ac2pid), new SqlParameter("@Project", project)).Tables[0];
            Gridview1.DataBind();
            lblSender.InnerText = sessionKeys.UName;
            lblReciver.InnerText = reciverName;
        }

        //footer url
        linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        lblProjectReference.InnerText = sessionKeys.Prefix + project.ToString();
    }
    protected string getItemDes(string indent, string desc)
    {
        return ProjectTasksManager.DisplayIndentLevel(desc, Convert.ToInt32(string.IsNullOrEmpty(indent) ? "0" : indent));
    }
    #region properties
    public string Sender
    {
        get { return lblSender.InnerText; }
        set { lblSender.InnerText = value; }
    }
    public string Receiver
    {
        get { return lblReciver.InnerText; }
        set { lblReciver.InnerText = value; }
    }
    public string ProjectReference
    {
        get { return lblProjectReference.InnerText; }
        set { lblProjectReference.InnerText = value; }

    }
    #endregion

    #region AC2PID class
    public class AC2PID_details
    {
        int _AC2PID ;
        int _ContractorID;
        string _ContractorName ;

        public int AC2PID
        {
            get { return _AC2PID; }
            set { _AC2PID = value; }
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

        public AC2PID_details(int a_AC2PID, int a_ContractorID, string a_ContractorName)
        {
            AC2PID = a_AC2PID;
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
