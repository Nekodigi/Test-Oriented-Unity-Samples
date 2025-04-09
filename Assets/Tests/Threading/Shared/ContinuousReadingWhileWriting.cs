using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Threading.Shared
{
    public class ContinuousReadingWhileWriting
    {
        private const int N = 10;
        private readonly ConcurrentQueue<TestObject> _queue = new();
        private readonly List<TestObject> _sharedList = new();

        [Test]
        public async Task ContinuousReadingWhileWritingTest()
        {
            //try to run reader and writer at same time.
            var readerTask = Reader();
            var writerTask = Writer();

            await Task.WhenAll(readerTask, writerTask);
        }

        private async Task Reader()
        {
            for (var i = 0; i < 10; i++)
            {
                var localValue = new TestObject();
                Debug.Log($"{i} th loop");
                var str = "";
                foreach (var value in _sharedList) str += $"{value.Value} " + " ";
                Debug.Log(str);
                await Task.Delay(100);
            }
        }

        private async Task Writer()
        {
            for (var i = 0; i < N; i++)
                _sharedList.Add(
                    new TestObject());
            for (var i = 0; i < N; i++)
                _queue.Enqueue(_sharedList[i]);

            for (var i = 0; i < N; i++)
            {
                var obj = new TestObject();
                //if (_queue.TryDequeue(out obj)) obj.Value = i;
                obj.Value = i; //新規作成してしまってもListの参照が消えることはない
                _sharedList[i] = obj;
                await Task.Delay(1);
            }
        }
    }
}