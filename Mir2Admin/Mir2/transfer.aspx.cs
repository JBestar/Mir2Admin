using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class transfer : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {
            Hashtable jsonResponse = new Hashtable();   //Json 응답
            int iResult = 0;

            if (Request["type"] != null)
            {
                string reqType = Request["type"].ToString();
                if (reqType.Equals("get_money"))        //매장내 다이야정보
                {
                    iResult = GetMoneyResponse(ref jsonResponse);
                }
                else if (reqType.Equals("reg_agent"))   //에이전트 추가 및 변경 
                {
                    iResult = RegAgentResponse();
                }
                else if (reqType.Equals("delete_agent"))    //에이전트 삭제            
                {
                    iResult = DeleteAgentResponse();
                }
                else if (reqType.Equals("get_agent"))       //에이전트 검색
                {
                    iResult = GetAgentResponse(ref jsonResponse);
                }
                else if (reqType.Equals("active_agent"))    //에이전트 활성화
                {
                    iResult = ActiveAgentResponse();
                }
                else if (reqType.Equals("add_buyer"))       //구매자 등록
                {
                    iResult = AddBuyerResponse();
                }
                else if (reqType.Equals("add_buyer"))       //구매자 전체삭제
                {
                    iResult = DeleteAllBuyerResponse();
                }
                else if (reqType.Equals("reg_item"))        //구매자 아이템등록
                {
                    iResult = RegItemResponse();
                }
                else if (reqType.Equals("buy_item"))        //구매자 아이템구매
                {
                    iResult = BuyItemResponse();
                }
                else if (reqType.Equals("deleteall_buyer")) //구매자 리스트 전체삭제
                {
                    iResult = DeleteAllBuyerResponse();
                }
                else if (reqType.Equals("fetchall_buyer"))    //구매자 리스트 전체얻기
                {
                    iResult = FetchAllBuyerResponse(ref jsonResponse);
                }
                else if (reqType.Equals("getall_buyer"))       //구매자 리스트 전체얻기
                {
                    iResult = GetAllBuyerResponse(ref jsonResponse);
                }
                else if (reqType.Equals("get_buyer"))       //구매자 검색
                {
                    iResult = GetBuyerResponse(ref jsonResponse);
                } else
                {
                    iResult = 0;
                }


            } else
            {
                iResult = 0;
            }
            //결과
            jsonResponse.Add("result", iResult.ToString());
            SendJsonResult(jsonResponse);
        }
        
        private void SendJsonResult(Hashtable hashTable)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string strJsonResponse = jsSerializer.Serialize(hashTable);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(strJsonResponse);
            Response.End();
        }

        private int GetMoneyResponse(ref Hashtable jsonResponse)
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["user_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["user_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;
                
            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            List<Hashtable> dataList = new List<Hashtable>();
            Hashtable hashItem = null;

            List<CLm2Session> sessionList = session.GetSessionListByServer(session.sess_emp_fid, iServerNo);
            for (int i = 0; i < sessionList.Count; i++)
            {
                hashItem = new Hashtable();
                hashItem.Add("user_id", sessionList[i].sess_game_name);
                hashItem.Add("amount", sessionList[i].sess_game_money2.ToString());
                dataList.Add(hashItem);
            }

            jsonResponse.Add("data", dataList);
            jsonResponse.Add("server_no", iServerNo.ToString());
            iResult = 1;
            
            return iResult;

        }

        private int RegAgentResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["agent_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strAgentId = Request["agent_id"].ToString();

            if (iServerNo < 1 || strAgentId.Length < 1)
                return iResult;
            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strAgentId);
            if (session == null)
                return iResult;

            CAgent agent = new CAgent();
            agent = agent.GetAgentByServer(session.sess_emp_fid, iServerNo);
            if(agent == null)
            {
                agent = new CAgent();
                agent.emp_fid = session.sess_emp_fid;
                agent.server_no = iServerNo;
                agent.agent_id = strAgentId;
                agent.active_state = 0;
                if(agent.AddAgent())
                    iResult = 1;

            } else
            {
                agent = new CAgent();
                agent.emp_fid = session.sess_emp_fid;
                agent.server_no = iServerNo;
                agent.agent_id = strAgentId;
                agent.active_state = 0;                       //Active상태 
                if (agent.UpdateAgent())
                    iResult = 1;
            }
            

            return iResult;

        }
        
        private int DeleteAgentResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["user_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["user_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;        

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;
                    
            CAgent agent = new CAgent();
            agent = agent.GetAgentByServer(session.sess_emp_fid, iServerNo);
            if (agent == null)
            {
                iResult = 1;
            }
            else
            {
                if (agent.DeleteAgent(session.sess_emp_fid, iServerNo))
                    iResult = 1;
            }
            
            return iResult;

        }


        private int GetAgentResponse(ref Hashtable jsonResponse)
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["user_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["user_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            List<Hashtable> dataList = new List<Hashtable>();
            Hashtable hashItem = null;

            CAgent agent = new CAgent();
            agent = agent.GetAgentByServer(session.sess_emp_fid, iServerNo);

            if(agent != null) { 
                hashItem = new Hashtable();
                hashItem.Add("agent_id", agent.agent_id);
                hashItem.Add("active", agent.active_state.ToString());
                dataList.Add(hashItem);
            }
            jsonResponse.Add("data", dataList);
            jsonResponse.Add("server_no", iServerNo.ToString());
            iResult = 1;
            
            return iResult;

        }

        private int ActiveAgentResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["agent_id"] == null || Request["active"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strAgentId = Request["agent_id"].ToString();
            int iActive = int.Parse(Request["active"].ToString());

            if (iServerNo < 1 || strAgentId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strAgentId);
            if (session == null)
                return iResult;

            CAgent agent = new CAgent();
            agent = agent.GetAgentByServer(session.sess_emp_fid, iServerNo);
            if (agent == null)
            {
                iResult = 0;
            }
            else
            {
                agent = new CAgent();
                agent.emp_fid = session.sess_emp_fid;
                agent.server_no = iServerNo;
                agent.agent_id = strAgentId;
                agent.active_state = iActive;
                if (agent.UpdateAgent())
                    iResult = 1;
            }
            
            return iResult;
        }


        private int AddBuyerResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["banker"] == null || Request["buyers"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strBankerId = Request["banker"].ToString();
            string strBuyers = Request["buyers"].ToString();

            if (iServerNo < 1 || strBankerId.Length < 1 && strBuyers.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strBankerId);
            if (session == null)
                return iResult;

            string[] arrBuyer = strBuyers.Split('@');

            CBuyer buyer = null;
            CBuyer mBuyer = new CBuyer();
            foreach (string strBuyer in arrBuyer)
            {
                if (strBuyer.Length < 1)
                    continue;
                buyer = mBuyer.GetBuyerByBuyer(iServerNo, strBuyer);
                if (buyer == null)
                {
                    buyer = new CBuyer();
                    buyer.emp_fid = session.sess_emp_fid;
                    buyer.server_no = iServerNo;
                    buyer.banker_id = strBankerId;
                    buyer.buyer_id = strBuyer;
                    buyer.AddBuyer();
                }
                else
                {
                    buyer = new CBuyer();
                    buyer.emp_fid = session.sess_emp_fid;
                    buyer.server_no = iServerNo;
                    buyer.banker_id = strBankerId;
                    buyer.buyer_id = strBuyer;
                    buyer.UpdateBuyer();
                }
            }
            iResult = 1;

            
            return iResult;

        }


        private int DeleteAllBuyerResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["agent_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strAgentId = Request["agent_id"].ToString();

            if (iServerNo < 1 || strAgentId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strAgentId);
            if (session == null)
                return iResult;
            CBuyer mBuyer = new CBuyer();
            if (mBuyer.DeleteAllBuyer(session.sess_emp_fid, iServerNo))
                iResult = 1;

            return iResult;

        }

        private int RegItemResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["user_id"] == null || Request["item_id"] == null
                || Request["sale_id"] == null )
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["user_id"].ToString();
            
            if (iServerNo < 1 || strUserId.Length < 1 )
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;
            
            CBuyer buyer = null;
            CBuyer mBuyer = new CBuyer();

            buyer = mBuyer.GetBuyerByBuyer(iServerNo, strUserId);
            if (buyer != null)
            {
                buyer.emp_fid = session.sess_emp_fid;
                buyer.server_no = iServerNo;
                buyer.item_id = Request["item_id"].ToString();
                buyer.sale_id = Request["sale_id"].ToString();

                if (Request["reg_date"] == null) buyer.reg_date = "";
                else buyer.reg_date = Request["reg_date"].ToString();

                if (Request["item_length"] == null) buyer.item_length = 0;
                else buyer.item_length = int.Parse(Request["item_length"].ToString());

                buyer.reg_state = 1;
                buyer.buy_state = 0;

                if (buyer.UpdateBuyer())
                    iResult = 1;
            }
            

            return iResult;

        }

        private int BuyItemResponse()
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["user_id"] == null )
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["user_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            CBuyer buyer = null;
            CBuyer mBuyer = new CBuyer();

            buyer = mBuyer.GetBuyerByBuyer(iServerNo, strUserId);
            if (buyer != null)
            {
                
                buyer.buy_state = 1;

                if (buyer.UpdateBuyer())
                    iResult = 1;
            }


            return iResult;

        }

        private int GetAllBuyerResponse(ref Hashtable jsonResponse)
        {
            int iResult = 0;

            if (Request["server_no"] == null || Request["banker_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["banker_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            List<Hashtable> dataList = new List<Hashtable>();
            Hashtable hashItem = null;

            CBuyer buyer = new CBuyer();
            List<CBuyer> buyerList = buyer.GetAllBuyerByBanker(iServerNo, strUserId);
            for (int i = 0; i < buyerList.Count; i++)
            {  
           
                hashItem = new Hashtable();
                hashItem.Add("buyer_id", buyerList[i].buyer_id);
                hashItem.Add("banker_id", buyerList[i].banker_id);
                hashItem.Add("item_id", buyerList[i].item_id);
                hashItem.Add("sale_id", buyerList[i].sale_id);
                hashItem.Add("reg_date", buyerList[i].reg_date);
                hashItem.Add("item_length", buyerList[i].item_length.ToString());
                hashItem.Add("reg_state", buyerList[i].reg_state.ToString());
                hashItem.Add("buy_state", buyerList[i].buy_state.ToString());

                dataList.Add(hashItem);
            }
            jsonResponse.Add("data", dataList);
            jsonResponse.Add("server_no", iServerNo.ToString());
            iResult = 1;
            

            return iResult;
        }


        private int FetchAllBuyerResponse(ref Hashtable jsonResponse)
        {
            int iResult = 0;

            if (Request["server_no"] == null || Request["agent_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["agent_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            CAgent agent = new CAgent();
            agent = agent.GetAgentByServer(session.sess_emp_fid, iServerNo);
            if (agent == null)
                return iResult;
            else if (!agent.agent_id.Equals(strUserId))
                return iResult;

            List<Hashtable> dataList = new List<Hashtable>();
            Hashtable hashItem = null;

            CBuyer buyer = new CBuyer();
            List<CBuyer> buyerList = buyer.GetAllBuyerByServer(session.sess_emp_fid, iServerNo);
            for (int i = 0; i < buyerList.Count; i++)
            {

                hashItem = new Hashtable();
                hashItem.Add("buyer_id", buyerList[i].buyer_id);
                hashItem.Add("banker_id", buyerList[i].banker_id);
                hashItem.Add("item_id", buyerList[i].item_id);
                hashItem.Add("sale_id", buyerList[i].sale_id);
                hashItem.Add("reg_date", buyerList[i].reg_date);
                hashItem.Add("item_length", buyerList[i].item_length.ToString());
                hashItem.Add("reg_state", buyerList[i].reg_state.ToString());
                hashItem.Add("buy_state", buyerList[i].buy_state.ToString());

                dataList.Add(hashItem);
            }
            jsonResponse.Add("data", dataList);
            jsonResponse.Add("server_no", iServerNo.ToString());
            iResult = 1;


            return iResult;
        }




        private int GetBuyerResponse(ref Hashtable jsonResponse)
        {
            int iResult = 0;
            if (Request["server_no"] == null || Request["buyer_id"] == null)
                return iResult;

            int iServerNo = int.Parse(Request["server_no"].ToString());
            string strUserId = Request["buyer_id"].ToString();

            if (iServerNo < 1 || strUserId.Length < 1)
                return iResult;

            CLm2Session session = new CLm2Session();
            session = session.GetSessionByGame(iServerNo, strUserId);
            if (session == null)
                return iResult;

            List<Hashtable> dataList = new List<Hashtable>();
            Hashtable hashItem = null;

            CBuyer buyer = new CBuyer();
            buyer = buyer.GetBuyerByBuyer(iServerNo, strUserId);
            if (buyer != null)
            {
                hashItem = new Hashtable();
                hashItem.Add("buyer_id", buyer.buyer_id);
                hashItem.Add("banker_id", buyer.banker_id);
                hashItem.Add("item_id", buyer.item_id);
                hashItem.Add("sale_id", buyer.sale_id);
                hashItem.Add("reg_date", buyer.reg_date);
                hashItem.Add("item_length", buyer.item_length.ToString());
                hashItem.Add("reg_state", buyer.reg_state.ToString());
                hashItem.Add("buy_state", buyer.buy_state.ToString());

                dataList.Add(hashItem);
            }
            jsonResponse.Add("data", dataList);
            jsonResponse.Add("server_no", iServerNo.ToString());
            iResult = 1;

            return iResult;

        }


    }
}