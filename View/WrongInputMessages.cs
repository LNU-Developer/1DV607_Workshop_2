using System;

namespace View
{
    abstract class WrongInputMessages
    {
        public void NotCorrectPId()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis is not a correct social security number.");
            Console.ResetColor();
        }
        public void PrintSsnNotExisting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis social security number was not found in the register.");
            Console.ResetColor();
        }
    }
}