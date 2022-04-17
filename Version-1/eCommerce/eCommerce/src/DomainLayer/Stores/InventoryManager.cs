using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace eCommerce.src.DomainLayer.Store
{
    public class InventoryManager
    {
        public ConcurrentDictionary<String, Product> Products { get; }
        public ConcurrentDictionary<String, int> ProductsQuantity { get; }

        public InventoryManager()
        {
            Products = new ConcurrentDictionary<String, Product>();
        }

        public InventoryManager(ConcurrentDictionary<String, Product> products)
        {
            Products = products;
        }

        public Product AddNewProduct(String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            Product newProduct = new Product(productName, price, category, keywords);
            Products.TryAdd(newProduct.Id, newProduct);
            return newProduct;
        }

        public Product RemoveProduct(string productID)
        {
            if (Products.TryRemove(productID, out Product toRemove))
            {
                return toRemove;
            }
            //else
            throw new Exception($"Trying to remove non-existed product. (ID {productID})");
        }

        public Product EditProduct(string productID, IDictionary<String, object> details)
        {
            return null;
        }
    }
}
