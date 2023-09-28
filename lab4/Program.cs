using lab4;
using System.Collections.Generic;
using System;

namespace Program
{
    class Program
    {
        static void Main()
        {
            HrefHandler hrefHandler = new HrefHandler(maxNumberOfPages: 10);
            ImgHandler imgHandler = new ImgHandler();

            List<IHandler> handlers = new List<IHandler> { hrefHandler, imgHandler};

            RequestEvent.AddList(handlers);

            RequestEvent.Notify(new Uri("https://www.susu.ru/structure"));
        }
    }
}