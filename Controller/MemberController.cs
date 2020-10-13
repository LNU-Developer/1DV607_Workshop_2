using Model;
using View.member;
using System.Linq;

namespace Controller.member
{
    class MemberController : InputController
    {
        private MemberView _memberView;
        private MemberRegister _memberRegister;
        private MemberViewWrongInputMessages _memberViewWrongInputMessages;
        public void AddMember()
        {
            string pId = _memberView.InputSsn();

            if(!IsCorrectInputOfSsn(pId, true))
            {
                AddMember();
            }
            else
            {
                if (pId.Length == 12) pId = pId.Substring(2);

                string firstName = ValidateName(_memberView.InputFirstName());

                string lastName = ValidateName(_memberView.InputLastName(), false);

                //TODO: Are these credentials correct: show credentials.
                _memberRegister.AddMember(firstName, lastName, pId);

                if(DoesPIdExistInRegister(pId))
                {
                    _memberView.PrintActionSuccess();
                }
                else
                {
                    _memberView.PrintActionFail();
                }
            }
        }
        public void DeleteMember()
        {
            string pId = _memberView.InputSsn();

            if(!IsCorrectInputOfSsn(pId))
            {
                DeleteMember();
            }
            else
            {
                _memberRegister.DeleteMemberBySsn(pId);
            }

            if(!DoesPIdExistInRegister(pId))
            {
                _memberView.PrintActionSuccess();
            }
            else
            {
                _memberView.PrintActionFail();
            }
        }
        public void UpdateMember()
        {
            string pId = _memberView.InputSsn();
            
            if(!DoesPIdExistInRegister(pId))
            {
                _memberViewWrongInputMessages.PrintSsnNotExisting();
                UpdateMember();
            }
            else
            {
                string firstName = ValidateName(_memberView.InputFirstName());
                string lastName = ValidateName(_memberView.InputLastName(), false);
                _memberRegister.UpdateMember(firstName, lastName, pId);
                _memberView.PrintActionSuccess();
            }
        }
        public void ShowCompactMemberList()
        {
            foreach (Member member in _memberRegister.Members)
            {
                BoatRegister boatRegister = new BoatRegister(member.PersonalId);
                _memberView.PrintMember(member.FirstName, member.LastName, member.MemberId.ToString());
                _memberView.PrintBoatTotal(boatRegister.Boats.Count);
            }
            _memberView.PrintEndOfInformation();
        }
        public void ShowVerboseMemberList()
        {
            foreach (Member member in _memberRegister.Members)
            {
               BoatRegister boatRegister = new BoatRegister(member.PersonalId);
               _memberView.PrintMember(member.FirstName, member.LastName, member.MemberId.ToString(), member.PersonalId);

               int count = 0;
               foreach (Boat boat in boatRegister.Boats)
               {
                   count += 1;
                   _memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
               }
            }
            _memberView.PrintEndOfInformation();
        }
        public void ShowMember()
        {
            string pId = _memberView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) ShowMember();

            Member selectedMember =  _memberRegister.GetMemberBySsn(pId);
            _memberView.PrintMember(selectedMember.FirstName, selectedMember.LastName, selectedMember.MemberId.ToString(), selectedMember.PersonalId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            int count = 0;
            foreach (Boat boat in boatRegister.Boats)
            {
                count += 1;
                _memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
            }
            _memberView.PrintEndOfInformation();
        }

        private string ValidateName(string name, bool isFirstName = true)
        {
            if(!IsCorrectNameInput(name) && isFirstName)
            {
                name = _memberView.InputFirstName();
            }
            else if(!IsCorrectNameInput(name) && !isFirstName)
            {
                name = _memberView.InputLastName();
            }
            return name;
        }

        private bool IsCorrectNameInput(string input)
        {
            if(input.Any(char.IsDigit) || input.Length < 1)
            {
                _memberViewWrongInputMessages.NotCorrectName();
                return false;
            }
            else {
                return true;
            }
        }

        public MemberController(MemberRegister memberRegister)
        {
            _memberView = new MemberView();
             _memberViewWrongInputMessages = new MemberViewWrongInputMessages();
            _memberRegister = memberRegister;
        }
    }
}