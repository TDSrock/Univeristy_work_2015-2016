using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        long values, closestHeight, schijfjes = 0;
        values = stringToLong(Console.ReadLine());
        long[] standaardMaaten = new long[values + 1];
        standaardMaaten[0] = 0;
        for(long i =0; i < values; i++)
        {
            standaardMaaten[i + 1] = stringToLong(Console.ReadLine());
        }
        long values2 = stringToLong(Console.ReadLine());
        long[] hoogteTeTimerren = new long[values2];
        for(long j =0; j < values2; j++)
        {
            hoogteTeTimerren[j] = stringToLong(Console.ReadLine());
        }
        for(long k = 0; k < hoogteTeTimerren.Length; k++)
        {
            long index = binarySearch(standaardMaaten, hoogteTeTimerren[k], 0, hoogteTeTimerren.Length - 1);
            //int index = Array.BinarySearch(standaardMaaten, hoogteTeTimerren[k]);
            if (index < 0)
            {
                index = ~index;
            } else
            {
                //closestHeight = 0;
            }
            closestHeight = standaardMaaten[index];
            //Console.WriteLine("hoogteTetimerren :" + hoogteTeTimerren[k] + " closestHeight :" + closestHeight + " Index was : " + index);
            schijfjes += hoogteTeTimerren[k] - closestHeight;
            
        }
        Console.WriteLine(schijfjes);
       // Console.ReadLine();
    }

    public static long binarySearch(long[] inputArray, long key, long min, long max)
    {
        while (min <= max)
        {
            long mid = (min + max) / 2;
            if (key == inputArray[mid])
            {
                return ++mid;
            }
            else if (key < inputArray[mid])
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }
        return -1;
    }

    public static long stringToLong(string i)
    {
        long r;
        try
        {
            r = long.Parse(i);
        }
        catch (FormatException e)
        {
            //So we caught an error, lets clean up the input based on the limitions posed by the course.
            string[] temp = new string[10];
            temp = i.Split(' ');
            return stringToLong(temp[0]);
        }
        return r;
    }
}

