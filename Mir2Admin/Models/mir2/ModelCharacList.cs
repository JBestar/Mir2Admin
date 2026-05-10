using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CCharacList
    {
        CTblCharacList tblCharacList = new CTblCharacList();

        public int charac_no;
        public int charac_emp_fid;
        public string charac_mb_uid;
        public int charac_server_no;
        public string charac_player_name;
        public DateTime charac_update_date;
        public int charac_player_enable;
        public string charac_player_content;

        public CCharacList()
        {
            charac_no = 0;            
            charac_mb_uid = "";
            charac_emp_fid = 0;
            charac_server_no = 0;
            charac_player_name = "";
            charac_update_date = DateTime.Now;
            charac_player_enable = 0;
            charac_player_content = "";
        }

        public Boolean AddCharacList()
        {
            if (FindCharacList())
                return true;
            return tblCharacList.AddTblCharacList(this);
        }

        public Boolean DeleteCharacList()
        {
            if (FindCharacList())
                return tblCharacList.DeleteTblCharacList(this);
            return true;
        }

        public Boolean DeleteCharacListByNo(int iCharacNo)
        {
            CCharacList mCharac = FindCharacByNo(iCharacNo);

            if (mCharac != null)
                return tblCharacList.DeleteTblCharacList(mCharac);
            return false;
        }

        public Boolean UpdateCharacList()
        {
            if (FindCharacList())
                return tblCharacList.UpdateTblCharacList(this);
            return false;
        }

        public Boolean UpdateCharacListByNo(int iCharacNo, int bActive)
        {
            CCharacList mCharac = FindCharacByNo(iCharacNo);
            mCharac.charac_player_enable = bActive;

            if (mCharac != null)
                return tblCharacList.UpdateTblCharacList(mCharac);
            return false;
        }

        public CCharacList FindCharacByNo(int iCharacNo)
        {
            return tblCharacList.GetCharacListByNo(iCharacNo);
        }
        public Boolean FindCharacList()
        {
            List<CCharacList> mCharacList =  tblCharacList.GetAllCharacList(this);
            if (mCharacList.Count > 0)
                return true;
            return false;
        }

        public List<CCharacList> GetAllCharacList()
        {
            return tblCharacList.GetAllCharacList(this);
        }

        public List<CCharacList> GetCharacList(List<int> employeeIds, int servNo, string playerName)
        {
            return tblCharacList.GetCharacList(employeeIds, servNo, playerName);
        }

    }
}
