using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Services;
using FileExplorer.Infrastructure.Services;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestMethod();
            //var items = Directory.EnumerateDirectories(@"G:\System Volume Information");
            //if(Directory.Exists(@"G:\System Volume Information"))
            //    Console.WriteLine("Hello world");
            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}


            //DirectoryInfo directory = new DirectoryInfo(@"C:\");
            //DriveInfo[] allDrives = DriveInfo.GetDrives();
            //foreach (DriveInfo d in allDrives)
            //{
            //    Console.WriteLine("Drive {0}", d.Name);
            //    Console.WriteLine("  Drive type: {0}", d.DriveType);
            //    if (d.IsReady == true)
            //    {
            //        Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
            //        Console.WriteLine("  File system: {0}", d.DriveFormat);
            //        Console.WriteLine("  RootDirectory: {0}", d.RootDirectory.Name);
            //        Console.WriteLine(
            //            "  Available space to current user:{0, 15} bytes",
            //            d.AvailableFreeSpace);

            //        Console.WriteLine(
            //            "  Total available space:          {0, 15} bytes",
            //            d.TotalFreeSpace);

            //        Console.WriteLine(
            //            "  Total size of drive:            {0, 15} bytes ",
            //            d.TotalSize);
            //    }
            //}
            TestCopy();
            Console.ReadKey();
        }

        public static async void TestCopy()
        {
            string path = "H:\\New folder";
            string targetPath = "G:\\Tmp\\sadasd";
            IList<FileItemInfo> items=new List<FileItemInfo>();
            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                var dirs= directory.EnumerateDirectories();
                foreach (var dir in dirs)
                {
                    items.Add(new FileItemInfo
                    {
                        FullName = dir.FullName,
                        Name = dir.Name,
                        IsDirectory = true
                    });
                }
                var files = directory.EnumerateFiles();
                foreach (var file in files)
                {
                    items.Add(new FileItemInfo
                    {
                        FullName = file.FullName,
                        Name = file.Name,
                        IsDirectory = false
                    });
                }
                FileOperationService service=new FileOperationService();
                var result =await service.CopyFileItem(items, targetPath, true);
                foreach (var r in result)
                {
                    Console.WriteLine($"{r.SourceName}\t{r.TargetName}\t{r.IsDirectory}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public static void TestMethod()
        {
            var s = new FileService();
            var task = s.FindFileItemsAsync(@"G:\\", "*",SearchOption.AllDirectories);
            task.Wait();
            var list = task.Result;
            foreach (var i in list)
            {
                Console.WriteLine($"{i.FullName}\tIsDirectory:{i.IsDirectory}");
            }
        }
    }
}
