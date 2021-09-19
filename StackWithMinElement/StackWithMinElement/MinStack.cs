using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWithMinElement
{
    class MinStack<T> : IEnumerable<(T, bool)> where T : IComparable
    {
        private Stack<(T,bool)> Array;
        private T MinElement;
        public T Get()
        {
            return Array.Peek().Item1;
        }

        public T GetMinElement()
        {
            if (Array.Count == 0)
            {
                throw default(Exception);
            }
            return MinElement;
        }
        public MinStack()
        {

            Array = new Stack<(T, bool)>(0);
            return;

        }
        public MinStack(Stack<T> array)
        {
            if (array.Count == 0)
            {
                Array = new Stack<(T, bool)>(0);
                return;
            }
            Stack<T> Array2 = array;
            for(int i = 0; i < Array2.Count; i++)
            {
                T some = Array2.Pop();
                this.Push(some);
            }
        }
        public T Pop()
        {
            T some = Array.Pop().Item1;
            MinElement = Array.Peek().Item1;
            foreach ((T, bool) elem in Array)
            {
                if (elem.Item2 && elem.Item1.CompareTo(MinElement) < 0)
                {
                    MinElement = elem.Item1;
                }
            }
            return some;
        }

        public void Push(T element)
        {
            if (Array.Count == 0)
            {
                Array.Push((element, true));
                MinElement = element;
                return;
            }
            if (element.CompareTo(MinElement) < 0)
            {
                Array.Push((element, true));
                MinElement = element;
                return;
            }
            Array.Push((element, false));
        }

        public int size()
        {
            return Array.Count();
        }

        public IEnumerator<(T, bool)> GetEnumerator()
        {
            return Array.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Array.GetEnumerator();
        }
    }
}
