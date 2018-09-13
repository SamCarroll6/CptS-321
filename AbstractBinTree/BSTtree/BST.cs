using System;
namespace BSTPA1
{
    public class BST<T> : BinTree<T> where T : IComparable
    {
        private Node<T> root;
        public int NumNodes;

        public Node<T> Retroot
        {
            get
            {
                return root;
            }
        }

        public override bool Contains(T val)
        {
            if ((object)root == null)
               return false;
            else
                return Contains(val, root);
        }

        private bool Contains(T val, Node<T> current)
        {
            if ((object)current == null)
                return false;
            else if (val.CompareTo(current.values) < 0)
                return Contains(val, current.Left);
            else if (val.CompareTo(current.values) > 0)
                return Contains(val, current.Right);
            else
                return true;
        }

        public override void Insert(T val)
        {
            NumNodes++;
            if ((object)root == null)
                root = new Node<T>(val);
            else
                root = Inserts(val, root);
        }

        private Node<T> Inserts(T newval, Node<T> current)
        {
            if ((object)current == null)
                return new Node<T>(newval);
            else if (newval.CompareTo(current.values) < 0)
                current.Left = Inserts(newval, current.Left);
            else if (newval.CompareTo(current.values) > 0)
                current.Right = Inserts(newval, current.Right);
            else
                NumNodes--;
                return current;
        }

        public override void InOrder()
        {
            PIO(root);
        }

        private void PIO(Node<T> current)
        {
            if ((object)current == null)
                return;
            PIO(current.Left);
            Console.Write(current.values);
            Console.Write(' ');
            PIO(current.Right);
        }

        public override void PostOrder()
        {
            PO(root);
        }

        private void PO(Node<T> current)
        {
            if ((object)current == null)
                return;
            PO(current.Left);
            PO(current.Right);
            Console.Write(current.values);
            Console.Write(' ');

        }

        public int Pheight()
        {
            if ((object)root == null)
                return 0;
            return Height(root, 1);
        }

        private int Height(Node<T> current, int height)
        {
            if ((object)current == null)
                return 0;
            return Math.Max(Height(current.Left, height++) + 1, Height(current.Right, height++) + 1);

        }


    }
}
