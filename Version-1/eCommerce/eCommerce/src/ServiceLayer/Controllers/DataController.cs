using eCommerce.src.DomainLayer;
using eCommerce.src.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IDataController
    {
        List<StoreService> GetAllStoresToDisplay();
        List<ProductService> GetAllProductByStoreIDToDisplay(string storeID);
        Boolean[] GetPermission(string userID, string storeID);
        List<ProductService> GetAllProducts();
    }

    public class DataController : IDataController
    {
        //Properties
        public ISystemFacade systemfacade { get; }

        //Constructor
        public DataController(ISystemFacade storesAndManagementInterface)
        {
            this.systemfacade = storesAndManagementInterface;
        }

        public List<StoreService> GetAllStoresToDisplay()
        {
            return systemfacade.GetAllStoresToDisplay();
        }
        public List<ProductService> GetAllProductByStoreIDToDisplay(string storeID)
        {
            return systemfacade.GetAllProductByStoreIDToDisplay(storeID);
        }

        public Boolean[] GetPermission(string userID, string storeID)
        {
            return systemfacade.GetPermission(userID, storeID);
        }

        public List<ProductService> GetAllProducts()
        {

        }


       
    }
}
