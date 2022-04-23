using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Response
{
    /// <summary>
    /// This class extends <c>Response</c> and represents the result of a call to a non-void function. 
    /// In addition to the behavior of <c>Response</c>, the class holds the value of the returned value in the variable <c>Value</c>.
    /// </summary>
    /// <typeparam name="T">The type of the returned value of the function.</typeparam>
    public class Result<T> : Result
    {
        #region parameters
        public readonly T Value;
        #endregion

        #region constructos
        public Result(string msg) : base(msg) { }
        public Result(T value) : base()
        {
            this.Value = value;
        }
        public Result(T value, string msg) : base(msg)
        {
            this.Value = value;
        }
        #endregion
    }
}
