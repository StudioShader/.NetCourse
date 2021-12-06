using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace Hw10
{
    public class BlackBox
    {
        public int some = 0;
        private int innerValue;
        private BlackBox(int innerValue)
        {
            this.innerValue = innerValue;
        }
        private BlackBox()
        {
            this.innerValue = 5;
        }
        private void Add(int addend)
        {
            this.innerValue += addend;
        }
        private void Subtract(int subtrahend)
        {
            this.innerValue -= subtrahend;
        }
        private void Multiply(int multiplier)
        {
            this.innerValue *= multiplier;
        }
        private void Divide(int divider)
        {
            this.innerValue /= divider;
        }
    }

    // свои атрибуты лучше всегда называть с суффиксом *Attribute
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Method)]
    public class Custom : System.Attribute
    {
        public string author;
        public int revision;
        public string description;
        public string[] reviewers;

        public Custom(string _author, int _revision, string _description, params string[] _reviewers)
        {
            author = _author;
            revision = _revision;
            description = _description;
            reviewers = _reviewers;
        }
    }

    [Custom("Joe", 2, "Class to work with health data.", "Arnold", "Bernard")]
    public class HealthScore
    {
        [Custom("Andrew", 3, "Method to collect health data.", "Sam", "Alex")]
        public static long CalcScoreData()
        {
            return 0;
        }

        [Custom("Andrew2", 5, "Method to determine something", "Jam", "Alexine")]
        public static long CalcAnotherData()
        {
            return 0;
        }

        public static long LiterallyNothing()
        {
            return 0;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // FIRST TASK
            Assembly currentAssem = Assembly.GetExecutingAssembly();
            Type bbType = currentAssem.GetType("Hw10.BlackBox");
            Console.WriteLine(bbType);
            BlackBox bb = (BlackBox)Activator.CreateInstance(bbType, true);
            bbType.GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(bb, new object[] { 5 });
            Console.WriteLine(bbType.GetField("innerValue", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(bb));
            string curent = "";
            while (curent != "exit")
            {
                curent = Console.ReadLine();
                if (curent == "exit")
                {
                    break;
                }
                Console.WriteLine(curent.IndexOf('('));
                Console.WriteLine(curent.IndexOf(')'));
		// обратите внимание на множество лишних повторяющихся операций curent.IndexOf('(')
                string first = curent.Substring(0, curent.IndexOf('('));
                // ? именование внутренней переменной
                int INT = Int32.Parse(curent.Substring(curent.IndexOf('(') + 1, curent.Length - curent.IndexOf('(') - 2));
                Console.WriteLine(first + "      _____________");
                Console.WriteLine(INT + "      _____________");
                bbType.GetMethod(first, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(bb, new object[] { INT });
                Console.WriteLine("=> " + bbType.GetField("innerValue", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(bb));
            }

            // SECOND TASK
            Console.WriteLine("__________XXX" + typeof(HealthScore) + "XXX_____________");
            Custom attr = (Custom)System.Attribute.GetCustomAttribute(typeof(HealthScore), typeof(Custom));
            Console.WriteLine(attr.author + " " + attr.description + " " + attr.revision);
            foreach (var reviewer in attr.reviewers)
            {
                Console.WriteLine("Reviwer: " + reviewer);
            }
            Console.WriteLine(attr.author);

            foreach (var method in typeof(HealthScore).GetMethods())
            {
                Console.WriteLine(method.Name + " _______ ");
                Custom method_attr = (Custom)System.Attribute.GetCustomAttribute(method, typeof(Custom));
                if (method_attr != null)
                {
                    Console.WriteLine(method_attr.author + " " + method_attr.description + " " + method_attr.revision);
                    foreach(var reviewer in method_attr.reviewers)
                    {
                        Console.WriteLine("Reviwer: " + reviewer);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
