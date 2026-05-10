using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class ActiveKeep : System.Web.UI.Page
    {

        public string strKeepResult = "";
        public CLm2Member curMember = new CLm2Member();
        public CLm2Session curSession = new CLm2Session();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strSessId = "";
            if (Request["websession"] == null)
            {
                //if (Session["sessId"] == null)
                    Session["sessId"] = Session.SessionID;
                strSessId = Session["sessId"].ToString();
                curMember = CLm2Member.CheckLogin(strSessId);
            }
            else
            {
                strSessId = Request["websession"].ToString();
                curMember = CLm2Member.CheckLogin(strSessId);
            }

            if (curMember == null)
            {
                Session["sessId"] = Session.SessionID;
                strSessId = Session["sessId"].ToString();
                int iLoginResult = -1;

                if(Request["username"] != null)
                {
                    curMember = new CLm2Member();
                    iLoginResult = curMember.ActionLogin(
                    Request["username"].ToString(),
                    "@***@",
                    strSessId,
                    "",
                    "",
                    CUtilsNetwork.GetClientIpAddress(Request),
                    "1");

                }


                if(iLoginResult != 1)
                {
                    strKeepResult = "result=0; member=null;";
                    return;
                }
                else curMember = CLm2Member.CheckLogin(strSessId);

                if(curMember == null)
                {
                    strKeepResult = "result=0; member=null;";
                    return;
                }

            }

            Boolean bCheckPermision = CheckPermision(strSessId, curMember);
            if (bCheckPermision == false)
            {
                strKeepResult = "result=1; permision=false;";
                return;
            }
            else
            {
                curSession = curSession.GetSessionBySessId(strSessId);
                ProcessActiveKeepSuccess();
            }
        }
        protected void ProcessActiveKeepSuccess()
        {
            TimeSpan timespan = (curMember.mb_time_limit - DateTime.Now);
            int iSeconds = 86400 * timespan.Days + 3600 * timespan.Hours + 60 * timespan.Minutes + timespan.Seconds;
            strKeepResult = "result=1;remained=" + iSeconds.ToString() + ";command=" + curSession.sess_client_command.ToString() + ";";
            strKeepResult += "address="+ curSession.sess_pub_addr+";";
            //curSession.sess_client_command = 0;
            //curSession.UpdateMemberSession();
        }
        protected Boolean CheckPermision(string sessionId, CLm2Member tmpMember)
        {
            if (tmpMember.mb_time_limit < DateTime.Now) return false;   // 기간만기
            if (tmpMember.mb_state_active == false) return false;       // 회원차단

            CEmployee mEmployee = new CEmployee();
            mEmployee = mEmployee.GetMemberByFid(tmpMember.mb_emp_fid);
            if (mEmployee == null) return false;
            if (mEmployee.emp_state_active == false || mEmployee.emp_b_app_01_lm2 == false) return false;

            // 세션데이터를 갱신
            CLm2Session mSession = new CLm2Session();
            mSession = mSession.GetSessionBySessId(sessionId);
            if (mSession == null) return false;

            mSession.sess_time_last = DateTime.Now;
            if (Request["auto_running"] == null) mSession.sess_auto_running = 0;
            else mSession.sess_auto_running = int.Parse(Request["auto_running"].ToString());    //자동사냥 시작, 정지상태

            if (Request["game_server"] == null) mSession.sess_game_server = "";
            else mSession.sess_game_server = Request["game_server"].ToString();

            if (Request["game_serverno"] == null) mSession.sess_game_serverno = 0;
            else mSession.sess_game_serverno = int.Parse(Request["game_serverno"].ToString());


            if (Request["game_name"] == null) mSession.sess_game_name = "";
            else mSession.sess_game_name = Request["game_name"].ToString();
                        
            if (Request["game_level"] == null) mSession.sess_game_level = 0;
            else mSession.sess_game_level = int.Parse(Request["game_level"].ToString());
            
            if (Request["game_locale"] == null) mSession.sess_game_locale = "";
            else mSession.sess_game_locale = Request["game_locale"].ToString();

            if (Request["game_money1"] == null) mSession.sess_game_money1 = 0;
            else mSession.sess_game_money1 = (int)Int64.Parse(Request["game_money1"].ToString());

            if (Request["game_money2"] == null) mSession.sess_game_money2 = 0;
            else mSession.sess_game_money2 = (int)Int64.Parse(Request["game_money2"].ToString());
                        
            if (Request["app_title"] == null) mSession.sess_app_title = "";
            else mSession.sess_app_title = Request["app_title"].ToString();

            if (Request["app_version"] == null) mSession.sess_app_version = "";
            else mSession.sess_app_version = Request["app_version"].ToString();
                        
            if (Request["client_memo"] == null) mSession.sess_client_memo = "";
            else mSession.sess_client_memo = Request["client_memo"].ToString();

            return mSession.UpdateMemberSession();
        }

    }
}