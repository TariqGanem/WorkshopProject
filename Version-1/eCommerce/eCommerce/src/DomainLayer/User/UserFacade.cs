using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal interface IUserFacade
    {
        RegisteredUser Login(String userName, String password);
        void Logout(String userId);
    }

    internal class UserFacade : IUserFacade
    {
        public ConcurrentDictionary<String, SystemAdmin> SystemAdmins { get; }
        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers { get; }
        public ConcurrentDictionary<String, GuestUser> GuestUsers { get; }

        private readonly object my_lock = new object();

        public UserFacade()
        {
            SystemAdmins = new ConcurrentDictionary<string, SystemAdmin>();
            RegisteredUsers = new ConcurrentDictionary<string, RegisteredUser>();
            GuestUsers = new ConcurrentDictionary<string, GuestUser>();
        }
        #region Public Methods
        public RegisteredUser Login(String userName, String password)
        {
            RegisteredUser registeredUser = GetUserByUserName(userName);
            if (registeredUser == null)
            {
                throw new Exception($"The username {userName} doesn't exist!");
            }
            registeredUser.Login(password);
            return registeredUser;
        }

        public void Logout(String userId)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guestUser))
            {
                guestUser.Logout();
                GuestUsers.Remove(userId, out GuestUser dump);
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registeredUser))
            {
                registeredUser.Logout();
            }
            else
            {
                throw new Exception("Cannot logout, user doesn't exist!");
            }
        }
        #endregion

        #region Private Methods
        private RegisteredUser GetUserByUserName(String userName)
        {
            foreach (RegisteredUser registeredUser in RegisteredUsers.Values)
            {
                if (registeredUser.UserName.Equals(userName))
                {
                    return registeredUser;
                }
            }
            throw new Exception($"Username {userName} doesn't exist!");
        }
        #endregion
    }
}
