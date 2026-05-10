using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblBlackList
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2_blacklist";

        public CTblBlackList()
        {
        }
        protected CBlackList GetCBlackListBySQL(SqlDataReader reader)
        {
            CBlackList mBlackList = new CBlackList();
            mBlackList.black_no = (int)sdr["black_no"];
            mBlackList.black_mb_uid = sdr["black_mb_uid"].ToString();
            mBlackList.black_emp_fid = (int)sdr["black_emp_fid"];
            mBlackList.black_server_no = (int)sdr["black_server_no"];
            mBlackList.black_player_name = sdr["black_player_name"].ToString();
            mBlackList.black_update_date = (DateTime)sdr["black_update_date"];
            mBlackList.black_player_enable = (int)sdr["black_player_enable"];
            mBlackList.black_player_content = sdr["black_player_content"].ToString();

            return mBlackList;
        }

        public Boolean AddTblBlackList(CBlackList mBlackList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + " (";
            sql += " black_emp_fid, black_mb_uid, black_server_no, black_player_name, black_update_date, black_player_enable, black_player_content ";
            sql += ") values (";
            sql += " @black_emp_fid, @black_mb_uid, @black_server_no, @black_player_name, @black_update_date, @black_player_enable, @black_player_content ";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@black_mb_uid", mBlackList.black_mb_uid);
                cmd.Parameters.AddWithValue("@black_emp_fid", mBlackList.black_emp_fid);
                cmd.Parameters.AddWithValue("@black_server_no", mBlackList.black_server_no);
                cmd.Parameters.AddWithValue("@black_player_name", mBlackList.black_player_name);
                cmd.Parameters.AddWithValue("@black_update_date", mBlackList.black_update_date);
                cmd.Parameters.AddWithValue("@black_player_enable", mBlackList.black_player_enable);
                cmd.Parameters.AddWithValue("@black_player_content", mBlackList.black_player_content);

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

        public Boolean DeleteTblBlackList(CBlackList mBlackList)
        {

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE FROM " + table_name;
            sql += " WHERE black_emp_fid=@black_emp_fid";

            if (mBlackList.black_server_no > 0)
                sql += " AND black_server_no=@black_server_no";

            if (mBlackList.black_player_name.Length > 0)
                sql += " AND black_player_name=@black_player_name";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@black_emp_fid", mBlackList.black_emp_fid);
                cmd.Parameters.AddWithValue("@black_server_no", mBlackList.black_server_no);
                cmd.Parameters.AddWithValue("@black_player_name", mBlackList.black_player_name);

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


        public Boolean UpdateTblBlackList(CBlackList mBlackList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += " black_update_date=@black_update_date, ";
            sql += " black_player_enable=@black_player_enable ";
            sql += " WHERE black_emp_fid=@black_emp_fid ";
            sql += " AND black_server_no=@black_server_no ";
            sql += " AND black_player_name=@black_player_name";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@black_emp_fid", mBlackList.black_emp_fid);
                cmd.Parameters.AddWithValue("@black_server_no", mBlackList.black_server_no);
                cmd.Parameters.AddWithValue("@black_player_name", mBlackList.black_player_name);
                cmd.Parameters.AddWithValue("@black_update_date", mBlackList.black_update_date);
                cmd.Parameters.AddWithValue("@black_player_enable", mBlackList.black_player_enable);

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


        public List<CBlackList> GetAllBlackList(CBlackList mBlackList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE black_emp_fid=@black_emp_fid";
            if (mBlackList.black_server_no > 0)
                sql += " AND black_server_no=@black_server_no";
            if (mBlackList.black_player_name.Length > 0)
                sql += " AND black_player_name=@black_player_name";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@black_emp_fid", mBlackList.black_emp_fid);
                cmd.Parameters.AddWithValue("@black_server_no", mBlackList.black_server_no);
                cmd.Parameters.AddWithValue("@black_player_name", mBlackList.black_player_name);

                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CBlackList> blackList = new List<CBlackList>();
            while (this.sdr.Read())
            {
                CBlackList mBlack = GetCBlackListBySQL(sdr);
                blackList.Add(mBlack);
            }
            if (conn != null) conn.Close();
            return blackList;
        }

        public CBlackList GetBlackListByNo(int iBlackNo)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE black_no=@black_no";
            
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@black_no", iBlackNo);
                
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            if (sdr.Read())
            {
                CBlackList mBlack = GetCBlackListBySQL(sdr);
                if (conn != null) conn.Close();
                return mBlack;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }

        }

        public List<CBlackList> GetBlackList(List<int> employeeIds, int servNo, string playerName)
        {
            
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
            string sql = "SELECT * FROM " + table_name ;
            sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + ".black_emp_fid ";
            sql += " WHERE black_emp_fid > 0 ";

            if (employeeIds != null && sql_inarray != "")
                sql += " AND black_emp_fid IN " + sql_inarray;
            if (servNo > 0)
            {
                sql += " AND black_server_no=@black_server_no";                
            }
            else if(servNo < 0)
            {
                
               int iStartNo = -servNo - 10;
               int iEndNo = -servNo + 1;

               sql += " AND black_server_no > '" + iStartNo.ToString() + "' ";
               sql += " AND black_server_no < '" + iEndNo.ToString() + "' ";                    
                
            }
            if (playerName.Length > 0)
                sql += " AND black_player_name LIKE @black_player_name + '%' ";
            
            sql += " ORDER BY black_server_no";   //mb_uid";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@black_server_no", servNo-10);
                cmd.Parameters.AddWithValue("@black_player_name", playerName);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CBlackList> blackList = new List<CBlackList>();
            while (this.sdr.Read())
            {
                CBlackList mBlack = GetCBlackListBySQL(sdr);
                blackList.Add(mBlack);
            }
            if (conn != null) conn.Close();
            return blackList;
        }


    }



}
