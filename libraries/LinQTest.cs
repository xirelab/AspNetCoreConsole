using System;
using System.Collections.Generic;
using System.Linq;

namespace firtApp.libraries
{
    public class LinQ
    {
        public IList<classA> classAList { get; set; }
        public IList<classB> classBList { get; set; }
        
        public LinQ()
        {
            classAList = new List<classA>() { 
                new classA() { Id = 1, Name = "John", Age = 18, RefId = 1 } ,
                new classA() { Id = 2, Name = "Steve",  Age = 21, RefId = 1 } ,
                new classA() { Id = 3, Name = "Bill",  Age = 18, RefId = 2 } ,
                new classA() { Id = 4, Name = "Ram" , Age = 20, RefId = 2 } ,
                new classA() { Id = 5, Name = "Ron" , Age = 21 } 
            };
            classBList = new List<classB>() { 
                new classB(){ Id = 1, Address="class 1"},
                new classB(){ Id = 2, Address="class 2"},
                new classB(){ Id = 3, Address="class 3"}
            };
        }
        
        public void Simple()
        {
            Console.WriteLine("\nSimple LinQ...");
            var teenStudentsName = from s in classAList
                                    where s.Age > 12 && s.Age < 20
                                    select s; //new { Name = s.Name };

            teenStudentsName.ToList().ForEach(s => Console.WriteLine(s.Name));
        }

        public void GroupBy()
        {
            Console.WriteLine("\nGroupBy LinQ...");
            var studentsGroupByStandard = from s in classAList
                                            where s.Age > 18 
                                            group s by s.Id into sg
                                            orderby sg.Key 
                                            select new { sg.Key, sg };

            foreach (var group in studentsGroupByStandard)
            {
                Console.WriteLine("StandardID {0}:", group.Key);            
                group.sg.ToList().ForEach(st => Console.WriteLine(st.Name ));
            }
        }

        public void Joined()
        {
            Console.WriteLine("\nJoined LinQ...");
            var studentsWithStandard = from stad in classBList
                                        join s in classAList
                                        on stad.Id equals s.RefId
                                        into sg
                                            from std_grp in sg 
                                            orderby stad.Address, std_grp.Name 
                                            select new { 
                                                            StudentName = std_grp.Name, 
                                                            StandardName = stad.Address 
                                            };

            foreach (var group in studentsWithStandard)
            {
                Console.WriteLine("{0} is in {1}", group.StudentName, group.StandardName);
            }
        }

        public void Nested()
        {
            Console.WriteLine("\nNested LinQ...");
            var nestedQueries = from s in classAList
                                where s.Age > 18 && s.RefId ==
                                    (from std in classBList
                                    where std.Address == "class 2"
                                    select std.Id).FirstOrDefault()
                                select s;

            nestedQueries?.ToList()?.ForEach(s => Console.WriteLine(s?.Name));
        }
    }

    public class classA
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? RefId { get; set; }
    }

    public class classB
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }
}