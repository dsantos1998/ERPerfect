namespace DSM.ERPerfect.Models.Errors
{
    public class ResultError
    {
        public string ErrorMessage { get; set; }
        public string Method { get; set; }
        public bool IsException { get; set; }

        public ResultError(string errorMessage, bool isException, string method)
        {
            ErrorMessage = errorMessage;
            IsException = isException;
            Method = method;
        }
    }
}