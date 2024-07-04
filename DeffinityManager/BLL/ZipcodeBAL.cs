using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace DeffinityManager.BLL
{
    public class ZipcodeBAL
    {

        public static List<string> GetNearByZipCodes(string zipcode, int range)
        {
            List<string> retZip = new List<string>();
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "ZipCodes_GetNearBy", new SqlParameter("@zipcode", zipcode), new SqlParameter("@range", range));
                while (dr.Read())
                {
                    retZip.Add(dr["zipcode"].ToString());
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retZip;
        }

        //return number of postcodes are added
        public static int AddNearByZipCodes(int userid,string zipcode, int range)
        {
            int cnt = 0;
            IUserRepository<UserMgt.Entity.UserAssociatedPostcode> rPost = new UserRepository<UserMgt.Entity.UserAssociatedPostcode>();

            var existingZiplist = rPost.GetAll().Where(o => o.UserID == userid).ToList();
            var Ziplist = GetNearByZipCodes(zipcode, range);
            foreach(var z in Ziplist)
            {
                //check alreay exists the postcode
                if(existingZiplist.Where(o=>o.Postcode == z.ToString()).Count() ==0)
                {
                    var c = new UserMgt.Entity.UserAssociatedPostcode();
                    c.Postcode = z.ToString();
                    c.UserID = userid;
                    rPost.Add(c);
                    cnt++;
                }
            }
            return cnt;
        }

        //Update postcodes
        public static int UpdateNearByZipCodes(int userid, string zipcode, int range)
        {
            int cnt = 0;
            IUserRepository<UserMgt.Entity.UserAssociatedPostcode> rPost = new UserRepository<UserMgt.Entity.UserAssociatedPostcode>();

            var existingZiplist = rPost.GetAll().Where(o => o.UserID == userid).ToList();
            //delete all existing records
            if (existingZiplist.Count > 0)
            {
                rPost.DeleteAll(existingZiplist);
            }
            //add zipcode
           cnt = AddNearByZipCodes(userid, zipcode, range);

           return cnt;
        }
    }
}
