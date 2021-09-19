
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MyReader3 : IEnumerable<int>
{
    private readonly List<string> items = new List<string>();

    public MyReader3(Stream stream)
    {
        using (var reader = new StreamReader(stream))
        {
            string line;            
            while((line = reader.ReadLine()) != null)  
            {  
                items.Add(line);
            } 
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new MyReader3Enumerator(items);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // return new MyReader3Enumerator(items);
        return this.GetEnumerator();
    }
}

public class MyReader3Enumerator : IEnumerator<int>
{
    private int index = -1;
    private int itemCount = 0;
    private readonly List<string> myItems = new List<string>();
    public int Current => Convert.ToInt32(myItems[index]);
    object IEnumerator.Current => Current;

    public MyReader3Enumerator(List<string> items)
    {
        myItems = items;
        itemCount = items.Count;
    }

    public void Dispose()
    {        
    }

    public bool MoveNext()
    {        
        if (index < itemCount) {
            do {
                index++;
            } while (index < itemCount && !IsValid(myItems[index]));
        }

        return index < itemCount;
    }

    public void Reset()
    {
        // index= 0;
    }

    private bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int x);
    }
}