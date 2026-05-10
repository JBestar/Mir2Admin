using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;

using System.IO;
using Newtonsoft.Json;
using Mir2Admin.Models;

namespace Mir2Admin.Update
{
    public partial class version : System.Web.UI.Page
    {

        public CLm2Member mMember = new CLm2Member();
        public CLm2Session mSession = new CLm2Session();
        public CUpdate mUpdate = new CUpdate();

        List<Hashtable> mFileList = new List<Hashtable>();

        string m_downDir = "~/Download/";              //서버안의 다운로드 등록부이름

        string m_keyVersion = "version";            //Json 키 
        string m_keyFiles = "files";                //Json 키 
        string m_keyPath = "path";                  //Json 키 
        string m_keyValue = "value";                  //Json 키 

        protected void Page_Load(object sender, EventArgs e)
        {

            Hashtable jsonResponse = new Hashtable();


            List<CUpdate> updateList = mUpdate.GetAllUpdateList();
            string strVersion = "0.0.0.0";            
            CUpdate lastUpdate = null;
            if (updateList.Count > 0)
            {
                lastUpdate = updateList.First<CUpdate>();
                strVersion = lastUpdate.update_version;
                
            }
            else
            {
                jsonResponse.Add(m_keyVersion, strVersion);         //버젼없는 경우
                SendJsonResult(jsonResponse);
            }
            jsonResponse.Add(m_keyVersion, strVersion);             //버젼정보추가

            string strPhPath = Server.MapPath(m_downDir) + strVersion;  //서버의 물리적인 경로

            DirectoryInfo dirInfo = new DirectoryInfo(strPhPath);
            if (!dirInfo.Exists) { //원천내용이 없는 경우
                SendJsonResult(jsonResponse);
            }
            mFileList.Clear();

            using (MD5 md5Hash = MD5.Create())
            {
                FileInfo[] fileInfos = dirInfo.GetFiles();      //루트 디렉토리의 화일정보 얻기
                string dirPath = "";
                AddFilesToList(md5Hash, strVersion, dirPath, fileInfos);

                DirectoryInfo[] dirInfos = dirInfo.GetDirectories();    // Get SubDirectroy
                for (int i = 0; i < dirInfos.Length; i++)
                {
                    dirPath = dirInfos[i].Name + Path.AltDirectorySeparatorChar;

                    AddFilesToList(md5Hash, strVersion, dirPath, dirInfos[i].GetFiles());
                }
            }
            jsonResponse.Add(m_keyFiles, mFileList);       //파일리스트추가

            SendJsonResult(jsonResponse);
        }


        private void SendJsonResult(Hashtable jsonResponse)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string jsonResponseAsString = jsSerializer.Serialize(jsonResponse);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(jsonResponseAsString);
            Response.End();
        }

        public void AddFilesToList(MD5 md5Hash, string version, string dirPath, FileInfo[] files)
        {
            
            Hashtable hashItem = null;
            for(int i = 0; i < files.Length; i ++)
            {
                if (files[i].Exists)
                {
                    string extension = System.IO.Path.GetExtension(files[i].Name);
                    string filePath = "";
                    string md5Value = "";
                    if (extension != ".zip")
                    {
                        
                        hashItem = new Hashtable();
                        hashItem.Add(m_keyPath, dirPath + files[i].Name);

                        //파일 내용을 md5값으로 얻기
                        filePath = version + Path.AltDirectorySeparatorChar + dirPath + files[i].Name;
                        md5Value = GetFileMd5Hash(md5Hash, filePath, files[i].Length);

                        hashItem.Add(m_keyValue, md5Value);

                        mFileList.Add(hashItem);
                    }
                }
            }

        }


        private string GetFileMd5Hash(MD5 md5Hash, string filePath, long length)
        {
            byte[] readBytes = new byte[length];
            FileStream fi = null;
            Boolean bRead = false;
            string errMsg = "";
            try
            {
                string downPath = Server.MapPath("~/Download/");
                string fullPath = downPath + filePath;

                FileInfo fileInfo = new FileInfo(fullPath);
                if (!fileInfo.Exists)
                    return "0";

                fi = new FileStream(fullPath, FileMode.Open);
                int i, index = 0;
                do
                {
                    i = fi.ReadByte();//한바이트를 읽고 그 값을 i에 저장
                    if (i != -1)
                    {
                        readBytes[index] = (byte)i;
                        index++;
                        if (index > length-1)
                            break;
                    }

                } while (i != -1);

                fi.Close();
                bRead = true;
            } catch (Exception ex)
            {
                errMsg = ex.ToString();
                fi.Close();
            }
            if (bRead)
                return GetMd5Hash(md5Hash, readBytes);
            else return "0";
        }

        private string GetMd5Hash(MD5 md5Hash, byte[] inputBytes)
        {
            //입력한 바이트 배렬의 MD5문자렬 반환
            byte[] data = md5Hash.ComputeHash(inputBytes);

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}