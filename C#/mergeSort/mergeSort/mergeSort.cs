
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

public class Mergesort
{

    static List<int> copyArray(List<int> list)
    {
        List<int> newList = new List<int>();

        foreach (int i in list)
        {
            newList.Add(i);
        }

        return newList;
    }

    static void runSort(int n, string infil, string utfil)
    {
        List<int> ogList = new List<int>();

        //Läs in listan från fil
        string[] text = File.ReadAllLines(infil, Encoding.UTF8);
            
        foreach (string i in text)
        {
            ogList.Add(int.Parse(i));
        }

        string[] times = new string[n];

        for (int i = 0; i < n; i++)
        {
            List<int> list = copyArray(ogList);

            long start = 1000L * Stopwatch.GetTimestamp();
            merge(0, list.Count - 1, list);
            long stop = 1000L * Stopwatch.GetTimestamp();

            long diff = (stop - start);

            long micro = diff / TimeSpan.TicksPerMillisecond;

            times[i] = micro.ToString();
        }

        File.WriteAllLines(utfil, times);
    }

    private static void merge(int a, int b, List<int> list)
    {
        int mid = (a + b) / 2;
        if (a != b)
        {

            merge(a, mid, list);
            merge(mid + 1, b, list);

            int left = a; int right = mid + 1; List<int> tmp = new List<int>();
            while (left <= mid && right <= b)
            {
                if (list[left] < list[right])
                {
                    tmp.Add(list[left]);
                    left++;
                }
                else
                {
                    tmp.Add(list[right]);
                    right++;
                }
            }

            if (right > b)
            {
                while (left <= mid)
                {
                    tmp.Add(list[left]);
                    left++;
                }
            }

            for (int i = 0; i < tmp.Count; i++)
            {
                list[i + a] = tmp[i];
            }

        }
    }

    public static void main(string[] args)
    {
        runSort(int.Parse(args[0]), ".\\" + args[1], ".\\" + args[2]);
    }
}