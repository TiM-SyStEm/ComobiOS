using System;
using FS = Cosmos.System.FileSystem;
using System.IO;
using Sys = Cosmos.System;
using System.Linq;

namespace ComobiOS.BootCore
{
    class FileSystem
    {
        public static FS.CosmosVFS fs = new FS.CosmosVFS();
        public static string cdisk = @"0:\";
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
        public static void CreateFile(string name)
        {
            try
            {
                File.Create(cdisk + name);
            }
            catch (Exception)
            {
                IOsteram.Out("Error in create file", ConsoleColor.Red);
            }
        }
        public static bool ExistsFile(string path)
        {
            return File.Exists(cdisk + path);
        }
        public static void DeleteFile(string name)
        {
            try
            {
                File.Delete(cdisk + name);
            }
            catch (Exception)
            {
                IOsteram.Out("Error in delete file", ConsoleColor.Red);
            }
        }
        public static void CreateDir(string name)
        {
            try
            {
                Directory.CreateDirectory(cdisk + name);
            }
            catch (Exception)
            {
                IOsteram.Out("Error in dir file", ConsoleColor.Red);
            }
        }
        public static bool ExistsDir(string path)
        {
            return Directory.Exists(cdisk + path);
        }
        public static void DeleteDir(string name)
        {
            try
            {
                Directory.Delete(cdisk + name);
            }
            catch (Exception e)
            {
                IOsteram.Out(string.Format("Error in delete dir [{0}]", e.ToString().ToUpper()), ConsoleColor.Red);
            }
        }
        public static void Listing()
        {
            var directory_list = fs.GetDirectoryListing(cdisk);
            foreach (var directoryEntry in directory_list)
            {
                if (directoryEntry.mEntryType == FS.Listing.DirectoryEntryTypeEnum.File) {
                    string text = directoryEntry.mName + "\t" + "FILE" + "\t" + Units.Convert((int)directoryEntry.mSize, CompUnits.BITE);
                    IOsteram.Out(text);
                }
                else
                {
                    string text = directoryEntry.mName + "\t" + "DIR" + "\t" + Units.Convert((int)directoryEntry.mSize, CompUnits.BITE);
                    IOsteram.Out(text);
                }
            }
        }
    }
}
