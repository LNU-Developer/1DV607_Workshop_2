using System;
using Enum.boat.type;

namespace View.menu
{
    class BoatTypeMenu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\nPlease pick from the selected boat types:");
            Console.WriteLine("1. Sailboat");
            Console.WriteLine("2. Motorsailer");
            Console.WriteLine("3. Kayak");
            Console.WriteLine("4. Other");
        }
        public BoatType GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return BoatType.Sailboat;
                case '2':
                    return BoatType.Motorsailer;
                case '3':
                    return BoatType.Kayak;
                default:
                    return BoatType.Other;
            }
        }
    }
}