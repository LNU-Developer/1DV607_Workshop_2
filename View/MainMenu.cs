using System;

namespace workshop_2
{
    static class MainMenu
    {

         public static string startProgram()
        {
            Console.WriteLine("Welcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (1-4):");
            Console.WriteLine("1. Register member");
            string choice = Console.ReadLine();
            return choice;
        }

         public static void readInput(string choice)
        {
            if(Int32.Parse(choice) == 1)
            {
                Console.WriteLine("You choose to register member");
            } else 
            {
                Console.WriteLine("Not a valid option");
            }
        }

    }
}
