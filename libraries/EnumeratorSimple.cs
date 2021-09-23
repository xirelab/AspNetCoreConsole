using System;
using System.Collections;

namespace firtApp.libraries
{
    public class Teacher : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new TeacherEnumerator();
        }
    }

    public class TeacherEnumerator : IEnumerator
    {
        public object Current { get; private set; } = 0;

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}