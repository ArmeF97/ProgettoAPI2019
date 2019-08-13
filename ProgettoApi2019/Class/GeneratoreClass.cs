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
            return RandomClass.RandomInt(5,10);
        }

        internal static int GeneraRighe()
        {
            return RandomClass.RandomInt(100, 200) * RandomClass.RandomInt(1,4) * RandomClass.RandomInt(100,200) * RandomClass.RandomInt(2,5);
        }

        private static string GeneraDelRel(ref NomiClass gen)   //MODIFICATO
        {
            String nome1, nome2;
            int n1, n2;
            Random rand = new Random();
            n1 = rand.Next(11);
            n2 = rand.Next(11);
            if(n1 <= 7)
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
            char carattere3 = RandomClass.RandomLetteraRelazione();
            return "delrel " + nome1 + " " + nome2 + " \"" + carattere3 + "\"";
        }

        private static string GeneraAddRel(ref NomiClass gen)   //MODIFICATO
        {
            String nome1, nome2;
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
            char carattere3 = RandomClass.RandomLetteraRelazione();
            return "addrel " + nome1 + " " + nome2 + " \"" + carattere3 + "\"";
        }

        private static void GeneraTest(int avg_lines, bool interagisciFinale)
        {
            NomiClass genNomi = new NomiClass();    //AGGIUNTO
            List<string> L = new List<string>();
            for (int i=0; i<avg_lines; i++)
            {
                L.Add(GeneraLinea(ref genNomi));
            }
            L.Add("end");

            string output_path = DirectoryClass.GetOutputPath();
            File.WriteAllLines("i/" + output_path, L);
            var r = SolveClass.Solve("i/" + output_path, interagisciFinale);
            DirectoryClass.Save("o/" + output_path, r);
        }

        private static string GeneraLinea(ref NomiClass gen)
        {
            int choice = RandomClass.rnd.Next(0, 100);
            if (choice >= 0 && choice <= 20)
                return GeneraAddEnt(ref gen);
            if (choice > 20 && choice <= 60)
                return GeneraDelEnt(ref gen);
            if (choice > 60 && choice <= 85)
                return GeneraAddRel(ref gen);
            if (choice > 85 && choice <= 95)
                return GeneraDelRel(ref gen);
            if (choice > 95 && choice <= 100)
                return "report";

            throw new Exception();
        }

        private static string GeneraAddEnt(ref NomiClass gen)   //MODIFICATO
        {
            String nome = gen.GeneraNome();
            return "addent " + nome;
        }


        private static string GeneraDelEnt(ref NomiClass gen)   //MODIFICATO
        {
            String nome;
            Random rand = new Random();
            int n = rand.Next(11);
            if(n <= 5)
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