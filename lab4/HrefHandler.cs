using System;
using System.Collections.Generic;

namespace lab4
{
    class HrefHandler : IHandler
    {   
        int MaxNumberOfPages { get; set; }

        int MaxDepth { get; set; }

        string BaseURI { get; set; }

        readonly HashSet<string> passedLinks = new HashSet<string>();

        public HrefHandler(int maxNumberOfPages, int maxDepth) 
        {   
            MaxNumberOfPages = maxNumberOfPages;
            
            MaxDepth = maxDepth;
        } 
        
        public void Process(Uri uri, int currentDepth) 
        {
            if (MaxNumberOfPages <= 0)
            { 
                return;
            }

            if(BaseURI == null) 
            {
                BaseURI = uri.ToString();
            }

            passedLinks.Add(uri.ToString());
            --MaxNumberOfPages;
            
            string currentPage = Utils.GetPageByURI(uri);

            List<string> parametrsURI = Utils.GetParametrsURI(ref currentPage);

            if (parametrsURI[0] == "#main-content")
                parametrsURI.Remove(parametrsURI[0]);
            
            while (passedLinks.Count < MaxNumberOfPages)
            {
                try 
                {
                    foreach (var link in parametrsURI)
                    {
                        if (!passedLinks.Contains(link) && (link.ToString().StartsWith(BaseURI) && currentDepth < MaxDepth))
                        {
                            RequestEvent.Notify(new Uri(link), currentDepth++);
                        }
                    }
                }
                catch 
                {
                    continue;
                }
            }
        }
    }
}