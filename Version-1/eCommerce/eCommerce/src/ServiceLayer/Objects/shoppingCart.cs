using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class shoppingCart
    {
        public string id { get; }
        public List<shoppingBasket> baskets { get; }
        public double totalPrice { get; }

        public shoppingCart(string id, List<shoppingBasket> baskets, double totalPrice)
        {
            this.id = id;
            this.baskets = baskets;
            this.totalPrice = totalPrice;
        }
    }
}
