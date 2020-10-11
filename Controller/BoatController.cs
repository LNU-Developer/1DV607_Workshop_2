using Enum.boat.type;
using Model;
using View.menu;
using View.boat;
namespace Controller.boat
{
    class BoatController
    {
        public void AddBoat(MemberRegister memberRegister, BoatView boatView)
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            //TODO: Fetch by member id or SSN
            string pId = boatView.InputSsn();

            if(!InputHandler.IsCorrectInputOfSsn(pId)) AddBoat(memberRegister, boatView);

            BoatRegister boatRegister = new BoatRegister(memberRegister.GetMemberBySsn(pId).PersonalId);
            boatTypeMenu.DisplayMenu();
            BoatType boatType = boatTypeMenu.GetInput();

            string lengthString = boatView.InputBoatLength();
            if(InputHandler.ConvertToDouble(lengthString) == 0)
            {
                boatView.PrintNotADoubleAboveZero();
                AddBoat(memberRegister, boatView);
            } else {
                boatRegister.AddBoat(boatType, InputHandler.ConvertToDouble(lengthString));
                boatView.PrintActionSuccess();
            }
        }
        public void RemoveBoat(MemberRegister memberRegister, BoatView boatView)
        {
            //TODO: Fetch by member id or SSN
            string pId = boatView.InputSsn();

            if(!InputHandler.IsCorrectInputOfSsn(pId)) RemoveBoat(memberRegister, boatView);
            BoatRegister boatRegister = new BoatRegister(memberRegister.GetMemberBySsn(pId).PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                boatView.PrintNoBoatsFound();
                return;
            }
            else
            {
                int count = 0;
                foreach (Boat boat in boatRegister.Boats)
                {
                    count += 1;
                    boatView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
                }
                boatView.PrintEndOfInformation();
            }

            string idString = boatView.InputBoatId();
            if(InputHandler.ConvertToInt(idString) == 0)
            {
                boatView.PrintNotADoubleAboveZero();
                RemoveBoat(memberRegister, boatView);
            } else {
                int id = InputHandler.ConvertToInt(idString);
                if(boatRegister.IsBoat(id))
                {
                    boatRegister.DeleteById(id);
                    boatView.PrintActionSuccess();
                }
                else
                {
                    boatView.PrintActionFail();
                }
            }
        }
        public void UpdateBoat(MemberRegister memberRegister, BoatView boatView)
        {
            //TODO: Fetch by member id or SSN
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            string pId = boatView.InputSsn();

            if(!InputHandler.IsCorrectInputOfSsn(pId)) UpdateBoat(memberRegister, boatView);

            Member selectedMember =  memberRegister.GetMemberBySsn(pId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                boatView.PrintNoBoatsFound();
                return;
            }
            else
            {
                int count = 0;
                foreach (Boat boat in boatRegister.Boats)
                {
                    count += 1;
                    boatView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
                }
                boatView.PrintEndOfInformation();
            }

            string idString = boatView.InputBoatId();

            if(InputHandler.ConvertToInt(idString) == 0)
            {
                boatView.PrintNotAnIntAboveZero();
                UpdateBoat(memberRegister, boatView);
            }
            else
            {
                int id = InputHandler.ConvertToInt(idString);

                if(boatRegister.IsBoat(id))
                {
                    boatTypeMenu.DisplayMenu();
                    BoatType boatType = boatTypeMenu.GetInput();

                    string lengthString = boatView.InputBoatLength();
                    if(InputHandler.ConvertToDouble(lengthString) == 0)
                    {
                        //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                        boatView.PrintNotADoubleAboveZero();
                        UpdateBoat(memberRegister, boatView);
                    }

                    boatRegister.UpdateBoat(id, boatType, InputHandler.ConvertToDouble(lengthString));
                    boatView.PrintActionSuccess();
                }
                else
                {
                    boatView.PrintActionFail();
                }
            }
        }
    }
}