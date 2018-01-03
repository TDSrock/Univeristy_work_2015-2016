using System;
using System.Collections.Generic;
using System.Linq;

class MinHeap<T> where T : IComparable          //Zero based heap
{
    private List<T> data = new List<T>();

    public void Insert(T o)
    {
        data.Add(o);

        int i = data.Count - 1;
        while (i > 0)
        {
            int j = (i + 1) / 2 - 1;

            // Check if the invariant holds for the element in data[i]
            T v = data[j];
            if (v.CompareTo(data[i]) < 0 || v.CompareTo(data[i]) == 0)
            {
                break;
            }

            // Swap the elements
            T tmp = data[i];
            data[i] = data[j];
            data[j] = tmp;

            i = j;
        }
    }

    public T ExtractMin()
    {
        if (data.Count < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        T min = data[0];
        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);
        this.MinHeapify(0);
        return min;
    }

    public T Peek()
    {
        return data[0];
    }

    public int Count
    {
        get { return data.Count; }
    }

    private void MinHeapify(int i)
    {
        int smallest;
        int l = 2 * (i + 1) - 1;
        int r = 2 * (i + 1) - 1 + 1;

        if (l < data.Count && (data[l].CompareTo(data[i]) < 0))
        {
            smallest = l;
        }
        else
        {
            smallest = i;
        }

        if (r < data.Count && (data[r].CompareTo(data[smallest]) < 0))
        {
            smallest = r;
        }

        if (smallest != i)
        {
            T tmp = data[i];
            data[i] = data[smallest];
            data[smallest] = tmp;
            this.MinHeapify(smallest);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MinHeap<int> eventHeap = new MinHeap<int>();
        Stack<int> toCutStack = new Stack<int>();//put customer ID's here, customer ID's are their time of entrance
        Queue<int>[] printers = new Queue<int>[3];
        Queue<int> printerQueueA = new Queue<int>();
        Queue<int> printerQueueB = new Queue<int>();
        Queue<int> printerQueueC = new Queue<int>();
        Queue<int[]> Events = new Queue<int[]>();
        printers[0] = printerQueueA;
        printers[1] = printerQueueB;
        printers[2] = printerQueueC;
        int numbOfEvents, queueInQuestion, customerCutTime;
        bool cutting = false;
        string evenT;
        List<string> eventList;
        int[] longestQueuedCustomer = new int[2];
        int[] longestWaitedCustomer = new int[2];//for the customer who waited the longest AFTER print
        int[] longestInShopCustomer = new int[2];
        bool firstQueued = true;
        int[] tempValue = new int[4];

        //Customer ID's are the key, Cusomter ID's are the time of entrance, 
        //int[] will containt in this order the data: [CustomerEntranceNumber, CustomerPrintTime, CustomerCutTime, CustomerPrintTimeDone]
        //CustomerEntranceNumber is 1 based and the line count in which they show up in the input
        int CustomerEntranceNumber = 1, CustomerID, CustomerPrintTime, CustomerCutTime;
        
        Dictionary<int, int[]> customerDictionary = new Dictionary<int, int[]>();

        //Key will represent the time of the event, List will maintain the tasks at that moment Each string Laid out like this:
        // "1 A 7" where 1 is the customer ID, where A is the task and 7 is the time till task is completed
        Dictionary<int, List<string>> eventDictionary = new Dictionary<int, List<string>>();
        /*Event nameing scheme
        n : Customer enters, In does not have a time to complete, when in occurs the new cusomter will enqueue except if there is an empty queue then he starts printing imediatly(through sp event)
        p1 : printer a is done
        p2 : printer b is done
        p3 : printer c is done
        cd : cutting is done
        */

        //handle all inputs
        string[] input;
        input = Console.ReadLine().Split();
        int t = 0;
        while(input[0] != "sluit")
        {
            //build int[] for the dictionary
            CustomerID = stringToInt(input[0]);
            CustomerPrintTime = stringToInt(input[1]);
            CustomerCutTime = stringToInt(input[2]);
            int[] value = { CustomerEntranceNumber, CustomerPrintTime, CustomerCutTime, 0 };

            customerDictionary.Add(CustomerID, value);
            eventHeap.Insert(CustomerID);
            if (!eventDictionary.ContainsKey(CustomerID))
            {
                List<string> tmp = new List<string>();
                //build string
                string listContent = CustomerID.ToString() + " " + "n";
                tmp.Add(listContent);
                eventDictionary.Add(CustomerID, tmp);
            }
            //next input
            CustomerEntranceNumber++;
            input = Console.ReadLine().Split();
        }

        while(eventHeap.Count != 0)
        {
            //Console.WriteLine(t);
            Dictionary<string, int[]> eventStructureHelper = new Dictionary<string, int[]>();
            List<string> eventStructureList = new List<string>();
            t = eventHeap.ExtractMin();
            numbOfEvents = eventDictionary[t].Count;
            eventList = eventDictionary[t];
            string eventType;
            int customerID;
            int eventTime;
            for (int i = 0; i < numbOfEvents; i++)
            {
                evenT = eventList[i];
                string[] eventSplit = evenT.Split(' ');
                customerID = stringToInt(eventSplit[0]);
                eventType = eventSplit[1];
                eventTime = 0;
                if (eventType != "n")
                    eventTime = stringToInt(eventSplit[2]);
                int[] v = { customerID, eventTime };
                eventStructureHelper.Add(eventType, v);
                eventStructureList.Add(eventType);
            }

            eventStructureList.Sort();// Due to how the values are called their ASCII values should result in this order: n, p1, p2, p3, cd

            for (int i = 0; i < numbOfEvents; i++)
            {
                eventType = eventStructureList[i];
                int tc;
                switch (eventType)
                {
                    case "n":
                        
                        int s = getShortestQueue(printers);
                        if (s == -1)
                            Console.WriteLine("No shortest queue found");
                        printers[s].Enqueue(eventStructureHelper["n"][0]);//enqueue the customer
                        if (printers[s].Count == 1)//start printing imediatly at the found printer
                        {
                            customerID = eventStructureHelper["n"][0];

                            //Console.WriteLine("Customer " + customerID + " has entered the shop at " + t + " and started printing at printer p" + (s+1));

                            int printDoneTime = t + customerDictionary[customerID][1];
                            int printerUsed = s + 1;
                            //construct String
                            string listContent = eventStructureHelper["n"][0] + " " + "p" + printerUsed + " " + printDoneTime;
                            if (eventDictionary.ContainsKey(printDoneTime))
                            {
                                List<string> temp = eventDictionary[printDoneTime];
                                
                                temp.Add(listContent);
                                eventDictionary.Remove(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                                //The eventHeap already has a count of this index, no need to add it again.
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                temp.Add(listContent);
                                eventHeap.Insert(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                            }
                        }
                        break;
                    case "p1":
                        //Console.WriteLine("A plate came out of p1 at: " + t + " Which was created by " + eventStructureHelper["p1"][0]);
                        customerID = eventStructureHelper["p1"][0];
                        queueInQuestion = 0;
                        //Add to the customerDictionry when the customer was done with printing
                        tempValue = customerDictionary[customerID];
                        tempValue[3] = t;
                        customerDictionary.Remove(customerID);
                        customerDictionary.Add(customerID, tempValue);

                        toCutStack.Push(customerID);

                        tc = t - customerID - customerDictionary[customerID][1];
                        if (tc > longestQueuedCustomer[0] || firstQueued)
                        {
                            longestQueuedCustomer[0] = tc;
                            longestQueuedCustomer[1] = customerDictionary[customerID][0];
                            firstQueued = false;
                        }

                        printers[queueInQuestion].Dequeue();
                        if(printers[queueInQuestion].Count != 0)//Create a new printDone event with the data of the next person in the queue
                        {
                            customerID = printers[queueInQuestion].Peek();
                            int printDoneTime = t + customerDictionary[customerID][1];
                            //Console.WriteLine("p" + (queueInQuestion + 1) + " started a new plate from customer " + customerID + " which should be done at " + printDoneTime);
                            string listContent = customerID + " " + "p" + (queueInQuestion + 1) + " " + printDoneTime;
                            if (eventDictionary.ContainsKey(printDoneTime))
                            {
                                List<string> temp = eventDictionary[printDoneTime];

                                temp.Add(listContent);
                                eventDictionary.Remove(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                                //The eventHeap already has a count of this index, no need to add it again.
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                temp.Add(listContent);
                                eventHeap.Insert(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                            }

                        }

                        break;
                    case "p2":
                        //Console.WriteLine("A plate came out of p2 at: " + t + " Which was created by " + eventStructureHelper["p2"][0]);
                        customerID = eventStructureHelper["p2"][0];
                        queueInQuestion = 1;
                        //Add to the customerDictionry when the customer was done with printing
                        tempValue = customerDictionary[customerID];
                        tempValue[3] = t;
                        customerDictionary.Remove(customerID);
                        customerDictionary.Add(customerID, tempValue);

                        toCutStack.Push(customerID);

                        tc = t - customerID - customerDictionary[customerID][1];
                        if (tc > longestQueuedCustomer[0] || firstQueued)
                        {
                            longestQueuedCustomer[0] = tc;
                            longestQueuedCustomer[1] = customerDictionary[customerID][0];
                            firstQueued = false;
                        }

                        printers[queueInQuestion].Dequeue();
                        if (printers[queueInQuestion].Count != 0)//Create a new printDone event with the data of the next person in the queue
                        {
                            customerID = printers[queueInQuestion].Peek();
                            int printDoneTime = t + customerDictionary[customerID][1];
                            string listContent = customerID + " " + "p" + (queueInQuestion + 1) + " " + printDoneTime;
                            //Console.WriteLine("p" + (queueInQuestion + 1) + " started a new plate from customer " + customerID + " which should be done at " + printDoneTime);
                            if (eventDictionary.ContainsKey(printDoneTime))
                            {
                                List<string> temp = eventDictionary[printDoneTime];

                                temp.Add(listContent);
                                eventDictionary.Remove(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                                //The eventHeap already has a count of this index, no need to add it again.
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                temp.Add(listContent);
                                eventHeap.Insert(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                            }
                        }

                        break;
                    case "p3":
                        //Console.WriteLine("A plate came out of p3 at: " + t + " Which was created by " + eventStructureHelper["p3"][0]);
                        customerID = eventStructureHelper["p3"][0];
                        queueInQuestion = 2;
                        //Add to the customerDictionry when the customer was done with printing
                        tempValue = customerDictionary[customerID];
                        tempValue[3] = t;
                        customerDictionary.Remove(customerID);
                        customerDictionary.Add(customerID, tempValue);

                        toCutStack.Push(customerID);

                        tc = t - customerID - customerDictionary[customerID][1];
                        if (tc > longestQueuedCustomer[0] || firstQueued)
                        {
                            longestQueuedCustomer[0] = tc;
                            longestQueuedCustomer[1] = customerDictionary[customerID][0];
                            firstQueued = false;
                        }

                        printers[queueInQuestion].Dequeue();
                        if (printers[queueInQuestion].Count != 0)//Create a new printDone event with the data of the next person in the queue
                        {
                            customerID = printers[queueInQuestion].Peek();
                            int printDoneTime = t + customerDictionary[customerID][1];
                            string listContent = customerID + " " + "p" + (queueInQuestion + 1) + " " + printDoneTime;
                            //Console.WriteLine("p" + (queueInQuestion + 1) + " started a new plate from customer " + customerID + " which should be done at " + printDoneTime);
                            if (eventDictionary.ContainsKey(printDoneTime))
                            {
                                List<string> temp = eventDictionary[printDoneTime];

                                temp.Add(listContent);
                                eventDictionary.Remove(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                                //The eventHeap already has a count of this index, no need to add it again.
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                temp.Add(listContent);
                                eventHeap.Insert(printDoneTime);
                                eventDictionary.Add(printDoneTime, temp);
                            }
                        }
                        break;

                    case "cd":
                        //Console.WriteLine("Piet finsihed a plate at " + t + " Which was requested by " + eventStructureHelper["cd"][0]);
                        customerID = eventStructureHelper["cd"][0];
                        int timeInShop = t - customerID;
                        if (timeInShop > longestInShopCustomer[0])
                        {
                            longestInShopCustomer[0] = timeInShop;
                            longestInShopCustomer[1] = customerDictionary[customerID][0];
                            //Console.WriteLine("longestInShopCusomter was overwritten with: " + longestInShopCustomer[1] + ":" + longestInShopCustomer[0]);
                        }
                        int timeDoneWithPrint = customerDictionary[customerID][3];
                        int bt = t - timeDoneWithPrint;
                        if (bt > longestWaitedCustomer[0])
                        {
                            longestWaitedCustomer[0] = bt;
                            longestWaitedCustomer[1] = customerDictionary[customerID][0];
                        }


                        cutting = false;
                        //Console.WriteLine("Piet has started loitering again at " + t);

                        break;
                    default:
                        Console.WriteLine("Eventype that is unsuported found, The event was: " + eventType);
                        break;
                }
            }
            //check if piet is loitering while there is something in the stack
            if (!cutting && toCutStack.Count != 0)
            {
                cutting = true;
                customerID = toCutStack.Pop();
                customerCutTime = customerDictionary[customerID][2];
                int cutTimeDone = customerCutTime + t;
                string listContent = customerID + " " + "cd" + " " + cutTimeDone;
                if (eventDictionary.ContainsKey(cutTimeDone))
                {
                    List<string> temp = eventDictionary[cutTimeDone];

                    temp.Add(listContent);
                    eventDictionary.Remove(cutTimeDone);
                    eventDictionary.Add(cutTimeDone, temp);
                    //The eventHeap already has a count of this index, no need to add it again.
                }
                else
                {
                    List<string> temp = new List<string>();
                    temp.Add(listContent);
                    eventHeap.Insert(cutTimeDone);
                    eventDictionary.Add(cutTimeDone, temp);
                }
                //Console.WriteLine("Piet started plate at " + t + "Which was requested by " + customerID + " through loitering handler");
            }
        }
        Console.WriteLine(longestQueuedCustomer[1] + ": " + longestQueuedCustomer[0]);
        Console.WriteLine(longestWaitedCustomer[1] + ": " + longestWaitedCustomer[0]);
        Console.WriteLine(longestInShopCustomer[1] + ": " + longestInShopCustomer[0]);
        Console.WriteLine("sluitingstijd: " + (t + 1));
        Console.ReadLine();
    }


    public static int getShortestQueue(Queue<int>[] p)
    {
        //return 0 == a, 1 == b, 2 == c
        if(p[0].Count < p[1].Count && p[0].Count < p[2].Count)
            return 0;

        if (p[1].Count < p[0].Count && p[1].Count < p[2].Count)
            return 1;

        if (p[2].Count < p[0].Count && p[2].Count < p[1].Count)
            return 2;

        if (p[0].Count == p[1].Count || p[0].Count == p[2].Count)
            return 0;

        if (p[1].Count == p[2].Count)
            return 1;

        return -1;//Trow -1 for error handling on the other side.
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


