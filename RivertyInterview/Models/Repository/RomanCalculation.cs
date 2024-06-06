using System.ComponentModel;

namespace RivertyInterview.Models.Repository
{
	public class RomanCalculation
	{
		public int Id { get; set; }

		[DisplayName("Roman Left Operand")]
		public string LeftOperand { get; set; } = string.Empty;

		[DisplayName("Roman Right Operand")]
		public string RightOperand { get; set; } = string.Empty;

		[DisplayName("Operation")]
		public RomanCalculationOperation Operation { get; set; }

        [DisplayName("Operation Result")]
        public string Result { get; set; } = string.Empty;

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
