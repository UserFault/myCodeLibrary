using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using MyCodeLibrary.TextProcessing;

namespace MyCodeLibrary.HTMLoperations
{
    public class HTMLprocessor
    {
        private static char[] tagsSeparator = new char[] { '<', '>' };
        /// <summary>
        /// NT-Разделить строку тегов из Posts на отдельные теги без скобок.
        /// Теги еще надо потом дообработать: .Trim().ToLowerInvariant() для гарантии.
        /// </summary>
        /// <param name="input">Строка тегов</param>
        /// <returns>Возвращает массив строк тегов</returns>
        public static String[] parseTagString(String input)
        {
            return input.Split(tagsSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Собрать строку тегов через запятую и убрать угловые скобки
        /// </summary>
        /// <param name="tags">Строка тегов в угловых скобках</param>
        /// <returns></returns>
        public static string makeTagString(string tags)
        {
            String[] tagar = HTMLprocessor.parseTagString(tags);
            return String.Join(", ", tagar);
        }

        /// <summary>
        /// NT-Экранировать символы для показа в HTML-файлах
        /// </summary>
        /// <param name="titleText"></param>
        /// <returns></returns>
        public static string getSafeHtmlText(String titleText)
        {
            String t = titleText.Replace("<", "&lt;").Replace(">", "&gt;");
            t = t.Replace("\"", "&quot;").Replace("=", "&equiv;");

            return t;
        }

        /// <summary>
        /// NT-попробовать собрать ссылку из этого веб-адреса
        /// Вернуть ссылку или исходную строку
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string tryMakeLink(string p)
        {

            if (String.IsNullOrEmpty(p))
                return String.Empty;

            String src = p;
            if (p.StartsWith("ww"))
                src = "http://" + p;

            if (p.StartsWith("http") || p.StartsWith("ftp"))
                return String.Format("<a href=\"{0}\">{1}</a>", src, p);
            else return p;
        }

        /// <summary>
        /// NT-Собрать веб-ссылку
        /// </summary>
        /// <param name="link">веб-ссылка или ссылка на файл</param>
        /// <param name="title">Название ссылки</param>
        /// <returns></returns>
        public static string makeHtmlFileLink(string link, string title)
        {
            return String.Format("<A href=\"{0}\">{1}</A>", link, title);
        }

        /// <summary>
        /// Returns a string of the HTML of the specified URL String.
        /// </summary>
        /// <param name="url"></param>
        public static String GetHtml(String url)
        {
            if ((!String.IsNullOrEmpty(url)) && CStringProcessor.IsValidUrl(url))
            {
                return GetHtml(new Uri(url));
            }
            throw new ArgumentException("URL string is invalid!");
        }

        /// <summary>
        /// Returns a string of the HTML of the specified Uri object.
        /// </summary>
        /// <param name="url"></param>
        public static String GetHtml(Uri url)
        {
            try
            {
                //Create request For given url
                var response = ((HttpWebRequest)WebRequest.Create(url)).GetResponse();

                //Take response stream
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    //Read response stream (html code)
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Returns a string of the JSON request
        /// </summary>
        /// <param name="url">API URL</param>
        /// <param name="CurrentToken">API Security Token</param>
        /// <returns></returns>
        public static String GetJson(String url, String CurrentToken)
        {
            if ((!String.IsNullOrEmpty(url)) && CStringProcessor.IsValidUrl(url))
            {
                return GetJson(new Uri(url), CurrentToken);
            }
            throw new ArgumentException("URL string is invalid!");
        }

        /// <summary>
        /// Returns a string of the JSON request
        /// </summary>
        /// <param name="url">API URL</param>
        /// <param name="currentToken">API Security Token</param>
        /// <returns></returns>
        public static String GetJson(Uri url, String currentToken)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + currentToken);
                request.ContentType = "application/json";
                request.Method = "GET";

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
