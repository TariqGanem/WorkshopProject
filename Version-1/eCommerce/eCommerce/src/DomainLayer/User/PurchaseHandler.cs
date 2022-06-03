using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ExternalSystems;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public static class PurchaseHandler
    {

        public static ShoppingCart Purchase(ShoppingCart shoppingCart, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails)
        {
            if (shoppingCart.ShoppingBags.IsEmpty)
            {
                throw new Exception("The shopping cart is empty!");
            }

            if (!isValidCartQuantity(ref shoppingCart))
            {
                throw new Exception("Notice - The store is out of stock!");
            }

            if (!shoppingCart.ValidPolicy())
                throw new Exception("Some shopping bag doesn't match the store policy!");

            Double amount = shoppingCart.GetTotalShoppingCartPrice();

            bool paymentSuccess = Proxy.Pay(amount, paymentDetails);

            if (!paymentSuccess)
            {
                throw new Exception("Atempt to purchase the shopping cart faild due to error in payment details!");
            }

            bool deliverySuccess = Proxy.Deliver(deliveryDetails);
            if (!deliverySuccess)
            {
                Proxy.CancelTransaction(paymentDetails);
                throw new Exception("Atempt to purchase the shopping cart faild due to error in delivery details!");
            }
            ShoppingCart copy = new ShoppingCart(shoppingCart);
            return copy;
        }



        private static Boolean isValidCartQuantity(ref ShoppingCart shoppingCart)
        {
            ConcurrentDictionary<String, ShoppingBag> ShoppingBags = shoppingCart.ShoppingBags;

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
