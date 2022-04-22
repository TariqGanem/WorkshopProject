using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ConcurrencyTests
{
    public class ResgisterTest
    {
        private eCommerceSystem api = new eCommerceSystem();
        private BlockingCollection<bool> results;

        public ResgisterTest()
        {
            results = new BlockingCollection<bool>();
        }

        internal void ThreadWork()
        {
            Result result = api.Register("tariq@gmail.com", "password");
            if (!result.ErrorOccured)
                results.TryAdd(true);
            else
                results.TryAdd(false);
        }

        // 2 threads attempt to register with the same data at the same time, but only one should success!
        [Fact]
        [Trait("Category", "concurrency")]
        public void Register()
        {
            Thread thread1 = new Thread(() => ThreadWork());
            Thread thread2 = new Thread(() => ThreadWork());

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            int count = 0;
            foreach (bool result in results)
            {
                if (result)
                    count++;
            }
            Assert.True(count == 1, count + " succeded out of " + results.Count);
        }
    }
}
