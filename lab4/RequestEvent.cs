using System;
using System.Collections.Generic;

namespace lab4
{
    static class RequestEvent
    {
        public delegate void PNGDelegate(Uri page);

        static event PNGDelegate _SomeEvent;

        public static void Add(PNGDelegate pngDelegate) => _SomeEvent += pngDelegate;

        public static void AddList(List<IHandler> handlers) => handlers.ForEach(handler => _SomeEvent += handler.Process);

        public static void Notify(Uri str) => _SomeEvent?.Invoke(str);
    }
}
