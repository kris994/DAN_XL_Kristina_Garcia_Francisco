using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that creates the documents used for prining
    /// </summary>
    class Document
    {
        #region Properties
        public string Form { get; set; }
        public string Color { get; set; }
        public string Orientation { get; set; }
        #endregion

        /// <summary>
        /// Used for generating random document values
        /// </summary>
        private Random rng = new Random();
        /// <summary>
        /// Locks the document to be accessed by only one thread at a time
        /// </summary>
        static EventWaitHandle waitDocument = new AutoResetEvent(true);

        #region Constructor
        public Document(string form, string color, string orientation)
        {
            Form = form;
            Color = color;
            Orientation = orientation;
        }

        public Document()
        {

        }
        #endregion

        /// <summary>
        /// Method used for creating random documents
        /// </summary>
        public void CreateDocument()
        {
            Printer printer = new Printer();
            string form = "";
            string color = "";
            string orientation = "";
            string threadName = "";
            Document doc = new Document(form, color, orientation);

            // Can only be accessed by one thread at a time, since AutoResetEvent is set to true
            // the first thread will pass, the rest has to wait for Set().
            waitDocument.WaitOne();

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

            threadName = Thread.CurrentThread.Name;
            doc = new Document(form, color, orientation);

            Console.WriteLine("{0} sent request to print document with {1} form. Color: {2}. Orientation: {3}",
                Thread.CurrentThread.Name, form, color, orientation);
            Thread.Sleep(100);

            // Let the next thread pass
            waitDocument.Set();

            // Pass the document to the printer
            printer.DocumentToPrint(threadName, doc);
        }
    }
}
