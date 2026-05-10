using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mir2Admin.Models
{
    public class CTblCompSession
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl0_employee";

        public CTblCompSession()
        {
        }

        protected CEmployeeSession GetSessionBySQL(SqlDataReader reader)
        {
            CEmployeeSession mEmployeeSession = new CEmployeeSession();
            mEmployeeSession.sess_id = sdr["sess_id"].ToString();
            mEmployeeSession.sess_time_begin = (DateTime)sdr["sess_time_begin"];
            mEmployeeSession.sess_time_last = (DateTime)sdr["sess_time_last"];
            mEmployeeSession.sess_hostname = sdr["sess_hostname"].ToString();
            mEmployeeSession.sess_browser = sdr["sess_browser"].ToString();
            mEmployeeSession.sess_ipaddress = sdr["sess_ipaddress"].ToString();
            mEmployeeSession.sess_emp_uid = sdr["sess_emp_uid"].ToString();
            mEmployeeSession.sess_emp_level = (int)sdr["sess_emp_level"];
            return mEmployeeSession;
        }
        public void ClearTblSession()
        {
            DateTime time_check = DateTime.Now;
            time_check = time_check.AddMinutes(-60);

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "DELETE FROM " + table_name + "_session WHERE sess_time_last < @time_check";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@time_check", time_check);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        public Boolean RegisterTblSession(CEmployeeSession mEmployeeSession)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "INSERT INTO " + table_name + "_session (";
            sql += " sess_id, sess_time_begin, sess_time_last, ";
            sql += " sess_hostname, sess_browser, sess_ipaddress, sess_emp_uid, sess_emp_level";
            sql += " ) VALUES (";
            sql += " @sess_id, @sess_time_begin, @sess_time_last, ";
            sql += " @sess_hostname, @sess_browser, @sess_ipaddress, @sess_emp_uid, @sess_emp_level";
            sql += ")";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_id", mEmployeeSession.sess_id);
                cmd.Parameters.AddWithValue("@sess_time_begin", mEmployeeSession.sess_time_begin);
                cmd.Parameters.AddWithValue("@sess_time_last", mEmployeeSession.sess_time_last);
                cmd.Parameters.AddWithValue("@sess_hostname", mEmployeeSession.sess_hostname);
                cmd.Parameters.AddWithValue("@sess_browser", mEmployeeSession.sess_browser);
                cmd.Parameters.AddWithValue("@sess_ipaddress", mEmployeeSession.sess_ipaddress);
                cmd.Parameters.AddWithValue("@sess_emp_uid", mEmployeeSession.sess_emp_uid);
                cmd.Parameters.AddWithValue("@sess_emp_level", mEmployeeSession.sess_emp_level);
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
        public Boolean UpdateTblSession(CEmployeeSession mEmployeeSession)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + "_session SET ";
            sql += "sess_time_begin=@sess_time_begin, ";
            sql += "sess_time_last=@sess_time_last, ";
            sql += "sess_hostname=@sess_hostname, ";
            sql += "sess_browser=@sess_browser, ";
            sql += "sess_ipaddress=@sess_ipaddress, ";
            sql += "sess_emp_uid=@sess_emp_uid, ";
            sql += "sess_emp_level=@sess_emp_level ";
            sql += "WHERE sess_id=@sess_id";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_id", mEmployeeSession.sess_id);
                cmd.Parameters.AddWithValue("@sess_time_begin", mEmployeeSession.sess_time_begin);
                cmd.Parameters.AddWithValue("@sess_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@sess_hostname", mEmployeeSession.sess_hostname);
                cmd.Parameters.AddWithValue("@sess_browser", mEmployeeSession.sess_browser);
                cmd.Parameters.AddWithValue("@sess_ipaddress", mEmployeeSession.sess_ipaddress);
                cmd.Parameters.AddWithValue("@sess_emp_uid", mEmployeeSession.sess_emp_uid);
                cmd.Parameters.AddWithValue("@sess_emp_level", mEmployeeSession.sess_emp_level);
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
        public Boolean DeleteTblSession(string sess_id)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "DELETE FROM " + table_name + "_session WHERE sess_id=@sess_id";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_id", sess_id);
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
        public CEmployeeSession GetSessionBySessId(string sess_id)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session WHERE sess_id=@sess_id";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@sess_id", sess_id);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            if (sdr.Read())
            {
                CEmployeeSession mEmployeeSession = GetSessionBySQL(sdr);
                if (conn != null) conn.Close();
                return mEmployeeSession;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CEmployeeSession GetSessionByEmpUId(string emp_uid)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session WHERE sess_emp_uid=@sess_emp_uid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@sess_emp_uid", emp_uid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            if (sdr.Read())
            {
                CEmployeeSession mEmployeeSession = GetSessionBySQL(sdr);
                if (conn != null) conn.Close();
                return mEmployeeSession;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public List<CEmployeeSession> GetEmployeeSessionList()
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            cmd = new SqlCommand(sql, conn);

            try
            {
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CEmployeeSession> sessionObjectList = new List<CEmployeeSession>();
            while (this.sdr.Read())
            {
                CEmployeeSession mSession = GetSessionBySQL(sdr);
                sessionObjectList.Add(mSession);
            }

            if (conn != null) conn.Close();
            return sessionObjectList;
        }



    }
}