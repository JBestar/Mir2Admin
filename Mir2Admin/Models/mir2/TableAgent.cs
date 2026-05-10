using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblAgent
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2_agentlist";

        public CTblAgent()
        {
        }
        protected CAgent GetAgentBySQL(SqlDataReader reader)
        {
            CAgent mAgent       = new CAgent();
            mAgent.emp_fid      = (int)sdr["emp_fid"];
            mAgent.server_no    = (int)sdr["server_no"];
            mAgent.agent_id     = sdr["agent_id"].ToString();
            mAgent.active_state = (int)sdr["active_state"];
            return mAgent;
        }

        public Boolean AddTblAgent(CAgent mAgent)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + " (";
            sql += " emp_fid, server_no, agent_id, active_state ";
            sql += ") values (";
            sql += " @emp_fid, @server_no, @agent_id, @active_state ";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mAgent.emp_fid);
                cmd.Parameters.AddWithValue("@server_no", mAgent.server_no);
                cmd.Parameters.AddWithValue("@agent_id", mAgent.agent_id);
                cmd.Parameters.AddWithValue("@active_state", mAgent.active_state);

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

        public Boolean UpdateTblAgent(CAgent mAgent)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "agent_id=@agent_id, ";
            sql += "active_state=@active_state ";
            sql += "WHERE emp_fid=@emp_fid AND ";
            sql += "server_no=@server_no ";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mAgent.emp_fid);
                cmd.Parameters.AddWithValue("@server_no", mAgent.server_no);
                cmd.Parameters.AddWithValue("@agent_id", mAgent.agent_id);
                cmd.Parameters.AddWithValue("@active_state", mAgent.active_state);

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


        public CAgent GetTblAgentByServer(int emp_fid, int server_no)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE emp_fid=@emp_fid AND server_no=@server_no ";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", emp_fid);
                cmd.Parameters.AddWithValue("@server_no", server_no);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CAgent agent = GetAgentBySQL(sdr);
                if (conn != null) conn.Close();
                return agent;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }

        public List<CAgent> GetTblAllAgent()
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " ORDER BY server_no ";
            cmd = new SqlCommand(sql, conn);

            try
            {
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                
            }
            List<CAgent> agentList = new List<CAgent>();
            while (this.sdr.Read())
            {
                CAgent agent = GetAgentBySQL(sdr);
                agentList.Add(agent);
            }
            if (conn != null) conn.Close();
            return agentList;
        }
        public Boolean DeleteTblAgent(int emp_fid, int server_no)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "DELETE FROM " + table_name;
            sql += " WHERE emp_fid=@emp_fid AND server_no=@server_no";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", emp_fid);
                cmd.Parameters.AddWithValue("@server_no", server_no);
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




    }
}
