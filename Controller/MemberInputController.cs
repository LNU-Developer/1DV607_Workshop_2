using Model;
using View.member;
using System.Linq;

namespace Controller.member
{
    /// <summary>
    ///  This class inherits from the MainInputController and handles and validates inputs related to the membercommands.
    /// </summary>
    class MemberInputController : MainInputController
    {
        private MemberView _memberView;
        private MemberViewWrongInputMessages _memberViewWrongInputMessages;

        /// <summary>
        /// Adds a member to the member register
        /// </summary>
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

                MemberRegister.AddMember(firstName, lastName, pId);

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

        /// <summary>
        /// Deletes a member to from the member register
        /// </summary>
        public void DeleteMember()
        {
            string pId = _memberView.InputSsn();

            if(!IsCorrectInputOfSsn(pId))
            {
                DeleteMember();
            }
            else
            {
                MemberRegister.DeleteMemberBySsn(pId);

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

        /// <summary>
        /// Updates a member on the member register
        /// </summary>
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
                MemberRegister.UpdateMember(firstName, lastName, pId);
                _memberView.PrintActionSuccess();
            }
        }

        /// <summary>
        /// Method that goes through the members in the member register and prints them in a compact manner.
        /// </summary>
        public void ShowCompactMemberList()
        {
            foreach (Member member in MemberRegister.Members)
            {
                _memberView.PrintMember(member.FirstName, member.LastName, member.MemberId.ToString());
                _memberView.PrintBoatTotal(member.BoatRegister.Boats.Count);
            }
            _memberView.PrintEndOfInformation();
        }

        /// <summary>
        /// Method that goes through the members in the member register and prints them in a verbose manner.
        /// </summary>
        public void ShowVerboseMemberList()
        {
            foreach (Member member in MemberRegister.Members)
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

        /// <summary>
        /// Method that fetches a specific member and shows their information.
        /// </summary>
        public void ShowMember()
        {
            string pId = _memberView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) ShowMember();

            Member selectedMember =  MemberRegister.GetMemberBySsn(pId);
            _memberView.PrintMember(selectedMember.FirstName, selectedMember.LastName, selectedMember.MemberId.ToString(), selectedMember.PersonalId);

            int count = 0;
            foreach (Boat boat in selectedMember.BoatRegister.Boats)
            {
                count += 1;
                _memberView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
            }
            _memberView.PrintEndOfInformation();
        }

        /// <summary>
        /// Check if the input is correct in a social security number
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="id">the social security number in string format.</param>
        /// <param name="idExists">Flag to show if the social security number exists in the register or not.</param>
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

        /// <summary>
        /// Validates if a name is in the correct format, and prompts the user to enter a new name if it isn't.
        /// </summary>
        /// <returns>
        /// A string with a validated name
        /// </returns>
        /// <param name="name">the name in string format.</param>
        /// <param name="isFirstName">Flag to show if it's the first name or last name being inputted.</param>
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

        /// <summary>
        /// Validates if a name is in the correct format, and shows an error message if it isn't
        /// </summary>
        /// <returns>
        /// True or false
        /// </returns>
        /// <param name="input">the name in string format.</param>
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

        public MemberInputController(MemberRegister memberRegister) : base (memberRegister)
        {
            _memberView = new MemberView();
            _memberViewWrongInputMessages = new MemberViewWrongInputMessages();
        }
    }
}