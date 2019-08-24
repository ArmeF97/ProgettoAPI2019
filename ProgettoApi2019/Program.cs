using System;

namespace ProgettoApi2019
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Main2(args, true);
                return;
            }

            int nCicli = 1;
            for (int i = 0; i < nCicli; i++)
            {
                int n = GeneratoreClass.GeneraNumeroTest() + i;

                int righe = GeneratoreClass.GeneraRighe() + i;

                args = new string[] { "-g", n.ToString(), righe.ToString(), "N", "100000", "3", "Y", "1000" };
                Main2(args, false);
            }
        }

        private static void Main2(string[] args, bool interazioneFinale)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-g")
                {
                    GeneratoreClass.Generatore(args, interazioneFinale);
                    return;
                }
            }

            var r = SolveClass.Solve("input.txt", interazioneFinale);
            if (r != null)
            {
                foreach (var r2 in r)
                {
                    Console.WriteLine(r2);
                }

                ConsoleClass.ConsoleStampaConInterazioneUtente("", interazioneFinale);
            }
        }
    }
}