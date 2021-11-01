using System;
using System.Collections.Generic;

namespace HW5
{
    // не забывайте вставлять пустые строки между объявлениями классов и методов внутри классов
    public delegate void Notify();

    static public class EventBus<T>
    {
        public static event Notify Event = null;

        public static void Post(Action<Notify> subscribe_self)
        {
            subscribe_self.Invoke(Invoke);
        }

        public static void Subscribe(Notify notification)
        {
            Event += notification;
        }

        public static void Invoke()
        {
            Console.WriteLine(Event.ToString());
            Event?.Invoke();
        }
    }

    public class FirstPublisher
    {
        private string book = "firstBook";

        public static event Notify Event = null;

        public FirstPublisher(String book_name)
        {
            book = book_name;
        }

        public static void Stream(Notify on_publish)
        {
            Event += on_publish;
        }

        public void Publish()
        {
            Console.WriteLine(book);
            Event?.Invoke();
        }
    }

    public class FirstUser
    {
        public void GetNotification()
        {
            Console.WriteLine("First User Got Notification");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FirstPublisher Michail = new FirstPublisher("MishaPishet");
            FirstPublisher Anton = new FirstPublisher("AntonBaton");
            FirstUser Sasha = new FirstUser();

            EventBus<FirstPublisher>.Post(FirstPublisher.Stream);
            EventBus<FirstPublisher>.Subscribe(Sasha.GetNotification);

            Michail.Publish();
            Anton.Publish();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
