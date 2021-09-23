using System.Collections.Generic;

namespace LearningNetCore.libraries
{
    public class SampleIndexer
    {
        // private string[] array = new string[10];
        private List<string> array = new List<string>();

        public string this[int i]
        {
            get { return array[i]; }
            set {  
                    // array[i] = value;
                    if (array.Count > i) array[i] = value;
                    else array.Add(value); 
                }
        }

        public string this[string name]
        {
            get { return array.Find(x => x == name); }
            set {  
                    var xx = array.Find(x => x == name);
                    if (xx != null) xx = value;
                    else array.Add(value); 
                }
        }
    }
}