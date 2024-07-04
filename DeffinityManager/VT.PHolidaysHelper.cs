using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VT.PHEntity;
using VT;
/// <summary>
/// Summary description for VT
/// </summary>
namespace VT.PHDAL
{


    public class VTPHolidaysHelpher
    {
        public VTPHolidaysHelpher()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VTPHolidaysCollection Select(int year)
        {
            VTPHolidaysCollection VTPHCollection = new VTPHolidaysCollection();
           
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(VTPHolidaysProcedure.Select, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VTPHolidays vtpholiday = new VTPHolidays();
                            vtpholiday.ID = Convert.ToInt32(reader["ID"]);
                            vtpholiday.Description = reader["Description"].ToString();
                            vtpholiday.Date = Convert.ToDateTime(reader["Date"].ToString());
                            vtpholiday.AnnualReoccurence = Convert.ToBoolean(reader["AnnualReoccurence"].ToString());
                            VTPHCollection.Add(vtpholiday);

                        }
                    }
                }
            }

            return VTPHCollection;
        }

        public int Insert(VTPHolidays vtpholidays)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(VTPHolidaysProcedure.Insert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Description", vtpholidays.Description);
                    cmd.Parameters.AddWithValue("Date", vtpholidays.Date);
                    cmd.Parameters.AddWithValue("AnnualReoccurence", vtpholidays.AnnualReoccurence);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(VTPHolidays vtpholidays)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(VTPHolidaysProcedure.Update, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", vtpholidays.ID);
                    cmd.Parameters.AddWithValue("Description", vtpholidays.Description);
                    cmd.Parameters.AddWithValue("Date", vtpholidays.Date);
                    cmd.Parameters.AddWithValue("AnnualReoccurence", vtpholidays.AnnualReoccurence);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(VTPHolidaysProcedure.Delete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", id);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int InsertLastyearLeave(int Year)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(VTPHolidaysProcedure.LastYearInsert, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Year", Year);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }

        }
    }
}