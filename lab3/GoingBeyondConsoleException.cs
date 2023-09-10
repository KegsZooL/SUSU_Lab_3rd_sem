using System;

namespace CustomException
{
    class GoingBeyondConsoleException : FormatException
    {
        public GoingBeyondConsoleException(string message) : base(message) { }
    }
}
