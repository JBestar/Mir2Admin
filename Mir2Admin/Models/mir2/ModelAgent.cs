using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CAgent
    {
        CTblAgent tblAgent= new CTblAgent();

        public int emp_fid;
        public int server_no;
        public string agent_id;
        public int active_state;

        public CAgent()
        {
            emp_fid = 0;
            server_no = 0;
            agent_id = "";
            active_state = 0;

        }
        public Boolean AddAgent()
        {
            return tblAgent.AddTblAgent(this);
        }
        
        public Boolean UpdateAgent()
        {
            return tblAgent.UpdateTblAgent(this);
        }
        public CAgent GetAgentByServer(int emp_fid, int server_no)
        {
            return tblAgent.GetTblAgentByServer(emp_fid, server_no);
        }
        public List<CAgent> GetAllAgent()
        {
            return tblAgent.GetTblAllAgent();
        }
        public Boolean DeleteAgent(int emp_fid, int server_no)
        {
            return tblAgent.DeleteTblAgent(emp_fid, server_no);
        }
        

    }
}
