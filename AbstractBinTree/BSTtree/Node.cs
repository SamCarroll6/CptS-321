using System;
namespace BSTPA1
{
    public class Node<T> where T : IComparable
    {
        private T val;
        private Node<T> pLeft;
        private Node<T> pRight;

        public Node()
        {
        }

        public Node(T newval)
        {
            val = newval;
        }

        public T values
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }

        public Node<T> Left
        {
            get
            {
                return pLeft;
            }
            set
            {
                pLeft = value;
            }
        }

        public Node<T> Right
        {
            get
            {
                return pRight;
            }
            set
            {
                pRight = value;
            }
        }

        public static bool operator ==(Node<T> a, Node<T> b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            else if ((Object)a == null || (Object)b == null)
                return false;
            return a.val.CompareTo(b.val) == 0;
        }

        public static bool operator !=(Node<T> a, Node<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(Object obj)
        {
            Node<T> hold = obj as Node<T>;
            if (hold == null)
                return false;
            else
                return val.Equals(hold.val);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator <(Node<T> a, Node<T> b) 
        {
            return a.val.CompareTo(b.val) < 0;
        }

        public static bool operator >(Node<T> a, Node<T> b)
        {
            return a.val.CompareTo(b.val) > 0;
        }
    }
}