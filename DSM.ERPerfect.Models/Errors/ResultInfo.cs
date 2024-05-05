namespace DSM.ERPerfect.Models.Errors
{
    public class ResultInfo<T>
    {
        public T Content { get; set; }
        private List<ResultError> errors;
        public List<ResultError> Errors
        {
            get
            {
                if (errors == null) errors = new List<ResultError>();
                return errors;
            }
            set { errors = value; }
        }
        public bool HasErrors
        {
            get
            {
                return (Errors.Count > 0);
            }
        }
    }
}