using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Deffinity.ForumEntitys;


/// <summary>
/// Summary description for ForumManager
/// </summary>
namespace Deffinity.ForumManager
{
    public class ForumManager
    {
        
        //Forum Master
        #region forum Master
        public static bool ForumMasterInsert(ForumMasterEntity forumMasterEntity,out int forumID)
        {
            
            int restult;
            restult = ForumMasterInsert_Update(forumMasterEntity, true, "Deffinity_ForumMasterInsert", out forumID);
            return ((restult > 0) ? true : false);
        }
        public static bool ForumMasterUpdate(ForumMasterEntity forumMasterEntity)
        {
            int restult;
            int forumID;
            restult = ForumMasterInsert_Update(forumMasterEntity, false, "Deffinity_ForumMasterUpdate", out forumID);
            return ((restult > 0) ? true : false);
        }
        
        private static int ForumMasterInsert_Update(ForumMasterEntity forumMasterEntity, bool SqlType, string spName,out int ForumID)
        {//"@ID", forumMasterEntity.ID),
            SqlParameter[] sqlParams;
            SqlParameter OutID = new SqlParameter("@outID",SqlDbType.Int);
            OutID.Direction = ParameterDirection.Output;
            if (SqlType)
            {
                sqlParams = new SqlParameter[]{    new SqlParameter("@ProjectReference", forumMasterEntity.ProjectReference),
                                                   new SqlParameter("@Title", forumMasterEntity.Title),
                                                   new SqlParameter("@Message", forumMasterEntity.Message),
                                                   new SqlParameter("@AuthorID", forumMasterEntity.AuthorID),
                                                   new SqlParameter("@MsgType", forumMasterEntity.MsgType),
                                                   new SqlParameter("@Ftype", forumMasterEntity.Ftype),OutID};
            }
            else
            {
                sqlParams = new SqlParameter[]{    new SqlParameter("@ID", forumMasterEntity.ID),                                                   
                                                   new SqlParameter("@Title", forumMasterEntity.Title),
                                                   new SqlParameter("@Message", forumMasterEntity.Message),
                                                   new SqlParameter("@AuthorID", forumMasterEntity.AuthorID),
                                                    };
            }
             

        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);
        ForumID = int.Parse(OutID.Value.ToString());
        return 1;

        }
        public static ForumMasterEntity forumMasterSelect(int ID)
        {
            ForumMasterEntity forumMasterEntity = new ForumMasterEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_ForumMasterSelect", new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                        forumMasterEntity.ID = int.Parse(dr["ID"].ToString());
                        forumMasterEntity.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                        forumMasterEntity.Title = dr["Title"].ToString();
                        forumMasterEntity.Message = dr["Message"].ToString();
                        forumMasterEntity.AuthorID = int.Parse(dr["AuthorID"].ToString());
                        forumMasterEntity.PostedDate = DateTime.Parse(dr["PostedDate"].ToString());
                        forumMasterEntity.Visited = int.Parse(dr["Visited"].ToString());
                        forumMasterEntity.MsgType = bool.Parse(dr["MsgType"].ToString());
                    }
                }
            }
            return forumMasterEntity;
        }

        #endregion
        //Forum Items
        public static bool ForumItemInsert(ForumItemEntity forumItemEntity)
        {
            int restult;
            restult = ForumItemInsert_Update(forumItemEntity, true, "Deffinity_ForumItemInsert");
            return ((restult > 0) ? true : false);
        }
        public static bool ForumItemUpdate(ForumItemEntity forumItemEntity)
        {
            int restult;
            restult = ForumItemInsert_Update(forumItemEntity, false, "Deffinity_ForumItemUpdate");
            return ((restult > 0) ? true : false);
        }
        private static int ForumItemInsert_Update(ForumItemEntity forumItemEntity, bool SqlType, string spName)
        {

            SqlParameter[] sqlParams;
            if (SqlType)
            {
                sqlParams = new SqlParameter[]{    new SqlParameter("@ForumMasterID", forumItemEntity.ForumMasterID),
                                                   new SqlParameter("@MsgLevel", forumItemEntity.MsgLevel),                                                   
                                                   new SqlParameter("@Rating", forumItemEntity.Rating)
                                                   };
            }
            else
            {
                sqlParams = new SqlParameter[]{    new SqlParameter("@ID", forumItemEntity.ID),                                  
                                                   new SqlParameter("@Rating", forumItemEntity.Rating)
                                                   };
            }
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);

        }
        //public static ForumItemEntity fourmItemSelect(int ID)
        //{
        //    ForumItemEntity forumItemEntity = new ForumItemEntity();
        //    using (SqlConnection cn = new SqlConnection(Constants.DBString))
        //    {
        //        using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_ForumMasterSelect", new SqlParameter("@ID", ID)))
        //        {
        //            while (dr.Read())
        //            {
        //                forumItemEntity.ID = int.Parse(dr["ID"].ToString());
        //                forumItemEntity.ForumMasterID = int.Parse(dr["ForumMasterID"].ToString());
        //                forumItemEntity.SubTitle = dr["SubTitle"].ToString();
        //                forumItemEntity.ReplayMessage = dr["ReplayMessage"].ToString();
        //                forumItemEntity.MsgLevel = int.Parse(dr["MsgLevel"].ToString());
        //                forumItemEntity.Rating = int.Parse(dr["Rating"].ToString());
        //                forumItemEntity.PostedBy = int.Parse(dr["PostedBy"].ToString());
        //                forumItemEntity.PostedDate = DateTime.Parse(dr["PostedDate"].ToString());                       
        //            }
        //        }
        //    }
        //    return forumItemEntity;
        //}
        //file upload

        //@ProjectReference int  
        //   ,@Document image  
        //   ,@ContentType nvarchar(50)  
        //   ,@SourceFileName nvarchar(250)  
        //   ,@DataSize int  
        //   ,@UploadDT datetime  
        //   ,@ApplicationSection char(1)             
        //   ,@UserID int
        //    @ForumID int
        public static int ForumFileInsert_Update(object[] FileData)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, "Deffinity_ForumMasterFileUpload", FileData);
        }
        ////file upload
        ////@ProjectReference int  
        ////   ,@Document image  
        ////   ,@ContentType nvarchar(50)  
        ////   ,@SourceFileName nvarchar(250)  
        ////   ,@DataSize int  
        ////   ,@UploadDT datetime  
        ////   ,@ApplicationSection char(1)             
        ////   ,@UserID int  
        //public static int ForumItemFileInsert_Update(object[] FileData)
        //{
        //    return SqlHelper.ExecuteNonQuery(Constants.DBString, "Deffinity_ForumItemFileUpload", FileData);
        //}
        //************************************************
        //Return's No of vistors
        //parameters
        //@ID int,@UserID int
        //************************************************
        public static int ForumVisitorsUpdate(object[] FileData)
        {
            return SqlHelper.ExecuteNonQuery(Constants.DBString, "Deffinity_ForumVisiters", FileData);
        }
        //************************************************
        //Return's forum mater data
        //parameter        
        //************************************************
        /// <summary>
        ///
        /// </summary>
        /// <param name="projectReference"></param>
        /// <param name="Ftype">forum type</param>
        /// <returns></returns>
        public static DataTable b_ForumMaster(int projectReference,int Ftype)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ForumMasterSelectAll", new SqlParameter("@ProjectReference", projectReference), new SqlParameter("@Ftype", Ftype)).Tables[0];
        }
        //************************************************
        //Return's forum mater data
        //parameter        
        //************************************************
        public static DataTable b_ForumMaster(int projectReference,int ForumID,int Ftype)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ForumMasterSelectAll", new SqlParameter("@ProjectReference", projectReference), new SqlParameter("@ID", ForumID), new SqlParameter("@Ftype", Ftype)).Tables[0];
        }
        
        //************************************************
        //Return's forum mater data
        //parameter        
        //************************************************
        public static DataTable b_ForumMasterFiles(int projectReference, int ForumID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ForumFileDisplay", new SqlParameter("@ProjectReference", projectReference), new SqlParameter("@ForumID", ForumID)).Tables[0];
        }
        ////************************************************
        ////Return's forum mater data
        ////parameter        
        ////************************************************
        //public static DataTable b_ForumItemFiles(int projectReference, int ForumItemID)
        //{
        //    return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ForumFileDisplay", new SqlParameter("@ProjectReference", projectReference), new SqlParameter("@ForumItemId", ForumItemID)).Tables[0];
        //}
        /// <summary>
        /// Delete forum items
        /// </summary>
        /// <param name="ForumID"></param>
        /// <returns></returns>
        public static void b_ForumMasterDelete(int ForumID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ForumMasterDelete", new SqlParameter("@ForumID", ForumID));
        }

    }
}