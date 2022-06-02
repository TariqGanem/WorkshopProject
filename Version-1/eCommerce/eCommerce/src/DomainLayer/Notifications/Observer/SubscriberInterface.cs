using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    public interface SubscriberInterface
    {
            bool Update(Notification notification);
    }
}
