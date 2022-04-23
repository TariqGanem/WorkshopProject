using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class StoreOwnerService : IStaffService
    {
        //Properties
        public String UserId { get; }
        public String StoreId { get; }
        public String OwnerId { get; }
        public LinkedList<String> StoreOwners { get; }
        public LinkedList<String> StoreManager { get; }

        //Constructor
        public StoreOwnerService(String userID, String storeID, String ownerID, LinkedList<String> storeOwners, LinkedList<String> storeManager) : base(userID)
        {
            UserId = userID;
            StoreId = storeID;
            OwnerId = ownerID;
            StoreOwners = storeOwners;
            StoreManager = storeManager;
        }

    }


}
