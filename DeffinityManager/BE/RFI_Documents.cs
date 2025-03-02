using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Deffinity.BE
{
	/// <summary>
	/// Table RFI_Documents
	/// 
	/// Generated by matricrix's C# Layer Builder
	/// 30/07/2009 15:30:31
	/// </summary>
	public class RFI_Documents
	{
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
	}
}
