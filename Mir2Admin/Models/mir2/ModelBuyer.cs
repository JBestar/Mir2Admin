using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CBuyer
    {
        CTblBuyer tblBuyer= new CTblBuyer();

        public int emp_fid;
        public int server_no;
        public string banker_id;
        public string buyer_id;
        public string item_id;
        public string sale_id;
        public int item_length;
        public string reg_date;
        public int reg_state;
        public int buy_state;

        public CBuyer()
        {
            emp_fid = 0;
            server_no = 0;
            banker_id = "";
            buyer_id = "";
            item_id = "";
            sale_id = "";
            item_length = 0;
            reg_date = "";
            reg_state = 0;
            buy_state = 0;
        }
        public Boolean AddBuyer()
        {
            
            return tblBuyer.AddTblBuyer(this);
        }
        
        public Boolean UpdateBuyer()
        {
            return tblBuyer.UpdateTblBuyer(this);
        }
        public CBuyer GetBuyerByBuyer(int server_no, string buyer_id)
        {
            return tblBuyer.GetTblBuyerByBuyer(server_no, buyer_id);
        }
        public List<CBuyer> GetAllBuyerByServer(int emp_fid, int server_no)
        {
            return tblBuyer.GetTblAllBuyerByServer(emp_fid, server_no);
        }
        public List<CBuyer> GetAllBuyerByBanker(int server_no, string banker_id)
        {
            return tblBuyer.GetTblAllBuyerByBanker(server_no, banker_id);
        }
        public List<CBuyer> GetAllBuyer()
        {
            return tblBuyer.GetTblAllBuyer();
        }
        public Boolean DeleteAllBuyer(int emp_fid, int server_no)
        {

            return tblBuyer.DeleteAllTblBuyer(emp_fid, server_no);
        }

    }
}
