using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;

namespace eCommerce.src.DomainLayer.User.Roles
{
    public class StoreManager : IStaff , SubscriberInterface
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

        public void SetPermission(int method, Boolean active)
        {
            Permission.SetPermission(method, active);
        }

        public void SetPermission(Methods method, Boolean active)
        {
            Permission.SetPermission(method, active);
        }

        public String GetId()
        {
            return User.Id;
        }
        public bool Update(Notification notification)
        {
            return User.Update(notification);
        }
        public DTO_StoreManager getDTO()
        {
            return new DTO_StoreManager(User.Id, Permission.functionsBitMask, AppointedBy.GetId(), Store.Id);
        }
    }
}
