using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IRegisteredUserController
    {
        Result<RegisteredUserSO> Login(String userName, String password);
        Result<UserHistorySO> GetUserPurchaseHistory(String userId);
        Result<StoreService> OpenNewStore(String storeName, String userId);
        Result CloseStore(string userId, string storeId);
        Result<StoreService> ReOpenStore(string storeid, string userid);
        Result<bool> AddStoreRating(string userid, string storeid, double rate);
        Result<bool> AddProductRatingInStore(string userid, string storeid, string productid, double rate);
        Result<LinkedList<Notification>> getUserNotifications(string userid);
    }

    public class RegisteredUserController : UserController, IRegisteredUserController
    {
        Logger logger = Logger.GetInstance();
        public RegisteredUserController(ISystemFacade systemFacade) : base(systemFacade) { }

        #region RegisteredtUserInterfaceMethods
        public Result<RegisteredUserSO> Login(String userName, String password)
        {
            try
            {
                ValidateCredentials(userName, password);
                RegisteredUserSO user = SystemFacade.Login(userName, password);
                logger.LogInfo($"RegisteredUserController --> User with id: {userName}, successfully logged in to the system.");
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        public new Result<UserHistorySO> GetUserPurchaseHistory(String userId)
        {
            try
            {
                ValidateId(userId);
                UserHistorySO history = SystemFacade.GetUserPurchaseHistory(userId);
                logger.LogInfo($"RegisteredUserController --> Getting user: {userId} purchase history successfully.");
                return new Result<UserHistorySO>(history);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<UserHistorySO>(e.Message);
            }
        }

        public Result<StoreService> OpenNewStore(string storeName, string userId)
        {
            try
            {
                ValidateId(userId);
                if (storeName == null || storeName.Length == 0)
                    return new Result<StoreService>("Tyring to open new store with invalid args.");
                StoreService store = SystemFacade.OpenNewStore(storeName, userId);
                logger.LogInfo($"RegisteredUserController --> User with id: {userId}, has opened a new store with name: {storeName} successfully.");
                return new Result<StoreService>(store);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<StoreService>(e.Message);
            }
        }

        public Result CloseStore(string userId, string storeId)
        {
            try
            {
                ValidateId(userId);
                ValidateId(storeId);
                SystemFacade.CloseStore(userId, storeId);
                logger.LogInfo($"RegisteredUserController --> User with id: {userId}, closed a store with id: {storeId} successfully.");
                return new Result();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result(e.Message);
            }
        }

        public Result<StoreService> ReOpenStore(string storeid, string userid)
        {
            try
            {
                ValidateId(storeid);
                ValidateId(userid);
                StoreService res = SystemFacade.ReOpenStore(storeid, userid);
                return new Result<StoreService>(res);
            }
            catch(Exception e)
            {
                return new Result<StoreService>(e.ToString());
            }
        }

        public Result<bool> AddStoreRating(string userid, string storeid, double rate)
        {
            try
            {
                bool res = this.SystemFacade.AddStoreRating(userid,storeid,rate);
                return new Result<bool>(res);
            }
            catch (Exception e)
            {
                return new Result<bool>(e.ToString());
            }
        }

        public Result<bool> AddProductRatingInStore(string userid, string storeid, string productid, double rate)
        {
            try
            {
                bool res = this.SystemFacade.AddProductRatingInStore(userid, storeid,productid, rate);
                return new Result<bool>(res);
            }
            catch (Exception e)
            {
                return new Result<bool>(e.ToString());
            }
        }

         public Result<LinkedList<Notification>> getUserNotifications(string userid)
        {
            try
            {
                LinkedList<Notification> res = this.SystemFacade.getUserNotifications(userid);
                return new Result<LinkedList<Notification>>(res);
            }
            catch (Exception e)
            {
                return new Result<LinkedList<Notification>>(e.ToString());
            }
        }



        #endregion
    }
}
