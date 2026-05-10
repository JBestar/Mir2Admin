using System;
using System.Web;
using System.Net;
using System.Configuration;
using System.Net.Mail;

namespace Mir2Admin.Models
{
    public class CUtilsNetwork
    {
        public CUtilsNetwork()
        {
        }
        public static string GetClientHostname(HttpRequest httpRequest)
        {
            return (Dns.GetHostEntry(httpRequest.ServerVariables["remote_addr"]).HostName);
        }
        public static string GetClientBrowser(System.Web.HttpBrowserCapabilities browser)
        {
            string mClientBrowser = "Browser Capabilities\n"
                + "Type = " + browser.Type + "\n"
                + "Name = " + browser.Browser + "\n"
                + "Version = " + browser.Version + "\n"
                + "Major Version = " + browser.MajorVersion + "\n"
                + "Minor Version = " + browser.MinorVersion + "\n"                
                + "Supports JavaScript Version = " + browser["JavaScriptVersion"] + "\n";
            return mClientBrowser;
        }
        public static string GetClientIpAddress(HttpRequest httpRequest)
        {
            string mClientIpAddress = HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;
            if (mClientIpAddress == "::1") mClientIpAddress = "127.0.0.1";
            return mClientIpAddress;
        }

        /// <summary>
        /// IPv4 문자열에서 앞 세 옥텟까지의 접두어를 만든다 (예 203.203.205.12 → 203.203.205.).
        /// </summary>
        public static bool TryGetIpv4ThreeOctetPrefix(string ipCandidate, out string prefixEndsWithDot)
        {
            prefixEndsWithDot = null;
            if (string.IsNullOrWhiteSpace(ipCandidate))
                return false;
            string[] parts = ipCandidate.Trim().Split('.');
            if (parts.Length != 4)
                return false;
            for (int i = 0; i < 4; i++)
            {
                int n;
                if (!int.TryParse(parts[i].Trim(), out n) || n < 0 || n > 255)
                    return false;
            }
            prefixEndsWithDot = parts[0].Trim() + "." + parts[1].Trim() + "." + parts[2].Trim() + ".";
            return true;
        }
        public static bool SendMail(string toAddress, string firstNm, string lastNm, string empid, string certsess)
        {
            string title = "Online Training Password Reset!";
            string body = "Hi Mr " + firstNm + " " + lastNm + "<br>Please click the link to reset your password: " +
                "<a href='" + ConfigurationManager.ConnectionStrings["webServerHost"].ConnectionString +
                "guest/certEmail.aspx?empid=" + empid + "&certsess=" + certsess + "'>Password Reset</a>";

            bool result = true;

            string from = ConfigurationManager.ConnectionStrings["mailFrom"].ConnectionString;
            string password = ConfigurationManager.ConnectionStrings["mailPass"].ConnectionString;
            string smtpHost = ConfigurationManager.ConnectionStrings["smtpHost"].ConnectionString;
            int smtpPort = Convert.ToInt32(ConfigurationManager.ConnectionStrings["smtpPort"].ConnectionString);

            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(from, password);

            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(toAddress);
                message.From = new MailAddress(from);
                message.Subject = title;
                message.Body = body;
                message.IsBodyHtml = true;
                smtpClient.Send(message);
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}