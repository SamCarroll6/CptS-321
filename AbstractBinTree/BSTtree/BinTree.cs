using System;
namespace BSTPA1
{
    public abstract class BinTree<T> where T : IComparable
    {
        protected BinTree()
        {
        }
        public abstract void Insert(T val);
        public abstract bool Contains(T val);
        public abstract void InOrder();
        public abstract void PostOrder();
    }
}
