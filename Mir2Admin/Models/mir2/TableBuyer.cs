using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Mir2Admin.Models
{
    public class CTblBuyer
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader sdr;
        private string table_name = "tbl1_lm2_buyerlist";

        public CTblBuyer()
        {
        }
        protected CBuyer GetBuyerBySQL(SqlDataReader reader)
        {
            CBuyer mBuyer       = new CBuyer();
            mBuyer.emp_fid      = (int)sdr["emp_fid"];
            mBuyer.server_no    = (int)sdr["server_no"];
            mBuyer.banker_id    = sdr["banker_id"].ToString();
            mBuyer.buyer_id     = sdr["buyer_id"].ToString();
            mBuyer.item_id      = sdr["item_id"].ToString();
            mBuyer.sale_id      = sdr["sale_id"].ToString();
            mBuyer.item_length = (int)sdr["item_length"];
            mBuyer.reg_date     = sdr["reg_date"].ToString();
            mBuyer.reg_state    = (int)sdr["reg_state"];
            mBuyer.buy_state    = (int)sdr["buy_state"];
            return mBuyer;
        }

        public Boolean AddTblBuyer(CBuyer mBuyer)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "INSERT INTO " + table_name + " (";
            sql += " emp_fid, server_no, banker_id, buyer_id, item_id, sale_id, item_length, reg_date, reg_state, buy_state ";
            sql += ") values (";
            sql += " @emp_fid, @server_no, @banker_id, @buyer_id, @item_id, @sale_id, @item_length, @reg_date, @reg_state, @buy_state ";
            sql += ")";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mBuyer.emp_fid);
                cmd.Parameters.AddWithValue("@server_no", mBuyer.server_no);
                cmd.Parameters.AddWithValue("@banker_id", mBuyer.banker_id);
                cmd.Parameters.AddWithValue("@buyer_id", mBuyer.buyer_id);
                cmd.Parameters.AddWithValue("@item_id", mBuyer.item_id);
                cmd.Parameters.AddWithValue("@sale_id", mBuyer.sale_id);
                cmd.Parameters.AddWithValue("@item_length", mBuyer.item_length);
                cmd.Parameters.AddWithValue("@reg_date", mBuyer.reg_date);
                cmd.Parameters.AddWithValue("@reg_state", mBuyer.reg_state);
                cmd.Parameters.AddWithValue("@buy_state", mBuyer.buy_state);



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

        public Boolean UpdateTblBuyer(CBuyer mBuyer)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();

            string sql = "UPDATE " + table_name + " SET ";
            sql += "emp_fid=@emp_fid, ";
            sql += "banker_id=@banker_id, ";
            sql += "item_id=@item_id, ";
            sql += "sale_id=@sale_id, ";
            sql += "item_length=@item_length, ";
            sql += "reg_date=@reg_date, ";
            sql += "reg_state=@reg_state, ";
            sql += "buy_state=@buy_state ";
            sql += "WHERE server_no=@server_no AND ";
            sql += "buyer_id=@buyer_id ";

            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@emp_fid", mBuyer.emp_fid);
                cmd.Parameters.AddWithValue("@server_no", mBuyer.server_no);
                cmd.Parameters.AddWithValue("@banker_id", mBuyer.banker_id);
                cmd.Parameters.AddWithValue("@buyer_id", mBuyer.buyer_id);
                cmd.Parameters.AddWithValue("@item_id", mBuyer.item_id);
                cmd.Parameters.AddWithValue("@sale_id", mBuyer.sale_id);
                cmd.Parameters.AddWithValue("@item_length", mBuyer.item_length);
                cmd.Parameters.AddWithValue("@reg_date", mBuyer.reg_date);
                cmd.Parameters.AddWithValue("@reg_state", mBuyer.reg_state);
                cmd.Parameters.AddWithValue("@buy_state", mBuyer.buy_state);

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


        public CBuyer GetTblBuyerByBuyer(int server_no, string buyer_id)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " WHERE server_no=@server_no AND buyer_id=@buyer_id ";
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@server_no", server_no);
                cmd.Parameters.AddWithValue("@buyer_id", buyer_id);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
                return null;
            }

            if (sdr.Read())
            {
                CBuyer Buyer = GetBuyerBySQL(sdr);
                if (conn != null) conn.Close();
                return Buyer;
            }
            else
            {
                if (conn != null) conn.Close();
                return null;
            }
        }

        public List<CBuyer> GetTblAllBuyerByBanker(int server_no, string banker_id)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " JOIN tbl1_lm2_session ON " +
            table_name + ".buyer_id = tbl1_lm2_session.sess_game_name ";
            sql += " AND "+ table_name + ".server_no = tbl1_lm2_session.sess_game_serverno ";

            sql += " WHERE server_no = @server_no  ";
            sql += " AND banker_id = @banker_id ";

            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.Parameters.AddWithValue("@server_no", server_no);
                cmd.Parameters.AddWithValue("@banker_id", banker_id);
                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CBuyer> buyerList = new List<CBuyer>();
            while (this.sdr.Read())
            {
                CBuyer mBuyer = GetBuyerBySQL(sdr);
                buyerList.Add(mBuyer);
            }
            if (conn != null) conn.Close();
            return buyerList;
        }

        public List<CBuyer> GetTblAllBuyerByServer(int emp_fid, int server_no)
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " JOIN tbl1_lm2_session ON " +
            table_name + ".buyer_id = tbl1_lm2_session.sess_game_name ";
            sql += " AND " + table_name + ".server_no = tbl1_lm2_session.sess_game_serverno ";

            sql += " WHERE server_no = @server_no  ";
            sql += " AND emp_fid = @emp_fid ";

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
            }

            List<CBuyer> buyerList = new List<CBuyer>();
            while (this.sdr.Read())
            {
                CBuyer mBuyer = GetBuyerBySQL(sdr);
                buyerList.Add(mBuyer);
            }
            if (conn != null) conn.Close();
            return buyerList;
        }
        public List<CBuyer> GetTblAllBuyer()
        {
            conn = CutilsMsSql.GetSqlConn();
            conn.Open();
            string sql = "SELECT * FROM " + table_name;
            sql += " ORDER BY server_no";

            cmd = new SqlCommand(sql, conn);

            try
            {

                sdr = cmd.ExecuteReader();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string error_msg = ex.Message;
            }

            List<CBuyer> buyerList = new List<CBuyer>();
            while (this.sdr.Read())
            {
                CBuyer mBuyer = GetBuyerBySQL(sdr);
                buyerList.Add(mBuyer);
            }
            if (conn != null) conn.Close();
            return buyerList;
        }
        public Boolean DeleteAllTblBuyer(int emp_fid, int server_no)
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
