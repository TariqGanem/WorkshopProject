using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class RegisteredUser : User
    {
        public string UserName { get; }
        public string Email { get; }
        private string _password;

        public RegisteredUser(String userName, string email, String password) : base()
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
