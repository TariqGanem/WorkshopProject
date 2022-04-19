using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class GuestUser : User
    {
        public GuestUser(string id) : base(id) { }

        public void Logout() { Active = false; }
    }
}
