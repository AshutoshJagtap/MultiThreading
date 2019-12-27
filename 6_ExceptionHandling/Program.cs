using System;
using System.Threading.Tasks;

namespace _6_ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            ExceptionFromWait();
            ExceptionFromAsyncMethod();
            ExceptionFromNestedTask();
            UseMethodWithAsyncTask();

            //If there is no error expected from async operation which return task and you don’t want to wit anymore.
            //That time attached a child task with onlyOnFaulted option.
            Task t = Task.Factory.StartNew(() => Console.WriteLine("Welcome !"));
            t.ContinueWith(prevTask=> {
                prevTask.Exception.Flatten().Handle(ex =>
                {
                    if (ex is InvalidOperationException)
                        Console.WriteLine(ex.Message);

                    return ex is InvalidOperationException;
                });
            }, TaskContinuationOptions.OnlyOnFaulted);

            Console.Read();
        }

        static void ExceptionFromWait()
        {
            // Start a Task that throws a NullReferenceException:
            Task task = Task.Run(() => { throw null; });
            try
            {
                task.Wait();
            }
            catch (AggregateException aex)
            {
                if (aex.InnerException is NullReferenceException)
                    Console.WriteLine("NullReferenceException !");
                else
                    throw;
            }
        }

        public static async Task ExceptionFromAsyncMethod()
        {
            var task1 = Task.Run(() => throw new NotImplementedException("NotImplementedException is expected!"));
            var task2 = Task.Run(() => throw new InvalidOperationException("InvalidOperationException is expected!"));

            Task allTasks = Task.WhenAll(task1, task2);

            try
            {
                await allTasks;
            }
            catch
            {
                allTasks.Exception.Handle(ex =>
                {
                    if (ex is InvalidOperationException)
                        Console.WriteLine(ex.Message);

                    return ex is InvalidOperationException;
                });
            }

        }

        public static void ExceptionFromNestedTask()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                var child1 = Task.Factory.StartNew(() =>
                {
                    var child2 = Task.Factory.StartNew(() => throw new InvalidOperationException("Attached child2 faulted."), TaskCreationOptions.AttachedToParent);

                    throw new InvalidOperationException("Attached child1 faulted.");

                }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                task1.Exception.Flatten().Handle(ex =>
                {
                    if (ex is InvalidOperationException)
                        Console.WriteLine(ex.Message);

                    return ex is InvalidOperationException;
                });
            }
        }

        public static async Task UseMethodWithAsyncTask()
        {
            try
            {
                await MethodWithException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task MethodWithException()
        {
            await Task.Delay(1000);
            throw new Exception("The control hit the catch line if async Task is used.");
        }

    }
}
