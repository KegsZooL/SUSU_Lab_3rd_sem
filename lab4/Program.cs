using lab4;
using System.Collections.Generic;
using System;

namespace Program
{
    class Program
    {
        static void Main()
        {
            ParserLinksToPNG parserhLinks = new ParserLinksToPNG();

            ConsoleOutputHandler consoleOutputHandler = new ConsoleOutputHandler();
            FileFormationHandler fileFormationHandler = new FileFormationHandler();

            List<IHandler> handlers = new List<IHandler> { consoleOutputHandler, fileFormationHandler };
            RequestEvent.AddList(handlers);

            RequestEvent.Notify(new Uri("https://www.susu.ru/structure"));
        }
    }
}