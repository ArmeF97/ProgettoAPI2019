using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    public static class GeneratoreClass
    {
        public static void Generatore(string[] args, bool interagisciFinale)
        {
            //=> syntax    .exe -g [NUM_TEST] [AVARAGE_LINES_PER_TEST]
            //=> example   .exe -g 100 150


            if (args.Length < 3)
            {
                ConsoleClass.ConsoleStampaConInterazioneUtente("Need more arguments", interagisciFinale);
                return;
            }

            RandomClass.rnd = new Random();
            DirectoryClass.CreaDirectory();

            int n_test = Convert.ToInt32(args[1]);
            int avg_lines = Convert.ToInt32(args[2]);
            for(int i=0; i< n_test; i++)
            {
                GeneraTest(avg_lines, interagisciFinale);
            }

            ConsoleClass.ConsoleStampaConInterazioneUtente(n_test + " generated.", interagisciFinale);
            return;

        }

        internal static int GeneraNumeroTest()
        {
            return RandomClass.RandomInt(10,100);
        }

        internal static int GeneraRighe()
        {
            return RandomClass.RandomInt(100, 200) * RandomClass.RandomInt(1,4) * RandomClass.RandomInt(100,200) * RandomClass.RandomInt(2,5);
        }

        private static string GeneraDelRel()
        {
            char carattere1 = RandomClass.RandomLettera();
            char carattere2 = RandomClass.RandomLettera();
            char carattere3 = RandomClass.RandomLetteraRelazione();
            return "delrel \"" + carattere1 + "\" \"" + carattere2 + "\" \"" + carattere3 + "\"";
        }

        private static string GeneraAddRel()
        {
            char carattere1 = RandomClass.RandomLettera();
            char carattere2 = RandomClass.RandomLettera();
            char carattere3 = RandomClass.RandomLetteraRelazione();
            return "addrel \"" + carattere1 + "\" \"" + carattere2 + "\" \"" + carattere3 + "\"";
        }

        private static void GeneraTest(int avg_lines, bool interagisciFinale)
        {
            List<string> L = new List<string>();
            for (int i=0; i<avg_lines; i++)
            {
                L.Add(GeneraLinea());
            }
            L.Add("end");

            string output_path = DirectoryClass.GetOutputPath();
            File.WriteAllLines("i/" + output_path, L);
            var r = SolveClass.Solve("i/" + output_path, interagisciFinale);
            DirectoryClass.Save("o/" + output_path, r);
        }

        private static string GeneraLinea()
        {
            int choice = RandomClass.rnd.Next(0, 100);
            if (choice >= 0 && choice <= 20)
                return GeneraAddEnt();
            if (choice > 20 && choice <= 60)
                return GeneraDelEnt();
            if (choice > 60 && choice <= 85)
                return GeneraAddRel();
            if (choice > 85 && choice <= 95)
                return GeneraDelRel();
            if (choice > 95 && choice <= 100)
                return "report";

            throw new Exception();
        }

        private static string GeneraAddEnt()
        {
            int lettera = RandomClass.rnd.Next(0, 26);
            lettera += 'a';
            char carattere = (char)lettera;
            return "addent \""+carattere+"\"";
        }


        private static string GeneraDelEnt()
        {
            char carattere = RandomClass.RandomLettera();
            return "delent \"" + carattere + "\"";
        }

    }
}