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
    public partial class Setting : System.Web.UI.Page
    {
        
        public CEmployee curEmployee = new CEmployee();
        public CLm2Member mMember = new CLm2Member();
        public List<CAgent> mAgentList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
            else if (curEmployee.emp_level < 11)
            {
                Response.Redirect("../index.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ViewAgentList();
                ViewBuyerList();
            }
        }

        private void ViewAgentList()
        {
            CAgent mAgent = new CAgent();
            mAgentList = mAgent.GetAllAgent();

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_agent_serverno");
            dt.Columns.Add("lbl_agent_name");
            dt.Columns.Add("lbl_agent_active");
            dt.Columns.Add("lbl_agent_index");

            for (int i = 0; i < mAgentList.Count; i++)
            {
                DataRow newRow = dt.NewRow();


                newRow["lbl_agent_serverno"] = mAgentList[i].server_no;
                newRow["lbl_agent_name"] = mAgentList[i].agent_id;
                newRow["lbl_agent_active"] = mAgentList[i].active_state;
                newRow["lbl_agent_index"] = i+1;

                dt.Rows.Add(newRow);
            }

            GridAgentList.DataSource = dt.DefaultView;
            GridAgentList.DataBind();

        }

        private void ViewBuyerList()
        {
            CBuyer mBuyer = new CBuyer();
            List<CBuyer> buyerList = mBuyer.GetAllBuyer();

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_buyer_serverno");
            dt.Columns.Add("lbl_buyer_banker");
            dt.Columns.Add("lbl_buyer_name");
            dt.Columns.Add("lbl_buyer_itemid");
            dt.Columns.Add("lbl_buyer_saleid");
            dt.Columns.Add("lbl_buyer_length");
            dt.Columns.Add("lbl_buyer_date");
            dt.Columns.Add("lbl_buyer_regst");
            dt.Columns.Add("lbl_buyer_buyst");

            int time_t = 0;
            DateTime dtSpan ; 
            for (int i = 0; i < buyerList.Count; i++)
            {
                DataRow newRow = dt.NewRow();


                newRow["lbl_buyer_serverno"] = buyerList[i].server_no;
                newRow["lbl_buyer_banker"] = buyerList[i].banker_id;
                newRow["lbl_buyer_name"] = buyerList[i].buyer_id;
                newRow["lbl_buyer_itemid"] = buyerList[i].item_id;
                newRow["lbl_buyer_saleid"] = buyerList[i].sale_id;
                newRow["lbl_buyer_length"] = buyerList[i].item_length;

                if (buyerList[i].reg_date.Length > 0)
                {
                    time_t = int.Parse(buyerList[i].reg_date);
                    dtSpan = new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(time_t);

                    newRow["lbl_buyer_date"] = dtSpan.ToString("yyyy-MM-dd hh:mm:ss");
                }
                else newRow["lbl_buyer_date"] = "";
                newRow["lbl_buyer_regst"] = buyerList[i].reg_state;
                newRow["lbl_buyer_buyst"] = buyerList[i].buy_state;
                
                dt.Rows.Add(newRow);
            }

            GridBuyerList.DataSource = dt.DefaultView;
            GridBuyerList.DataBind();
        }


        public void DeleteAgent(string tIdx)
        {

            int idx = 0;
            if (!int.TryParse(tIdx, out idx))
                return;
            CAgent mAgent = new CAgent();
            mAgentList = mAgent.GetAllAgent();

            if (idx < 1 || mAgentList.Count < idx)
                return;

            CAgent agent = mAgentList[idx-1];
            CBuyer buyer = new CBuyer();
            if (buyer.DeleteAllBuyer(agent.emp_fid, agent.server_no))
               agent.DeleteAgent(agent.emp_fid, agent.server_no);

            ViewAgentList();
            ViewBuyerList();

        }
        protected void GridAgentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string tIdx = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "cmdUpdateDelete":    // 삭제
                    DeleteAgent(tIdx);
                    break;
            }
        }

        protected void GridAgentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void AgentListChange(object sender, GridViewPageEventArgs e)
        {
            GridAgentList.PageIndex = e.NewPageIndex;
            ViewAgentList();
        }

        protected void GridBuyerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            /*
            switch (e.CommandName)
            {

            }
            */
        }

        protected void GridBuyerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void BuyerListChange(object sender, GridViewPageEventArgs e)
        {
            GridBuyerList.PageIndex = e.NewPageIndex;
            ViewBuyerList();
        }


    }
}