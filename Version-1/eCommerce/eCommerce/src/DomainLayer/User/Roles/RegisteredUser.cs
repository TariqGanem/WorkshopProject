using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal class RegisteredUser : User
    {
        public string UserName { get; private set; }
        private string _password;

        public RegisteredUser(String userName, String password) : base()
        {
            UserName = userName;
            _password = password;
        }

        public void Login(String password)
        {
            if (!_password.Equals(password))
            {
                throw new Exception("Wrong password!");
            }
            Active = true;
        }

        public void Logout() { Active = false; }
    }
}
