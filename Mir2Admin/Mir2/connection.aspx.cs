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
    public partial class connection : System.Web.UI.Page
    {
        public int category = 1;
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();
        public CLm2Session mSession = new CLm2Session();
        public List<CLm2Member> memberList = new List<CLm2Member>();
        public List<CLm2Session> sessionList = new List<CLm2Session>();
        public List<CLm2Member> connectionList = new List<CLm2Member>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }            
//             else if (curEmployee.emp_level < 10)
//             {
                DropDownEmployee.Visible = true;
                Button1.Visible = true;
                //Button2.Visible = true;   Excel출력
//            }

            DataTable dtEmployee;
            if (!IsPostBack)
            {
                dtEmployee = curEmployee.BindEmployeeDropDownList(category, curEmployee.emp_level, curEmployee.emp_fid, curEmployee.emp_level, false);
                if (curEmployee.emp_level >= 10)
                {
                    dtEmployee.Rows.Add(new object[] { "--전체--", 0 });
                }
                DropDownEmployee.DataSource = dtEmployee;
                DropDownEmployee.DataTextField = "name";
                DropDownEmployee.DataValueField = "value";
                DropDownEmployee.DataBind();
                DropDownEmployee.SelectedValue = "0";

                //initDropDownList();

                BindData();
            }
        }


        private void initDropDownList()
        {
           
        }
        private void BindData()
        {
            List<int> employeeIds = new List<int>();
            List<CEmployee> EmployeeList = curEmployee.GetEmployeeList();
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
            memberList = mMember.GetAllMemberList(employeeIds, "");
            List<string> clientids = new List<string>();
            for (int i = 0; i < memberList.Count; i++) clientids.Add(memberList[i].mb_uid);
            
            sessionList.Clear();

            string serverName = "";

            //int iSelSevName = int.Parse(DropDownServerName.SelectedValue);
            
            string characName = txt_search.Text;

            // 세션목록
            List<CLm2Session> all_sessionList = mSession.GetSessionList(serverName, characName);
            for (int i = 0; i < all_sessionList.Count; i++)
            {
                if (clientids.Contains(all_sessionList[i].sess_mb_uid))
                {
                    sessionList.Add(all_sessionList[i]);
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_index", typeof(System.Int32));
            dt.Columns.Add("lbl_employee");
            dt.Columns.Add("lbl_mb_uid");
            dt.Columns.Add("lbl_nickname");
            dt.Columns.Add("lbl_mb_vip");
            dt.Columns.Add("lbl_game_server");
            dt.Columns.Add("lbl_game_name");
            dt.Columns.Add("lbl_auto_running");
            dt.Columns.Add("lbl_game_level");
            dt.Columns.Add("lbl_game_locale");
            dt.Columns.Add("lbl_game_money1");
            dt.Columns.Add("lbl_game_money2");
            dt.Columns.Add("lbl_game_money3");
            dt.Columns.Add("lbl_game_money4");
            dt.Columns.Add("lbl_time_begin");
            dt.Columns.Add("lbl_time_last");
            dt.Columns.Add("lbl_app_title");
            dt.Columns.Add("lbl_app_version");
            dt.Columns.Add("lbl_pub_addr");

            for (int i = 0; i < sessionList.Count; i++)
            {
                CLm2Member tmpMemberInfo = new CLm2Member();
                for (int j = 0; j < memberList.Count; j++)
                {
                    if (sessionList[i].sess_mb_uid == memberList[j].mb_uid)
                    {
                        tmpMemberInfo = memberList[j];
                        break;
                    }
                }
                sessionList[i].mb_backcolor = tmpMemberInfo.mb_bgcolor;

                DataRow newRow = dt.NewRow();
                newRow["lbl_index"] = (i + 1).ToString();
                newRow["lbl_employee"] = tmpMemberInfo.label_company;
                newRow["lbl_mb_uid"] = tmpMemberInfo.mb_uid;
                newRow["lbl_nickname"] = tmpMemberInfo.mb_nickname;
                if (tmpMemberInfo.mb_vip) newRow["lbl_mb_vip"] = " (VIP)";

                newRow["lbl_game_server"] = sessionList[i].sess_game_server;
                newRow["lbl_game_name"] = sessionList[i].sess_game_name;
                if (sessionList[i].sess_game_level > 0)
                    newRow["lbl_game_level"] = sessionList[i].sess_game_level + "레벨";
                else newRow["lbl_game_level"] = "";
                newRow["lbl_game_locale"] = sessionList[i].sess_game_locale;
                newRow["lbl_auto_running"] = sessionList[i].sess_auto_running == 1 ? "자동사냥" : "정지";
                newRow["lbl_game_money1"] = "골드: " + sessionList[i].sess_game_money1;
                newRow["lbl_game_money2"] = "환: " + sessionList[i].sess_game_money2;
                newRow["lbl_time_begin"] = "시작: "+ sessionList[i].sess_time_begin.ToString("MM-dd HH:mm");
                newRow["lbl_time_last"] = "최신: " + sessionList[i].sess_time_last.ToString("MM-dd HH:mm");
                newRow["lbl_app_title"] = sessionList[i].sess_app_title;
                newRow["lbl_app_version"] = sessionList[i].sess_app_version;
                
                newRow["lbl_pub_addr"] = sessionList[i].sess_pub_addr;
                
                dt.Rows.Add(newRow);
            }

            GridConnection.DataSource = dt.DefaultView;
            GridConnection.DataBind();

            if (memberList.Count > 0)
            {
                Lbl_ToTal.Text = "";
            }
            else Lbl_ToTal.Text = "실행중인 오토가 없습니다..";
        }
        
        protected void PageChang(object sender, GridViewPageEventArgs e)
        {
            GridConnection.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void GridConnection_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridConnection_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Excel.aspx");
        }
    }
}