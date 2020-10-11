using Controller;
namespace workshop_2
{
    class MainController
    {
        public bool Start(Menu menu, SubMenu subMenu, MemberController memberController , MemberRegister memberRegister, MemberView memberView, BoatController boatController)
        {
            menu.DisplayMenu();
            Input menuInput = menu.GetInput();
            switch(menuInput)
            {
                case Input.RegisterMember:
                    memberController.AddMember(memberRegister, memberView);
                    break;
                case Input.DeleteMember:
                    memberController.DeleteMember(memberRegister, memberView);
                    break;
                case Input.ShowMemberList:
                    subMenu.DisplayMenu();
                    Input subMenuInput = subMenu.GetInput();
                    if(subMenuInput == Input.ShowCompactMemberList)
                    {
                        memberController.ShowCompactMemberList(memberRegister, memberView);
                    }
                    else if(subMenuInput ==Input.ShowVerboseMemberList)
                    {
                        memberController.ShowVerboseMemberList(memberRegister, memberView);
                    }
                    break;
                case Input.UpdateMemberInformation:
                    memberController.UpdateMember(memberRegister, memberView);
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
                    memberController.ShowMember(memberRegister, memberView);
                    break;
                }
            return menuInput != Input.Exit;
        }
    }
}