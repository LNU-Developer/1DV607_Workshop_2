using System;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            DotNetEnv.Env.Load();

            MainMenu menu = new MainMenu();
            menu.startProgram();

            Console.WriteLine("-------------------------------------");

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
