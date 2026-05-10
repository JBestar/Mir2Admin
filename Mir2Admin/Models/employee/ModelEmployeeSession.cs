using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mir2Admin.Models
{
    public class CEmployeeSession
    {
        CTblCompSession tblSession = new CTblCompSession();

        public string sess_id;
        public DateTime sess_time_begin;
        public DateTime sess_time_last;
        public string sess_hostname;
        public string sess_browser;
        public string sess_ipaddress;
        public string sess_emp_uid;
        public int sess_emp_level;

        public CEmployeeSession()
        {
        }
        public static void ClearSession()
        {
            CTblCompSession tblSession = new CTblCompSession();
            tblSession.ClearTblSession();
        }
        public Boolean ResigterSession()
        {
            return tblSession.RegisterTblSession(this);
        }
        public Boolean UpdateSession()
        {
            return tblSession.UpdateTblSession(this);
        }
        public Boolean DeleteSession(string sess_id)
        {
            return tblSession.DeleteTblSession(sess_id);
        }
        public CEmployeeSession GetSessionBySessId(string sess_id)
        {
            return tblSession.GetSessionBySessId(sess_id);
        }
        public CEmployeeSession GetSessionByEmpUId(string emp_mb_uid)
        {
            return tblSession.GetSessionByEmpUId(emp_mb_uid);
        }
        public List<CEmployeeSession> GetEmployeeSessionList()
        {
            return tblSession.GetEmployeeSessionList();
        }
    }
}