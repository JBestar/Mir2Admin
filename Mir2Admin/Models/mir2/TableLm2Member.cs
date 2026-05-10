using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class TblLm2Member
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2";

        public TblLm2Member()
        {
        }
        protected CLm2Member GetMemberBySQL(SqlDataReader reader)
        {
            CLm2Member member = new CLm2Member();
            member.mb_fid = (int)sdr["mb_fid"];
            member.mb_emp_fid = (int)sdr["mb_emp_fid"];
            member.mb_uid = sdr["mb_uid"].ToString();
            member.mb_pwd = sdr["mb_pwd"].ToString();
            member.mb_nickname = sdr["mb_nickname"].ToString();
            member.mb_handphone = sdr["mb_handphone"].ToString();
            member.mb_level = (int)sdr["mb_level"];
            member.mb_vip = (int)sdr["mb_vip"] == 1 ? true : false;

            member.mb_domain = sdr["mb_domain"].ToString();
            member.mb_username = sdr["mb_username"].ToString();
            member.mb_password = sdr["mb_password"].ToString();
            member.mb_local_key = sdr["mb_local_key"].ToString();
            member.mb_time_join = (DateTime)sdr["mb_time_join"];
            member.mb_time_last = (DateTime)sdr["mb_time_last"];
            member.mb_time_limit = (DateTime)sdr["mb_time_limit"];
            member.mb_time_limit = member.mb_time_limit.Date.AddDays(1);
            member.mb_last_ipaddr = sdr["mb_last_ipaddr"].ToString();
            member.mb_state_active = (int)sdr["mb_state_active"] == 1 ? true : false;
            member.mb_state_delete = (int)sdr["mb_state_delete"] == 1 ? true : false;

            member.mb_bgcolor = sdr["emp_color"].ToString();
            member.label_company = sdr["emp_nickname"].ToString();

            return member;
        }
        public Boolean RegisterTblMember(CLm2Member member)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + "_member (";
            sql += " mb_emp_fid, mb_uid, mb_pwd, mb_nickname, mb_handphone, mb_level, mb_vip, ";
            sql += " mb_domain, mb_username, mb_password, mb_local_key, ";
            sql += " mb_time_join, mb_time_last, mb_time_limit, ";
            sql += " mb_last_ipaddr, mb_state_active, mb_state_delete";
            sql += ") values (";
            sql += " @mb_emp_fid, @mb_uid, @mb_pwd, @mb_nickname, @mb_handphone, @mb_level, @mb_vip, ";
            sql += " @mb_domain, @mb_username, @mb_password, @mb_local_key, ";
            sql += " @mb_time_join, @mb_time_last, @mb_time_limit, ";
            sql += " @mb_last_ipaddr, @mb_state_active, @mb_state_delete";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@mb_fid", member.mb_fid);
                cmd.Parameters.AddWithValue("@mb_emp_fid", member.mb_emp_fid);
                cmd.Parameters.AddWithValue("@mb_uid", member.mb_uid);
                cmd.Parameters.AddWithValue("@mb_pwd", member.mb_pwd);
                cmd.Parameters.AddWithValue("@mb_nickname", member.mb_nickname);
                cmd.Parameters.AddWithValue("@mb_handphone", member.mb_handphone);
                cmd.Parameters.AddWithValue("@mb_level", member.mb_level);
                cmd.Parameters.AddWithValue("@mb_vip", member.mb_vip ? 1 : 0);
                cmd.Parameters.AddWithValue("@mb_domain", member.mb_domain);
                cmd.Parameters.AddWithValue("@mb_username", member.mb_username);
                cmd.Parameters.AddWithValue("@mb_password", member.mb_password);
                cmd.Parameters.AddWithValue("@mb_local_key", member.mb_local_key);
                cmd.Parameters.AddWithValue("@mb_time_join", DateTime.Now);
                cmd.Parameters.AddWithValue("@mb_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@mb_time_limit", member.mb_time_limit);
                cmd.Parameters.AddWithValue("@mb_last_ipaddr", member.mb_last_ipaddr);
                cmd.Parameters.AddWithValue("@mb_state_active", member.mb_state_active ? 1 : 0);
                cmd.Parameters.AddWithValue("@mb_state_delete", member.mb_state_delete ? 1 : 0);
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
        public Boolean UpdateTblMember(CLm2Member member)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "UPDATE " + table_name + "_member SET ";
            sql += " mb_emp_fid=@mb_emp_fid, ";
            sql += " mb_uid=@mb_uid, ";
            sql += " mb_pwd=@mb_pwd, ";
            sql += " mb_nickname=@mb_nickname, ";
            sql += " mb_handphone=@mb_handphone, ";
            sql += " mb_level=@mb_level, ";
            sql += " mb_vip=@mb_vip, ";
            sql += " mb_domain=@mb_domain, ";
            sql += " mb_username=@mb_username, ";
            sql += " mb_password=@mb_password, ";
            sql += " mb_local_key=@mb_local_key, ";
            //sql += " mb_time_join=@mb_time_join, ";
            //sql += " mb_time_last=@mb_time_last, ";
            sql += " mb_time_limit=@mb_time_limit, ";
            sql += " mb_last_ipaddr=@mb_last_ipaddr, ";
            sql += " mb_state_active=@mb_state_active, ";
            sql += " mb_state_delete=@mb_state_delete";
            sql += " WHERE mb_fid=@mb_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_fid", member.mb_fid);
                cmd.Parameters.AddWithValue("@mb_emp_fid", member.mb_emp_fid);
                cmd.Parameters.AddWithValue("@mb_uid", member.mb_uid);
                cmd.Parameters.AddWithValue("@mb_pwd", member.mb_pwd);
                cmd.Parameters.AddWithValue("@mb_nickname", member.mb_nickname);
                cmd.Parameters.AddWithValue("@mb_handphone", member.mb_handphone);
                cmd.Parameters.AddWithValue("@mb_level", member.mb_level);
                cmd.Parameters.AddWithValue("@mb_vip", member.mb_vip ? 1 : 0);
                cmd.Parameters.AddWithValue("@mb_domain", member.mb_domain);
                cmd.Parameters.AddWithValue("@mb_username", member.mb_username);
                cmd.Parameters.AddWithValue("@mb_password", member.mb_password);
                cmd.Parameters.AddWithValue("@mb_local_key", member.mb_local_key);
                //cmd.Parameters.AddWithValue("@mb_time_join", DateTime.Now);
                //cmd.Parameters.AddWithValue("@mb_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@mb_time_limit", member.mb_time_limit);
                cmd.Parameters.AddWithValue("@mb_last_ipaddr", member.mb_last_ipaddr);
                cmd.Parameters.AddWithValue("@mb_state_active", member.mb_state_active ? 1 : 0);
                cmd.Parameters.AddWithValue("@mb_state_delete", member.mb_state_delete ? 1 : 0);
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


        public Boolean UpdateLastTimeTblMember(CLm2Member member)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "UPDATE " + table_name + "_member SET ";
            sql += " mb_time_last=@mb_time_last";
            sql += " WHERE mb_fid=@mb_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_fid", member.mb_fid);
                cmd.Parameters.AddWithValue("@mb_time_last", DateTime.Now);
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

        public Boolean DeleteTblMember(int mb_fid)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE " + table_name + "_member WHERE mb_fid=@mb_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_fid", mb_fid);
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
        public CLm2Member GetMemberByFid(int mb_fid)
        {
            if (mb_fid == 0) return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_member";
            sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + "_member.mb_emp_fid ";
            sql += " WHERE mb_fid=@mb_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_fid", mb_fid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CLm2Member member = GetMemberBySQL(sdr);
                if (conn != null) conn.Close();
                return member;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CLm2Member GetMemberByUid(string mb_uid)
        {
            if (mb_uid == "") return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_member";
            sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + "_member.mb_emp_fid ";
            sql += " WHERE mb_uid=@mb_uid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_uid", mb_uid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CLm2Member member = GetMemberBySQL(sdr);
                if (conn != null) conn.Close();
                return member;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CLm2Member GetMemberByNickName(string mb_nickname)
        {
            if (mb_nickname == "") return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_member";
            sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + "_member.mb_emp_fid ";
            sql += " WHERE mb_nickname=@mb_nickname";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@mb_nickname", mb_nickname);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CLm2Member member = GetMemberBySQL(sdr);
                if (conn != null) conn.Close();
                return member;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public List<CLm2Member> GetMemberList(List<int> employeeIds, string searchWords)
        {
            List<CLm2Member> memberObjectList = new List<CLm2Member>();

            string sql_inarray = "";
            if (employeeIds != null)
            {
                if (employeeIds.Count > 0)
                {
                    int idsLast = employeeIds.Count - 1;
                    sql_inarray += "(";
                    for (int i = 0; i < employeeIds.Count; i++)
                    {
                        if (i == idsLast) sql_inarray += "'" + employeeIds[i] + "'";
                        else sql_inarray += "'" + employeeIds[i] + "',";
                    }
                    sql_inarray += ")";
                }
            }

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name + "_member";
            sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + "_member.mb_emp_fid ";
            sql += " WHERE mb_fid > 0 ";

            if (employeeIds != null && sql_inarray != "")
                sql += " AND mb_emp_fid IN " + sql_inarray;
            if (searchWords.Length > 0) {
                //sql += " AND (mb_uid LIKE '%@search%' OR mb_nickname LIKE '%@search%' OR ";
                sql += " AND ( mb_uid = @search OR mb_nickname = @search )";

            }
            sql += " ORDER BY mb_time_join";   //mb_uid";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@search", searchWords);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            while (this.sdr.Read())
            {
                CLm2Member member = GetMemberBySQL(sdr);
                memberObjectList.Add(member);
            }

            if (conn != null) conn.Close();
            return memberObjectList;
        }

    }
}
