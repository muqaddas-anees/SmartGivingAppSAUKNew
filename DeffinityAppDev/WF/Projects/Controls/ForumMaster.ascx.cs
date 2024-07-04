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
using Deffinity.Bindings;
using Deffinity.ForumEntitys;
using Deffinity.ForumManager;
using System.IO;
using Deffinity.ProjectMangers;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


public partial class controls_ForumMaster : System.Web.UI.UserControl
{
    int temp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDatalist();
        }
    }
    protected void btnAddNewpost_Click(object sender, EventArgs e)
    {
        HiddenField_id.Value = temp.ToString();
        //change the visibility div page
        MessageEntryVisibility();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ForumMasterInsert();
        //chege visiblility
        MessageDisplayVisibility();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //clear fields
        ClearFields();
        MessageDisplayVisibility();
    }
    #region DataBinding functions
    private void ForumMasterInsert()
    {
        ForumMasterEntity fme = new ForumMasterEntity();
        
        fme.AuthorID = sessionKeys.UID;
        fme.Message = txtMessage.Text.Trim();
        fme.ProjectReference = GetProjectReferenceOrPortfolio();
        fme.Title = txtTitle.Text.Trim();
        fme.MsgType = false;
        fme.Ftype = GetForumType();
        int OutForumID ;
        if (HiddenField_id.Value =="0")
         {
             ForumManager.ForumMasterInsert(fme, out OutForumID);
             SendMail(fme.ProjectReference, fme.Message, OutForumID, txtTitle.Text.Trim(), sessionKeys.UName);
        }
        else
        {
            fme.ID = int.Parse(HiddenField_id.Value);
            ForumManager.ForumMasterUpdate(fme);
        }
        //insert new file
        InsertFile(int.Parse(HiddenField_id.Value));
        //clear fields
        ClearFields();
        //bind forum data
        BindDatalist();
        

    }
    private void InsertFile(int ForumID)
    {
        HttpFileCollection uploads = HttpContext.Current.Request.Files;
        for (int i = 0; i < uploads.Count; i++)
        {
            HttpPostedFile upload = uploads[i];

            if (upload.ContentLength == 0)
                continue;

            //string c = System.IO.Path.GetFileName(upload.FileName); // We don't need the path, just the name.
            string strFilename = System.IO.Path.GetFileName(upload.FileName);
            Stream imgStream = upload.InputStream;
            int imgLen = upload.ContentLength;
            string imgContentType = upload.ContentType;
            byte[] imgBinaryData = new byte[imgLen];
            int n = imgStream.Read(imgBinaryData, 0, imgLen);
            int length = upload.ContentLength;
            //insert or update to db
            WriteToDB(strFilename, upload.ContentType,length, ref imgBinaryData, ForumID);

            imgStream.Flush();
            imgStream.Flush();
            upload.InputStream.Flush();
            upload.InputStream.Close();
           
        }
    }
    private void WriteToDB(string strName, string strType,int length, ref byte[] Buffer,int ForumID)
    {
        try
        {
            int i = 0;
            //@ProjectReference int,@Document image ,@ContentType nvarchar(50)  ,@SourceFileName nvarchar(250),@DataSize int             
            //   ,@ApplicationSection char(1) ,@UserID int,@ForumID int = null  
            object[] FileObj = new object[] { QueryStringValues.Project, Buffer, strType, strName, length, "F", sessionKeys.UID, ForumID };
            ForumManager.ForumFileInsert_Update(FileObj);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }       

    }
    private void BindDatalist()
    {
        DataTable dt = ForumManager.b_ForumMaster(GetProjectReferenceOrPortfolio(),GetForumType());
        if (dt.Rows.Count > 0)
        {
            DataListFourmMaster.DataSource = dt;
            DataListFourmMaster.DataBind();
            DataListFourmMaster.Visible = true;
            DivNoitems.Visible = false;
        }
        else
        {
            DataListFourmMaster.Visible = false;
            DivNoitems.Visible = true;
        }
    }
    private void SelectOneMessage(int ID)
    { 
        
    }
    #endregion
    #region common functions
    private void ClearFields()
    {
        HiddenField_id.Value = "0";
        txtMessage.Text = string.Empty;
        txtTitle.Text = string.Empty;
    }
    private void MessageDisplayVisibility()
    {
        MessageDisplayPanle.Visible = true;
        MessagePanle.Visible = false;
    }
    private void MessageEntryVisibility()
    {
        MessageDisplayPanle.Visible = false;
        MessagePanle.Visible = true;
    }
    #endregion

    protected void DataListFourmMaster_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
         
        if (e.Item.ItemType ==  ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRow row = e.Item.DataItem as DataRow;
            LinkButton btnForumItemLink = e.Item.FindControl("btnForumItemLink") as LinkButton;
            btnForumItemLink.CommandName = "Thread";
            btnForumItemLink.CommandArgument = (e.Item.FindControl("HidForumMaster")as HiddenField).Value;
        } 
    }
    protected void DataListFourmMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Thread")
        {
            ForumManager.ForumVisitorsUpdate(new object[] { e.CommandArgument,sessionKeys.UID });
            if (Request.Url.ToString().ToLower().Contains("customer"))
            {
                Response.Redirect(string.Format("~/WF/Projects/CustomerForumMsg.aspx?forumid={0}", e.CommandArgument.ToString()), false);
            }
            else if (Request.Url.ToString().ToLower().Contains("update"))
            {
                Response.Redirect(string.Format("~/WF/Projects/UpdateForumMsg.aspx?project={0}&AC2PID={1}&ContractorID={2}&forumid={3}", QueryStringValues.Project.ToString(), QueryStringValues.AC2PID.ToString(), sessionKeys.UID.ToString(), e.CommandArgument.ToString()), false);
            }
            else
            {
                Response.Redirect(string.Format("~/WF/Projects/ProjectForumMsg.aspx?project={0}&forumid={1}", QueryStringValues.Project.ToString(), e.CommandArgument.ToString()), false);
                
            }
        } 
    }
 //Get type of Forum
    private int GetForumType()
    {
        int _Forumtemp = 0;
          if (Request.Url.ToString().ToLower().Contains("customer"))
            {
                _Forumtemp = (int)ForumMasterEntity.Fourmtype.Portfolio;
            }
            else
            {
                _Forumtemp = (int)ForumMasterEntity.Fourmtype.Project;
            }
          return _Forumtemp;
    }
    //Get type of Forum reference
    private int GetProjectReferenceOrPortfolio()
    {
        int _ProjectReferenceOrPortfolio = 0;
        if (Request.Url.ToString().ToLower().Contains("customer"))
        {
            _ProjectReferenceOrPortfolio = sessionKeys.PortfolioID;
        }
        else
        {
            _ProjectReferenceOrPortfolio = QueryStringValues.Project;
        }
        return _ProjectReferenceOrPortfolio;
    }
    #region send Mail
    private void SendMail(int project,string message,int ForumID,string Title,string RaisedBy)
    {
        string messageformatted = ClearHTMLTags(message, 0);
        try
        {
            ArrayList ac2pid_list = new ArrayList();
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetAC2PID", new SqlParameter("@ProjectReference", project));
            while (dr.Read())
            {
                ac2pid_list.Add(new AC2PID_details(message, int.Parse(dr["ContractorID"].ToString()), dr["ContractorName"].ToString(), dr["EmailAddress"].ToString()));
            }
            dr.Close();

            ProjectForumMessageMail1.Visible = true;
            foreach (AC2PID_details details in ac2pid_list)
            {
                ProjectForumMessageMail1.setdata(project, messageformatted, details.ContractorName, ForumID, Title, RaisedBy);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                ProjectForumMessageMail1.RenderControl(htmlWrite);
                Email ToEmail = new Email();
                ToEmail.SendingMail(details.ContractorEmail, "Forum", htmlWrite.InnerWriter.ToString());
            }
            ProjectForumMessageMail1.Dispose();
            ProjectForumMessageMail1.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            ProjectForumMessageMail1.Visible = false;
        }
    }    
