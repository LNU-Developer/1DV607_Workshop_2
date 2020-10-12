using System;
using Enum.boat.type;
using View;

namespace View.member
{
    class MemberView : MainView
    {
        public string InputFirstName()
        {
            Console.WriteLine("\nPlease enter first name:");
            string input = Console.ReadLine();
            if(input.Length < 1)
            {
                Console.WriteLine("\nYou didn't enter a name. Please try again.");
            }
            return Console.ReadLine();
        }
        public string InputLastName()
        {
            Console.WriteLine("\nPlease enter last name:");
            string input = Console.ReadLine();
            if(input.Length < 1)
            {
                Console.WriteLine("\nYou didn't enter a name. Please try again.");
            }
            return Console.ReadLine();
        }
        public override void PrintActionFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSomething went wrong. Try again:");
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

        public void PrintMember(string fullName, string memberId, string ssn = "")
        {
            Console.WriteLine("\n═══════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Full name: " + fullName);
            Console.WriteLine("Member id: " + memberId);
            if(ssn != "")
            {
                Console.WriteLine("Personal id: " + ssn);
                Console.WriteLine("Boat information:");
            }
        }

        public void PrintBoatTotal(int total)
        {
            Console.WriteLine("Number of boats: " + total);
            Console.ResetColor();
            Console.WriteLine("═══════════════════════════════════════════");
        }
    }
}