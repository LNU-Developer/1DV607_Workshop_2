using View.menu;

namespace Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DotNetEnv.Env.Load();
                MainController controller = new MainController();
                while (controller.Start(new Menu(), new SubMenu()));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
