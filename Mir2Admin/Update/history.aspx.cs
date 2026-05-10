using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using Mir2Admin.Models;

namespace Mir2Admin.Update
{
    public partial class history : System.Web.UI.Page
    {
        public CEmployee curEmployee = new CEmployee();
        public CUpdate mUpdate = new CUpdate();

        public List<CUpdate> updateList = new List<CUpdate>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessId"] == null) Session["sessId"] = Session.SessionID;
            curEmployee = CEmployee.CheckLogin(Session["sessId"].ToString());
            if (curEmployee == null)
            {
                Response.Redirect("../index.aspx");
                return;
            }
            else if (curEmployee.emp_level < 11)
            {
                Response.Redirect("../index.aspx");
                return;
            }


            if (!IsPostBack)
            {
                BindUpdateData();
            }
        }

        private void BindUpdateData()
        {
            List<CUpdate> updateList = mUpdate.GetAllUpdateList();

            DataTable dt = new DataTable();
            dt.Columns.Add("lbl_index", typeof(System.Int32));
            dt.Columns.Add("lbl_version");
            dt.Columns.Add("lbl_date");
            dt.Columns.Add("lbl_content");
            dt.Columns.Add("lbl_author");

            for (int i = 0; i < updateList.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["lbl_index"] = (i + 1).ToString();
                newRow["lbl_version"] = updateList[i].update_version;
                //newRow["lbl_path"] = updateList[i].update_path;
                newRow["lbl_date"] = updateList[i].update_date.ToString("yyyy-MM-dd HH:mm");
                newRow["lbl_content"] = updateList[i].update_content;
                newRow["lbl_author"] = updateList[i].update_author;
                dt.Rows.Add(newRow);

            }
            GridUpdate.DataSource = dt.DefaultView;
            GridUpdate.DataBind();
        }

        protected void PageChang(object sender, GridViewPageEventArgs e)
        {
            GridUpdate.PageIndex = e.NewPageIndex;
            BindUpdateData();
        }

        protected void GridUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void GridUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String version = e.CommandArgument.ToString();
            
            switch (e.CommandName)
            {
                case "cmdUpdateDelete":    // 삭제
                    DeleteVersion(version);
                    break;
            }
        }

        public void DeleteVersion(String version)
        {
            //CUpdate update = mUpdate.GetUpdateByVersion(version);

            if (mUpdate.DeleteVersion(version))
            {
                //CUtilsMessageBox.ShowAlertLinkBox(this.Page, "업뎃이 삭제되었습니다.", "history.aspx");

                string extractPath = Server.MapPath("~/Download/") + version;    //해당버젼 폴더

                DirectoryInfo di = new DirectoryInfo(extractPath);

                if (di.Exists)                                          //폴더가 존재하면 삭제
                {
                    di.Delete(true);
                }

                BindUpdateData();

            }
            else
            {
                CUtilsMessageBox.ShowAlertLinkBox(this.Page, "선택된 버전을 삭제할수 없습니다.", "history.aspx");

            }

        }
    }


}