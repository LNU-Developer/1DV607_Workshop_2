using System;
namespace View.input
{
    class WrongInput
    {
        public void NotCorrectPId()
        {
            Console.WriteLine("\nThis is not a correct personal number.");
        }

        public void PrintSsnNotExisting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis social security number was not found in the register. Please try again!");
            Console.ResetColor();
        }

        public void MemberAlreadyExists()
        {
            Console.WriteLine("\nA member with this personal number already exists in the register.");

        }

        public void NoName()
        {
            Console.WriteLine("\nYou didn't enter a name. Please try again.");
        }
    }
}