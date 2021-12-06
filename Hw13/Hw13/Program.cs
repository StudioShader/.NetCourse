using System;
using System.Threading;
using System.Data.Common;

namespace Hw13
{
    class Program
    {

        internal class DeadLock : IDisposable
        {
            private readonly Mutex mutexA;

            private readonly Mutex mutexB;
            public DeadLock()
            {
                mutexA = new Mutex(false);
                mutexB = new Mutex(false);
            }

            public void Dispose()
            {
                mutexA.Dispose();
                mutexB.Dispose();
            }

            private void ThreadRoutineA(Int32 threadNumber)
            {
                Console.WriteLine("Thread.LocalNumber = {0}: start", threadNumber);

                mutexB.WaitOne();

                Console.WriteLine("Thread.LocalNumber = {0}: mutex acquired", threadNumber);
                Thread.Sleep(2000);
                mutexA.WaitOne();
                Console.WriteLine("Full inside ind: ", threadNumber);
                mutexA.ReleaseMutex();
                mutexB.ReleaseMutex();
            }
            private void ThreadRoutineB(Int32 threadNumber)
            {
                Console.WriteLine("Thread.LocalNumber = {0}: start", threadNumber);

                mutexA.WaitOne();

                Console.WriteLine("Thread.LocalNumber = {0}: mutex acquired", threadNumber);
                Thread.Sleep(2000);
                mutexB.WaitOne();
                Console.WriteLine("Full inside ind: ", threadNumber);
                mutexB.ReleaseMutex();
                mutexA.ReleaseMutex();
            }

            public void Run()
            {
                Thread[] threads = new Thread[2];
                Int32 copy1 = 1;
                Int32 copy2 = 2;
                threads[0] = new Thread(() => ThreadRoutineA(copy1));
                threads[1] = new Thread(() => ThreadRoutineB(copy2));
                threads[0].Start();
                threads[1].Start();

                for (Int32 i = 0; i < 2; ++i)
                {
                    threads[i].Join();
                }

                Console.WriteLine("Hello World!");
                Console.ReadKey();
            }
        }

        internal class SinchronizedLock : IDisposable
        {
            private readonly Mutex mutexA;

            private readonly Mutex mutexB;
            public SinchronizedLock()
            {
                mutexA = new Mutex(false);
                mutexB = new Mutex(false);
            }

            public void Dispose()
            {
                mutexA.Dispose();
                mutexB.Dispose();
            }

            private void ThreadRoutineA(Int32 threadNumber)
            {
                for (int i = 0; i < 10; i++)
                {
                    mutexB.WaitOne();
                    mutexA.WaitOne();
                    mutexB.ReleaseMutex();
                    Console.WriteLine("the number: " + (i) + " from thread: " + threadNumber);
                    mutexA.ReleaseMutex();
                }
            }
            private void ThreadRoutineB(Int32 threadNumber)
            {
                for (int i = 0; i < 10; i ++)
                {
                    mutexB.WaitOne();
                    mutexA.WaitOne();
                    mutexB.ReleaseMutex();
                    Console.WriteLine("the number: " + (i) + " from thread: " + threadNumber);
                    mutexA.ReleaseMutex();
                }
            }

            public void Run()
            {
                Thread[] threads = new Thread[2];
                Int32 copy1 = 1;
                Int32 copy2 = 2;
                threads[0] = new Thread(() => ThreadRoutineA(copy1));
                threads[1] = new Thread(() => ThreadRoutineB(copy2));
                threads[0].Start();
                threads[1].Start();

                for (Int32 i = 0; i < 2; ++i)
                {
                    threads[i].Join();
                }

                Console.WriteLine("Hello World!");
                Console.ReadKey();
            }
        }
        public class FooBar
        {
            private readonly Mutex mutexWait;
            private readonly Mutex mutexGo;
            private int n;
            public FooBar(int n)
            {
                mutexWait = new Mutex(false);
                mutexGo = new Mutex(false);
                this.n = n;
            }
            public void Foo(Action printFoo)
            {

                for (int i = 0; i < n; i++)
                {
                    mutexWait.WaitOne();
                    mutexGo.WaitOne();
                    mutexWait.ReleaseMutex();
                    // printFoo() outputs "foo". Do not change or remove this line.
                    printFoo();
                    mutexGo.ReleaseMutex();
                }
            }
            public void Bar(Action printBar)
            {

                for (int i = 0; i < n; i++)
                {
                    mutexWait.WaitOne();
                    mutexGo.WaitOne();
                    mutexWait.ReleaseMutex();
                    // printBar() outputs "bar". Do not change or remove this line.
                    printBar();
                    mutexGo.ReleaseMutex();
                }
            }
        }
        static void Main(string[] args)
        {
            DeadLock deads = new DeadLock();
            //deads.Run(); // cooment if you want to proceed to the next tasks
            Console.WriteLine("Hello World!");

            //SECOND TASK
            //SinchronizedLock SL = new SinchronizedLock();
            //SL.Run();
            //Console.ReadLine();
            FooBar FB = new FooBar(4);
            Thread[] threads = new Thread[2];
            Int32 copy1 = 1;
            Int32 copy2 = 2;
            threads[0] = new Thread(() => FB.Foo(delegate () { Console.Write("foo"); }));
            threads[1] = new Thread(() => FB.Bar(delegate () { Console.WriteLine("bar"); }));
            threads[0].Start();
            threads[1].Start();

            for (Int32 i = 0; i < 2; ++i)
            {
                threads[i].Join();
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();

        }
    }
}
