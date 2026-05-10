using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblUpdate
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2_update";

        public CTblUpdate()
        {
        }
        protected CUpdate GetUpdateBySQL(SqlDataReader reader)
        {
            CUpdate mUpdate         = new CUpdate();
            mUpdate.update_no       = (int)sdr["update_no"];
            mUpdate.update_version = sdr["update_version"].ToString();
            mUpdate.update_path = sdr["update_path"].ToString();
            mUpdate.update_date     = (DateTime)sdr["update_date"];
            mUpdate.update_content  = sdr["update_content"].ToString();
            mUpdate.update_author   = sdr["update_author"].ToString();
            return mUpdate;
        }

        public Boolean AddTblUpdate(CUpdate mUpdate)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + " (";
            sql += " update_version, update_path, update_date, update_content, update_author ";
            sql += ") values (";
            sql += " @update_version, @update_path, @update_date, @update_content, @update_author ";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                //cmd.Parameters.AddWithValue("@update_no", mUpdate.update_no);
                cmd.Parameters.AddWithValue("@update_version", mUpdate.update_version);
                cmd.Parameters.AddWithValue("@update_path", mUpdate.update_path);
                cmd.Parameters.AddWithValue("@update_date", mUpdate.update_date);
                cmd.Parameters.AddWithValue("@update_content", mUpdate.update_content);
                cmd.Parameters.AddWithValue("@update_author", mUpdate.update_author);
                
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

        public Boolean UpdateTblUpdate(CUpdate mUpdate)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "update_path=@update_path, ";
            sql += "update_date=@update_date, ";
            sql += "update_content=@update_content, ";
            sql += "update_author=@update_author ";
            sql += "WHERE update_version=@update_version";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@update_date", DateTime.Now);
                cmd.Parameters.AddWithValue("@update_content", mUpdate.update_content);
                cmd.Parameters.AddWithValue("@update_author", mUpdate.update_author);
                cmd.Parameters.AddWithValue("@update_path", mUpdate.update_path);
                cmd.Parameters.AddWithValue("@update_version", mUpdate.update_version);

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

        public CUpdate GetUpdateByVersion(string strVersion)
        {
            if (strVersion.Length < 1) return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE update_version=@update_version";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@update_version", strVersion);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CUpdate updateObj = GetUpdateBySQL(sdr);
                if (conn != null) conn.Close();
                return updateObj;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }

        public Boolean DeleteVersion(string strVersion)
        {
            if (strVersion.Length < 1) return false;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE FROM " + table_name;
            sql += " WHERE update_version=@update_version";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@update_version", strVersion);
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

        public List<CUpdate> GetAllUpdateList()
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql +=" ORDER BY 'update_version' DESC";
            cmd = new SqlCommand(sql, conn);

            try
            {
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CUpdate> updateList = new List<CUpdate>();
            while (this.sdr.Read())
            {
                CUpdate mUpdate = GetUpdateBySQL(sdr);
                updateList.Add(mUpdate);
            }
            if (conn != null) conn.Close();
            return updateList;
        }
    }
}
