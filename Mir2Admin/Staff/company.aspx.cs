using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

using Mir2Admin.Models;

namespace Mir2Admin.Staff
{
    public partial class company : System.Web.UI.Page
    {

        public int category = 0;
        public CEmployee curEmployee = new CEmployee();
        public CEmployee mCompany = new CEmployee();
        public List<CEmployee> companyList = new List<CEmployee>();

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



            if (!IsPostBack) BindCompanyData();
        }
        private void BindCompanyData()
        {
            companyList = curEmployee.GetEmployeeListByEmployee(category, curEmployee.emp_level, curEmployee.emp_fid);

            List<CEmployee> tempList = companyList;
            companyList = new List<CEmployee>();
            for (int i = 0; i < tempList.Count; i++) if (tempList[i].emp_level == 9) companyList.Add(tempList[i]);

            DataTable dt = new DataTable();

            dt.Columns.Add("lbl_index", typeof(System.Int32));
            dt.Columns.Add("lbl_mb_uid");
            dt.Columns.Add("lbl_nickname");
            dt.Columns.Add("lbl_time_join");
            dt.Columns.Add("lbl_time_last");

            dt.Columns.Add("lbl_mb_fid");

            for (int i = 0; i < companyList.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["lbl_index"] = (i + 1).ToString();
                newRow["lbl_mb_uid"] = companyList[i].emp_uid;
                newRow["lbl_nickname"] = companyList[i].emp_nickname;
                newRow["lbl_time_join"] = companyList[i].emp_time_join.ToString("yyyy-MM-dd");
                newRow["lbl_time_last"] = companyList[i].emp_time_last.ToString("yyyy-MM-dd");

                newRow["lbl_mb_fid"] = companyList[i].emp_fid;
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
                btnActive.Text = companyList[iRowIndex].emp_state_active == true ? "승인" : "차단";
                btnActive.ForeColor = companyList[iRowIndex].emp_state_active == true ?
                    Color.FromName("#0000FF") : Color.FromName("#FF0000");

//                 Button Btn1LuckyGold = e.Row.FindControl("Btn1LuckyGold") as Button;
//                 Button Btn2LuckyPal = e.Row.FindControl("Btn2LuckyPal") as Button;
//                 Button Btn3Cross = e.Row.FindControl("Btn3Cross") as Button;
//                 Button Btn4Mpowerball = e.Row.FindControl("Btn4Mpowerball") as Button;
//                 Button Btn5Paradais = e.Row.FindControl("Btn5Paradais") as Button;
//                 Button Btn6YPowerball = e.Row.FindControl("Btn6YPowerball") as Button;

//                 // 럭키골드존
//                 Btn1LuckyGold.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_01_luckygold) ? true : false;
//                 Btn1LuckyGold.ForeColor = (companyList[iRowIndex].emp_b_app_01_luckygold) ? Color.FromName("#0000FF") : Color.FromName("#000000");
// 
//                 // 럭키팔팔
//                 Btn2LuckyPal.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_02_luckypal) ? true : false;
//                 Btn2LuckyPal.ForeColor = (companyList[iRowIndex].emp_b_app_02_luckypal) ? Color.FromName("#0000FF") : Color.FromName("#000000");
// 
//                 // 조합베팅
//                 Btn3Cross.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_03_cross) ? true : false;
//                 Btn3Cross.ForeColor = (companyList[iRowIndex].emp_b_app_03_cross) ? Color.FromName("#0000FF") : Color.FromName("#000000");
// 
//                 // 멀티파워볼
//                 Btn4Mpowerball.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_04_mpowerball) ? true : false;
//                 Btn4Mpowerball.ForeColor = (companyList[iRowIndex].emp_b_app_04_mpowerball) ? Color.FromName("#0000FF") : Color.FromName("#000000");
// 
//                 // Q파라다이스
//                 Btn5Paradais.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_05_paradais) ? true : false;
//                 Btn5Paradais.ForeColor = (companyList[iRowIndex].emp_b_app_05_paradais) ? Color.FromName("#0000FF") : Color.FromName("#000000");
// 
//                 // Y파워볼
//                 Btn6YPowerball.Visible = (curEmployee.emp_level >= 9 || curEmployee.emp_b_app_06_ypowerball) ? true : false;
//                 Btn6YPowerball.ForeColor = (companyList[iRowIndex].emp_b_app_06_ypowerball) ? Color.FromName("#0000FF") : Color.FromName("#000000");

                e.Row.BackColor = Color.FromName("#" + companyList[iRowIndex].emp_color);
            }
        }
        protected void GridCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String cmdFid = e.CommandArgument.ToString();
            int rowFid = int.Parse(cmdFid);
            mCompany = mCompany.GetMemberByFid(rowFid);
            if (mCompany == null) return;

            switch (e.CommandName)
            {
                case "cmdUpdateActive":    // 승인 | 차단
                    if (mCompany.emp_level < curEmployee.emp_level)
                    {
                        if (mCompany.emp_state_active)
                            mCompany.emp_state_active = false;
                        else
                            mCompany.emp_state_active = true;
                        mCompany.UpdateEmployee();
                    }
                    BindCompanyData();
                    break;
                case "cmdUpdateChange":    // 수정
                    Response.Redirect("companyEdit.aspx?id=" + cmdFid);
                    break;
                case "cmdUpdateDelete":    // 삭제
                    if (mCompany.emp_level < curEmployee.emp_level)
                    {
                        if (curEmployee.DeleteCompany(rowFid)) BindCompanyData();
                    }
                    break;                   
            }
        }
        protected void BtnRegCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("companyEdit.aspx");
        }

    }
}