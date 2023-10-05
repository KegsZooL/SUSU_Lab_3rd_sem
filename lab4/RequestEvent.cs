using System;
using System.Collections.Generic;
using System.Threading;

namespace lab4
{
    static class RequestEvent
    {   
        
        delegate void URLHandler(Uri page);

        static event URLHandler _SomeEvent;

        public static void AddList(List<IHandler> handlers) => handlers.ForEach(handler => _SomeEvent += handler.Process);

        public static void Notify(Uri str) 
        {   
            _SomeEvent?.Invoke(str);

            Thread.Sleep(1000);
        } 
    }
}