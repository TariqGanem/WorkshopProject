using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    public interface PublisherInterface
    {
        bool notifyStorePurchase(Product product, int quantity);
        bool notifyStoreClosed();
        bool notifyStoreOpened();
        bool notifyOwnerSubscriptionRemoved(String ownerID, StoreOwner removedOwner);
    }
}
