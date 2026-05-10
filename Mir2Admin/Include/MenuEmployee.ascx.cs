using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Include
{
    public partial class MenuEmployee : System.Web.UI.UserControl
    {

        public CEmployee curEmployee = new CEmployee();
        public string strBgCompany = "#FFFFFF";
        public string strBgAgency = "#FFFFFF";
        public string strBgEmployee = "#FFFFFF";

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
                case "company.aspx":
                case "companyEdit.aspx": strBgCompany = "#FFFA6B"; break;
                case "agency.aspx":
                case "agencyEdit.aspx": strBgAgency = "#FFFA6B"; break;
                case "employee.aspx":
                case "employeeEdit.aspx": strBgEmployee = "#FFFA6B"; break;
            }
        }
    }
}