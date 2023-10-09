using System;
using System.Collections.Generic;
using lab4;

namespace Program
{
    class Program
    {
        static void Main()
        {   
            // Основыне классы, которые будут реализовывать интерфейс
            List<IHandler> handlers = new List<IHandler> { new HrefHandler(maxNumberOfPages: 10, maxDepth: 3),
                                                           new ImgHandler(),
                                                         };
            //Подписываю классы на событие
            RequestEvent.AddList(handlers);

            //Оповещаю подписчиков
            RequestEvent.Notify(new Uri("https://www.susu.ru/ru/structure"), currentDepth: 0);
        }
    }
}