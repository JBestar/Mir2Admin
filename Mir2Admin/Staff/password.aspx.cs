using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Staff
{
    public partial class password : System.Web.UI.Page
    {
        CEmployee curEmployee = new CEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
        }
        protected void BtnSavePassword_Click(object sender, EventArgs e)
        {
            if (txt_password_0.Text == "" || txt_password_1.Text == "" || txt_password_2.Text == "") return;
            if (txt_password_1.Text != txt_password_2.Text) return;
            if (txt_password_0.Text != curEmployee.emp_pwd) return;

            curEmployee.emp_pwd = txt_password_1.Text;
            if (curEmployee.UpdateEmployee())
                CUtilsMessageBox.ShowAlertBox(this.Page, "비밀번호가 변경되었습니다.");
        }
        protected void BtnCancelPassword_Click(object sender, EventArgs e)
        {

        }
    }
}