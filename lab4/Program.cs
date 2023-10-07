using System.Collections.Generic;
using System;
using lab4;

namespace Program
{
    class Program
    {
        static void Main()
        {
            List<IHandler> handlers = new List<IHandler> { new HrefHandler(maxNumberOfPages: 75, maxDepth: 3), 
                                                           new ImgHandler(),
                                                           new FileFormationHandler()
                                                         };
            RequestEvent.AddList(handlers);

            RequestEvent.Notify(new Uri("https://www.susu.ru/ru/structure"), currentDepth: 0);
        }
    }
}