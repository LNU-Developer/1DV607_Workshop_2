using System;
using System.Collections.Generic;

namespace Model
{
    class MemberRegister : Register
    {

        private SsnRegister SsnRegister = new SsnRegister();
        public IReadOnlyList<Member> Members
        {
            get
            {
                return database.FetchAllMembers().Result.AsReadOnly();
            }
        }

        public void AddMember(string firstName, string lastName, string personalId)
        {
            if(!database.MemberExist(personalId).Result)
            {
                if(!SsnRegister.IsSwedishSsn(personalId)) throw new ArgumentOutOfRangeException( $"{nameof(personalId)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GenerateId()
                };
                database.AddMember(newMember).Wait();
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(personalId)} already exists. Unable to register new member.");
            }
        }

        public Member GetMemberBySsn(string id)
        {
            id = id.Replace("-", "");
            id = id.Replace("+", "");

            if (id.Length == 12)
                id = id.Substring(2, 10);

            if(database.MemberExist(id).Result)
            {
                return database.FetchMemberBySsn(id).Result;
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(id)} member doesn't exists.");
            }
        }

        public Member GetMemberByMemberId(int id)
        {
            if(database.MemberIdExist(id).Result)
            {
                return database.FetchMemberById(id).Result;
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(id)} member doesn't exists.");
            }
        }

        public void DeleteMemberBySsn(string id)
        {
            if(database.MemberExist(id).Result)
            {
                database.RemoveMemberBySsn(id).Wait();
            }
        }

        public override void DeleteById(int id)
        {
            if(database.MemberIdExist(id).Result)
            {
                database.RemoveMemberById(id).Wait();
            }
        }

        public void UpdateMember(string firstName, string lastName, string personalId)
        {
            if(database.MemberExist(personalId).Result)
            {
                if(!SsnRegister.IsSwedishSsn(personalId)) throw new ArgumentOutOfRangeException( $"{nameof(personalId)} not a valid social security number. Please use the format xxYYMMDD-NNNN, xxYYMMDD+NNNN, YYMMDD-NNNN, YYMMDD-NNNN or YYMMDDNNN");

                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GetMemberBySsn(personalId).MemberId
                };
                database.AddMember(newMember).Wait();
            }
        }

        public override int GenerateId()
        {
            Random a = new Random();

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(database.MemberIdExist(newMemberId).Result)
    	        newMemberId = a.Next(0, 100000000);

            return newMemberId;
        }
    }
}