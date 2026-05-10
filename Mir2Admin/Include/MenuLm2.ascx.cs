using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Include
{
    public partial class MenuLm2 : System.Web.UI.UserControl
    {
        public CEmployee curEmployee = new CEmployee();
        public string strBgNotice = "#FFFFFF";
        public string strBgMember = "#FFFFFF";
        public string strBgConnect = "#FFFFFF";
        public string strBgHistory = "#FFFFFF";
        public string strBgBlack = "#FFFFFF";
        public string strBgSetting = "#FFFFFF";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }

            string[] path = Request.CurrentExecutionFilePath.Split('/');
            string strFold = path[path.Length - 1];

            switch (strFold)
            {
                case "notice.aspx": strBgNotice = "#FFFA6B"; break;
                case "member.aspx":
                case "memberMulti.aspx":
                case "memberEdit.aspx": strBgMember = "#FFFA6B"; break;
                case "connection.aspx": strBgConnect = "#FFFA6B"; break;
                case "UserManage.aspx": strBgBlack = "#FFFA6B"; break;
                case "Setting.aspx": strBgSetting = "#FFFA6B"; break;
            }
        }
    }
}