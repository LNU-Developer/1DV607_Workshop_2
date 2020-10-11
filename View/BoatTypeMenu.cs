using System;
using EnumBoatTypes;

namespace workshop_2
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

        public BoatTypes GetInput()
        {
            switch (System.Console.ReadKey().KeyChar)
            {
                case '1':
                    return BoatTypes.Kayak;
                case '2':
                    return BoatTypes.Motorsailer;
                case '3':
                    return BoatTypes.Sailboat;
                default:
                    return BoatTypes.Other;
            }
        }
    }
}