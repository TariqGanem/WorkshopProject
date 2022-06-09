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
        /*
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

            Double amount = shoppingCart.GetTotalShoppingCartPrice();

            String paymentSuccess = Proxy.Pay(amount, paymentDetails);

            if (paymentSuccess.Equals("-1"))
            {
                throw new Exception("Atempt to purchase the shopping cart faild due to error in payment details!");
            }

            String deliverySuccess = Proxy.Deliver(deliveryDetails);
            if (deliverySuccess.Equals("-1"))
            {
                String cancelres = Proxy.CancelTransaction(paymentDetails);
                if (cancelres.Equals("-1"))
                    throw new Exception("Error in Delivery system yet the transaction could not be canceled!");
                else
                    throw new Exception("Error while Delivery , transaction was canceled");
            }
            ShoppingCart copy = new ShoppingCart(shoppingCart);
            return copy;
        }
        */


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
