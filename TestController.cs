using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace sell.Controllers
{
    public class HomeController : Controller
    {
        //MD5加密
       public string md5(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }
        // curl post
        private string post(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
        //获取两个时间之间的分钟/小时
        public string getCNTime(string matchdate, string contime, int tytime = 0)
        {
            var tday = (Convert.ToDateTime(matchdate) - Convert.ToDateTime(contime)).Days;
            var thou = (Convert.ToDateTime(matchdate) - Convert.ToDateTime(contime)).Hours;
            var tmin = 0;
            if (tytime == 0)
            {
                tmin = (Convert.ToDateTime(matchdate) - Convert.ToDateTime(contime)).Minutes;
            }
            else
            {
                tmin = (Convert.ToDateTime(matchdate) - Convert.ToDateTime(contime)).Minutes + 10;
            }
            var etime = "";
            if (tday != 0)
            {
                var stday = (tday * 24) + thou;
                etime = stday + ":" + tmin;
            }
            else
            {
                etime = thou + ":" + tmin;
            }
            var timecha = etime;
            var strtime = "";
            if (timecha != "")
            {
                if (Convert.ToInt32(timecha.Split(':')[0]) < 0 || Convert.ToInt32(timecha.Split(':')[1]) < 0)
                {
                    var th = "";
                    var tm = "";
                    var strtimecha = timecha.Split(':');
                    if (strtimecha[0].Split('-').Length >= 2)
                    {
                        th = strtimecha[0].Split('-')[1];
                    }
                    else
                    {
                        th = strtimecha[0].Split('-')[0];
                    }
                    if (strtimecha[1].Split('-').Length >= 2)
                    {
                        tm = strtimecha[1].Split('-')[1];
                    }
                    else
                    {
                        tm = strtimecha[1].Split('-')[0];
                    }
                    strtime = tm;
                    //strtime = "赛后" + th + "h" + " " + tm + "m";
                }
                else
                {
                    strtime = timecha.Split(':')[1];
                    //strtime = "赛前" + timecha.Split(':')[0] + "h" + " " + timecha.Split(':')[1] + "m";
                }

            }
            return strtime;
        }
        //时间转换
        public static string getConvertTime(string time, int type = 0)
        {
            var endtime = "";
            if (type != 2)
            {
                string format = "ddd MMM dd HH:mm:ss CST yyyy";
                DateTime d = DateTime.ParseExact(time, format, System.Globalization.CultureInfo.InvariantCulture);
                endtime = d.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(time + "0000");
                TimeSpan toNow = new TimeSpan(lTime);
                endtime = dtStart.Add(toNow).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return endtime;
        }
       
        
     
       

    }
}
