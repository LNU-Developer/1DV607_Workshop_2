namespace workshop_2
{
    class MainController
    {
        public bool Start(Menu menu, SubMenu subMenu, MemberController memberController, BoatController boatController)
        {
            menu.DisplayMenu();
            Input menuInput = menu.GetInput();

            switch(menuInput)
            {
                case Input.RegisterMember:
                    memberController.AddMember();
                    break;
                case Input.DeleteMember:
                    memberController.DeleteMember();
                    break;
                case Input.ShowMemberList:
                    subMenu.DisplayMenu();
                    Input subMenuInput = subMenu.GetInput();
                    break;
                case Input.UpdateMemberInformation:
                    memberController.UpdateMember();
                    break;
                case Input.AddBoat:
                    boatController.AddBoat();
                    break;
                case Input.RemoveBoat:
                    boatController.RemoveBoat();
                    break;
                case Input.UpdateBoat:
                    boatController.UpdateBoat();
                    break;
                case Input.ShowMemberInformation:
                    memberController.ShowMember();
                    break;
                }
            return menuInput != Input.Exit;
        }
    }
}