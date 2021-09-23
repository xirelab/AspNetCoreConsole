using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace firtApp.libraries
{
    public class Student : IEnumerable<int>
    {
        private int[] data;
        
        public Student()
        {
            // data = new int[] {3, 5, 7};
        }    

        public Student(int[] numbers)
        {
            data = numbers;
        }

        public void GetData(int[] value)  
        {  
            // delegate method
            data = value;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new StudentEnumerator(data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StudentEnumerator(data);
        }
    }

    public class StudentEnumerator : IEnumerator<int>
    {
        private int index = -1;
        private int[] myValues;

        // public int Current { get; private set;} = 0;
        public int Current => myValues[index];
        object IEnumerator.Current => Current;

        public StudentEnumerator(int[] values)
        {
            myValues = values;
        }

        public void Dispose()
        {        
        }

        public bool MoveNext()
        {
            index++;
            return index < myValues.Length;
        }

        public void Reset()
        {
            index= 0;
        }
    }
}