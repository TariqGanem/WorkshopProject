using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace ConcurrencyTests
{
    public class AddStoreManagerTest
    {
        private eCommerceSystem api = new eCommerceSystem();
        private string tariq_id;
        private string yazan_id;
        private string eran_id;
        private string store_id;
        private BlockingCollection<bool> results;

        public AddStoreManagerTest()
        {
            api.Register("tariq@gmail.com", "test1");
            api.Register("yazan@gmail.com", "test12");
            api.Register("eran@gmail.com", "navtut");
            api.Register("ahmad@gmail.com", "test");
            api.Register("mohamad@gmail.com", "test123");
            this.tariq_id = api.Login("tariq@gmail.com", "test1").Value.Id;
            this.yazan_id = api.Login("yazan@gmail.com", "test12").Value.Id;
            this.eran_id = api.Login("eran@gmail.com", "navtut").Value.Id;
            this.store_id = api.OpenNewStore("test_store", tariq_id).Value.Id;
            api.AddStoreManager(yazan_id, tariq_id, store_id);
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(4);
            api.SetPermissions(store_id, yazan_id, tariq_id, permission);
            results = new BlockingCollection<bool>();
        }

        internal void ThreadWork(string manager_id)
        {
            Result result = api.AddStoreManager(eran_id, manager_id, store_id);
            if (!result.ErrorOccured)
                results.TryAdd(true);
            else
                results.TryAdd(false);
        }

        // 2 threads tying to add a new manager to the store, but only 1 should success!
        [Fact]
        [Trait("Category", "concurrency")]
        public void AddStoreManager()
        {
            Thread thread1 = new Thread(() => ThreadWork(tariq_id));
            Thread thread2 = new Thread(() => ThreadWork(yazan_id));

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
