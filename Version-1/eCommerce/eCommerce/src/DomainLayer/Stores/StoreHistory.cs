using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.DomainLayer.User;

namespace eCommerce.src.DomainLayer.Store
{
    public class StoreHistory
    {
        public ConcurrentBag<ShoppingBag> history { get; }

        public StoreHistory()
        {
            history = new ConcurrentBag<ShoppingBag>();
        }

        public void addShoppingBasket(ShoppingBag toadd)
        {
            history.Add(toadd);
        }

    }
}
