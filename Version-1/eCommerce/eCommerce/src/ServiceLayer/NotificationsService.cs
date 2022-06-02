using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer
{
    public class NotificationsService
    {
        private static NotificationsService Instance { get; set; }

        private NotificationsService()
        {
            Instance = null;
        }

        public static NotificationsService GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NotificationsService();
            }
            return Instance;
        }

        //TODO
        public Result<bool> Update(Notification notification)
        {
            notification.isOpened = true;

            //throw new NotImplementedException();
            Logger.GetInstance().LogInfo("Notification is displayed to manager\n");
            return new Result<bool>(true);
        }
    }
}
