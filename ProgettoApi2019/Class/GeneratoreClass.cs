using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    public static class GeneratoreClass
    {
        public static void Generatore(string[] args, bool interagisciFinale)
        {
            //=> syntax    .exe -g [NUM_TEST] [AVARAGE_LINES_PER_TEST] [LETTERE (Y/N)] [MILLISECOND_TIME_LIMIT, 0 = NONE] [RANDOM_CHANCES_LINES (Y/N)] [N_TEST_IS_MILLISECONDS (Y/N)] [N_SCALE_MILLISECONDS]
            //=> example   .exe -g 100 150 Y 0

            /* Spiegazione parametri
             * NUM_TEST => intero => numero dei test, il numero dei file prodotti
             * AVARAGE_LINES_PER_TEST => intero => numero medio di righe per ogni test/file. Questo numero potrebbe essere alzato a seconda del valore di MILLISECOND_TIME_LIMIT
             * LETTERE => char (Y/N) => indica se per relazioni e entità si vogliono usare delle semplici lettere (Y) oppure dei nomi (N)
             * MILLISECOND_TIME_LIMIT => intero => valore minimo di esecuzione. Esempio: se metto 2500, i test prodotti devono essere stati risolti dal generatore in almeno 2,5 secondi
             *                                      questo valore può alterare AVARAGE_LINES_PER_TEST, facendolo aumentare, se dopo un po' di tentativi non si riesce a produrre dei test validi.
             *                                      Se date un valore "alto" di MILLISECOND_TIME_LIMIT, è consigliato non dare un valore basso di AVARAGE_LINES_PER_TEST, così da guadagnare tempo
             * RANDOM_CHANCES_LINES => intero => se 1, le probabilità che scriva DELREL, DELENT, ecc saranno anch'esse casuali, se 2 saranno standard come nel codice.
             *                                      Se 3, si testerà dropOff, creando tante linee add e poi tante del
             * N_TEST_IS_MILLISECONDS => char (Y/n) => se N, funziona normale. Se Y, il valore NUM_TEST sarà ignorato, e sarà pari ai millisecondi. Saranno prodotti tot test quanti i millisecondi
             * N_SCALE_MILLISECONDS => intero => di quanto scala se Y il valore N_TEST_IS_MILLISECONDS. Se vuoi arrivare fino a 10000 millisecondi, ma vuoi arrivarci di secondo in secondo, allora
             *                                      N_SCALE_MILLISECONDS deve valere 1000
             */

            if (args.Length < 7)
            {
                ConsoleClass.ConsoleStampaConInterazioneUtente("Need more arguments", interagisciFinale);
                return;
            }

            RandomClass.rnd = new Random();
            DirectoryClass.CreaDirectory();

            int n_test = Convert.ToInt32(args[1]);
            int avg_lines = Convert.ToInt32(args[2]);
            bool lettere = args[3].ToString().ToLower() == "y";
            int millseconds = Convert.ToInt32(args[4]);
            int random_chances_lines = Convert.ToInt32(args[5]);
            bool n_test_is_milliseconds = args[6].ToString().ToLower() == "y";
            int scale_milliseconds = Convert.ToInt32(args[7]);
            int failed = 0;

            if (n_test_is_milliseconds)
            {
                n_test = millseconds;
            }
            else
            {
                scale_milliseconds = 1;
            }

            int start_i = 1;
            for (int i = start_i; i <= n_test; i += scale_milliseconds)
            {
                bool valido;

                if (n_test_is_milliseconds)
                    valido = GeneraTest(avg_lines, interagisciFinale, lettere, i, random_chances_lines);
                else
                    valido = GeneraTest(avg_lines, interagisciFinale, lettere, millseconds, random_chances_lines);

                if (!valido)
                {
                    Grow_lines(ref avg_lines, ref failed);

                    i -= scale_milliseconds;

                    if (i < start_i)
                        i = start_i;
                }
            }

            ConsoleClass.ConsoleStampaConInterazioneUtente(n_test + " generated.", interagisciFinale);
            return;
        }

        private static void Grow_lines(ref int avg_lines, ref int failed)
        {
            failed++;

            avg_lines++;
            int r2 = RandomClass.rnd.Next(0, 1000);
            if (r2 < 5)
            {
                avg_lines++;
                avg_lines = (int)(avg_lines * 1.01d);
                avg_lines++;
            }
            else if (r2 < 10)
            {
                avg_lines++;
                avg_lines = (int)(avg_lines * 1.001d);
                avg_lines++;
            }
            else if (r2 < 30)
            {
                avg_lines++;
                avg_lines = (int)(avg_lines * 1.0001d);
                avg_lines++;
            }
            avg_lines++;

            if (failed > 100)
            {
                failed = 0;

                avg_lines++;
                avg_lines = (int)(avg_lines * 1.5d);
                avg_lines++;
            }
        }

        internal static int GeneraNumeroTest()
        {
            return RandomClass.RandomInt(5, 50);
        }

        internal static int GeneraRighe()
        {
            return RandomClass.RandomInt(50, 200) * RandomClass.RandomInt(1, 4) * RandomClass.RandomInt(100, 200) * RandomClass.RandomInt(2, 5);
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

        private static bool GeneraTest(int avg_lines, bool interagisciFinale, bool lettere, int millseconds, int random_chances_lines)
        {
            List<int> random_chances_lines2 = null;
            NomiClass genNomi = new NomiClass();
            List<string> L = new List<string>();

            if (random_chances_lines == 1)
            {
                random_chances_lines2 = GetRandomListRandomChances();
                for (int i = 0; i < avg_lines; i++)
                {
                    L.Add(GeneraLinea(ref genNomi, lettere, random_chances_lines2, null));
                }
                L.Add("end");
                return GeneraTest2(ref L, ref interagisciFinale, ref millseconds);
            }
            else if (random_chances_lines == 3)
            {
                for (int i = 0; i < avg_lines / 2; i++)
                {
                    L.Add(GeneraLinea(ref genNomi, lettere, random_chances_lines2, true));
                }
                L.Add("report");
                for (int i = 0; i < avg_lines / 2; i++)
                {
                    L.Add(GeneraLinea(ref genNomi, lettere, random_chances_lines2, false));
                }
                L.Add("end");
                return GeneraTest2(ref L, ref interagisciFinale, ref millseconds);
            }
            else
            {
                for (int i = 0; i < avg_lines; i++)
                {
                    L.Add(GeneraLinea(ref genNomi, lettere, random_chances_lines2, null));
                }
                L.Add("end");
                return GeneraTest2(ref L, ref interagisciFinale, ref millseconds);
            }
        }

        private static bool GeneraTest2(ref List<string> L, ref bool interagisciFinale, ref int millseconds)
        {
            string output_path = DirectoryClass.GetOutputPath();
            File.WriteAllLines("i/" + output_path, L);
            DateTime d1 = DateTime.Now;
            var r = SolveClass.Solve("i/" + output_path, interagisciFinale);
            DateTime d2 = DateTime.Now;

            if (TestValido(d1, d2, millseconds))
            {
                DirectoryClass.Save("o/" + output_path, r);
                return true;
            }
            else
            {
                File.Delete("i/" + output_path);
                return false;
            }
        }

        private static bool TestValido(DateTime d1, DateTime d2, int millseconds)
        {
            TimeSpan span = d2 - d1;
            int ms = (int)span.TotalMilliseconds;
            if (ms >= millseconds)
                return true;
            else if (ms >= 3000)
                return false;

            return false;
        }

        private static List<int> GetRandomListRandomChances()
        {
            List<int> l = new List<int>();
            while (l.Count < 4)
            {
                int r = RandomClass.rnd.Next(0, 100);
                if (r == 0 || r == 100)
                    continue;

                if (l.Contains(r))
                    continue;

                l.Add(r);
            }

            l.Sort();
            return l;
        }

        private static string GeneraLinea(ref NomiClass gen, bool lettere, List<int> random_chances_lines, bool? p)
        {
            if (p == null)
            {
                if (random_chances_lines == null)
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
                }
                else
                {
                    int choice = RandomClass.rnd.Next(0, 100);
                    if (choice >= 0 && choice <= random_chances_lines[0])
                        return GeneraAddEnt(ref gen, lettere);
                    if (choice > random_chances_lines[0] && choice <= random_chances_lines[1])
                        return GeneraDelEnt(ref gen, lettere);
                    if (choice > random_chances_lines[1] && choice <= random_chances_lines[2])
                        return GeneraAddRel(ref gen, lettere);
                    if (choice > random_chances_lines[2] && choice <= random_chances_lines[3])
                        return GeneraDelRel(ref gen, lettere);
                    if (choice > random_chances_lines[3] && choice <= 100)
                        return "report";
                }
            }

            if (p.Value)
            {
                int choice = RandomClass.rnd.Next(0, 100);
                if (choice >= 0 && choice <= 45)
                    return GeneraAddEnt(ref gen, lettere);
                if (choice > 45 && choice <= 95)
                    return GeneraAddRel(ref gen, lettere);

                return "report";
            }
            else
            {
                int choice = RandomClass.rnd.Next(0, 100);
                if (choice > 0 && choice <= 45)
                    return GeneraDelEnt(ref gen, lettere);
                if (choice > 45 && choice <= 95)
                    return GeneraDelRel(ref gen, lettere);

                return "report";
            }

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