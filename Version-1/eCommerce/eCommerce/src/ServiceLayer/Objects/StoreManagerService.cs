using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class StoreManagerService : IStaffService
    {
        //Properties
        public String UserId { get; }
        public PermissionService Permissions { get; }
        public String OwnerId { get; }

        //Constructor
        public StoreManagerService(String userID, PermissionService permissions, String ownerID) : base(userID)
        {
            UserId = userID;
            Permissions = permissions;
            OwnerId = ownerID;
        }

        public Boolean[] getPermissions()
        {
            return Permissions.toArray();
        }

    }
}
