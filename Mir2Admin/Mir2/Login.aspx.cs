using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class Login : System.Web.UI.Page
    {

        public string strLoginResult = "";
        public CLm2Member curMember = new CLm2Member();
        string loginRecordedIp = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int iLoginResult = 10;
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;

            if (Request["username"] != null && Request["password"] != null)
            {
                string strForce = "";
                if (Request["force"] != null)
                    strForce = Request["force"].ToString();

                loginRecordedIp = CUtilsNetwork.GetClientIpAddress(Request);
                string strConipForStore = Request["conip"] != null ? Request["conip"].ToString() : "";
                string unusedPrefix;
                if (CUtilsNetwork.TryGetIpv4ThreeOctetPrefix(strConipForStore, out unusedPrefix))
                    loginRecordedIp = strConipForStore.Trim();

                iLoginResult = curMember.ActionLogin(
                    Request["username"].ToString(),
                    Request["password"].ToString(),
                    Session["sessId"].ToString(),
                    "",
                    "",
                    loginRecordedIp,
                    strForce);
            }
            
            if (iLoginResult == 1)
            {
                curMember = curMember.GetMemberByUid(Request["username"].ToString());//CheckLogin(Session["sessId"].ToString());
                ProcessLoginSuccess();
            }
                
            else
                strLoginResult = "result=" + iLoginResult + ";";
        }
        protected void ProcessLoginSuccess()
        {
            string tIpAddr = loginRecordedIp.Length > 0 ? loginRecordedIp : CUtilsNetwork.GetClientIpAddress(Request);
            TimeSpan timespan = (curMember.mb_time_limit - DateTime.Now);
            int iSeconds = 86400 * timespan.Days + 3600 * timespan.Hours + 60 * timespan.Minutes + timespan.Seconds;

            int iConipSamePrefixCount = 0;
            string strConip = Request["conip"] != null ? Request["conip"].ToString() : "";
            string tPrefix;
            if (CUtilsNetwork.TryGetIpv4ThreeOctetPrefix(strConip, out tPrefix))
            {
                CLm2Session cntSession = new CLm2Session();
                iConipSamePrefixCount = cntSession.CountSessionsByPubAddrPrefix(tPrefix);
            }

            strLoginResult = "result=1;remained=" + iSeconds.ToString() + ";address="+ tIpAddr+";";
            strLoginResult += "conip_cnt=" + iConipSamePrefixCount.ToString() + ";";
            if (curMember.mb_vip) strLoginResult += "isvip;";
        }
    }
}