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
        public UserHistorySO(LinkedList<ShoppingBagSO> shoppingBags)
        {
            ShoppingBags = shoppingBags;
        }
    }
}
