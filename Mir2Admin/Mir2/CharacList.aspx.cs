using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class CharacList : System.Web.UI.Page
    {

        //public string strKeepResult = "";
        public CLm2Member curMember = new CLm2Member();
        public CLm2Session curSession = new CLm2Session();
        CEmployee curEmployee = new CEmployee();

        string m_keyResult = "result";              //Json 키 
        string m_keyLists = "lists";                //Json 키 
        string m_keyServerNo = "serverno";          //Json 키 
        string m_keyName = "name";                  //Json 키 
        string m_keyEnable = "enable";              //Json 키 

        string m_resultValue = "0";

        string m_requestType_Add = "add";
        string m_requestType_Del = "delete";
        string m_requestType_All = "all";
        string m_requestType_Enable = "enable";
        string m_requestType_Disable = "disable";
        

        Hashtable mJsonResponse = new Hashtable();   //Json 응답

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["websession"] == null)
            {
                if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
                curMember = CLm2Member.CheckLogin(Session["sessId"].ToString());
            }
            else
            {
                curMember = CLm2Member.CheckLogin(Request["websession"].ToString());
                Session["sessId"] = Request["websession"].ToString();
            }

            mJsonResponse = new Hashtable();

            if (curMember == null)
            {
                m_resultValue = "0";
            }

            else
            {
                Boolean bResult = RequestProcess(Request["websession"].ToString(), curMember);
                if (bResult == false)
                {
                    m_resultValue = "0";
                }
                else
                {
                    m_resultValue = "1";

//                     curSession = curSession.GetSessionBySessId(Session["sessId"].ToString());
//                     UpdateMemberSession();

                }
            }

            mJsonResponse.Add(m_keyResult, m_resultValue);             // 요청결과 

            //////////Json 결과 반환
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string jsonResponseAsString = jsSerializer.Serialize(mJsonResponse);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(jsonResponseAsString);
            Response.End();


        }
        protected void UpdateMemberSession()
        {
            curSession.sess_client_command = 0;
            curSession.UpdateMemberSession();
        }
        protected Boolean RequestProcess(string sessionId, CLm2Member tmpMember)
        {
            if (tmpMember.mb_time_limit < DateTime.Now) return false;   // 기간만기
            if (tmpMember.mb_state_active == false) return false;       // 회원차단


            curEmployee = curEmployee.GetMemberByFid(tmpMember.mb_emp_fid);
            if (curEmployee == null) return false;
            if (curEmployee.emp_state_active == false || curEmployee.emp_b_app_01_lm2 == false) return false;

            Boolean bResult = false;

            // 세션데이터를 갱신
            CLm2Session mSession = new CLm2Session();
            mSession = mSession.GetSessionBySessId(sessionId);
            if (mSession == null) return bResult;

            if (Request["charac_type"] != null)
            {
                string requestType = Request["charac_type"].ToString();
                if (requestType.Equals(m_requestType_Add))                   //캐랙리스트 추가
                {
                    bResult = AddCharacList();
                }
                else if (requestType.Equals(m_requestType_Del))           //캐랙리스트 삭제
                {
                    bResult = DeleteCharacList();
                }
                else if (requestType.Equals(m_requestType_All))           //캐랙리스트 목록얻기
                {
                    bResult = GetAllCharacList();
                }
                else if (requestType.Equals(m_requestType_Enable))           //캐랙리스트 대상활성
                {
                    bResult = SetEnableCharacList(1);
                }
                else if (requestType.Equals(m_requestType_Disable))           //캐랙리스트 대상비활성
                {
                    bResult = SetEnableCharacList(0);
                }

            }

            return bResult;

        }

        protected Boolean AddCharacList()
        {
            CCharacList mCharacList = new CCharacList();

            if (Request["user_id"] == null) mCharacList.charac_mb_uid = "";
            else mCharacList.charac_mb_uid = Request["user_id"].ToString();

            if (Request["server_no"] == null) mCharacList.charac_server_no = 0;
            else mCharacList.charac_server_no = int.Parse(Request["server_no"].ToString());

            if (Request["player_name"] == null) mCharacList.charac_player_name = "";
            else mCharacList.charac_player_name = Request["player_name"].ToString();

            mCharacList.charac_emp_fid = curEmployee.emp_fid;
            mCharacList.charac_player_enable = 1;

            return mCharacList.AddCharacList();

        }

        protected Boolean DeleteCharacList()
        {
            CCharacList mCharacList = new CCharacList();

            if (Request["user_id"] == null) mCharacList.charac_mb_uid = "";
            else mCharacList.charac_mb_uid = Request["user_id"].ToString();

            if (Request["server_no"] == null) mCharacList.charac_server_no = 0;
            else mCharacList.charac_server_no = int.Parse(Request["server_no"].ToString());

            if (Request["player_name"] == null) mCharacList.charac_player_name = "";
            else mCharacList.charac_player_name = Request["player_name"].ToString();

            mCharacList.charac_emp_fid = curEmployee.emp_fid;

            return mCharacList.DeleteCharacList();

        }

        protected Boolean SetEnableCharacList(int iEnable)
        {
            CCharacList mCharacList = new CCharacList();

            if (Request["user_id"] == null) mCharacList.charac_mb_uid = "";
            else mCharacList.charac_mb_uid = Request["user_id"].ToString();

            if (Request["server_no"] == null) mCharacList.charac_server_no = 0;
            else mCharacList.charac_server_no = int.Parse(Request["server_no"].ToString());

            if (Request["player_name"] == null) mCharacList.charac_player_name = "";
            else mCharacList.charac_player_name = Request["player_name"].ToString();

            mCharacList.charac_emp_fid = curEmployee.emp_fid;
            mCharacList.charac_player_enable = iEnable;


            return mCharacList.UpdateCharacList();

        }

        protected Boolean GetAllCharacList()
        {
            CCharacList mCharacList = new CCharacList();

            if (Request["user_id"] == null) mCharacList.charac_mb_uid = "";
            else mCharacList.charac_mb_uid = Request["user_id"].ToString();

            if (Request["server_no"] == null) mCharacList.charac_server_no = 0;
            else mCharacList.charac_server_no = int.Parse(Request["server_no"].ToString());

            mCharacList.charac_emp_fid = curEmployee.emp_fid;

            List<CCharacList> mArrayCharac = mCharacList.GetAllCharacList();

            int iServerNo = 0;
            string strPlayerName = "";
            int iEnable = 0;

            Hashtable hashItem = null;
            if (mArrayCharac.Count > 0)
            {
                List<Hashtable> mHashCharacList = new List<Hashtable>();

                for (int i = 0; i < mArrayCharac.Count; i++)
                {

                    iServerNo = mArrayCharac.ElementAt(i).charac_server_no;
                    strPlayerName = mArrayCharac.ElementAt(i).charac_player_name;
                    iEnable = mArrayCharac.ElementAt(i).charac_player_enable;

                    hashItem = new Hashtable();
                    hashItem.Add(m_keyServerNo, iServerNo.ToString());
                    hashItem.Add(m_keyName, strPlayerName);
                    hashItem.Add(m_keyEnable, iEnable.ToString());

                    mHashCharacList.Add(hashItem);
                }

                mJsonResponse.Add(m_keyLists, mHashCharacList);       //파일리스트추가
            }
            return true;
        }


    }
}