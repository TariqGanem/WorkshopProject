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
        
        public InventoryManagerTests()
        {
            this.InventoryManager = new InventoryManager();
        }


    }
}
