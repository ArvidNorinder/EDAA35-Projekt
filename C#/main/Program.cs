using System;
using System.Collections;
using System.Collections.Generic;

namespace main
{
    class Program
    {
        static List<int> copyArray(List<int> list)
        {
            return null;
        }


        static void Main(string[] args)
        {
            string infil = "Data.txt";
            string utfil = "utfil.txt";
            string pathToFiles = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/EDAA35-Projekt/C#/main/";
            int repetitions = 600;
            string[] lines = System.IO.File.ReadAllLines(pathToFiles + infil);
            System.IO.File.WriteAllLines(pathToFiles + utfil, lines);
            Console.WriteLine();
        }
    }
}
