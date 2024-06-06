namespace RivertyInterview.Models.Validation
{
	public class RomanNumberValidationStatus : ValidationState
	{
		public RomanNumberValidationStatus(bool isInvalid, string? errorMessage = null) : base(isInvalid, errorMessage)
		{
		}
	}

}
