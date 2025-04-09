using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Debug = UnityEngine.Debug;

namespace Tests.Threading.Shared
{
    public class ConcurrentListEdit
    {
        private const int N = 10;
        private readonly ConcurrentQueue<TestObject> _done = new();
        private readonly List<TestObject> _shared = new();
        private readonly ConcurrentQueue<TestObject> _waiting = new();

        private void Dispatch()
        {
            while (_done.TryDequeue(out var obj)) _waiting.Enqueue(obj.Clone());
        }


        [Test]
        public void ConcurrentListEditTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < N; i++) _shared.Add(new TestObject(i));
            for (var i = 0; i < N; i++) _done.Enqueue(_shared[i].Clone());
            Dispatch();
            Parallel.ForEach(_waiting, i =>
            {
                var value = i.Value;
                Thread.Sleep(100);
                i.Value = value + 1;
                _done.Enqueue(i);
            });
            var str = "";
            while (_done.TryDequeue(out var obj)) str += obj.Value + " ";
            Debug.Log(str);
            str = "";
            _shared.Select(i => i.Value).ToList().ForEach(i => str += i + " ");
            Debug.Log(str);
            Debug.Log($"Time {sw.ElapsedMilliseconds} ms");
            //w/o concurrent 
            sw.Restart();
            _shared.Clear();
            for (var i = 0; i < N; i++) _shared.Add(new TestObject(i));
            Parallel.ForEach(_shared, i =>
            {
                lock (i)
                {
                    var value = i.Value;
                    Thread.Sleep(100);
                    i.Value = value + 1;
                }
            });
            foreach (var i in _shared) Debug.Log(i.Value);
            Debug.Log($"Time {sw.ElapsedMilliseconds} ms");
        }
    }
}