using System;
namespace View.input
{
    class WrongInput
    {
        public void NotCorrectPId()
        {
            Console.WriteLine("\nThis is not a correct personal number.");
        }

        public void MemberDoesNotExists()
        {
            Console.WriteLine("\nA member with this personal id doesn't exist in the register.");
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