using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
/// <summary>
/// Summary description for AssetsAdmin
/// </summary>
public class AssetsAdminClass
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DataTable dt;
    DataSet ds;

	public AssetsAdminClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //protected void SqlDBconn(string cmd)
    //{
    //    SqlConnection con  = new SqlConnection(connectionString);
    //    SqlCommand myCommand = new SqlCommand (cmd,con);
    //    myCommand.CommandType= CommandType.StoredProcedure;

    //}

    public void NewAssetSubmitBtn_Click(string[] nwAsset)
    {


        cmd = db.GetStoredProcCommand("DN_InsertNewAdminAssetsDB");

        db.AddInParameter(cmd, "@SerialNo", DbType.String, nwAsset[0]);
        db.AddInParameter(cmd, "@AssetNo", DbType.String, nwAsset[1]);
        if (nwAsset[2] != "ALL")
        {
            //db.AddInParameter(cmd, "@Pref", DbType.Int32, UpdAsset[21]);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(nwAsset[2]));

        }
        else
        {
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));

        }
               
        if (nwAsset[3]== "")
        {
            //it represents the null value to @make
            db.AddInParameter(cmd, "@Make", DbType.Int32, DBNull.Value);
            
            
        }
        else
        {
            db.AddInParameter(cmd, "@Make", DbType.Int32, Convert.ToInt32(nwAsset[3]));

        }
        if(nwAsset[4] == "")
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
                    
        if(nwAsset[6]=="")
        {
            db.AddInParameter(cmd, "@FromSite", DbType.Int32, DBNull.Value);
           

        }
        else
        {
            db.AddInParameter(cmd, "@FromSite", DbType.Int32,Convert.ToInt32( nwAsset[6]));
          
        }

        db.AddInParameter(cmd, "@FromBuilding", DbType.String, nwAsset[7]);
        db.AddInParameter(cmd, "@FromFloor", DbType.String, nwAsset[8]);
        db.AddInParameter(cmd, "@FromRoom", DbType.String, nwAsset[9]);
        db.AddInParameter(cmd, "@FromLocation", DbType.String, nwAsset[10]);
        if (nwAsset[11] == "")
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime,Convert.ToDateTime( nwAsset[11]));

        }
        if (nwAsset[12] == "")
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime,Convert.ToDateTime( nwAsset[12]));

        }
        db.AddInParameter(cmd, "@FromPort", DbType.String, nwAsset[13]);
        db.AddInParameter(cmd, "@FromOwner", DbType.String, nwAsset[14]);
        db.AddInParameter(cmd, "@FromNotes", DbType.String, nwAsset[15]);
        db.AddInParameter(cmd, "@Technical", DbType.String, nwAsset[16]);
        if (nwAsset[17] == null)
        {
            db.AddInParameter(cmd, "@userid", DbType.String, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@userid", DbType.String, nwAsset[17]);
        }
        db.AddInParameter(cmd, "@FromVLAN", DbType.String, nwAsset[18]);           
        db.AddInParameter(cmd, "@FromIPAddress", DbType.String, nwAsset[19]);
        db.AddInParameter(cmd, "@FromSubnet", DbType.String, nwAsset[20]);
        if (nwAsset[21] == "")
        {
            db.AddInParameter(cmd, "@NewAsset", DbType.Int32, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@NewAsset", DbType.Int32, Convert.ToInt32(nwAsset[21]));

        }
       
        if (nwAsset[22] == "")
        {
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32,Convert.ToInt32( nwAsset[22]));


        }

        if (nwAsset[23] == "")
        {
            db.AddInParameter(cmd, "@ToSite", DbType.Int32, DBNull.Value);


        }
        else
        {
            db.AddInParameter(cmd, "@ToSite", DbType.Int32,Convert.ToInt32( nwAsset[23]));

        }
        db.AddInParameter(cmd, "@ToBuilding", DbType.String, nwAsset[24]);
        db.AddInParameter(cmd, "@ToFloor", DbType.String, nwAsset[25]);
        db.AddInParameter(cmd, "@ToRoom", DbType.String, nwAsset[26]);
        db.AddInParameter(cmd, "@ToLocation", DbType.String, nwAsset[27]);
        db.AddInParameter(cmd, "@ToIPAddress", DbType.String, nwAsset[28]);
        db.AddInParameter(cmd, "@ToSubnet", DbType.String, nwAsset[29]);
        db.AddInParameter(cmd, "@ToPort", DbType.String, nwAsset[30]);
        db.AddInParameter(cmd, "@ToNotes", DbType.String, nwAsset[31]);
        db.AddInParameter(cmd, "@ToOwner", DbType.String, nwAsset[32]);
        db.AddInParameter(cmd, "@ToVLAN",DbType.String,nwAsset[33]);
        db.AddInParameter(cmd, "@Approve", DbType.Boolean, false);



        db.ExecuteNonQuery(cmd);
        cmd.Dispose();
                    
                   

    
    }


    public void UpdateAssetBtn_Click(string[] UpdAsset)
    {


        cmd = db.GetStoredProcCommand("DN_UpdateAdminAssetsNew");
        db.AddInParameter(cmd, "@AssetNo", DbType.String, UpdAsset[0]);
        if (UpdAsset[1] != "ALL")
        {
            //db.AddInParameter(cmd, "@Pref", DbType.Int32, UpdAsset[21]);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(UpdAsset[1]));

        }
        else
        {
            //db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32,0);
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

            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "Datemoved", DbType.DateTime, Convert.ToDateTime(UpdAsset[10]));

        }
        if (UpdAsset[11] == "")
        {
            db.AddInParameter(cmd, "Datecommision", DbType.DateTime, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "Datecommision", DbType.DateTime, Convert.ToDateTime(UpdAsset[11]));

        }
        db.AddInParameter(cmd, "@FromPort", DbType.String, UpdAsset[12]);
        db.AddInParameter(cmd, "@FromOwner", DbType.String, UpdAsset[13]);
        db.AddInParameter(cmd, "@userid", DbType.Int32, Convert.ToInt32(UpdAsset[14]));
        db.AddInParameter(cmd, "@FromVLAN", DbType.String, UpdAsset[15]);
        db.AddInParameter(cmd, "@FromIPAddress", DbType.String, UpdAsset[16]);
        db.AddInParameter(cmd, "@FromSubnet", DbType.String, UpdAsset[17]);
        db.AddInParameter(cmd, "@NewAsset", DbType.Int32,Convert.ToInt32( UpdAsset[18]));
        db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, Convert.ToInt32(UpdAsset[19]));
        db.AddInParameter(cmd, "@ID",DbType.Int32,Convert.ToInt32(UpdAsset[20]));
        db.AddInParameter(cmd, "@SerialNo", DbType.String, UpdAsset[21]);
        db.AddInParameter(cmd, "@Technical", DbType.String, UpdAsset[22]);
        db.AddInParameter(cmd, "@Notes", DbType.String, UpdAsset[23]);
               

        db.ExecuteNonQuery(cmd);
        cmd.Dispose();

    }

    // Project ASSETS.. for UPDATING ASSET Schedule

    public void UpdateProjectASSET(string[] ProjectASSETSch)
    {
        //cmd = db.GetStoredProcCommand("DN_UpdateAssetsAssetSchedule1");
        cmd = db.GetStoredProcCommand("DN_UpdateProjectAssetsNew");


        db.AddInParameter(cmd, "@id", DbType.Int32, Convert.ToInt32(ProjectASSETSch[30]));
        db.AddInParameter(cmd, "@AssetNo", DbType.String, ProjectASSETSch[0]);
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(ProjectASSETSch[1]));
        if (ProjectASSETSch[2] == "")
        {
            db.AddInParameter(cmd, "@make", DbType.Int32, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@make", DbType.Int32, Convert.ToInt32(ProjectASSETSch[2]));
        }

        if (ProjectASSETSch[3] == "")
        {
            db.AddInParameter(cmd, "@model", DbType.Int32, DBNull.Value);

        }

        else
        {
            db.AddInParameter(cmd, "@model", DbType.Int32, Convert.ToInt32(ProjectASSETSch[3]));

        }
        if (ProjectASSETSch[4] == "")
        {
            db.AddInParameter(cmd, "@Type", DbType.Int32, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@Type", DbType.Int32, Convert.ToInt32(ProjectASSETSch[4]));

        }
        db.AddInParameter(cmd, "@FromBuilding", DbType.String, ProjectASSETSch[5]);
        db.AddInParameter(cmd, "@FromFloor", DbType.String, ProjectASSETSch[6]);
        db.AddInParameter(cmd, "@FromRoom", DbType.String, ProjectASSETSch[7]);
        db.AddInParameter(cmd, "@FromLocation", DbType.String, ProjectASSETSch[8]);
        if (ProjectASSETSch[9] == "")
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, ProjectASSETSch[9]);

        }
        if (ProjectASSETSch[10] == "")
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, ProjectASSETSch[10]);


        }
        db.AddInParameter(cmd, "@FromPort", DbType.String, ProjectASSETSch[11]);
        db.AddInParameter(cmd, "@FromOwner", DbType.String, ProjectASSETSch[12]);
        db.AddInParameter(cmd, "@userid", DbType.Int32, Convert.ToInt32(ProjectASSETSch[13]));
        db.AddInParameter(cmd, "@FromVLAN", DbType.String, ProjectASSETSch[14]);
        db.AddInParameter(cmd, "@FromIPAddress", DbType.String, ProjectASSETSch[15]);
        db.AddInParameter(cmd, "@FromSubnet", DbType.String, ProjectASSETSch[16]);
        db.AddInParameter(cmd, "@NewAsset", DbType.Int32, Convert.ToInt32(ProjectASSETSch[17]));
        db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, Convert.ToInt32(ProjectASSETSch[18]));

        if (ProjectASSETSch[19] == "")
        {
            db.AddInParameter(cmd, "@ToSite", DbType.Int32, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@ToSite", DbType.Int32, Convert.ToInt32(ProjectASSETSch[19]));

        }
        db.AddInParameter(cmd, "@ToBuilding", DbType.String, ProjectASSETSch[20]);
        db.AddInParameter(cmd, "@ToFloor", DbType.String, ProjectASSETSch[21]);
        db.AddInParameter(cmd, "@ToRoom", DbType.String, ProjectASSETSch[22]);
        db.AddInParameter(cmd, "@ToLocation", DbType.String, ProjectASSETSch[23]);
        db.AddInParameter(cmd, "@ToIPAddress", DbType.String, ProjectASSETSch[24]);
        db.AddInParameter(cmd, "@ToSubnet", DbType.String, ProjectASSETSch[25]);
        db.AddInParameter(cmd, "@ToPort", DbType.String, ProjectASSETSch[26]);
        db.AddInParameter(cmd, "@ToNotes", DbType.String, ProjectASSETSch[27]);
        db.AddInParameter(cmd, "@ToOwner", DbType.String, ProjectASSETSch[28]);
        db.AddInParameter(cmd, "@ToVLAN", DbType.String, ProjectASSETSch[29]);

        db.ExecuteNonQuery(cmd);
        cmd.Dispose();


        
        
    }

    public DataTable dt_ProjectAssets(int ProjectReference)
    {
        DataSet ds= new DataSet();
        cmd = db.GetStoredProcCommand("DN_ProjectAssetsDisplayNew");
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
        ds=db.ExecuteDataSet(cmd);
        dt=ds.Tables[0];
        cmd.Dispose();
        return dt;
    }
    public DataTable dt_AdminAsset(int ID)
    {
        DataSet ds = new DataSet();
        cmd = db.GetStoredProcCommand("DN_EDITASSETSNew");
        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        ds = db.ExecuteDataSet(cmd);
        dt = ds.Tables[0];
        cmd.Dispose();
        return dt;
    }
    public void Approve(int id)
    {
        cmd = db.GetStoredProcCommand("DN_CheckPointAssetApprove");
        db.AddInParameter(cmd, "@ID", DbType.Int32, id);
        db.ExecuteDataSet(cmd);
        cmd.Dispose();
    }
    public void CSVAssetInsert(string[] nwAsset)
    {
        //DN_ImportAssets.
        cmd = db.GetStoredProcCommand("DN_ImportAssets");

        db.AddInParameter(cmd, "@SerialNo", DbType.String, nwAsset[0]);
        db.AddInParameter(cmd, "@AssetNo", DbType.String, nwAsset[1]);
        if (nwAsset[2] != "ALL")
        {
            //db.AddInParameter(cmd, "@Pref", DbType.Int32, UpdAsset[21]);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(nwAsset[2]));

        }
        else
        {
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32("0"));

        }

        if (nwAsset[3] == "")
        {
            //it represents the null value to @make
            db.AddInParameter(cmd, "@Make", DbType.String, DBNull.Value);


        }
        else
        {
            db.AddInParameter(cmd, "@Make", DbType.String,  (nwAsset[3]));

        }
        if (nwAsset[4] == "")
        {
            db.AddInParameter(cmd, "@Model", DbType.String, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@Model", DbType.String,  (nwAsset[4]));


        }

        if (nwAsset[5] == "")
        {
            db.AddInParameter(cmd, "@Type", DbType.String, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@Type", DbType.String,  (nwAsset[5]));

        }

        if (nwAsset[6] == "")
        {
            db.AddInParameter(cmd, "@FromSite", DbType.String, DBNull.Value);


        }
        else
        {
            db.AddInParameter(cmd, "@FromSite", DbType.String,  (nwAsset[6]));

        }

        db.AddInParameter(cmd, "@FromBuilding", DbType.String, nwAsset[7]);
        db.AddInParameter(cmd, "@FromFloor", DbType.String, nwAsset[8]);
        db.AddInParameter(cmd, "@FromRoom", DbType.String, nwAsset[9]);
        db.AddInParameter(cmd, "@FromLocation", DbType.String, nwAsset[10]);
        if ((nwAsset[11] == "")||(nwAsset[11] == " "))
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, DBNull.Value);

        }
        else
        {
            db.AddInParameter(cmd, "@Datemoved", DbType.DateTime, Convert.ToDateTime(nwAsset[11]));

        }
        if ((nwAsset[12] == "")||(nwAsset[12] == " "))
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@Datecommision", DbType.DateTime, Convert.ToDateTime(nwAsset[12]));

        }
        db.AddInParameter(cmd, "@FromPort", DbType.String, nwAsset[13]);
        db.AddInParameter(cmd, "@FromOwner", DbType.String, nwAsset[14]);
        db.AddInParameter(cmd, "@FromNotes", DbType.String, nwAsset[15]);
        db.AddInParameter(cmd, "@Technical", DbType.String, nwAsset[16]);
        if (nwAsset[17] == null)
        {
            db.AddInParameter(cmd, "@userid", DbType.String, DBNull.Value);
        }
        else
        {
            db.AddInParameter(cmd, "@userid", DbType.String, nwAsset[17]);
        }
        db.AddInParameter(cmd, "@FromVLAN", DbType.String, nwAsset[18]);
        db.AddInParameter(cmd, "@FromIPAddress", DbType.String, nwAsset[19]);
        db.AddInParameter(cmd, "@FromSubnet", DbType.String, nwAsset[20]);
       



        db.ExecuteNonQuery(cmd);
        cmd.Dispose();
                    
    }

    }

