using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.src.DomainLayer.Store;
using Xunit;

namespace eCommerce.Tests.DomainLayerTests.StoresTests
{
    public  class InventoryManagerTests
    {
        public InventoryManager InventoryManager { get; }
        public Product productTester { get; set; }
        
        public InventoryManagerTests()
        {
            String[] kws = { "Chocolate", "Sugar" };
            this.InventoryManager = new InventoryManager();
            this.productTester = new Product("Nutella", 25.99, "Food", 5, new LinkedList<string> (kws));
            productTester.AddRating(3); // To test : SearchByProductRating
            this.InventoryManager.Products.TryAdd(productTester.Id,productTester);
        }

        [Theory()]
        [Trait("Category","UnitTesting")]
        [InlineData("Nutella" , true)]
        [InlineData("NotNutella" , false)]
        [InlineData("RandomName",false)]
        public void SearchProductByNameTest(String Name , Boolean Expected)
        {
            double notImportantStoreRating = 1.1;
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ "Name", Name }};
            if(Name.Equals("Nutella"))
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData(30, true)]
        [InlineData(25.5, false)]
        [InlineData(30, false)]
        public void SearchProductByPriceTest(double price , Boolean Expected)
        {
            double notImportantStoreRating = 1.1;
            IDictionary<String, Object> searchAttributeslow = new Dictionary<String, Object>()
                                                            {{ "lowprice", price }};
            if(price < 25.99)
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributeslow));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributeslow));
            IDictionary<String, Object> searchAttributeshigh = new Dictionary<String, Object>()
                                                            {{ "highprice", price }};
            if (price > 25.99)
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributeshigh));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributeshigh));
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData("Food", true)]
        [InlineData("NotFood", false)]
        [InlineData("Random", false)]

        public void SearchProductByCategoryTest(String Category , Boolean Expected)
        {
            double notImportantStoreRating = 1.1;
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ "category", Category }};
            if (Category.Equals("Food"))
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData(3.5, true)]
        [InlineData(3, false)]
        [InlineData(2.99, false)]
        public void SearchProductByProductRatingTest(double ProductRating , Boolean Expected)
        {
            double notImportantStoreRating = 1.1;
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ "productrating", ProductRating }};
            if (ProductRating <= 3)
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
        }


        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData(3.5, 3 , true)]
        [InlineData(2.99, 2.99, true)]
        [InlineData(3, 3.5 , false)]
        public void SearchProductByStoreRating(double StoreRatingActaul , double StoreRatingTest, Boolean Expected)
        {
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ "storerating", StoreRatingTest }};
            if (StoreRatingActaul >= StoreRatingTest)
                Assert.NotEmpty(InventoryManager.SearchProduct(StoreRatingActaul, searchAttributes));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(StoreRatingActaul, searchAttributes));
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData("Chocolate", true)]
        [InlineData("Sugar", true)]
        [InlineData("nutella", false)]
        [InlineData("food", false)]
        [InlineData("Random", false)]
        public void SearchProductByKeyword(String Keyword, Boolean Expected)
        {
            String[] kws = { Keyword };
            double notImportantStoreRating = 1.1;
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ "keywords", new List<String>(kws) }};
            if (Keyword.Equals("Chocolate") | Keyword.Equals("Sugar"))
                Assert.NotEmpty(InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
            else
                Assert.Throws<Exception>(() => InventoryManager.SearchProduct(notImportantStoreRating, searchAttributes));
        }

    }
}
