using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class UserService
    {
        public String Id { get; }
        public ShoppingCartService ShoppingCart { get; set; }

        public UserService(String id, ShoppingCartService shoppingCart)
        {
            Id = id;
            ShoppingCart = shoppingCart;
        }
    }

}
