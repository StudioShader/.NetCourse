using System;
using System.Collections;
using System.Collections.Generic;

namespace StackWithMinElement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MinStack<int> stack = new MinStack<int>();
            Console.WriteLine(stack.size());
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);
            stack.Push(7);
            stack.Push(1);
            stack.Push(9);
            Console.WriteLine(stack.GetMinElement());
            Console.WriteLine(stack.size());
            stack.Pop();
            stack.Pop();
            Console.WriteLine(stack.GetMinElement());
            Console.WriteLine(stack.size());
            Console.ReadLine();
            
        }
    }
}
