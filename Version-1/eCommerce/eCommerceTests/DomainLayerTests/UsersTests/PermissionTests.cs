using eCommerce.src.DomainLayer.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace eCommerce.Tests.DomainLayerTests.UsersTests
{
    public class PermissionTests
    {
        Permission permission = new Permission();

        [Fact]
        public void SetPermissionTest1()
        {
            permission.SetPermission(Methods.GetStoreStaff, true);
            Assert.IsTrue(permission.functionsBitMask[(int)Methods.GetStoreStaff]);
            permission.SetPermission(Methods.GetStoreStaff, false);
            Assert.IsFalse(permission.functionsBitMask[(int)Methods.GetStoreStaff]);
        }

        [Fact]
        public void SetPermissionTest2()
        {
            permission.SetPermission(7, true);
            Assert.IsTrue(permission.functionsBitMask[7]);
            permission.SetPermission(7, false);
            Assert.IsFalse(permission.functionsBitMask[7]);
        }

        [Fact]
        public void SetAllMethodesPermittedTest()
        {
            permission.SetAllMethodesPermitted();
            int true_counter = 0;
            for (int i = 0; i < permission.functionsBitMask.Length; i++)
            {
                if(permission.functionsBitMask[i] == true)
                    true_counter++;
            }
            Assert.IsTrue(true_counter == 13);
        }
    }
}
