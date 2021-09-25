using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

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
            for(int i=0; i<5; i++) {                
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

        public async Task<int> AyncRealExample(string file)
        {
            Console.WriteLine("Async Realtime example starting...");
            var sw = new Stopwatch();
            sw.Start();
            int length = 0;
            // using (StreamReader reader = new StreamReader(file))
            // {
            //     string s = reader.ReadToEnd();
            //     length = s.Length;
            // }
            Console.WriteLine("File read Sync took {0} seconds", sw.Elapsed.TotalSeconds);
            using (StreamReader reader = new StreamReader(file))
            {
                string s = await reader.ReadToEndAsync();
                length = s.Length;
            }
            Console.WriteLine("File read Async took {0} seconds", sw.Elapsed.TotalSeconds);
            Console.WriteLine("Async Realtime example completed...");
            return length;            
        }

        private async static Task<int> Running(int ms)
        {
            Console.WriteLine("Running for {0}", ms);
            await Task.Delay(ms);
            Console.WriteLine("Running finished after {0}", ms);
            return ms;
        }

        public void MultipleAsync1()
        {
            Console.WriteLine("Async multiple Call trial 1 starting...");
            var sw = new Stopwatch();
            sw.Start();
            string filePath = "/home/shijuloves/XireLab/NetCoreConsole/testdata/testfile2.txt";
            
            var tasks = new List<Task>
            {
                Running(5000),
                Running(8000),
                Running(3000),
                TestDelay1Async(20),
                AyncRealExample(filePath),
                asyncTask_TestDelay()
            };

            // One way
            // int[] result = await Task.WhenAll(tasks);
            // Console.WriteLine("Total Running time: " + Convert.ToInt32(result[0] + result[0]) + " ms");
            // Console.WriteLine("Actual Running time: " + sw.Elapsed.TotalSeconds + " seconds");

            // Another way            
            Task.WaitAll(tasks.ToArray());            
            Console.WriteLine("Total Running time: " + Convert.ToInt32(5 + 3 + 8) + " seconds");            
            Console.WriteLine("Actual Running time: " + sw.Elapsed.TotalSeconds + " seconds");
            sw.Stop();
            Console.WriteLine("Async multiple Call trial 1 completed...");
        }

        public async Task<double> MultipleAsync2()
        {           
            Console.WriteLine("Async multiple Call Trial 2 starting...");
            var sw = new Stopwatch();            
            sw.Start();            
            var tasks = new List<Task>
            {
                // new Task( () => { Task.Delay(1000); Console.WriteLine("task1 execution"); }),
                // new Task( () => { Task.Delay(2000); Console.WriteLine("task2 execution"); }),
                // new Task( () => { Task.Delay(3000); Console.WriteLine("task3 execution"); }),
                // new Task( () => { Task.Delay(4000); Console.WriteLine("task4 execution"); }),
                // new Task( () => { Task.Delay(5000); Console.WriteLine("task5 execution"); }),
                new Task( async () => { await Task.Run(() => { Task.Delay(1000).Wait(); Console.WriteLine("task1 execution"); }); }),
                new Task( async () => { await Task.Run(() => { Task.Delay(2000).Wait(); Console.WriteLine("task2 execution"); }); }),
                new Task( async () => { await Task.Run(() => { Task.Delay(3000).Wait(); Console.WriteLine("task3 execution"); }); }),
                new Task( async () => { await Task.Run(() => { Task.Delay(4000).Wait(); Console.WriteLine("task4 execution"); }); }),
                new Task( async () => { await Task.Run(() => { Task.Delay(5000).Wait(); Console.WriteLine("task5 execution"); }); }),
                // new Task( async () => { await Task.Delay(1000); Console.WriteLine("task1 execution"); }),
                // new Task( async () => { await Task.Delay(2000); Console.WriteLine("task2 execution"); }),
                // new Task( async () => { await Task.Delay(3000); Console.WriteLine("task3 execution"); }),
                // new Task( async () => { await Task.Delay(4000); Console.WriteLine("task4 execution"); }),
                // new Task( async () => { await Task.Delay(5000); Console.WriteLine("task5 execution"); }),
            };
            
            Console.WriteLine("Tasks added.. going to start execution..");            
            tasks.ForEach(t => t.Start());

            Console.WriteLine("Tasks Started.. wait untill all completed..");
            await Task.WhenAll(tasks);
            /* OR below is another way  */
            // while(tasks.Count > 0)
            // {
            //     Task finishedTask = await Task.WhenAny(tasks);
            //     tasks.Remove(finishedTask);
            // }

            Console.WriteLine("Async multiple Call trial 2 completed...");
            return sw.Elapsed.TotalSeconds;
        }
    }
}