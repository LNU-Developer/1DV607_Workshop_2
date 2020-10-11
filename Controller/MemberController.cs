using Controller;
using Model;
namespace workshop_2
{
    class MemberController
    {
        public void AddMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.FetchSsn();

            string firstName = memberView.FetchFirstName();
            string lastName = memberView.FetchLastName();

            if(!InputHandler.isCorrectInputOfSsn(pId, true))
            {
                AddMember(memberRegister, memberView);
            }
            else
            {
                if (pId.Length == 12) pId = pId.Substring(2);

                //TODO: Are these credentials correct: show credentials.
                memberRegister.addMember(firstName, lastName, pId);

                if(InputHandler.doesPIdExistInRegister(pId))
                {
                    memberView.ActionSuccess();
                }
                else
                {
                    memberView.ActionFail();
                }
            }
        }
        public void DeleteMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.FetchSsn();

            if(!InputHandler.isCorrectInputOfSsn(pId))
            {
                DeleteMember(memberRegister, memberView);
            }
            else
            {
                memberRegister.deleteMemberBySsn(pId);
            }

            if(!InputHandler.doesPIdExistInRegister(pId))
            {
                memberView.ActionSuccess();
            }
            else
            {
                memberView.ActionFail();
            }
        }
        public void UpdateMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.FetchSsn();
            if(!InputHandler.doesPIdExistInRegister(pId))
            {
                memberView.SsnNotExisting();
                UpdateMember(memberRegister, memberView);
            }
            string firstName = memberView.FetchFirstName();
            string lastName = memberView.FetchLastName();

            memberRegister.updateMember(firstName, lastName, pId);
            memberView.ActionSuccess();

        }
        public void ShowCompactMemberList(MemberRegister memberRegister, MemberView memberView)
        {
            foreach (Member member in memberRegister.Members)
            {
            BoatRegister boatRegister = new BoatRegister(member.PersonalId);
            memberView.PrintMember(member.FullName, member.MemberId.ToString());
            memberView.PrintBoatTotal(boatRegister.Boats.Count);
            }
        }
        public void ShowVerboseMemberList(MemberRegister memberRegister, MemberView memberView)
        {
            foreach (Member member in memberRegister.Members)
            {
               BoatRegister boatRegister = new BoatRegister(member.PersonalId);
               memberView.PrintMember(member.FullName, member.MemberId.ToString(), member.PersonalId);

               int count = 0;
               foreach (Boat boat in boatRegister.Boats)
               {
                   count += 1;
                   memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
               }
            }
            memberView.PrintEndOfInformation();
        }
        public void ShowMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.FetchSsn();

            if(!InputHandler.isCorrectInputOfSsn(pId)) ShowMember(memberRegister, memberView);

            Member selectedMember =  memberRegister.getMemberBySsn(pId);
            memberView.PrintMember(selectedMember.FullName, selectedMember.MemberId.ToString(), selectedMember.PersonalId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            int count = 0;
            foreach (Boat boat in boatRegister.Boats)
            {
                count += 1;
                memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
            }
            memberView.PrintEndOfInformation();
        }
    }
}