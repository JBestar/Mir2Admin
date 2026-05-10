using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Mir2Admin.Models
{
    public class CutilsDate
    {
        public CutilsDate()
        {
        }
        public static DateTime GetDateTime(string dateStr)
        {
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.Parse(dateStr);
            }
            catch
            {
                string tmp = "2012-01-01";
                dt = DateTime.ParseExact(tmp, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture);
            }
            return dt;
        }
        public static string GetDateStr(DateTime dt)
        {
            string dtStr = dt.ToString("yyyy-MM-dd");
            return dtStr;
        }
        public static int GetDateDiff(DateTime dt)
        {
            //string dtStr = dt.ToString("yyyy-MM-dd");
            //TimeSpan timespan = (DateTime.Now - new DateTime(2011, 1, 1));
            TimeSpan timespan = (dt - new DateTime(2015, 1, 1));
            return timespan.Days;
        }
        public static DateTime GetLastDayOfMonth(int year, int month)
        {
            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);
            return lastDay;
        }
        public static string GetMonthStr(int month)
        {
            string monthStr = "";
            switch (month)
            {
                case 1: monthStr = "January"; break;
                case 2: monthStr = "February"; break;
                case 3: monthStr = "March"; break;
                case 4: monthStr = "April"; break;
                case 5: monthStr = "May"; break;
                case 6: monthStr = "June"; break;
                case 7: monthStr = "July"; break;
                case 8: monthStr = "August"; break;
                case 9: monthStr = "September"; break;
                case 10: monthStr = "October"; break;
                case 11: monthStr = "November"; break;
                case 12: monthStr = "December"; break;
            }
            return monthStr;
        }
        public static string GetWeek(string week)
        {
            string weekStr = "";
            switch (week)
            {
                case "Sunday":  weekStr = "일"; break;
                case "Monday":  weekStr = "월"; break;
                case "Tuesday": weekStr = "화"; break;
                case "Wednesday":   weekStr = "수"; break;
                case "Thursday":weekStr = "목"; break;
                case "Friday":  weekStr = "금"; break;
                case "Saturday":weekStr = "토"; break;
            }
            return weekStr;
        }
    }
}