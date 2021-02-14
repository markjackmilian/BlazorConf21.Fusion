using System;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Fusion.Sample1
{
    public class CounterService
    {
        private readonly object _lock = new object();
        private int _count;
        private DateTime _changeTime = DateTime.Now;

        [ComputeMethod]
        public virtual Task<(int, DateTime)> GetCounterAsync()
        {
            lock (this._lock) {
                Console.WriteLine("Get Called");
                return Task.FromResult((this._count, this._changeTime));
            }
        }

        public Task IncrementCounterAsync()
        {
            lock (this._lock) {
                ++this._count;
                this._changeTime = DateTime.Now;
            }
            using (Computed.Invalidate()) this.GetCounterAsync();
            return Task.CompletedTask;
        }
    }
}