using System;

namespace View.boat
{
    class BoatView : MainView
    {
        public string InputBoatLength()
        {
            Console.WriteLine("\nPlease type in the length of the boat:");
            return Console.ReadLine();
        }
        public string InputBoatId()
        {
            Console.WriteLine("\nPlease enter the ID of the boat you want to select.");
            return Console.ReadLine();
        }
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
        }
        public override void PrintActionFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe boat doesn't exist.");
            Console.ResetColor();
        }
        public override void PrintActionSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Action completed successfully!");
            Console.ResetColor();
            Console.WriteLine("\n═══════════════════════════════════════════");
        }

        public void PrintSsnNotExisting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis social security number was not found in the register. Please try again!");
            Console.ResetColor();
        }
    }
}