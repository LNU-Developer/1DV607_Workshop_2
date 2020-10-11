using Model;
using View.member;
namespace Controller.member
{
    class MemberController
    {
        public void AddMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.InputSsn();

            string firstName = memberView.InputFirstName();
            string lastName = memberView.InputLastName();

            if(!InputHandler.IsCorrectInputOfSsn(pId, true))
            {
                AddMember(memberRegister, memberView);
            }
            else
            {
                if (pId.Length == 12) pId = pId.Substring(2);

                //TODO: Are these credentials correct: show credentials.
                memberRegister.AddMember(firstName, lastName, pId);

                if(InputHandler.DoesPIdExistInRegister(pId))
                {
                    memberView.PrintActionSuccess();
                }
                else
                {
                    memberView.PrintActionFail();
                }
            }
        }
        public void DeleteMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.InputSsn();

            if(!InputHandler.IsCorrectInputOfSsn(pId))
            {
                DeleteMember(memberRegister, memberView);
            }
            else
            {
                memberRegister.DeleteMemberBySsn(pId);
            }

            if(!InputHandler.DoesPIdExistInRegister(pId))
            {
                memberView.PrintActionSuccess();
            }
            else
            {
                memberView.PrintActionFail();
            }
        }
        public void UpdateMember(MemberRegister memberRegister, MemberView memberView)
        {
            string pId = memberView.InputSsn();
            if(!InputHandler.DoesPIdExistInRegister(pId))
            {
                memberView.PrintSsnNotExisting();
                UpdateMember(memberRegister, memberView);
            }
            string firstName = memberView.InputFirstName();
            string lastName = memberView.InputLastName();

            memberRegister.UpdateMember(firstName, lastName, pId);
            memberView.PrintActionSuccess();

        }
        public void ShowCompactMemberList(MemberRegister memberRegister, MemberView memberView)
        {
            foreach (Member member in memberRegister.Members)
            {
                BoatRegister boatRegister = new BoatRegister(member.PersonalId);
                memberView.PrintMember(member.FullName, member.MemberId.ToString());
                memberView.PrintBoatTotal(boatRegister.Boats.Count);
            }
            memberView.PrintEndOfInformation();
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
            string pId = memberView.InputSsn();

            if(!InputHandler.IsCorrectInputOfSsn(pId)) ShowMember(memberRegister, memberView);

            Member selectedMember =  memberRegister.GetMemberBySsn(pId);
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