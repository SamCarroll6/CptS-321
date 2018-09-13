using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CptS321;
namespace CptS321
{
    public class Program
    {
        static int Main(string[] Args)
        {
            Dictionary<string,double> _dict = new Dictionary<string,double>();
            // Set start at values, basically 0+0+0 until A1, B1, and/or C1 declared
            ExpTree Exp = new ExpTree("A1+B1+C1");
            int choice = 0;
            double result;
            string choice1 = string.Empty;
            // Loops indefinitely, ends if selection 4 is chosen
            while (true)
            {
                // Menu portion
                    Console.WriteLine("1) Enter Expression");
                    Console.WriteLine("2) Set Variable Value");
                    Console.WriteLine("3) Evaluate Expression");
                    Console.WriteLine("4) Quit");
                    Console.Write("Enter Choice: ");
                    choice1 = Console.ReadLine();
                    Int32.TryParse(choice1, out choice);
                // Actions for various choices
                if (choice == 1)
                {
                    Console.Write("Enter New Expression: ");
                    Exp._Evalstring = Console.ReadLine();
                    
                }
                else if (choice == 2)
                {
                    // Request Variable name and new value from user
                    Console.Write("Enter Variable Name: ");
                    string replace = Console.ReadLine();
                    Console.Write("Enter New Value: ");
                    string with = Console.ReadLine();
                    // Add values to dictionary
                    double.TryParse(with, out result);
                    Exp.setVar(replace, result);
                    // Replace instances of variable with newly established value
                }
                else if (choice == 3)
                {
                    Console.WriteLine(Exp._Evalstring + " = " + Exp.Evaluate());
                }
                else if (choice ==4)
                {
                    return 0;
                }
                // If input isnt 1-4 tell them invalid and ask again
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}
