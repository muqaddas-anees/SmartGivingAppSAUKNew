using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Quote.DAL;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for QuoteItemManager
/// </summary>

namespace Quote.BLL
{
    public class QuoteItemManager
    {
        public QuoteItemManager()
        {
        }
        public static int QuoteItemInsert(string IDs, String TypeIds, int UserID, int Projectreference)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.QuoteItemInsert, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StringIds", IDs);
                    cmd.Parameters.AddWithValue("@StringTypes", TypeIds);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@ProjectReference", Projectreference);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int SupplierQuoteItemInsert(string IDs, String TypeIds, int UserID, int Projectreference)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.SupplierReqQuoteItemInsert, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StringIds", IDs);
                    cmd.Parameters.AddWithValue("@StringTypes", TypeIds);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@ProjectReference", Projectreference);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }


        public static void SupplierReqQuoteItemUpdate(int QuoteID, int ItemNo,  int Projectreference,int Qty,int UserID)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.SupplierReqQuoteItemUpdate, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuoteID", QuoteID);
                    cmd.Parameters.AddWithValue("@ItemNo", ItemNo);
                    cmd.Parameters.AddWithValue("@ProjectRef", Projectreference);
                    cmd.Parameters.AddWithValue("@Qty", Qty);
                    cmd.Parameters.AddWithValue("@UserID ",UserID);

                    cmd.ExecuteScalar();
                }
            }
        }

        public static int QuoteInvoiceInsert(int ProjectReference, string DocName, string ContentType, byte[] Document, int Size)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteMainProcs.QuoteInvoiceInsert, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectReference", ProjectReference);
                    cmd.Parameters.AddWithValue("@DocName", DocName);
                    cmd.Parameters.AddWithValue("@ContentType", ContentType);
                    cmd.Parameters.AddWithValue("@Document", Document);
                    cmd.Parameters.AddWithValue("@DataSize", Size);
                    cmd.Parameters.AddWithValue("@FolderName", "Quotations");
                    cmd.Parameters.AddWithValue("@UserID", sessionKeys.UID);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static int QuoteProjectSupplierReqInsert(int ProjectReference, string DocName, string ContentType, byte[] Document, int Size)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteMainProcs.QuoteInvoiceInsert, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectReference", ProjectReference);
                    cmd.Parameters.AddWithValue("@DocName", DocName);
                    cmd.Parameters.AddWithValue("@ContentType", ContentType);
                    cmd.Parameters.AddWithValue("@Document", Document);
                    cmd.Parameters.AddWithValue("@DataSize", Size);
                    cmd.Parameters.AddWithValue("@FolderName", "Supplier Requisitions");
                    cmd.Parameters.AddWithValue("@UserID", sessionKeys.UID);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static int QuoteItemUpdate(QuoteItemsEntity Qi)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using(SqlCommand cmd=new SqlCommand(QuoteItemsProcs.QuoteItemUpdate,con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Qi.ID);
                    cmd.Parameters.AddWithValue("@ItemDescription", Qi.ItemDescription);
                    cmd.Parameters.AddWithValue("@Unitprice", Qi.UnitPrice);
                    cmd.Parameters.AddWithValue("@Qty", Qi.Qty);
                    
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static int QuoteItemDelete(int ID)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.QuoteItemDelete, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@ID", ID);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static int SuplierRequisitionDelete(int ID)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.SupplierReqQuoteItemDelete, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static void QuoteItemDelete()
        {
            
        }
        public  IEnumerable<QuoteItemsEntity> SelectQuoteItemsByQuoteID(int QuoteID,int WorksheetID,int ProjectReference)
        {
            
            List<QuoteItemsEntity> QuoteList = new List<QuoteItemsEntity>();
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, QuoteItemsProcs.QuoteItemsSelectByProject_Worksheet, new SqlParameter[] { new SqlParameter("@QuoteID", QuoteID), new SqlParameter("@WorksheetID", WorksheetID), new SqlParameter("@ProjectReference", ProjectReference) });
            
            while (dr.Read())
            {
                var Qe = new QuoteItemsEntity();
               
                    Qe.ID = int.Parse(dr["ID"].ToString());
                    Qe.QuoteID=int.Parse(dr["QuoteID"].ToString());
                    Qe.Worksheet = dr["Worksheet"].ToString(); 
                    Qe.ItemDescription=dr["ItemDesc"].ToString();
                    Qe.Qty=float.Parse(dr["Qty"].ToString());
                    Qe.UnitPrice = float.Parse(dr["UnitPrice"].ToString());
                    Qe.Total=float.Parse(dr["Total"].ToString());
                    Qe.Unit = dr["Unit"].ToString();
                
                QuoteList.Add(Qe);
            }
            dr.Close();
            dr.Dispose();

            return QuoteList;
        }

        //public QuoteItemsEntity SelectQuoteItem(int QuoteID)
        //{
        //    var qe = from QuoteItemsEntity in SelectQuoteItemsByQuoteID(QuoteID)
        //             where QuoteItemsEntity.QuoteID == QuoteID
        //             select QuoteItemsEntity;
        //    return (QuoteItemsEntity)qe.First();
        //}

        public static void QuoteMainUpdate(int QuoteID, int ProjectReference, string Description, string RequesterName, string RequesterEmail, string RequesterPhoneNo)
        {
            using (SqlConnection con = new SqlConnection(Constants.DBString))
            {
                using (SqlCommand cmd = new SqlCommand(QuoteItemsProcs.QuoteMainUpdate, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuoteID", QuoteID);
                    cmd.Parameters.AddWithValue("@ProjectReference", ProjectReference);
                    cmd.Parameters.AddWithValue("@DescriptionofWorks", Description);
                    cmd.Parameters.AddWithValue("@RequesterName", RequesterName);
                    cmd.Parameters.AddWithValue("@RequesterEmail", RequesterEmail);
                    cmd.Parameters.AddWithValue("@RequesterPhoneNo", RequesterPhoneNo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
