using System;
using System.Collections.Generic;
using Model;

namespace Controller
{
    class MemberRegister : Register
    {
        public IReadOnlyList<Member> Members
        {
            get
            {
                return database.fetchAllMembers().Result.AsReadOnly();
            }
        }

        public void addMember(string firstName, string lastName, string personalId)
        {
            if(!database.memberExist(personalId).Result)
            {
                if(!IsSwedishSsn(personalId)) throw new ArgumentOutOfRangeException( $"{nameof(personalId)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = generateId()
                };
                database.addMember(newMember).Wait();
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(personalId)} already exists. Unable to register new member.");
            }
        }

        public Member getMemberBySsn(string id)
        {
            id = id.Replace("-", "");
            id = id.Replace("+", "");

            if (id.Length == 12)
                id = id.Substring(2, 10);

            if(database.memberExist(id).Result)
            {
                return database.fetchMemberBySsn(id).Result;
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(id)} member doesn't exists.");
            }
        }

        public Member getMemberByMemberId(int id)
        {
            if(database.memberIdExist(id).Result)
            {
                return database.fetchMemberById(id).Result;
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(id)} member doesn't exists.");
            }
        }

        public void deleteMemberBySsn(string id)
        {
            if(database.memberExist(id).Result)
            {
                database.removeMemberBySsn(id).Wait();
            }
        }

        public override void deleteById(int id)
        {
            if(database.memberIdExist(id).Result)
            {
                database.removeMemberById(id).Wait();
            }
        }

        public void updateMember(string firstName, string lastName, string personalId)
        {
            if(database.memberExist(personalId).Result)
            {
                if(!IsSwedishSsn(personalId)) throw new ArgumentOutOfRangeException( $"{nameof(personalId)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");

                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = getMemberBySsn(personalId).MemberId
                };
                database.addMember(newMember).Wait();
            }
        }

        public override int generateId()
        {
            Random a = new Random();

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(database.memberIdExist(newMemberId).Result)
    	        newMemberId = a.Next(0, 100000000);

            return newMemberId;
        }

        //Lunas Algorithm
        public bool IsSwedishSsn(string identity)
        {
            identity = identity.Replace("-", "");
            identity = identity.Replace("+", "");

            if (identity.Length == 12) {
                identity = identity.Substring(2);
            }

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