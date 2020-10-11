using System;

namespace workshop_2
{
    class SubMenu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("═══════════════════════════════════════════");
            Console.WriteLine("Choose which type of list you want to view:");
            Console.WriteLine("1. Compact list");
            Console.WriteLine("2. Verbose list");
            Console.WriteLine("0. Go back");
            Console.WriteLine("═══════════════════════════════════════════");
        }
        public MenuInput GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return MenuInput.ShowCompactMemberList;
                case '2':
                    return MenuInput.ShowVerboseMemberList;
                default:
                    return MenuInput.Unknown;
            }
        }
    }
}