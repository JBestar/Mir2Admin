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
    public partial class notice : System.Web.UI.Page
    {
        public CEmployee curEmployee = new CEmployee();
        public CNotice mNotice = new CNotice();
        public int category = 1;

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
                // Lm2
                mNotice = mNotice.GetNoticeByCategory(category);
                notice_contents.Text = mNotice.notice_content;
            }
        }

        protected void OnClickUpdateNotice(object sender, EventArgs e)
        {
            if (curEmployee.emp_level == 10)
            {
                mNotice.notice_author = curEmployee.emp_uid;
                mNotice.notice_content = notice_contents.Text;
                mNotice.notice_cat = category; // 파라다이스 공지사항
                if (mNotice.UpdateNotice())
                {
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, "공지사항이 성과적으로 업뎃되었습니다.", "notice.aspx");
                }
            }
        }
    }
}