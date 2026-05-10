using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblCharacList
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2_characlist";

        public CTblCharacList()
        {
        }
        protected CCharacList GetCharacListBySQL(SqlDataReader reader)
        {
            CCharacList mCharacList         = new CCharacList();
            mCharacList.charac_no = (int)sdr["charac_no"];
            mCharacList.charac_mb_uid = sdr["charac_mb_uid"].ToString();
            mCharacList.charac_emp_fid = (int)sdr["charac_emp_fid"];
            mCharacList.charac_server_no = (int)sdr["charac_server_no"];
            mCharacList.charac_player_name = sdr["charac_player_name"].ToString();
            mCharacList.charac_update_date = (DateTime)sdr["charac_update_date"];
            mCharacList.charac_player_enable = (int)sdr["charac_player_enable"];
            mCharacList.charac_player_content = sdr["charac_player_content"].ToString() ;

            return mCharacList;
        }

        public Boolean AddTblCharacList(CCharacList mCharacList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + " (";
            sql += " charac_emp_fid, charac_mb_uid, charac_server_no, charac_player_name, charac_update_date, charac_player_enable, charac_player_content ";
            sql += ") values (";
            sql += " @charac_emp_fid, @charac_mb_uid, @charac_server_no, @charac_player_name, @charac_update_date, @charac_player_enable, @charac_player_content ";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@charac_mb_uid", mCharacList.charac_mb_uid);
                cmd.Parameters.AddWithValue("@charac_emp_fid", mCharacList.charac_emp_fid);
                cmd.Parameters.AddWithValue("@charac_server_no", mCharacList.charac_server_no);
                cmd.Parameters.AddWithValue("@charac_player_name", mCharacList.charac_player_name);
                cmd.Parameters.AddWithValue("@charac_update_date", mCharacList.charac_update_date);
                cmd.Parameters.AddWithValue("@charac_player_enable", mCharacList.charac_player_enable);
                cmd.Parameters.AddWithValue("@charac_player_content", mCharacList.charac_player_content);

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

        public Boolean DeleteTblCharacList(CCharacList mCharacList)
        {

            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE FROM " + table_name;
            sql += " WHERE charac_emp_fid=@charac_emp_fid";

            if (mCharacList.charac_server_no > 0)
                sql += " AND charac_server_no=@charac_server_no";

            if (mCharacList.charac_player_name.Length > 0)
                sql += " AND charac_player_name=@charac_player_name";
            
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@charac_emp_fid", mCharacList.charac_emp_fid);
                cmd.Parameters.AddWithValue("@charac_server_no", mCharacList.charac_server_no);
                cmd.Parameters.AddWithValue("@charac_player_name", mCharacList.charac_player_name);

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


        public Boolean UpdateTblCharacList(CCharacList mCharacList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += " charac_update_date=@charac_update_date, ";
            sql += " charac_player_enable=@charac_player_enable ";
            sql += " WHERE charac_emp_fid=@charac_emp_fid ";
            sql += " AND charac_server_no=@charac_server_no ";
            sql += " AND charac_player_name=@charac_player_name";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@charac_emp_fid", mCharacList.charac_emp_fid);
                cmd.Parameters.AddWithValue("@charac_server_no", mCharacList.charac_server_no);
                cmd.Parameters.AddWithValue("@charac_player_name", mCharacList.charac_player_name);
                cmd.Parameters.AddWithValue("@charac_update_date", mCharacList.charac_update_date);
                cmd.Parameters.AddWithValue("@charac_player_enable", mCharacList.charac_player_enable);
                
                cmd.ExecuteNonQuery();
            } catch (System.Data.SqlClient.SqlException ex)
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


        public List<CCharacList> GetAllCharacList(CCharacList mCharacList)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE charac_emp_fid=@charac_emp_fid";
            if (mCharacList.charac_server_no > 0)
                sql += " AND charac_server_no=@charac_server_no";
            if (mCharacList.charac_player_name.Length > 0)
                sql += " AND charac_player_name=@charac_player_name";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@charac_emp_fid", mCharacList.charac_emp_fid);
                cmd.Parameters.AddWithValue("@charac_server_no", mCharacList.charac_server_no);
                cmd.Parameters.AddWithValue("@charac_player_name", mCharacList.charac_player_name);

                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CCharacList> characList = new List<CCharacList>();
            while (this.sdr.Read())
            {
                CCharacList mCharac = GetCharacListBySQL(sdr);
                characList.Add(mCharac);
            }
            if (conn != null) conn.Close();
            return characList;
        }
    


    public CCharacList GetCharacListByNo(int iCharacNo)
    {
        conn = CutilsMsSql.GetSqlConn();
        conn.Open();
        string sql = "SELECT * FROM " + table_name;
        sql += " WHERE charac_no=@charac_no";

        cmd = new SqlCommand(sql, conn);

        try
        {
            cmd.Parameters.AddWithValue("@charac_no", iCharacNo);

            sdr = cmd.ExecuteReader();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string error_msg = ex.Message;
        }

        if (sdr.Read())
        {
            CCharacList mCharac = GetCharacListBySQL(sdr);
            if (conn != null) conn.Close();
            return mCharac;
        }
        else
        {
            if (conn != null) conn.Close();
            return null;
        }

    }

    public List<CCharacList> GetCharacList(List<int> employeeIds, int servNo, string playerName)
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
        string sql = "SELECT * FROM " + table_name;
        sql += " JOIN tbl0_employee ON tbl0_employee.emp_fid=" + table_name + ".charac_emp_fid ";
        sql += " WHERE charac_emp_fid > 0 ";

        if (employeeIds != null && sql_inarray != "")
            sql += " AND charac_emp_fid IN " + sql_inarray;

       if (servNo > 0)
       {
           sql += " AND charac_server_no=@charac_server_no";
       }
       else if (servNo < 0)
       {
           int iStartNo = -servNo - 10;
           int iEndNo = -servNo + 1;

           sql += " AND charac_server_no > '" + iStartNo.ToString() + "' ";
           sql += " AND charac_server_no < '" + iEndNo.ToString() + "' ";
       }
       if (playerName.Length > 0)
            sql += " AND charac_player_name LIKE @charac_player_name + '%' ";

        sql += " ORDER BY charac_server_no";   //mb_uid";

        cmd = new SqlCommand(sql, conn);
        try
        {
            cmd.Parameters.AddWithValue("@charac_server_no", servNo - 10);
            cmd.Parameters.AddWithValue("@charac_player_name", playerName);
            sdr = cmd.ExecuteReader();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string error_msg = ex.Message;
        }

        List<CCharacList> characList = new List<CCharacList>();
        while (this.sdr.Read())
        {
            CCharacList mCharac = GetCharacListBySQL(sdr);
            characList.Add(mCharac);
        }
        if (conn != null) conn.Close();
        return characList;
    }


    }
}
