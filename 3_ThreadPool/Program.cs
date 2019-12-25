using System;
using System.Threading;

namespace _3_ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use ThreadPool for a worker thread        
            ThreadPool.QueueUserWorkItem(BackgroundTask, "CLR's ThreadPool");
            Console.WriteLine("Main thread does some work, then sleeps.\n");

            int workers, ports;
            // Get available threads  
            ThreadPool.GetAvailableThreads(out workers, out ports);
            //Should be less than Max--because we have started 1 thread above.
            Console.WriteLine($"Availalbe worker threads: {workers} ");
            Console.WriteLine($"Available completion port threads: {ports}");

            Console.WriteLine();

            int workers1, ports1;
            // Get maximum number of threads  
            ThreadPool.GetMaxThreads(out workers1, out ports1);
            Console.WriteLine($"Maximum worker threads: {workers1} ");
            Console.WriteLine($"Maximum completion port threads: {ports1}");

            Console.WriteLine();

            int minWorker, minIOC;
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            Console.WriteLine($"Minimum worker threads: {minWorker} ");
            Console.WriteLine($"Minimum completion port threads: {minIOC}");

            Console.WriteLine();

            // Set minimum threads  
            ThreadPool.SetMinThreads(4, 5);
            int minWorker1, minIOC1;
            ThreadPool.GetMinThreads(out minWorker1, out minIOC1);
            Console.WriteLine($"Minimum worker threads: {minWorker1} ");
            Console.WriteLine($"Minimum completion port threads: {minIOC1}");

            Thread.Sleep(500);
            Console.WriteLine("\nMain thread exits.");
            Console.Read();
        }
        static void BackgroundTask(Object stateInfo)
        {
            Console.WriteLine($"Hello! I'm a worker from {stateInfo.ToString()}\n");
            Thread.Sleep(1000);
        }
    }
}
