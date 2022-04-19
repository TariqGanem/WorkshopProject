using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class shoppingBasket
    {
        public string id { get; }
        public string userId { get; }
        public string storeId { get; }
        public List<product> products { get; }
        public double totalPrice { get; }

        public shoppingBasket(string id, string userId, string storeId, List<product> products, double totalPrice)
        {
            this.id = id;
            this.userId = userId;
            this.storeId = storeId;
            this.products = products;
            this.totalPrice = totalPrice;
        }
    }
}