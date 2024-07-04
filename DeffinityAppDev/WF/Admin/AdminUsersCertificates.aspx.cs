using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.BE;
using Deffinity.BLL;
using Certifications;
using VT.Entity;
using VT.DAL;
using System.Text;
//using Deffinity.TrainingEntity;
//using Deffinity.TrainingManager;
using System.Collections.Generic;

public partial class AdminUsersCertificates : System.Web.UI.Page
{
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Admin";
       
        if (!this.IsPostBack)
        {
            SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
            getUserId.Value = Request.QueryString["uid"];
           
        }

       
    }

    protected void imgBtnUpload_Click(object sender, EventArgs e)
    {

        

        try
        {
            Usercertification _rfiVCertificate = new Usercertification();
            User_Documents _rfiDocument = new User_Documents();
            if (FileUploadCertificate.HasFile)
            {
            Guid _guid = Guid.NewGuid();
            _rfiVCertificate.CERTIFICATEEXPIRY = DateTime.Now;// Convert.ToDateTime(txtCertificationExp.Text.Trim());
            _rfiVCertificate.CERTIFICATION = FileUploadCertificate.FileName;// txtCertificate.Text.Trim();
            _rfiVCertificate.CERTIFIEDFROM = DateTime.Now; //Convert.ToDateTime(txtCertifiedFrom.Text.Trim());
            _rfiVCertificate.VENDORID = Convert.ToInt32(getUserId.Value);
            _rfiVCertificate.CERTIFICATEIMAGE = _guid;
          
                HttpPostedFile _h = FileUploadCertificate.PostedFile;
                _rfiDocument.DOCUMENT = FileUploadCertificate.FileBytes;
                _rfiDocument.DOCUMENTNAME = FileUploadCertificate.FileName;
                _rfiDocument.DOCUMENTTYPE = _h.ContentType;
                _rfiDocument.ID = _guid;

                User_Documents.UserInsert_Document(_rfiDocument);


            }
            Usercertification.InsertUser(_rfiVCertificate);
            //  Usercertification.Fill(
            GridViewCertification.DataBind();
            clear();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {

        }



    }
    public void clear()
    {
        txtCertificate.Text = "";

        txtCertifiedFrom.Text = "";

        txtCertificationExp.Text = "";
    }
    private void DownLoadDoc(Guid _guid)
    {
        User_Documents _rdoc = User_Documents.Select(_guid);
        string path = Server.MapPath(@"Documentation\\");

        Response.ContentType = _rdoc.DOCUMENTTYPE;//"application/ms-word";
        byte[] getContent = (byte[])_rdoc.DOCUMENT;
        Response.BinaryWrite(getContent);
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; FileName=" + _rdoc.DOCUMENTNAME);
        Response.End();

    }
    protected void GridViewCertification_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            Guid _g = new Guid(e.CommandArgument.ToString());
            DownLoadDoc(_g);
        }
    }
    protected void Imgbtn_Cancel12_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    lblcertificateUserName.Text = dr["ContractorName"].ToString();
                    lblusername.Text = dr["ContractorName"].ToString();

                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }
}
