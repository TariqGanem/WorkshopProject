using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class Store
    {
        #region parameters
        string id { get; }
        string name { get; }
        public string founder { get; }
        public List<string> owners { get; }
        public List<string> managers { get; }
        #endregion

        #region constructors
        public Store(string id, string name, string founder, List<string> owners, List<string> managers)
        {
            this.id = id;
            this.name = name;
            this.founder = founder;
            this.managers = managers;
        }
        #endregion
    }
}
