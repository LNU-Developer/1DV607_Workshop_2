using System;
using Controller;

namespace workshop_2
{
 class MainMenu
{
        public void startProgram()
        {
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

            string input = Console.ReadLine();
            if(!InputHandler.isCorrectMenuInput(input, 0, 10))
            {
                handleInput(11);
            }
            else
            {
                handleInput(Int32.Parse(input));
            }
        }

         public void handleInput(int input)
         {
             MemberRegister Register = new MemberRegister();
             SubMenu SubMenu = new SubMenu(Register);
             switch(input)
             {
                case 1:
                    Console.Clear();
                    SubMenu.registerMember();
                    startProgram();
                break;
                case 2:
                    Console.Clear();
                    SubMenu.deleteMember();
                    startProgram();
                break;
                case 3:
                    Console.Clear();
                    handleInput(SubMenu.showMemberList());
                    startProgram();
                break;
                case 4:
                    Console.Clear();
                    SubMenu.updateMember();
                    startProgram();
                break;
                case 5:
                    Console.Clear();
                    SubMenu.addBoatToMember();
                    startProgram();
                break;
                case 6:
                    Console.Clear();
                    SubMenu.deleteBoatFromMember();
                    startProgram();
                break;
                case 7:
                    Console.Clear();
                    SubMenu.updateBoat();
                    startProgram();
                break;
                    case 8:
                    Console.Clear();
                    SubMenu.showMemberInfo();
                    startProgram();
                break;
                case 9:
                    Console.Clear();
                    SubMenu.showCompactList();
                    startProgram();
                break;
                case 10:
                    Console.Clear();
                    SubMenu.showVerboseList();
                    startProgram();
                break;
                case 0:
                    Environment.Exit(0);
                break;
                default:
                    startProgram();
                break;
            }
        }
    }
}
