using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Infrastructure.Services;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestMethod();
            //var drives=Directory.EnumerateDirectories(@"C:\");
            //foreach (var drive in drives)
            //{
            //    Console.WriteLine(drive);
            //}
            //DirectoryInfo directory =new DirectoryInfo(@"C:\");
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  RootDirectory: {0}", d.RootDirectory.Name);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }

            Console.ReadKey();
        }

        public static void TestMethod()
        {
            var s = new FileService();
            var task = s.FindFileItemsAsync(@"G:\Xuefeng\OneDrive\个人\Code\ChengJiGuanLiXiTong\CJGLXT.App", "App*.*",SearchOption.AllDirectories);
            task.Wait();
            var list = task.Result;
            foreach (var i in list)
            {
                Console.WriteLine($"{i.Name}\tIsDirectory:{i.IsDirectory}");
            }
        }
    }
}
