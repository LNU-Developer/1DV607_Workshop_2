using System;
using Model;

namespace Controller
{
    /// <summary>
    ///  This class that handles and validates inputs from the user.
    /// </summary>
    abstract class MainInputController
    {
        private MemberRegister _memberRegister;

        public MemberRegister MemberRegister { get { return _memberRegister; } }

        public abstract bool IsCorrectInputOfSsn (string id, bool idExists = false);

        /// <summary>
        /// Validates a social secutiry number if this is the correct format and is a real swedish social security number.
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="_identity">A string containing the social security number.</param>
        public bool ValidatePidInput(string _identity)
        {
            if (PIdInputIsCorrectFormat(_identity) && IsSwedishSsn(_identity))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Validates a social secutiry number if this is the correct format.
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="_identity">A string containing the social security number.</param>

        private bool PIdInputIsCorrectFormat(string _identity) {
            _identity = _identity.Replace("-", "");
            _identity = _identity.Replace("+", "");

            // Check so every character in identity is a number between 0 and 9
            foreach (char c in _identity)
            {
                if (c < '0' || c > '9') return false;
            }

            if (_identity.Length < 10)
            {
                return false;
            }
            else if (_identity.Length == 12)
            {
                _identity = _identity.Substring(2);
            }
            return true;
        }
        /// <summary>
        /// Validates a social secutiry number if this is a real social security number.
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="_identity">A string containing the social security number.</param>

        private bool IsSwedishSsn(string _identity)
        {
            double[] chars = new double[10];

            for (int i = 0; i<10; i=i+2)
            {
                chars[i] = SumDigits(Char.GetNumericValue(_identity[i])*2);
            }
            for (int i = 1; i<10; i=i+2)
            {
                chars[i] = SumDigits(Char.GetNumericValue(_identity[i]));
            }

            double sum = 0;
            for (int i = 0; i<chars.Length-1;i++)
            {
                sum = sum + chars[i];
            }
            string digitString = sum.ToString();
            double lastDigit = Char.GetNumericValue(digitString[digitString.Length-1]);

            double checksum;
            double lastDigitDiff = 10-lastDigit;

            if(lastDigitDiff == 10)
            {
                checksum = 0;
            }
            else
            {
                checksum = lastDigitDiff;
            }

            if (checksum == Char.GetNumericValue(_identity[_identity.Length-1]))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Summurizes all individual digits in a number. E.g 12 would return 1+2 = 3.
        /// </summary>
        /// <returns>
        /// Sum of digits in a number
        /// </returns>
        /// <param name="number">a number.</param>

        private double SumDigits(double number)
        {
            double sum = 0;
            string temp = number.ToString();
            for(int i = 0; i < temp.Length; i++)
            {
                sum = sum + Char.GetNumericValue(temp[i]);
            }
            return sum;
        }

        /// <summary>
        /// Check if the input is correct in a social security number
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="id">the social security number in string format.</param>
        /// <param name="idExists">Flag to show if the social security number exists in the register or not.</param>
        public bool DoesPIdExistInRegister(string pId)
        {
            foreach (Member member in _memberRegister.Members)
            {
                if(member.PersonalId == pId)
                {
                    return true;
                }
            }
            return false;
        }

        protected MainInputController (MemberRegister MemberRegister)
        {
            _memberRegister = MemberRegister;
        }
    }
}