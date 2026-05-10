using System;
using System.Collections.Generic;
using System.Web;

namespace Mir2Admin.Models
{
    public class CLm2Session
    {
        TblLm2Session tblSession = new TblLm2Session();

        public string sess_id;
        public DateTime sess_time_begin;
        public DateTime sess_time_last;
        public string sess_hostname;
        public string sess_browser;
        public string sess_pub_addr;
        public string sess_mb_uid;
        public int sess_emp_fid;
        public int sess_auto_running;
        public string sess_game_server;
        public int sess_game_serverno;
        public string sess_game_name;
        public int sess_game_level;
        public string sess_game_locale;

        public int sess_game_money1;
        public int sess_game_money2;
        public int sess_game_money3;
        public int sess_game_money4;
        public int sess_game_money5;

        public string sess_app_version;
        public string sess_app_title;

        public int sess_client_command;
        public string sess_client_memo;

        public int mb_fid;
        public string mb_nickname;
        public int mb_level;

        public string mb_backcolor;

        public CLm2Session()
        {
            sess_id = "";
            sess_time_begin = DateTime.Now;
            sess_time_last = DateTime.Now;
            sess_hostname = "";
            sess_browser = "";
            sess_pub_addr = "";
            sess_mb_uid = "";
            sess_emp_fid = 0;
            sess_auto_running = 0;
            sess_game_server = "";
            sess_game_serverno = 0;
            sess_game_name = "";
            sess_game_level = 0;
            sess_game_locale = "";

            sess_game_money1 = 0;
            sess_game_money2 = 0;
            sess_game_money3 = 0;
            sess_game_money4 = 0;
            sess_game_money5 = 0;

            sess_app_version = "";
            sess_app_title = "";

            sess_client_command = 0;
            sess_client_memo = "";
            
    }
        public static void ClearSession()
        {
            TblLm2Session tblSession = new TblLm2Session();
            tblSession.ClearTblSession();
        }
        public Boolean ResigterMemberSession()
        {
            return tblSession.RegisterTblSession(this);
        }
        public Boolean UpdateMemberSession()
        {
            return tblSession.UpdateTblSession(this);
        }
        public Boolean DeleteSession(string sess_id)
        {
            return tblSession.DeleteTblSession(sess_id);
        }
        public CLm2Session GetSessionBySessId(string sess_id)
        {
            return tblSession.GetTblSessionBySessId(sess_id);
        }
        public CLm2Session GetSessionByUId(string sess_mb_uid)
        {
            return tblSession.GetTblSessionByUId(sess_mb_uid);
        }
        public CLm2Session GetSessionByGame(int iServerNo, string strCharName)
        {
            return tblSession.GetTblSessionByGame(iServerNo, strCharName);
        }
        public List<CLm2Session> GetSessionListByServer(int iEmpFid, int iServerNo)
        {
            return tblSession.GetTblSessionByServer(iEmpFid, iServerNo);
        }
        public List<CLm2Session> GetSessionList(string servName, string characName)
        {
            return tblSession.GetTblSessionList(servName, characName);
        }

        public int CountSessionsByPubAddrPrefix(string threeOctetPrefixEndsWithDot)
        {
            return tblSession.CountTblSessionByPubAddrPrefix(threeOctetPrefixEndsWithDot);
        }

    }
}

