namespace workshop_2
{
    class MainController
    {
        public bool Start(Menu menu, SubMenu subMenu)
        {
            menu.DisplayMenu();

            Input menuInput = menu.GetInput();

            switch(menuInput)
            {
                case Input.RegisterMember:
                    break;
                case Input.DeleteMember:
                    break;
                case Input.ShowMemberList:
                subMenu.DisplayMenu();
                Input subMenuInput = subMenu.GetInput();
                    break;
                case Input.UpdateMemberInformation:
                    break;
                case Input.AddBoat:
                    break;
                case Input.RemoveBoat:
                    break;
                case Input.UpdateBoat:
                    break;
                case Input.ShowMemberInformation:
                    break;
                }
            return menuInput != Input.Exit;
        }
    }
}