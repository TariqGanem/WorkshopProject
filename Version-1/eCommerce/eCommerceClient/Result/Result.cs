using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerceClient.Result
{
    public class Result
    {
        #region parameters
        public readonly string ErrorMessage;
        public bool ErrorOccured { get => ErrorMessage != null; }
        #endregion

        #region constructors
        public Result() { }
        public Result(string msg)
        {
            this.ErrorMessage = msg;
        }
        #endregion
    }
}