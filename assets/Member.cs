using System;

namespace workshop_2
{
    class Member
    {
        //Fields
        private string _firstName;
        private string _lastName;
        private int _personalId;
        private int _memberId;

        //Properties
        public string FullName
        {
             get
             {
                return _firstName + " " + _lastName;
             }
        }
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
        public int PersonalId
        {
            get
            {
                return _personalId;
            }
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");

                _personalId=value;
            }
        }
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

        //Constructor
        public Member(string firstName, string lastName, int personalId, int memberId)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalId = personalId;
            MemberId = memberId;
        }
    }
}