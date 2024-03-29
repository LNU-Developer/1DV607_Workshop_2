using View.menu;
using View.input;
using Controller.member;
using Controller.boat;
using Model;

namespace Controller
{
    /// <summary>
    ///  This class is the main router/controller guiding the user to the correct controller though the options that the user selects.
    /// </summary>
    class MainController
    {
        public bool Start()
        {
            Menu menu = new Menu();
            SubMenu subMenu = new SubMenu();
            MemberRegister memberRegister = new MemberRegister();
            MemberInputController memberController = new MemberInputController(memberRegister);
            BoatInputController boatController = new BoatInputController(memberRegister);
            menu.DisplayMenu();
            MenuChoice menuChoice = menu.GetInput();
            switch(menuChoice)
            {
                case MenuChoice.RegisterMember:
                    memberController.AddMember();
                    break;
                case MenuChoice.DeleteMember:
                    memberController.DeleteMember();
                    break;
                case MenuChoice.ShowMemberList:
                    subMenu.DisplayMenu();
                    MenuChoice subMenuChoice = subMenu.GetInput();
                    if(subMenuChoice == MenuChoice.ShowCompactMemberList)
                    {
                        memberController.ShowCompactMemberList();
                    }
                    else if(subMenuChoice == MenuChoice.ShowVerboseMemberList)
                    {
                        memberController.ShowVerboseMemberList();
                    }
                    break;
                case MenuChoice.UpdateMemberInformation:
                    memberController.UpdateMember();
                    break;
                case MenuChoice.AddBoat:
                    boatController.AddBoat();
                    break;
                case MenuChoice.RemoveBoat:
                    boatController.RemoveBoat();
                    break;
                case MenuChoice.UpdateBoat:
                    boatController.UpdateBoat();
                    break;
                case MenuChoice.ShowMemberInformation:
                    memberController.ShowMember();
                    break;
                }
            return menuChoice != MenuChoice.Exit;
        }
    }
}