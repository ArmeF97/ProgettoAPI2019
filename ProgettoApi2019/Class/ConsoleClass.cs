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
            catch{
                ;
            }
        }

    }
}