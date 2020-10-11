using System;
using Model;

namespace Controller

{
    static class InputHandler
    {
        public static bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            MemberRegister Register = new MemberRegister();

            if(!Register.IsSwedishSsn(id))
            {
                Console.WriteLine("\nThis is not a correct personal number.");
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                Console.WriteLine("\nA member with this personal id doesn't exist in the register.");
                return false;
            }
            else if(DoesPIdExistInRegister(id) && idExists)
            {
                Console.WriteLine("\nA member with this personal number already exists in the register.");
                return false;
            }

            return true;
        }

        public static bool DoesPIdExistInRegister(string pId)
        {
            MemberRegister Register = new MemberRegister();

            int count = 0;
            foreach (Member member in Register.Members)
            {
                if(member.PersonalId == pId)
                {
                    count++;
                }
            }

            if(count == 1) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int ConvertToInt(string input)
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

        public static double ConvertToDouble(string input)
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