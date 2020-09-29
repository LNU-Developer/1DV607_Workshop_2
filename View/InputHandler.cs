using System;

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