﻿using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Purchase.Policies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;


namespace eCommerce.src.DomainLayer.Stores.Purchase.Types
{
    internal interface IPurchaseType
    {
        string Id { get; }
        bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user);
        bool AddPolicy(IPurchasePolicy policy);
        IPurchasePolicy RemovePolicy(string id);

    }
}