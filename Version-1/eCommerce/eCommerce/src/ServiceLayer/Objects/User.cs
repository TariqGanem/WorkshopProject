using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal abstract class User
    {
        #region parameters
        public string id { get; }
        public shoppingCart cart { get; }
        #endregion

        #region constructors
        public User(string id, shoppingCart cart)
        {
            this.id = id;
            this.cart = cart;
        }
        #endregion
    }

    internal class guestUser : User
    {
        #region constructos
        public guestUser(string id, shoppingCart cart) : base(id, cart) { }
        public guestUser(GuestUser guest): base(guest.id,new shoppingCart(guest.ShoppingCart)) {}
        #endregion
    }

    internal class registeredUser : User
    {
        #region parameters
        public string username { get; }
        public string email { get; }
        #endregion

        #region constructors
        public registeredUser(string id, shoppingCart cart, string username, string email) : base(id, cart)
        {
            this.username = username;
            this.email = email;
        }
        #endregion
    }
}
