using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using Mir2Admin.Models;
namespace Mir2Admin.Staff
{
    public partial class agency : System.Web.UI.Page
    {

        public int category = 0;
        public CEmployee curEmployee = new CEmployee();
        public CEmployee mAgency = new CEmployee();
        public List<CEmployee> agencyList = new List<CEmployee>();

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

            if (!IsPostBack) BindCompanyData();
        }
        private void BindCompanyData()
        {
            agencyList = curEmployee.GetEmployeeListByEmployee(category, curEmployee.emp_level, curEmployee.emp_fid);
            DataTable dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, 8, false);

            List<CEmployee> tempList = agencyList;
            agencyList = new List<CEmployee>();
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].emp_level == 8)
                {
                    for (int j = 0; j < dtEmployee.Rows.Count; j++)
                    {
                        string key = Convert.ToString(dtEmployee.Rows[j].ItemArray[1]);
                        string value = Convert.ToString(dtEmployee.Rows[j].ItemArray[0]);

                        int category_fid = int.Parse(key);
                        if (category_fid == tempList[i].emp_fid)
                        {
                            tempList[i].label_company = value;
                            break;
                        }
                    }
                    agencyList.Add(tempList[i]);
                }
            }

            DataTable dt = new DataTable();

            dt.Columns.Add("lbl_index", typeof(System.Int32));
            dt.Columns.Add("lbl_mb_uid");
            dt.Columns.Add("lbl_category");
            dt.Columns.Add("lbl_nickname");
            dt.Columns.Add("lbl_time_join");
            dt.Columns.Add("lbl_time_last");

            dt.Columns.Add("lbl_mb_fid");

            for (int i = 0; i < agencyList.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["lbl_index"] = (i + 1).ToString();
                newRow["lbl_mb_uid"] = agencyList[i].emp_uid;
                newRow["lbl_nickname"] = agencyList[i].label_company;
                newRow["lbl_time_join"] = agencyList[i].emp_time_join.ToString("yyyy-MM-dd");
                newRow["lbl_time_last"] = agencyList[i].emp_time_last.ToString("yyyy-MM-dd");

                newRow["lbl_mb_fid"] = agencyList[i].emp_fid;
                dt.Rows.Add(newRow);
            }

            GridCompany.DataSource = dt.DefaultView;
            GridCompany.DataBind();
        }
        protected void PageChang(object sender, GridViewPageEventArgs e)
        {
            GridCompany.PageIndex = e.NewPageIndex;
            BindCompanyData();
        }
        protected void GridCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iRowIndex = GridCompany.PageIndex * GridCompany.PageSize + e.Row.RowIndex;

                Button btnActive = e.Row.FindControl("BtnActive") as Button;
                btnActive.Text = agencyList[iRowIndex].emp_state_active == true ? "승인" : "차단";
                btnActive.ForeColor = agencyList[iRowIndex].emp_state_active == true ?
                    Color.FromName("#0000FF") : Color.FromName("#FF0000");

                e.Row.BackColor = Color.FromName("#" + agencyList[iRowIndex].emp_color);
            }
        }
        protected void GridCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String cmdFid = e.CommandArgument.ToString();
            int rowFid = int.Parse(cmdFid);
            mAgency = mAgency.GetMemberByFid(rowFid);
            if (mAgency == null) return;

            switch (e.CommandName)
            {
                case "cmdUpdateActive":    // 승인 | 차단
                    if (mAgency.emp_level < curEmployee.emp_level)
                    {
                        if (mAgency.emp_state_active)
                            mAgency.emp_state_active = false;
                        else
                            mAgency.emp_state_active = true;
                        mAgency.UpdateEmployee();
                    }
                    BindCompanyData();
                    break;
                case "cmdUpdateChange":    // 수정
                    Response.Redirect("agencyEdit.aspx?id=" + cmdFid);
                    break;
                case "cmdUpdateDelete":    // 삭제
                    if (mAgency.emp_level < curEmployee.emp_level)
                    {
                        if (curEmployee.DeleteAgency(rowFid)) BindCompanyData();
                    }
                    break;
                case "cmdUpdate1LuckyGold":   // 리니지2M
                    if (mAgency.emp_level < curEmployee.emp_level)
                    {
                        if (mAgency.emp_b_app_01_lm2)
                            mAgency.emp_b_app_01_lm2 = false;
                        else
                            mAgency.emp_b_app_01_lm2 = true;
                        mAgency.UpdateEmployee();
                    }
                    BindCompanyData();
                    break;
            }
        }
        protected void BtnRegAgency_Click(object sender, EventArgs e)
        {
            Response.Redirect("agencyEdit.aspx");
        }
    }
}