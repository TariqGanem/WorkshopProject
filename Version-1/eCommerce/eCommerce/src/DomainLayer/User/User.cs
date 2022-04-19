using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace eCommerce.src.DomainLayer.User
{
    public abstract class User
    {
        public String Id { get; }
        protected Boolean Active { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        protected User(string id)
        {
            this.Id = id;
            Active = false;
            ShoppingCart = new ShoppingCart();
        }

        public void AddProductToCart(Product product, int productQuantity, Store.Store store)
        {
            try
            {
                Monitor.TryEnter(product.Id);
                try
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
                        throw new Exception();
                    }
                }
                finally
                {
                    Monitor.Exit(product.Id);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
            }
        }
    }
}
