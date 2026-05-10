using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Mir2Admin.Models;

namespace Mir2Admin.Mir2
{
    public partial class GetNotice : System.Web.UI.Page
    {

        public CNotice mNotice = new CNotice();
        public int category = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            mNotice = mNotice.GetNoticeByCategory(category);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlElement notice_title = xmlDoc.CreateElement("notice");
            notice_title.InnerText = "notice";
            xmlDoc.AppendChild(notice_title);

            XmlElement notice_updated = xmlDoc.CreateElement("updated");
            notice_updated.InnerText = mNotice.notice_updated.ToString("yyyy-MM-dd hh:mm:ss");
            xmlDoc.DocumentElement.AppendChild(notice_updated);

            XmlElement notice_content = xmlDoc.CreateElement("content");
            String strNoticeContents = mNotice.notice_content;
            strNoticeContents = strNoticeContents.Replace("\r\n", "@@");
            notice_content.InnerText = strNoticeContents;
            xmlDoc.DocumentElement.AppendChild(notice_content);

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/xml";
            xmlDoc.Save(Response.Output);
        }
    }
}