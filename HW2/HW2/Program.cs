﻿using System;
using System.Collections.Generic;

namespace HW2
{
    class Program
    {
        struct SomeBase
        {
            public int index { get; set; }
            public SomeBase(int somInt)
            {
                index = somInt;
            }
            public int GetIndex()
            {
                return index;
            }
        }
        
        // получилось инкапсулировать SomeBase внутри Derived. Однако нет связи между жэтими классами - никак не понять, что один унаследован от другого.
        // Можно унаследовать обе структуры от общего интерфейса. Реализовать методы интерфейса в первой структуре, а реализацию методов интерфейса во второй структуре реализовать через обращение к соответствующему методу экземпляра "базовой" сущности baseClass (проксирование вызовов)
        struct Derived
        {
            public SomeBase baseClass;

            public string Name { get; set; }

            public Derived(SomeBase based){
                baseClass = based;
                Name = "Knot";
            }
            public void ChangeName()
            {
                Name += baseClass.index;
            }

        }
        static void Main(string[] args)
        {

            // inheritage woth structs
            SomeBase A = new SomeBase(4);
            Derived B = new Derived(A);
            B.baseClass.index = 4;
            B.ChangeName();
            Console.WriteLine(B.Name);

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

            ImmutableType my = ImmutableType.ConstructArticle("some name", "article");
            ImmutableType another = my.ChangeArticleTo("some other article");
            Console.WriteLine("my: " + my.Name + " article: " + my.Article);
            Console.WriteLine("another: " + another.Name + " article: " + another.Article);
            var x = new
            {
                Items = B
            };
            Console.WriteLine(x.Items.Name);
            x.Items.ChangeName();
            Console.WriteLine(x.Items.Name);
            /*var some = x.Items;
            Console.WriteLine(some.Current);
            Console.WriteLine(x);
            unsafe
            {
                int* ptr = &x;
            }
            Console.WriteLine(x.Items);
            Console.WriteLine(x.Items.MoveNext());
            Console.WriteLine(x.Items.Current);
            while (x.Items.MoveNext())
                Console.WriteLine(x.Items.Current);
            Console.ReadLine();*/
        }
    }
}
