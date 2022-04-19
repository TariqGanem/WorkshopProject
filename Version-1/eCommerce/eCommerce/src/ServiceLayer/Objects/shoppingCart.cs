using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class shoppingCart
    {
        #region parameters
        public string id { get; }
        public Dictionary<string, shoppingBasket> baskets { get; }
        public double totalPrice { get; }
        #endregion

        #region constructors
        public shoppingCart(string id, Dictionary<string,shoppingBasket> baskets, double totalPrice)
        {
            this.id = id;
            this.baskets = baskets;
            this.totalPrice = totalPrice;
        }

        public shoppingCart(ShoppingCart shoppingCart)
        {
            this.id = shoppingCart.Id;
            baskets = new Dictionary<string, shoppingBasket>();
            foreach (string basket_key in shoppingCart.ShoppingBags.Keys)
            {
                baskets[basket_key] = new shoppingBasket(shoppingCart.ShoppingBags[basket_key]);
            }
            this.totalPrice = shoppingCart.TotalCartPrice;
        }
        #endregion
    }
}
