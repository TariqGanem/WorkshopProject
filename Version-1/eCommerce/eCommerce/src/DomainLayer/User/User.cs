using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal abstract class User
    {
        protected Boolean Active { get; set; }
        protected ShoppingCart ShoppingCart { get; set; }

        protected User()
        {
            Active = false;
            ShoppingCart = new ShoppingCart();
        }

    }
}
