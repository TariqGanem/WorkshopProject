using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class Store
    {
        #region parameters
        string Id { get; }
        string Name { get; }
        public string Founder { get; }
        public List<string> Owners { get; }
        public List<string> Managers { get; }
        #endregion

        #region constructors
        public Store(string id, string name, string founder, List<string> owners, List<string> managers)
        {
            this.Id = id;
            this.Name = name;
            this.Owners = owners;
            this.Founder = founder;
            this.Managers = managers;
        }
        #endregion
    }
}
