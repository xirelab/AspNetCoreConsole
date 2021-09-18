
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

    public void Dispose()
    {
        reader.DiscardBufferedData();
        reader.Close();
        reader.Dispose();        
    }

    public bool MoveNext()
    {       
        bool isValid = false;
        string item;
        do {
            item = reader.ReadLine();
            if (IsValid(item)) {
                isValid = true;
                value = Convert.ToInt32(item);
            }
        } while(!isValid);
        
        return item != null;
    }

    public void Reset()
    {
        reader.DiscardBufferedData();
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
    }

    private bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int x);
    }
}