using System;
using Model;
using View.input;

namespace Controller

{
    class InputController
    {
        private MemberRegister Register = new MemberRegister();
        private SsnRegister SsnRegister = new SsnRegister();

        public bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            WrongInput wrongInput = new WrongInput();
            if(!SsnRegister.validatePidInput(id))
            {
                wrongInput.NotCorrectPId();
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                 wrongInput.MemberDoesNotExists();
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
    }
}