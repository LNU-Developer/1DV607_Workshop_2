using System;
using EnumBoatTypes;

namespace workshop_2
{
    class BoatView
    {
        public string FetchSsn()
        {
            Console.WriteLine("\nPlease enter a social security number (10 digits):");
            return Console.ReadLine();
        }

        public string FetchBoatLength()
        {
            Console.WriteLine("\nPlease type in the length of the boat:");
            return Console.ReadLine();
        }
        public string FetchBoatId()
        {
            Console.WriteLine("\nPlease enter the ID of the boat you want to select.");
            return Console.ReadLine();
        }
        public void NotADoubleAboveZero()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input provided. Please enter a decimal number above zero.");
            Console.ResetColor();
        }
        public void NotAnIntAboveZero()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input provided. Please enter a number above zero.");
            Console.ResetColor();
        }

        public void NoBoatsFound()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This member has no boats in register.");
        }
        public void ActionFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe boat doesn't exist.");
            Console.ResetColor();
        }
        public void ActionSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Action completed successfully!");
            Console.ResetColor();
            Console.WriteLine("\n═══════════════════════════════════════════");
        }

        public void SsnNotExisting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThis social security number was not found in the register. Please try again!");
            Console.ResetColor();
        }

        public void PrintBoatInformation (int reference, BoatTypes type, double length, int id)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine();
            Console.WriteLine(reference + ". Boat type: " + type);
            Console.WriteLine("   Boat length: " + length);
            Console.WriteLine("   Boat id: " + id);
            Console.WriteLine("__________");
        }

        public void PrintEndOfInformation()
        {
            Console.WriteLine("END OF INFORMATION");
            Console.ResetColor();
            Console.WriteLine("═══════════════════════════════════════════");
        }
    }
}