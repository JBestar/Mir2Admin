using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO.Compression;
using System.IO;
//using System.IO.Compression.FileSystem;
//using Ionic.Zip;


using Mir2Admin.Models;

namespace Mir2Admin.Update
{
    public partial class update : System.Web.UI.Page
    {
        
        public CEmployee curEmployee = new CEmployee();
        
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


            txt_version_last.Text = GetLastVersion();         
        }
        
        public string GetLastVersion()
        {
            CUpdate updateObj = new CUpdate();
            List<CUpdate> updateList = updateObj.GetAllUpdateList();

            string strVersion = "";
            string zipPath = "";
            string strResult = "";
            CUpdate lastUpdate = null;
            if (updateList.Count > 0)
            {
                lastUpdate = updateList.First<CUpdate>();

                strVersion = lastUpdate.update_version;
                zipPath = lastUpdate.update_path;

                strResult = " (최신버전은 '" + strVersion + "' 입니다.)";
            }
            else strResult = " (최신버전을 찾을수 없습니다.)";

            return strResult;
        }
        protected void OnClickUpdate(object sender, EventArgs e)
        {
            if (curEmployee.emp_level >= 11)
            {
                
                try
                { 
                    if (UploadCtrl.HasFiles)
                    {
                        foreach (HttpPostedFile uploadedFile in UploadCtrl.PostedFiles)
                        {
                            
                            string fileName = Server.HtmlEncode(uploadedFile.FileName);

                            // Get the extension of the uploaded file.
                            string extension = System.IO.Path.GetExtension(fileName);

                            if (extension != ".zip")
                            {
                                CUtilsMessageBox.ShowAlertLinkBox(this.Page, "업로드파일을 정확히 선택해주세요.", "update.aspx");

                                return;
                            }
                            if(txt_version.Text.Length <= 0)
                            {
                                CUtilsMessageBox.ShowAlertLinkBox(this.Page, "버전을 입력해주세오.", "update.aspx");

                                return;
                            }
                            
                            string extractPath = Server.MapPath("~/Download/") + txt_version.Text;    //압축풀기할 폴더

                            DirectoryInfo di = new DirectoryInfo(extractPath);

                            if (di.Exists)                                                       //폴더가 이미 존재하면 삭제
                            {
                                di.Delete(true);
                            }

                            di.Create();

                            string zipSourcePath = txt_version.Text + Path.AltDirectorySeparatorChar + uploadedFile.FileName;
                            string zipPath = Server.MapPath("~/Download/") + zipSourcePath;         //zip파일 경로
                            uploadedFile.SaveAs(zipPath);

        
                            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.OpenOrCreate))
                            {                                
                                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                                {                                    
                                    archive.ExtractToDirectory(extractPath);
                                }
                            }

                            bool bRet = false;
                            CUpdate updateModel = new CUpdate();
                            
                            updateModel = updateModel.GetUpdateByVersion(txt_version.Text);
                            if(updateModel != null)
                            {
                                updateModel.update_path = zipSourcePath;
                                updateModel.update_date = DateTime.Now;
                                updateModel.update_content = "";
                                updateModel.update_author = curEmployee.emp_uid;

                                bRet = updateModel.UpdateVersion();

                            }
                            else { 
                            
                                updateModel = new CUpdate();
                                updateModel.update_version = txt_version.Text;
                                updateModel.update_path = zipSourcePath;
                                updateModel.update_date = DateTime.Now;
                                updateModel.update_content = "";
                                updateModel.update_author = curEmployee.emp_uid;

                                bRet = updateModel.AddVersion();
                            }

                            if (bRet)
                                CUtilsMessageBox.ShowAlertLinkBox(this.Page, uploadedFile.FileName + "이 성과적으로 업로드되었습니다.", "history.aspx");

                           else
                                CUtilsMessageBox.ShowAlertLinkBox(this.Page, uploadedFile.FileName + "이 업로드중에 오류가 발생되었습니다.", "update.aspx");


                        }
                    }
                }
                catch (HttpException ex)
                {
                    CUtilsMessageBox.ShowAlertLinkBox(this.Page, UploadCtrl.FileName + "이 업뎃에 실패되었습니다.", "update.aspx");
                    string error_msg = ex.Message;
                    return;
                }
              
            }
        }


        protected void OnClickCancel(object sender, EventArgs e)
        {
            Response.Redirect("update.aspx");
        }
        
    }
}