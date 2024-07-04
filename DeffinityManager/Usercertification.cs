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
/// Summary description for Usercertification
/// </summary>
namespace Certifications
{
    public class Usercertification
    {
        public Usercertification()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private int _certificationid;
        private int _vendorid;
        private string _certification;
        private DateTime? _certifiedfrom;
        private DateTime? _certificateexpiry;
        private Guid _certificateimage;

        /// <summary>
        /// Column RFI_VendorCertification.CertificationID
        /// </summary>
        public int CERTIFICATIONID
        {
            get { return this._certificationid; }
            set { this._certificationid = value; }
        }

        /// <summary>
        /// Column RFI_VendorCertification.VendorID
        /// </summary>
        public int VENDORID
        {
            get { return this._vendorid; }
            set { this._vendorid = value; }
        }

        /// <summary>
        /// Column RFI_VendorCertification.Certification
        /// </summary>
        public string CERTIFICATION
        {
            get { return this._certification; }
            set { this._certification = value; }
        }

        /// <summary>
        /// Column RFI_VendorCertification.CertifiedFrom
        /// </summary>
        public DateTime? CERTIFIEDFROM
        {
            get { return this._certifiedfrom; }
            set { this._certifiedfrom = value; }
        }

        /// <summary>
        /// Column RFI_VendorCertification.CertificateExpiry
        /// </summary>
        public DateTime? CERTIFICATEEXPIRY
        {
            get { return this._certificateexpiry; }
            set { this._certificateexpiry = value; }
        }

        /// <summary>
        /// Column RFI_VendorCertification.CertificateImage
        /// </summary>
        public Guid CERTIFICATEIMAGE
        {
            get { return this._certificateimage; }
            set { this._certificateimage = value; }
        }


        /// <summary>
        /// Entity properties
        /// </summary>
        /// <returns>Collection properties</returns>
        public System.Data.SqlClient.SqlParameter[] ItemParameter()
        {
            List<System.Data.SqlClient.SqlParameter> oParameters = new List<System.Data.SqlClient.SqlParameter>();
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFICATIONID", this._certificationid));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@VENDORID", this._vendorid));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFICATION", this._certification));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFIEDFROM", (this._certifiedfrom)));// == null ? string.Empty : this._certifiedfrom)));
            //oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFICATEEXPIRY", (this._certificateexpiry == null ? System.DBNull.Value : this._certificateexpiry)));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFICATEEXPIRY", (this._certificateexpiry)));
            oParameters.Add(new System.Data.SqlClient.SqlParameter("@CERTIFICATEIMAGE", (this._certificateimage == null ? Guid.Empty : this._certificateimage)));
            return oParameters.ToArray();
        }

        public static int InsertUser(Usercertification oRFI_VendorCertification)
        {
            return Insert_User(oRFI_VendorCertification);
        }
        public static int Insert_User(Usercertification oRFI_VendorCertification)
        {
            return SqlSrv.SqlHelper.ExecuteNonQuery(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.Contractor_VENDORCERTIFICATION_INSERT", oRFI_VendorCertification.ItemParameter());
        }

        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        //public static DataTable Fill(int vendorID)
        //{
        //    return Usercertification.Fill(vendorID);

        //    //Deffinity.BLL.RFI_VendorCertification_SVC
        //}
        public static DataTable Fill(int ContractorID)
        {
            DataSet dsRFI_VendorCertification = SqlSrv.SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "dbo.DN_User_FILLBYVENDORID", new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ContractorID", ContractorID) });
            return dsRFI_VendorCertification.Tables[0];
        }

       
    }
}