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
            TestMethod();
            Console.ReadKey();
        }

        public static void TestMethod()
        {
            var s = new FileService();
            var task = s.FindFileItems(@"G:\Xuefeng\OneDrive\个人\Code\ChengJiGuanLiXiTong\CJGLXT.App", "App*.*",SearchOption.AllDirectories);
            task.Wait();
            var list = task.Result;
            foreach (var i in list)
            {
                Console.WriteLine($"{i.Name}\tIsDirectory:{i.IsDirectory}");
            }
        }
    }
}
