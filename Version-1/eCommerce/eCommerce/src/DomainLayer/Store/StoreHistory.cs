using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;


namespace eCommerce.src.DomainLayer.Store
{
    public class StoreHistory
    {
        ConcurrentBag<ShoppingBag> history { get; }

        public StoreHistory()
        {
            history = new ConcurrentBag<ShoppingBag>();
        }

        public void addShoppingBag(ShoppingBag toadd)
        {
            history.Add(toadd);
        }
    }
}
