using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public abstract class User
    {
        #region parameters
        public string Id { get; }
        public shoppingCart Cart { get; }
        #endregion

        #region constructors
        public User(string id, shoppingCart cart)
        {
            this.Id = id;
            this.Cart = cart;
        }
        #endregion
    }

    public class guestUser : User
    {
        #region constructos
        public guestUser(string id, shoppingCart cart) : base(id, cart) { }
        public guestUser(GuestUser guest): base(guest.Id,new shoppingCart(guest.ShoppingCart)) {}
        #endregion
    }

    public class registeredUser : User
    {
        #region parameters
        public string UserName { get; }
        //public string Email { get; }
        #endregion

        #region constructors
        public registeredUser(string id, shoppingCart cart, string username, string email) : base(id, cart)
        {
            this.UserName = username;
            //this.Email = email;
        }

        public registeredUser(RegisteredUser registeredUser): base(registeredUser.Id,new shoppingCart(registeredUser.ShoppingCart))
        {
            //this.Email = registeredUser.Email;
            this.UserName = registeredUser.UserName;
        }
        #endregion
    }

}
