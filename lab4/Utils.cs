using System;
using System.Net;
using System.Text;

namespace lab4
{
    static class Utils
    {   
        public static string Domain { get; set; }

        public static string GetPageByURI(Uri uri) 
        {
            if (Domain == null)
            {
                int countOfSlashes = 0;
                
                string currentUri = uri.ToString();

                for (int i = 0; i < currentUri.Length; i++)
                {
                    if (currentUri[i] == '/') 
                    {
                        ++countOfSlashes;
                    }

                    if (countOfSlashes == 3) 
                    {
                        break;
                    }

                    Domain += currentUri[i];
                }
            }

            WebClient webClient = new WebClient();

            webClient.Headers["User-Agent"] = "Mozila/5.0";
            webClient.Encoding = Encoding.UTF8;

            return webClient.DownloadString(uri);
        }
    }
}