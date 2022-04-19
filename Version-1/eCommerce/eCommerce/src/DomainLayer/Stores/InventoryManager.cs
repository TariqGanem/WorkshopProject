using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.src.DomainLayer.Store
{
    public interface IInventoryManager
    {
        Product AddNewProduct(String productName, Double price, int initialQuantity, String category, LinkedList<String> keyWords = null);
        Product RemoveProduct(String productID);
        Product EditProduct(String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(Double storeRating, IDictionary<String, Object> searchAttributes);
    }

    public class InventoryManager : IInventoryManager
    {
        public ConcurrentDictionary<String, Product> Products { get; }

        public InventoryManager()
        {
            Products = new ConcurrentDictionary<String, Product>();
        }

        public InventoryManager(ConcurrentDictionary<String, Product> products, ConcurrentDictionary<String, int> productsQuantity)
        {
            Products = products;
        }

        public Product AddNewProduct(String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            Product newProduct = new Product(productName, price, category, initialQuantity, keywords);
            Products.TryAdd(newProduct.Id, newProduct);
            return newProduct;
        }

        public Product RemoveProduct(string productID)
        {
            if (Products.TryRemove(productID, out Product toRemove))
            {
                return toRemove;
            }
            throw new Exception($"Trying to remove non-existed product. (ID: {productID})");
        }

        public Product EditProduct(string productID, IDictionary<String, object> details)
        {
            /*if (Products.TryGetValue(productID, out Product toEdit))
            {
                ObjectDictionaryMapper<Product>.SetPropertyValue(toEdit, details);
            }
            throw new Exception($"Faild to edit product (ID: {productID}) - Product not found");*/
            throw new NotImplementedException();
        }

        public List<Product> SearchProduct(Double storeRating, IDictionary<String, Object> searchAttributes)
        {
            List<Product> searchResults = new List<Product>();
            foreach (Product product in this.Products.Values)
            {
                if (CheckProduct(storeRating, product, searchAttributes))
                {
                    searchResults.Add(product);
                }
            }
            if (searchResults.Count > 0)
            {
                return searchResults;
            }
            else
            {
                throw new Exception($"No item has been found");
            }
        }

        internal bool CheckProduct(Double storeRating, Product product, IDictionary<String, Object> searchAttributes)
        {
            Boolean result = true;
            ICollection<String> properties = searchAttributes.Keys;
            foreach (string property in properties)
            {
                var value = searchAttributes[property];
                switch (property.ToLower())
                {
                    case "name":
                        if (!product.Name.ToLower().Contains(((string)value).ToLower())) { result = false; }
                        break;
                    case "category":
                        if (!product.Category.ToLower().Equals(((string)value).ToLower())) { result = false; }
                        break;
                    case "lowprice":
                        if (product.Price < (Double)value) { result = false; }
                        break;
                    case "highprice":
                        if (product.Price > (Double)value) { result = false; }
                        break;
                    case "productrating":
                        if (product.Rate < (Double)value) { result = false; }
                        break;
                    case "storerating":
                        if (storeRating < (Double)value) { result = false; }
                        break;
                    case "keywords":
                        bool found = false;
                        List<string> productKeywords = product.KeyWords.Select(word => word.ToLower()).ToList();
                        List<string> searchWords = (List<String>)value;
                        for (int i = 0; i < searchWords.Count && !found; i++)
                        {
                            if (productKeywords.Contains(searchWords[i].ToLower()))
                            {
                                //One keyword has been found
                                found = true;
                            }
                        }
                        //No keyword has been found
                        if (!found)
                        {
                            result = false;
                        }
                        break;
                }
            }
            return result;
        }

        public Product GetProduct(String productID)
        {
            if (Products.TryGetValue(productID, out Product product))
            {
                return product;
            }
            throw new Exception($"Product (ID: {productID}) not found");
        }
    }
}
