using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;

namespace ComobiOS
{
    class Notepad
    {
        private string user;
        private string path;
        public Notepad(string user, string path)
        {
            this.user = user;
            this.path = path;
        }
        public void Show()
        {
            try
            {
                string content = File.ReadAllText(path);
                Console.Write("");
                Console.Write(content);
                string line = Console.ReadLine();
                Console.WriteLine("\n| F1 - save | F2 - save as |  F3 - close |\n");
                while (true)
                {
                    ConsoleKeyInfo key = BootCore.keyPressed();
                    if (key.Key == ConsoleKey.F1)
                    {
                        BootCore.fileSave(user, path, line);
                        Console.WriteLine("File Saved!");
                        BootCore.sleep(3000);
                        Console.Clear();
                        Notepad notepad = new Notepad(user, path);
                        notepad.Show();
                    }
                    else if (key.Key == ConsoleKey.F2)
                    {
                        Console.WriteLine("Save as:");
                        Console.Write("");
                        path = Console.ReadLine();
                        BootCore.fileSave(user, path, line);
                    }
                    else if (key.Key == ConsoleKey.F3)
                    {
                        Console.Clear();
                        FileManager filem = new FileManager(Kernel.user, Kernel.path);
                        filem.Show();
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid file path!");
                Console.ForegroundColor = ConsoleColor.Green;
                BootCore.sleep(3000);
                Console.Clear();
                Show();
            }
        }
    }
}
