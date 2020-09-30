using System;
using Model;
using Controller;

namespace workshop_2

{
    static class InputHandler
    {
        public static bool isCorrectMenuInput(string input , int minValue, int maxValue)
        {
            try
            {
                if(Int32.Parse(input) >= minValue && Int32.Parse(input) <= maxValue)
                {
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nWrong input provided. Please pick a number from the list");
                    return false;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("\nWrong input provided. Please pick a number from the list");
                return false;
            }
        }

        public static bool isCorrectInputOfSsn (string id, bool idExists = false)
        {
            MemberRegister Register = new MemberRegister();

            if(!Register.IsSwedishSsn(id))
            {
                Console.WriteLine("\nThis is not a correct personal number.");
                return false;
            }

            if(!doesPIdExistInRegister(id) && !idExists)
            {
                Console.WriteLine("\nA member with this personal id doesn't exist in the register.");
                return false;
            }
            else if(doesPIdExistInRegister(id) && idExists)
            {
                Console.WriteLine("\nA member with this personal number already exists in the register.");
                return false;
            }

            return true;
        }

        public static bool doesPIdExistInRegister(string pId)
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

        public static int convertToInt(string input)
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

        public static double convertToDouble(string input)
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