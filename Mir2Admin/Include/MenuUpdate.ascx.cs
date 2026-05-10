using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Include
{
    public partial class MenuUpdate : System.Web.UI.UserControl
    {

        public CEmployee curEmployee = new CEmployee();
        public string strBgHistory = "#FFFFFF";
        public string strBgUpdate = "#FFFFFF";
        public string strBgDown = "#FFFFFF";

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
                case "history.aspx": strBgHistory = "#FFFA6B"; break;
                case "update.aspx": strBgUpdate = "#FFFA6B"; break;
                case "download.aspx":  strBgDown = "#FFFA6B"; break;
            }
        }


    }
}