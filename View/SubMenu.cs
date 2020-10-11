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
            Console.WriteLine("═══════════════════════════════════════════");
        }
        public Input GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return Input.ShowCompactMemberList;
                case '2':
                    return Input.ShowVerboseMemberList;
                default:
                    return Input.Unknown;
            }
        }
    }
}