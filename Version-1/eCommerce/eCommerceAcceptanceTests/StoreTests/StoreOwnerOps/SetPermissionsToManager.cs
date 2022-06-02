﻿using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.4.7
namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class SetPermissionsToManager : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public String manager_id { set; get; }
        public SetPermissionsToManager() : base()
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
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            Assert.True(permRes.ErrorOccured == false);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyManagerPermissions_removeProduct()
        { 
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(1);
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            Assert.True(permRes.ErrorOccured == false);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadManagerPermissions_UneligiblePerm()
        {
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(20);
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            Assert.True(permRes.ErrorOccured == true);
        }
    }
}