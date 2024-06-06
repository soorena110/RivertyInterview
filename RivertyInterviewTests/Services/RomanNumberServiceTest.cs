using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using RivertyInterview.Models.Repository;
using RivertyInterview.Services;

namespace RivertyTest.Tests
{
    public class RomanNumberServiceTests
	{
#pragma warning disable CS8618 // Non-nullable field! I know what I am doing here!
        private RomanNumberService romanNumberService;
#pragma warning restore CS8618


        [SetUp]
		public void Setup()
		{
            var romanCalculationHistoryRepository = new Mock<IRomanCalculationHistoryRepository>(); // ⚠️ Should not be connected to the real database.
            var romanCalculationHistoryService = new RomanCalculationHistoryService(romanCalculationHistoryRepository.Object);
            romanNumberService = new RomanNumberService(romanCalculationHistoryService);
        }


		[TestCase("XIV")]
		[TestCase("XXIX")]
		[TestCase("LXXVII")]
		[TestCase("XCIV")]
		[TestCase("CXLVIII")]
		[TestCase("CLXIII")]
		[TestCase("CDXII")]
		[TestCase("DCCCXC")]
		[TestCase("CMX")]
		[TestCase("MCDXCIV")]
		public void Validate_ValidRomanNumber_ReturnsNoError(string romanNumber)
		{
			var validationStatus = romanNumberService.Validate(romanNumber);

			ClassicAssert.IsFalse(validationStatus.IsInvalid);
			ClassicAssert.IsNull(validationStatus.Message);
		}

		[TestCase("iV")]
		[TestCase("MmM")]
		[TestCase("vI")]
		[TestCase("Cm")]
		public void Validate_ValidLowerCaseRomanNumber_ReturnsNoError(string romanNumber)
		{
			var validationStatus = romanNumberService.Validate(romanNumber);

			ClassicAssert.IsFalse(validationStatus.IsInvalid);
			ClassicAssert.IsNull(validationStatus.Message);
		}

		[TestCase("ABCD")]
		[TestCase("EFGHIJK")]
		[TestCase("12345")]
		[TestCase("!@#$%")]
		public void Validate_InvalidCharacters_ReturnsError(string romanNumber)
		{
			var validationStatus = romanNumberService.Validate(romanNumber);
			ClassicAssert.IsTrue(validationStatus.IsInvalid);
			ClassicAssert.AreEqual("Only these are allowed: 'I','V','X','L','C','D','M'.", validationStatus.Message);
		}

		[TestCase("IIII")]
		[TestCase("IIX")]
		[TestCase("XXXX")]
		[TestCase("VV")]
		[TestCase("IVX")]
		[TestCase("LL")]
		[TestCase("DD")]
		[TestCase("VVV")]
		[TestCase("LLLC")]
		[TestCase("IC")]
		[TestCase("IL")]
		[TestCase("IM")]
		[TestCase("XD")]
		[TestCase("XM")]
		[TestCase("ID")]
		[TestCase("DM")]
		[TestCase("VM")]
		[TestCase("VX")]
		[TestCase("LC")]
		[TestCase("DM")]
		public void Validate_NonValidRomanNumber_ReturnsError(string romanNumber)
		{
			var validationStatus = romanNumberService.Validate(romanNumber);
			ClassicAssert.IsTrue(validationStatus.IsInvalid);
			ClassicAssert.AreEqual("The roman number is not valid.", validationStatus.Message);
		}


		[TestCase("I", 1)]
		[TestCase("V", 5)]
		[TestCase("X", 10)]
		[TestCase("L", 50)]
		[TestCase("C", 100)]
		[TestCase("D", 500)]
		[TestCase("M", 1000)]
		[TestCase("IV", 4)]
		[TestCase("IX", 9)]
		[TestCase("XL", 40)]
		[TestCase("XC", 90)]
		[TestCase("CD", 400)]
		[TestCase("CM", 900)]
		[TestCase("III", 3)]
		[TestCase("XXX", 30)]
		[TestCase("CCC", 300)]
		[TestCase("MMM", 3000)]
		[TestCase("XXII", 22)]
		[TestCase("MDCLXVI", 1666)]
		[TestCase("MMXXI", 2021)]
		public void ConvertToInteger_ValidRomanNumeral_ReturnsCorrectValue(string romanNumber, int expected)
		{
			int result = romanNumberService.ConvertToInteger(romanNumber);
			ClassicAssert.AreEqual(expected, result);
		}


