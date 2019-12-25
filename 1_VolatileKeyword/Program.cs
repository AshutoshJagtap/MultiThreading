using System;
using System.Threading;

namespace _1_VolatileKeyword
{
    class Program
    {
        static void Main()
        {
            // Create the worker thread object. This does not start the thread.
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine($"Main thread: starting worker thread...{Thread.CurrentThread.ManagedThreadId}");

            // Loop until the worker thread activates. 
            while (!workerThread.IsAlive) ;

            // Put the main thread to sleep for 1 millisecond to 
            // allow the worker thread to do some work.
            Thread.Sleep(1);

            // Request that the worker thread stop itself.
            workerObject.RequestStop();

            // Use the Thread.Join method to block the current thread  
            // until the object's thread terminates.
            workerThread.Join();
            Console.WriteLine($"Main thread: worker thread has terminated...{Thread.CurrentThread.ManagedThreadId}");
            Console.Read();
        }
    }

    public class Worker
    {
        // Keyword volatile is used as a hint to the compiler that this data 
        // member is accessed by multiple threads. 
        private volatile bool _shouldStop;

        // This method is called when the thread is started. 
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine($"Worker thread: working...{Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"Worker thread: terminating gracefully...{Thread.CurrentThread.ManagedThreadId}");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}
