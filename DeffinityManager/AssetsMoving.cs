using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Configuration;
using System.Collections;
namespace UserAssetsMovingSourecToDestination
{
    public class AssetsMoving
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        public AssetsMoving()
        {

        }

        #region DestinationProperties
        private int DestinationSite;
        private string DestinationNewOwner;
        private string DestinationBuilding;
        private string DestinationFloor;
        private string DestinationRoom;
        private string DestinationPort;
        private string DestinationIPAddress;
        private string DestinationSubnet;
        private string DestinationVLAN;
        private string Notes;
        private string DestinationLocation;

        public int destinationsite
        {
            get { return DestinationSite; }
            set { DestinationSite = value; }
        }

        public string destinationnew0wner
        {
            get { return DestinationNewOwner; }
            set { DestinationNewOwner = value; }
        }
        public string destinationlocation
        {
            get { return DestinationLocation; }
            set { DestinationLocation = value; }
        }


        public string destinationbuilding
        {
            get { return DestinationBuilding; }
            set { DestinationBuilding = value ; }
        }

        public string destinationfloor
        {
            get { return DestinationFloor; }
            set { DestinationFloor = value; }

        }
        public string destinationroom
        {
            get { return DestinationRoom; }
            set { DestinationRoom = value; }

        }

        public string destinationport
        {
            get { return DestinationRoom; }
            set { DestinationRoom = value; }

        }
        public string destinationipAddress
        {
            get { return DestinationIPAddress; }
            set { DestinationIPAddress = value; }

        }
        public string destinationsubnet
        {
            get { return DestinationSubnet; }
            set { DestinationSubnet = value; }

        }
        public string destinationvlan
        {
            get { return DestinationVLAN; }
            set { DestinationVLAN = value; }

        }
        public string notes
        {
            get { return Notes; }
            set { Notes = value; }

        }


        #endregion



        #region Search Assest

        #region Properties of Search Assest Grid
        private int attribute;
        private string textvalue = string.Empty;
        //here secarh variables

        public int Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        public string TextValue
        {
            get { return textvalue; }
            set { textvalue = value; }
        }

        #endregion

        public static string SearchAssestSpName = "DN_UserAssetsSearch";
        public DataSet GetSearchGrid(int AttributeValue, string TextValue,int AssignType)
        {

            DataSet SearchDS = new DataSet();

            cmd = db.GetStoredProcCommand(SearchAssestSpName);
            db.AddInParameter(cmd, "@ItemValue", DbType.String, TextValue);
            db.AddInParameter(cmd, "@Attribute", DbType.Int32, AttributeValue);
            db.AddInParameter(cmd, "@AssignType", DbType.Int32, AssignType);
            
            SearchDS = db.ExecuteDataSet(cmd);

            return SearchDS;

        }

