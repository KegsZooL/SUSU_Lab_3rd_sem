using System;
using System.Collections.Generic;
using System.Threading;

namespace lab4
{   
    static class RequestEvent
    {   
        delegate void URLHandler(Uri page, int currentDepth);

        static event URLHandler _SomeEvent;

        /*
         * Каждый элемент коллекции c одинаковой конструкцией подписываю методом .ForEach на событие _SomeEvent, которое 
         * "обворачивает" делегат URLHandler
         */
        public static void AddList(List<IHandler> handlers) => handlers.ForEach(handler => _SomeEvent += handler.Process);
        
        public static void Notify(Uri str, int currentDepth) 
        {   
            _SomeEvent?.Invoke(str, currentDepth);
            
            Thread.Sleep(1000); //Задержка после оповещения подписчиков
        } 
    }
}