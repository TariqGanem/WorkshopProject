using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class ShoppingCart
    {
        public string Id { get; private set; }
        public ConcurrentDictionary<String, ShoppingBag> ShoppingBags { get; set; }  // <StoreId, ShoppingBag>
        public Double TotalCartPrice { get; set; }

        public ShoppingCart()
        {
            //Id = Service.GetId();
            ShoppingBags = new ConcurrentDictionary<string, ShoppingBag>();
            TotalCartPrice = 0;
        }

        public ShoppingCart(ShoppingCart shoppingCart)
        {
            Id = shoppingCart.Id;
            ShoppingBags = shoppingCart.ShoppingBags;
            TotalCartPrice = shoppingCart.TotalCartPrice;
        }

        public ShoppingBag GetShoppingBag(string storeId)
        {
            if (ShoppingBags.TryGetValue(storeId, out ShoppingBag shoppingBag))
                return shoppingBag;
            throw new Exception($"Shopping bag not found for the store id: {storeId}.");
        }

        public Boolean AddShoppingBagToCart(ShoppingBag shoppingBag)
        {
            //return ShoppingBags.TryAdd(shoppingBag.Store.Id, shoppingBag);
            throw new NotImplementedException();
        }

        public Double GetTotalShoppingCartPrice()
        {
            Double sum = 0;
            foreach (ShoppingBag bag in ShoppingBags.Values)
            {
                sum = sum + bag.GetTotalPrice();
            }
            TotalCartPrice = sum;
            return sum;
        }
    }
}
