using System;

namespace View.boat
{
    class BoatViewWrongInputMessages : WrongInputMessages
    {
        public void PrintNotADoubleAboveZero()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input provided. Please enter a decimal number above zero.");
            Console.ResetColor();
        }
        public void PrintNotAnIntAboveZero()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input provided. Please enter a number above zero.");
            Console.ResetColor();
        }

        public void PrintNoBoatsFound()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This member has no boats in register.");
            Console.ResetColor();
        }
    }
}