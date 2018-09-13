using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;

namespace CptS321
{
    public abstract class Cell : INotifyPropertyChanged
    {
        // Set values for row, column and internal text
        readonly int RowIndex;
        readonly int ColumnIndex;
        public Dictionary<string, Cell> sharevals;
        // Property for Text, includes property change event
        protected string Text;

        public string _Text
        {
            get
            {
                return this.Text;
            }
            set
            {
                if (this.Text != value)
                {
                    this.Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        // Only getter for Value, setter declared later per instructions
        protected string Value;

        public string _Value
        {
            get
            {
                return Value;
            }
        }
        // Property changed event variable declaration
        public event PropertyChangedEventHandler ValPropertyChanged;
        // Property changed event function
        public void Val_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Value");
            if (ValPropertyChanged != null)
            {
                ValPropertyChanged(this, e);
            }
        }
        // Declaring property changed handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        // Constructor for Cell class, initializes the Row and Column index for that cell
        public Cell(int columnindex, int rowindex)
        {
            RowIndex = rowindex;
            ColumnIndex = columnindex;
            Text = "";
            Value = "";
            sharevals = new Dictionary<string, Cell>();
        }

        // Property declarations for Row index and Col index (Both only getters since readonly)
        public int _Rowindex
        {
            get
               {
                return this.RowIndex;
               }
        }

        public int _Columnindex
        {
            get
               {
                return this.ColumnIndex;
               }
        }
    }

/***********************************Spreadsheet Class Declaration*********************************************/

    public class spreadsheet
    {
/***********************************Cell2 Class Declaration*********************************************/
        // Cell2 helper class, only accessible to spreadsheet and creates instances of cell for spreadsheet
        private class Cell2 : Cell
        {
            // Constructor just uses the Cell constructor 
            public Cell2(int Col, int Row):base(Col,Row)
            {
            }

            // Factory function, returns new cell instances
            public static Cell2 Factory(int Col, int Row)
            {
                return new Cell2(Col, Row);
            }

            public void SetValue(string newval)
            {
                this.Value = newval;
            }
        }
/******************************************End of Cell2 Declaration***************************************************/ 
        // Grid to represent our cell collection
        private Cell2[,] Collection;

        // integer representations for total Row and Column count of our collection
        private int ColumnCount;

        public int _ColumnCount
        {
            get { return ColumnCount; }
        }

        private int RowCount;

        public int _RowCount
        {
            get { return RowCount; }
        }

        // Constructor for spreadsheet
        public spreadsheet(int Col, int Row)
        {
            // Set size for Collection
            Collection = new Cell2[Col, Row];
            // Set each cell in Collection to specific Cell2 and tie to Property changed event
            for(int i = 0; i < Col; i++)
            {
                for (int a = 0; a < Row; a++)
                {
                    Collection[i, a] = new Cell2(i, a);
                    Collection[i, a].PropertyChanged += Cell_PropertyChanged;
                    Collection[i,a].ValPropertyChanged += Cell_PropertyChanged;
                }
            }
            // Set ColumnCount and RowCount values
            ColumnCount = Col;
            RowCount = Row;
        }

        // Returns cell text for given cell in newval
        // Necessary for converting cell name to cell in form1 
        public Cell getCell(string newval)
        {
            int R = 0;
            int ret1 = -26;
            string Letters = "[a-zA-Z]+";
            string Row = "";
            Regex Val = new Regex(Letters);
            MatchCollection Matches = Val.Matches(newval);
            //char Col = newval[0];
            foreach (Match matches in Matches)
            {
                if (new Regex(Letters).IsMatch(matches.Value))
                {
                    foreach (char Col in matches.Value)
                    {
                        char hold = Col;
                        if (Col <= 'z' && Col >= 'a')
                        {
                            hold = char.ToUpper(Col);
                        }
                        Row = newval.Substring(1, newval.Length - 1);
                        ret1 += (hold - 65) + 26;
                    }
                }
            }
            Int32.TryParse(Row, out R);
            return getCell((ret1), (R - 1));
        }

        // getcell for passing when events are being changed and values being set
        public string getCell(string newval, Cell pass)
        {
            int R = 0;
            int ret1 = -26;
            string Letters = "[a-zA-Z]+";
            string Row = "";
            Regex Val = new Regex(Letters);
            MatchCollection Matches = Val.Matches(newval);
            //char Col = newval[0];
            foreach (Match matches in Matches)
            {
                if (new Regex(Letters).IsMatch(matches.Value))
                {
                    foreach (char Col in matches.Value)
                    {
                        char hold = Col;
                        if (Col <= 'z' && Col >= 'a')
                        {
                            hold = char.ToUpper(Col);
                        }
                        Row = newval.Substring(1, newval.Length - 1);
                        ret1 += (hold - 65) + 26;
                    }
                }
            }
            Int32.TryParse(Row, out R);
            return getCell((ret1), (R - 1), pass)._Value;
        }
   
        public string Calculator(string newval, Cell hold)
        {
            if (newval == string.Empty)
            {
                // Clear values and unassign cells if current cell reset
                foreach(string key in hold.sharevals.Keys)
                {
                    hold.sharevals[key].PropertyChanged -= hold.Val_PropertyChanged;
                }
                hold.sharevals.Clear();
                return "";
            }
            else if (newval[0] != '=')
            {
                // Clear values and unassign cells if current cell reset
                foreach (string key in hold.sharevals.Keys)
                {
                    hold.sharevals[key].PropertyChanged -= hold.Val_PropertyChanged;
                }
                hold.sharevals.Clear();
                return newval;
            }
            // use getcell to find cell value if set equal to one
            else
            {
                // Added expression tree implementation for '=' condition
                ExpTree ret = new ExpTree(newval);
                string grab = "";
                string Letters = "[a-zA-Z]+[\\d]*";
                string Ops = "[+\\-/*()\\^]";
                Regex Val = new Regex(Letters);
                MatchCollection Matches = Val.Matches(newval);
                foreach (Match match in Matches)
                {
                    grab = getCell(match.Value, hold);
                    double set = 0.0;
                    if (double.TryParse(grab, out set))
                    {
                        ret.setVar(match.Value, set);
                    }
                    else
                    {
                        if (new Regex(Ops).IsMatch(newval) && grab != string.Empty)
                        {
                            return "#REF";
                        }
                        else
                        {
                            return grab;
                        }
                    }
                }
                return ret.Evaluate().ToString();
            }
        }

        public Cell getCell(int Col, int Row, Cell pass)
        {
            int row = Row + 1;
            int colnum = Col / 26;
            int Colhold = Col;
            string store = "";
            for (int i = 0; i < colnum; i++)
            {
                Colhold = Colhold - 26;
            }
            Colhold = Col + 65;
            for (int i = 0; i <= colnum; i++)
            {
                store = store + ((char)Colhold).ToString();
            }
            store = store + row.ToString();
            try
            {
                if (!pass.sharevals.ContainsKey(store))
                {
                    Collection[Col, Row].PropertyChanged += pass.Val_PropertyChanged;            
                    pass.sharevals.Add(store, Collection[Col,Row]);
                }
                return Collection[Col, Row];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Out of range exception caught");
                return new Cell2(-1, -1);
            }
        }

        // Return given Cell from Collection
        public Cell getCell(int Col, int Row)
        {
            try
            {
                return Collection[Col, Row];
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Out of range exception caught");
                return new Cell2(-1, -1);
            }
        }
        // Property changed event variable declaration
        public event PropertyChangedEventHandler CellPropertyChanged;
        // Property changed event function
        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var thing = sender as Cell2;
            // Sets value 
            thing.SetValue(Calculator(thing._Text, thing));
            // Sends property changed 
            if (CellPropertyChanged != null)
            {
                CellPropertyChanged(sender, e);
            }   
        }

        public XDocument saveSpreadsheet()
        {
            List<Cell> holder = new List<Cell>();

            for (int i = 0; i < ColumnCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    if (Collection[i,j]._Text != string.Empty)
                    {
                        holder.Add(Collection[i, j]);
                    }
                }
            }
            XDocument xmlret = new XDocument(new XDeclaration("5.0", "utf-8", "yes"), new XComment("Creating XML Doc"),
            new XElement("content",
            new XElement("cells",
            from hold in holder
            select new XElement("cell", 
                   new XElement("placement", ((char)(hold._Columnindex + 65)).ToString() + (hold._Rowindex + 1).ToString()),
                   new XElement("text", hold._Text),
                   new XElement("value", hold._Value))),
                   new XElement("spreadsheet", 
                   new XElement("columns", ColumnCount), 
                   new XElement("rows", RowCount))));
            return xmlret;
        }

