﻿using lab4;
using System.Collections.Generic;
using System;

namespace Program
{
    class Program
    {
        static void Main()
        {
            HrefHandler hrefHandler = new HrefHandler();
            FileFormationHandler fileFormationHandler = new FileFormationHandler();

            List<IHandler> handlers = new List<IHandler> { hrefHandler, fileFormationHandler };

            RequestEvent.AddList(handlers);

            RequestEvent.Notify(new Uri("https://www.susu.ru/structure"));
        }
    }
}