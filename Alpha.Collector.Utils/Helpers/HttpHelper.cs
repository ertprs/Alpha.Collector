using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Alpha.Collector.Tools.Helpers
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 20000;

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();

            return responseContent;
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string postData = "")
        {
            string result = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(requestStream, Encoding.UTF8))
                {
                    writer.Write(postData);
                }
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        /// <summary>
        /// 获取网页源代码
        /// </summary>
        /// <param name="param"></param>
        /// <param name="errInfo">错误信息</param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static string GetHtml(HttpRequestParam param, ref string errInfo, CookieContainer cookieContainer = null)
        {
            if (string.IsNullOrEmpty(param.Url))
            {
                errInfo = "请求地址不能为空";
                return "";
            }

            HttpWebResponse response = null;
            Stream stream = null;
            List<byte> bytes2 = new List<byte>();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(param.Url);
                request.Accept = "*/*";
                request.Timeout = 60000;

                if (cookieContainer != null)
                {
                    request.CookieContainer = cookieContainer;
                    CookieCollection coll = cookieContainer.GetCookies(new Uri(param.Url));
                }

                if (!string.IsNullOrEmpty(param.Origin))
                {
                    request.Headers["Origin"] = param.Origin;
                }

                if (!string.IsNullOrEmpty(param.Methond))
                {
                    request.Method = param.Methond.ToUpper();
                }

                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }

                request.UserAgent = !string.IsNullOrEmpty(param.UserAgent)
                    ? param.UserAgent
                    : "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36";

                Encoding encoding = !string.IsNullOrEmpty(param.Encode) ? Encoding.GetEncoding(param.Encode) : Encoding.UTF8;

                if (!string.IsNullOrEmpty(param.PostData))
                {
                    var postDataByte = encoding.GetBytes(param.PostData);
                    request.ContentLength = postDataByte.Length;
                    stream = request.GetRequestStream();
                    stream.Write(postDataByte, 0, postDataByte.Length);
                    stream.Close();
                }

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    errInfo = response.StatusDescription;
                    return "";
                }

                stream = response.GetResponseStream();
                int count1;

                byte[] bytes = new byte[1024];
                while ((count1 = stream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    bytes2.AddRange(bytes.ToList().Take(count1));
                }

                string html = Encoding.UTF8.GetString(bytes2.ToArray());

                return html;
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return "";
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }

    /// <summary>
    /// Http请求的参数
    /// </summary>
    public class HttpRequestParam
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 主要是用来说明最初请求是从哪里发起的
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 要发送的数据
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// 要发布的数据的编码格式
        /// </summary>
        public string Encode { get; set; }

        /// <summary>
        /// 请求的方法，Post、Get等
        /// </summary>
        public string Methond { get; set; }

        /// <summary>
        /// UA
        /// </summary>
        public string UserAgent { get; set; }
    }
}
