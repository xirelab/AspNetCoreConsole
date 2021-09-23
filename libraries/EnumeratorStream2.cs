using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace firtApp.libraries
{
    public class MyReader2 : IEnumerable<int>
    {
        private Stream myStream;

        public MyReader2(Stream stream)
        {
            myStream = stream;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new MyReaderEnumerator2(myStream);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class MyReaderEnumerator2 : IEnumerator<int>
    {
        private StreamReader reader;
        private int value;
        public int Current => value;
        object IEnumerator.Current => Current;

        public MyReaderEnumerator2(Stream stream)
        {
            reader = new StreamReader(stream);
        }
        
        public bool MoveNext()
        {       
            bool isValid = false;
            string item;
            // Console.WriteLine("MoveNext start:" + value);
            do {
                item = reader.ReadLine();
                if (IsValid(item)) {
                    isValid = true;
                    value = Convert.ToInt32(item);
                }
            } while(item!= null && !isValid);
            // Console.WriteLine("MoveNext value:" + value);
            return item != null && isValid;
        }

        public void Reset()
        {
            Console.WriteLine("re-start........");
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        public void Dispose()
        {
            Console.WriteLine("bye!");
            if (reader != null)
            {
                // Dispose(true);
                reader.Close();
                // reader.Dispose();
                // GC.SuppressFinalize(this);
            }
        }

        // protected virtual void Dispose(bool disposing)
        // {
        //     reader.Close();
        //     reader.Dispose();    
        // }

        private bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int x);
        }
    }
}