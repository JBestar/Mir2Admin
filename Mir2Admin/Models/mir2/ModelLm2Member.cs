using System;
using System.Collections.Generic;
using System.Web;
using System.Data;


namespace Mir2Admin.Models
{
    public class CLm2Member
    {
        CLm2Session mSession = new CLm2Session();
        TblLm2Member tblMember = new TblLm2Member();

        public int mb_fid;
        public int mb_emp_fid;
        public string mb_uid;
        public string mb_pwd;
        public string mb_nickname;
        public string mb_handphone;
        public int mb_level;
        public Boolean mb_vip;
        public string mb_domain;
        public string mb_username;
        public string mb_password;
        public string mb_local_key;
        public DateTime mb_time_join;
        public DateTime mb_time_last;
        public DateTime mb_time_limit;
        public string mb_last_ipaddr;
        public Boolean mb_state_active;
        public Boolean mb_state_delete;

        public string mb_bgcolor = "FFFFFF";
        public string label_company = "";
        public CLm2Member()
        {
        }
        public static CLm2Member CheckLogin(string sessionId)
        {
            CLm2Session.ClearSession();

            CLm2Session mSession = new CLm2Session();
            mSession = mSession.GetSessionBySessId(sessionId);
            if (mSession == null) return null;

            //mSession.sess_time_last = DateTime.Now;
            //mSession.UpdateMemberSession();

            CLm2Member curMember = new CLm2Member();
            curMember = curMember.GetMemberByUid(mSession.sess_mb_uid);
            return curMember;
        }
        public Boolean ResigterMember()
        {
            return tblMember.RegisterTblMember(this);
        }
        public Boolean UpdateMember()
        {
            return tblMember.UpdateTblMember(this);
        }

        public Boolean UpdateLastTimeMember()
        {
            return tblMember.UpdateLastTimeTblMember(this);
        }

        public Boolean DeleteMember(int mb_fid)
        {
            return tblMember.DeleteTblMember(mb_fid);
        }
        public CLm2Member GetMemberByFid(int mb_fid)
        {
            return tblMember.GetMemberByFid(mb_fid);
        }
        public CLm2Member GetMemberByUid(string mb_uid)
        {
            return tblMember.GetMemberByUid(mb_uid);
        }
        public CLm2Member GetMemberByNickName(string mb_nickname)
        {
            return tblMember.GetMemberByNickName(mb_nickname);
        }
        public List<CLm2Member> GetAllMemberList(List<int> employeeIds, string searchWords)
        {
            List<CLm2Member> memberList = tblMember.GetMemberList(employeeIds, searchWords);

            return memberList;
        }
        // 로그인액션
        public int ActionLogin(string mb_uid, string mb_pwd,
            string sess_id, string hostname, string browser, string ipaddress, string force = "")
        {
            // result=0: 아이디틀림
            // result=1: 정상
            // result=2: 비번틀림
            // result=3: 기간만기
            // result=4: 계정차단
            // result=5: 중복로그인
            if (mb_uid == "" || mb_pwd == "") return 0;

            CLm2Session.ClearSession();
            CLm2Member tmpMember = GetMemberByUid(mb_uid);
            if (tmpMember == null) return 0;
            if (mb_pwd == "@***@")
                mb_pwd = tmpMember.mb_pwd;

            if (tmpMember.mb_pwd != mb_pwd) return 2;
            if (tmpMember.mb_time_limit < DateTime.Now) return 3;
            if (tmpMember.mb_state_active == false) return 4;

            CEmployee mEmployee = new CEmployee();
            mEmployee = mEmployee.GetMemberByFid(tmpMember.mb_emp_fid);
            if (mEmployee == null) return 4;
            if (mEmployee.emp_state_active == false || mEmployee.emp_b_app_01_lm2 == false) return 4;

            CLm2Session sessionHelper = new CLm2Session();
            CLm2Session existingSession = sessionHelper.GetSessionByUIdAndPubAddr(mb_uid, ipaddress);
            bool sessionSaved = false;

            if (existingSession != null)
            {
                string oldSessId = existingSession.sess_id;
                existingSession.sess_time_last = DateTime.Now;
                existingSession.sess_hostname = hostname;
                existingSession.sess_browser = browser;
                existingSession.sess_emp_fid = tmpMember.mb_emp_fid;

                if (oldSessId != sess_id)
                {
                    existingSession.DeleteSession(oldSessId);
                    existingSession.sess_id = sess_id;
                    sessionSaved = existingSession.ResigterMemberSession();
                }
                else
                {
                    sessionSaved = existingSession.UpdateMemberSession();
                }

                if (sessionSaved)
                    sessionHelper.DeleteSessionsByUIdAndPubAddrExcept(mb_uid, ipaddress, sess_id);
            }
            else
            {
                mSession = new CLm2Session();
                mSession.sess_id = sess_id;
                mSession.sess_time_begin = DateTime.Now;
                mSession.sess_time_last = DateTime.Now;
                mSession.sess_hostname = hostname;
                mSession.sess_browser = browser;
                mSession.sess_pub_addr = ipaddress;
                mSession.sess_mb_uid = mb_uid;
                mSession.sess_emp_fid = tmpMember.mb_emp_fid;
                sessionSaved = mSession.ResigterMemberSession();
            }

            if (sessionSaved)
            {
                tmpMember.mb_time_last = DateTime.Now;
                tmpMember.UpdateLastTimeMember();
                return 1;
            }
            return 10;
        }
        // 로그아웃액션
        public void ActionLogOut(string sess_id)
        {
            mSession = new CLm2Session();
            mSession.DeleteSession(sess_id);
        }

    }
}
