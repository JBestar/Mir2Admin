using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mir2Admin.Models
{
    public class CTblCompEmployee
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl0_employee";

        public CTblCompEmployee()
        {
        }
        protected CEmployee GetEmployeeBySQL(SqlDataReader reader)
        {
            CEmployee mEmployee = new CEmployee();
            mEmployee.emp_fid = (int)sdr["emp_fid"];
            mEmployee.emp_uid = sdr["emp_uid"].ToString();
            mEmployee.emp_pwd = sdr["emp_pwd"].ToString();
            mEmployee.emp_level = (int)sdr["emp_level"];
            mEmployee.emp_company_fid = (int)sdr["emp_company_fid"];
            mEmployee.emp_nickname = sdr["emp_nickname"].ToString();
            mEmployee.emp_time_join = (DateTime)sdr["emp_time_join"];
            mEmployee.emp_time_last = (DateTime)sdr["emp_time_last"];
            mEmployee.emp_last_ipaddr = sdr["emp_last_ipaddr"].ToString();
            mEmployee.emp_color = sdr["emp_color"].ToString();
            mEmployee.emp_state_active = (int)sdr["emp_state_active"] == 1 ? true : false;
            mEmployee.emp_comment = sdr["emp_comment"].ToString();
            mEmployee.emp_b_app_01_lm2 = (int)sdr["emp_b_app_01"] == 1 ? true : false;
//             mEmployee.emp_b_app_02_luckypal = (int)sdr["emp_b_app_02"] == 1 ? true : false;
//             mEmployee.emp_b_app_03_cross = (int)sdr["emp_b_app_03"] == 1 ? true : false;
//             mEmployee.emp_b_app_04_mpowerball = (int)sdr["emp_b_app_04"] == 1 ? true : false;
//             mEmployee.emp_b_app_05_paradais = (int)sdr["emp_b_app_05"] == 1 ? true : false;
//             mEmployee.emp_b_app_06_ypowerball = (int)sdr["emp_b_app_06"] == 1 ? true : false;
//             mEmployee.emp_b_app_07 = (int)sdr["emp_b_app_07"] == 1 ? true : false;
//             mEmployee.emp_b_app_08 = (int)sdr["emp_b_app_08"] == 1 ? true : false;
//             mEmployee.emp_b_app_09 = (int)sdr["emp_b_app_09"] == 1 ? true : false;
//             mEmployee.emp_b_app_10 = (int)sdr["emp_b_app_10"] == 1 ? true : false;
            return mEmployee;
        }

        public Boolean RegisterTblEmployee(CEmployee mEmployee)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "INSERT INTO " + table_name + " (";
            sql += "emp_uid, emp_pwd, emp_level, emp_company_fid, emp_nickname, ";
            sql += "emp_time_join, emp_time_last, emp_last_ipaddr, emp_color, ";
            sql += "emp_state_active, emp_comment, ";
            sql += "emp_b_app_01, emp_b_app_02, emp_b_app_03, emp_b_app_04, emp_b_app_05, ";
            sql += "emp_b_app_06, emp_b_app_07, emp_b_app_08, emp_b_app_09, emp_b_app_10";
            sql += ") VALUES (";
            sql += "@emp_uid, @emp_pwd, @emp_level, @emp_company_fid, @emp_nickname, ";
            sql += "@emp_time_join, @emp_time_last, @emp_last_ipaddr, @emp_color, ";
            sql += "@emp_state_active, @emp_comment, ";
            sql += "@emp_b_app_01, @emp_b_app_02, @emp_b_app_03, @emp_b_app_04, @emp_b_app_05, ";
            sql += "@emp_b_app_06, @emp_b_app_07, @emp_b_app_08, @emp_b_app_09, @emp_b_app_10";
            sql += ")";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_uid", mEmployee.emp_uid);
                cmd.Parameters.AddWithValue("@emp_pwd", mEmployee.emp_pwd);
                cmd.Parameters.AddWithValue("@emp_level", mEmployee.emp_level);
                cmd.Parameters.AddWithValue("@emp_company_fid", mEmployee.emp_company_fid);
                cmd.Parameters.AddWithValue("@emp_nickname", mEmployee.emp_nickname);
                cmd.Parameters.AddWithValue("@emp_time_join", DateTime.Now);
                cmd.Parameters.AddWithValue("@emp_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@emp_last_ipaddr", mEmployee.emp_last_ipaddr);
                cmd.Parameters.AddWithValue("@emp_color", mEmployee.emp_color);
                cmd.Parameters.AddWithValue("@emp_state_active", mEmployee.emp_state_active ? 1 : 0);
                cmd.Parameters.AddWithValue("@emp_comment", mEmployee.emp_comment);
                cmd.Parameters.AddWithValue("@emp_b_app_01", 1 );
                cmd.Parameters.AddWithValue("@emp_b_app_02", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_03", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_04", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_05", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_06", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_07", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_08", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_09", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_10", 1);
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
        public Boolean UpdateTblEmployee(CEmployee mEmployee)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "emp_uid=@emp_uid, ";
            sql += "emp_pwd=@emp_pwd, ";
            sql += "emp_level=@emp_level, ";
            sql += "emp_company_fid=@emp_company_fid, ";
            sql += "emp_nickname=@emp_nickname, ";
            sql += "emp_time_join=@emp_time_join,";
            sql += "emp_time_last=@emp_time_last,";
            sql += "emp_last_ipaddr=@emp_last_ipaddr,";
            sql += "emp_color=@emp_color,";
            sql += "emp_state_active=@emp_state_active,";
            sql += "emp_comment=@emp_comment,";
            sql += "emp_b_app_01=@emp_b_app_01,";
            sql += "emp_b_app_02=@emp_b_app_02,";
            sql += "emp_b_app_03=@emp_b_app_03,";
            sql += "emp_b_app_04=@emp_b_app_04,";
            sql += "emp_b_app_05=@emp_b_app_05,";
            sql += "emp_b_app_06=@emp_b_app_06,";
            sql += "emp_b_app_07=@emp_b_app_07,";
            sql += "emp_b_app_08=@emp_b_app_08, ";
            sql += "emp_b_app_09=@emp_b_app_09, ";
            sql += "emp_b_app_10=@emp_b_app_10 ";
            sql += "WHERE emp_fid=@emp_fid";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mEmployee.emp_fid);
                cmd.Parameters.AddWithValue("@emp_uid", mEmployee.emp_uid);
                cmd.Parameters.AddWithValue("@emp_pwd", mEmployee.emp_pwd);
                cmd.Parameters.AddWithValue("@emp_level", mEmployee.emp_level);
                cmd.Parameters.AddWithValue("@emp_company_fid", mEmployee.emp_company_fid);
                cmd.Parameters.AddWithValue("@emp_nickname", mEmployee.emp_nickname);
                cmd.Parameters.AddWithValue("@emp_time_join", DateTime.Now);
                cmd.Parameters.AddWithValue("@emp_time_last", DateTime.Now);
                cmd.Parameters.AddWithValue("@emp_last_ipaddr", mEmployee.emp_last_ipaddr);
                cmd.Parameters.AddWithValue("@emp_color", mEmployee.emp_color);
                cmd.Parameters.AddWithValue("@emp_state_active", mEmployee.emp_state_active ? 1 : 0);
                cmd.Parameters.AddWithValue("@emp_comment", mEmployee.emp_comment);
                cmd.Parameters.AddWithValue("@emp_b_app_01", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_02", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_03", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_04", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_05", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_06", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_07", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_08", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_09", 1);
                cmd.Parameters.AddWithValue("@emp_b_app_10", 1);

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

        public Boolean UpdateLastTimeTblEmployee(CEmployee mEmployee)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "emp_time_last=@emp_time_last ";
            sql += "WHERE emp_fid=@emp_fid";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mEmployee.emp_fid);
                cmd.Parameters.AddWithValue("@emp_time_last", DateTime.Now);
                
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
        public Boolean DeleteTblEmployee(int emp_fid)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE " + table_name + " WHERE emp_fid=@emp_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", emp_fid);
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
        public CEmployee GetEmployeeByFid(int emp_fid)
        {
            if (emp_fid == 0) return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE emp_fid=@emp_fid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", emp_fid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CEmployee employee = GetEmployeeBySQL(sdr);
                if (conn != null) conn.Close();
                return employee;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CEmployee GetEmployeeByUid(string emp_uid)
        {
            if (emp_uid == "") return null;
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE emp_uid=@emp_uid";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_uid", emp_uid);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CEmployee employee = GetEmployeeBySQL(sdr);
                if (conn != null) conn.Close();
                return employee;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }
        public CEmployee GetEmployeeByNickName(string emp_nickname)
        {
            if (emp_nickname == "") return null;

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE emp_nickname=@emp_nickname";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_nickname", emp_nickname);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CEmployee employee = GetEmployeeBySQL(sdr);
                if (conn != null) conn.Close();
                return employee;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }

        public List<CEmployee> GetAllEmployeeList()
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE emp_fid > 0";
            cmd = new SqlCommand(sql, conn);

            try
            {
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CEmployee> employeeObjectList = new List<CEmployee>();
            while (this.sdr.Read())
            {
                CEmployee mEmployee = GetEmployeeBySQL(sdr);
                employeeObjectList.Add(mEmployee);
            }
            if (conn != null) conn.Close();
            return employeeObjectList;
        }


    }
}