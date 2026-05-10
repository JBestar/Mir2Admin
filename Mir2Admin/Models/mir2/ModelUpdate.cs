using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mir2Admin.Models
{
    public class CUpdate
    {
        CTblUpdate tblUpdate = new CTblUpdate();

        public int update_no;
        public string update_version;
        public string update_path;
        public DateTime update_date;
        public string update_content;
        public string update_author;

        public CUpdate()
        {
            update_version="";
            update_path = "";
            update_date = DateTime.Now;
            update_content = "";
            update_author = "";

        }
    public Boolean UpdateVersion()
        {
            return tblUpdate.UpdateTblUpdate(this);
        }
        public CUpdate GetUpdateByVersion(string strVersion)
        {
            return tblUpdate.GetUpdateByVersion(strVersion);
        }

        public Boolean AddVersion()
        {
            return tblUpdate.AddTblUpdate(this);
        }

        public Boolean DeleteVersion(string strVersion)
        {
            return tblUpdate.DeleteVersion(strVersion);
        }

        public List<CUpdate> GetAllUpdateList()
        {
            return tblUpdate.GetAllUpdateList();
        }
    }
}
