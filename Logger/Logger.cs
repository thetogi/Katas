using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : ILogger
    {
        public void Log(string message)        
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void LogError(string message, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Exception occurred. Type: {exception.GetType()}, exception text: {exception}. Message: {message}");
            Console.ResetColor();
        }

        public void LogException(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception occurred. Type: {exception.GetType()}, exception text: {exception}");
            Console.ResetColor();
        }
    }
}
