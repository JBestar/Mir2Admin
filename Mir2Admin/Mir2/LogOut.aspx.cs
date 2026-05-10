using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class LogOut : System.Web.UI.Page
    {
        
        public CLm2Member mMember = new CLm2Member();
        public CLm2Session mSession = new CLm2Session();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            mSession.DeleteSession(Session["sessId"].ToString());
        }
    }
}