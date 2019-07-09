using System;

namespace ProgettoApi2019
{
    public static class ConsoleClass
    {
        public static void ConsoleStampaConInterazioneUtente(string v)
        {
            Console.Write(v);
            try{
                Console.ReadKey();
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch
            {
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body

            }
        }

    }
}