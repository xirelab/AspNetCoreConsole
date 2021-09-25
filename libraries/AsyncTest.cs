using System.Threading;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace NetCoreConsole.libraries
{
    public class AsyncTest
    {
        public void syncTask_ForTestAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sync: Starting");
            Thread.Sleep(1000);
            Console.WriteLine("Sync: Running for {0} seconds", sw.Elapsed.TotalSeconds);
            Console.WriteLine("Sync: Done");
        }
        public async Task asyncTask_TestDelay()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("async: Starting");
            Task delay = Task.Delay(3000);
            Console.WriteLine("async: Running for {0} seconds", sw.Elapsed.TotalSeconds);
            await delay;
            // await Task.Delay(3000);
            Console.WriteLine("async: Running for {0} seconds", sw.Elapsed.TotalSeconds);
            Console.WriteLine("async: Done");
        }
        
        public async Task DoSimpleAsync()
        {
            Console.WriteLine("Async call starting...");
            await DoTask();
            Console.WriteLine("Async call completed...");
        }

        private Task DoTask()
        {
            return Task.Factory.StartNew(() => 
            {
                var sw = new Stopwatch();
                sw.Start();
                Console.WriteLine("Async Task starting...");
                Thread.Sleep(3000);
                Console.WriteLine("Async Task Running for {0} seconds", sw.Elapsed.TotalSeconds);
                Console.WriteLine("Async Task completed...");
            });            
        }

        public void TestDelayWait()
        {
            Console.WriteLine("Async Wait starting...");
            for(int i=0; i<10; i++) {                
                Task.Delay(500).Wait();
                // Thread.Sleep(500);  // either use this. in this case both working same
                Console.WriteLine("Async Wait : " + i);
            }
            Console.WriteLine("Async Wait completed...");  // tis will display after 5 sec..
        }

        public async Task<int> TestDelay1Async(int count)
        {
            Console.WriteLine("Async Call 1 starting...");
            int counter = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Delay test 1 : " + i+1);
                    Task.Delay(500).Wait(); // making its wait. will wait untill it finishes
                    // Thread.Sleep(500);   // since its already under task, we can also use Sleep
                    counter += 1;
                }
            });
            Console.WriteLine("Async Call 1 completed...");
            return counter;
        }

        public async Task TestDelay2Async(int count)
        {
            Console.WriteLine("Async Call 2 starting...");
            for(int i=0; i<count; i++) {
                Console.WriteLine("Delay test 2 : " + i+1);
                await Task.Delay(500); //no wait here.. to wait, ned to implement wait in consumer
            }
            Console.WriteLine("Async Call 2 completed...");
        }        
    }
}