using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace ComobiOS
{
    public static class BootCore
    {
        public static string version = "21";
        public static bool deviceTest()
        {
            try
            {
                Kernel.fs = new CosmosVFS();
                VFSManager.RegisterVFS(Kernel.fs);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Device testing [DONE]");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Device testing [ERROR] : FileSystem");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }
        public static void sleep(int sec)
        {
            sec *= 100000;
            for (int x = 0; x < sec; x++) { }
        }
        public static void singup(){
            Console.WriteLine("Creating new user\n");

            if (!File.Exists(Kernel.usersFile))
            {
                File.Create(Kernel.usersFile);
            }
            Console.Write("User name: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Save user (y/n)? ");
            string yesOrNo = Console.ReadLine();
            if (yesOrNo == "y")
            {
                Directory.CreateDirectory(username);
                File.WriteAllText(Kernel.usersFile, string.Format("{0}:{1}\n", username, password));
            }
            else if (yesOrNo == "n")
            {
                return;
            }
        }
        public static bool login()
        {
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();
            string[] lines = File.ReadAllLines(Kernel.usersFile);
            foreach (string line in lines)
            {
                string[] objs = line.Split(':');
                if (objs[0] == login)
                {
                    if (objs[1] == pass)
                    {
                        Kernel.user = objs[0];
                        return true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid password!");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
            }
            return false;
        }
        public static void clear()
        {
            Console.Clear();
            Console.WriteLine("TIM SYSTEM");
            Console.WriteLine("Operation System, ComobiOS " + version);
            Console.WriteLine("=============================");
        }
        public static void loader(string cmd)
        {
            string[] lines = cmd.Split('\n');
            foreach(string line in lines)
            {
                string[] objs = line.Split(' ');
                switch (objs[0])
                {
                    case "poweroff":
                        Sys.Power.Shutdown();
                        break;
                    case "reboot":
                        Sys.Power.Reboot();
                        break;
                    case "signup":
                        singup();
                        break;
                    case "login":
                        bool result = login();
                        if (result)
                        {
                            Kernel.main();
                        }
                        break;
                    case "clear":
                        clear();
                        break;
                    case "filem":
                        FileManager filem = new FileManager(Kernel.user, Kernel.path);
                        filem.Show();
                        break;
                    case "bootcore":
                        if (objs[1] == "loader")
                        {
                            string content = File.ReadAllText(objs[2]);
                            loader(content);
                        }
                        break;
                    case "notepad":
                        string file = objs[1];
                        Console.Clear();
                        Notepad notepad = new Notepad(Kernel.user, file);
                        notepad.Show();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Command is not found!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }
            }
        }
        public static void microLoader(string cmd)
        {
            switch (cmd)
            {
                case "poweroff":
                    Sys.Power.Shutdown();
                    break;
                case "reboot":
                    Sys.Power.Reboot();
                    break;
                case "signup":
                    singup();
                    break;
                case "login":
                    bool result = login();
                    if (result)
                    {
                        Kernel.main();
                    }
                    break;
                case "clear":
                    clear();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command is not found!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }
        public static ConsoleKeyInfo keyPressed()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            return key;
        }
        public static void fileSave(string user, string path, string line)
        {
            var file = VFSManager.GetFile(path);
            var file_stream = file.GetFileStream();

            if (file_stream.CanWrite)
            {
                byte[] text_to_write = Encoding.ASCII.GetBytes(line);
                file_stream.Write(text_to_write, 0, text_to_write.Length);
            }
        }
    }
}
