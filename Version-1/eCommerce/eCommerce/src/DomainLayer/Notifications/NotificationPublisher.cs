using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store
{
    public class NotificationPublisher : PublisherInterface
    {
        public Store Store { get; set; }
        public Logger logger { get; set; }

        public NotificationPublisher(Store store)
        {
            this.Store = store;
            logger = Logger.GetInstance();
        }

        public NotificationPublisher()
        {
        }

        public bool notifyStorePurchase(Product product, int quantity)
        {
            String msg = $"Event : Product Purchased     Store Id : {Store.Id}     Product Name : {product.Name}     Product Quantity : {quantity}";
            notify(msg, true);
            logger.LogInfo("All staff members are notified with product purchase\n");
            return true;
        }

        public bool notifyStoreClosed()
        {
            String msg = $"Event : Store Closed     Store Id : {Store.Id}";
            notify(msg, true);
            //logger.LogInfo($"All staff members are notified that store {Store.Id} is closed\n");
            return true;

        }

        public bool notifyStoreOpened()
        {
            String msg = $"Event : Store Opened     Store Id : {Store.Id}     ";
            notify(msg, true);
            //logger.LogInfo($"All staff members are notified that store {Store.Id} is opened\n");
            return true;
        }

        public bool notifyOwnerSubscriptionRemoved(string ownerID, StoreOwner removedOwner) // ?
        {
            String msg = $"Event : Owner Subscription Removed     Store Id : {Store.Id}     Owner Id : {ownerID}";
            Notification notification = new Notification(ownerID, msg, true);
            removedOwner.Update(notification);      
            notify(msg, true);
            //logger.LogInfo($"All staff members are notified that owner ({ownerID}) subscriptoin as store owner ({Store.Id}) has been removed\n");
            return true;
        }

        private void notify(String msg, Boolean isStaff)
        {
            ConcurrentDictionary<String, StoreOwner> Owners = Store.Owners;
            ConcurrentDictionary<String, StoreManager> Managers = Store.Managers;

            foreach (var owner in Owners)
            {
                Notification notification = new Notification(owner.Value.GetId(), msg, true);
                owner.Value.Update(notification);
            }

            foreach (var manager in Managers)
            {
                Notification notification = new Notification(manager.Value.GetId(), msg, true);
                manager.Value.Update(notification);
            }
        }

        public void notifyOfferRecievedUser(string userID, string storeID, string productID, int amount, double price, double counterOffer, bool v)
        {
            String msg = "";
            if (v)
                msg = $"Event : An offer has been accepted     Product Id : {productID}     Store Id : {storeID}     Amount : {amount}     Price : {price}";
            else if (counterOffer == -1)
                msg = $"Event : An offer has been declined     Product Id : {productID}     Store Id : {storeID}     Amount : {amount}     Price : {price}";
            else
                msg = $"Event : A counter offer was recieved     Product Id : {productID}     Store Id : {storeID}     Amount : {amount}     New price : {counterOffer}";

            notifyOwners(msg, false);
            //Logger.GetInstance().LogInfo($"All store owners are notified that an offer was recieved from the user : { userID}\n");
        }

        private void notifyOwners(string msg, bool v)
        {
            ConcurrentDictionary<String, StoreOwner> Owners = Store.Owners;

            foreach (var owner in Owners)
            {
                Notification notification = new Notification(owner.Value.GetId(), msg, true);
                owner.Value.Update(notification);
            }
        }
    }
}
