using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class memberMulti : System.Web.UI.Page
    {
        public int category = 1;
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();
        //public string to_date = DateTime.Now.ToString("yyyy-MM-dd");

        public static Dictionary<string, string> mUidList = new Dictionary<string, string>();
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

            if (!IsPostBack)
            {
                txt_until_date.Text = DateTime.Today.ToShortDateString();
            }
            
        }

        private void CreateMemberData()
        {
            //성원아이디, 암호 다중생성
            int nMkCnt = 0;
            try
            {
                nMkCnt = int.Parse(txt_mk_cnt.Text);
            }
            catch
            {
                CUtilsMessageBox.ShowAlertBox(this.Page, "생성개수를 정확히 입력해주세요.");
                return;
            }

            DateTime toDate = CutilsDate.GetDateTime(txt_until_date.Text);
            DateTime regDate = DateTime.Now;
            int pwdLen = 4;

            int seed = Environment.TickCount;

            mUidList.Clear();                   //아이디와 암호 림시보관을 위한 매프 초기화

            int curCnt = 0;
            while (curCnt < nMkCnt)
            {
                UtilsKeyGen keyGen = new UtilsKeyGen();
                string strUid = keyGen.GenerateId(seed++, toDate.Year, toDate.Month, toDate.Day);
                string strPwd = keyGen.GeneratePwd(seed++, pwdLen);

                mMember = new CLm2Member();
                mMember = mMember.GetMemberByUid(strUid);

                if (mMember == null && !mUidList.ContainsKey(strUid))
                {            
                    curCnt++;
                    mUidList.Add(strUid, strPwd);

                }
            }            
        }

        private void ViewMemberData()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_mb_uid");
            dt.Columns.Add("lbl_mb_pwd");

            for (int i = 0; i < mUidList.Count; i++)
            {
                
                DataRow newRow = dt.NewRow();

                newRow["lbl_mb_uid"] = mUidList.ElementAt(i).Key;
                newRow["lbl_mb_pwd"] = mUidList.ElementAt(i).Value;
                dt.Rows.Add(newRow);
                            
            }

            GridMember.DataSource = dt.DefaultView;
            GridMember.DataBind();
        }

        protected Boolean CheckMemberParams()
        {
            Boolean bResult = true;
            /*
            if (txt_mb_uid.Text == "") { txt_mb_uid.Focus(); bResult = false; }
            if (txt_mb_pwd.Text == "") { txt_mb_pwd.Focus(); bResult = false; }
            if (txt_mb_nickname.Text == "") { txt_mb_nickname.Focus(); bResult = false; }
            if (txt_mb_handphone.Text == "") { txt_mb_handphone.Focus(); bResult = false; }
            */
            if (DropDownEmployee.SelectedValue.Length <= 0)
            {
                bResult = false;

            }

            return bResult;
        }

        protected void PageChang(object sender, GridViewPageEventArgs e)
        {            
            GridMember.PageIndex = e.NewPageIndex;
            ViewMemberData();            
        }


    protected void BtnSaveMember_Click(object sender, EventArgs e)
        {
            Boolean bRes = true;

            if (CheckMemberParams() == false)
            {
                CUtilsMessageBox.ShowAlertLinkBox(this.Page, "회원정보를 등록할수 없습니다.", "memberMulti.aspx");
                return;
            }
            CLm2Member tmpMember;
            for(int i = 0; i < mUidList.Count; i ++)
            {               

                mMember = new CLm2Member();
                mMember.mb_uid = mUidList.ElementAt(i).Key;
                mMember.mb_nickname = txt_mb_nickname.Text;
                mMember.mb_level = 1;
                mMember.mb_domain = "";
                mMember.mb_username = "";
                mMember.mb_password = "";
                mMember.mb_local_key = "";
                mMember.mb_last_ipaddr = "127.0.0.1";
                
                mMember.mb_state_active = true;
                mMember.mb_state_delete = false;

                mMember.mb_pwd = mUidList.ElementAt(i).Value;
                mMember.mb_handphone = "";
                mMember.mb_time_limit = CutilsDate.GetDateTime(txt_until_date.Text);
                //mMember.mb_time_limit = mMember.mb_time_limit.AddHours(12);
                mMember.mb_vip = Chk_mb_vip.Checked;
                mMember.mb_emp_fid = int.Parse(DropDownEmployee.SelectedValue);
                if (mMember.mb_emp_fid == 0) return;

                tmpMember = new CLm2Member();
                tmpMember = tmpMember.GetMemberByUid(mMember.mb_uid);
                if(tmpMember == null)
                    bRes = mMember.ResigterMember();
            }
            if(bRes && mUidList.Count>0)
                CUtilsMessageBox.ShowAlertLinkBox(this.Page, "회원정보가 성공적으로 등록되었습니다.", "member.aspx");

        }

        protected void BtnAutoMember_Click(object sender, EventArgs e)
        {
            CreateMemberData();
            ViewMemberData();
        }

        protected void BtnCancelMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("member.aspx");
        }
    }
}