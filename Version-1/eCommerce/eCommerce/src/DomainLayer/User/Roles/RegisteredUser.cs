using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class RegisteredUser : User , SubscriberInterface
    {
        public string UserName { get; set; }
        public string _password { get; set; }
        public History History { get; set; }
        public LinkedList<Notification> PendingNotification { get; set; }
        public NotificationsDistributer NotificationsDistributer { get; set; }


        public RegisteredUser(String userName, String password) : base()
        {
            UserName = userName;
            _password = password;
            this.History = new History();
            this.PendingNotification = new LinkedList<Notification>();
            this.NotificationsDistributer = NotificationsDistributer.GetInstance();
        }

        public RegisteredUser(string id, string userName, string password, bool active, History history, LinkedList<Notification> notifications) 
        {
            this.Id = id;
            UserName = userName;
            Active = active;
            History = history;
            this._password = password;
            this.PendingNotification = notifications;
        }

        public void Login(String password)
        {
            //if (Active)
            //    throw new Exception("User is Logged in in an another page");
            if (!_password.Equals(password))
            {
                throw new Exception("Wrong password!");
            }
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
            PendingNotification.AddLast(notification);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_notification = Builders<BsonDocument>.Update.Set("PendingNotification", getPendingNotificationsDTO());
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_notification);
            return false;
        }

        public LinkedList<DTO_Notification> getPendingNotificationsDTO()
        {
            LinkedList<DTO_Notification> notifications_dto = new LinkedList<DTO_Notification>();
            foreach (var n in PendingNotification)
            {
                notifications_dto.AddLast(n.getDTO());
            }
            return notifications_dto;
        }

        public LinkedList<Notification> getNotifications()
        {
            LinkedList<Notification> notifications = new LinkedList<Notification>();
            foreach (Notification noti in this.PendingNotification)
            {
                notifications.AddLast(noti);
            }
            this.PendingNotification.Clear();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_notification = Builders<BsonDocument>.Update.Set("PendingNotification", getPendingNotificationsDTO());
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_notification);
            return notifications;
        }

        public DTO_RegisteredUser getDTO()
        {
            LinkedList<DTO_Notification> notifications_dto = new LinkedList<DTO_Notification>();
            foreach (var n in PendingNotification)
            {
                notifications_dto.AddLast(n.getDTO());
            }
            return new DTO_RegisteredUser(Id, ShoppingCart.getDTO(), this.UserName, _password,
                                         Active, History.getDTO(), notifications_dto, Get_DTO_Offers(PendingOffers), Get_DTO_Offers(AcceptedOffers));
        }

        public override UserSO getSO()
        {
            UserSO user = new RegisteredUserSO(this);
            return user;
        }

        public ShoppingCart Purchase(IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails, MongoDB.Driver.IClientSessionHandle session = null)
        {
            if (ShoppingCart.ShoppingBags.IsEmpty)
            {
                throw new Exception("The shopping cart is empty\n");
            }

            ShoppingCart result = ShoppingCart.Purchase(paymentDetails, deliveryDetails, AcceptedOffers, this , session);
            if (result != null)
            {
                History.AddPurchasedShoppingCart(ShoppingCart, session);
                this.ShoppingCart = new ShoppingCart();          // create new shopping cart for user

                bool removeAccatedOffersResult = removeAcceptedOffers(session);
            }
            return result;
        }


        public override bool AcceptOffer(string offerID)
        {
            Offer offer = findPendingOffer(offerID);
            return MovePendingOfferToAccepted(offerID);
        }

        public override bool DeclineOffer(string offerID)
        {
            Offer offer = findPendingOffer(offerID);
            RemovePendingOffer(offerID);
            return true;
        }

        public override bool CounterOffer(string offerID)
        {
            Offer offer = findPendingOffer(offerID);
            return true;
        }
    }
}
