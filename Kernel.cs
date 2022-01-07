using System;
using Sys = Cosmos.System;
using ComobiOS.BootCore;

namespace ComobiOS
{
    public class Kernel : Sys.Kernel
    {
        public static string version = "1.0";
        private static Shell main;
        public static bool rst = false;
        protected override void BeforeRun()
        {
            IOsteram.Clear();
            IOsteram.Out("Loading ComobiOS...\n", ConsoleColor.Green);
            FileSystem.Init();

            Console.Beep();
            Console.Beep();
            Console.Beep();
            main = new Shell();
            main.Home();
        }

        protected override void Run()
        {
            if (rst)
            {
                rst = false;
                IOsteram.Clear();
                main.Home();
            }
            main.Start();
        }
    }
}
