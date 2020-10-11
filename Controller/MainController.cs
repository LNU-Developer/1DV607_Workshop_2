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
            MenuInput menuInput = menu.GetInput();
            switch(menuInput)
            {
                case MenuInput.RegisterMember:
                    memberController.AddMember(memberRegister, memberView);
                    break;
                case MenuInput.DeleteMember:
                    memberController.DeleteMember(memberRegister, memberView);
                    break;
                case MenuInput.ShowMemberList:
                    subMenu.DisplayMenu();
                    MenuInput subMenuInput = subMenu.GetInput();
                    if(subMenuInput == MenuInput.ShowCompactMemberList)
                    {
                        memberController.ShowCompactMemberList(memberRegister, memberView);
                    }
                    else if(subMenuInput == MenuInput.ShowVerboseMemberList)
                    {
                        memberController.ShowVerboseMemberList(memberRegister, memberView);
                    }
                    break;
                case MenuInput.UpdateMemberInformation:
                    memberController.UpdateMember(memberRegister, memberView);
                    break;
                case MenuInput.AddBoat:
                    boatController.AddBoat(memberRegister, boatView);
                    break;
                case MenuInput.RemoveBoat:
                    boatController.RemoveBoat(memberRegister, boatView);
                    break;
                case MenuInput.UpdateBoat:
                    boatController.UpdateBoat(memberRegister, boatView);
                    break;
                case MenuInput.ShowMemberInformation:
                    memberController.ShowMember(memberRegister, memberView);
                    break;
                }
            return menuInput != MenuInput.Exit;
        }
    }
}