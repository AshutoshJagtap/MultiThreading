using System;
using System.Threading;
using System.Threading.Tasks;

namespace _5_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Greeding);//hot task

            Task.Factory.StartNew(WhatTypeOfThreadAmI, TaskCreationOptions.LongRunning);//compute-base task

            //Passing data to task and returning data from task
            Task<string> t = new Task<string>(WhereIam, "Task");//cold task
            t.Start();
            Console.WriteLine(t.Result);

            Console.Read();
        }

        static void Greeding()
        {
            Console.WriteLine("Hello World !");
        }
        static void WhatTypeOfThreadAmI()
        {
            Console.WriteLine("I'm a {0} thread",
            Thread.CurrentThread.IsThreadPoolThread ? "Thread Pool" : "Custom");
        }

        static string WhereIam(Object location)
        {
            return $"I am in {location}";
        }
    }
}
