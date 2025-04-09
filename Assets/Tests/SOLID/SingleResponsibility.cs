using NUnit.Framework;
using UnityEngine;

namespace Tests.SOLID
{
    public class SingleResponsibility
    {
        private SrCalculator _srCalculator = new();
        private SrLogger _srLogger = new();

        [Test]
        public void SingleResponsibilityTest()
        {
            Initialization();
            var result = _srCalculator.Add(2);
            _srLogger.Log(result.ToString());
        }

        private void Initialization()
        {
            _srCalculator = new SrCalculator(1);
            _srLogger = new SrLogger();
        }
    }

    public class SrCalculator
    {
        private readonly int _baseValue;

        public SrCalculator(int baseValue = 0)
        {
            _baseValue = baseValue;
        }

        public int Add(int a)
        {
            return a + _baseValue;
        }
    }

    public class SrLogger
    {
        public void Log(string log)
        {
            Debug.Log(log);
        }
    }
}