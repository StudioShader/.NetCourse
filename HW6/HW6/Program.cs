using System;
using System.Collections;
using System.Collections.Generic;

namespace HW6
{
    public class Lake : IEnumerable<int>
    {
        int[] stones;

        public Lake(int[] _stones)
        {
            stones = _stones;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new LakeEnum(stones);
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public class LakeEnum : IEnumerator<int>
        {
            public int[] _stones;   

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public LakeEnum(int[] list)
            {
                _stones = list;
            }

            public bool MoveNext()
            {
                if (position == -1)
                {
                    position = 0;
                    return (position < _stones.Length);
                }
                if (position % 2 == 0 && ((_stones.Length-1) - position) >= 2)
                {
                    position += 2;
                    return (position < _stones.Length) && position > 0;
                }
                if (position % 2 == 1)
                {
                    position -= 2;
                    return (position < _stones.Length) && position > 0;
                }
                if(position % 2 == 0 && (_stones.Length - 1) - position == 1)
                {
                    position += 1;
                    return true;
                }
                if (position % 2 == 0 && (_stones.Length - 1) - position == 0)
                {
                    position -= 1;
                    return (position < _stones.Length) && position > 0;
                }
                position++;
                return (position < _stones.Length);
            }
            public void Dispose()
            { 

            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public int Current
            {
                get
                {
                    try
                    {
                        return _stones[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }

    public class Person
    {
        public int age { get; set; }
        public string Name { get; set; }
    }

    class NamePeopleComparer : IComparer<Person>
    {
        public int Compare(Person p1, Person p2)
        {
            if (p1.Name.Length > p2.Name.Length)
                return 1;
            else if (p1.Name.Length < p2.Name.Length)
                return -1;
            else
                return 0;
        }
    }

    class AgePeopleComparer : IComparer<Person>
    {
        public int Compare(Person p1, Person p2)
        {
            if (p1.age > p2.age)
                return 1;
            else if (p1.age < p2.age)
                return -1;
            else
                return 0;
        }
    }

    internal class Node<T> : IEnumerable<T> where T : IComparable<T>
    {
        internal T data;
        internal Node<T> next;
        public Node(T d)
        {
            data = d;
            next = null;
        }
        internal void InsertFront(SingleLinkedList<T> singlyList, T new_data)
        {
            Node<T> new_node = new Node<T>(new_data);
            new_node.next = singlyList.head;
            singlyList.head = new_node;
        }
        internal void InsertLast(SingleLinkedList<T> singlyList, T new_data)
        {
            Node<T> new_node = new Node<T>(new_data);
            if (singlyList.head == null)
            {
                singlyList.head = new_node;
                return;
            }
            Node<T> lastNode = GetLastNode(singlyList);
            lastNode.next = new_node;
        }
        internal Node<T> GetLastNode(SingleLinkedList<T> singlyList)
        {
            Node<T> temp = singlyList.head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }

        internal int Count(SingleLinkedList<T> singlyList)
        {
            int count = 1;
            Node<T> temp = singlyList.head;
            if (temp == null)
            {
                return 0;
            }
            while (temp.next != null)
            {
                count++;
                temp = temp.next;
            }
            return count;
        }
        internal bool DeleteNodebyKey(SingleLinkedList<T> singlyList, T key)
        {
            Node<T> temp = singlyList.head;
            Node<T> prev = null;
            if (temp != null && temp.data.CompareTo(key)==0)
            {
                singlyList.head = temp.next;
                return true;
            }
            while (temp != null && temp.data.CompareTo(key) != 0)
            {
                prev = temp;
                temp = temp.next;
            }
            if (temp == null)
            {
                return false;
            }
            prev.next = temp.next;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Nodes(this);
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public class Nodes : IEnumerator<T>
        {
            public Node<T> head { get; set; }

            public T Current => current.data;

            object IEnumerator.Current => Current;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;
            private Node<T> current;
            public Nodes(Node<T> _head)
            {
                head = _head;
            }
            public bool MoveNext()
            {
                if(head != null && position == -1)
                {
                    current = head;
                    position = 0;
                    return true;
                }
                if(current!=null && current.next != null)
                {
                    current = current.next;
                    position += 1;
                    return true;
                }
                return false;
            }
            public void Dispose()
            {

            }

            public void Reset()
            {
                position = -1;
                current = null;
            }

        }
    }
    internal class SingleLinkedList<T> where T : IComparable<T>
    {
        public Node<T> head { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] some = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Lake lake = new Lake(some);
            foreach (var a in lake)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("Hello World!");
            int[] some1 = { 13, 23, 1, -8, 4, 9 };
            Lake lake1 = new Lake(some1);
            foreach (var a in lake1)
            {
                Console.WriteLine(a);
            }
            Console.ReadLine();
            Console.WriteLine("SECOND TASK ----------------------");
            Person p1 = new Person { Name = "Bill", age = 34 };
            Person p2 = new Person { Name = "Tom", age = 23 };
            Person p3 = new Person { Name = "Alice", age = 21 };
            Person[] people = new Person[] { p1, p2, p3 };
            Array.Sort(people, new AgePeopleComparer());

            foreach (Person p in people)
            {
                Console.WriteLine($"{p.Name} - {p.age}");
            }
            Console.WriteLine("and now by name:");
            Array.Sort(people, new NamePeopleComparer());

            foreach (Person p in people)
            {
                Console.WriteLine($"{p.Name} - {p.age}");
            }
            Console.ReadLine();
            Console.WriteLine("THIRD TASK ----------------------");
            Node<int> head1 = new Node<int>(1);
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            list.head = head1;
            head1.InsertLast(list, 3);
            head1.InsertLast(list, 2);
            foreach(var a in head1)
            {
                Console.WriteLine(a);
            }
            Console.ReadLine();
            // извните что опять всё в одном файле я знаю что так неправильно но кажется так всё равно удобно.
            // Я знаю что методы в Node должны лежать в SingleLinkedList. Просто так получилось. 
        }
    }
}
