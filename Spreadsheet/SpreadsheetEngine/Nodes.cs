using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /* Base abstract Node, Contains left and right nodes
    ** and that's it **********************************/
    public abstract class Node
    {
        protected Node Left;
        protected Node Right;

        public Node _Right
        {
            get { return Right; }
            set { Right = value; }
        }

        public Node _Left
        {
            get { return Left; }
            set { Left = value; }
        }

        public abstract double Eval();
    }
    /* Node class for multiplication */
    public class MulNode : Node
    {
        public MulNode()
        {
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Left.Eval() * Right.Eval();
        }
    }
    /* Node class for addition */
    public class AddNode : Node
    {
        public AddNode()
        {
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Left.Eval() + Right.Eval();
        }
    }
    /* Node class for subtraction */
    public class SubNode : Node
    {
        public SubNode()
        {
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Right.Eval() - Left.Eval();
        }
    }
    /* Node class for addition */
    public class DivNode : Node
    {
        public DivNode()
        {
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Right.Eval() / Left.Eval();
        }
    }
    /* Node class for numbers only */
    public class LeafNode : Node
    {
        private double Sol;
        public LeafNode(string val)
        {
            double res = 0.0;
            double.TryParse(val, out res);
            Sol = res;
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Sol;
        }
    }
    /* Node class for exponential */
    public class ExpoNode : Node
    {
        public ExpoNode()
        {
            Left = null;
            Right = null;
        }
        public override double Eval()
        {
            return Math.Pow(Right.Eval(), Left.Eval());
        }
    }
}
