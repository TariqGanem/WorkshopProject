﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class PermissionService
    {
        //Properties
        public Boolean[] functionsBitMask { get; }
        public bool isOwner { get; }

        //Constructor
        public PermissionService(bool[] functionsBitMask, bool isOwner = false)
        {
            this.isOwner = isOwner;
            this.functionsBitMask = functionsBitMask;
        }
    }
}
