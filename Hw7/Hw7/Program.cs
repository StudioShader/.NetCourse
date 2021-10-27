using System;
using System.Collections.Generic;
using System.Linq;

namespace Hw7
{
    class Program
    {
        class People
        {
            public string name { get; set; }
            public int age { get; set; }
        }
        static void Main(string[] args)
        {
            //FIRST TASK
            List<People> list = new List<People>();
            list.Add(new People { name = "some", age = 2 });
            list.Add(new People { name = "some2", age = 2 });
            list.Add(new People { name = "some3", age = 2 });
            list.Add(new People { name = "some4", age = 2 });
            list.Add(new People { name = "some5", age = 2 });
            list.Add(new People { name = "some5", age = 2 });
            list.Add(new People { name = "some6", age = 2 });
            list.Add(new People { name = "some8", age = 2 });
            string delim = " | ";
            // Для конкатенации коллекции строк с указанным разделителем лучше использовать String.Join
            String all = list
                    .Skip(3)
                    .Select((p, i) => (i == (list.Count-4)) ? p.name : (p.name + delim))
                    .Aggregate((longest, name) => longest + name);
            Console.WriteLine(all);

            // Second Task
            int number = list.Where((n, i) => n.name.Length > i).Count();
            Console.WriteLine(number);

            //Third
            string s = "Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена";
            string[] split = s.Split(':', ',', ' ', '-');
            // задание: отсортировать группы по убыванию количества элементов в каждой группе
            var another = split.Where(a => a.Length != 0).GroupBy(str => str.Length).OrderByDescending(a => a.Count()).ThenByDescending(a => a.Key);
            int i = 0; 
            foreach(var a in another)
            {
                i++;
                // используйте интерполяцию строк - работает быстрее и эффективнее по памяти!
                // спутаны местами длина и количество
                Console.WriteLine($"Группа {i} Длина: {a.Key} Количество: {a.Count()}");
                // давайте "говорящие" имена переменным.
                // код должен быть самодокументирующимся
                foreach(var b in a)
                {
                    Console.WriteLine(b);
                }
            }

            //Fourth
            string s2 = "This dog eats too much vegetables after lunch";
            string[] split2 = s2.Split(':', ',', ' ', '-');
            string Dictionary(string some)
            {
                return String.Join("", some.Reverse().ToList()).ToUpper(); 
            }
            var zip = split2.Where(a => a.Length != 0).Select(a => Dictionary(a))
                .Zip(Enumerable.Range(0, split2.Count()),
                      (s, r) => new { Group = r / 3, Item = s })
                 .GroupBy(i => i.Group, g => g.Item)
                 .ToList();
            i = 0;
            foreach (var a in zip)
            {
                i++;
                Console.WriteLine("_____________" + i + "Страница");
                foreach(var b in a)
                {
                    Console.WriteLine(b);
                }
            }
            Console.ReadLine();
        }
    }
}
