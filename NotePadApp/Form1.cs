/*

 Name: Sam Carroll
 ID: 11477450
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Numerics;

namespace NotePadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadText(TextReader read)
        {
            textBox1.Text = read.ReadToEnd();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open file dialog
            openFileDialog1.ShowDialog();
            // Set path to chosen file name
            string path = openFileDialog1.FileName;
            // Load file based on path
                using (StreamReader fLoad = new StreamReader(path))
                {
                    LoadText(fLoad);
                }
        }
        // Loads the Fibonacci sequence for first 50 when option on drop down menu selected 
        private void loadFibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib50 = new FibonacciTextReader(50);
            LoadText(fib50);
        }
        // Loads the Fibonacci sequence for first 100 when option on drop down menu selected
        private void loadFibonacciNumbersFirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fib100 = new FibonacciTextReader(100);
            LoadText(fib100);
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            if (path != "")
            File.WriteAllText(path, textBox1.Text);
        }
    }
}
