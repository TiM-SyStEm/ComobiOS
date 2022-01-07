using System;

namespace ComobiOS.BootCore
{
    class IOsteram
    {
        public static void Out(string text, ConsoleColor color = ConsoleColor.White)
        {
            if(color != ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else Console.WriteLine(text);
        }
        public static string In(string text, ConsoleColor color = ConsoleColor.White)
        {
            if (color != ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                Console.Write(text);
                Console.ForegroundColor = ConsoleColor.White;
                return Console.ReadLine();
            }
            Console.Write(text);
            return Console.ReadLine();
        }
        public static void Clear()
        {
            Console.Clear();
        }
    }
}
