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
        public Boolean Active { get; set; }
        public ShoppingCartSO Cart { get; }
        #endregion

        #region constructors
        public UserSO(string id, ShoppingCartSO cart)
        {
            this.Id = id;
            this.Cart = cart;
        }

        public abstract String[] toArray();

        #endregion
    }

    public class GuestUserSO : UserSO
    {
        #region constructos
        public GuestUserSO(string id, ShoppingCartSO cart) : base(id, cart) { }
        public GuestUserSO(GuestUser guest) : base(guest.Id, new ShoppingCartSO(guest.ShoppingCart)) { }

        public override string[] toArray()
        {
            String[] str = new string[2];
            str[0] = Id;
            str[1] = Cart.Id;
            return str;
        }


        #endregion
    }

    public class RegisteredUserSO : UserSO
    {
        #region parameters
        public string UserName { get; }
        #endregion

        #region constructors
        public RegisteredUserSO(RegisteredUser user) : base(user.Id, new ShoppingCartSO(user.ShoppingCart))
        {
            this.UserName = user.UserName;
            this.Active = user.Active;
        }

        public override string[] toArray()
        {
            String[] str = new string[3];
            str[0] = Id;
            str[1] = Cart.Id;
            str[2] = UserName;
            return str;
        }
        #endregion
    }

}
