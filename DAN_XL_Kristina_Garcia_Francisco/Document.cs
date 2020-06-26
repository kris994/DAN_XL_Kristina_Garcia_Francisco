using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    class Document
    {
        public string Form { get; set; }
        public string Color { get; set; }
        public string Orientation { get; set; }

        private Random rng = new Random();
        private readonly object lockDoc = new object();

        public Document(string form, string color, string orientation)
        {
            Form = form;
            Color = color;
            Orientation = orientation;
        }

        public Document()
        {
        }

        public void CreateDocument()
        {
            Printer printer = new Printer();
            string form = "";
            string color = "";
            string orientation = "";
            string threadName = "";
            Document doc = new Document(form, color, orientation);

            lock (lockDoc)
            {
                // Get random form
                int formNumber = rng.Next(0, 2);
                form = Program.allForms[formNumber];

                // Number of colors in the file
                int colorNumber = rng.Next(0, 8);
                // Get a random color from the file
                color = File.ReadLines(Program.palatteFile).Skip(colorNumber).Take(1).First();

                // Get random orientation
                int orientationNumber = rng.Next(0, 2);
                orientation = Program.allOrientations[orientationNumber];

                doc = new Document(form, color, orientation);
                Program.allDocuments.Add(doc);
                Thread.Sleep(100);
                Console.WriteLine("{0} sent request to print document with {1} form. Color: {2}. Orientation: {3}",
                    Thread.CurrentThread.Name, form, color, orientation);

                threadName = Thread.CurrentThread.Name;
            }
            printer.DocumentToPrint(threadName, doc);
        }
    }
}
