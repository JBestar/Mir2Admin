using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblNotice
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl0_apps_notice";

        public CTblNotice()
        {
        }
        protected CNotice GetNoticeBySQL(SqlDataReader reader)
        {
            CNotice mNotice         = new CNotice();
            mNotice.notice_cat      = (int)sdr["notice_cat"];
            mNotice.notice_updated  = (DateTime)sdr["notice_updated"];
            mNotice.notice_content  = sdr["notice_content"].ToString();
            mNotice.notice_author   = sdr["notice_author"].ToString();
            return mNotice;
        }

        public Boolean UpdateTblNotice(CNotice mNotice)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "notice_updated=@notice_updated, ";
            sql += "notice_content=@notice_content, ";
            sql += "notice_author=@notice_author ";
            sql += "WHERE notice_cat=@notice_cat";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@notice_updated", DateTime.Now);
                cmd.Parameters.AddWithValue("@notice_content", mNotice.notice_content);
                cmd.Parameters.AddWithValue("@notice_author", mNotice.notice_author);
                cmd.Parameters.AddWithValue("@notice_cat", mNotice.notice_cat);

                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return false;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return true;
        }

        public CNotice GetNoticeByCat(int notice_cat)
        {
            if (notice_cat == 0) return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE notice_cat=@notice_cat";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@notice_cat", notice_cat);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CNotice notice = GetNoticeBySQL(sdr);
                if (conn != null) conn.Close();
                return notice;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }

 
    }
}
