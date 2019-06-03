using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    public class DirectoryClass
    {
        
        public static void CreaDirectory()
        {
            ProvaCreaDirectory("o");
            ProvaCreaDirectory("i");
        }

        public static void ProvaCreaDirectory(string v)
        {
            if (Directory.Exists(v))
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(v);
            }
            catch
            {
                ;
            }
        }


        public static string GetOutputPath()
        {
            return DataClass.GetDataOra() + ".txt";
        }



        public static void Save(string v, List<string> r)
        {
            File.WriteAllLines(v, r);
        }


    }
}