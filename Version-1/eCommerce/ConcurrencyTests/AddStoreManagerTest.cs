using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace ConcurrencyTests
{
    public class AddStoreManagerTest : XeCommerceTestCase
    {
        private string tariq_id;
        private string yazan_id;
        private string eran_id;
        private string random_id;
        private string store_id;
        private BlockingCollection<bool> results;

        public AddStoreManagerTest() : base()
        {
            api.Register("tariq@gmail.com", "test1");
            api.Register("yazan@gmail.com", "test12");
            api.Register("eran@gmail.com", "navtut");
            api.Register("random@gmail.com", "randomPass");
            this.tariq_id = api.Login("tariq@gmail.com", "test1").Value;
            this.yazan_id = api.Login("yazan@gmail.com", "test12").Value;
            this.eran_id = api.Login("eran@gmail.com", "navtut").Value;
            this.random_id = api.Login("random@gmail.com", "randomPass").Value;
            this.store_id = api.OpenNewStore("test_store", tariq_id).Value;
            api.AddStoreManager(yazan_id, tariq_id, store_id);
            api.AddStoreManager(eran_id, tariq_id, store_id);
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(4);
            api.SetPermissions(store_id, yazan_id, tariq_id, permission);
            api.SetPermissions(store_id, eran_id, tariq_id, permission);
            results = new BlockingCollection<bool>();
        }

        internal void ThreadWork(string manager_id)
        {
            Result result = api.AddStoreManager(random_id, manager_id, store_id);
            if (!result.ErrorOccured)
                results.TryAdd(true);
            else
                results.TryAdd(false);
        }

        // 3 threads tying to add a new manager to the store, but only 1 should success!
        [Fact]
        [Trait("Category", "concurrency")]
        public void AddStoreManager()
        {
            Thread thread1 = new Thread(() => ThreadWork(tariq_id));
            Thread thread2 = new Thread(() => ThreadWork(yazan_id));
            Thread thread3 = new Thread(() => ThreadWork(eran_id));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

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
