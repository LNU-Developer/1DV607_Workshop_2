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
                MemberController memberController = new MemberController();
                BoatController boatController = new BoatController();
                MainController controller = new MainController();
                while (controller.Start(menu, subMenu, memberController, boatController));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
