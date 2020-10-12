using System;

namespace Model
{
    class SsnRegister
    {
        //Lunas Algorithm
        public bool validatePidInput(string identity)
        {
            if (PIdInputIsCorrectFormat(identity) && isSwedishSsn(identity))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private bool PIdInputIsCorrectFormat(string identity) {
            identity = identity.Replace("-", "");
            identity = identity.Replace("+", "");

            // Check so every character in identity is a number between 0 and 9
            foreach (char c in identity)
            {
                if (c < '0' || c > '9') return false;
            }

            if (identity.Length < 10)
            {
                return false;
            }
            else if (identity.Length == 12)
            {
                identity = identity.Substring(2);
            }
            return true;
        }

        private bool isSwedishSsn(string identity)
        {
            double[] chars = new double[10];

            for (int i = 0; i<10; i=i+2)
            {
                chars[i] = SumDigits(Char.GetNumericValue(identity[i])*2);
            }
            for (int i = 1; i<10; i=i+2)
            {
                chars[i] = SumDigits(Char.GetNumericValue(identity[i]));
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

            if (checksum == Char.GetNumericValue(identity[identity.Length-1]))
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
    }

}

    
