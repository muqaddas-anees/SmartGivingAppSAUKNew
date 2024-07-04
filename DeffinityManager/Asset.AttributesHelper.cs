using System;
using System.Collections.Generic;
using System.Web;
using AssetConfigurator.Entity;
using System.Data.SqlClient;
using System.Data;

namespace AssetConfigurator.DAL
{
    public class AttributesHelper
    {

        public List<AttributesEntity> Select(int Id)
        {
            List<AttributesEntity> attributesCollection = new List<AttributesEntity>();
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[AttributesById]", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AttributesEntity tempAttributes = new AttributesEntity();
                            tempAttributes.Id = Convert.ToInt32(reader["Id"]);
                            tempAttributes.AssetId = Convert.ToInt32(reader["AssetId"]);
                            tempAttributes.Attachment = Convert.ToBoolean(reader["Attachment"]);
                            tempAttributes.AttributeName = reader["AttributeName"].ToString();
                            tempAttributes.Type = reader["Type"].ToString();
                            attributesCollection.Add(tempAttributes);
                        }
                    }
                }
            }
            return attributesCollection;
        }

        public int Add(AttributesEntity attributes)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("AssetType", attributes.AssetId );
            parameters[1] = new SqlParameter("Attachment", attributes.Attachment);
            parameters[2] = new SqlParameter("AttributeName", attributes.AttributeName);
            parameters[3] = new SqlParameter("Type", attributes.Type);
            SqlCommand cmd = new SqlCommand();
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                cmd.Connection = conn;
                for (int i = 0; i < 4; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Asset].[AttributesInsertion]";
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int Edit(int ID)
        {
            throw new NotImplementedException("Yet to be implemented");
        }

        public int Delete(int ID)
        {
            using(SqlConnection conn=new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand("[Asset].[DeleteImage]", conn))
                {
                    cmd.Parameters.Add("ID", ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public AttributesHelper()
        {
                      
        }
    }
}