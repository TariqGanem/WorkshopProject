using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal abstract class User
    {
        public String Id { get; }
        protected Boolean Active { get; set; }
        protected ShoppingCart ShoppingCart { get; set; }

        protected User()
        {
            //Id = Service.getID();
            Active = false;
            ShoppingCart = new ShoppingCart();
        }

    }
}
