using System;

namespace ProgettoApi2019
{
    public static class RandomClass
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

        internal static int RandomInt(int v1, int v2)
        {
            if (rnd == null)
                rnd = new Random();

            return rnd.Next(v1, v2);
        }
    }
}