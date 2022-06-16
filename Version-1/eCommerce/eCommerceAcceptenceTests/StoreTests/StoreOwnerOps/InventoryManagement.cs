using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.4.1
namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class InventoryManagement : XeCommerceTestCase // (Adding , Removing , Detail Editing) from products
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public String user_id { get; set; }
        public InventoryManagement() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;
            Result<bool> reguserId2 = this.api.Register("Buyer@gmail.com", "BuyerPassword");
            Result<String> userId2 = this.api.Login("Buyer@gmail.com", "BuyerPassword");
            this.user_id = userId2.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyAddProductToStore()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            String product_id = productId.Value;
            Result<bool> producttocart = api.AddProductToCart(this.user_id, product_id, 5, store_id);
            Assert.True(!producttocart.ErrorOccured);    
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadAddProductToStore_addProductByIneligibleUser()
        {
            Result<String> productId = this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.True(productId.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyRemoveProduct()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            String product_id = productId.Value;
            Result<bool> producttocart = api.AddProductToCart(this.user_id, product_id, 5, store_id);
            Assert.True(!producttocart.ErrorOccured);
            Result<bool> remProduct = this.api.RemoveProductFromStore(store_owner, store_id, product_id);
            Assert.True(!remProduct.ErrorOccured);
            producttocart = api.AddProductToCart(this.user_id, product_id, 1, store_id);
            Assert.True(producttocart.ErrorOccured);

        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadRemoveProduct_InvalidProductId()
        {
            Result<bool> remProduct = this.api.RemoveProductFromStore(store_owner, store_id, "product_id");
            Assert.True(remProduct.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadRemoveProduct_UneligableUser()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            Result<bool> remProduct = this.api.RemoveProductFromStore("random_user_id", store_id, productId.Value);
            Assert.True(remProduct.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyEditProductName()
        {

            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            IDictionary<String, Object> dictonary = new Dictionary<String, Object>() { { "Name", "new_name" } };
            Result<bool> editRes = api.EditProductDetails(store_owner, store_id, productId.Value, dictonary);
            Assert.False(editRes.ErrorOccured);
            Assert.True(!api.SearchProduct(dictonary).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyEditProductPrice()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            IDictionary<String, Object> dictonary = new Dictionary<String, Object>() { { "Price", 270 } };
            Result<bool> editRes = api.EditProductDetails(store_owner, store_id, productId.Value, dictonary);
            Assert.False(editRes.ErrorOccured);
            Assert.True(!api.SearchProduct(dictonary).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadEditProduct_UserWithNoPermission()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            IDictionary<String, Object> dictonary = new Dictionary<String, Object>() { { "Price", 270 } };
            Result<bool> EditRes = api.EditProductDetails(user_id, store_id, productId.Value,dictonary);
            Assert.True(EditRes.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void ComplexIntegrationEditProductTest()
        {
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            Result<bool> remProduct = this.api.RemoveProductFromStore(store_owner, store_id, productId.Value);
            Assert.True(!remProduct.ErrorOccured);
            remProduct = this.api.RemoveProductFromStore(store_owner, store_id, productId.Value);
            Assert.True(remProduct.ErrorOccured);
            productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(productId.ErrorOccured);
            IDictionary<String, Object> dictonary = new Dictionary<String, Object>() { { "Price", 270 } };
            Result<bool> editRes = api.EditProductDetails(user_id, store_id, productId.Value, dictonary);
            Assert.True(editRes.ErrorOccured);
            editRes = api.EditProductDetails(store_owner, store_id, productId.Value, dictonary);
            Assert.False(editRes.ErrorOccured);
            Assert.True(!api.SearchProduct(dictonary).ErrorOccured);
        }






    }
}
