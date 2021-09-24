using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetCoreConsole.libraries;

namespace NetCoreConsole
{
    public delegate void MyDelegate(int[] data);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# Learning tasks...");

            TestEnumerators();  // Test Enumerators
            
            SampleIndexerTest(); // Test Indexer

            TestExamQuestion();  // Test ExamQuestion
            
            TestLinQ(); // Test LinQ

            TestAsync(); // Test Async

            // Console.ReadLine();
        }
        
        static void TestEnumerators()
        {
            // Trial 1 - default
            Console.WriteLine("\nTest Enumerators - Trial 1 - default");
            var array = new int[] {1,2,3};
            var enume = array.GetEnumerator();
            while (enume.MoveNext())
            {
                 Console.WriteLine($"A is {enume.Current}");
            }

            // Trial 2 - Int Enumerator - using delegate and foreach
            Console.WriteLine("\nTest Enumerators - Trial 2 - Int Enumerator - using delegate and foreach");
            var students1 =  new Student();
            MyDelegate del = students1.GetData;
            del(new int[]{56, 67, 89});
            foreach(var st in students1)
            {
                Console.WriteLine($"Student1 is {st}");
            }

            // Trial 3 - Int Enumerator - using while
            Console.WriteLine("\nTest Enumerators - Trial 3 - Int Enumerator - using while");
            var students2 =  new Student(new int[] {8, 9, 10});
            var student = students2.GetEnumerator();
            while (student.MoveNext())
            {
                 Console.WriteLine($"Student2 is {student.Current}");
            }

            // Trial 4 - Streamer Enumerator - not a proper way
            Console.WriteLine("\nTest Enumerators - Trial 4 - Streamer Enumerator - not a proper way");
            var filestream = new FileStream("/home/shijuloves/XireLab/NetCoreConsole/testdata/testfile.txt", FileMode.Open);            
            var arr = new MyReader(filestream).ToArray();
            foreach(var item in arr)
            {
                Console.WriteLine($"file data1 is {item}");
            }
            // var myReader =  new MyReader(filestream).GetEnumerator();
            // while (myReader.MoveNext())
            // {
            //      Console.WriteLine($"File data 1 is {myReader.Current}");
            // }

            // Trial 5 - Streamer Enumerator - proper way
            Console.WriteLine("\nTest Enumerators - Trial 5 - Streamer Enumerator - proper way");
            var filestream2 = new FileStream("/home/shijuloves/XireLab/NetCoreConsole/testdata/testfile2.txt", FileMode.Open);
            var myReader2 =  new MyReader2(filestream2);
            foreach(var item in myReader2)
            {
                Console.WriteLine($"file data2 is {item}");
            }
        }

        static void SampleIndexerTest()
        {
            Console.WriteLine("\nTest Indexer...");
            var indexer = new SampleIndexer();
            indexer[0] = "SK";
            Console.WriteLine($"Value in indexer is {indexer[0]}");
        }

        static void TestExamQuestion()
        {
            Console.WriteLine("\nTest Exam question...");
            var j=23; var k=23;
            var req = new int[]{23,23,5,6,7,8,9,6,5,3,4,8,6,7,5,5};
            var res = new Solution().solution(j, k, req);
            Console.WriteLine("response = " + res);
        }

        static void TestLinQ()
        {
            Console.WriteLine("\nTest LinQ...");
            var linq = new LinQ();
            linq.Simple();
            linq.GroupBy();
            linq.Joined();
            linq.Nested();
        }

        static async void TestAsync()
        {
            Console.WriteLine("\nTest Async...");
            var async = new AsyncTest();
            // using (Task task = async.TestDelay1())
            // {
            //     int count = await task;
            // }
            int count = await async.TestDelay1();
            Console.WriteLine("\nTest Asyncc - count = " + count);
            async.TestDelay2();
        }
    }    
}
