using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class RegisteredUser : User , SubscriberInterface
    {
        public string UserName { get; }
        private string _password;
        public History History { get; set; }
        public LinkedList<Notification> PendingNotification { get; }
        public NotificationsDistributer NotificationsDistributer { get; }

        public RegisteredUser(String userName, String password) : base()
        {
            UserName = userName;
            _password = password;
            this.History = new History();
            this.PendingNotification = new LinkedList<Notification>();
            this.NotificationsDistributer = NotificationsDistributer.GetInstance();
        }

        public void Login(String password)
        {
            if (!_password.Equals(password))
            {
                throw new Exception("Wrong password!");
            }
            DisplayPendingNotifications();
            Active = true;
        }

        public void Logout()
        {
            if (Active)
                Active = false;
            else throw new Exception("User already loged out!");
        }
        public bool Update(Notification notification)
        {
            if (Active)
            {
                NotificationsDistributer.notifyNotificationServer(notification);
                Logger.GetInstance().LogInfo("User is LoggedIn , therefor displaying the notification\n");
                return true;
            }
            PendingNotification.AddLast(notification);
            Logger.GetInstance().LogInfo("User not logged in , therefore the notification is added to pending list\n");

            return false;
        }

        private void DisplayPendingNotifications()
        {
            foreach (Notification notification in PendingNotification)
            {
                if (!notification.isOpened)
                {
                    NotificationsDistributer.notifyNotificationServer(notification);
                }
            }

            RemoveOpenedNotifications();
        }

        private void RemoveOpenedNotifications()
        {
            foreach (Notification notification in PendingNotification)
            {
                if (notification.isOpened)
                {
                    PendingNotification.Remove(notification);
                }
            }
        }

        public DTO_RegisteredUser getDTO()
        {
            LinkedList<DTO_Notification> notifications_dto = new LinkedList<DTO_Notification>();
            foreach (var n in PendingNotification)
            {
                notifications_dto.AddLast(n.getDTO());
            }
            return new DTO_RegisteredUser(Id, ShoppingCart.getDTO(), UserName, _password,
                                        Active, History.getDTO(), notifications_dto);

        }
    }
}
