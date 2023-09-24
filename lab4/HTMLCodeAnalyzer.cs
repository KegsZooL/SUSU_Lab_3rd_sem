using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    static class HTMLCodeAnalyzer 
    {
        private static HttpWebRequest request;
        private static HttpWebResponse response;

        public static List<string> GetHTMLCode(string address) 
        {
            try 
            {
                request = (HttpWebRequest)WebRequest.Create(address);
                
                request.Method = "Get";
                request.UserAgent = "Mozila/5.0";

                response = (HttpWebResponse)request.GetResponse();

                var stream = response.GetResponseStream();

                if (stream != null)
                    return new StreamReader(stream).ReadToEnd().Split(',').ToList<string>();
            }
            catch {
            
            }
            finally{
                response.Close();
            }

            return null;
        }
    }
}