using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Prints the given document
    /// </summary>
    class Printer
    {
        /// <summary>
        /// Wait for the signal to start printing the A4 pages
        /// </summary>
        static EventWaitHandle waitA4 = new AutoResetEvent(true);
        /// <summary>
        /// Wait for the signal to start printing the A3 pages
        /// </summary>
        static EventWaitHandle waitA3 = new AutoResetEvent(true);
        /// <summary>
        /// Saves all names of cumputers that already printed
        /// </summary>
        public static List<string> allComputersThatPrinted = new List<string>();

        /// <summary>
        /// Startes the threads needed to print a given document depending if the document was A3 or A4
        /// </summary>
        /// <param name="thread">the computer thread name</param>
        /// <param name="doc">the document that will be printed</param>
        public void DocumentToPrint(string thread, Document doc)
        {
            if (doc.Form == "A4")
            {           
                // Can only be accessed by one A4 document thread at a time, since AutoResetEvent is set to true
                // the first thread will pass, the rest has to wait for Set().
                waitA4.WaitOne();
                Thread A4 = new Thread(() => PrintDocument(thread, doc, waitA4));             
                A4.Start();              
            }
            else
            {
                // Can only be accessed by one A3 document thread at a time, since AutoResetEvent is set to true
                // the first thread will pass, the rest has to wait for Set().
                waitA3.WaitOne();
                Thread A3 = new Thread(() => PrintDocument(thread, doc, waitA3));
                A3.Start();
            }
        }

        /// <summary>
        /// Prints the given document depending of the document form
        /// </summary>
        /// <param name="threadName">The name of the computer thread</param>
        /// <param name="doc">The document that is being print</param>
        /// <param name="waitEvent">The event that controls when the next document can be printed</param>
        public void PrintDocument(string threadName, Document doc, EventWaitHandle waitEvent)
        {
            // Adds the comupter name that already printed to the list
            allComputersThatPrinted.Add(threadName);
            Thread.Sleep(1000);
            Console.WriteLine("{0} user can receive the {1} document.", threadName, doc.Form);

            // Continue printing new documents until the last Computer finished
            if (allComputersThatPrinted.Distinct().Count() != 10)
            {
                // Start the thread with the same computer name as the one that finished printing
                Thread thread = new Thread(doc.CreateDocument)
                {
                    Name = threadName
                };
                thread.Start();

                // Lets the next document thread start
                waitEvent.Set();
            }
        }
    }
}
