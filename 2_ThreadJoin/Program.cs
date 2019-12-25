using System;
using System.Threading;

namespace _2_ThreadJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(PrintMessage);
            t.Start();

            if (t.Join(10))
            {
                Console.WriteLine("Work Completed!");
            }
            else
            {
                Console.WriteLine("Work not Completed!");
            }
            Console.WriteLine($"Main thread exits...{Thread.CurrentThread.ManagedThreadId}");
            Console.Read();
        }

        static void PrintMessage()
        {
            Console.WriteLine($"Printing msg...{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(100);
        }
    }
}
