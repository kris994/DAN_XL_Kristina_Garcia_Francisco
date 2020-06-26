using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    // The main program class
    class Program
    {
        /// <summary>
        /// List that contains all colors used in the documents
        /// </summary>
        public static List<string> allColors = new List<string>()
        { "red","blue","green","black","white","purple","yellow","pink"};
        /// <summary>
        /// Types of document forms
        /// </summary>
        public static string[] allForms = {"A3","A4"};
        /// <summary>
        /// Types of document orientations
        /// </summary>
        public static string[] allOrientations = { "portrait", "landscape" };
        /// <summary>
        /// List of all computer threads
        /// </summary>
        public static List<Thread> allComputers = new List<Thread>();
        /// <summary>
        /// The color palatte file
        /// </summary>
        public static readonly string palatteFile = "pallate.txt";

        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ReadWriteFile rwf = new ReadWriteFile();
            Computer comp = new Computer();

            // Saves the colors from the list to the file and waits the thread to finish before continuing 
            Thread writeFile = new Thread(() => rwf.WriteToFile(palatteFile, allColors));
            writeFile.Start();
            writeFile.Join();

            // Starts the computer threads
            comp.AddComputers();

            Console.ReadKey();
        }
    }
}
