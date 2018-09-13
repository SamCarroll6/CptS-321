using System;
namespace BSTPA1
{
    public class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 &&string.Compare(args[0], "--strtest") == 0)
            {
                // Run test with string
                Runtest();
            }
            else if (args.Length > 0 && string.Compare(args[0], "--chartest") == 0)
            {
                // Run test with chars
                Runbigtest();
            }
            else
            {
                // Standard go through 
                bool[] reps = new bool[101];
                string vals = "";
                string[] breaks;
                int hold = 0;
                BST<int> tree = new BST<int>();
                for (int i = 0; i < 101; i++)
                {
                    reps[i] = false;
                }
                Console.WriteLine("Enter a collection of numbers in the range [0, 100], separated by spaces:");
                // Read in provided values
                vals = Console.ReadLine();
                // Print to screen
                Console.WriteLine(vals);
                // Break up string and convert each value to int
                breaks = vals.Split(' ');
                for (int i = 0; i < breaks.Length; i++)
                {
                    if (Int32.TryParse(breaks[i], out hold))
                        if (hold <= 100 && hold >= 0)
                        {
                            if (reps[hold] == false)
                            {
                                reps[hold] = true;
                                tree.Insert(hold);
                            }
                        }
                }
                // Display tree in order and post order, display stats.
                tree.InOrder();
                Console.WriteLine();
                Console.Write("Post Order: ");
                tree.PostOrder();
                Console.WriteLine();
                Console.WriteLine("Tree Statistics: ");
                Console.WriteLine("\tNumber of Nodes: " + tree.NumNodes);
                Console.WriteLine("\tNumber of Levels: {0}", tree.Pheight());
                if (tree.NumNodes != 0)
                    Console.WriteLine("\tMinimum number of levels a tree of size {0} could have: {1}",
                                      tree.NumNodes, (int)Math.Log(tree.NumNodes, 2.0) + 1);
                else
                    Console.WriteLine("\tMinimum number of levels a tree of size {0} coult have: 0", tree.NumNodes);
            }
        }

        static void Runtest()
        {
            // Run test with strings
            BST<string> testtree = new BST<string>();
            Random rand = new Random();
            int val = 0;
            string ins = "";
            // Because it takes strings 100 < 12 because first 1's compared then 0 and 2.
            for (int i = 0; i < 150; i++)
            {
               val = rand.Next(0, 101);
               ins = val.ToString();
               testtree.Insert(ins);
            }
            testtree.Insert("Zebra");
            testtree.Insert("Apple");
            testtree.Insert("zebra");
            testtree.Insert("apple");
            testtree.Insert("Cat");
            Console.WriteLine("Word");
            testtree.InOrder();
            Console.WriteLine();
            Console.WriteLine("Tree Statistics: ");
            Console.WriteLine("\tNumber of Nodes: " + testtree.NumNodes);
            Console.WriteLine("\tNumber of Levels: {0}", testtree.Pheight());
            if (testtree.NumNodes != 0)
                Console.WriteLine("\tMinimum number of levels a tree of size {0} could have: {1}",
                                  testtree.NumNodes, (int)Math.Log(testtree.NumNodes, 2.0) + 1);
            else
                Console.WriteLine("\tMinimum number of levels a tree of size {0} coult have: 0", testtree.NumNodes);
        }

        static void Runbigtest()
        {
            // Run test with chars
            BST<char> testtree = new BST<char>();
            Random rand = new Random();
            int val = 0;
            char ins;
            for (int i = 0; i < 150; i++)
            {
                val = rand.Next(33, 126);
                ins = (char)val;
                testtree.Insert(ins);
            }
            testtree.InOrder();
            Console.WriteLine();
            Console.WriteLine("Tree Statistics: ");
            Console.WriteLine("\tNumber of Nodes: " + testtree.NumNodes);
            Console.WriteLine("\tNumber of Levels: {0}", testtree.Pheight());
            if (testtree.NumNodes != 0)
                Console.WriteLine("\tMinimum number of levels a tree of size {0} could have: {1}",
                                  testtree.NumNodes, (int)Math.Log(testtree.NumNodes, 2.0) + 1);
            else
                Console.WriteLine("\tMinimum number of levels a tree of size {0} coult have: 0", testtree.NumNodes);  
        }
    }
}
