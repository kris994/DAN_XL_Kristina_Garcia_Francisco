using System.Threading;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that creates all comuter threads used for inital printing
    /// </summary>
    class Computer
    {
        /// <summary>
        /// Creates all computer threads and starts them at the same time
        /// </summary>
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
