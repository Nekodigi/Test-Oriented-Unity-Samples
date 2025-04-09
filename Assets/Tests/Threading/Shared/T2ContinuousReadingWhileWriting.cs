using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Debug = UnityEngine.Debug;

namespace Tests.Threading.Shared
{
    public class T2ContinuousReadingWhileWriting
    {
        private const int N = 10;

        //use semaphore slim
        private readonly ReaderWriterLockSlim _rwLock = new();
        private readonly TestObject _testObject = new();

        [Test]
        public async Task ContinuousReadingWhileWritingTest()
        {
            //Execute Reader
            // var task = Task.Run(async () => await Reader());
            // task.Wait();
            var readerTask = Reader();
            var tasks = new Task[N];
            for (var i = 0; i < N; i++)
                tasks[i] = Task.Run(async () =>
                {
                    _rwLock.EnterWriteLock();
                    try
                    {
                        var value = _testObject.Value;
                        Thread.Sleep(100);
                        _testObject.Value = value + 1;
                    }
                    finally
                    {
                        _rwLock.ExitWriteLock();
                    }
                });

            Task.WaitAll(tasks);
            await readerTask;

            Debug.Log(_testObject.Value);
        }

        private async Task Reader()
        {
            Debug.Log("Reader is called");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < N; i++)
            {
                _rwLock.EnterReadLock();
                try
                {
                    Debug.Log($"Reader sees: {_testObject.Value}, when {sw.ElapsedMilliseconds} ms");
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }

                var sw2 = new Stopwatch();
                sw2.Start();
                await Task.Delay(1);
                Debug.Log($"Delay: {sw2.ElapsedMilliseconds} ms");
            }
        }
    }
}