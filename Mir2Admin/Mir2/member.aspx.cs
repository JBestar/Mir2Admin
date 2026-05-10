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
    public partial class member : System.Web.UI.Page
    {
        public int category = 1;
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();
        public List<CLm2Member> memberList = new List<CLm2Member>();

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
                //하부 총판, 매장을 찾아서 얻는다.
                DataTable dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, curEmployee.emp_level, false);
                if (curEmployee.emp_level >= 10)
                {
                    dtEmployee.Rows.Add(new object[] { "--전체--", 0 });
                }
                DropDownEmployee.DataSource = dtEmployee;
                DropDownEmployee.DataTextField = "name";
                DropDownEmployee.DataValueField = "value";
                DropDownEmployee.DataBind();
                DropDownEmployee.SelectedValue = "0";
                BindMemberData();
            }
        }
        private void BindMemberData()
        {
            List<int> employeeIds = new List<int>();
            List<CEmployee> EmployeeList = curEmployee.GetEmployeeListByEmployee(category, curEmployee.emp_level, curEmployee.emp_fid);
            if (DropDownEmployee.SelectedValue == "0")
            {
                for (int iEmployee = 0; iEmployee < EmployeeList.Count; iEmployee++)
                    employeeIds.Add(EmployeeList[iEmployee].emp_fid);
            }
            else
            {
                int iSelected = int.Parse(DropDownEmployee.SelectedValue);
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
            mMember = new CLm2Member();
            memberList = mMember.GetAllMemberList(employeeIds, txt_search.Text);

            DataTable dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, 7, true);

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_index", typeof(System.Int32));
            dt.Columns.Add("lbl_employee");
            dt.Columns.Add("lbl_mb_uid");
            dt.Columns.Add("lbl_nickname");
            dt.Columns.Add("lbl_time_join");
            dt.Columns.Add("lbl_time_last");
            dt.Columns.Add("lbl_time_limit");
            dt.Columns.Add("lbl_mb_fid");

            for (int i = 0; i < memberList.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["lbl_index"] = (i + 1).ToString();
                for (int j = 0; j < dtEmployee.Rows.Count; j++)
                {
                    string key = Convert.ToString(dtEmployee.Rows[j].ItemArray[1]);
                    string value = Convert.ToString(dtEmployee.Rows[j].ItemArray[0]);
                    int category_fid = int.Parse(key);

                    if (category_fid == memberList[i].mb_emp_fid)
                    {
                        memberList[i].label_company = value;
                        break;
                    }
                }
                newRow["lbl_employee"] = memberList[i].label_company;
                newRow["lbl_mb_uid"] = memberList[i].mb_uid + ":" + memberList[i].mb_pwd;
                newRow["lbl_nickname"] = memberList[i].mb_nickname;
                newRow["lbl_time_join"] = memberList[i].mb_time_join.ToString("yyyy-MM-dd");
                newRow["lbl_time_last"] = memberList[i].mb_time_last.ToString("yyyy-MM-dd");
                newRow["lbl_time_limit"] = memberList[i].mb_time_limit.AddDays(-1).ToString("yyyy-MM-dd");
                newRow["lbl_mb_fid"] = memberList[i].mb_fid;
                dt.Rows.Add(newRow);
            }
            GridMember.DataSource = dt.DefaultView;
            GridMember.DataBind();
        }
        protected void PageChang(object sender, GridViewPageEventArgs e)
        {
            GridMember.PageIndex = e.NewPageIndex;
            BindMemberData();
        }
        protected void GridMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iRowIndex = GridMember.PageIndex * GridMember.PageSize + e.Row.RowIndex;

                Button btnActive = e.Row.FindControl("BtnActive") as Button;
                btnActive.Text = memberList[iRowIndex].mb_state_active == true ? "승인" : "차단";
                btnActive.ForeColor = memberList[iRowIndex].mb_state_active == true ?
                    Color.FromName("#0000FF") : Color.FromName("#FF0000");

                e.Row.BackColor = Color.FromName("#" + memberList[iRowIndex].mb_bgcolor);
            }
        }
        protected void GridMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String cmdFid = e.CommandArgument.ToString();
            int rowFid = int.Parse(cmdFid);
            mMember = mMember.GetMemberByFid(rowFid);
            if (mMember == null) return;

            switch (e.CommandName)
            {
                case "cmdUpdateActive":    // 승인 | 차단
                    if (mMember.mb_state_active)
                        mMember.mb_state_active = false;
                    else
                        mMember.mb_state_active = true;
                    if (mMember.UpdateMember()) BindMemberData();
                    break;
                case "cmdUpdateChange":    // 수정
                    Response.Redirect("memberEdit.aspx?id=" + cmdFid);
                    break;
                case "cmdUpdateDelete":    // 삭제
                    if (mMember.DeleteMember(rowFid)) BindMemberData();
                    break;

                case "cmdUpdate1Week":    // 1주연장
                    mMember.mb_time_limit = mMember.mb_time_limit.AddDays(6);
                    mMember.UpdateMember();
                    BindMemberData();
                    break;
                case "cmdUpdate2Week":    // 2주연장
                    mMember.mb_time_limit = mMember.mb_time_limit.AddDays(13);
                    mMember.UpdateMember();
                    BindMemberData();
                    break;
                case "cmdUpdateMonth":    // 1달연장
                    mMember.mb_time_limit = mMember.mb_time_limit.AddMonths(1).AddDays(-1);
                    mMember.UpdateMember();
                    BindMemberData();
                    break;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            BindMemberData();
        }

        protected void BtnRegistorMulti_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberMulti.aspx");
        }

        protected void BtnRegistorMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberEdit.aspx");
        }
    }
}