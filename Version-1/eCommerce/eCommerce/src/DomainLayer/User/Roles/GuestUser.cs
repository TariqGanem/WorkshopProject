using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal class GuestUser : User
    {
        public GuestUser() : base() { }

        public void Login() { Active = true; }

        public void Logout() { Active = false; }
    }
}
