using System;
namespace View.input
{
    class WrongInput
    {
        public void NotCorrectPId()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis is not a social security number.");
            Console.ResetColor();
        }

        public void PrintSsnNotExisting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis social security number was not found in the register. Please try again!");
            Console.ResetColor();
        }

        public void MemberAlreadyExists()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nA member with this personal number already exists in the register.");
            Console.ResetColor();
        }

        public void NoName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou didn't enter a name. Please try again.");
            Console.ResetColor();
        }

        public void NameHasNumber()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPlease don't put a number in your name.");
            Console.ResetColor();
        }
    }
}