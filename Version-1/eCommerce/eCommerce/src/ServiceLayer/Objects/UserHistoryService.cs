using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class UserHistoryService
    {
        public LinkedList<ShoppingBasketService> ShoppingBags { get; }

        public UserHistoryService(LinkedList<ShoppingBasketService> shoppingBags)
        {
            ShoppingBags = shoppingBags;
        }
    }
}
