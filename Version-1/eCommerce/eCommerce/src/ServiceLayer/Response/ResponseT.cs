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
    public class Response<T> : Response
    {
        #region parameters
        public readonly T Value;
        #endregion

        #region constructos
        public Response(string msg) : base(msg) { }
        public Response(T value) : base()
        {
            this.Value = value;
        }
        public Response(T value, string msg) : base(msg)
        {
            this.Value = value;
        }
        #endregion
    }
}
