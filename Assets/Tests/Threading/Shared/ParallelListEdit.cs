using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Debug = UnityEngine.Debug;

namespace Tests.Threading.Shared
{
    public class ParallelListEdit
    {
        private const int N = 100;
        private const int Delay = 10;
        private readonly List<string> _list = new();

        [Test]
        public void ParallelListRWTest()
        {
            //Will update list
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < N; i++) _list.Add("0");
            //update list using parallel
            Parallel.For(0, N, i =>
            {
                Thread.Sleep(Delay);
                _list[i] += "1";
            });
            Parallel.For(0, N, i =>
            {
                Thread.Sleep(Delay);
                _list[i] += "2";
            });

            Debug.Log("Final:" + _list[N - 1]);
            Debug.Log("Total time: " + sw.ElapsedMilliseconds);
        }
    }
}