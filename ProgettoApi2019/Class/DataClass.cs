using System;

namespace ProgettoApi2019
{
    public static class DataClass
    {
    
        public static string GetDataOra()
        {
            return DateTime.Now.Year + "_" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "_" +
            DateTime.Now.Day.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" +
            DateTime.Now.Minute.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "_" +
            DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
        }
    }
}