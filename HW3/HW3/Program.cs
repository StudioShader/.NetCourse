using System;

namespace HW3
{
    public sealed class Node<T>
    {
// публичные методы и свойства начинайте с заглавной буквы
// вставляйте пустые строки между логическими блоками кода
        public T value { get; }
        public Node<T> next;
        public Node(T _value)
        {
            value = _value;
        }

        public static Node<T> FindCommonNode(Node<T> head1, Node<T> head2)
        {
            var current1 = head1;
            var current2 = head2;
            // algorithm
            while (current1.next != null)
            {
                current2 = head2;
                while (current2.next != null)
                {
                    if (current1 == current2)
                    {
                        return current2;
                    }
                    current2 = current2.next;
                }
                current1 = current1.next;
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
// для проверки работоспособности вы требуете довольно много действий со стороны пользователя
// лучше всего это оформить в виде нескольких автоматических проверок, где проверяются наиболее интересные пограничные случаи
// а еще лучше начать использовать юнит-тесты, как это мы делаем при разборе примеров на практике

            // FOR INTS   -----   x>3 что бы в третьем элементе два списка совпали по ссылке
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("nmber for first?");
            int a = Convert.ToInt32(Console.ReadLine());
            Node<int> head = new Node<int>(a);
            var current = head;
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine("nmber for first?");
                a = Convert.ToInt32(Console.ReadLine());
                current.next = new Node<int>(a);
                current = current.next;
            }

            // FOR INTS   -----   x>5 что бы в пятом элементе два списка совпали по ссылке
            x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("nmber for second?");
            a = Convert.ToInt32(Console.ReadLine());
            Node<int> head2 = new Node<int>(a);
            current = head2;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("nmber for second?");
                a = Convert.ToInt32(Console.ReadLine());
                current.next = new Node<int>(a);
                current = current.next;
            }
            current.next = head.next.next.next;


            Console.WriteLine(Node<int>.FindCommonNode(head, head2).value);
            Console.ReadLine();
        }
    }
}
