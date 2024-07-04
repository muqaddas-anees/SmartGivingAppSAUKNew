using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Deffinity.BLL;
//using Deffinity.DocumentSearch;
using System.Text;

public partial class POPCheckin :BasePage
{

    //protected void Page_Unload(object sender, EventArgs e)
    //{
        
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string id = Request.QueryString["id"];        
        string checkIn = Request.QueryString["mode"];

        lblMsg.Visible = false;
        if (!IsPostBack)
        {

            if (checkIn == "false")
            {
                

                AC2P_DocumentsController _AC2P_DocumentsController = new AC2P_DocumentsController();
                _AC2P_DocumentsController.DocumentJournalInsert(Convert.ToInt32(id), sessionKeys.PortfolioID, sessionKeys.ContractorId);

                PnlFileUpload.Visible = false;
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID={0}", id);
                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HttpContext.Current.Response.ContentType = reader.GetString(0);
                                byte[] getContent = (byte[])reader[1];
                                HttpContext.Current.Response.BinaryWrite(getContent);
                                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + reader.GetString(2).Replace(" ", string.Empty));                                
                                HttpContext.Current.Response.End();
                            }
                            else
                            {

                                //lblMsg.Text = "The document is not found.  Possible reasons may be the other users have deleted the document you are looking for.";
                            }
                        }
                    }
                }
            }

            else
            {
                PnlFileUpload.Visible = true;
                PnlFileDownload.Visible = false;
            }
        }
    }
    protected void UpdateAndClose()
    {
        
        string id = Request.QueryString["id"];
        CheckInOutUpdate(false, id);

        string scriptString = "<script language='JavaScript'> " + "window.opener.document.forms(0).submit(); window.close(); </script>";
        if (!Page.ClientScript.IsClientScriptBlockRegistered(scriptString))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", scriptString);
            
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //string id = Request.QueryString["id"];   
        //AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
        //string strDocumentName;
        //strDocumentName=AC2PDocumentsController.GetAC2PGetDocumentName( Convert.ToInt32(id));
        bool blnFile=false;
        blnFile = blnFileExists();
        if (blnFile)
        {
            UploadDocuments();
            UpdateAndClose();
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = Resources.DeffinityRes.Upld_Valid_File;//"Please upload the valid file...";
        }
    }

    private bool blnFileExists()
    {

        bool blnFile=false;
        string id = Request.QueryString["id"];
        AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
        string strDocumentName=string.Empty;
        string strDocumentNameCheckin = string.Empty;
        
        
        HttpFileCollection hfc = Request.Files;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength > 0)
            {

               strDocumentNameCheckin= System.IO.Path.GetFileName(hpf.FileName);

            }
        }

        if (strDocumentNameCheckin.Trim() != "")
        {

            strDocumentName = AC2PDocumentsController.GetAC2PGetDocumentName(Convert.ToInt32(id));

            if (strDocumentNameCheckin.Trim() == strDocumentName.Trim())
            {
                blnFile = true;
            }
        }
        return blnFile;   
    }
    private void UploadDocuments()
    {

        //string FileName = System.IO.Path.GetFileName(hpf.FileName);

        string folderID = Request.QueryString["folder"];

        try
        {
            // Get the HttpFileCollection
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {                    
                   
                    byte[] myFileData = new byte[hpf.ContentLength];
                    hpf.InputStream.Read(myFileData, 0, hpf.ContentLength);
                    AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();                    
                    AC2PDocumentsController.DN_ProjectUploadInsertNew(sessionKeys.Project.ToString(), System.IO.Path.GetFileName(hpf.FileName), myFileData, System.IO.Path.GetFileName(hpf.FileName), hpf.ContentType, "P", hpf.ContentLength, Convert.ToInt32(folderID), sessionKeys.UID, sessionKeys.IncidentID);
                    
                }
            }     
            //Infragistics.WebUI.UltraWebNavigator.Node SelectedFolder = null;
            //SelectedFolder = UltraWebTree1.SelectedNode;
            //SelectedFolder.Text = hfc.Count.ToString();
        }
        catch (Exception ex)
        {

        }


    }

    

    private void CheckInOutUpdate(bool check, string CommandArgumentID)
    {
        //lblMsg.Text = string.Empty;
        string strDeleteIDS = string.Empty;
        int intChecked = 0;
        //foreach (GridViewRow dgi in gridFiles.Rows)
        //{
        //    CheckBox chkChecked = (CheckBox)dgi.FindControl("chkChecked");
        //    if (chkChecked.Checked)
        //    {
        //        intChecked++;
        //        Label lblID = (Label)dgi.FindControl("lblID");
        //        if (strDeleteIDS == string.Empty)
        //        {
        //            strDeleteIDS = lblID.Text.Trim() + ",";
        //        }
        //        else
        //        {
        //            strDeleteIDS = strDeleteIDS + lblID.Text.Trim() + ",";
        //        }
        //    }
        //}

        strDeleteIDS = CommandArgumentID + ",";
        if (strDeleteIDS != string.Empty)
        {
            AC2P_DocumentsController ObjAC2P_DocumentsController = new AC2P_DocumentsController();

            int intRet = ObjAC2P_DocumentsController.AC2PDocumentsCheckOut(strDeleteIDS, check, sessionKeys.UID);

            if (check == true)
            {
                //lblMsg.Text = intChecked.ToString() + " file(s) are Checked Out.";
            }
            else
            {
                //lblMsg.Text = intChecked.ToString() + " file(s) are Checked In.";
            }
        }
        else
        {
            if (check == true)
            {
                //lblMsg.Text = "Plese select file(s) to Check Out.";
            }
            else
            {
                //lblMsg.Text = "Plese select file(s) to Check In.";
            }
        }
    }
}
