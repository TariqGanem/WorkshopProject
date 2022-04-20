using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class IStaffService
    {
        public String Id { get; }

        public IStaffService(string id)
        {
            Id = id;
        }
    }
}
