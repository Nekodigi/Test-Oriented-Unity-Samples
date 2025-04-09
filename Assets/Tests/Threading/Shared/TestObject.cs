using System;

namespace Tests.Threading.Shared
{
    [Serializable]
    public class TestObject
    {
        public int Value;


        public TestObject(int i)
        {
            Value = i;
        }

        public TestObject() : this(0)
        {
        }
    }
}