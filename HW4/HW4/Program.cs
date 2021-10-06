using System;
using System.Collections.Generic;

namespace HW4
{
    interface firstSome
    {
        public void ToSome();
    }
    interface secondSome
    {
        public void ToSome();
    }
    public abstract class someClass
    {
        public abstract void ToSome();
    }

    public sealed class Final : someClass, firstSome, secondSome
    {
        void secondSome.ToSome()
        {
            Console.WriteLine("form second interface");
        }
        void firstSome.ToSome()
        {
            Console.WriteLine("form first interface");
        }
        public override void ToSome()
        {
            Console.WriteLine("form base abstract");
        }
    }
    class horse : IEquatable<horse>, IComparable<horse>
    {
        public bool horseshoe = false;
        public int horsepower = 1;
        public HorseMark horsemark;
        public enum HorseMark
        {
            heavy,
            jumpy,
            slow
        }
        public horse(bool st, int hp, HorseMark mark_)
        {
            horseshoe = st;
            horsepower = hp;
            horsemark = mark_;
        }
        public static bool operator >(horse a, horse b) => (a.horsepower * Convert.ToInt32(a.horseshoe) > b.horsepower * Convert.ToInt32(b.horseshoe));
        public static bool operator <(horse a, horse b) => !(a > b);

        public static bool operator ==(horse a, horse b) => ((a.horsepower == b.horsepower) && (a.horseshoe == b.horseshoe));
        public static bool operator !=(horse a, horse b) => !(a == b);

        int IComparable<horse>.CompareTo(horse other)
        {
            // A null value means that this object is greater.
            if (other > this)
                return 1;
            else if (other < this)
                return -1;
            return 0;
        }

        bool IEquatable<horse>.Equals(horse other)
        {
            if (other == this) return true;
            return false;
        }

        public static implicit operator Car(horse h)
        {
            if (h.horsemark == horse.HorseMark.heavy)
            {
                return new Car(h.horseshoe, h.horsepower * 20, Car.Mark.BMW);
            }
            else if (h.horsemark == horse.HorseMark.jumpy)
            {
                return new Car(h.horseshoe, h.horsepower * 20, Car.Mark.mitsubishi);
            }
            return new Car(h.horseshoe, h.horsepower * 20, Car.Mark.nisan);
        }
        public static explicit operator horse(Car h)
        {
            if (h.mark == Car.Mark.BMW)
            {
                return new horse(h.studdedtire, h.horsepower / 20, horse.HorseMark.heavy);
            }
            else if (h.mark == Car.Mark.mitsubishi)
            {
                return new horse(h.studdedtire, h.horsepower / 20, horse.HorseMark.jumpy);
            }
            return new horse(h.studdedtire, h.horsepower / 20, horse.HorseMark.slow);
        }
    }
    class Car
    {
        public bool studdedtire = false;
        public int horsepower = 10;
        public Mark mark;
        public enum Mark
        {
            BMW,
            mitsubishi,
            nisan
        }
        public static explicit operator Car(horse h)
        {
            if (h.horsemark == horse.HorseMark.heavy)
            {
                return new Car(h.horseshoe, h.horsepower * 10, Mark.BMW);
            }
            else if (h.horsemark == horse.HorseMark.jumpy)
            {
                return new Car(h.horseshoe, h.horsepower * 10, Mark.mitsubishi);
            }
            return new Car(h.horseshoe, h.horsepower * 10, Mark.nisan);
        }
        public Car(bool st, int hp, Mark mark_)
        {
            studdedtire = st;
            horsepower = hp;
            mark = mark_;
        }

    }
    public delegate double Function(double x);

    class Program
    {

        static void Main(string[] args)
        {
            double GetGas(Car someCar) // немного не понял почему нельзя в пространстве имён просто создать ф-цию
            {
                return someCar.horsepower * 1.53;
            }
            horse someHorse = new horse(true, 2, horse.HorseMark.jumpy);
            horse someHorse2 = new horse(true, 4, horse.HorseMark.heavy);
            horse someHorse3 = new horse(true, 1, horse.HorseMark.slow);
            Console.WriteLine(GetGas(someHorse));
            Console.WriteLine(someHorse < someHorse2);



            Final final = new Final();
            final.ToSome();
            ((secondSome)final).ToSome();
            ((firstSome)final).ToSome();


            double Integrate(Function f, double a, double b)
            {
                // a < b
                double sum = 0;
                int N = 10000;
                for (int i = 0; i < N; i++)
                {
                    sum += f(a + i * (b - a) / N);
                }
                return sum / ((b - a) * N);
            }
            Console.WriteLine(Integrate(Math.Sin, 0, 1)); // точность - 4 знака после запятой
            Console.WriteLine(Integrate(Math.Cos, 0, 1));

            //вместо хомячков отсортирую лошадей
            List<horse> horses = new List<horse>();
            horses.Add(someHorse);
            horses.Add(someHorse2);
            horses.Add(someHorse3);
            horses.Sort();
            foreach(var a in horses)
            {
                Console.WriteLine(a.horsemark);
            }
            Console.ReadLine();
        }
    }
}
