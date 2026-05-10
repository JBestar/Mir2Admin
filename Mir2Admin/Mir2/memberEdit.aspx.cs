using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class memberEdit : System.Web.UI.Page
    {
        public int category = 1;
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();
        //public string to_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DataTable dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, 7, true);

                DropDownEmployee.DataSource = dtEmployee;
                DropDownEmployee.DataTextField = "name";
                DropDownEmployee.DataValueField = "value";
                DropDownEmployee.DataBind();
            }

            mMember = new CLm2Member();
            if (Request["id"] == null)
            {
                mMember = null;
                BtnAutoMember.Visible = true;                
            }
            else
            {
                mMember = mMember.GetMemberByFid(int.Parse(Request["id"]));
                BtnAutoMember.Visible = false;
            }
            if (!IsPostBack) ShowMemberParams();
        }
        protected void ShowMemberParams()
        {
            if (mMember == null)
            {
                txt_until_date.Text = DateTime.Today.ToShortDateString();
                return;
            }
            else
                DropDownEmployee.SelectedValue = mMember.mb_emp_fid.ToString();

            txt_mb_uid.Text = mMember.mb_uid;
            txt_mb_pwd.Text = mMember.mb_pwd;
            txt_mb_nickname.Text = mMember.mb_nickname;
            txt_mb_handphone.Text = mMember.mb_handphone;
            Chk_mb_vip.Checked = mMember.mb_vip;
            //to_date = mMember.mb_time_limit.ToString("yyyy-MM-dd");

            txt_until_date.Text = mMember.mb_time_limit.ToString("yyyy-MM-dd");

            txt_mb_uid.Enabled = false;
            txt_mb_nickname.Enabled = false;
        }
        protected Boolean CheckMemberParams()
        {
            Boolean bResult = true;

            if (txt_mb_uid.Text == "") { txt_mb_uid.Focus(); bResult = false; }
            if (txt_mb_pwd.Text == "") { txt_mb_pwd.Focus(); bResult = false; }
            //if (txt_mb_nickname.Text == "") { txt_mb_nickname.Focus(); bResult = false; }
            //if (txt_mb_handphone.Text == "") { txt_mb_handphone.Focus(); bResult = false; }
            return bResult;
        }
        protected void BtnSaveMember_Click(object sender, EventArgs e)
        {
            if (CheckMemberParams() == false) return;

            int iMode = 0;
            if (Request["id"] == null)
            {
                iMode = 0;
                mMember = new CLm2Member();
            }
            else
            {
                iMode = 1;
                mMember = new CLm2Member();
                mMember = mMember.GetMemberByUid(txt_mb_uid.Text);
            }

            if (iMode == 0) // 새로등록일때 아이디, 닉네임체크
            {
                mMember = new CLm2Member();
                mMember = mMember.GetMemberByUid(txt_mb_uid.Text);
                if (mMember != null)
                {
                    CUtilsMessageBox.ShowAlertBox(this.Page, "이미 존재하는 아이디입니다.");
                    return;
                }

//                 mMember = new CLm2Member();
//                 mMember = mMember.GetMemberByNickName(txt_mb_nickname.Text);
//                 if (mMember != null)
//                 {
//                     CUtilsMessageBox.ShowAlertBox(this.Page, "이미존재하는 닉네임입니다.");
//                     return;
//                 }

                mMember = new CLm2Member();
                mMember.mb_uid = txt_mb_uid.Text;
                mMember.mb_nickname = txt_mb_nickname.Text;
                mMember.mb_level = 1;
                mMember.mb_domain = "";
                mMember.mb_username = "";
                mMember.mb_password = "";
                mMember.mb_local_key = "";
                mMember.mb_last_ipaddr = "127.0.0.1";

                mMember.mb_state_active = true;
                mMember.mb_state_delete = false;
            }
            mMember.mb_pwd = txt_mb_pwd.Text;
            mMember.mb_handphone = txt_mb_handphone.Text;
            
            mMember.mb_time_limit = CutilsDate.GetDateTime(txt_until_date.Text);
            //mMember.mb_time_limit = mMember.mb_time_limit.AddHours(12);
            mMember.mb_vip = Chk_mb_vip.Checked;
            mMember.mb_emp_fid = int.Parse(DropDownEmployee.SelectedValue);
            if (mMember.mb_emp_fid == 0) return;

            if (iMode == 0) if (mMember.ResigterMember())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "회원정보가 성공적으로 등록되었습니다.", "member.aspx");
            if (iMode == 1) if (mMember.UpdateMember())
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "회원정보가 성공적으로 변경되었습니다.", "member.aspx");
        }

        protected void BtnAutoMember_Click(object sender, EventArgs e)
        {
            int seed = Environment.TickCount;
            //DateTime dtTime = CutilsDate.GetDateTime(Request["text_to_date"].ToString());
            DateTime dtTime = CutilsDate.GetDateTime(txt_until_date.Text);
            UtilsKeyGen keyGen = new UtilsKeyGen();
            string strUid = keyGen.GenerateId(seed, dtTime.Year, dtTime.Month, dtTime.Day);
            int pwdLen = 4;
            string strPwd = keyGen.GeneratePwd(seed, pwdLen);

            txt_mb_uid.Text = strUid;
            txt_mb_pwd.Text = strPwd;
            txt_mb_nickname.Text = "";
            txt_mb_handphone.Text = "111";
            Chk_mb_vip.Checked = true;
        }

        protected void BtnCancelMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("member.aspx");
        }
    }
}