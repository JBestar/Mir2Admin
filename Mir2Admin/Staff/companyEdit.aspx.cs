using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using Mir2Admin.Models;

namespace Mir2Admin.Staff
{
    public partial class companyEdit : System.Web.UI.Page
    {
        public int category = 0;
        public CEmployee curEmployee = new CEmployee();
        public CEmployee mCompany = new CEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
            else if (curEmployee.emp_level < 10)
            {
                Response.Redirect("../index.aspx");
                return;
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////

            if (!IsPostBack)
            {
                DataTable dtEmployee;
                dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, 9, true);
                if (curEmployee.emp_level >= 10 && Request["id"] == null)
                    dtEmployee.Rows.Add(new object[] { "새로등록", 0 });
                
            }

            mCompany = new CEmployee();
            if (Request["id"] == null)
            {
                mCompany = null;
            }
            else
            {
                mCompany = mCompany.GetMemberByFid(int.Parse(Request["id"]));
                if (!IsPostBack) ShowCompanyParams();
            }
            
        }
        protected void ShowCompanyParams()
        {

            txt_mb_uid.Text = mCompany.emp_uid;
            txt_mb_pwd.Text = mCompany.emp_pwd;
            txt_mb_nickname.Text = mCompany.emp_nickname;
            txt_mb_color.Text = mCompany.emp_color;
            
            txt_mb_uid.Enabled = false;
            txt_mb_nickname.Enabled = false;
        }
        protected Boolean CheckCompanyParams()
        {
            Boolean bResult = true;
            if (txt_mb_uid.Text == "") { txt_mb_uid.Focus(); bResult = false; }
            if (txt_mb_pwd.Text == "") { txt_mb_pwd.Focus(); bResult = false; }
            if (txt_mb_nickname.Text == "") { txt_mb_nickname.Focus(); bResult = false; }
            return bResult;
        }
        protected void BtnSaveCompany_Click(object sender, EventArgs e)
        {
            if (CheckCompanyParams() == false) return;

            int iMode = 0;
            if (Request["id"] == null)
            {
                iMode = 0;
                mCompany = new CEmployee();
            }
            else
            {
                iMode = 1;
                mCompany = new CEmployee();
                mCompany = mCompany.GetMemberByUid(txt_mb_uid.Text);
            }

            if (iMode == 0) // 새로등록일때 아이디, 닉네임체크
            {
                mCompany = new CEmployee();
                mCompany = mCompany.GetMemberByUid(txt_mb_uid.Text);
                if (mCompany != null)
                {
                    CUtilsMessageBox.ShowAlertBox(this.Page, "이미존재하는 아이디입니다.");
                    return;
                }

                mCompany = new CEmployee();
                mCompany = mCompany.GetMemberByNickName(txt_mb_nickname.Text);
                if (mCompany != null)
                {
                    CUtilsMessageBox.ShowAlertBox(this.Page, "이미존재하는 닉네임입니다.");
                    return;
                }

                mCompany = new CEmployee();
                mCompany.emp_uid = txt_mb_uid.Text;
                mCompany.emp_company_fid = 0;
                mCompany.emp_nickname = txt_mb_nickname.Text;
                mCompany.emp_last_ipaddr = "";
                mCompany.emp_comment = "";
                mCompany.emp_state_active = true;
                if (curEmployee.emp_level >= 10)
                {
                    // 운영팀만 본사생성
                    mCompany.emp_level = 9;
                }
                if (mCompany.emp_level == 0) return;
            }
            mCompany.emp_pwd = txt_mb_pwd.Text;
            mCompany.emp_color = txt_mb_color.Text;

            if (curEmployee.emp_level >= 10)
            {
                mCompany.emp_b_app_01_lm2 = true;
            }

            if (iMode == 0) if (mCompany.ResigterEmployee())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "본사정보가 성공적으로 등록되었습니다.", "company.aspx");
            if (iMode == 1) if (mCompany.UpdateEmployee())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "본사정보가 성공적으로 변경되었습니다.", "company.aspx");
        }
        protected void BtnCancelCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("company.aspx");
        }
    }
}