using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<string> inputs = new List<string>();

        int n = stringToInt(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string text = Console.ReadLine();
            string[] words = text.Split(' ');
            foreach (string s in words)
            {
                inputs.Add(s);
            }
        }
        inputs = inputs.Distinct().ToList();
        Dictionary<string, List<string>> d = createDictionary(inputs);

        List<List<string>> anagrams = new List<List<string>>();

        anagrams = findAnagrams(inputs, d);
        foreach (List<string> l in anagrams)
        {
            l.Sort();
            foreach(string s in l)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine();
        }
        Console.ReadLine();
    }

    public static Dictionary<string, List<string>> createDictionary(List<string> words)
    {
        Dictionary<string, List<string>> d = new Dictionary<string, List<string>>();
        for (int i = 0; i < words.Count; i++)
        {
            char[] a = words[i].ToCharArray();
            Array.Sort(a);
            try
            {
                List<string> anagrams = new List<string>();
                anagrams.Add(new string(a));
                anagrams.Add(words[i]);
                d.Add(new string(a), anagrams);
            }
            catch (ArgumentException)
            {
                List<string> anagrams = d[new string(a)];
                anagrams.Add(words[i]);
                d[new string(a)] = anagrams;
            }
        }
        return d;
    }

    public static List<List<string>> findAnagrams(List<string> list, Dictionary<string, List<string>> d)
    {
        List<List<string>> Anagrams = new List<List<string>>();
        string[] arrayOfAllKeys = d.Keys.ToArray();
        Array.Sort(arrayOfAllKeys);

        for (int i = 0; i < arrayOfAllKeys.Length; i++)
        {
            if(d[arrayOfAllKeys[i]].Count > 2)
                Anagrams.Add(d[arrayOfAllKeys[i]]);
        }

        return Anagrams;
    }


    public static long stringToLong(string i)
    {
        long r;
        r = long.Parse(i);
        return r;
    }
    public static double stringToDouble(string i)
    {
        double r;
        r = long.Parse(i);
        return r;
    }
    public static int stringToInt(string i)
    {
        int r;
        r = int.Parse(i);
        return r;
    }



}


