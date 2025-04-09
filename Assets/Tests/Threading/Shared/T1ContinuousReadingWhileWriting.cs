using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Threading.Shared
{
    public class T1ContinuousReadingWhileWriting
    {
        private const int N = 100;

        //use semaphore slim
        private readonly SemaphoreSlim _semaphoreSlim = new(1);
        private readonly TestObject _testObject = new();

        [Test]
        public void ContinuousReadingWhileWritingTest()
        {
            //Execute Reader
            // var task = Task.Run(async () => await Reader());
            // task.Wait();
            Reader().ContinueWith(
                t =>
                    Debug.Log("Finished"));

            //readerTask.Wait();

            var tasks = new Task[N];
            for (var i = 0; i < N; i++)
                tasks[i] = Task.Run(async () =>
                {
                    await _semaphoreSlim.WaitAsync();
                    var value = _testObject.Value;
                    await Task.Delay(1);
                    _testObject.Value = value + 1;
                    _semaphoreSlim.Release();
                });

            Task.WaitAll(tasks);

            Debug.Log(_testObject.Value);
        }

        private async Task Reader()
        {
            Debug.Log("Reader is called");
            for (var i = 0; i < N; i++)
            {
                Debug.Log("By reader: " + _testObject.Value);
                await Task.Delay(1);
            }
        }
    }
}