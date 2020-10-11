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
                Menu menu = new Menu();
                SubMenu subMenu = new SubMenu();
                MainController controller = new MainController();
                while (controller.Start(menu, subMenu));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
