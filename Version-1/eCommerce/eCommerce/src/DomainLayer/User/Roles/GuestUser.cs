using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class GuestUser : User
    {
        public GuestUser() : base() { }

        public void Logout() { Active = false; }

        public DTO_GuestUser getDTO()
        {
            return new DTO_GuestUser(Id, ShoppingCart.getDTO(), Active);
        }

        public override UserSO getSO()
        {
            UserSO user = new GuestUserSO(this);
            return user;
        }

        public override bool DeclineOffer(string offerID)
        {
            RemovePendingOffer(offerID);
            return true;
        }

        public override bool CounterOffer(string offerID)
        {
            return true;
        }

        public override bool AcceptOffer(string offerID)
        {
            return MovePendingOfferToAccepted(offerID);
        }

    }
}
