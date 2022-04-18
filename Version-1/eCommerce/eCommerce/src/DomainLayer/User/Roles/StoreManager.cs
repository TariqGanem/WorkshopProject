using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.Store;

namespace eCommerce.src.DomainLayer.User.Roles
{
    internal class StoreManager : IStaff
    {
        public RegisteredUser User { get; }
        public Permission Permission { get; }
        public IStaff AppointedBy { get; }
        public Store.Store Store { get; }

        public StoreManager(RegisteredUser user, Store.Store store, Permission permission, IStaff appointedBy)
        {
            User = user;
            Store = store;
            Permission = permission;
            AppointedBy = appointedBy;
        }

/*        public void SetPermission(int method, Boolean active)
        {
            return Permission.SetPermission(method, active);
        }

        public void SetPermission(Methods method, Boolean active)
        {
            return Permission.SetPermission(method, active);
        }*/

        public String GetId()
        {
            return User.Id;
        }
    }
}
