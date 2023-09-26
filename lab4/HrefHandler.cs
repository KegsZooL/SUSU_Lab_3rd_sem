using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4
{
    class HrefHandler : IHandler
    {
        
        public void Process(Uri uri)
        {

            string page = Utils.GetPageByURI(uri);

            const string URLRegExPattern = "a\\s+[^>]*href=\"([^\"]*)\"[^>]*>";

            string[] banWords = { "class=", "data-target=", "data-toggle=", "data-target=" };

            //List<string> parametrsInTag = Regex.Matches(page, URLRegExPattern).Cast<Match>().
            //    Select(key => key.Value).ToList().ForEach((currentLine) => 
            //    {
            //        currentLine = 



            //    });
        }
    }
}
    

