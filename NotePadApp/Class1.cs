using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace NotePadApp
{
    class FibonacciTextReader : TextReader
    {
        private int n;
        // Only constructor takes int for number of fibs. No general constructor since not necessary 
        public FibonacciTextReader(int f)
        {
            n = f;
        }
        // ReadLine override, takes int and returns the fib number at the int value as a string
        private string ReadLine(int fib)
        {
            // Base cases for first and second steps
            if (fib == 1)
            {
                return "0";
            }
            else if (fib == 2)
            {
                return "1";
            }
            else
            {
                // Calculates after first and second values
                BigInteger a = 0, b = 1;
                for(int i = 3; i <= fib; i++)
                {
                    if (i%2 == 1)
                    {
                        a = BigInteger.Add(a,b);
                    }
                    else
                    {
                        b = BigInteger.Add(a,b);
                    }
                }
                if (fib % 2 == 1)
                    return a.ToString();
                else
                    return b.ToString();
            }
        }

        // Override of ReadToEnd(), calls ReadLine and appends it to make printed string look pretty
        public override string ReadToEnd()
        {
            StringBuilder hold = new StringBuilder();
            if (n < 0)
                return "";
            for (int i = 1; i <= n; i++)
            {
                hold.Append(i.ToString() + ": " + ReadLine(i) + Environment.NewLine);
            }
            return hold.ToString();
        }
    }
}
