using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using SqlSrv = Microsoft.ApplicationBlocks.Data;
using Deffinity;

/// <summary>
/// Summary description for User_Documents
/// </summary>
public class User_Documents
{
	public User_Documents()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private Guid _id;
    private string _documentname;
    private string _documenttype;
    private byte[] _document;

    /// <summary>
    /// Column RFI_Documents.ID
    /// </summary>
    public Guid ID
    {
        get { return this._id; }
        set { this._id = value; }
    }

    /// <summary>
    /// Column RFI_Documents.DocumentName
    /// </summary>
    public string DOCUMENTNAME
    {
        get { return this._documentname; }
        set { this._documentname = value; }
    }

    /// <summary>
    /// Column RFI_Documents.DocumentType
    /// </summary>
    public string DOCUMENTTYPE
    {
        get { return this._documenttype; }
        set { this._documenttype = value; }
    }

    /// <summary>
    /// Column RFI_Documents.Document
    /// </summary>
    public byte[] DOCUMENT
    {
        get { return this._document; }
        set { this._document = value; }
    }


    /// <summary>
    /// Entity properties
    /// </summary>
    /// <returns>Collection properties</returns>
    public System.Data.SqlClient.SqlParameter[] ItemParameter()
    {
        List<System.Data.SqlClient.SqlParameter> oParameters = new List<System.Data.SqlClient.SqlParameter>();
        oParameters.Add(new System.Data.SqlClient.SqlParameter("@ID", this._id));
        oParameters.Add(new System.Data.SqlClient.SqlParameter("@DOCUMENTNAME", this._documentname));
        oParameters.Add(new System.Data.SqlClient.SqlParameter("@DOCUMENTTYPE", this._documenttype));
        oParameters.Add(new System.Data.SqlClient.SqlParameter("@DOCUMENT", this._document));
        return oParameters.ToArray();
    }

    public static int UserInsert_Document(User_Documents oRFI_Documents)
    {
        return User_Insert(oRFI_Documents);
    }
    public static int User_Insert(User_Documents oRFI_Documents)
    {
        return SqlSrv.SqlHelper.ExecuteNonQuery(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DN_User_DOCUMENTS_INSERT", oRFI_Documents.ItemParameter());
    }

     [DataObjectMethod(DataObjectMethodType.Select, true)]
        

        public static User_Documents Select(Guid id)
		{
            DataSet dsRFI_Documents = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.User_DOCUMENTS_SELECT", new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID", id) });
			User_Documents oRFI_Documents = null;
			if (dsRFI_Documents.Tables[0].Rows.Count > 0)
				oRFI_Documents =Load(dsRFI_Documents.Tables[0].Rows[0]);
			return oRFI_Documents;
		}
        public static bool Exists(Guid id)
		{
            return Convert.ToInt32(SqlSrv.SqlHelper.ExecuteScalar(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DN_User_DOCUMENTS_EXISTS", new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID", id) })) > 0;
		}
        private static User_Documents Load(DataRow oRow)
		{
			User_Documents oReturn = new User_Documents();
			oReturn.ID = (Guid)oRow["ID"];
			oReturn.DOCUMENTNAME = (string)oRow["DOCUMENTNAME"];
			oReturn.DOCUMENTTYPE = (string)oRow["DOCUMENTTYPE"];
			oReturn.DOCUMENT = (byte[])oRow["DOCUMENT"];
			return oReturn;
		}
}
