using RivertyInterview.Models.Validation;

namespace RivertyInterview.Models.ServiceModel
{

    /// 
    ///// Here we can also used abstract instead of interface because this is an entity (class) not a behaviour (interface)
    ///   This patter is also used some codes in .NET

    public interface IRomanNumberService
	{
		public string ConvertToRoman(int number);

		public int ConvertToInteger(string romanNumber);

		public string AddRomanNumbers(string leftRomanNumber, string rightRomanNumber);

		public RomanNumberValidationStatus Validate(string romanNumber);
	}
}
