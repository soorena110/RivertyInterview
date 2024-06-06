using RivertyInterview.Models.Validation;

namespace RivertyInterview.Models
{
    public class RomanAddFormDto
	{
		public FormFieldState<string> Left { get; set; } = new FormFieldState<string>(string.Empty);
		public FormFieldState<string> Right { get; set; } = new FormFieldState<string>(string.Empty);

		public string Result { get; set; } = string.Empty;

		public bool IsSubmitable => Left.IsValidated && Right.IsValidated && !Left.Validation.IsInvalid && !Right.Validation.IsInvalid;
	}
}
