using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    class Computer
    {
        public void AddComputers()
        {
            Document doc = new Document();

            for (int i = 1; i < 11; i++)
            {
                Thread comp = new Thread(doc.CreateDocument)
                {
                    Name = "Computer_" + i
                };

                Program.allComputers.Add(comp);
            }

            foreach (var item in Program.allComputers)
            {
                item.Start();
            }
        }
    }
}
