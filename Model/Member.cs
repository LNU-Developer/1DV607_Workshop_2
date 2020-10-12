using System;
using System.Text.RegularExpressions;
using Google.Cloud.Firestore;

namespace Model
{
    [FirestoreData]
    class Member
    {
        private string _firstName;
        private string _lastName;
        private string _personalId;
        private int _memberId;

        private BoatRegister _boatRegister;

        [FirestoreProperty]
        public string FirstName
        {
            get { return _firstName; }
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
            get { return _lastName; }
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
            get { return _personalId; }
            set
            {
                string pattern = @"^(19|20)?(\d{6}([-+]|)\d{4}|(?!19|20)\d{10})$";
                if (!Regex.IsMatch(value, pattern))
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");
                _personalId=value;
                _boatRegister = new BoatRegister(value);
            }
        }

        [FirestoreProperty]
        public int MemberId
        {
            get { return _memberId; }
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
            get { return _boatRegister; }
        }
    }
}