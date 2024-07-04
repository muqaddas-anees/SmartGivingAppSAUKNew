using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using ProjectMgt.BAL;
using ProjectMgt.BLL;
using System.Linq;
using System.Data.Linq;

/// <summary>
/// Summary description for Worksheet
/// </summary>
namespace Deffinity
{
    public class Worksheet
    {
        public static DataTable Worksheet_SelectAll(int ProjectReference)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT  ID,TypeName FROM BOM_Types WHERE ProjectReference = @ProjectReference and IsDeleted=0  ORDER BY TypeName", new SqlParameter("@ProjectReference", ProjectReference)).Tables[0];
            return dt;
        }
        
        public static int Worksheet_InsertUpdate(int id,int ProjectReference,string worksheet_name)
        {
            SqlParameter outval = new SqlParameter("@output",SqlDbType.Int,4);
            outval.Direction= ParameterDirection.Output;
            SqlParameter outID = new SqlParameter("@outID", SqlDbType.Int, 4);
            outID.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_InsertUpdateBOMType",
                new SqlParameter("@ID",id),new SqlParameter("@ProjectReference",ProjectReference),
                new SqlParameter("@TypeName", worksheet_name), new SqlParameter("@createdBy", sessionKeys.UID),
                outval, outID);


            return int.Parse(outID.Value.ToString());
        }
        public static bool Worksheet_Delete(int id)
        {
            bool deleterecord = true;
            bool Status = true;
            projectTaskDataContext BOM = new projectTaskDataContext();
            var BOMValue = (from a in BOM.ProjectBOMs where a.WorkSheetID == id select a).ToList();
            if (BOMValue != null)
            {
               
                foreach (var x in BOMValue)
                {
                    var getValues = (from r in BOM.GoodsReceipts
                                     where r.BOMID == x.ID
                                     select r).FirstOrDefault();
                    if (getValues != null)
                    {
                        if (getValues.QtyReceived != 0)
                        {
                            deleterecord = false;
                            break;
                        }
                    }
                }
                if (deleterecord == true)
                {
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DeleteBOMType",
                       new SqlParameter("@ID", id));
                    Status = true;
                }
                else
                {
                    Status = false;
                }
            }
            else
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DeleteBOMType",
                                        new SqlParameter("@ID", id));
                Status = true;
            }
            return Status;
        }
    }
}
