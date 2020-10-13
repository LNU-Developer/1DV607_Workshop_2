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
        public override void PrintActionFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe boat doesn't exist.");
            Console.ResetColor();
        }
    }
}