using System;
using System.Collections.Generic;
using System.Text;

namespace HiroEngine.HiroEngine.Data.Logger
{
    public class Logger
    {
        public static void Debug(string caller, string msg)
        {
            Console.WriteLine($"[DEBUG] [{caller}] : {msg}");
        }

        public static void Error(string caller, string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] [{caller}] : {msg}");
            Console.ResetColor();
        }

        public static void Warn(string caller, string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] [{caller}] : {msg}");
            Console.ResetColor();
        }
    }
}
