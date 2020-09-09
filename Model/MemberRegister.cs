using System;
using System.Collections.Generic;

namespace workshop_2
{
    class MemberRegister
    {
        private List<int> _memberIds = new List<int>();
        private List<Member> _members = new List<Member>();

        public void addMember(string firstName, string lastName, string personalId)
        {
            int index = _members.FindIndex(member => member.PersonalId == personalId);
            if(index != 0)
            {
                Member newMember = new Member(firstName, lastName, personalId, generateMemberId());
                _members.Add(newMember);
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

            Member foundMember = _members.Find(member => member.PersonalId == id);
            return foundMember;
        }

        public Member getMemberByMemberId(int id)
        {
            Member foundMember = _members.Find(member => member.MemberId == id);
            return foundMember;
        }

        public void deleteMemberBySsn(string id)
        {
            Member foundMember = getMemberBySsn(id);
            _memberIds.RemoveAll(memberId => memberId == foundMember.MemberId);
            _members.RemoveAll(member => member.PersonalId == id);

        }

        public void deleteMemberByMemberId(int id)
        {
            _memberIds.RemoveAll(memberId => memberId == id);
            _members.RemoveAll(member => member.MemberId == id);
        }

        public void updateMember(string firstName, string lastName, string personalId)
        {
            Member foundMember = getMemberBySsn(personalId);
            foundMember.FirstName=firstName;
            foundMember.LastName=lastName;
            foundMember.PersonalId=personalId;
        }

        private int generateMemberId()
        {
            Random a = new Random();

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(_memberIds.Contains(newMemberId))
    	        newMemberId = a.Next(0, 100000000);

            _memberIds.Add(newMemberId);

            return newMemberId;
        }
    }
}