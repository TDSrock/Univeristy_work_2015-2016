using System;
using System.Collections.Generic;
using System.Linq;

class Tree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        // node internal data
        internal int level;
        internal Node left;
        internal Node right;

        // user data
        internal TKey key;
        internal TValue value;

        // constuctor for the sentinel node
        internal Node()
        {
            this.level = 0;
            this.left = this;
            this.right = this;
        }

        // constuctor for regular nodes (that all start life as leaf nodes)
        internal Node(TKey key, TValue value, Node sentinel)
        {
            this.level = 1;
            this.left = sentinel;
            this.right = sentinel;
            this.key = key;
            this.value = value;
        }
    }

    Node root;
    Node sentinel;
    Node deleted;

    public Tree()
    {
        root = sentinel = new Node();
        deleted = null;
    }

    private void Skew(ref Node node)
    {
        if (node.level == node.left.level)
        {
            // rotate right
            Node left = node.left;
            node.left = left.right;
            left.right = node;
            node = left;
        }
    }

    private void Split(ref Node node)
    {
        if (node.right.right.level == node.level)
        {
            // rotate left
            Node right = node.right;
            node.right = right.left;
            right.left = node;
            node = right;
            node.level++;
        }
    }

    private bool Insert(ref Node node, TKey key, TValue value)
    {
        if (node == sentinel)
        {
            node = new Node(key, value, sentinel);
            return true;
        }

        int compare = key.CompareTo(node.key);
        if (compare < 0)
        {
            if (!Insert(ref node.left, key, value))
            {
                return false;
            }
        }
        else if (compare > 0)
        {
            if (!Insert(ref node.right, key, value))
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        Skew(ref node);
        Split(ref node);

        return true;
    }

    private bool Delete(ref Node node, TKey key)
    {
        if (node == sentinel)
        {
            return (deleted != null);
        }

        int compare = key.CompareTo(node.key);
        if (compare < 0)
        {
            if (!Delete(ref node.left, key))
            {
                return false;
            }
        }
        else
        {
            if (compare == 0)
            {
                deleted = node;
            }
            if (!Delete(ref node.right, key))
            {
                return false;
            }
        }

        if (deleted != null)
        {
            deleted.key = node.key;
            deleted.value = node.value;
            deleted = null;
            node = node.right;
        }
        else if (node.left.level < node.level - 1
                || node.right.level < node.level - 1)
        {
            --node.level;
            if (node.right.level > node.level)
            {
                node.right.level = node.level;
            }
            Skew(ref node);
            Skew(ref node.right);
            Skew(ref node.right.right);
            Split(ref node);
            Split(ref node.right);
        }

        return true;
    }

    private Node Search(Node node, TKey key)
    {
        if (node == sentinel)
        {
            return null;
        }

        int compare = key.CompareTo(node.key);
        if (compare < 0)
        {
            return Search(node.left, key);
        }
        else if (compare > 0)
        {
            return Search(node.right, key);
        }
        else
        {
            return node;
        }
    }

    public bool Add(TKey key, TValue value)
    {
        return Insert(ref root, key, value);
    }

    public bool Remove(TKey key)
    {
        return Delete(ref root, key);
    }

    public TValue this[TKey key]
    {
        get
        {
            Node node = Search(root, key);
            return node == null ? default(TValue) : node.value;
        }
        set
        {
            Node node = Search(root, key);
            if (node == null)
            {
                Add(key, value);
            }
            else
            {
                node.value = value;
            }
        }
    }

    public bool ContainsKey(TKey key)
    {
        if (this.Search(root, key) == null)
            return false;
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Tree<int, int> ScoreTree = new Tree<int, int>();
        string input = Console.ReadLine();

        while (input != null)//null would imply we have no more lines to be read
        {
            string[] inputs = input.Split(' ');
            string disp;
            switch (inputs[0])
            {
                case "T":
                    ScoreTree.Add(stringToInt(inputs[1]), stringToInt(inputs[2]));
                    Console.WriteLine("Added");
                    break;
                case "G":

                    //Console.WriteLine(disp);
                    break;
                case "R":

                    disp = ScoreTree[stringToInt(inputs[1])].ToString();
                    Console.WriteLine(disp);
                    break;
                default:
                    Console.WriteLine("We encounterd a input with a non recognized intial char the input was: " + input);
                    break;
            }


            //prepare while loop for next input
            input = Console.ReadLine();
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
    public static int stringToInt(string i)
    {
        int r;
        r = int.Parse(i);
        return r;
    }
}


