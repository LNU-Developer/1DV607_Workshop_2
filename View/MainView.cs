using System;
using Enum.boat.type;

namespace View
{
    abstract class MainView
    {
        public abstract void PrintActionFail();
        public void PrintActionSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Action completed successfully!");
            Console.ResetColor();
            Console.WriteLine("\n═══════════════════════════════════════════");
        }
        public string InputSsn()
        {
            Console.WriteLine("\nPlease enter a social security number (10 digits):");
            return Console.ReadLine();
        }
        public void PrintBoatInformation (int reference, BoatType type, double length, int id)
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