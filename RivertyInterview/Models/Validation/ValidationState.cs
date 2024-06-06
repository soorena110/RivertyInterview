namespace RivertyInterview.Models.Validation
{

    public class ValidationState
    {
        public bool IsInvalid { get; }
        public string? Message { get; }

        public ValidationState(bool isInvalid, string? errorMessage = null)
        {
            IsInvalid = isInvalid;
            Message = errorMessage;
        }
    }

}
