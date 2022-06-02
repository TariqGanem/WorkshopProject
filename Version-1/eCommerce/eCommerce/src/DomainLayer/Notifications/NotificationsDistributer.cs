using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Notifications
{
    public class NotificationsDistributer
    {
        private static NotificationsDistributer Instance { get; set; }
        public NotificationsService NotificationService { get; }

        private NotificationsDistributer()
        {
            Instance = null;
            this.NotificationService = NotificationsService.GetInstance();
        }

        public static NotificationsDistributer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NotificationsDistributer();
            }
            return Instance;
        }

        public bool notifyNotificationServer(Notification notification)
        {
            return NotificationService.Update(notification).Value;
        }
    }
}
