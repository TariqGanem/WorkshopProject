using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class History
    {
        public LinkedList<ShoppingBag> ShoppingBags { get; }

        public History()
        {
            ShoppingBags = new LinkedList<ShoppingBag>();
        }

        public void AddPurchasedShoppingCart(ShoppingCart shoppingCart)
        {
            ConcurrentDictionary<String, ShoppingBag> bags = shoppingCart.ShoppingBags;
            foreach (ShoppingBag bag in bags.Values)
            {
                ShoppingBags.AddLast(bag);
            }
        }

        public void AddPurchasedShoppingBag(ShoppingBag shoppingBag)
        {
            ShoppingBags.AddLast(shoppingBag);
        }
    }
}
