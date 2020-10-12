using System;
using View.input;

namespace View.member
{
    class MemberView : MainView
    {
        public string InputFirstName()
        {
            Console.WriteLine("\nPlease enter first name:");
            return Console.ReadLine();
        }
        public string InputLastName()
        {
            Console.WriteLine("\nPlease enter last name:");
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