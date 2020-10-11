using System;
namespace workshop_2
{
    class Menu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("═══════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nWelcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (0-8):\n");
            Console.ResetColor();
            Console.WriteLine("1. Register member");
            Console.WriteLine("2. Delete member");
            Console.WriteLine("3. Show member list");
            Console.WriteLine("4. Update member information");
            Console.WriteLine("5. Add boat to member");
            Console.WriteLine("6. Remove boat from member");
            Console.WriteLine("7. Update boat information");
            Console.WriteLine("8. Show information about member");
            Console.WriteLine("0. Exit program");
            Console.WriteLine("═══════════════════════════════════════════");
        }

        public Input GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return Input.RegisterMember;
                case '2':
                    return Input.DeleteMember;
                case '3':
                    return Input.ShowMemberList;
                case '4':
                    return Input.UpdateMemberInformation;
                case '5':
                    return Input.AddBoat;
                case '6':
                    return Input.RemoveBoat;
                case '7':
                    return Input.UpdateBoat;
                case '8':
                    return Input.ShowMemberInformation;
                case '0':
                    return Input.Exit;
                default:
                    return Input.Unknown;
            }
        }
    }
}
