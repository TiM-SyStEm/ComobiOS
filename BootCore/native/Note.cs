using System;
using System.IO;
using Sys = Cosmos.System;

namespace ComobiOS.BootCore.native
{
    class Note
    {
        public static void Notepad()
        {
            IOsteram.Clear();
            string inp = IOsteram.In("Create new file?(y/n) ");
            if(inp == "y")
            {
                IOsteram.Clear();
                IOsteram.Out("To save the file, click insert on keyboard", ConsoleColor.Green);
                IOsteram.Out("==========================================", ConsoleColor.Green);
            }
        }
    }
}
