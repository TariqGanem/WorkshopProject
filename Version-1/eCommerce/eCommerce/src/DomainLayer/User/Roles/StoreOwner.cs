using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User.Roles
{
    public class StoreOwner : IStaff
    {
        public RegisteredUser User { get; }
        public String StoreId { get; }
        public IStaff AppointedBy { get; }
        public LinkedList<StoreManager> StoreManagers { get; }
        public LinkedList<StoreOwner> StoreOwners { get; }

        public StoreOwner(RegisteredUser user, String storeId, IStaff appointedBy)
        {
            User = user;
            StoreId = storeId;
            AppointedBy = appointedBy;
            StoreManagers = new LinkedList<StoreManager>();
            StoreOwners = new LinkedList<StoreOwner>();
        }
        public string GetId()
        {
            return User.Id;
        }
    }
}
