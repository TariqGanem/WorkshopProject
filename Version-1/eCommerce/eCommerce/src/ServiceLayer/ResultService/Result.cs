namespace eCommerce.src.ServiceLayer.ResultService
{
    /// <summary>
    /// Class <c>Response</c> represents the result of a call to a void function.
    /// If an exception was thrown, <c>ErrorOccured = true</c> and <c>ErrorMessage != null</c>. 
    /// Otherwise, <c>ErrorOccured = false</c> and <c>ErrorMessage = null</c>.
    /// </summary>
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

