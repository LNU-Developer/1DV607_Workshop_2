using System;
using System.Collections.Generic;

namespace workshop_2
{
    class MemberRegister
    {
        Database database = new Database(Environment.GetEnvironmentVariable("projectId"), Environment.GetEnvironmentVariable("serviceAccountPath"));
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
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = generateMemberId()
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

        public void deleteMemberByMemberId(int id)
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

        private int generateMemberId()
        {
            Random a = new Random();

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(database.memberIdExist(newMemberId).Result)
    	        newMemberId = a.Next(0, 100000000);

            return newMemberId;
        }
    }
}