using System;
using System.Collections.Generic;

namespace Model
{
    class MemberRegister : Register
    {
        public IReadOnlyList<Member> Members
        {
            get { return Database.FetchAllMembers().Result.AsReadOnly(); }
        }
        public void AddMember(string firstName, string lastName, string personalId)
        {
            if(!Database.MemberExist(personalId).Result)
            {
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GenerateId()
                };
                Database.AddMember(newMember).Wait();
            }
            else
            {
                throw new ArgumentException($"{nameof(personalId)} already exists. Unable to register new member.");
            }
        }
        public Member GetMemberBySsn(string id)
        {
            id = id.Replace("-", "");
            id = id.Replace("+", "");

            if (id.Length == 12)
                id = id.Substring(2, 10);

            if(Database.MemberExist(id).Result)
            {
                return Database.FetchMemberBySsn(id).Result;
            }
            else
            {
                throw new ArgumentException($"{nameof(id)} member doesn't exists.");
            }
        }
        public Member GetMemberByMemberId(int id)
        {
            if(Database.MemberIdExist(id).Result)
            {
                return Database.FetchMemberById(id).Result;
            }
            else
            {
                throw new ArgumentException($"{nameof(id)} member doesn't exists.");
            }
        }
        public void DeleteMemberBySsn(string id)
        {
            if(Database.MemberExist(id).Result)
            {
                Database.RemoveMemberBySsn(id).Wait();
            }
        }
        public override void DeleteById(int id)
        {
            if(Database.MemberIdExist(id).Result)
            {
                Database.RemoveMemberById(id).Wait();
            }
        }
        public void UpdateMember(string firstName, string lastName, string personalId)
        {
            if(Database.MemberExist(personalId).Result)
            {
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GetMemberBySsn(personalId).MemberId
                };
                Database.AddMember(newMember).Wait();
            }
        }
        public override int GenerateId()
        {
            Random a = new Random();

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(Database.MemberIdExist(newMemberId).Result)
    	        newMemberId = a.Next(0, 100000000);

            return newMemberId;
        }
    }
}