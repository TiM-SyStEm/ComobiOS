using System;
using System.IO;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Sys = Cosmos.System;

namespace ComobiOS
{
    public class Kernel : Sys.Kernel
    {
        public static CosmosVFS fs;
        public static string usersFile = @"0:\users.dat";
        public static string user = string.Empty;
        public static string path = "0:";
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Loading the kernel...");
            bool isOkey = BootCore.deviceTest();
            if (isOkey)
            {
                BootCore.sleep(2000);
                Console.Clear();
                // настраиваем цвет косоли
                Console.ForegroundColor = ConsoleColor.Green;
                // привествие
                Console.WriteLine("TIM SYSTEM");
                Console.WriteLine("Operation System, ComobiOS " + BootCore.version);
                Console.WriteLine("=============================");
            }
            else Console.Write("");
        }

        protected override void Run()
        {
            Console.Write("$> ");
            var cmd = Console.ReadLine();
            // Bcl micro
            BootCore.microLoader(cmd);
        }
        public static void main()
        {
            while (true)
            {
                Console.Write("$" + user + "> ");
                var cmd = Console.ReadLine();
                BootCore.loader(cmd);
            }
        }
    }
}
