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

        public MenuInput GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return MenuInput.RegisterMember;
                case '2':
                    return MenuInput.DeleteMember;
                case '3':
                    return MenuInput.ShowMemberList;
                case '4':
                    return MenuInput.UpdateMemberInformation;
                case '5':
                    return MenuInput.AddBoat;
                case '6':
                    return MenuInput.RemoveBoat;
                case '7':
                    return MenuInput.UpdateBoat;
                case '8':
                    return MenuInput.ShowMemberInformation;
                case '0':
                    return MenuInput.Exit;
                default:
                    return MenuInput.Unknown;
            }
        }
    }
}
