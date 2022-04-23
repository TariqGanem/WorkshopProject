using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ExternalSystems;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace eCommerce.src.DomainLayer.User
{
    public abstract class User
    {
        public String Id { get; }
        public Boolean Active { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        protected User()
        {
            this.Id = Service.GenerateId();
            Active = false;
            ShoppingCart = new ShoppingCart();
        }

        public void AddProductToCart(Product product, int productQuantity, Store.Store store)
        {
            ShoppingBag sb;
            try
            {
                ShoppingBag getSB = ShoppingCart.GetShoppingBag(store.Id);
                getSB.AddProtuctToShoppingBag(product, productQuantity);
            }
            catch (Exception)
            {
                //else create shopping bag for storeID
                sb = new ShoppingBag(this.Id, store);
                sb.AddProtuctToShoppingBag(product, productQuantity);
                ShoppingCart.AddShoppingBagToCart(sb);
            }
        }


        public void UpdateShoppingCart(String storeID, Product product, int quantity)
        {
            ShoppingBag bag = ShoppingCart.GetShoppingBag(storeID);
            bag.UpdateShoppingBag(product, quantity);
        }
    }
}