        public int checkexistsAssets(string SerialNo, string AssetNO)
        {
            try
            {

                cmd = db.GetStoredProcCommand("DN_AC2PAssetScheduleCheckSerialNo1");
                db.AddInParameter(cmd, "@serialno", DbType.String, (string.Empty == SerialNo ? "*" : SerialNo));
                db.AddInParameter(cmd, "@assetno", DbType.String, (string.Empty == AssetNO ? "*" : AssetNO));
                db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd);
                int getVal = (int)db.GetParameterValue(cmd, "@output");
                cmd.Dispose();
                return getVal;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return 0;
            }

        }

        #endregion



        #region Search Serialnumber and Asstes number

        #region Proerties
        private string SerialNo = string.Empty;
        private string AssetNO = string.Empty;

        public string serialno
        {
            get { return SerialNo; }
            set { SerialNo = value; }
        }
        public string assetno
        {
            get { return AssetNO; }
            set { AssetNO = value; }
        }



        #endregion

        #region DataBaseConnection Retrivae values
        public DataTable GetAssetsValues(string Assestno, string Serialno,int ID)
        {
            DataTable GetAssest = new DataTable();

            if ((Assestno != "") && (Serialno == "") && (ID==0))
            {
                SqlCommand cmd = new SqlCommand("select *   from  Assets where AssetNo= '" + Assestno + "'", con);
                con.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                myadapter.Fill(GetAssest);
                cmd.Connection.Close();
            }

            else if ((Serialno != "") && (Assestno == "") && (ID == 0))
            {
                SqlCommand cmd = new SqlCommand("select *   from  Assets where serialno= '" + Serialno + "'", con);
                con.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                myadapter.Fill(GetAssest);
                cmd.Connection.Close();
            }

            else if((Serialno != "") && (Assestno != "") && (ID == 0))
            {
                SqlCommand cmd = new SqlCommand("select *   from  Assets where serialno= '" + Serialno + "'" + " and AssetNo= '" + Assestno + "'", con);
                con.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                myadapter.Fill(GetAssest);
                cmd.Connection.Close();
            }

            else if ((Serialno == "") && (Assestno == "") && (ID != 0))
            {
                SqlCommand cmd = new SqlCommand("select *   from  Assets where ID=" + ID, con);
                con.Open();
                SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                myadapter.Fill(GetAssest);
                cmd.Connection.Close();
            }


            return GetAssest;
        }



    

        #endregion


        #endregion

        #region SaveAssest And Moveto Destination

        public DataTable dt_ProjectAssets(int ProjectReference,int AssigType,int Assign_Project_Incidenttype)
        {
            DataTable dt;
            DataSet ds = new DataSet();
            cmd = db.GetStoredProcCommand("DN_AssetsMoveDisplayNew");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
            db.AddInParameter(cmd, "@AssignType", DbType.Int32, AssigType);
            db.AddInParameter(cmd, "@AssignName", DbType.Int32, Assign_Project_Incidenttype);
            ds = db.ExecuteDataSet(cmd);
            dt = ds.Tables[0];
            cmd.Dispose();
            return dt;
        }

        public int NewAssetSubmitBtn_Click(string[] nwAsset,int make)
        {
            int GetID = 0;

            cmd = db.GetStoredProcCommand("DN_InsertNewAssetsMove");

            #region SerialNo and AssetsNo --2


            db.AddInParameter(cmd, "@SerialNo", DbType.String, nwAsset[0]);
            db.AddInParameter(cmd, "@AssetNo", DbType.String, nwAsset[1]);
            #endregion

            #region ProjectReference
            if (nwAsset[2] != "ALL")
            {
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(nwAsset[2]));

            }
            else
            {
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));

            }
            #endregion

            #region Assets Types




            if (make == 0)
            {
                //it represents the null value to @make
                db.AddInParameter(cmd, "@Make", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@Make", DbType.Int32, Convert.ToInt32(nwAsset[3]));

            }
            if (nwAsset[3] == "")
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, Convert.ToInt32(nwAsset[3]));


            }

            if (nwAsset[4] == "")
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, Convert.ToInt32(nwAsset[4]));

            }

            #endregion


            #region From Fields


            if (nwAsset[5] == "")
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, Convert.ToInt32(nwAsset[5]));

            }

            db.AddInParameter(cmd, "@FromBuilding", DbType.String, nwAsset[7]);
            db.AddInParameter(cmd, "@FromFloor", DbType.String, nwAsset[8]);
            db.AddInParameter(cmd, "@FromRoom", DbType.String, nwAsset[9]);
            db.AddInParameter(cmd, "@FromLocation", DbType.String, nwAsset[10]);
            db.AddInParameter(cmd, "@FromPort", DbType.String, nwAsset[11]);
            db.AddInParameter(cmd, "@FromOwner", DbType.String, nwAsset[12]);
            db.AddInParameter(cmd, "@FromNotes", DbType.String, nwAsset[13]);
            db.AddInParameter(cmd, "@Technical", DbType.String, nwAsset[14]);
            db.AddInParameter(cmd, "@FromVLAN", DbType.String, nwAsset[15]);
            db.AddInParameter(cmd, "@FromIPAddress", DbType.String, nwAsset[16]);
            db.AddInParameter(cmd, "@FromSubnet", DbType.String, nwAsset[17]);
            if (nwAsset[17] == "")
            {
                db.AddInParameter(cmd, "@NewAsset", DbType.Int32, DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@NewAsset", DbType.Int32, Convert.ToInt32(nwAsset[17]));

            }
           
          
            #endregion


            #region Date and User Assign

            if (nwAsset[18] == null)
            {
                db.AddInParameter(cmd, "@userid", DbType.String, DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@userid", DbType.String, nwAsset[18]);
            }



            if (nwAsset[19] == "")
            {
                //db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);

                db.AddInParameter(cmd, "@Datemoved", DbType.DateTime,Convert.ToDateTime("1/1/1900"));

            }
            else
            {
                db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, Convert.ToDateTime(nwAsset[19]));

            }
            if (nwAsset[20] == "")

            {
               // db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
            }
            else
            {
                db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, Convert.ToDateTime(nwAsset[20]));

            }
             
            
            #endregion

            
            #region Destination Fields



            if (nwAsset[21] == "")
            {
                db.AddInParameter(cmd, "@ToSite", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@ToSite", DbType.Int32, Convert.ToInt32(nwAsset[21]));

            }
            db.AddInParameter(cmd, "@ToBuilding", DbType.String, nwAsset[22]);
            db.AddInParameter(cmd, "@ToFloor", DbType.String, nwAsset[23]);
            db.AddInParameter(cmd, "@ToRoom", DbType.String, nwAsset[24]);
            db.AddInParameter(cmd, "@ToLocation", DbType.String, nwAsset[25]);
            db.AddInParameter(cmd, "@ToIPAddress", DbType.String, nwAsset[26]);
            db.AddInParameter(cmd, "@ToSubnet", DbType.String, nwAsset[27]);
            db.AddInParameter(cmd, "@ToPort", DbType.String, nwAsset[28]);
            db.AddInParameter(cmd, "@ToNotes", DbType.String, nwAsset[29]);
            db.AddInParameter(cmd, "@ToOwner", DbType.String, nwAsset[30]);
            db.AddInParameter(cmd, "@ToVLAN", DbType.String, nwAsset[31]);

            #endregion
            
            #region Others
                     
            db.AddInParameter(cmd, "@AssetsTypeID", DbType.Int32, Convert.ToInt32(nwAsset[32]));
            db.AddInParameter(cmd, "@AssignName", DbType.Int32, Convert.ToInt32(nwAsset[33]));
            db.AddInParameter(cmd, "@AssignType", DbType.Int32, Convert.ToInt32(nwAsset[34]));
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, Convert.ToInt32(nwAsset[35]));
           db.AddInParameter(cmd, "@Approve", DbType.Boolean, false);
           db.AddOutParameter(cmd, "@ID", DbType.Int32, 4);
           if (nwAsset[40] == "")
           {
               db.AddInParameter(cmd, "@VendorID", DbType.Int32, DBNull.Value);
           }
           else
           {
               db.AddInParameter(cmd, "@VendorID", DbType.Int32, Convert.ToInt32(nwAsset[40]));

           }
           if (nwAsset[37] == "")
           {
               // @ExpDatedb.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
               db.AddInParameter(cmd, "@PurchasedDate", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
           }
           else
           {
               db.AddInParameter(cmd, "@PurchasedDate", DbType.DateTime, Convert.ToDateTime(nwAsset[37]));

           }
           if (nwAsset[38] == "")
           {
               // @ExpDatedb.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
               db.AddInParameter(cmd, "@ExpDate", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
           }
           else
           {
               db.AddInParameter(cmd, "@ExpDate", DbType.DateTime, Convert.ToDateTime(nwAsset[38]));

           }
           if (nwAsset[39] == "")
           {
               db.AddInParameter(cmd, "@AssestValue", DbType.Double, DBNull.Value);
           }
           else
           {
               db.AddInParameter(cmd, "@AssestValue", DbType.Double, Convert.ToDouble(nwAsset[39]));

           }
            #endregion
            db.ExecuteNonQuery(cmd);
           GetID = (int)db.GetParameterValue(cmd, "ID");
            cmd.Dispose();

            return GetID;


        }

        public int updategridviewdestination(string[] nwAsset)
        {
            int status = 0;

          
            //cmd.Dispose();
            cmd = db.GetStoredProcCommand("DN_UpdatemoveAssetsNew");

            #region UpdateSerialnumber


            db.AddInParameter(cmd, "@SerialNo", DbType.String, nwAsset[0]);
            db.AddInParameter(cmd, "@AssetNo", DbType.String, nwAsset[1]);
            #endregion 

            #region Update ProjectReference
            if (nwAsset[2] != "ALL")
            {
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(nwAsset[2]));

            }
            else
            {
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));

            }
            #endregion

            #region UpdateAssetsType

            if (nwAsset[3] == "")
            {
                //it represents the null value to @make
                db.AddInParameter(cmd, "@Make", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@Make", DbType.Int32, Convert.ToInt32(nwAsset[3]));

            }
            if (nwAsset[4] == "")
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, Convert.ToInt32(nwAsset[4]));


            }

            if (nwAsset[5] == "")
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, Convert.ToInt32(nwAsset[5]));

            }

            #endregion

         


            #region Update From Details


            if (nwAsset[6] == "")
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, Convert.ToInt32(nwAsset[6]));

            }

            db.AddInParameter(cmd, "@FromBuilding", DbType.String, nwAsset[7]);
            db.AddInParameter(cmd, "@FromFloor", DbType.String, nwAsset[8]);
            db.AddInParameter(cmd, "@FromRoom", DbType.String, nwAsset[9]);
            db.AddInParameter(cmd, "@FromLocation", DbType.String, nwAsset[10]);

            #region Datevalues
            if (nwAsset[11] == "")
            {
                //db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, Convert.ToDateTime("1/1/1900"));

            }
            else
            {
                db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, Convert.ToDateTime(nwAsset[11]));

            }
            if (nwAsset[12] == "")
            {
                //db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
            }
            else
            {
                db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, Convert.ToDateTime(nwAsset[12]));

            }
            #endregion


            db.AddInParameter(cmd, "@FromPort", DbType.String, nwAsset[13]);
            db.AddInParameter(cmd, "@FromOwner", DbType.String, nwAsset[14]);
            db.AddInParameter(cmd, "@FromVLAN", DbType.String, nwAsset[15]);
            db.AddInParameter(cmd, "@FromIPAddress", DbType.String, nwAsset[16]);
            db.AddInParameter(cmd, "@FromSubnet", DbType.String, nwAsset[17]);

            #endregion





            #region Update Destination Details




            if (nwAsset[18] == "")
            {
                db.AddInParameter(cmd, "@ToSite", DbType.Int32, DBNull.Value);


            }
            else
            {
                db.AddInParameter(cmd, "@ToSite", DbType.Int32, Convert.ToInt32(nwAsset[18]));

            }
            db.AddInParameter(cmd, "@ToBuilding", DbType.String, nwAsset[19]);
            db.AddInParameter(cmd, "@ToFloor", DbType.String, nwAsset[20]);
            db.AddInParameter(cmd, "@ToRoom", DbType.String, nwAsset[21]);
            db.AddInParameter(cmd, "@ToLocation", DbType.String, nwAsset[22]);
            db.AddInParameter(cmd, "@ToIPAddress", DbType.String, nwAsset[23]);
            db.AddInParameter(cmd, "@ToSubnet", DbType.String, nwAsset[24]);
            db.AddInParameter(cmd, "@ToPort", DbType.String, nwAsset[25]);
            db.AddInParameter(cmd, "@ToNotes", DbType.String, nwAsset[26]);
            db.AddInParameter(cmd, "@ToOwner", DbType.String, nwAsset[27]);
            db.AddInParameter(cmd, "@ToVLAN", DbType.String, nwAsset[28]);
           #endregion

            #region update AssetsType

            db.AddInParameter(cmd, "@AssetsTypeID", DbType.Int32, Convert.ToInt32(nwAsset[29]));
                  
            
            #endregion

            db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(nwAsset[30]));
            status = Convert.ToInt32(db.ExecuteNonQuery(cmd));
            cmd.Dispose();


            return status;

        }

        public int AssigntoProjectToMove(int ID, int Project, int SessionKey, int AssignNameLikeProjectref, int AssignType)
        {
            int GetResult = 0;
            try
            {
                //add parameters of assetid and Projectreference
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_InsertAssetMoveAssign");
                db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Project);
                db.AddInParameter(cmd, "@UserID1", DbType.Int32, SessionKey);
                db.AddInParameter(cmd, "@AssignType", DbType.Int32, AssignType);
                db.AddInParameter(cmd, "@AssignName", DbType.Int32, AssignNameLikeProjectref);
                GetResult = Convert.ToInt32(db.ExecuteNonQuery(cmd));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetResult;

        }


        #endregion


        #region GetAssetsPortFolio
        public int GetPortfilioID(int projectReference)
        {
            int getID = 0;

            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select Portfolio from Projects where ProjectReference = {0}", projectReference));
            while (dr.Read())
            {
                getID =Convert.ToInt32(dr["Portfolio"]);
              
            }
            dr.Close();

            return getID;
        }
        #endregion


        #region Permission
        public static bool IsPermitted(int ProjectReference, int contractorID, PermissionsTo permissionTo)
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd;
            DataSet ds;
            try
            {
                cmd = db.GetStoredProcCommand("DEFFINITY_GETPROJ_PERMISSIONS");
                db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, ProjectReference);
                db.AddInParameter(cmd, "@CONTRACTORID", DbType.Int32, contractorID);
                ds = db.ExecuteDataSet(cmd);
                cmd.Dispose();
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
                    return true;
                else if (Convert.ToInt32(ds.Tables[0].Rows[0][permissionTo.GetHashCode()]) == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "Get Permissions for contractor to a Project");
                return false;
            }
        }
        public enum PermissionsTo
        {
            AdminRights = 0,
            ModifyProject = 1,
            AllocateTask = 2,
            ManagageIssues = 3,
            ManageRisk = 4,
            AddAssets = 5,
            AddDocuments = 6,
            ManageProjectFinancials = 7,
            ApproveVariations = 8,
            DeleteDocument = 9
        };
        #endregion




        #region UpdateAssets with Admin
        public void UpdateAssetByAdminBtn_Click(string[] UpdAsset)
        {
           

            cmd = db.GetStoredProcCommand("DN_UpdateAdmin_AssetsNew");
            db.AddInParameter(cmd, "@AssetNo", DbType.String, UpdAsset[0]);
            if (UpdAsset[1] != "ALL")
            {
                //db.AddInParameter(cmd, "@Pref", DbType.Int32, UpdAsset[21]);
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(UpdAsset[1]));

            }
            else
            {
                //db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, 0);
            }

            if (UpdAsset[2] == "")
            {
                db.AddInParameter(cmd, "@Make", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Make", DbType.Int32, Convert.ToInt32(UpdAsset[2]));

            }

            if (UpdAsset[3] == "")
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Model", DbType.Int32, Convert.ToInt32(UpdAsset[3]));

            }
            if (UpdAsset[4] == "")
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@Type", DbType.Int32, Convert.ToInt32(UpdAsset[4]));
            }
            if (UpdAsset[5] == "")
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, DBNull.Value);

            }
            else
            {
                db.AddInParameter(cmd, "@FromSite", DbType.Int32, Convert.ToInt32(UpdAsset[5]));

            }

            db.AddInParameter(cmd, "@FromBuilding", DbType.String, UpdAsset[6]);
            db.AddInParameter(cmd, "@FromFloor", DbType.String, UpdAsset[7]);
            db.AddInParameter(cmd, "@FromRoom", DbType.String, UpdAsset[8]);
            db.AddInParameter(cmd, "@FromLocation", DbType.String, UpdAsset[9]);
            if (UpdAsset[10] == "")
            {

                db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, Convert.ToDateTime("1/1/1900"));

            }
            else
            {
                db.AddInParameter(cmd, "Datemoved", DbType.DateTime, Convert.ToDateTime(UpdAsset[10]));

            }
            if (UpdAsset[11] == "")
            {
                db.AddInParameter(cmd, "Datecommision", DbType.DateTime, Convert.ToDateTime("1/1/1900"));

            }
            else
            {
                db.AddInParameter(cmd, "Datecommision", DbType.DateTime, Convert.ToDateTime(UpdAsset[11]));

            }
            if (UpdAsset[25] == "")
            {
                db.AddInParameter(cmd, "@AssestValue", DbType.Int32, DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@AssestValue", DbType.Int32, Convert.ToInt32(UpdAsset[25]));

            }
            if (UpdAsset[28] == "")
            {
                db.AddInParameter(cmd, "@VendorID", DbType.Int32, DBNull.Value);
            }
            else
            {
                db.AddInParameter(cmd, "@VendorID", DbType.Int32, Convert.ToInt32(UpdAsset[28]));

            }
            if (UpdAsset[26] == "")
            {
                // @ExpDatedb.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmd, "@PurchasedDate", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
            }
            else
            {
                db.AddInParameter(cmd, "@PurchasedDate", DbType.DateTime, Convert.ToDateTime(UpdAsset[26]));

            }
            if (UpdAsset[27] == "")
            {
                // @ExpDatedb.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmd, "@ExpDate", DbType.DateTime, Convert.ToDateTime("1/1/1900"));
            }
            else
            {
                db.AddInParameter(cmd, "@ExpDate", DbType.DateTime, Convert.ToDateTime(UpdAsset[27]));

            }
            db.AddInParameter(cmd, "@FromPort", DbType.String, UpdAsset[12]);
            db.AddInParameter(cmd, "@FromOwner", DbType.String, UpdAsset[13]);
            db.AddInParameter(cmd, "@userid", DbType.Int32, Convert.ToInt32(UpdAsset[14]));
            db.AddInParameter(cmd, "@FromVLAN", DbType.String, UpdAsset[15]);
            db.AddInParameter(cmd, "@FromIPAddress", DbType.String, UpdAsset[16]);
            db.AddInParameter(cmd, "@FromSubnet", DbType.String, UpdAsset[17]);
            db.AddInParameter(cmd, "@NewAsset", DbType.Int32, Convert.ToInt32(UpdAsset[18]));
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, Convert.ToInt32(UpdAsset[19]));
            db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(UpdAsset[20]));
            db.AddInParameter(cmd, "@SerialNo", DbType.String, UpdAsset[21]);
            db.AddInParameter(cmd, "@Technical", DbType.String, UpdAsset[22]);
            db.AddInParameter(cmd, "@Notes", DbType.String, UpdAsset[23]);
            db.AddInParameter(cmd, "@AssetsTypeID", DbType.String, UpdAsset[24]);


            db.ExecuteNonQuery(cmd);
            cmd.Dispose();

        }
        #endregion



        //Image Upload in the DataBase.......
        #region ImageUpload and Display Not Using Any thing

        public int GetimageID(string Assets_ImageName, byte[] imageBin, string ImageContent, int ImageLength,int ID)
        {
            int GetImageIDVale = 0;
            try
            {
                cmd = db.GetStoredProcCommand("DN_AssetsImageInsert");
                db.AddInParameter(cmd, "@ImageName", DbType.String, Assets_ImageName);
                db.AddInParameter(cmd, "@ImageData", DbType.Binary , imageBin);
                db.AddInParameter(cmd, "@Content_Type", DbType.String, ImageContent);
                db.AddInParameter(cmd, "@image_Length", DbType.String, ImageLength);
                db.AddInParameter(cmd, "@GetImageID", DbType.Int32, ID);
                db.AddOutParameter(cmd, "@ID", DbType.Int32, 4);
                
         db.ExecuteNonQuery(cmd);
         GetImageIDVale = (int)db.GetParameterValue(cmd, "ID");
            cmd.Dispose();
          //  GetImageIDVale = Convert.ToInt32(db.AddOutParameter(cmd, "@ID", DbType.Int32, 4));
             }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetImageIDVale;
            
        }

        public DataTable FetchAllImages(int AssetImageID)
        {

            string sqlQry = "select * from AssetsImage where AssetID=" + AssetImageID;
            DataTable dt;
            DataSet ds = new DataSet();
            cmd = db.GetSqlStringCommand(sqlQry);
             ds = db.ExecuteDataSet(cmd);
             dt = ds.Tables[0];
            cmd.Dispose();
            return dt;
      
        }
        public DataTable FetchAllDocumentsofAssets(int AssetImageID)
        {

            string sqlQry = "select * from Assets_Documents where AssetID=" + AssetImageID;
            DataTable dt;
            DataSet ds = new DataSet();
            cmd = db.GetSqlStringCommand(sqlQry);
            ds = db.ExecuteDataSet(cmd);
            dt = ds.Tables[0];
            cmd.Dispose();
            return dt;

        }


        public int WriteToDB(string strName, string strType, ref byte[] Buffer,ref byte[] OriginalSizeBuffer,int mylenth,int AssetID)
        {
            int GetDocument = 0;
            try
            {
                cmd = db.GetStoredProcCommand("DN_AssetsDocumentaion");
                db.AddInParameter(cmd, "@DocumentName", DbType.String, strName);
                db.AddInParameter(cmd, "@Document", DbType.Binary, Buffer);
                db.AddInParameter(cmd, "@OriginalSize", DbType.Binary, OriginalSizeBuffer);
                db.AddInParameter(cmd, "@ContentType", DbType.String, strType);
                db.AddInParameter(cmd, "@DataSize", DbType.String, mylenth);
                db.AddInParameter(cmd, "@AssetID", DbType.Int32, AssetID);
                db.AddOutParameter(cmd, "@ID", DbType.Int32, 4);
               db.ExecuteNonQuery(cmd);
               GetDocument = (int)db.GetParameterValue(cmd, "ID");
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return GetDocument;
        }



        public int GetAssetsImageID(int AssetID)
        {
            int ImageID = 0;

            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select AssetsImageID from Assets  where ID = {0}", AssetID));
            while (dr.Read())
            {
                ImageID = Convert.ToInt32(dr["AssetsImageID"]);

            }
            dr.Close();

            return ImageID;
        }


        public int RemoveImagefromTable(int AssetID)
        {
            int DocumentID = 0;


            string sqlQry = "delete from  Assets_Image  where ID=" + AssetID;

            cmd = db.GetSqlStringCommand(sqlQry);
            DocumentID = Convert.ToInt32(db.ExecuteNonQuery(cmd));

            return DocumentID;
        }


        #endregion


        #region ImageUpload and Display

        public void ImageInsertion(int AssetID, Guid   ImageName)
        {

           
            try
            {
                //add parameters of assetid and Projectreference
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_insertUpdateAssetsImage");
                db.AddInParameter(cmd, "@AssetID", DbType.Int32, AssetID);
                db.AddInParameter(cmd, "@Image", DbType.Guid , ImageName);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           

        }

        #endregion








        #region AssetsDocuments



        public int GetAssetsDocumnetID(int AssetID)
        {
            int DocumentID = 0;

            //SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("select AssetsDocumentID from Assets where ID={0}", AssetID));
            //while (dr.Read())
            //{
            //    DocumentID = Convert.ToInt32(dr["AssetsDocumentID"]);

            //}
            //dr.Close();

            return DocumentID;
        }


        public int RemoveAssetsDocuments(int AssetID)
        {
            int DocumentID = 0;


            string sqlQry = "delete from  Assets_Documents  where ID=" + AssetID;
           
             cmd = db.GetSqlStringCommand(sqlQry);
          DocumentID=Convert.ToInt32(db.ExecuteNonQuery(cmd));

             return DocumentID;
        }

        public void DownLoadDoc(string str)
        {
            SqlDataReader dr;
            string abc = str;
            
            SqlCommand cmd = new SqlCommand("DN_AssetsDocSelectDownLoad", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", int.Parse(abc)));
            cmd.Connection.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string path = HttpContext.Current.Server.MapPath(@"Documentation\\");

                HttpContext.Current.Response.ContentType = dr["ContentType"].ToString();//"application/ms-word";
                byte[] getContent = (byte[])dr["Document"];
                HttpContext.Current.Response.BinaryWrite(getContent);
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; FileName=" + dr["DocumentName"].ToString());
                HttpContext.Current.Response.End();
            }
            cmd.Dispose();
            cmd.Connection.Close();

        }


        //Delete the Assets Document

       


        #endregion







        #region AssetsTypeAdding

        public void AddAsstesType(string TypeName)
        {

        //    SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string updateSQL;
            SqlCommand sqlcomm = new SqlCommand("display_i_type", con);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            SqlParameter typename = new SqlParameter("@typename", SqlDbType.VarChar, 50);
            typename.Value = TypeName;
            sqlcomm.Parameters.Add(typename);
            SqlParameter typeid = new SqlParameter("@func", SqlDbType.Int, 32);
            typeid.Value = 1;
            sqlcomm.Parameters.Add(typeid);
            sqlcomm.ExecuteNonQuery();
            con.Close();

        }



        public int  AddModelType(string ModelName1)
        {

            int GetModelID = 0;

            try
            {

               
                con.Open();
                //string updateSQL;
                SqlCommand sqlcomm = new SqlCommand("display_i_model", con);
                sqlcomm.CommandType = CommandType.StoredProcedure;
                SqlParameter modelname = new SqlParameter("@modelname", SqlDbType.VarChar, 50);
                modelname.Value =ModelName1;
                sqlcomm.Parameters.Add(modelname);
                SqlParameter modelid = new SqlParameter("@func", SqlDbType.Int, 32);
                modelid.Value = 1;
                sqlcomm.Parameters.Add(modelid);
                 GetModelID =Convert.ToInt32(sqlcomm.ExecuteScalar());
              
                con.Close();
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);

            }

            return GetModelID;
        }






        public int AddMakeType(string MakeName1)
        {
            int resultsmake = 0;
            try
            {
                //SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                ////string updateSQL;
                SqlCommand sqlcomm = new SqlCommand("display_i_make", con);
                sqlcomm.CommandType = CommandType.StoredProcedure;
                SqlParameter makename = new SqlParameter("@makename", SqlDbType.VarChar, 50);
                makename.Value = MakeName1;
                sqlcomm.Parameters.Add(makename);
                SqlParameter makeno = new SqlParameter("@func", SqlDbType.Int, 32);
                makeno.Value = 1;
                sqlcomm.Parameters.Add(makeno);
                resultsmake = Convert.ToInt32(sqlcomm.ExecuteScalar());
                
                con.Close();
                //lblerror.Text = "";
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
            return resultsmake;
        }

        #endregion



        public int updatelistAssets(int AssetsID, int ToSiteID,string Tobuilding, string ToRoom, string ToFloor, string ToIpAddress, string ToSunnet, string ToPort, string Valn, string Tonotes, string ToOwner, string Tolocation)
        {
            int Return = 0;


            try
            {
                //add parameters of assetid and Projectreference
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_UpdateAssetsMove_ListView");
                db.AddInParameter(cmd, "@ID", DbType.Int32, AssetsID);
                db.AddInParameter(cmd, "@ToSite", DbType.String, ToSiteID);
                db.AddInParameter(cmd, "@ToBuilding", DbType.String, Tobuilding);
                db.AddInParameter(cmd, "@ToRoom", DbType.String, ToRoom);
                db.AddInParameter(cmd, "@ToFloor", DbType.String, ToFloor);
                db.AddInParameter(cmd, "@ToLocation", DbType.String, Tolocation);


                db.AddInParameter(cmd, "@ToIPAddress", DbType.String, ToIpAddress);
                db.AddInParameter(cmd, "@ToSubnet ", DbType.String, ToSunnet);
                db.AddInParameter(cmd, "@ToPort", DbType.String, ToPort);
                db.AddInParameter(cmd, "@ToVLAN", DbType.String, Valn);

                db.AddInParameter(cmd, "@ToOwner", DbType.String, ToOwner);
                db.AddInParameter(cmd, "@ToNotes", DbType.String, Tonotes);

               Return = Convert.ToInt32(db.ExecuteNonQuery(cmd));
            }
            catch (Exception ex)
            {
                throw;
            }
       


            return Return;
        }

    }
    
}
