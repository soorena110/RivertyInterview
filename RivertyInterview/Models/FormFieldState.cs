using RivertyInterview.Models.Validation;

namespace RivertyInterview.Models
{
	public class FormFieldState<TValue>
	{
		public TValue Value { get; set; }
		public bool IsValidated { get; set; } = false;
		public ValidationState Validation { get; set; } = new ValidationState(false);

		public FormFieldState() : this(default!) { } // Value can be null here but we are ok with it. So I suppressed the warning with a lot of caution

		public FormFieldState(TValue value)
		{
			Value = value;
		}
	}
}
