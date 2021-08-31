using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace ComobiOS
{
    class FileManager
    {
        private string user;
        private string path;
        public FileManager(string user, string path)
        {
            this.user = user;
            this.path = path;
        }

        public object Sys { get; private set; }

        public void Show()
        {
            Console.Clear();
            string user_path = user + "\t" + path + @"\";
            Console.WriteLine("User: "+ "\t" + "Path:");
            Console.WriteLine(user_path);
            Console.WriteLine("\n");
            CosmosVFS fs = Kernel.fs;

            var directory_list = fs.GetDirectoryListing(path);

            foreach (var directoryEntry in directory_list)
            {
                Console.WriteLine(directoryEntry.mName);
            }

            Console.WriteLine("\n");
            Console.WriteLine("| F1 - creat file | F2 - delete file |");
            Console.WriteLine("| F3 - open       | F4 - close       |");
            while (true)
            {
                ConsoleKeyInfo key = BootCore.keyPressed();
                if (key.Key == ConsoleKey.F1)
                {
                    try
                    {
                        Console.WriteLine("Create file:");
                        Console.Write("");
                        File.Create(path + @"\" + Console.ReadLine());
                        Console.Clear();
                        Show();
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
                else if (key.Key == ConsoleKey.F2)
                {
                    try
                    {
                        Console.WriteLine("Delete file:");
                        File.Delete(path + @"\" + Console.ReadLine());
                        Console.Clear();
                        Show();
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
                else if (key.Key == ConsoleKey.F3)
                {
                    try
                    {
                        Console.WriteLine("Open file:");
                        Console.Write("");
                        string file = path + @"\" + Console.ReadLine();
                        string ext = file.Substring(file.LastIndexOf('.'));
                        if (ext.Contains("txt") || ext.Contains("bcl"))
                        {
                            Console.Clear();
                            Notepad notepad = new Notepad(user, file);
                            notepad.Show();
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Fatal error!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        BootCore.sleep(3000);
                        Console.Clear();
                        Show();
                    }
                }
                else if (key.Key == ConsoleKey.F4)
                {
                    BootCore.clear();
                    Kernel.main();
                }
            }
        }
    }
}
