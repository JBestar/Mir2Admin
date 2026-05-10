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
    public partial class agencyEdit : System.Web.UI.Page
    {
        public int category = 0;
        public CEmployee curEmployee = new CEmployee();
        public CEmployee mAgency = new CEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
            else if (curEmployee.emp_level < 9)
            {
                Response.Redirect("../index.aspx");
                return;
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////

            if (!IsPostBack)
            {
                DataTable dtEmployee;
                dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, 9, true);

                DropDownEmployee.DataSource = dtEmployee;
                DropDownEmployee.DataTextField = "name";
                DropDownEmployee.DataValueField = "value";
                DropDownEmployee.DataBind();
            }

            mAgency = new CEmployee();
            if (Request["id"] == null)
            {
                mAgency = null;
            }
            else
            {
                mAgency = mAgency.GetMemberByFid(int.Parse(Request["id"]));
                if (!IsPostBack) ShowCompanyParams();
            }
            
        }
        protected void ShowCompanyParams()
        {
     

            DropDownEmployee.SelectedValue = mAgency.emp_company_fid.ToString();
            txt_mb_uid.Text = mAgency.emp_uid;
            txt_mb_pwd.Text = mAgency.emp_pwd;
            txt_mb_nickname.Text = mAgency.emp_nickname;
            txt_mb_color.Text = mAgency.emp_color;
            
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
                mAgency = new CEmployee();
            }
            else
            {
                iMode = 1;
                mAgency = new CEmployee();
                mAgency = mAgency.GetMemberByUid(txt_mb_uid.Text);
            }

            if (iMode == 0) // 새로등록일때 아이디, 닉네임체크
            {
                mAgency = new CEmployee();
                mAgency = mAgency.GetMemberByUid(txt_mb_uid.Text);
                if (mAgency != null)
                {
                    CUtilsMessageBox.ShowAlertBox(this.Page, "이미존재하는 아이디입니다.");
                    return;
                }

                mAgency = new CEmployee();
                mAgency = mAgency.GetMemberByNickName(txt_mb_nickname.Text);
                if (mAgency != null)
                {
                    CUtilsMessageBox.ShowAlertBox(this.Page, "이미존재하는 닉네임입니다.");
                    return;
                }

                mAgency = new CEmployee();
                mAgency.emp_uid = txt_mb_uid.Text;                
                mAgency.emp_nickname = txt_mb_nickname.Text;
                mAgency.emp_last_ipaddr = "";
                mAgency.emp_comment = "";
                mAgency.emp_state_active = true;
                if (curEmployee.emp_level >= 9)
                {
                    // 본사이상일떄 총판생성
                    mAgency.emp_level = 8;
                }
                if (mAgency.emp_level == 0) return;
            }

            mAgency.emp_company_fid = int.Parse(DropDownEmployee.SelectedValue);
            mAgency.emp_pwd = txt_mb_pwd.Text;
            mAgency.emp_color = txt_mb_color.Text;

            if (curEmployee.emp_level >= 9)
            {
                mAgency.emp_b_app_01_lm2 = true;

            }

            if (iMode == 0) if (mAgency.ResigterEmployee())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "총판정보가 성공적으로 등록되었습니다.", "agency.aspx");
            if (iMode == 1) if (mAgency.UpdateEmployee())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "총판정보가 성공적으로 변경되었습니다.", "agency.aspx");
        }
        protected void BtnCancelCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("agency.aspx");
        }
    }
}