using System;
using System.Collections.Generic;

class breuk
{
    public double value, noemer, teller;

    public breuk(double Teller, double Noemer)
    {
        value = 0;
        noemer = Noemer;
        teller = Teller;
        if(noemer != 0)
            value = teller / noemer;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Random rng = new Random();
        long values, k;
        List<breuk> breuken = new List<breuk>();
        string[] temp = new string[1];
        string q;
        q = Console.ReadLine();
        temp = q.Split(' ');

        values = stringToLong(temp[0]);
        k = stringToLong(temp[1]);
        for (int i = 0; i < values; i++)
        {
            /*int r1 = rng.Next(1, 20);
            int r2 = rng.Next(1, 10);
            breuken.Add(new breuk(r1 , r2));
            Console.WriteLine(r1 + " " + r2); //Debugging aid*/
            q = Console.ReadLine();
            temp = q.Split(' ');
            breuken.Add(new breuk(stringToDouble(temp[0]), stringToDouble(temp[1])));
        }

        MyQuickSort(breuken, 0, breuken.Count, (int)k);

        Console.WriteLine(values);
        for (int i = 0; i < values; i++)
        {
            //Console.WriteLine(breuken[i].teller + " " + breuken[i].noemer+ " " + breuken[i].value);
            //if (breuken[i].value == breuken[i + 1].value)//debugging aid
            //{

                //Console.WriteLine(breuken[i].value + " " + breuken[i].teller + " " + breuken[i].noemer);
                //if (breuken[i + 1].value != breuken[i + 2].value) {
                //Console.WriteLine(breuken[i + 1].value + " " + breuken[i + 1].teller + " " + breuken[i + 1].noemer);//Debugging aid
                //Console.WriteLine();
                //}
            //}

            Console.WriteLine(breuken[i].teller + " " + breuken[i].noemer);
        }
        Console.ReadLine();

        }
    static int MyPartition(List<breuk> list, int left, int right)
    {
        int start = left;
        double pivot = list[start].value;
        breuk pivotContents = list[start];
        left++;
        right--;

        while (true)
        {
            while (left <= right && list[left].value < pivot)
                left++;

            while (left <= right && list[left].value == pivot && list[left].teller <= pivotContents.teller)
                left++;

            while (left <= right && list[right].value > pivot)
                right--;

            while (left <= right && list[right].value == pivot && list[right].teller > pivotContents.teller)
                right--;

            if (left > right)
            {
                list[start] = list[left - 1];
                list[left - 1] = pivotContents;

                return left;
            }

            breuk temp = list[left];
            list[left] = list[right];
            list[right] = temp;
        }
        
    }

    static void MyQuickSort(List<breuk> list, int left, int right, int k)
    {
        if (list == null || list.Count <= 1)
            return;

        if (k > right - left)
        {
            MySelectionSort(list, left, right);
        }
        else
        {
            if (left < right)
            {
                int pivotIdx = MyPartition(list, left, right);
                //Console.WriteLine("MQS Left :" + left + " Right" + right);
                MyQuickSort(list, left, pivotIdx - 1, k);
                MyQuickSort(list, pivotIdx, right, k);
            }
        }
    }
    static void MySelectionSort(List<breuk> list, int left, int right)
    {
        for(int i = 0 + left; i < right - 1; i++)
        {
            int indexMin = i;
            for (int j = i + 1; j < right; j++)
            {
                if(list[j].value < list[indexMin].value)
                {
                    indexMin = j;
                }
                else if (list[j].value == list[indexMin].value && list[j].teller < list[indexMin].teller)
                {
                    indexMin = j;
                }
            }
            if (indexMin != i)
            {
                breuk temp = list[i];
                list[i] = list[indexMin];
                list[indexMin] = temp;
            }
        }
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


}


