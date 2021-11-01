using System;
using System.Runtime.ExceptionServices;
using System.Text;

namespace HW8
{
    class Program
    {
        
        static void Main(string[] args)
        {
            void Recode(string s)
            {
                Encoding decoder = Encoding.UTF8;
                Byte[] encodeBytes = decoder.GetBytes(s);
                Console.WriteLine(BitConverter.ToString(encodeBytes));
                Console.WriteLine(decoder.GetString(encodeBytes));
            }
            // FIRST TASK
            string s1 = "Hi hello";
            string s2 = "Baum hängt am Baum";
            // Для японцкого нужна дополнительная настройка окружения
            string s3 = "木にぶら下がっている木";
            Recode(s1);
            Recode(s2);
            Recode(s3);

            // SECOND TASK
            void Foo() => throw new InvalidOperationException("foo");

            Exception original = null;
            ExceptionDispatchInfo dispatchInfo = null;
            try
            {
                try
                {
                    Foo();
                }
                catch (Exception ex)
                {
                    original = ex;
                    dispatchInfo = ExceptionDispatchInfo.Capture(ex);
                    throw ex;
                }
            }
            catch (Exception ex2)
            {
                // ex2 is the same object as ex. But with a mutated StackTrace.
                Console.WriteLine(ex2 == original);  // True
            }

            // So now "original" has lost the StackTrace containing "Foo":
            Console.WriteLine(original.StackTrace.Contains("Foo"));  // False

            // But dispatchInfo still has it:
            try
            {
                dispatchInfo.Throw();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.Contains("Foo"));   // True
            }

            Console.ReadLine();

        }
    }
}
