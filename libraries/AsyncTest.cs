using System.Threading.Tasks;
using System;

namespace NetCoreConsole.libraries
{
    public class AsyncTest
    {
        public AsyncTest()
        {
            
        }
        
        public async Task<int> TestDelay1()
        {
            Console.WriteLine("Async delay1 starting...");
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Delay test 1 : " + i+1);
                    // Task.Delay(500).Wait();
                    count += 1;
                }
            });
            // for(int i=0; i<10; i++) {
            //     Console.WriteLine("Delay test 1 : " + i+1);
            //     Task.Delay(500).Wait();
            // }
            Console.WriteLine("Async delay1 completed...");
            return count;
        }

        public void TestDelay2()
        {
            Console.WriteLine("\nAsync delay2 starting...");
            // await Task.Run(() =>
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Console.WriteLine("Delay test 2 : " + i+1);
            //         Task.Delay(500).Wait();
            //     }
            // });
            for(int i=0; i<10; i++) {
                Console.WriteLine("Delay test 2 : " + i+1);
                Task.Delay(500).Wait();
            }
            Console.WriteLine("Async delay2 completed...");
        }
    }
}