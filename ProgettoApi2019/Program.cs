using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    class Program
    {


        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Main2(args, true);
                return;
            }

            //De-commentare questa linea per forzare la generazione di test
            for (int i = 0; i < 10; i++)
            {
                int n = GeneratoreClass.GeneraNumeroTest() + i;

                int righe = GeneratoreClass.GeneraRighe() + i;

                args = new string[] { "-g", n.ToString(), righe.ToString() };
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
