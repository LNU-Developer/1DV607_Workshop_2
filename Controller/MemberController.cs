using Model;
using View.member;
using View;
namespace Controller.member
{
    class MemberController
    {
        private MemberView _memberView;
        private MemberRegister _memberRegister;
        private InputChecker _inputChecker;
        private MemberViewWrongInputMessages _memberViewWrongInputMessages;
        public void AddMember()
        {
            string pId = _memberView.InputSsn();

            if(!_inputChecker.IsCorrectInputOfSsn(pId, true))
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

                if(_inputChecker.DoesPIdExistInRegister(pId))
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

            if(!_inputChecker.IsCorrectInputOfSsn(pId))
            {
                DeleteMember();
            }
            else
            {
                _memberRegister.DeleteMemberBySsn(pId);
            }

            if(!_inputChecker.DoesPIdExistInRegister(pId))
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
            
            if(!_inputChecker.DoesPIdExistInRegister(pId))
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

            if(!_inputChecker.IsCorrectInputOfSsn(pId)) ShowMember();

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
            if(!_inputChecker.IsCorrectNameInput(name) && isFirstName)
            {
                name = _memberView.InputFirstName();
            }
            else if(!_inputChecker.IsCorrectNameInput(name) && !isFirstName)
            {
                name = _memberView.InputLastName();
            }
            return name;
        }

        public MemberController(MemberRegister memberRegister, InputChecker inputChecker)
        {
            _memberView = new MemberView();
             _memberViewWrongInputMessages = new MemberViewWrongInputMessages();
            _memberRegister = memberRegister;
            _inputChecker = inputChecker;  
        }
    }
}