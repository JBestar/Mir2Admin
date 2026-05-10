using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CBlackList
    {
        CTblBlackList tblBlackList = new CTblBlackList();

        public int black_no;
        public int black_emp_fid;
        public string black_mb_uid;
        public int black_server_no;
        public string black_player_name;
        public DateTime black_update_date;
        public int black_player_enable;
        public string black_player_content;

        public CBlackList()
        {
            black_no = 0;
            black_mb_uid = "";
            black_emp_fid = 0;
            black_server_no = 0;
            black_player_name = "";
            black_update_date = DateTime.Now;
            black_player_enable = 0;
            black_player_content = "";
        }

        public Boolean AddBlackList()
        {
            if (FindBlackList())
                return true;
            return tblBlackList.AddTblBlackList(this);
        }

        public Boolean DeleteBlackList()
        {
            if (FindBlackList())
                return tblBlackList.DeleteTblBlackList(this);
            return true;
        }

        public Boolean DeleteBlackListByNo(int iBlackNo)
        {
            CBlackList mBlack = FindBlackByNo(iBlackNo);
            
            if (mBlack != null)
                return tblBlackList.DeleteTblBlackList(mBlack);
            return false;
        }

        public Boolean UpdateBlackList()
        {
            if (FindBlackList())
                return tblBlackList.UpdateTblBlackList(this);
            return false;
        }

        public Boolean UpdateBlackListByNo(int iBlackNo, int bActive)
        {
            CBlackList mBlack = FindBlackByNo(iBlackNo);
            mBlack.black_player_enable = bActive;

            if (mBlack != null)
                return tblBlackList.UpdateTblBlackList(mBlack);
            return false;
        }

        public CBlackList FindBlackByNo(int iBlackNo)
        {
            return tblBlackList.GetBlackListByNo(iBlackNo);            
        }

        public Boolean FindBlackList()
        {
            List<CBlackList> mBlackList =  tblBlackList.GetAllBlackList(this);
            if (mBlackList.Count > 0)
                return true;
            return false;
        }

        public List<CBlackList> GetAllBlackList()
        {
            return tblBlackList.GetAllBlackList(this);
        }

        public List<CBlackList> GetBlackList(List<int> employeeIds, int servNo, string playerName)
        {
            return tblBlackList.GetBlackList(employeeIds, servNo, playerName);
        }        

    }
}
