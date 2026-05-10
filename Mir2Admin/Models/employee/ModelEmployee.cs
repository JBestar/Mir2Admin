using System;
using System.Collections.Generic;
using System.Data;

namespace Mir2Admin.Models
{
    public class CEmployee
    {
        CEmployeeSession mEmployeeSession = new CEmployeeSession();
        CTblCompEmployee tblEmployee = new CTblCompEmployee();

        public int emp_fid;
        public string emp_uid;
        public string emp_pwd;
        public int emp_level;
        public int emp_company_fid;
        public string emp_nickname;
        public DateTime emp_time_join;
        public DateTime emp_time_last;
        public string emp_last_ipaddr;
        public string emp_color;
        public Boolean emp_state_active;
        public string emp_comment;
        public Boolean emp_b_app_01_lm2;

//         public Boolean emp_b_app_02_luckypal;


        public string label_company = "";

        public CEmployee()
        {
        }
        public static CEmployee CheckLogin(string sessionId)
        {
            CEmployeeSession mEmployeeSession = new CEmployeeSession();
            mEmployeeSession = mEmployeeSession.GetSessionBySessId(sessionId);
            if (mEmployeeSession == null) return null;

            mEmployeeSession.sess_time_last = DateTime.Now;
            mEmployeeSession.UpdateSession();

            CEmployee curEmployee = new CEmployee();
            curEmployee = curEmployee.GetMemberByUid(mEmployeeSession.sess_emp_uid);
            return curEmployee;
        }
        public Boolean ResigterEmployee()
        {
            return tblEmployee.RegisterTblEmployee(this);
        }
        public Boolean UpdateEmployee()
        {
            return tblEmployee.UpdateTblEmployee(this);
        }

        public Boolean UpdateLastTimeEmployee()
        {
            return tblEmployee.UpdateLastTimeTblEmployee(this);
        }

