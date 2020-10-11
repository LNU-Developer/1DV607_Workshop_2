using System;
using EnumBoatTypes;

namespace workshop_2
{
    class MemberView
    {
        public string FetchFirstName()
        {
            Console.WriteLine("\nPlease enter first name:");
            return Console.ReadLine();
        }
        public string FetchLastName()
        {
            Console.WriteLine("\nPlease enter last name:");
            return Console.ReadLine();
        }
        public string FetchSsn()
        {
            Console.WriteLine("\nPlease enter a social security number (10 digits):");
            return Console.ReadLine();
        }
        public void ActionFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSomething went wrong. Try again:");
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

        public void PrintBoatInformation (int reference, BoatTypes type, double length, int id)
        {
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