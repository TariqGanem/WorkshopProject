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
        Result<Boolean> notifyStorePurchase(Product product, int quantity);
        Result<Boolean> notifyStoreClosed();
        Result<Boolean> notifyStoreOpened();
        Result<Boolean> notifyOwnerSubscriptionRemoved(String ownerID, StoreOwner removedOwner);
    }
}