		[TestCase(1, "I")]
		[TestCase(2, "II")]
		[TestCase(3, "III")]
		[TestCase(4, "IV")]
		[TestCase(5, "V")]
		[TestCase(6, "VI")]
		[TestCase(7, "VII")]
		[TestCase(8, "VIII")]
		[TestCase(9, "IX")]
		[TestCase(10, "X")]
		[TestCase(11, "XI")]
		[TestCase(12, "XII")]
		[TestCase(13, "XIII")]
		[TestCase(14, "XIV")]
		[TestCase(15, "XV")]
		[TestCase(16, "XVI")]
		[TestCase(17, "XVII")]
		[TestCase(18, "XVIII")]
		[TestCase(19, "XIX")]
		[TestCase(20, "XX")]
		[TestCase(21, "XXI")]
		[TestCase(22, "XXII")]
		[TestCase(23, "XXIII")]
		[TestCase(24, "XXIV")]
		[TestCase(25, "XXV")]
		[TestCase(26, "XXVI")]
		[TestCase(27, "XXVII")]
		[TestCase(28, "XXVIII")]
		[TestCase(29, "XXIX")]
		[TestCase(30, "XXX")]
		[TestCase(31, "XXXI")]
		[TestCase(32, "XXXII")]
		[TestCase(33, "XXXIII")]
		[TestCase(34, "XXXIV")]
		[TestCase(35, "XXXV")]
		[TestCase(36, "XXXVI")]
		[TestCase(37, "XXXVII")]
		[TestCase(38, "XXXVIII")]
		[TestCase(39, "XXXIX")]
		[TestCase(40, "XL")]
		[TestCase(41, "XLI")]
		[TestCase(42, "XLII")]
		[TestCase(43, "XLIII")]
		[TestCase(44, "XLIV")]
		[TestCase(45, "XLV")]
		[TestCase(46, "XLVI")]
		[TestCase(47, "XLVII")]
		[TestCase(48, "XLVIII")]
		[TestCase(49, "XLIX")]
		[TestCase(50, "L")]
		[TestCase(51, "LI")]
		[TestCase(52, "LII")]
		[TestCase(53, "LIII")]
		[TestCase(54, "LIV")]
		[TestCase(55, "LV")]
		[TestCase(56, "LVI")]
		[TestCase(57, "LVII")]
		[TestCase(58, "LVIII")]
		[TestCase(59, "LIX")]
		[TestCase(60, "LX")]
		[TestCase(61, "LXI")]
		[TestCase(62, "LXII")]
		[TestCase(63, "LXIII")]
		[TestCase(64, "LXIV")]
		[TestCase(65, "LXV")]
		[TestCase(66, "LXVI")]
		[TestCase(67, "LXVII")]
		[TestCase(68, "LXVIII")]
		[TestCase(69, "LXIX")]
		[TestCase(70, "LXX")]
		[TestCase(71, "LXXI")]
		[TestCase(72, "LXXII")]
		[TestCase(73, "LXXIII")]
		[TestCase(74, "LXXIV")]
		[TestCase(75, "LXXV")]
		[TestCase(76, "LXXVI")]
		[TestCase(77, "LXXVII")]
		[TestCase(78, "LXXVIII")]
		[TestCase(79, "LXXIX")]
		[TestCase(80, "LXXX")]
		[TestCase(81, "LXXXI")]
		[TestCase(82, "LXXXII")]
		[TestCase(83, "LXXXIII")]
		[TestCase(84, "LXXXIV")]
		[TestCase(85, "LXXXV")]
		[TestCase(86, "LXXXVI")]
		[TestCase(87, "LXXXVII")]
		[TestCase(88, "LXXXVIII")]
		[TestCase(89, "LXXXIX")]
		[TestCase(90, "XC")]
		[TestCase(91, "XCI")]
		[TestCase(92, "XCII")]
		[TestCase(93, "XCIII")]
		[TestCase(94, "XCIV")]
		[TestCase(95, "XCV")]
		[TestCase(96, "XCVI")]
		[TestCase(97, "XCVII")]
		[TestCase(98, "XCVIII")]
		[TestCase(99, "XCIX")]
		[TestCase(100, "C")]
		[TestCase(101, "CI")]
		[TestCase(102, "CII")]
		[TestCase(103, "CIII")]
		[TestCase(104, "CIV")]
		[TestCase(105, "CV")]
		[TestCase(106, "CVI")]
		[TestCase(107, "CVII")]
		[TestCase(108, "CVIII")]
		[TestCase(109, "CIX")]
		[TestCase(110, "CX")]
		[TestCase(111, "CXI")]
		[TestCase(112, "CXII")]
		[TestCase(113, "CXIII")]
		[TestCase(114, "CXIV")]
		[TestCase(115, "CXV")]
		[TestCase(116, "CXVI")]
		[TestCase(117, "CXVII")]
		[TestCase(118, "CXVIII")]
		[TestCase(400, "CD")]
		[TestCase(500, "D")]
		[TestCase(900, "CM")]
		[TestCase(1000, "M")]
		[TestCase(3999, "MMMCMXCIX")]
		[TestCase(14, "XIV")]
		[TestCase(29, "XXIX")]
		[TestCase(77, "LXXVII")]
		[TestCase(94, "XCIV")]
		[TestCase(148, "CXLVIII")]
		[TestCase(163, "CLXIII")]
		[TestCase(412, "CDXII")]
		[TestCase(890, "DCCCXC")]
		[TestCase(910, "CMX")]
		[TestCase(1494, "MCDXCIV")]
		public void ConvertToRoman_ValidInput_ReturnsExpectedResult(int input, string expected)
		{
			string result = romanNumberService.ConvertToRoman(input);
			ClassicAssert.AreEqual(expected, result);
		}

