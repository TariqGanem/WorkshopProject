using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public abstract class UserSO
    {
        #region parameters
        public string Id { get; }
        public ShoppingCartSO Cart { get; }
        #endregion

        #region constructors
        public UserSO(string id, ShoppingCartSO cart)
        {
            this.Id = id;
            this.Cart = cart;
        }
        #endregion
    }

    public class GuestUserSO : UserSO
    {
        #region constructos
        public GuestUserSO(string id, ShoppingCartSO cart) : base(id, cart) { }
        public GuestUserSO(GuestUser guest): base(guest.Id,new ShoppingCartSO(guest.ShoppingCart)) {}
        #endregion
    }

    public class RegisteredUserSO : UserSO
    {
        #region parameters
        public string UserName { get; }
        //public string Email { get; }
        #endregion

        #region constructors
        public RegisteredUserSO(string id, ShoppingCartSO cart, string username) : base(id, cart)
        {
            this.UserName = username;
        }

        public RegisteredUserSO(RegisteredUser registeredUser): base(registeredUser.Id,new ShoppingCartSO(registeredUser.ShoppingCart))
        {
            this.UserName = registeredUser.UserName;
        }
        #endregion
    }

}
