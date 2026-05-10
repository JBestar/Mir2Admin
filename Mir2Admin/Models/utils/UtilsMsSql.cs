using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Mir2Admin.Models
{
    public class CutilsMsSql
    {
        public CutilsMsSql()
        {
        }
        public static SqlConnection GetSqlConn()
        {
            string strConn = "Server="
                + ConfigurationManager.ConnectionStrings["hostname"].ConnectionString
                + "; User Id="
                + ConfigurationManager.ConnectionStrings["username"].ConnectionString
                + "; Password="
                + ConfigurationManager.ConnectionStrings["password"].ConnectionString
                + "; Initial Catalog="
                + ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            return conn;
        }
    }
}