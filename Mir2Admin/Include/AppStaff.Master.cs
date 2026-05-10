using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Include
{
    public partial class AppStaff : System.Web.UI.MasterPage
    {

        public string strTitle = "";
        public string strFold = "";
        public CEmployee curEmployee = new CEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            CEmployeeSession.ClearSession();

            CLm2Session.ClearSession();
            
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
            strTitle = "미르회원관리@2020 [" + curEmployee.emp_nickname + "]";
            LinkLogOut.Text = curEmployee.emp_uid + " 로그아웃";

            Link_Employee.Visible = curEmployee.GetTopMenu(curEmployee.emp_fid, 0);
            Link_LM2.Visible = curEmployee.GetTopMenu(curEmployee.emp_fid, 1);

            string[] path = Request.CurrentExecutionFilePath.Split('/');
            strFold = path[path.Length - 2];

            switch (strFold)
            {
                case "Staff": Link_Employee.ForeColor = System.Drawing.Color.LightSeaGreen; break;
                case "Mir2": Link_LM2.ForeColor = System.Drawing.Color.LightSeaGreen; break;
                case "Update": Link_Update.ForeColor = System.Drawing.Color.LightSeaGreen; break;
            }
        }

        protected void Link_Employee_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Staff/employee.aspx");
        }

        protected void Link_LM2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mir2/member.aspx");
        }

        protected void Link_Update_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Update/history.aspx");
        }

        protected void Link_Download_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Update/download.aspx");
        }

        protected void LinkPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Staff/password.aspx");
        }

        protected void LinkLogOut_Click(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = new CEmployee();
            curEmployee.ActionLogOut(Session["sessId"].ToString());
            Response.Redirect("../index.aspx");
        }
    }
}