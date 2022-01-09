using Cosmos.System.Graphics;
using System;
using System.Drawing;
using Sys = Cosmos.System;
using static Cosmos.HAL.VGADriver;
using Date = Cosmos.HAL.RTC;

namespace ComobiOS.BootCore
{
    class Shell
    {
        protected static Memory memory = new();
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
                        case "colDepth":
                            if (objects[2] == "4")
                                OpenCG.CG.ColorDepth = Sys.Graphics.ColorDepth.ColorDepth4;
                            else if (objects[2] == "8")
                                OpenCG.CG.ColorDepth = Sys.Graphics.ColorDepth.ColorDepth8;
                            else if (objects[2] == "16")
                                OpenCG.CG.ColorDepth = Sys.Graphics.ColorDepth.ColorDepth16;
                            else if (objects[2] == "24")
                                OpenCG.CG.ColorDepth = Sys.Graphics.ColorDepth.ColorDepth24;
                            else if (objects[2] == "32")
                                OpenCG.CG.ColorDepth = Sys.Graphics.ColorDepth.ColorDepth32;
                            break;
                    }
                    break;
                case "info":
                    IOsteram.Out("========================");
                    IOsteram.Out("Comobi version: " + Kernel.version);
                    IOsteram.Out("RuntimeAPI version: " + runtime.RuntimeAPI.version);
                    IOsteram.Out("OpenCG version: " + OpenCG.CG.version);
                    IOsteram.Out("Amount of RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() + " MB");
                    IOsteram.Out("Free RAM: " + memory.FreeMemory + " MB");
                    IOsteram.Out("CPU name: " + Cosmos.Core.CPU.GetCPUBrandString());
                    IOsteram.Out("========================");
                    break;
                case "home":
                    IOsteram.Clear();
                    string __main_txt__ = string.Format("\n========================\n     TIM SYSTEM 2022\n========================\nComobiOS {0}{1}", Kernel.version, "\n");
                    IOsteram.Out(__main_txt__);
                    break;
                case "out":
                    string[] text = comm.Split("\"");
                    IOsteram.Out(text[1]);
                    break;
                case "dt":
                    IOsteram.Out("===================");
                    IOsteram.Out(DateTime.STDview());
                    IOsteram.Out("===================");
                    break;
                case "mkfile":
                    FileSystem.CreateFile(objects[1]);
                    break;
                case "exfile":
                    if (FileSystem.ExistsFile(objects[1]))
                        IOsteram.Out("File exists", ConsoleColor.Green);
                    else IOsteram.Out("File not exists", ConsoleColor.Red);
                    break;
                case "core":
                    if (objects[1] == "author")
                    {
                        IOsteram.Out("Timofei Gorlov", ConsoleColor.Green);
                    }
                    break;
                case "exdir":
                    if (FileSystem.ExistsDir(objects[1]))
                        IOsteram.Out("Dir exists", ConsoleColor.Green);
                    else IOsteram.Out("Dir not exists", ConsoleColor.Red);
                    break;
                case "mkdir":
                    FileSystem.CreateDir(objects[1]);
                    break;
                case "rmfile":
                    FileSystem.DeleteFile(objects[1]);
                    break;
                case "rmdir":
                    FileSystem.DeleteDir(objects[1]);
                    break;
                case "clip":
                    if (objects[1] == "-c")
                    {
                        text = comm.Split("\"");
                        ClipBoard.Data = text[1];
                    }
                    else if (objects[1] == "-v")
                    {
                        IOsteram.Out(ClipBoard.Data);
                    }
                    break;
                case "test":
                    // test OpenCG
                    OpenCG.CG.Init();
                    OpenCG.CG.Clear(Color.Blue);
                    OpenCG.CG.Simple.drawRect(70, 70, 300, 300, Color.White);
                    OpenCG.CG.Simple.drawString("TEXT", 90, 90, Color.White);
                    break;
                case "ls":
                    FileSystem.Listing();
                    break;
                case "calc":
                    break;
                default:
                    IOsteram.Out("Error in cmd [COMMAND NOT FOUND]", ConsoleColor.Red);
                    break;
            }
        }
        public static string inpcmd()
        {
            return IOsteram.In("$" + FileSystem.cdir + ">");
        }
    }
}
