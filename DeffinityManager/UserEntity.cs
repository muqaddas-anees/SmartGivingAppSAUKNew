using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Deffinity.Users
{
	public class UserEntity
	{
		#region Member Variables

		string _contractorName;
		string _loginName;
		string _password;
		string _status;
		string _details;
		string _type;
		string _emailAddress;
		string _company;
		string _contactNumber;
        int _id;
		int _sID;
		int _groupOwnerID;
		int _timeApproveID;
		int _expClassification;
		double _normalBuyingRate;
		double _normalSellingRate;
		double _overtimeBuyingRate;
		double _overtimeSellingRate;
        DateTime _employmentStartDate, _releasedate;

		#endregion

		#region Properties

		public string ContractorName
		{
			set { _contractorName = value; }
			get { return _contractorName; }
		}

		public string LoginName
		{
			set { _loginName = value; }
			get { return _loginName; }
		}

		public string Password
		{
			set { _password = value; }
			get { return _password; }
		}

		public string Status
		{
			set { _status = value; }
			get { return _status; }
		}

		public string Details
		{
			set { _details = value; }
			get { return _details; }
		}

		public string Type
		{
			set { _type = value; }
			get { return _type; }
		}

		public string EmailAddress
		{
			set { _emailAddress = value; }
			get { return _emailAddress; }
		}

		public string Company
		{
			set { _company = value; }
			get { return _company; }
		}

		public string ContactNumber
		{
			set { _contactNumber = value; }
			get { return _contactNumber; }
		}

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

		public int SID
		{
			set { _sID = value; }
			get { return _sID; }
		}

		public int GroupOwnerID
		{
			set { _groupOwnerID = value; }
			get { return _groupOwnerID; }
		}

		public int TimeApproveID
		{
			set { _timeApproveID = value; }
			get { return _timeApproveID; }
		}

		public int ExpClassification
		{
			set { _expClassification = value; }
			get { return _expClassification; }
		}

		public double NormalBuyingRate
		{
			set { _normalBuyingRate = value; }
			get { return _normalBuyingRate; }
		}

		public double NormalSellingRate
		{
			set { _normalSellingRate = value; }
			get { return _normalSellingRate; }
		}

		public double OvertimeBuyingRate
		{
			set { _overtimeBuyingRate = value; }
			get { return _overtimeBuyingRate; }
		}

		public double OvertimeSellingRate
		{
			set { _overtimeSellingRate = value; }
			get { return _overtimeSellingRate; }
		}

		public DateTime EmploymentStartDate
		{
			set { _employmentStartDate = value; }
			get { return _employmentStartDate; }
		}
        public DateTime ReleaseDate
		{
			set { _releasedate = value; }
			get { return _releasedate; }
		}
        
		#endregion
		
	}

    public class UserManager
    {
        public static int Method_Insert(UserEntity UE)
        {

            SqlParameter OutStatus = new SqlParameter("@OutStatus", SqlDbType.Int, 4);
            OutStatus.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_InsertUser",
                new SqlParameter("@ContractorName", UE.ContractorName),
                new SqlParameter("@LoginName", UE.LoginName),
                new SqlParameter("@Password", UE.Password),
                new SqlParameter("@SID", UE.SID),
                new SqlParameter("@Status", UE.Status),
                new SqlParameter("@Details", UE.Details),
                new SqlParameter("@EmailAddress", UE.EmailAddress),
                new SqlParameter("@TimeApproveID", UE.TimeApproveID),
                new SqlParameter("@NormalBuyingRate", UE.NormalBuyingRate),
                new SqlParameter("@NormalSellingRate", UE.NormalSellingRate),
                new SqlParameter("@OvertimeBuyingRate", UE.OvertimeBuyingRate),
                new SqlParameter("@OvertimeSellingRate", UE.OvertimeSellingRate),
                new SqlParameter("@Company", UE.Company),
                new SqlParameter("@ContactNumber", UE.ContactNumber),
                new SqlParameter("@ReleaseDate", UE.ReleaseDate),
                new SqlParameter("@EmploymentStartDate", UE.EmploymentStartDate),
                new SqlParameter("@ExpClassification", UE.ExpClassification),
                OutStatus);

            return int.Parse(OutStatus.Value.ToString());

        
        }
        public static void Method_update_email_no(UserEntity UE)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Update Contractors set ContactNumber=@ContactNumber,EmailAddress=@EmailAddress where ID=@ID", new SqlParameter("@ID", UE.ID), new SqlParameter("@ContactNumber", UE.ContactNumber), new SqlParameter("@EmailAddress", UE.EmailAddress));
        }
        public static int Method_Update(UserEntity UE)
        {
            SqlParameter OutStatus = new SqlParameter("@OutStatus", SqlDbType.Int, 4);
            OutStatus.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_UpdateUser",
                new SqlParameter("@ID",UE.ID),
                new SqlParameter("@ContractorName", UE.ContractorName),
                new SqlParameter("@LoginName", UE.LoginName),
                new SqlParameter("@Password", UE.Password),
                new SqlParameter("@SID", UE.SID),
                new SqlParameter("@Status", UE.Status),
                new SqlParameter("@Details", UE.Details),
                new SqlParameter("@EmailAddress", UE.EmailAddress),
                new SqlParameter("@TimeApproveID", UE.TimeApproveID),
                new SqlParameter("@NormalBuyingRate", UE.NormalBuyingRate),
                new SqlParameter("@NormalSellingRate", UE.NormalSellingRate),
                new SqlParameter("@OvertimeBuyingRate", UE.OvertimeBuyingRate),
                new SqlParameter("@OvertimeSellingRate", UE.OvertimeSellingRate),
                new SqlParameter("@Company", UE.Company),
                new SqlParameter("@ContactNumber", UE.ContactNumber),
                new SqlParameter("@ReleaseDate", UE.ReleaseDate),
                new SqlParameter("@EmploymentStartDate", UE.EmploymentStartDate),
                new SqlParameter("@ExpClassification", UE.ExpClassification),
                OutStatus);

            return int.Parse(OutStatus.Value.ToString());
        }

        public static UserEntity Method_Select(int ID)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_SelectResource",
                                                                                        new SqlParameter("@ID", ID));
            UserEntity UE = new UserEntity();
            while (dr.Read())
            {
                UE.Company = dr["Company"].ToString();
                UE.ContactNumber = dr["ContactNumber"].ToString();
                UE.ContractorName = dr["ContractorName"].ToString();
                UE.Details = dr["Details"].ToString();
                UE.EmailAddress = dr["EmailAddress"].ToString();
                UE.EmploymentStartDate = DateTime.Parse(string.IsNullOrEmpty(dr["EmploymentStartDate"].ToString()) ? "01/01/1900" : dr["EmploymentStartDate"].ToString());
                UE.ExpClassification = int.Parse(string.IsNullOrEmpty(dr["ExpClassification"].ToString()) ? "0" : dr["ExpClassification"].ToString());
                UE.GroupOwnerID = int.Parse(string.IsNullOrEmpty(dr["GroupOwnerID"].ToString()) ? "0" : dr["GroupOwnerID"].ToString());
                UE.ID = int.Parse(dr["ID"].ToString());
                UE.LoginName = dr["LoginName"].ToString();
                UE.NormalBuyingRate = double.Parse(string.IsNullOrEmpty(dr["NormalBuyingRate"].ToString()) ? "0" : dr["NormalBuyingRate"].ToString());
                UE.NormalSellingRate = double.Parse(string.IsNullOrEmpty(dr["NormalSellingRate"].ToString()) ? "0" : dr["NormalSellingRate"].ToString());
                UE.OvertimeBuyingRate = double.Parse(string.IsNullOrEmpty(dr["OvertimeBuyingRate"].ToString()) ? "0" : dr["OvertimeBuyingRate"].ToString());
                UE.OvertimeSellingRate = double.Parse(string.IsNullOrEmpty(dr["OvertimeSellingRate"].ToString()) ? "0" : dr["OvertimeSellingRate"].ToString());
                UE.Password = dr["Password"].ToString();
                UE.ReleaseDate = DateTime.Parse(string.IsNullOrEmpty(dr["ReleaseDate"].ToString()) ? "01/01/1900" : dr["ReleaseDate"].ToString());
                UE.SID = int.Parse(dr["SID"].ToString());
                UE.Status = dr["Status"].ToString();
                UE.TimeApproveID = int.Parse(dr["TimeApproveID"].ToString());
                UE.Type = dr["Type"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return UE;
        
        }

        public static DataTable Method_SelectAdmin()
        {
            string key = CacheNames.DefaultNames.AdminUser.ToString();
            //if (BaseCache.Cache_Select(key) == null)
            //{
                BaseCache.Cache_Insert(key, Deffinity.Bindings.DefaultDatabind.UserSelectAll_Withselect(true));
           // }

            return BaseCache.Cache_Select(key) as DataTable;
        }



    }
}
