using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Drawing;
using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class UserManage : System.Web.UI.Page
    {
        public int category = 1;
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();

        List<CBlackList> mArrBlackList = new List<CBlackList>();
        List<CCharacList> mArrUserList = new List<CCharacList>();
        private int servKindCnt = 15;
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


            if (!IsPostBack)
            {
                initDropList();
                ViewBlackList();
                ViewUserList();
            }
        }

        private void initDropList()
        {
            //매장정보 드롭리스트
            DataTable dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, curEmployee.emp_level, false);
            if (curEmployee.emp_level >= 10)
            {
                dtEmployee.Rows.Add(new object[] { "--전체--", 0 });
            }
            //블랙리스트 매장정보
            DropBlackEmployee.DataSource = dtEmployee;
            DropBlackEmployee.DataTextField = "name";
            DropBlackEmployee.DataValueField = "value";
            DropBlackEmployee.DataBind();
            DropBlackEmployee.SelectedValue = "0";
            
            //아군리스트 매장정보
            DropUserEmployee.DataSource = dtEmployee;
            DropUserEmployee.DataTextField = "name";
            DropUserEmployee.DataValueField = "value";
            DropUserEmployee.DataBind();
            DropUserEmployee.SelectedValue = "0";

            //서버정보 드롭리스트
            DataTable dtServerName = new DataTable();
            dtServerName.Columns.Add("name");
            dtServerName.Columns.Add("value");
            string servName = "";
            dtServerName.Rows.Add(new object[] { "전체", 0 });
            for (int i = 1; i <= servKindCnt; i ++)
            {
                servName = getSererName(i);
                dtServerName.Rows.Add(new object[] { servName, i });
            }
            //블랙리스트 서버정보
            DropBlackServerName.DataSource = dtServerName;
            DropBlackServerName.DataTextField = "name";
            DropBlackServerName.DataValueField = "value";
            DropBlackServerName.DataBind();
            DropBlackServerName.SelectedValue = "0";

            //아군리스트 서버정보
            DropUserServerName.DataSource = dtServerName;
            DropUserServerName.DataTextField = "name";
            DropUserServerName.DataValueField = "value";
            DropUserServerName.DataBind();
            DropUserServerName.SelectedValue = "0";


            
            DataTable dtServerNo = new DataTable();
            dtServerNo.Columns.Add("name");
            dtServerNo.Columns.Add("value");
            dtServerNo.Rows.Add(new object[] { "전체", 0 });
            string servNo = "";
            for (int i = 1; i < 11; i++)
            {
                servNo = i.ToString();
                dtServerNo.Rows.Add(new object[] { servNo, i });
            }
            //블랙리스트 서버번호
            DropBlackServerNo.DataSource = dtServerNo;
            DropBlackServerNo.DataTextField = "name";
            DropBlackServerNo.DataValueField = "value";
            DropBlackServerNo.DataBind();
            DropBlackServerNo.SelectedValue = "0";
            //아군리스트 서버번호
            DropUserServerNo.DataSource = dtServerNo;
            DropUserServerNo.DataTextField = "name";
            DropUserServerNo.DataValueField = "value";
            DropUserServerNo.DataBind();
            DropUserServerNo.SelectedValue = "0";

        }

        protected string getSererName(int iNameIndex)
        {
            switch (iNameIndex)
            {
                case 0: return "";
                case 1: return "바츠";
                case 2: return "지그하르트";
                case 3: return "카인";
                case 4: return "리오나";
                case 5: return "에리카";
                case 6: return "거스틴";
                case 7: return "카스티엔";
                case 8: return "아리아";
                case 9: return "드비안느";
                case 10: return "테온";
                case 11: return "에르휘나";
                case 12: return "아이린";
                case 13: return "오필리아";
                case 14: return "바이움";
                case 15: return "안타라스";
                default: return "";
            }
        }

        protected void ViewBlackList()
        {
            //블랙리스트 현시
            List<int> employeeIds = new List<int>();
            List<CEmployee> EmployeeList = curEmployee.GetEmployeeList();
            if (DropBlackEmployee.SelectedValue == "0")
            {
                for (int iEmployee = 0; iEmployee < EmployeeList.Count; iEmployee++)
                    employeeIds.Add(EmployeeList[iEmployee].emp_fid);
            }
            else
            {
                int iSelected = int.Parse(DropBlackEmployee.SelectedValue);
                CEmployee mEmployee = new CEmployee();
                mEmployee = mEmployee.GetMemberByFid(iSelected);
                if (mEmployee == null) return;

                List<CEmployee> Employee2List = mEmployee.GetEmployeeListByEmployee(category, mEmployee.emp_level, mEmployee.emp_fid);
                for (int iEmployee = 0; iEmployee < EmployeeList.Count; iEmployee++)
                {
                    for (int iSelect = 0; iSelect < Employee2List.Count; iSelect++)
                    {
                        if (EmployeeList[iEmployee].emp_fid == Employee2List[iSelect].emp_fid)
                        {
                            employeeIds.Add(EmployeeList[iEmployee].emp_fid);
                            break;
                        }
                    }
                }
            }

            int iSelSevName = int.Parse(DropBlackServerName.SelectedValue);
            int iSelSevNo = 0;
            int serverNo = 0;
            if (iSelSevName > 0)
            { 
                iSelSevNo = int.Parse(DropBlackServerNo.SelectedValue);
                if (iSelSevNo > 0)
                    serverNo = iSelSevName * 10 + iSelSevNo;
                else serverNo = -iSelSevName * 10;

            }
            string blackName = txt_black.Text;

            CBlackList mBlackList = new CBlackList();
            mArrBlackList = mBlackList.GetBlackList(employeeIds, serverNo, blackName);

            
            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_black_index");
            dt.Columns.Add("lbl_black_server");
            dt.Columns.Add("lbl_black_name");
            dt.Columns.Add("lbl_black_no");

            string servName = "";
            for (int i = 0; i < mArrBlackList.Count; i++)
            {
                DataRow newRow = dt.NewRow();

                newRow["lbl_black_index"] = i + 1;

                if (mArrBlackList[i].black_server_no % 10 == 0)
                    servName = getSererName(mArrBlackList[i].black_server_no / 10) + 10;
                else
                    servName = getSererName(mArrBlackList[i].black_server_no / 10 + 1) + "0" + mArrBlackList[i].black_server_no % 10;

                newRow["lbl_black_server"] = servName;
                newRow["lbl_black_name"] = mArrBlackList[i].black_player_name;
                newRow["lbl_black_no"] = mArrBlackList[i].black_no;

                dt.Rows.Add(newRow);
            }

            GridBlackList.DataSource = dt.DefaultView;
            GridBlackList.DataBind();
            
        }
        protected void ViewUserList()
        {
            //아군리스트 현시
            List<int> employeeIds = new List<int>();
            List<CEmployee> EmployeeList = curEmployee.GetEmployeeList();
            if (DropUserEmployee.SelectedValue == "0")
            {
                for (int iEmployee = 0; iEmployee < EmployeeList.Count; iEmployee++)
                    employeeIds.Add(EmployeeList[iEmployee].emp_fid);
            }
            else
            {
                int iSelected = int.Parse(DropUserEmployee.SelectedValue);
                CEmployee mEmployee = new CEmployee();
                mEmployee = mEmployee.GetMemberByFid(iSelected);
                if (mEmployee == null) return;

                List<CEmployee> Employee2List = mEmployee.GetEmployeeListByEmployee(category, mEmployee.emp_level, mEmployee.emp_fid);
                for (int iEmployee = 0; iEmployee < EmployeeList.Count; iEmployee++)
                {
                    for (int iSelect = 0; iSelect < Employee2List.Count; iSelect++)
                    {
                        if (EmployeeList[iEmployee].emp_fid == Employee2List[iSelect].emp_fid)
                        {
                            employeeIds.Add(EmployeeList[iEmployee].emp_fid);
                            break;
                        }
                    }
                }
            }

            int iSelSevName = int.Parse(DropUserServerName.SelectedValue);
            int iSelSevNo = 0;
            int serverNo = 0;
            if (iSelSevName > 0)
            {
                iSelSevNo = int.Parse(DropUserServerNo.SelectedValue);
                if (iSelSevNo > 0)
                    serverNo = iSelSevName * 10 + iSelSevNo;
                else serverNo = -iSelSevName * 10;

            }
            string charName = txt_user.Text;

            CCharacList mCharacList = new CCharacList();
            mArrUserList = mCharacList.GetCharacList(employeeIds, serverNo, charName);
            
            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_user_index");
            dt.Columns.Add("lbl_user_server");
            dt.Columns.Add("lbl_user_name");
            dt.Columns.Add("lbl_user_no");

            string servName = "";
            for (int i = 0; i < mArrUserList.Count; i++)
            {
                DataRow newRow = dt.NewRow();

                newRow["lbl_user_index"] = i + 1;
                if(mArrUserList[i].charac_server_no % 10 == 0)
                    servName = getSererName(mArrUserList[i].charac_server_no / 10 ) + 10;
                else
                    servName = getSererName(mArrUserList[i].charac_server_no / 10 + 1) + "0" + mArrUserList[i].charac_server_no % 10;

                newRow["lbl_user_server"] = servName;
                newRow["lbl_user_name"] = mArrUserList[i].charac_player_name;
                newRow["lbl_user_no"] = mArrUserList[i].charac_no;

                dt.Rows.Add(newRow);
            }

            GridUserList.DataSource = dt.DefaultView;
            GridUserList.DataBind();
        }

        protected void GridBlackList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string blackNo = e.CommandArgument.ToString();
            int iBlackNo = int.Parse(blackNo);

            if (iBlackNo < 0)
                return;
            CBlackList mBlack = new CBlackList();
            mBlack = mBlack.FindBlackByNo(iBlackNo);

            if (mBlack == null)
                return;
            switch (e.CommandName)
            {
                case "cmdBlackDelete":    // 적삭제
                    mBlack.DeleteBlackListByNo(iBlackNo);
                    ViewBlackList();
                    break;
                case "cmdBlackActive":    // 적활성
                    if(mBlack.black_player_enable == 1)
                        mBlack.UpdateBlackListByNo(iBlackNo, 0);
                    else mBlack.UpdateBlackListByNo(iBlackNo, 1);
                    ViewBlackList();
                    break;
                    
            }
        }

        protected void GridBlackList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iRowIndex = GridBlackList.PageIndex * GridBlackList.PageSize + e.Row.RowIndex;
                if (mArrBlackList.Count > iRowIndex )
                { 

                Button btnActive = e.Row.FindControl("BtnBlackActive") as Button;
                btnActive.Text = mArrBlackList[iRowIndex].black_player_enable == 1 ? "활성" : "비활성";
                btnActive.ForeColor = mArrBlackList[iRowIndex].black_player_enable == 1 ?
                    Color.FromName("#0000FF") : Color.FromName("#FF0000");
                }
            }
        }
        protected void GridUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string charNo = e.CommandArgument.ToString();
            int iCharacNo = int.Parse(charNo);

            if (iCharacNo < 0)
                return;
            CCharacList mCharac = new CCharacList();
            mCharac = mCharac.FindCharacByNo(iCharacNo);

            if (mCharac == null)
                return;
            switch (e.CommandName)
            {
                case "cmdUserDelete":    // 아군삭제
                    mCharac.DeleteCharacListByNo(iCharacNo);
                    ViewUserList();
                    break;
                case "cmdUserActive":    // 아군활성
                    if (mCharac.charac_player_enable == 1)
                        mCharac.UpdateCharacListByNo(iCharacNo, 0);
                    else mCharac.UpdateCharacListByNo(iCharacNo, 1);
                    ViewUserList();
                    break;

            }
        }

        protected void GridUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iRowIndex = GridUserList.PageIndex * GridUserList.PageSize + e.Row.RowIndex;
                if (mArrUserList.Count > iRowIndex)
                { 
                Button btnActive = e.Row.FindControl("BtnUserActive") as Button;
                btnActive.Text = mArrUserList[iRowIndex].charac_player_enable == 1 ? "활성" : "비활성";
                btnActive.ForeColor = mArrUserList[iRowIndex].charac_player_enable == 1 ?
                    Color.FromName("#0000FF") : Color.FromName("#FF0000");
                }
            }
        }

        protected void BlackListChange(object sender, GridViewPageEventArgs e)
        {
            GridBlackList.PageIndex = e.NewPageIndex;
            ViewBlackList();
        }

        protected void UserListChange(object sender, GridViewPageEventArgs e)
        {
            GridUserList.PageIndex = e.NewPageIndex;
            ViewUserList();
        }

        protected void BtnBlackView_Click(object sender, EventArgs e)
        {
            ViewBlackList();
        }

        protected void BtnBlackAdd_Click(object sender, EventArgs e)
        {
            //블랙리스트 추가

            int iSelectedEmpId = int.Parse(DropBlackEmployee.SelectedValue);
            if(iSelectedEmpId < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            }

            CEmployee mEmployee = new CEmployee();
            mEmployee = mEmployee.GetMemberByFid(iSelectedEmpId);
            if (mEmployee == null)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            } else if (mEmployee.emp_level != 7)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            }

            
            int iSelSevName = int.Parse(DropBlackServerName.SelectedValue);
            int iSelSevNo = 0;
            if (iSelSevName < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "서버를 선택해주세요.");
                return;
            }
            else 
            {
                iSelSevNo = int.Parse(DropBlackServerNo.SelectedValue);
                if (iSelSevNo < 1)
                {
                    CUtilsMessageBox.ShowAlertBox2(this.Page, "서버를 선택해주세요.");
                    return;
                }

            }
            string blackName = txt_black.Text;
            if(blackName.Length < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "유저를 입력해주세요.");
                return;
            }


            CBlackList mBlackList = new CBlackList();
            
            mBlackList.black_server_no = (iSelSevName-1) * 10 + iSelSevNo;
            mBlackList.black_player_name = blackName;
            mBlackList.black_emp_fid = mEmployee.emp_fid;
            mBlackList.black_player_enable = 1;

            if (mBlackList.AddBlackList())
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "성공적으로 등록되었습니다.");
                txt_black.Text = "";
                ViewBlackList();
            }
            
        }

        protected void BtnBlackDelete_Click(object sender, EventArgs e)
        {
            ViewBlackList();
            
            for (int i = 0; i < mArrBlackList.Count; i ++)
            {
                mArrBlackList[i].DeleteBlackListByNo(mArrBlackList[i].black_no);
            }
            ViewBlackList();
        }

        protected void BtnBlackFind_Click(object sender, EventArgs e)
        {
            ViewBlackList();
        }


        protected void BtnUserView_Click(object sender, EventArgs e)
        {
            ViewUserList();
        }

        protected void BtnUserAdd_Click(object sender, EventArgs e)
        {

            //아군리스트 추가

            int iSelectedEmpId = int.Parse(DropUserEmployee.SelectedValue);
            if (iSelectedEmpId < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            }

            CEmployee mEmployee = new CEmployee();
            mEmployee = mEmployee.GetMemberByFid(iSelectedEmpId);
            if (mEmployee == null)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            }
            else if (mEmployee.emp_level != 7)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "매장을 선택해주세요.");
                return;
            }


            int iSelSevName = int.Parse(DropUserServerName.SelectedValue);
            int iSelSevNo = 0;
            if (iSelSevName < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "서버를 선택해주세요.");
                return;
            }
            else
            {
                iSelSevNo = int.Parse(DropUserServerNo.SelectedValue);
                if (iSelSevNo < 1)
                {
                    CUtilsMessageBox.ShowAlertBox2(this.Page, "서버를 선택해주세요.");
                    return;
                }

            }
            string userName = txt_user.Text;
            if (userName.Length < 1)
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "유저를 입력해주세요.");
                return;
            }


            CCharacList mCharacList = new CCharacList();

            mCharacList.charac_server_no = (iSelSevName - 1) * 10 + iSelSevNo;
            mCharacList.charac_player_name = userName;
            mCharacList.charac_emp_fid = mEmployee.emp_fid;
            mCharacList.charac_player_enable = 1;

            if (mCharacList.AddCharacList())
            {
                CUtilsMessageBox.ShowAlertBox2(this.Page, "성공적으로 등록되었습니다.");
                txt_user.Text = "";
                ViewUserList();
            }
        }

        protected void BtnUserDelete_Click(object sender, EventArgs e)
        {
            ViewUserList();

            for (int i = 0; i < mArrUserList.Count; i++)
            {
                mArrUserList[i].DeleteCharacListByNo(mArrUserList[i].charac_no);
            }
            ViewUserList();
        }

        protected void BtnUserFind_Click(object sender, EventArgs e)
        {
            ViewBlackList();
        }
    }
}