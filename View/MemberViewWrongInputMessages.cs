using System;

namespace View.member
{
    class MemberViewWrongInputMessages : WrongInputMessages
    {
        public void MemberAlreadyExists()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nA member with this personal number already exists in the register.");
            Console.ResetColor();
        }

        public void NotCorrectName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou didn't enter a correct name.");
            Console.ResetColor();
        }
    }
}