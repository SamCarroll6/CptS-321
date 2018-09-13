using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using CptS321;
namespace Spreadsheet
{
    public partial class Form1 : Form
    {
        spreadsheet CxR;
        public Form1()
        {
            InitializeComponent();
        }

        private void Sheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell thing = sender as Cell;
            dataGridView1.Rows[thing._Rowindex].Cells[thing._Columnindex].Value = thing._Value;
        }

        // Load Form
        private void Form1_Load(object sender, EventArgs e)
        {
            // Clear any existing Rows and Columns
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            int Col = 0, Row = 0;
            // Initialize Columns and count number
            for (char i = 'A'; i <= 'Z'; i++)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = i.ToString(), Name = i.ToString() });
                Col++;
            }
            // Initialize Rows and count number
            for (int a = 0; a < 50; a++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[a].HeaderCell.Value = (a + 1).ToString();
                Row++;
            }
            // Use row and column counts to initialize spreadsheet 
            CxR = new spreadsheet(Col, Row);
            CxR.CellPropertyChanged += Sheet_PropertyChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Demo1();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (CxR.getCell(e.ColumnIndex, e.RowIndex)._Text == dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = CxR.getCell(e.ColumnIndex, e.RowIndex)._Value;
                }
                else
                {
                    CxR.getCell(e.ColumnIndex, e.RowIndex)._Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            else
            {
                CxR.getCell(e.ColumnIndex, e.RowIndex)._Text = "";
            }
        }

        private void Demo1()
        {
            Random R = new Random();
            for (int i = 0; i < 50; i++)
            {
                int Col = R.Next(26);
                int Row = R.Next(50);
                CxR.getCell(Col, Row)._Text = "Hello World";
                //dataGridView1.Rows[Row].Cells[Col].Value = "Hello World";
            }
            for (int b = 0; b < 50; b++)
            {
                CxR.getCell(1, b)._Text = "This is Cell B" + (b + 1).ToString();
                //dataGridView1.Rows[b].Cells[1].Value = "This is Cell B" + (b+1).ToString();
            }
            for (int a = 0; a < 50; a++)
            {
                CxR.getCell(0, a)._Text = "=B" + (a + 1).ToString();
                //dataGridView1.Rows[a].Cells[0].Value = CxR.getCell(0, a)._Value;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If you press a cell it sets text box 2 to appropriate name of cell
            if (e.RowIndex != -1)
            {
                int row = e.RowIndex + 1;
                int colnum = e.ColumnIndex / 26;
                int Colhold = e.ColumnIndex;
                string store = "";
                for (int i = 0; i < colnum; i++)
                {
                    Colhold = Colhold - 26;
                }
                Colhold = e.ColumnIndex + 65;
                for (int i = 0; i <= colnum; i++)
                {
                    store = store + ((char)Colhold).ToString();
                }
                store = store + row.ToString();
                textBox2.Text = store;
                textBox1.Text = CxR.getCell(e.ColumnIndex, e.RowIndex)._Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // If no cell has been declared textbox 1 can't be edited
            if (textBox2.Text == "")
            {
                textBox1.Text = "No cell selected";
            }
        }

        // I deleted this and it wouldn't build so it's gonna stay and be empty
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Triggered when textbox1 has enter key hit
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Updates text value at specified cell 
                CxR.getCell(textBox2.Text)._Text = textBox1.Text;
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (CxR != null && dataGridView1.CurrentCell != null)
            {
                int row = dataGridView1.CurrentCell.RowIndex + 1;
                int colnum = dataGridView1.CurrentCell.ColumnIndex / 26;
                int Colhold = dataGridView1.CurrentCell.ColumnIndex;
                string store = "";
                for (int i = 0; i < colnum; i++)
                {
                    Colhold = Colhold - 26;
                }
                Colhold = dataGridView1.CurrentCell.ColumnIndex + 65;
                for (int i = 0; i <= colnum; i++)
                {
                    store = store + ((char)Colhold).ToString();
                }
                store = store + row.ToString();
                textBox2.Text = store;
                textBox1.Text = CxR.getCell(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex)._Text;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                dataGridView1.CurrentCell.Value = textBox1.Text;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;        
            try
            {
                if (path != string.Empty)
                {
                    CxR = new spreadsheet(26,50);
                    dataGridView1.SelectAll();
                    foreach(DataGridViewCell datacell in dataGridView1.SelectedCells)
                    {
                        datacell.Value = "";
                    }
                    dataGridView1.ClearSelection();
                    CxR.CellPropertyChanged += Sheet_PropertyChanged;
                    CxR.loadnewSS(path);
                }
                else
                {
                    Console.WriteLine("Empty file");
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File not found error");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show save dialog
            saveFileDialog1.ShowDialog();
            // Get path
            string path = saveFileDialog1.FileName;
            // Make sure string name has content
            if (path != string.Empty)
            {
                // If it does write to file name path the xml string
                File.WriteAllText(path, CxR.saveSpreadsheet().ToString());
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 LF2 = new Form2();
            LF2.Show();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}