using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.DomainLayer.User;

namespace eCommerce.src.DomainLayer.Store
{
    public class StoreHistory
    {
        ConcurrentBag<ShoppingBasket> history { get; }

        public StoreHistory()
        {
            history = new ConcurrentBag<ShoppingBasket>();
        }

        public void addShoppingBasket(ShoppingBasket toadd)
        {
            history.Add(toadd);
        }

    }
}
