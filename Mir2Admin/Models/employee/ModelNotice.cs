using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CNotice
    {
        CTblNotice tblNotice = new CTblNotice();

        public int notice_cat;
        public DateTime notice_updated;
        public string notice_content;
        public string notice_author;

        public CNotice()
        {
        }
        public Boolean UpdateNotice()
        {
            return tblNotice.UpdateTblNotice(this);
        }
        public CNotice GetNoticeByCategory(int notice_cat)
        {
            return tblNotice.GetNoticeByCat(notice_cat);
        }


    }
}
