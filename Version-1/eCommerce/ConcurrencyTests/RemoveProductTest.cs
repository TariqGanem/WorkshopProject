using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
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
    public class RemoveProductTest : XeCommerceTestCase
    {
        private string tariq_id;
        private string yazan_id;
        private string eran_id;
        private string random_id;
        private string store_id;
        private string product_id;
        private BlockingCollection<bool> results;

        public RemoveProductTest() : base()
        {
            api.Register("tariq@gmail.com", "test1");
            api.Register("yazan@gmail.com", "test12");
            api.Register("eran@gmail.com", "navtut");
            api.Register("random@gmail.com", "test");
            this.tariq_id = api.Login("tariq@gmail.com", "test1").Value;
            this.yazan_id = api.Login("yazan@gmail.com", "test12").Value;
            this.eran_id = api.Login("eran@gmail.com", "navtut").Value;
            this.random_id = api.Login("random@gmail.com", "test").Value;
            this.store_id = api.OpenNewStore("test_store", tariq_id).Value;
            api.AddStoreManager(yazan_id, tariq_id, store_id);
            api.AddStoreManager(eran_id, tariq_id, store_id);
            api.AddStoreManager(random_id, tariq_id, store_id);
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(0);
            permission.AddLast(1);
            api.SetPermissions(store_id, yazan_id, tariq_id, permission);
            api.SetPermissions(store_id, eran_id, tariq_id, permission);
            api.SetPermissions(store_id, random_id, tariq_id, permission);
            this.product_id = api.AddProductToStore(tariq_id, store_id, "sushi", 300, 1, "Asia").Value;
            results = new BlockingCollection<bool>();
        }

        internal void ThreadWork(string user_id)
        {
            Result result = api.RemoveProductFromStore(user_id, store_id, this.product_id);
            if (!result.ErrorOccured)
                results.TryAdd(true);
            else
                results.TryAdd(false);
        }

        // 4 threads trying to remove the same product at the same time, but only one should success!
        [Fact]
        [Trait("Category", "concurrency")]
        public void RemoveProduct()
        {
            Thread thread1 = new Thread(() => ThreadWork(tariq_id));
            Thread thread2 = new Thread(() => ThreadWork(yazan_id));
            Thread thread3 = new Thread(() => ThreadWork(eran_id));
            Thread thread4 = new Thread(() => ThreadWork(random_id));

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

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
