using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User.Roles
{
    public class StoreOwner : IStaff , SubscriberInterface
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

        public bool Update(Notification notification)
        {
            return User.Update(notification);
        }
        public DTO_StoreOwner getDTO()
        {
            LinkedList<string> managers_dto = new LinkedList<string>();
            foreach (StoreManager sm in StoreManagers)
            {
                managers_dto.AddLast(sm.GetId());
            }

            LinkedList<string> owners_dto = new LinkedList<string>();
            foreach (StoreOwner so in StoreOwners)
            {
                owners_dto.AddLast(so.GetId());
            }
            return new DTO_StoreOwner(User.Id, StoreId, AppointedBy.GetId(), managers_dto, owners_dto);
        }

        public StoreOwnerService getSO()
        {
            LinkedList<String> storeOwners = new LinkedList<String>();
            LinkedList<String> storeManagers = new LinkedList<String>();

            foreach (StoreOwner so in StoreOwners)
            {
                storeOwners.AddLast(so.User.Id);
            }

            foreach (StoreManager sm in StoreManagers)
            {
                storeManagers.AddLast(sm.User.Id);
            }
            if (AppointedBy != null)
                return new StoreOwnerService(User.Id, StoreId, AppointedBy.GetId(), storeOwners, storeManagers);
            return new StoreOwnerService(User.Id, StoreId, null, storeOwners, storeManagers);
        }
    }
}
