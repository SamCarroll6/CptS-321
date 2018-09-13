using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS321
{
    public class ExpTree
    {
        // Node variable
        private Node TreeRoot;
        // Variables Dictionaries
        private Dictionary<string, string> _assoc = new Dictionary<string, string> { { "(", "disregard" }, { ")", "disregard" }, { "+" , "left" }, { "-", "left" }, { "*", "left" }, { "/", "left" }, { "^", "right" } };
        private Dictionary<string, double> _dict = new Dictionary<string, double>(); 
        private Dictionary<string, int> _weights = new Dictionary<string, int> { { "(", 11 }, { "+", 2 }, { "-", 2 }, { "*", 3 }, { "/", 3 }, { "^", 4 }, { ")", 10 } };
        // Variables strings
        private string Evalstring;
        public string _Evalstring
        {
            get { return Evalstring; }
            // For set, match all non digit and operator strings
            // Add to dictionary as 0.0
            set
            {
                _dict.Clear();
                Evalstring = value;
                string Letters = "[a-zA-Z]+[\\d]*";
                Regex Val = new Regex(Letters);
                MatchCollection Matches = Val.Matches(Evalstring);
                foreach (Match match in Matches)
                {
                    setVar(match.Value, 0.0);
                }
            }
        }

        // Constructor, set evalstring
        public ExpTree(string evaltree)
        {
            Evalstring = evaltree;
        }

        public void setVar(string Old, double New)
        {
            // If in dictionary update
            if (_dict.ContainsKey(Old))
            {
                _dict[Old] = New;
            }
            // If not in dictionary add
            else
            {
                _dict.Add(Old, New);
            }
        }

        private Stack<string> MakeStack()
        {
            TreeRoot = null;
            Stack<string> Output = new Stack<string>();
            Stack<string> Operators = new Stack<string>();
            int weightmatch = 0, weighttop = 0;
            string NumPar = "[\\d]+[(]";
            // Regex operator pattern
            string Op = "[+\\-/*()\\^]";
            // Regex pattern for >1 letters and >0 digits (matches "hello" and "A1" but not 5)
            string Letters = "[a-zA-Z]+[\\d]*";
            // Regex pattern just digits
            string Digits = "[\\d]+";
            string Pattern = NumPar + "|" + Op + "|" + Letters + "|" + Digits;
            Regex Val = new Regex(Pattern);
            MatchCollection Matches = Val.Matches(_Evalstring);
            // Traverse matches from Regex
            foreach (Match match in Matches)
            {
                // For operators handles precedence and associativity for operator stack
                if (new Regex(NumPar).IsMatch(match.Value))
                {
                    Regex Break = new Regex(Digits + "|" + Op);
                    MatchCollection newMatches = Break.Matches(match.Value);
                    foreach(Match newMatch in newMatches)
                    {
                        if (new Regex(Digits).IsMatch(newMatch.Value))
                        {
                            Output.Push(newMatch.Value);
                        }
                        else
                        {
                            Operators.Push(newMatch.Value);
                            Operators.Push("*");  
                        }
                    }
                }
                else if (new Regex(Op).IsMatch(match.Value))
                {
                    if (Operators.Count() != 0)
                    {
                        // Establish precedence values for top of stack and given operator
                        weightmatch = _weights[match.Value];
                        weighttop = _weights[Operators.Peek()];
                        // Handles leftward associativity 
                        // EX: 15-35+20 = 0 but without Assoc it equals -40
                        if(weightmatch <= weighttop && _assoc[match.Value] == "left")
                        {
                            while (weightmatch <= _weights[Operators.Peek()] && _assoc[match.Value] == "left" && _weights[Operators.Peek()] < 10)
                            {
                                Output.Push(Operators.Pop());
                                if(Operators.Count == 0)
                                {
                                    break;
                                }
                            }
                            Operators.Push(match.Value);
                        }
                        // Handles parantheses left and right
                        else if (match.Value == "(")
                        {
                            Operators.Push(match.Value);
                        }
                        else if (match.Value == ")")
                        {
                            // if Right parantheses pop op stack and push out stack until left is found 
                            while (Operators.Count() != 0 && Operators.Peek() != "(")
                            {
                                Output.Push(Operators.Peek());
                                Operators.Pop();
                            }
                            // pop left parantheses from opstack but dont push to out stack
                            if (Operators.Count() != 0)
                            {
                                Operators.Pop();
                            }
                        }
                        // When operator not parantheses, measure weights of top and value
                        // if new value has lower weight than top pop whole op stack to out stack 
                        else if (weightmatch < weighttop && Operators.Peek() != "(") 
                        {
                            while(Operators.Count() != 0)
                            {
                                Output.Push(Operators.Peek());
                                Operators.Pop();
                            }
                            // Once op stack empty 
                                Operators.Push(match.Value);
                        }
                        // If none of the above conditions met simply push new value to op stack
                        else
                        {
                            Operators.Push(match.Value);
                        }
                    }
                    // if op stack empty just push to op stack no matter what
                    else
                    {
                        Operators.Push(match.Value);
                    }
                }
                // If digit push to out stack
                else if (new Regex("^[\\d]+").IsMatch(match.Value))
                {
                    Output.Push(match.Value);
                }
                // if a string and non digit or op value
                else
                {
                    // if it's already in the dictionary of values push its set
                    // value to out stack
                    if(_dict.ContainsKey(match.Value))
                    {
                        Output.Push(_dict[match.Value].ToString());
                    }
                    // if not in dict push 0.0 
                    else
                    {
                        Output.Push("0.0");
                    }
                }
            }
            // After all matches pop all off op stack onto out stack
            while (Operators.Count() != 0)
            {
                Output.Push(Operators.Pop());
            }
            // return you postfix stack
            return Output;
        }

        private void Inserts(Node newVal)
        {
            // Bool checks if the new value has been set into tree yet
            // passed by reference in tree insert below
            bool thing = false;
            // if tree not built yet set the root to value
            if (TreeRoot == null)
            {
                TreeRoot = newVal;
            }
            // if root exists insert newval using second insert function
            else
            {
                TreeRoot = Insert(newVal, TreeRoot, ref thing);
            }
        }

        private Node Insert(Node newVal, Node root, ref bool Hold)
        {
            // Base case for recursion, sets if root null
            // Changes value of bool to true so future nodes know not to add
            if (root == null)
            {
                root = newVal;
                Hold = true;
                return root;
            }
            // Check left node until null
            if (!(root._Left is LeafNode))
            {
                root._Left = Insert(newVal, root._Left, ref Hold);
            }
            // Only checks right nodes if wasn't set at left yet (by checking the bool)
            if (!(root._Right is LeafNode) && Hold == false)
            {
                root._Right = Insert(newVal, root._Right, ref Hold);
            }
            return root; 
        }

        // Creates the stack from the string
        // Creates the tree from the stack
        // Evaluates the tree
        public double Evaluate()
        { 
            string val = Evalstring;
            Stack<string> Output = MakeStack();
            while(Output.Count() !=  0)
            {
                // insert addition node
                if (Output.Peek() == "+")
                {
                    Inserts(new AddNode());
                }
                // insert subtraction node
                else if (Output.Peek() == "-")
                {
                    Inserts(new SubNode());
                }
                // insert multiplication node
                else if (Output.Peek() == "*")
                {
                    Inserts(new MulNode());
                }
                // insert division node
                else if (Output.Peek() == "/")
                {
                    Inserts(new DivNode());
                }
                // insert exponent node
                else if (Output.Peek() == "^")
                {
                    Inserts(new ExpoNode());
                }
                // If not operator insert as leafnode
                else
                {
                    Inserts(new LeafNode(Output.Peek()));
                }
                // Pop the value added to tree
                Output.Pop();
            }
            // Make sure no changes made to Evalstring by resetting it to it's start
            // Don't want to change in case it is reevaluated. 
            Evalstring = val;
            // Evaluate the tree 
            return TreeRoot.Eval();
        }
    }
}
