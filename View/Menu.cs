using System;
using View.input;
namespace View.menu
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

        public MenuChoice GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return MenuChoice.RegisterMember;
                case '2':
                    return MenuChoice.DeleteMember;
                case '3':
                    return MenuChoice.ShowMemberList;
                case '4':
                    return MenuChoice.UpdateMemberInformation;
                case '5':
                    return MenuChoice.AddBoat;
                case '6':
                    return MenuChoice.RemoveBoat;
                case '7':
                    return MenuChoice.UpdateBoat;
                case '8':
                    return MenuChoice.ShowMemberInformation;
                case '0':
                    return MenuChoice.Exit;
                default:
                    return MenuChoice.Unknown;
            }
        }
    }
}
