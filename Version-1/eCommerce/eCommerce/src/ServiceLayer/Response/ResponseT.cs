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
    internal class Response<T> : Response
    {
        public readonly T Value;
        internal Response(string msg) : base(msg) { }
        internal Response(T value) : base()
        {
            this.Value = value;
        }
        internal Response(T value, string msg) : base(msg)
        {
            this.Value = value;
        }
    }
}
