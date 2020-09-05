using System;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Member one = new Member("Kalle", "Anka", 123456789, 1);
            Console.WriteLine(one.FullName);
        }
    }
}
