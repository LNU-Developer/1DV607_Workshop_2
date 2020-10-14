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
    }
}