using System.Text;
using RivertyInterview.Models.ServiceModel;
using RivertyInterview.Models.Validation;

namespace RivertyInterview.Services
{
    internal class RomanLetterInfo
    {
        public RomanLetterInfo(char letter, int value, bool canConsecutivelyAppear)
        {
            Letter = letter;
            Value = value;
            CanConsecutivelyAppear = canConsecutivelyAppear;
        }

        public char Letter { get; init; }
        public int Value { get; init; }
        public bool CanConsecutivelyAppear { get; init; }
    }

    public class RomanNumberService : IRomanNumberService
    {
        private static readonly Dictionary<char, RomanLetterInfo> RomanLetterInfoMap = new Dictionary<char, RomanLetterInfo>
        {
            { 'I', new RomanLetterInfo('I', 1, true) },
            { 'V', new RomanLetterInfo('V', 5, false) },
            { 'X', new RomanLetterInfo('X', 10, true) },
            { 'L', new RomanLetterInfo('L', 50, false) },
            { 'C', new RomanLetterInfo('C', 100, true) },
            { 'D', new RomanLetterInfo('D', 500, false) },
            { 'M', new RomanLetterInfo('M', 1000, true) }
        };

        private char[] AllowedChars => RomanLetterInfoMap.Keys.ToArray();

        private string NotAllowedCharactersMessage
        {
            get
            {
                var AllowedCharsAsString = string.Join(",", AllowedChars.Select(ch => $"'{ch}'"));
                return $"Only these are allowed: {AllowedCharsAsString}.";
            }
        }

        private IRomanCalculationHistoryService _RomanCalculationHistoryService;

        public RomanNumberService(IRomanCalculationHistoryService RomanCalculationHistoryService)
        {
            _RomanCalculationHistoryService = RomanCalculationHistoryService;
        }

        public RomanNumberValidationStatus Validate(string romanNumber)
        {
            romanNumber = romanNumber.Trim().ToUpper();

            if (string.IsNullOrEmpty(romanNumber))
                return new RomanNumberValidationStatus(false);

            if (!HasValidCharacters(romanNumber))
                return new RomanNumberValidationStatus(true, NotAllowedCharactersMessage);

            if (!HasValidConsequence(romanNumber) && HasValidDiviasion(romanNumber) && HasOrderedSequence(romanNumber))
                return new RomanNumberValidationStatus(false);

            return new RomanNumberValidationStatus(true, "The roman number is not valid.");
        }

        private bool HasValidCharacters(string romanNumber)
        {
            return !romanNumber.Any(ch => !AllowedChars.Contains(ch));
        }

        private bool HasValidDiviasion(string romanNumber)
        {
            for (int i = 0; i < romanNumber.Length; i++)
            {
                var romanLetter = RomanLetterInfoMap[romanNumber[i]];

                if (i > 0)
                {
                    int prevValue = RomanLetterInfoMap[romanNumber[i - 1]].Value;
                    bool isCurrentValueGreaterThanPreviousValue = romanLetter.Value > prevValue;
                    bool isCurrentValueFiveTimesOfPreviousValue = romanLetter.Value != 5 * prevValue;
                    bool isCurrentValueTenTimesOfPreviousValue = romanLetter.Value != prevValue * 10;
                    if (isCurrentValueGreaterThanPreviousValue && isCurrentValueFiveTimesOfPreviousValue && isCurrentValueTenTimesOfPreviousValue)
                        return false;
                }
            }

            return true;
        }


        // ↓↓↓ This code is added because "IVX" is incorrect because I should not precede X after already being used in subtraction.
        private bool HasOrderedSequence(string romanNumber)
        {
            for (int i = 0; i < romanNumber.Length; i++)
            {
                var romanLetter = RomanLetterInfoMap.GetValueOrDefault(romanNumber[i]);
                if (romanLetter is null) return false;

                if (i > 1)
                {
                    int prevPrevValue = RomanLetterInfoMap[romanNumber[i - 2]].Value;
                    if (prevPrevValue < romanLetter.Value)
                        return false;
                }
            }

            return true;
        }

        private bool HasValidConsequence(string romanNumber)
        {
            int consecutiveCount = 1;
            char prevChar = '\0';

            foreach (char currentChar in romanNumber)
            {
                if (currentChar == prevChar)
                {
                    consecutiveCount++;
                    var romanLetterInfo = RomanLetterInfoMap[currentChar];
                    int maximumAllowedConsequenceForTheCharacter = romanLetterInfo.CanConsecutivelyAppear ? 3 : 1;
                    if (consecutiveCount > maximumAllowedConsequenceForTheCharacter) return true;
                }
                else
                { // ↓↓↓ reset consequence counter!
                    consecutiveCount = 1;
                    prevChar = currentChar;
                }
            }

            return false;
        }

        public int ConvertToInteger(string romanNumber)
        {
            romanNumber = romanNumber.Trim().ToUpper();
            if (!HasValidCharacters(romanNumber))
                throw new ArgumentException("Input has invalid characters.", nameof(romanNumber));


            int result = 0;

            for (int i = 0; i < romanNumber.Length; i++)
            {
                int currentValue = RomanLetterInfoMap[romanNumber[i]].Value;

                bool doesNextCharExistAndIsItGreaterThanCurrentCharacter = i < romanNumber.Length - 1 && RomanLetterInfoMap[romanNumber[i + 1]].Value > currentValue;
                result = doesNextCharExistAndIsItGreaterThanCurrentCharacter ? result - currentValue : result + currentValue;
            }

            return result;
        }


        private static string[] romanShortcuts = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
        private static int[] romanShortcutValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };

        public string ConvertToRoman(int number)
        {
            if (number < 1 || number > 3999) // the value should be between this range because I could not create an algorithm for all numbers.
                throw new ArgumentOutOfRangeException(nameof(number), "Input must be between 1 and 3999.");

            StringBuilder roman = new StringBuilder();

            for (int i = romanShortcutValues.Length - 1; i >= 0; i--)
            {
                while (number >= romanShortcutValues[i]) // add while instead of if because there might be several letter consequence. ex.: III
                {
                    roman.Append(romanShortcuts[i]);
                    number -= romanShortcutValues[i];
                }
            }

            return roman.ToString();
        }



        public string AddRomanNumbers(string leftRomanNumber, string rightRomanNumber)
        {
            int leftInteger = ConvertToInteger(leftRomanNumber);
            int rightInteger = ConvertToInteger(rightRomanNumber);
            int sum = leftInteger + rightInteger;
            string result = ConvertToRoman(sum);


            // ↓↓↓ No need to use await because we don't need to make user wait for the response.
            _RomanCalculationHistoryService.SaveAddHistory(
                new AddHistory { LeftOperand = leftRomanNumber, RightOperand = rightRomanNumber, Result = result }
                ); // We should also report if any problem occures here to our monitoring system. Here I didn't.

            return result;
        }

    }
}
