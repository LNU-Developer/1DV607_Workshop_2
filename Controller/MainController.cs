using View.menu;
using View.member;
using View.boat;
using View.input;
using Controller.member;
using Controller.boat;
using Model;

namespace Controller
{
    class MainController
    {
        public bool Start(Menu menu, SubMenu subMenu)
        {
            MemberRegister memberRegister = new MemberRegister();
            MemberController memberController = new MemberController();
            BoatController boatController = new BoatController();
            MemberView memberView = new MemberView();
            BoatView boatView = new BoatView();
            menu.DisplayMenu();
            MenuChoice menuChoice = menu.GetInput();
            switch(menuChoice)
            {
                case MenuChoice.RegisterMember:
                    memberController.AddMember(memberRegister, memberView);
                    break;
                case MenuChoice.DeleteMember:
                    memberController.DeleteMember(memberRegister, memberView);
                    break;
                case MenuChoice.ShowMemberList:
                    subMenu.DisplayMenu();
                    MenuChoice subMenuChoice = subMenu.GetInput();
                    if(subMenuChoice == MenuChoice.ShowCompactMemberList)
                    {
                        memberController.ShowCompactMemberList(memberRegister, memberView);
                    }
                    else if(subMenuChoice == MenuChoice.ShowVerboseMemberList)
                    {
                        memberController.ShowVerboseMemberList(memberRegister, memberView);
                    }
                    break;
                case MenuChoice.UpdateMemberInformation:
                    memberController.UpdateMember(memberRegister, memberView);
                    break;
                case MenuChoice.AddBoat:
                    boatController.AddBoat(memberRegister, boatView);
                    break;
                case MenuChoice.RemoveBoat:
                    boatController.RemoveBoat(memberRegister, boatView);
                    break;
                case MenuChoice.UpdateBoat:
                    boatController.UpdateBoat(memberRegister, boatView);
                    break;
                case MenuChoice.ShowMemberInformation:
                    memberController.ShowMember(memberRegister, memberView);
                    break;
                }
            return menuChoice != MenuChoice.Exit;
        }
    }
}