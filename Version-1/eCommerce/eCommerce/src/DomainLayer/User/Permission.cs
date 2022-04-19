using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public enum Methods : int
    {
        #region Inventory Management
        AddNewProduct = 0,
        RemoveProduct = 1,
        EditProduct = 2,
        #endregion

        #region Staff Management
        AddStoreOwner = 3,
        AddStoreManager = 4,
        RemoveStoreManager = 5,
        SetPermissions = 6,
        GetStoreStaff = 7,
        #endregion

        #region Policies Management
        SetPurchasePolicyAtStore = 8,
        GetPurchasePolicyAtStore = 9,
        SetDiscountPolicyAtStore = 10,
        GetDiscountPolicyAtStore = 11,
        #endregion

        #region Information
        GetStorePurchaseHistory = 12,
        #endregion

        AllPermissions = 777
    }

    public class Permission
    {
        public Boolean[] functionsBitMask { get; }
        public bool isOwner { get; }

        public Permission(bool isOwner = false)
        {
            this.isOwner = isOwner;
            functionsBitMask = new Boolean[13];
            functionsBitMask[(int)Methods.GetStoreStaff] = true;
        }

        public void SetPermission(Methods method, Boolean active)
        {
            functionsBitMask[(int)method] = active;
        }

        public void SetPermission(int method, Boolean active)
        {
            functionsBitMask[method] = active;
        }

        public void SetAllMethodesPermitted()
        {
            for (int i = 0; i < functionsBitMask.Length; i++)
            {
                functionsBitMask[i] = true;
            }
        }
    }
}
