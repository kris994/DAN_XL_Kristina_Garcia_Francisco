using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    class Program
    {
        public static List<string> allColors = new List<string>()
        { "red","blue","green","black","white","purple","yellow","pink"};
        public static string[] allForms = {"A3","A4"};
        public static string[] allOrientations = { "portrait", "landscape" };
        public static List<Document> allDocuments = new List<Document>();
        public static List<Thread> allComputers = new List<Thread>();
        public static readonly string palatteFile = "pallate.txt";

        static void Main(string[] args)
        {
            ReadWriteFile rwf = new ReadWriteFile();
            Computer comp = new Computer();

            Thread writeFile = new Thread(() => rwf.WriteToFile(palatteFile, allColors));
            writeFile.Start();
            writeFile.Join();

            comp.AddComputers();

            Console.ReadKey();
        }
    }
}
