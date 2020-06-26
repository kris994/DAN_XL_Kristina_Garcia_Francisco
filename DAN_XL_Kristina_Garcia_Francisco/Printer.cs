using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    class Printer
    {
        static EventWaitHandle waitA4 = new AutoResetEvent(true);
        static EventWaitHandle waitA3 = new AutoResetEvent(true);

        public static List<string> allComputersThatPrinted = new List<string>();

        public void DocumentToPrint(string thread, Document doc)
        {
            if (doc.Form == "A4")
            {
                waitA4.WaitOne();
                Thread A4 = new Thread(() => StartPrinter(doc, thread));             
                A4.Start();              
            }
            else
            {
                waitA3.WaitOne();
                Thread A3 = new Thread(() => StartPrinter(doc, thread));
                A3.Start();
            }
        }

        public void StartPrinter(Document doc, string threadName)
        {
            if (doc.Form == "A4")
            {
                allComputersThatPrinted.Add(threadName);
                Thread.Sleep(1000);
                Console.WriteLine("{0} user can receive the {1} document.", threadName, doc.Form);

                if (allComputersThatPrinted.Distinct().Count() != 10)
                {                  
                    Thread thread = new Thread(doc.CreateDocument)
                    {
                        Name = threadName
                    };
                    thread.Start();

                    waitA4.Set();
                }           
            }
            else
            {
                allComputersThatPrinted.Add(threadName);
                Thread.Sleep(1000);
                Console.WriteLine("{0} user can receive the {1} document.", threadName, doc.Form);

                if (allComputersThatPrinted.Distinct().Count() != 10)
                {                  
                    Thread thread = new Thread(doc.CreateDocument)
                    {
                        Name = threadName
                    };
                    thread.Start();

                    waitA3.Set();
                }
            }
        }
    }
}
