using System;

namespace ProgettoApi2019
{
    public class RandomClass
    {
        public static Random rnd;

        public static char RandomLettera()
        {
            int lettera = rnd.Next(0, 26);
            lettera += 'a';
            char carattere = (char)lettera;
            return carattere;
        }
        
        public static char RandomLetteraRelazione()
        {
            int lettera = rnd.Next(0, 5);
            lettera += 'a';
            char carattere = (char)lettera;
            return carattere;
        }
    }
}