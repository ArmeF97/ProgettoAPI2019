using System;
using System.Collections.Generic;
using System.IO;

namespace ProgettoApi2019
{
    public static class SolveClass
    {

        // a è amico di b

                                //amico_di          //b       //lista di a
        public static Dictionary<string, Dictionary<string, List<string> >> relazioni;
        public static List<string> entita;

        public static List<string> Solve(string v, bool interagisciFinale)
        {
            entita = new List<string>();
            relazioni = new Dictionary<string, Dictionary<string, List<string>>>();
            string[] s = null;
            try
            {
                s = File.ReadAllLines(v);
            }
            catch
            {
                ConsoleClass.ConsoleStampaConInterazioneUtente("Input file not found", interagisciFinale);
                return null;
            }

            List<string> list = new List<string>();

            foreach (string s2 in s)
            {
                if (s2.StartsWith("addrel", StringComparison.Ordinal))
                {
                    AddRel(s2);
                }
                else if (s2.StartsWith("addent", StringComparison.Ordinal))
                {
                    AddEnt(s2);
                }
                else if (s2.StartsWith("delent", StringComparison.Ordinal))
                {
                    DelEnt(s2);
                }
                else if (s2.StartsWith("delrel", StringComparison.Ordinal))
                {
                    DelRel(s2);
                }
                else if (s2.StartsWith("report", StringComparison.Ordinal))
                {
                    list.Add(Report());
                }
                else if (s2.StartsWith("end", StringComparison.Ordinal))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("UNEXPECTED INPUT: " + s2);
                }
            }

            return list;
        }



        private static void DelRel(string s2)
        {
            string[] s = s2.Split(' ');
            if (relazioni.ContainsKey(s[3]) && relazioni[s[3]].ContainsKey(s[2]) && relazioni[s[3]][s[2]].Contains(s[1]))
            {
                relazioni[s[3]][s[2]].Remove(s[1]);
            }
        }

        private static void DelEnt(string s2)
        {
            string[] s = s2.Split(' ');
            foreach (var x in relazioni.Keys)
            {
                relazioni[x].Remove(s[1]);

                foreach(var x2 in relazioni[x].Keys)
                {
                    relazioni[x][x2].Remove(s[1]);
                }
            }

            entita.Remove(s[1]);
        }

        private static void AddEnt(string s2)
        {
            string[] s = s2.Split(' ');
            if (!entita.Contains(s[1]))
                entita.Add(s[1]);
        }

        private static void AddRel(string s2)
        {
            string[] s = s2.Split(' ');

            if (!(entita.Contains(s[1]) && entita.Contains(s[2])))
            {
                return;
            }

            if (!relazioni.ContainsKey(s[3]))
            {
                relazioni.Add(s[3], new Dictionary<string, List<string>>());
            }

            if (!relazioni[s[3]].ContainsKey(s[2]))
            {
                relazioni[s[3]].Add(s[2], new List<string>());
            }

            if (!relazioni[s[3]][s[2]].Contains(s[1]))
            {
                relazioni[s[3]][s[2]].Add(s[1]);
            }
        }


        private static string Report()
        {
            string output = "";

            List<string> r = new List<string>();
            foreach (var x in relazioni.Keys)
            {
                r.Add(x);
            }
            r.Sort();

            foreach (var x in r)
            {
                List<string> champions = new List<string>();
                int max = -1;

                foreach (var x2 in relazioni[x].Keys)
                {
                    if (relazioni[x][x2].Count>max)
                    {
                        max = relazioni[x][x2].Count;
                        champions.Clear();
                        champions.Add(x2);
                    }
                    else if (relazioni[x][x2].Count == max)
                    {
                        champions.Add(x2);
                    }
                }

                if (max > 0)
                {
                    champions.Sort();

                    output += x;
                    foreach (var x2 in champions)
                    {
                        output += (" " + x2);
                    }

                    output += (" " + max + "; ");
                }
            }
            output = output.TrimEnd();

            if (String.IsNullOrEmpty(output))
                return "none";

            return output;
        }

    }
}