using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Spreadsheet";
            textBox2.Text = "version 5.0";
            textBox3.Text = "A spreadsheet application for handling simple calculations";
            textBox4.Text = ((char)0169).ToString() + " 2018";
            textBox5.Text = "This program comes with absolutely no warranty." + Environment.NewLine + "ALL RIGHTS RESERVED";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
