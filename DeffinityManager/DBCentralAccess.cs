using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Web;


namespace DeffinityManager
{
    public class DBCentralAccess
    {
        private static string getDBConnectionString(string InstanceName)
        {
            string retval = string.Empty;
            try
            {
                var dr = SqlHelper.ExecuteReader(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBCentral"].ToString(), CommandType.Text,
                    "select ID,Name,DBserver,DBname,DBuser,DBpwd from InstanceDetails where lower(Name) = lower(@name)", new SqlParameter("@name", InstanceName));
                while (dr.Read())
                {
                    retval = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Pooling=True;Min Pool Size=0;Max Pool Size=100;Connect Timeout=60"
                        , dr["DBserver"].ToString(), dr["DBname"].ToString(), dr["DBuser"].ToString(), dr["DBpwd"].ToString());
                }
                dr.Close();
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
            return retval;
        }
        public static string setInstance(string InstanceName)
        {
            string instance = string.Empty;

            if (HttpContext.Current.Application["instance"] == null)
            {
                HttpContext.Current.Application["instance"] = InstanceName;
            }
            instance = HttpContext.Current.Application["instance"].ToString();
            return instance;
        }
        public static void ClearInstance()
        {
            HttpContext.Current.Application["instance"] = null;
        }
        public static string getInstance()
        {
            string instance = string.Empty;
            if (HttpContext.Current.Application["instance"] != null)
            {
                instance = HttpContext.Current.Application["instance"].ToString();
            }
            else
            {
                instance = string.Empty;
            }
            return instance;
        }
        //Get Connection From Central database
        public static string getConnectionString()
        {
            string cn = string.Empty;
            string instancename = getInstance();
            if (HttpContext.Current.Application["cn" + instancename] == null)
            {
                HttpContext.Current.Application["cn" + instancename] = getDBConnectionString(instancename);
            }
            cn = HttpContext.Current.Application["cn" + instancename].ToString();
            return cn;
        }
      
    }
}
