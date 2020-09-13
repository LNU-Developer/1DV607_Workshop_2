using System;
using System.Text.RegularExpressions;
using Google.Cloud.Firestore;

namespace workshop_2
{
    [FirestoreData]
    class Member
    {
        //Fields
        private string _firstName;
        private string _lastName;
        private string _personalId;
        private int _memberId;

        private BoatRegister _boatRegister = new BoatRegister();

        //Properties
        public string FullName
        {
             get
             {
                return _firstName + " " + _lastName;
             }
        }
        [FirestoreProperty]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value.Length < 1 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must have more than two characters");

                _firstName=value;
            }
        }
        [FirestoreProperty]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value.Length < 1 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must have more than two characters");

                _lastName=value;
            }
        }
        [FirestoreProperty]
        public string PersonalId
        {
            get
            {
                return _personalId;
            }
            set
            {
                string pattern = @"^(19|20)?(\d{6}([-+]|)\d{4}|(?!19|20)\d{10})$";
                if (!IsSwedishSsn(value) || !Regex.IsMatch(value, pattern))
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");
                _personalId=value;
            }
        }
        [FirestoreProperty]
        public int MemberId
        {
            get
            {
                return _memberId;
            }
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");

                _memberId=value;
            }
        }
         public BoatRegister BoatRegister
        {
             get
             {
                return _boatRegister;
             }
        }

        //Method

        //Lunas Algorithm
        private bool IsSwedishSsn(string identity)
        {
            identity = identity.Replace("-", "");
            identity = identity.Replace("+", "");

            if (identity.Length == 12)
                identity = identity.Substring(2, 10);

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