using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Controllers;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using Newtonsoft.Json;

namespace eCommerce.src.ServiceLayer
{

    public interface IeCommerceAPI : IUserController, IGuestController, IRegisteredUserController, ISystemAdminController, IStoreStaffController { }
    public class eCommerceSystem 
    {
        public IUserController UserController { get; set; }
        public IGuestController GuestController { get; set; }
        public IRegisteredUserController RegisteredUserController { get; set; }
        public SystemAdminController SystemAdminController { get; set; }
        public IStoreStaffController StoreStaffController { get; set; }
        public SystemFacade systemFacade { get; set; }
        public NotificationsService notificationsService { get; set; }

        public eCommerceSystem(String config_path = @"..\eCommerce\Config.json" , string configData = "")
        {
            Config config;
            if (!(configData.Equals(String.Empty)))
            {
                config = JsonConvert.DeserializeObject<Config>(configData);

            }
            else
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(config_path));

            }

            //validate JSON
            if ((config.externalSystem_url is null) || (config.mongoDB_url is null)
                || (config.password is null) || (config.email is null) || (config.dbenv is null))
            {
                Logger.GetInstance().LogError("Invalid JSON format - One or more missing attribute has been found in the config JSON");
                Environment.Exit(1);
            }

            DBUtil.getInstance(config.mongoDB_url, config.dbenv);
            Proxy.getInstance(config.externalSystem_url);
            systemFacade = new SystemFacade();
            UserController = new UserController(systemFacade);
            GuestController = new GuestController(systemFacade);
            RegisteredUserController = new RegisteredUserController(systemFacade);
            SystemAdminController = new SystemAdminController(systemFacade);
            StoreStaffController = new StoreStaffController(systemFacade);
        }

    }
}
