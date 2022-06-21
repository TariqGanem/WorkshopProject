using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets
{
    public class DiscountTargetCategories : IDiscountTarget
    {
        public List<string> Categories { get; }
        public string Id { get; set; }
        public DiscountTargetCategories(List<string> categories)
        {
            Id = Service.GenerateId();
            Categories = categories;
        }

        // for loading from db
        public DiscountTargetCategories(List<string> categories, string id)
        {
            Id = id;
            Categories = categories;
        }

        public static IDiscountTarget create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create DiscountTargetCategories: ";
            if (!info.ContainsKey("Categories"))
                throw new Exception(errorMsg + "Categories not found");
            List<string> categories = createCategoriesList((string)info["Categories"]);

            return new DiscountTargetCategories(categories);
        }

        private static List<string> createCategoriesList(string categoriesElement)
        {
            string[] res = categoriesElement.Split(',');
            List<string> categories = new List<string>();
            foreach (string str in res)
                categories.Add(str);
            return categories;
        }

        public List<Product> getTargets(ConcurrentDictionary<Product, int> products)
        {
            List<Product> result = new List<Product>();
            foreach (KeyValuePair<Product, int> entry in products)
            {
                if (Categories.Contains(entry.Key.Category))
                    result.Add(entry.Key);
            }
            return result;
        }

        public String GetData()
        {
            String answer = "";
            foreach (String category in Categories)
                answer += category + ", ";
            if (Categories.Count > 0)
                answer = answer.Substring(0, answer.Length - 2);
            return answer;
        }

        public string getId()
        {
            return this.Id;
        }
    }
}
