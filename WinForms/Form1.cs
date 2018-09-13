using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int val = 0;
            Random rand = new Random();
            List<int> Lhold = new List<int>();
            Dictionary<int, int> Dict = new Dictionary<int, int>();
            for (int i = 0; i < 10000; i++)
            {
                val = rand.Next(0, 20000);
                Lhold.Add(val);
                if (!Dict.ContainsKey(val))
                    Dict.Add(val, val);
            }
            int count = 0, small = 0;
            for (int hold = 0; hold < 20001; hold++)
            {
                foreach (var second in Lhold)
                {
                    if (hold == second)
                    {
                        count++;
                        break;
                    }
                }
            }
            Lhold.Sort();
            bool[] Barray = new bool[20001];
            int count2 = 0;
            foreach (var value in Lhold)
            {
                if (Barray[value] == false)
                {
                    Barray[value] = true;
                    count2++;
                }
            }
                textBox1.Text = "1. Hash Set method: " +
                Dict.Count().ToString() + " unique numbers" + Environment.NewLine + Environment.NewLine
                + "\tThis Hash Set method has a time complexity "
                + "of O(n). While we use the Dictionary class (hash table) which has "
                + "O(1) for accessing indiviual elements we still have to get the size of "
                + "Entire hash table, this means counting every value in the hash table, leaving "
                + "us with a time complexity that reflects the time it takes to count every element "
                + "That is in the table, or a time complexity of O(n)."
                + Environment.NewLine + Environment.NewLine
                + "2. O(1) Storage Method: " + count.ToString() + " unique numbers"
                + Environment.NewLine
                + "3. Sorted Method: " + count2.ToString() + " unique numbers";

        }
    }
}
