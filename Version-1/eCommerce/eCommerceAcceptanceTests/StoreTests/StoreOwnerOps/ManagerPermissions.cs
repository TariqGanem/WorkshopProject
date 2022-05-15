using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class ManagerPermissions : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public String manager_id { set; get; }
        public ManagerPermissions() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;
            Result<bool> reguserId2 = this.api.Register("Manager@gmail.com", "ManagerPassword");
            Result<String> userId2 = this.api.Login("Manager@gmail.com", "ManagerPassword");
            this.manager_id = userId2.Value;
            Result<bool> managerRes = api.AddStoreManager(manager_id, store_owner, store_id);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyManagerPermissions_addProduct()
        {
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(0);
            permission.AddLast(1);
            Result<String> addProdRes = api.AddProductToStore(manager_id, store_id, "Product_name", 10, 10, "Category");
            Assert.True(addProdRes.ErrorOccured);
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            Assert.True(permRes.ErrorOccured == false);
            addProdRes = api.AddProductToStore(manager_id, store_id, "Product_name", 10, 10, "Category");
            Assert.False(addProdRes.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyManagerPermissions_removeProduct()
        {
            Result<String> product = api.AddProductToStore(store_owner, store_id, "Product_name", 10, 10, "Category");
            String product_id = product.Value;
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(0);
            permission.AddLast(1);
            Result<bool> addProdRes = api.RemoveProductFromStore(manager_id, store_id, product_id);
            Assert.True(addProdRes.ErrorOccured);
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            Assert.True(permRes.ErrorOccured == false);
            addProdRes = api.RemoveProductFromStore(manager_id, store_id, product_id);
            Assert.False(addProdRes.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadManagerPermissions_RemoveAddWithNoPerm()
        {
            Result<String> addProdRes = api.AddProductToStore(manager_id, store_id, "Product_name", 10, 10, "Category");
            Assert.True(addProdRes.ErrorOccured);
            Result<String> product = api.AddProductToStore(store_owner, store_id, "Product_name", 10, 10, "Category");
            String product_id = product.Value;
            Result<bool> RemoveRes = api.RemoveProductFromStore(manager_id, store_id, product_id);
            Assert.True(RemoveRes.ErrorOccured);
        }
    }
}
