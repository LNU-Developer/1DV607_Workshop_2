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
            InputChecker inputChecker = new InputChecker(memberRegister);
            MemberView memberView = new MemberView();
            MemberController memberController = new MemberController(memberView, memberRegister);
            BoatView boatView = new BoatView();
            BoatController boatController = new BoatController(boatView, memberRegister, inputChecker);
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