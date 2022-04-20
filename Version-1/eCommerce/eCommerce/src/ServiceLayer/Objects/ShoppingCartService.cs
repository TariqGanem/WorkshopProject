using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ShoppingCartService
    {
        public String Id { get; }
        public LinkedList<ShoppingBasketService> ShoppingBags { get; }
        public Double TotalCartPrice { get; }


        //Constructor
        public ShoppingCartService(string shoppingCartId, LinkedList<ShoppingBasketService> shoppingBaskets, Double totalCartPrice)
        {
            Id = shoppingCartId;
            ShoppingBags = shoppingBaskets;
            TotalCartPrice = totalCartPrice;
        }
    }
}