        public void loadnewSS(string filepath)
        {
            int Cols = 0;
            int Rows = 0;
            IEnumerable<string> SScols = from dimensions in XDocument.Load(filepath).Descendants("spreadsheet")
                                         select dimensions.Element("columns").Value;
            IEnumerable<string> SSrows = from dimensions in XDocument.Load(filepath).Descendants("spreadsheet")
                                         select dimensions.Element("rows").Value;
            if (Int32.TryParse(SScols.First(), out Cols) && Int32.TryParse(SSrows.First(), out Rows))
            {
                if (Cols != ColumnCount || Rows != RowCount)
                {
                    // Set size for Collection
                    Collection = new Cell2[Cols, Rows];
                    // Set each cell in Collection to specific Cell2 and tie to Property changed event
                    for (int i = 0; i < Cols; i++)
                    {
                        for (int a = 0; a < Rows; a++)
                        {
                            Collection[i, a] = new Cell2(i, a);
                            Collection[i, a].PropertyChanged += Cell_PropertyChanged;
                            Collection[i, a].ValPropertyChanged += Cell_PropertyChanged;
                        }
                    }
                    // Set ColumnCount and RowCount values
                    ColumnCount = Cols;
                    RowCount = Rows;
                }
            }
            var Cellvals = from dimensions in XDocument.Load(filepath).Descendants("cells").Descendants("cell")
                           where new Regex("[a-zA-Z]+[\\d]*").IsMatch(dimensions.Element("placement").Value)
                           select new
                           {
                               TextVal = dimensions.Element("text").Value,
                               CellVal = dimensions.Element("placement").Value,
                               ValVal = dimensions.Element("value").Value
                           };
            foreach(var cellval in Cellvals)
            {
                getCell(cellval.CellVal)._Text = cellval.TextVal;
                //((Cell2)hold).SetValue(cellval.ValVal);
            }
        }
    }
}