#endregion
    #region AC2PID class
    public class AC2PID_details
    {
        string  _Message;
        int _ContractorID;
        string _ContractorName;
        string _ContractorEmail;
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

        public string ContractorEmail
        {
            get { return _ContractorEmail; }
            set { _ContractorEmail = value; }
        }
        public AC2PID_details(string a_Message, int a_ContractorID, string a_ContractorName, string a_ContractorEmail)
        {
            Message = a_Message;
            ContractorID = a_ContractorID;
            ContractorName = a_ContractorName;
            ContractorEmail = a_ContractorEmail;
        }

        //public override string ToString()
        //{
        //    return
        //      String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
        //      itemDescription, startdate, enddate);
        //}

    }
    #endregion
    public string ClearHTMLTags(string strHTML, int intWorkFlow)
    {
        Regex regEx = null;
        string strTagLess = string.Empty; try
        {
            strTagLess = strHTML;//1. "remove html tags"

            if (intWorkFlow != 1)
            {

                //this pattern mathces any html tag
                regEx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
                strTagLess = regEx.Replace(strTagLess, "");
                //all html tags are stripped
            }//2. "remove rouge leftovers"// "or, I want to render the source"
            // "as html."
            //We *might* still have rouge < and > 
            //let's be positive that those that remain
            //are changed into html characters 

            if (intWorkFlow > 0 && intWorkFlow < 3)
            {
                regEx = new Regex("[<]", RegexOptions.IgnoreCase);//matches a single <
                strTagLess = regEx.Replace(strTagLess, "&lt;");
                regEx = new Regex("[>]", RegexOptions.IgnoreCase);//matches a single >
                strTagLess = regEx.Replace(strTagLess, "&gt;");
            }//3. return the stripped off text
            return strTagLess;
        }
        catch
        {
            throw;
        }
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //} 

}
