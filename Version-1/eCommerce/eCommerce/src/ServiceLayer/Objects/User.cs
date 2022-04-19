using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal abstract class User
    {
        public string id { get; }
        public shoppingCart cart{ get; }

        public User(string id,shoppingCart cart)
        {
            this.id = id;
            this.cart = cart;
        }
    }

    internal class guestUser : User
    {
        public guestUser(string id, shoppingCart cart):base(id,cart) {}
    }

    internal class registeredUser : User
    {
        public string username { get; }
        public registeredUser(string id, shoppingCart cart, string username):base(id,cart) {
            this.username = username;
        }
    }
}
