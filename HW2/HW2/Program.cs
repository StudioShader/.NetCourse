using System;

namespace HW2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyHashTable<int> Mass = new MyHashTable<int>(10);
            Mass.Add(3);
            Mass.Add(5);
            Mass.Add(6);
            Console.WriteLine(Mass.Contains(3));
            foreach(var a in Mass)
            {
                Console.WriteLine(a);
            }
            Mass.Remove(6);
            Console.WriteLine(Mass.Contains(6));
            Console.ReadLine();
        }
    }
}
