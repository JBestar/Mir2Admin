using System;
using System.Web.UI;

namespace Mir2Admin.Models
{
    public class CUtilsMessageBox
    {
        public CUtilsMessageBox()
        {
        }

        public static void ShowAlertBox(Page page, string msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", sb.ToString(), true);

        }

        public static void ShowAlertBox2(Page page, string msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script>alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');</script>");
            page.Response.Write(sb.ToString());
        }

        public static void ShowAlertLinkBox(Page page, string msg, string url)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            sb.Append("window.location='");
            sb.Append(url);
            sb.Append("';");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showAlert", sb.ToString(), true);
            
        }
    }
}