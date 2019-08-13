using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    public static class GeneratoreClass
    {
        public static void Generatore(string[] args, bool interagisciFinale)
        {
            //=> syntax    .exe -g [NUM_TEST] [AVARAGE_LINES_PER_TEST] [LETTERE (Y/N)]
            //=> example   .exe -g 100 150 Y

            if (args.Length < 3)
            {
                ConsoleClass.ConsoleStampaConInterazioneUtente("Need more arguments", interagisciFinale);
                return;
            }

            RandomClass.rnd = new Random();
            DirectoryClass.CreaDirectory();

            int n_test = Convert.ToInt32(args[1]);
            int avg_lines = Convert.ToInt32(args[2]);
            bool lettere = args[3].ToString().ToLower() == "y";
            for (int i = 0; i < n_test; i++)
            {
                GeneraTest(avg_lines, interagisciFinale, lettere);
            }

            ConsoleClass.ConsoleStampaConInterazioneUtente(n_test + " generated.", interagisciFinale);
            return;
        }

        internal static int GeneraNumeroTest()
        {
            return RandomClass.RandomInt(10, 100);
        }

        internal static int GeneraRighe()
        {
            return RandomClass.RandomInt(100, 200) * RandomClass.RandomInt(1, 4) * RandomClass.RandomInt(100, 200) * RandomClass.RandomInt(2, 5);
        }

        private static string GeneraDelRel(ref NomiClass gen, bool lettere)
        {
            char carattere3 = RandomClass.RandomLetteraRelazione();

            if (lettere)
            {
                char carattere1 = RandomClass.RandomLettera();
                char carattere2 = RandomClass.RandomLettera();

                return "delrel \"" + carattere1 + "\" \"" + carattere2 + "\" \"" + carattere3 + "\"";
            }

            string nome1, nome2;
            int n1, n2;
            Random rand = new Random();
            n1 = rand.Next(11);
            n2 = rand.Next(11);
            if (n1 <= 7)
            {
                nome1 = gen.GeneraNomeUsato();
            }
            else
            {
                nome1 = gen.NomeCasuale();
            }
            if (n2 <= 6)
            {
                nome2 = gen.GeneraNomeUsato();
            }
            else
            {
                nome2 = gen.NomeCasuale();
            }

            return "delrel " + nome1 + " " + nome2 + " \"" + carattere3 + "\"";
        }

        private static string GeneraAddRel(ref NomiClass gen, bool lettere)
        {
            char carattere3 = RandomClass.RandomLetteraRelazione();

            if (lettere)
            {
                char carattere1 = RandomClass.RandomLettera();
                char carattere2 = RandomClass.RandomLettera();
                return "addrel \"" + carattere1 + "\" \"" + carattere2 + "\" \"" + carattere3 + "\"";
            }

            string nome1, nome2;
            int n1, n2;
            Random rand = new Random();
            n1 = rand.Next(11);
            n2 = rand.Next(11);
            if (n1 <= 8)
            {
                nome1 = gen.GeneraNomeUsato();
            }
            else
            {
                nome1 = gen.NomeCasuale();
            }
            if (n2 <= 7)
            {
                nome2 = gen.GeneraNomeUsato();
            }
            else
            {
                nome2 = gen.NomeCasuale();
            }
            return "addrel " + nome1 + " " + nome2 + " \"" + carattere3 + "\"";
        }

        private static void GeneraTest(int avg_lines, bool interagisciFinale, bool lettere)
        {
            NomiClass genNomi = new NomiClass();
            List<string> L = new List<string>();
            for (int i = 0; i < avg_lines; i++)
            {
                L.Add(GeneraLinea(ref genNomi, lettere));
            }
            L.Add("end");

            string output_path = DirectoryClass.GetOutputPath();
            File.WriteAllLines("i/" + output_path, L);
            var r = SolveClass.Solve("i/" + output_path, interagisciFinale);
            DirectoryClass.Save("o/" + output_path, r);
        }

        private static string GeneraLinea(ref NomiClass gen, bool lettere)
        {
            int choice = RandomClass.rnd.Next(0, 100);
            if (choice >= 0 && choice <= 20)
                return GeneraAddEnt(ref gen, lettere);
            if (choice > 20 && choice <= 60)
                return GeneraDelEnt(ref gen, lettere);
            if (choice > 60 && choice <= 85)
                return GeneraAddRel(ref gen, lettere);
            if (choice > 85 && choice <= 95)
                return GeneraDelRel(ref gen, lettere);
            if (choice > 95 && choice <= 100)
                return "report";

            throw new Exception();
        }

        private static string GeneraAddEnt(ref NomiClass gen, bool lettere)
        {
            if (lettere)
            {
                int lettera = RandomClass.rnd.Next(0, 26);
                lettera += 'a';
                char carattere = (char)lettera;
                return "addent \"" + carattere + "\"";
            }
            string nome = gen.GeneraNome();
            return "addent " + nome;
        }

        private static string GeneraDelEnt(ref NomiClass gen, bool lettere)
        {
            if (lettere)
            {
                char carattere = RandomClass.RandomLettera();
                return "delent \"" + carattere + "\"";
            }

            string nome;
            Random rand = new Random();
            int n = rand.Next(11);
            if (n <= 5)
            {
                nome = gen.GeneraNomeUsato();
            }
            else
            {
                nome = gen.NomeCasuale();
            }
            gen.RimuoviDaUsati(nome);
            return "delent " + nome;
        }
    }
}