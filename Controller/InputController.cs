using System;
using System.Linq;
using Model;
using View.input;

namespace Controller

{
    class InputController
    {
        private MemberRegister Register = new MemberRegister();
        private SsnRegister SsnRegister = new SsnRegister();
        private WrongInput wrongInput = new WrongInput();

        public bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            if(!SsnRegister.ValidatePidInput(id))
            {
                wrongInput.NotCorrectPId();
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                wrongInput.PrintSsnNotExisting();
                return false;
            }
            else if(DoesPIdExistInRegister(id) && idExists)
            {
                wrongInput.MemberAlreadyExists();
                return false;
            }

            return true;
        }

        public bool DoesPIdExistInRegister(string pId)
        {
            foreach (Member member in Register.Members)
            {
                if(member.PersonalId == pId)
                {
                    return true;
                }
            }
            return false;
        }

        public int ConvertToInt(string input)
        {
            try
            {
                int number = Convert.ToInt32(input);
                if(number > 0)
                {
                   return number;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public double ConvertToDouble(string input)
        {
            try
            {
                double length = Convert.ToDouble(input);
                if(length > 0)
                {
                    return length;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    
        public bool isCorrectNameInput(string input)
        {

            if(input.Any(char.IsDigit))
            {
                wrongInput.NameHasNumber();
                return false;
            }
            else if(input.Length < 1)
            {
                wrongInput.NoName();
                return false;
            } else {
                return true;
            }
        }
    }
}