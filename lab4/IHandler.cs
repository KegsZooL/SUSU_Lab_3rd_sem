using System;

namespace lab4
{
    interface IHandler //Интерфейс для строгого назначения контракта реализующим методам
    {
        void Process(Uri uri, int currentDepth);
    }
}