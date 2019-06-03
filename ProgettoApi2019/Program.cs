using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    class Program
    {


        static void Main(string[] args)
        {
            //De-commentare questa linea per forzare la generazione di test
            //args = new string[] { "-g", "1", "500000"};

            if (args.Length>0)
            {
                if (args[0] == "-g")
                {
                    GeneratoreClass.Generatore(args);
                    return;
                }
            }

            var r = SolveClass.Solve("input.txt");
            if (r != null)
            {
                foreach(var r2 in r)
                {
                    Console.WriteLine(r2);
                }

                ConsoleClass.ConsoleStampaConInterazioneUtente("");
            }
        }


        

        


    }
}