		[Test]
		public void ConvertToRoman_InputZero_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => romanNumberService.ConvertToRoman(0));
		}

		[Test]
		public void ConvertToRoman_InputNegative_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => romanNumberService.ConvertToRoman(-1));
		}

		[Test]
		public void ConvertToRoman_InputGreaterThan3999_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => romanNumberService.ConvertToRoman(4000));
		}

		[TestCase("I", "I", ExpectedResult = "II")]
		[TestCase("V", "V", ExpectedResult = "X")]
		[TestCase("V", "I", ExpectedResult = "VI")]
		[TestCase("IV", "VI", ExpectedResult = "X")]
		[TestCase("IX", "I", ExpectedResult = "X")]
		[TestCase("X", "XX", ExpectedResult = "XXX")]
		[TestCase("XL", "XL", ExpectedResult = "LXXX")]
		[TestCase("C", "C", ExpectedResult = "CC")]
		[TestCase("CD", "DC", ExpectedResult = "M")]
		[TestCase("D", "D", ExpectedResult = "M")]
		[TestCase("M", "M", ExpectedResult = "MM")]
		[TestCase("CM", "C", ExpectedResult = "M")]
		[TestCase("LXXIV", "DII", ExpectedResult = "DLXXVI")]
		[TestCase("MCLI", "DXV", ExpectedResult = "MDCLXVI")]
		[TestCase("MCMXCIX", "IV", ExpectedResult = "MMIII")]
		[TestCase("MMM", "CM", ExpectedResult = "MMMCM")]
		public string AddRomanNumbers_ValidInput_ReturnsCorrectResult(string leftValue, string rightValue)
		{
			string result = romanNumberService.AddRomanNumbers(leftValue, rightValue);
			return result;
		}

		[TestCase("LXaXIV", "DII")]
		[TestCase("MM2M", "CM")]
		[TestCase("M", "CM2")]
		[TestCase("DII", "ABC")]
		public void AddRomanNumbers_InalidInput_ThrowsException(string leftValue, string rightValue)
		{
			Assert.Throws<ArgumentException>(() =>
			{
				romanNumberService.AddRomanNumbers(leftValue, rightValue);
			}, "Input has invalid characters.");
		}
	}
}