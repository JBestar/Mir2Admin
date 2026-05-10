using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class TblLm2Session
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2";

        public TblLm2Session()
        {
        }
        protected CLm2Session GetSessionBySQL(SqlDataReader reader)
        {
            CLm2Session mSession = new CLm2Session();
            mSession.sess_id = sdr["sess_id"].ToString();
            mSession.sess_time_begin = (DateTime)sdr["sess_time_begin"];
            mSession.sess_time_last = (DateTime)sdr["sess_time_last"];
            mSession.sess_hostname = sdr["sess_hostname"].ToString();
            mSession.sess_browser = sdr["sess_browser"].ToString();
            mSession.sess_pub_addr = sdr["sess_pub_addr"].ToString();
            mSession.sess_mb_uid = sdr["sess_mb_uid"].ToString();
            mSession.sess_emp_fid = (int)sdr["sess_emp_fid"];

            mSession.sess_auto_running = (int)sdr["sess_auto_running"];
            mSession.sess_game_server = sdr["sess_game_server"].ToString();
            mSession.sess_game_serverno = (int)sdr["sess_game_serverno"];
            mSession.sess_game_name = sdr["sess_game_name"].ToString();
            mSession.sess_game_level = (int)sdr["sess_game_level"];  //== 1 ? true : false;
            mSession.sess_game_locale = sdr["sess_game_locale"].ToString();
            
            mSession.sess_game_money1 = (int)sdr["sess_game_money1"];
            mSession.sess_game_money2 = (int)sdr["sess_game_money2"];
            mSession.sess_game_money3 = (int)sdr["sess_game_money3"];
            mSession.sess_game_money4 = (int)sdr["sess_game_money4"];
            mSession.sess_game_money5 = (int)sdr["sess_game_money5"];

            mSession.sess_app_version = sdr["sess_app_version"].ToString();
            mSession.sess_app_title = sdr["sess_app_title"].ToString();

            mSession.sess_client_command = (int)sdr["sess_client_command"];
            mSession.sess_client_memo = sdr["sess_client_memo"].ToString();

            mSession.mb_fid = (int)sdr["mb_fid"];
            mSession.mb_nickname = sdr["mb_nickname"].ToString();
            mSession.mb_level = (int)sdr["mb_level"];
            return mSession;
        }
        public void ClearTblSession()
        {
            DateTime time_check = DateTime.Now;
            time_check = time_check.AddMinutes(-30);         //30분 지난 세션 모두 삭제

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "DELETE FROM " + table_name + "_session WHERE sess_time_last < @sess_time_last";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_time_last", time_check);
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
        public Boolean RegisterTblSession(CLm2Session mSession)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "INSERT INTO " + table_name + "_session (";
            sql += " sess_id, sess_time_begin, sess_time_last, sess_hostname,";
            sql += " sess_browser, sess_pub_addr, sess_emp_fid, sess_mb_uid,";
            sql += " sess_auto_running, sess_game_server, sess_game_serverno, sess_game_name, sess_game_level,";
            sql += " sess_game_locale, sess_game_money1, sess_game_money2, sess_game_money3, ";
            sql += " sess_game_money4, sess_game_money5, ";
            sql += " sess_app_version, sess_app_title, ";
            sql += " sess_client_command, sess_client_memo";
            sql += ") VALUES (";
            sql += " @sess_id, @sess_time_begin, @sess_time_last, @sess_hostname,";
            sql += " @sess_browser, @sess_pub_addr, @sess_emp_fid, @sess_mb_uid,";
            sql += " @sess_auto_running, @sess_game_server, @sess_game_serverno, @sess_game_name, @sess_game_level,";
            sql += " @sess_game_locale, @sess_game_money1, @sess_game_money2, @sess_game_money3, ";
            sql += " @sess_game_money4, @sess_game_money5, ";
            sql += " @sess_app_version, @sess_app_title, ";
            sql += " @sess_client_command, @sess_client_memo";
            sql += ")";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_id", mSession.sess_id);
                cmd.Parameters.AddWithValue("@sess_time_begin", DateTime.Now);
                cmd.Parameters.AddWithValue("@sess_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@sess_hostname", mSession.sess_hostname);
                cmd.Parameters.AddWithValue("@sess_browser", mSession.sess_browser);
                cmd.Parameters.AddWithValue("@sess_pub_addr", mSession.sess_pub_addr);
                cmd.Parameters.AddWithValue("@sess_emp_fid", mSession.sess_emp_fid);
                cmd.Parameters.AddWithValue("@sess_mb_uid", mSession.sess_mb_uid);
                cmd.Parameters.AddWithValue("@sess_auto_running", mSession.sess_auto_running);
                cmd.Parameters.AddWithValue("@sess_game_server", mSession.sess_game_server);
                cmd.Parameters.AddWithValue("@sess_game_serverno", mSession.sess_game_serverno);
                cmd.Parameters.AddWithValue("@sess_game_name", mSession.sess_game_name);
                cmd.Parameters.AddWithValue("@sess_game_level", mSession.sess_game_level);
                cmd.Parameters.AddWithValue("@sess_game_locale", mSession.sess_game_locale);
                cmd.Parameters.AddWithValue("@sess_game_money1", mSession.sess_game_money1);
                cmd.Parameters.AddWithValue("@sess_game_money2", mSession.sess_game_money2);
                cmd.Parameters.AddWithValue("@sess_game_money3", mSession.sess_game_money3);
                cmd.Parameters.AddWithValue("@sess_game_money4", mSession.sess_game_money4);
                cmd.Parameters.AddWithValue("@sess_game_money5", mSession.sess_game_money5);                
                cmd.Parameters.AddWithValue("@sess_app_version", mSession.sess_app_version);
                cmd.Parameters.AddWithValue("@sess_app_title", mSession.sess_app_title);
                cmd.Parameters.AddWithValue("@sess_client_command", mSession.sess_client_command);
                cmd.Parameters.AddWithValue("@sess_client_memo", mSession.sess_client_memo);

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
        public Boolean UpdateTblSession(CLm2Session mSession)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + "_session SET ";
            sql += " sess_time_begin=@sess_time_begin,";
            sql += " sess_time_last=@sess_time_last,";
            sql += " sess_hostname=@sess_hostname,";
            sql += " sess_browser=@sess_browser,";
            sql += " sess_pub_addr=@sess_pub_addr,";
            sql += " sess_auto_running=@sess_auto_running,";
            sql += " sess_game_server=@sess_game_server,";
            sql += " sess_game_serverno=@sess_game_serverno,";
            sql += " sess_game_name=@sess_game_name,";
            sql += " sess_game_level=@sess_game_level,";
            sql += " sess_game_locale=@sess_game_locale,";
            sql += " sess_game_money1=@sess_game_money1,";
            sql += " sess_game_money2=@sess_game_money2,";
            sql += " sess_game_money3=@sess_game_money3,";
            sql += " sess_game_money4=@sess_game_money4,";
            sql += " sess_game_money5=@sess_game_money5,";
            sql += " sess_app_version=@sess_app_version,";
            sql += " sess_app_title=@sess_app_title,";
            sql += " sess_client_command=@sess_client_command,";
            sql += " sess_client_memo=@sess_client_memo,";
            sql += " sess_mb_uid=@sess_mb_uid WHERE sess_id=@sess_id";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@sess_id", mSession.sess_id);
                cmd.Parameters.AddWithValue("@sess_time_begin", mSession.sess_time_begin);
                cmd.Parameters.AddWithValue("@sess_time_last", mSession.sess_time_last);
                cmd.Parameters.AddWithValue("@sess_hostname", mSession.sess_hostname);
                cmd.Parameters.AddWithValue("@sess_browser", mSession.sess_browser);
                cmd.Parameters.AddWithValue("@sess_pub_addr", mSession.sess_pub_addr);
                cmd.Parameters.AddWithValue("@sess_mb_uid", mSession.sess_mb_uid);
                cmd.Parameters.AddWithValue("@sess_auto_running", mSession.sess_auto_running);
                cmd.Parameters.AddWithValue("@sess_game_server", mSession.sess_game_server);
                cmd.Parameters.AddWithValue("@sess_game_serverno", mSession.sess_game_serverno);
                cmd.Parameters.AddWithValue("@sess_game_name", mSession.sess_game_name);
                cmd.Parameters.AddWithValue("@sess_game_level", mSession.sess_game_level);
                cmd.Parameters.AddWithValue("@sess_game_locale", mSession.sess_game_locale);
                cmd.Parameters.AddWithValue("@sess_game_money1", mSession.sess_game_money1);
                cmd.Parameters.AddWithValue("@sess_game_money2", mSession.sess_game_money2);
                cmd.Parameters.AddWithValue("@sess_game_money3", mSession.sess_game_money3);
                cmd.Parameters.AddWithValue("@sess_game_money4", mSession.sess_game_money4);
                cmd.Parameters.AddWithValue("@sess_game_money5", mSession.sess_game_money5);
                cmd.Parameters.AddWithValue("@sess_app_version", mSession.sess_app_version);
                cmd.Parameters.AddWithValue("@sess_app_title", mSession.sess_app_title);
                cmd.Parameters.AddWithValue("@sess_client_command", mSession.sess_client_command);
                cmd.Parameters.AddWithValue("@sess_client_memo", mSession.sess_client_memo);

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
        public CLm2Session GetTblSessionBySessId(string sess_id)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            sql += " JOIN " + table_name + "_member ON " +
                table_name + "_member.mb_uid=" + table_name + "_session.sess_mb_uid ";
            sql += " WHERE sess_id=@sess_id";
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
                CLm2Session mSession = GetSessionBySQL(sdr);
                if (conn != null) conn.Close();
                return mSession;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CLm2Session GetTblSessionByUId(string sess_mb_uid)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            sql += " JOIN " + table_name + "_member ON " +
                table_name + "_member.mb_uid=" + table_name + "_session.sess_mb_uid ";
            sql += " WHERE sess_mb_uid=@sess_mb_uid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@sess_mb_uid", sess_mb_uid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            if (sdr.Read())
            {
                CLm2Session mSession = GetSessionBySQL(sdr);
                if (conn != null) conn.Close();
                return mSession;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public List<CLm2Session> GetTblSessionList(string servName, string characName)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            sql += " JOIN " + table_name + "_member ON " +
                table_name + "_member.mb_uid=" + table_name + "_session.sess_mb_uid ";
            

            if (servName.Length > 0 && characName.Length > 0)
            {
                sql += " WHERE (sess_game_server LIKE @ServerName + '%') ";
                sql += " AND ( sess_mb_uid = @CharacName OR sess_game_name LIKE @CharacName + '%') ";
            }
                
            else if (servName.Length > 0)
            {
                sql += " WHERE sess_game_server LIKE @ServerName + '%'";
            }
            else if (characName.Length > 0)
            {
                sql += " WHERE sess_mb_uid = @CharacName OR sess_game_name LIKE @CharacName + '%' ";
            }

            sql += " ORDER BY sess_mb_uid";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@ServerName", servName);
                cmd.Parameters.AddWithValue("@CharacName", characName);


                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CLm2Session> sessionObjectList = new List<CLm2Session>();
            if (sdr != null)
            {
                while (this.sdr.Read())
                {
                    CLm2Session mSession = GetSessionBySQL(sdr);
                    sessionObjectList.Add(mSession);
                }
            }
            if (conn != null) conn.Close();
            return sessionObjectList;
        }
        public CLm2Session GetTblSessionByGame(int iServerNo, string characName)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            sql += " JOIN " + table_name + "_member ON " +
            table_name + "_member.mb_uid=" + table_name + "_session.sess_mb_uid ";


            sql += " WHERE sess_game_serverno = @ServerNo  ";
            sql += " AND sess_game_name = @CharacName ";
            
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@ServerNo", iServerNo);
                cmd.Parameters.AddWithValue("@CharacName", characName);


                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            if (sdr.Read())
            {
                CLm2Session mSession = GetSessionBySQL(sdr);
                if (conn != null) conn.Close();
                return mSession;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public List<CLm2Session> GetTblSessionByServer(int iEmpFid, int iServerNo)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_session";
            sql += " JOIN " + table_name + "_member ON " +
            table_name + "_member.mb_uid=" + table_name + "_session.sess_mb_uid ";


            sql += " WHERE sess_emp_fid = @Emp_fid  ";
            sql += " AND sess_game_serverno = @ServerNo ";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@ServerNo", iServerNo);
                cmd.Parameters.AddWithValue("@Emp_fid", iEmpFid);


                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CLm2Session> sessionObjectList = new List<CLm2Session>();
            if (sdr != null)
            {
                while (this.sdr.Read())
                {
                    CLm2Session mSession = GetSessionBySQL(sdr);
                    sessionObjectList.Add(mSession);
                }
            }
            if (conn != null) conn.Close();
            return sessionObjectList;
        }

        /// <summary>
        /// sess_pub_addr가 지정 IPv4의 앞 세 옥텟과 같은 세션 행 전체 개수 (_session 테이블 기준).
        /// </summary>
        public int CountTblSessionByPubAddrPrefix(string threeOctetPrefixEndsWithDot)
        {
            if (string.IsNullOrEmpty(threeOctetPrefixEndsWithDot))
                return 0;

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string likePattern = threeOctetPrefixEndsWithDot + "%";
            string sql = "SELECT COUNT(*) FROM " + table_name + "_session";
            sql += " WHERE sess_pub_addr LIKE @like_pattern";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@like_pattern", likePattern);
                object scalar = cmd.ExecuteScalar();
                if (scalar == null || scalar == DBNull.Value)
                    return 0;
                return Convert.ToInt32(scalar);
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
    }
}
