using Model;
using View.member;
using System.Linq;

namespace Controller.member
{
    class MemberInputController : MainInputController
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

                if(!DoesPIdExistInRegister(pId))
                {
                    _memberView.PrintActionSuccess();
                }
                else
                {
                    _memberView.PrintActionFail();
                }
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
                _memberView.PrintMember(member.FirstName, member.LastName, member.MemberId.ToString());
                _memberView.PrintBoatTotal(member.BoatRegister.Boats.Count);
            }
            _memberView.PrintEndOfInformation();
        }
        public void ShowVerboseMemberList()
        {
            foreach (Member member in _memberRegister.Members)
            {
               _memberView.PrintMember(member.FirstName, member.LastName, member.MemberId.ToString(), member.PersonalId);

               int count = 0;
               foreach (Boat boat in member.BoatRegister.Boats)
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

            int count = 0;
            foreach (Boat boat in selectedMember.BoatRegister.Boats)
            {
                count += 1;
                _memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
            }
            _memberView.PrintEndOfInformation();
        }
        public override bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            if(!ValidatePidInput(id))
            {
                _memberViewWrongInputMessages.NotCorrectPId();
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                _memberViewWrongInputMessages.PrintSsnNotExisting();
                return false;
            }
            else if(DoesPIdExistInRegister(id) && idExists)
            {
                _memberViewWrongInputMessages.MemberAlreadyExists();
                return false;
            }

            return true;
        }
        private string ValidateName(string name, bool isFirstName = true)
        {
            if(!IsCorrectNameInput(name) && isFirstName)
            {
                name = ValidateName(_memberView.InputFirstName());
            }
            else if(!IsCorrectNameInput(name) && !isFirstName)
            {
                name = ValidateName(_memberView.InputLastName());
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
        private bool DoesPIdExistInRegister(string pId)
        {
            foreach (Member member in _memberRegister.Members)
            {
                if(member.PersonalId == pId)
                {
                    return true;
                }
            }
            return false;
        }
        public MemberInputController(MemberRegister memberRegister)
        {
            _memberView = new MemberView();
            _memberViewWrongInputMessages = new MemberViewWrongInputMessages();
            _memberRegister = memberRegister;
        }
    }
}