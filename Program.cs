using System;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Member one = new Member("Musse", "Pigg", "9510101349", 1); //Wrong Social Security Number
            Console.WriteLine(one.FullName);
            Boat two = new Boat(BoatTypes.Sailboat, 1.0);
            Console.WriteLine(two.Type);
        }
    }
}
