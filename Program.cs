
using System;
using System.IO;

namespace firtApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!!!!");

            TestEnumerators();  // To Test Enumerators
            
            // Console.ReadLine();
        }
        
        static void TestEnumerators()
        {
            // var array = new int[] {1,2,3};
            // var enume = array.GetEnumerator();
            // while (enume.MoveNext())
            // {
            //      Console.WriteLine($"A is {enume.Current}");
            // }

            // Int Enumerator
            var students1 =  new Student();

            foreach(var st in students1)
            {
                Console.WriteLine($"Student1 is {st}");
            }

            var students2 =  new Student(new int[] {8, 9, 10});
            var student = students2.GetEnumerator();
            while (student.MoveNext())
            {
                 Console.WriteLine($"Student2 is {student.Current}");
            }

            // Streamer Enumerator
            var filestream = new FileStream("/home/shijuloves/XireLab/firtApp/testfile.txt", FileMode.Open);
            var myReader =  new MyReader(filestream).GetEnumerator();
            while (myReader.MoveNext())
            {
                 Console.WriteLine($"File data 1 is {myReader.Current}");
            }

            // Streamer Enumerator
            var filestream2 = new FileStream("/home/shijuloves/XireLab/firtApp/testfile2.txt", FileMode.Open);
            var myReader2 =  new MyReader2(filestream2);
            foreach(var item in myReader2)
            {
                Console.WriteLine($"file data2 is {item}");
            }
        }
    }

    
}
