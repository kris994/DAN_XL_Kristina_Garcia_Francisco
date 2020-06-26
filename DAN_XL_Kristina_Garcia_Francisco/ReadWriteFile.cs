using System.Collections.Generic;
using System.IO;

namespace DAN_XL_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Reads or Writes from the file
    /// </summary>
    class ReadWriteFile
    {
        /// <summary>
        /// Write a single line to the file
        /// </summary>
        public void WriteToFile(string file, List<string> list)
        {
            // Save all elements of the list to file
            using (StreamWriter streamWriter = new StreamWriter(file))
            {
                foreach (var item in list)
                {
                    streamWriter.WriteLine(item);
                }              
            }
        }
    }
}
