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
            if (!bag.Products.ContainsKey(product))
                ShoppingCart.ShoppingBags.TryRemove(storeID, out _);
        }

        public ShoppingCart Purchase(IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails)
        {
            if (ShoppingCart.ShoppingBags.IsEmpty)
            {
                throw new Exception("The shopping cart is empty!");
            }

            if (!isValidCartQuantity())
            {
                throw new Exception("Notice - The store is out of stock!");
            }

            Double amount = ShoppingCart.GetTotalShoppingCartPrice();

            bool paymentSuccess = Payments.Pay(amount, paymentDetails);

            if (!paymentSuccess)
            {
                throw new Exception("Atempt to purchase the shopping cart faild due to error in payment details!");
            }

            bool deliverySuccess = Logistics.Deliver(deliveryDetails);
            if (!deliverySuccess)
            {
                Payments.CancelTransaction(paymentDetails);
                throw new Exception("Atempt to purchase the shopping cart faild due to error in delivery details!");
            }
            ShoppingCart copy = new ShoppingCart(ShoppingCart);
            ShoppingCart = new ShoppingCart();              // create new shopping cart for user
            return copy;
        }



        private Boolean isValidCartQuantity()
        {
            ConcurrentDictionary<String, ShoppingBag> ShoppingBags = ShoppingCart.ShoppingBags;

            foreach (var bag in ShoppingBags)
            {
                ConcurrentDictionary<Product, int> Products = bag.Value.Products;

                foreach (var product in Products)
                {
                    if (product.Key.Quantity < product.Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
