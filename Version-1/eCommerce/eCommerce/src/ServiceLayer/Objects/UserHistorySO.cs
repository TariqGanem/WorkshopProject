using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class UserHistorySO
    {
        //Properties
        public LinkedList<ShoppingBagSO> ShoppingBags { get; }

        //Constructor
        public UserHistorySO(History history)
        {
            ShoppingBags = new LinkedList<ShoppingBagSO>();
            LinkedList<ShoppingBag> shoppingBags = history.ShoppingBags;
            foreach (ShoppingBag bag in shoppingBags)
            {
                ShoppingBags.AddLast(new ShoppingBagSO(bag));
            }
        }
    }
}
