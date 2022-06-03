using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class GuestUser : User
    {
        public GuestUser() : base() { }

        public void Logout() { Active = false; }

        public DTO_GuestUser getDTO()
        {
            return new DTO_GuestUser(Id, ShoppingCart.getDTO(), Active);
        }

    }
}
