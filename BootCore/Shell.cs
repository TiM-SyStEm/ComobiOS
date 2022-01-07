using Cosmos.System.Graphics;
using System;
using static Cosmos.HAL.PCIDevice;
using static Cosmos.HAL.VGADriver;
using Date = Cosmos.HAL.RTC;
using Sys = Cosmos.System;

namespace ComobiOS.BootCore
{
    class Shell
    {
        public Shell()
        {

        }
        public void Start()
        {
            string cmd = inpcmd();
            string[] objs = cmd.Split(" ");
            Interpret(cmd, objs);
        }
        public void Home()
        {
            IOsteram.Clear();
            string __main_txt__ = string.Format("\n========================\n     TIM SYSTEM 2022\n========================\nComobiOS {0}{1}", Kernel.version, "\n");
            IOsteram.Out(__main_txt__);
        }
        public static void Interpret(string comm, string[] objects)
        {
            switch (objects[0].ToLower())
            {
                case "pwoff":
                    Sys.Power.Shutdown();
                    break;
                case "reboot":
                    Sys.Power.Reboot();
                    break;
                case "cls":
                    IOsteram.Clear();
                    break;
                // os settings
                case "set":
                    switch (objects[1].ToLower())
                    {
                        // screen  text mode
                        case "sctmode":
                            if (objects[2] == "40x25")
                                VGAScreen.SetTextMode(TextSize.Size40x25);
                            else if (objects[2] == "40x50")
                                VGAScreen.SetTextMode(TextSize.Size40x50);
                            else if (objects[2] == "80x25")
                                VGAScreen.SetTextMode(TextSize.Size80x25);
                            else if (objects[2] == "80x50")
                                VGAScreen.SetTextMode(TextSize.Size80x50);
                            else if (objects[2] == "90x30")
                                VGAScreen.SetTextMode(TextSize.Size90x30);
                            else if (objects[2] == "90x60")
                                VGAScreen.SetTextMode(TextSize.Size90x60);
                            break;
                        case "bgcolor":
                            if (objects[2] == "blue")
                                Console.BackgroundColor = ConsoleColor.Blue;
                            else if (objects[2] == "black")
                                Console.BackgroundColor = ConsoleColor.Black;
                            else if (objects[2] == "white")
                                Console.BackgroundColor = ConsoleColor.White;
                            else if (objects[2] == "cyan")
                                Console.BackgroundColor = ConsoleColor.Cyan;
                            else if (objects[2] == "yellow")
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            else if (objects[2] == "magenta")
                                Console.BackgroundColor = ConsoleColor.Magenta;
                            Kernel.rst = true;
                            break;
                    }
                    break;
                case "info":
                    IOsteram.Out("========================");
                    IOsteram.Out("Comobi version: " + Kernel.version);
                    IOsteram.Out("RuntimeAPI version: " + "0.0");
                    IOsteram.Out("Amount of RAM: " + Cosmos.Core.CPU.GetAmountOfRAM());
                    IOsteram.Out("Free RAM: ");
                    IOsteram.Out("CPU vendor: " + Cosmos.Core.CPU.GetCPUVendorName());
                    IOsteram.Out("========================");
                    break;
                case "home":
                    IOsteram.Clear();
                    string __main_txt__ = string.Format("\n========================\n     TIM SYSTEM 2022\n========================\nComobiOS {0}{1}", Kernel.version, "\n");
                    IOsteram.Out(__main_txt__);
                    break;
                case "out":
                    string[] text = comm.Split("\"");
                    IOsteram.Out(text[3]);
                    break;
                case "dt":
                    IOsteram.Out("===================");
                    IOsteram.Out(Date.DayOfTheMonth + "." + Date.Month + ".20" + Date.Year + " " + Date.Hour + ":" + Date.Minute);
                    IOsteram.Out("===================");
                    break;
            }
        }
        public static string inpcmd()
        {
            return IOsteram.In("$" + FileSystem.cdir + ">");
        }
    }
}
