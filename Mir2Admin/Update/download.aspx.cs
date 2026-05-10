using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

using System.IO;
using Newtonsoft.Json;
using Mir2Admin.Models;

namespace Mir2Admin.Update
{
    public partial class download : System.Web.UI.Page
    {

        public CLm2Member mMember = new CLm2Member();
        public CLm2Session mSession = new CLm2Session();
        public CUpdate mUpdate = new CUpdate();
  
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            //mSession.DeleteSession(Session["sessId"].ToString());
            
            List<CUpdate> updateList = mUpdate.GetAllUpdateList();

            string strVersion = "";
            string zipSource = "";

            CUpdate lastUpdate = null;
            if (updateList.Count > 0)
            {
                lastUpdate = updateList.First<CUpdate>();
                strVersion = lastUpdate.update_version;
                zipSource = lastUpdate.update_path;
            }
            else return;
            
            string zipPath = Server.MapPath("~/Download/") + zipSource;

            //Download zip File
            FileInfo fileInfo = new FileInfo(zipPath);

            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.TransmitFile(fileInfo.FullName);
                Response.End();
            } else CUtilsMessageBox.ShowAlertLinkBox(this.Page,"최신버전을 찾을수 없습니다.", "history.aspx");


        }        

    }
}