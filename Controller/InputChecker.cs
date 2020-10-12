using System;
using System.Linq;
using Model;
using View;

namespace Controller

{
    class InputChecker
    {
        private MemberRegister _memberRegister;
        private WrongInputMessages _wrongInput;

        public bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            if(!ValidatePidInput(id))
            {
                _wrongInput.NotCorrectPId();
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                _wrongInput.PrintSsnNotExisting();
                return false;
            }
            else if(DoesPIdExistInRegister(id) && idExists)
            {
                _wrongInput.MemberAlreadyExists();
                return false;
            }

            return true;
        }

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

        public int ConvertToInt(string input)
        {
            try
            {
                int number = Convert.ToInt32(input);
                if(number > 0)
                {
                   return number;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public double ConvertToDouble(string input)
        {
            try
            {
                double length = Convert.ToDouble(input);
                if(length > 0)
                {
                    return length;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public bool IsCorrectNameInput(string input)
        {
            if(input.Any(char.IsDigit) || input.Length < 1)
            {
                _wrongInput.NotCorrectName();
                return false;
            }
            else {
                return true;
            }
        }
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
    
        public InputChecker(MemberRegister memberRegister, WrongInputMessages wrongInput)
        {
            _memberRegister = memberRegister;
            _wrongInput = wrongInput;
        }
    }
}