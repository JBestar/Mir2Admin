using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin
{
    public partial class Index : System.Web.UI.Page
    {
        CEmployee curEmployee = new CEmployee();
        CEmployeeSession curEmployeeSession = new CEmployeeSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployeeSession = curEmployeeSession.GetSessionBySessId(Session["sessId"].ToString());
            if (curEmployeeSession != null)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "이미 로그인되어 있습니다.");
                string url = GetEmployeeLoginUrl();
                if (url != "") Response.Redirect(url);
                return;
            }
            else if (Request["uid"] != null && Request["pwd"] != null)
            {
                int iLoginResult = curEmployee.ActionEmployeeLogin(
                    Request["uid"].ToString(),
                    Request["pwd"].ToString(),
                    Session["sessId"].ToString(),
                    "",
                    "",
                    CUtilsNetwork.GetClientIpAddress(Request));
                switch (iLoginResult)
                {
                    case 0:
                        {
                            string url = GetEmployeeLoginUrl();
                            if (url != "") Response.Redirect(url);
                            
                        }
                        break;
                    case 1:
                        {
                            string url = GetEmployeeLoginUrl();
                            if (url != "") Response.Redirect(url);
                        }
                        break;
                    //case 2: CUtilsMessageBox.ShowAlertBox2(this.Page, "존재하지 않는 아이디입니다."); break;
                    //case 3: CUtilsMessageBox.ShowAlertBox2(this.Page, "비번이 정확하지 않습니다."); break;
                    //case 4: CUtilsMessageBox.ShowAlertBox2(this.Page, "삭제된 아이디입니다."); break;
                    //case 5: CUtilsMessageBox.ShowAlertBox2(this.Page, "차단된 아이디입니다."); break;
                    case 6: CUtilsMessageBox.ShowAlertBox2(this.Page, "이미 로그인되어 있습니다."); break;
                }

               
            }
        }
        protected string GetEmployeeLoginUrl()
        {
            string url = "";
            curEmployeeSession = new CEmployeeSession();
            curEmployeeSession = curEmployeeSession.GetSessionBySessId(Session["sessId"].ToString());
            if (curEmployeeSession == null) return url;

            curEmployee = new CEmployee();
            curEmployee = curEmployee.GetMemberByUid(curEmployeeSession.sess_emp_uid);
            if (curEmployee == null) return url;

            switch (curEmployee.emp_level)
            {
                case 11:
                case 10: url = "Staff/company.aspx"; break;                
                case 9: url = "Staff/agency.aspx"; break;
                case 8: url = "Staff/employee.aspx"; break;
                case 7: url = "Mir2/member.aspx"; break;

                default:
                    break;

  

            }
            return url;
        }
    }

}