using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Deffinity.BE
{
	/// <summary>
	/// Table RFI_SubSection
	/// 
	/// Generated by matricrix's C# Layer Builder
	/// 23/06/2009 17:12:29
	/// </summary>
	public class RFI_SubSection
	{
		private int _subsectionid;
		private string _subsectionname;
        private int _subsectiontype;
        private int _projectreference;
        private int _worksheetid;
		/// <summary>
		/// Column RFI_SubSection.SubSectionID
		/// </summary>
		public int SUBSECTIONID
		{
			get { return this._subsectionid; }
			set { this._subsectionid = value; }
		}
		
		/// <summary>
		/// Column RFI_SubSection.SubSectionName
		/// </summary>
		public string SUBSECTIONNAME
		{
			get { return this._subsectionname; }
			set { this._subsectionname = value; }
		}

        /// <summary>
        /// Column RFI_SubSection.SubSectionType
        /// </summary>
        public int SUBSECTIONTYPE
        {
            get { return this._subsectiontype; }
            set { this._subsectiontype = value; }
        }

        /// <summary>
        /// Column RFI_SubSection.ProjectReference
        /// </summary>
        public int PROJECTREFERENCE
        {
            get { return this._projectreference; }
            set { this._projectreference = value; }
        }
        /// <summary>
        /// Column RFI_SubSection.WorksheetID
        /// </summary>
        public int WORKSHEETID
        {
            get { return this._worksheetid; }
            set { this._worksheetid = value; }
        }

		/// <summary>
		/// Entity properties
		/// </summary>
		/// <returns>Collection properties</returns>
		public System.Data.SqlClient.SqlParameter[] ItemParameter()
		{
			List<System.Data.SqlClient.SqlParameter> oParameters = new List<System.Data.SqlClient.SqlParameter>();
			oParameters.Add(new System.Data.SqlClient.SqlParameter("@SUBSECTIONID", this._subsectionid));
			oParameters.Add(new System.Data.SqlClient.SqlParameter("@SUBSECTIONNAME", this._subsectionname));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@SUBSECTIONTYPE", this._subsectiontype));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@PROJECTREFERENCE", this._projectreference));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@WORKSHEETID", this._worksheetid));
			return oParameters.ToArray();
		}
	}
}
