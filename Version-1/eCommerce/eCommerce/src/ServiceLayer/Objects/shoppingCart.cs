using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class shoppingCart
    {
        #region parameters
        public string Id { get; }
        public Dictionary<string, shoppingBasket> Baskets { get; }
        public double TotalPrice { get; }
        #endregion

        #region constructors
        public shoppingCart(string id, Dictionary<string,shoppingBasket> baskets, double totalPrice)
        {
            this.Id = id;
            this.Baskets = baskets;
            this.TotalPrice = totalPrice;
        }

        public shoppingCart(ShoppingCart shoppingCart)
        {
            this.Id = shoppingCart.Id;
            Baskets = new Dictionary<string, shoppingBasket>();
            foreach (string basket_key in shoppingCart.ShoppingBags.Keys)
            {
                Baskets[basket_key] = new shoppingBasket(shoppingCart.ShoppingBags[basket_key]);
            }
            this.TotalPrice = shoppingCart.TotalCartPrice;
        }
        #endregion
    }
}
