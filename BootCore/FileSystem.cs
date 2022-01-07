using System;
using FS = Cosmos.System.FileSystem;
using System.IO;

namespace ComobiOS.BootCore
{
    class FileSystem
    {
        public static FS.CosmosVFS fs = new FS.CosmosVFS();
        public static string cdir = "0";

        public static void Init()
        {
            FS.VFS.VFSManager.RegisterVFS(fs);
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveFormat != "FAT32")
                {
                    IOsteram.Out("\nERROR INIT FS [not FAT32]", ConsoleColor.Red);
                }
                IOsteram.Out("\nINIT FS[DONE]", ConsoleColor.Green);
            }
        }
    }
}
