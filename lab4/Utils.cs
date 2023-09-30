using System;
using System.Net;
using System.Text;

namespace lab4
{
    static class Utils
    {
        public static string GetPageByURI(Uri uri) 
        {
            WebClient webClient = new WebClient();

            webClient.Headers["User-Agent"] = "Mozila/5.0";
            webClient.Encoding = Encoding.UTF8;

            return webClient.DownloadString(uri);
        }
    }
}