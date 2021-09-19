
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace firtApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!!!!");

            TestEnumerators();  // To Test Enumerators
            
            SampleIndexerTest(); // SampleIndexer test

            var j=23; var k=23;
            var req = new int[]{23,23,5,6,7,8,9,6,5,3,4,8,6,7,5,5};
            var res = new Solution().solution(j, k, req);
            Console.WriteLine("response = " + res);

            // Console.ReadLine();
        }
        
        static void TestEnumerators()
        {
            // Trial 1 - default
            var array = new int[] {1,2,3};
            var enume = array.GetEnumerator();
            while (enume.MoveNext())
            {
                 Console.WriteLine($"A is {enume.Current}");
            }

            // Trial 2 - Int Enumerator - using foreach
            var students1 =  new Student();
            foreach(var st in students1)
            {
                Console.WriteLine($"Student1 is {st}");
            }

            // Trial 3 - Int Enumerator - using while
            var students2 =  new Student(new int[] {8, 9, 10});
            var student = students2.GetEnumerator();
            while (student.MoveNext())
            {
                 Console.WriteLine($"Student2 is {student.Current}");
            }

            // Trial 4 - Streamer Enumerator - not a proper way
            var filestream = new FileStream("/home/shijuloves/XireLab/firtApp/testfile.txt", FileMode.Open);
            // var myReader =  new MyReader(filestream).GetEnumerator();
            // while (myReader.MoveNext())
            // {
            //      Console.WriteLine($"File data 1 is {myReader.Current}");
            // }

            IEnumerable<int> it = new MyReader(filestream);
            int[] arr = it.ToArray();
            foreach(var item in arr)
            {
                Console.WriteLine($"file data1 is {item}");
            }

            // Trial 5 - Streamer Enumerator - proper way
            var filestream2 = new FileStream("/home/shijuloves/XireLab/firtApp/testfile2.txt", FileMode.Open);
            var myReader2 =  new MyReader2(filestream2);
            foreach(var item in myReader2)
            {
                Console.WriteLine($"file data2 is {item}");
            }
        }

        static void SampleIndexerTest()
        {
            var indexer = new SampleIndexer();
            indexer[0] = "SK";
            Console.WriteLine($"Value in indexer is {indexer[0]}");
        }
    }

    
}