        public Boolean DeleteCompany(int emp_fid)
        {
            // 하부총판들을 찾아서 삭제한다.
            CEmployee mEmployee = new CEmployee();
            List<CEmployee> employeeList = mEmployee.GetEmployeeList();
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].emp_company_fid == emp_fid && employeeList[i].emp_level == 8) DeleteAgency(employeeList[i].emp_fid);
            }

            return tblEmployee.DeleteTblEmployee(emp_fid);
        }
        public Boolean DeleteAgency(int emp_fid)
        {
            // 하부매장들을 찾아서 삭제한다.
            CEmployee mEmployee = new CEmployee();
            List<CEmployee> employeeList = mEmployee.GetEmployeeList();
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].emp_company_fid == emp_fid && employeeList[i].emp_level == 7) DeleteEmployee(employeeList[i].emp_fid);
            }

            return tblEmployee.DeleteTblEmployee(emp_fid);
        }
        public Boolean DeleteEmployee(int emp_fid)
        {
            // 매장에 소속된 유저들을 삭제한다.
            List<int> employeeIds = new List<int>();
            employeeIds.Add(emp_fid);

            CLm2Member mLuckyPalMember = new CLm2Member();
            List<CLm2Member> lm2MemberList = mLuckyPalMember.GetAllMemberList(employeeIds, "");
            for (int i = 0; i < lm2MemberList.Count; i++)
            {
                mLuckyPalMember.DeleteMember(lm2MemberList[i].mb_fid);
            }

            return tblEmployee.DeleteTblEmployee(emp_fid);
        }

        public CEmployee GetMemberByFid(int emp_fid)
        {
            return tblEmployee.GetEmployeeByFid(emp_fid);
        }
        public CEmployee GetMemberByUid(string emp_uid)
        {
            return tblEmployee.GetEmployeeByUid(emp_uid);
        }
        public CEmployee GetMemberByNickName(string emp_nickname)
        {
            return tblEmployee.GetEmployeeByNickName(emp_nickname);
        }
        public List<CEmployee> GetEmployeeList()
        {
            return tblEmployee.GetAllEmployeeList();
        }

        // 로그인액션
        public int ActionEmployeeLogin(string emp_uid, string emp_pwd,
            string sess_id, string hostname, string browser, string ipaddress)
        {
            int iResult = 1;
            CEmployeeSession.ClearSession();
            CEmployee tmpEmployee = GetMemberByUid(emp_uid);
            if (tmpEmployee == null) return 2;                // 존재하지않는 아이디
            if (tmpEmployee.emp_pwd != emp_pwd) return 3;     // 비번틀림
            if (!tmpEmployee.emp_state_active) return 5;      // 차단된 아이디
//             if (tmpEmployee.emp_level >= 7)
//             {
//                 CEmployee mCompany = GetMemberByFid(tmpEmployee.emp_company_fid);
//                 if (!mCompany.emp_state_active) return 5;    // 차단된 아이디
//             }

            // 중복세션삭제
            mEmployeeSession = mEmployeeSession.GetSessionByEmpUId(emp_uid);
            if (mEmployeeSession != null) // multiple login with same account
            {
                if(ipaddress == mEmployeeSession.sess_ipaddress)    //같은 아이피인 경우 세션삭제
                    mEmployeeSession.DeleteSession(mEmployeeSession.sess_id);
                else return 6;   //이미 로그인 되어 있음
            }

            // 세션등록
            mEmployeeSession = new CEmployeeSession();
            mEmployeeSession.sess_id = sess_id;
            mEmployeeSession.sess_time_begin = DateTime.Now;
            mEmployeeSession.sess_time_last = DateTime.Now;
            mEmployeeSession.sess_hostname = hostname;
            mEmployeeSession.sess_browser = browser;
            mEmployeeSession.sess_ipaddress = ipaddress;
            mEmployeeSession.sess_emp_uid = emp_uid;
            mEmployeeSession.sess_emp_level = tmpEmployee.emp_level;
            if(mEmployeeSession.ResigterSession())
            {
                tmpEmployee.UpdateLastTimeEmployee();
            }

            return iResult;
        }
        // 로그아웃액션
        public void ActionLogOut(string sess_id)
        {
            mEmployeeSession = new CEmployeeSession();
            mEmployeeSession.DeleteSession(sess_id);
        }
        // 상단메뉴표시
        public Boolean GetTopMenu(int emp_fid, int menu_Index)
        {
            CEmployee mEmployee = new CEmployee();
            CEmployee mCompany = new CEmployee();
            mEmployee = mEmployee.GetMemberByFid(emp_fid);
            if (mEmployee == null) return false;
            if (mEmployee.emp_level == 11) return true;
            if (mEmployee.emp_level == 10) return true;
            if (mEmployee.emp_level == 8)
                mCompany = null;
            else if (mEmployee.emp_level == 7)
                mCompany = null;
            //mCompany = mCompany.GetMemberByFid(mEmployee.emp_company_fid);

            Boolean bMenuShow = false;
            switch (menu_Index)
            {
                case 0: // 매장관리
                    if (mEmployee.emp_level >= 8) bMenuShow = true;
                    break;
                case 1: // 리니지2M
                    bMenuShow = mEmployee.emp_b_app_01_lm2;
                    break;                
                
            }
            return bMenuShow;
        }

        // 부본사 & 총판 & 매장리스트를 반환해준다.(DROPDOWN)
        public DataTable BindEmployeeDropDownList(int category, int reqMb_Level, int reqMb_fid, int limit_level, Boolean bOnlyLevel)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            List<CEmployee> memberList = GetEmployeeList();
            List<CEmployee> companyList = new List<CEmployee>();
            List<CEmployee> agencyList = new List<CEmployee>();
            List<CEmployee> employeeList = new List<CEmployee>();

            int iCompanyFid = 0, iAgencyFid = 0;
            switch (reqMb_Level)
            {
                case 11:
                case 10:
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8) agencyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 7) employeeList.Add(memberList[i]);
                    }
                    break;
                case 9:
                    List<int> agencyIds = new List<int>();
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9 && memberList[i].emp_fid == reqMb_fid) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8 && memberList[i].emp_company_fid == reqMb_fid)
                        {
                            agencyList.Add(memberList[i]);
                            agencyIds.Add(memberList[i].emp_fid);
                        }
                    }
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 7 && agencyIds.Contains(memberList[i].emp_company_fid)) employeeList.Add(memberList[i]);
                    }
                    break;
                case 8:
                    CEmployee mLevel8Company = new CEmployee();
                    CEmployee mLevel8Agency = new CEmployee();
                    mLevel8Agency = mLevel8Agency.GetMemberByFid(reqMb_fid);
                    mLevel8Company = mLevel8Company.GetMemberByFid(mLevel8Agency.emp_company_fid);

                    iCompanyFid = mLevel8Company.emp_fid;

                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9 && memberList[i].emp_fid == mLevel8Company.emp_fid) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8 && memberList[i].emp_fid == reqMb_fid) agencyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 7 && memberList[i].emp_company_fid == reqMb_fid) employeeList.Add(memberList[i]);
                    }
                    break;
                case 7:
                    CEmployee mLevel7Company = new CEmployee();
                    CEmployee mLevel7Agency = new CEmployee();
                    CEmployee mLevel7Employee = new CEmployee();
                    mLevel7Employee = mLevel7Employee.GetMemberByFid(reqMb_fid);
                    mLevel7Agency = mLevel7Agency.GetMemberByFid(mLevel7Employee.emp_company_fid);
                    mLevel7Company = mLevel7Company.GetMemberByFid(mLevel7Agency.emp_company_fid);

                    iCompanyFid = mLevel7Company.emp_fid;
                    iAgencyFid = mLevel7Agency.emp_fid;

                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9 && memberList[i].emp_fid == mLevel7Company.emp_fid) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8 && memberList[i].emp_fid == mLevel7Agency.emp_fid) agencyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 7 && memberList[i].emp_fid == reqMb_fid) employeeList.Add(memberList[i]);
                    }
                    break;
            }

            // limit_level:: 7:매장만, 8: 총판만, 9:부본사만 표시
            for (int i = 0; i < companyList.Count; i++)
            {
                string strCompany = companyList[i].emp_nickname;

                if ((reqMb_Level == 11) || (reqMb_Level == 10) || (reqMb_Level == 9 && companyList[i].emp_fid == reqMb_fid) ||
                    (reqMb_Level == 8 && iCompanyFid == companyList[i].emp_fid) || (reqMb_Level == 7 && iCompanyFid == companyList[i].emp_fid))
                {
                    if (bOnlyLevel)
                    {
                        if (limit_level == 9)
                        {
                            Boolean bCategory = false;
                            switch (category)
                            {
                                case 0: bCategory = true; break;
                                case 1: if (companyList[i].emp_b_app_01_lm2) bCategory = true; break;                                
                            }
                            if (bCategory) dt.Rows.Add(new object[] { companyList[i].emp_nickname, companyList[i].emp_fid });
                        }
                    }
                    else
                    {
                        if (limit_level >= 9)
                        {
                            Boolean bCategory = false;
                            switch (category)
                            {
                                case 0: bCategory = true; break;
                                case 1: if (companyList[i].emp_b_app_01_lm2) bCategory = true; break;
                            }
                            if (bCategory) dt.Rows.Add(new object[] { companyList[i].emp_nickname, companyList[i].emp_fid });
                        }
                    }

                }

                for (int j = 0; j < agencyList.Count; j++)
                {
                    string strAgency = agencyList[j].emp_nickname;
                    if (companyList[i].emp_fid == agencyList[j].emp_company_fid)
                    {
                        if ((reqMb_Level == 11) || (reqMb_Level == 10) || (reqMb_Level == 9 && agencyList[i].emp_company_fid == reqMb_fid) ||
                            (reqMb_Level == 8 && agencyList[i].emp_fid == reqMb_fid) || (reqMb_Level == 7 && iAgencyFid == agencyList[i].emp_fid))
                        {
                            if (bOnlyLevel)
                            {
                                if (limit_level == 8)
                                {
                                    Boolean bCategory = false;
                                    switch (category)
                                    {
                                        case 0: bCategory = true; break;
                                        case 1: if (agencyList[j].emp_b_app_01_lm2) bCategory = true; break;
                                        
                                    }
                                    if (bCategory) dt.Rows.Add(new object[] { strCompany + "::" + agencyList[j].emp_nickname, agencyList[j].emp_fid });
                                }
                            }
                            else
                            {
                                if (limit_level >= 8)
                                {
                                    Boolean bCategory = false;
                                    switch (category)
                                    {
                                        case 0: bCategory = true; break;
                                        case 1: if (agencyList[j].emp_b_app_01_lm2) bCategory = true; break;

                                    }
                                    if (bCategory) dt.Rows.Add(new object[] { strCompany + "::" + agencyList[j].emp_nickname, agencyList[j].emp_fid });
                                }
                            }
                        }
                    }
                    for (int k = 0; k < employeeList.Count; k++)
                    {

                        if (companyList[i].emp_fid == agencyList[j].emp_company_fid && agencyList[j].emp_fid == employeeList[k].emp_company_fid)
                        {

                            if (bOnlyLevel)
                            {
                                if (limit_level == 7)
                                {
                                    Boolean bCategory = false;
                                    switch (category)
                                    {
                                        case 0: bCategory = true; break;
                                        case 1: if (employeeList[k].emp_b_app_01_lm2) bCategory = true; break;
                                        
                                    }
                                    if (bCategory) dt.Rows.Add(new object[] { strCompany + "::" + strAgency + "::" + employeeList[k].emp_nickname, employeeList[k].emp_fid });
                                }
                            }
                            else
                            {
                                if (limit_level >= 7)
                                {
                                    Boolean bCategory = false;
                                    switch (category)
                                    {
                                        case 0: bCategory = true; break;
                                        case 1: if (employeeList[k].emp_b_app_01_lm2) bCategory = true; break;

                                    }
                                    if (bCategory) dt.Rows.Add(new object[] { strCompany + "::" + strAgency + "::" + employeeList[k].emp_nickname, employeeList[k].emp_fid });
                                }
                            }
                        }

                    }
                }
            }
            return dt;
        }

        // 부본사 & 총판 & 매장리스트를 반환해준다.(현재 권한에 따른..)
        public List<CEmployee> GetEmployeeListByEmployee(int app_category, int reqMb_Level, int reqMb_fid)
        {
            List<CEmployee> tempList = GetEmployeeList();
            List<CEmployee> memberList = new List<CEmployee>();
            for (int i = 0; i < tempList.Count; i++)
            {
                Boolean bCategory = false;
                switch (app_category)
                {
                    case 0: bCategory = true; break;
                    case 1: if (tempList[i].emp_b_app_01_lm2) bCategory = true; break;
                                    }
                if (bCategory) memberList.Add(tempList[i]);
            }

            List<CEmployee> resultEmployeeList = new List<CEmployee>();

            List<CEmployee> companyList = new List<CEmployee>();
            List<CEmployee> agencyList = new List<CEmployee>();
            List<CEmployee> employeeList = new List<CEmployee>();

            switch (reqMb_Level)
            {
                case 11:
                case 10:
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8) agencyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 7) employeeList.Add(memberList[i]);
                    }
                    break;
                case 9:
                    List<int> agencyIds = new List<int>();
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 9 && memberList[i].emp_fid == reqMb_fid) companyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 8 && memberList[i].emp_company_fid == reqMb_fid)
                        {
                            agencyList.Add(memberList[i]);
                            agencyIds.Add(memberList[i].emp_fid);
                        }
                    }
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 7 && agencyIds.Contains(memberList[i].emp_company_fid)) employeeList.Add(memberList[i]);
                    }
                    break;
                case 8:
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 8 && memberList[i].emp_fid == reqMb_fid) agencyList.Add(memberList[i]);
                        if (memberList[i].emp_level == 7 && memberList[i].emp_company_fid == reqMb_fid) employeeList.Add(memberList[i]);
                    }
                    break;
                case 7:
                    for (int i = 0; i < memberList.Count; i++)
                    {
                        if (memberList[i].emp_level == 7 && memberList[i].emp_fid == reqMb_fid) employeeList.Add(memberList[i]);
                    }
                    break;
            }

            for (int i = 0; i < companyList.Count; i++) resultEmployeeList.Add(companyList[i]);
            for (int i = 0; i < agencyList.Count; i++) resultEmployeeList.Add(agencyList[i]);
            for (int i = 0; i < employeeList.Count; i++) resultEmployeeList.Add(employeeList[i]);
            return resultEmployeeList;
        }
    }
}