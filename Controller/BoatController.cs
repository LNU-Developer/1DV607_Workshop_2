using Controller;
using EnumBoatTypes;
using Model;
namespace workshop_2
{
    class BoatController
    {
        public void AddBoat(MemberRegister memberRegister, BoatView boatView)
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            //TODO: Fetch by member id or SSN
            string pId = boatView.FetchSsn();

            if(!InputHandler.isCorrectInputOfSsn(pId)) AddBoat(memberRegister, boatView);

            BoatRegister boatRegister = new BoatRegister(memberRegister.getMemberBySsn(pId).PersonalId);
            boatTypeMenu.DisplayMenu();
            BoatTypes boatType = boatTypeMenu.GetInput();

            string lengthString = boatView.FetchBoatLength();
            if(InputHandler.convertToDouble(lengthString) == 0)
            {
                boatView.NotADoubleAboveZero();
                AddBoat(memberRegister, boatView);
            } else {
                boatRegister.addBoat(boatType, InputHandler.convertToDouble(lengthString));
                boatView.ActionSuccess();
            }
        }
        public void RemoveBoat(MemberRegister memberRegister, BoatView boatView)
        {
            //TODO: Fetch by member id or SSN

            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            string pId = boatView.FetchSsn();

            if(!InputHandler.isCorrectInputOfSsn(pId)) RemoveBoat(memberRegister, boatView);
            BoatRegister boatRegister = new BoatRegister(memberRegister.getMemberBySsn(pId).PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                boatView.NoBoatsFound();
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
            }

            string idString = boatView.FetchBoatId();
            if(InputHandler.convertToInt(idString) == 0)
            {
                boatView.NotADoubleAboveZero();
                RemoveBoat(memberRegister, boatView);
            } else {
                int id = InputHandler.convertToInt(idString);
                if(boatRegister.isBoat(id))
                {
                    boatRegister.deleteById(id);
                    boatView.ActionSuccess();
                }
                else
                {
                    boatView.ActionFail();
                }
            }
        }
        public void UpdateBoat(MemberRegister memberRegister, BoatView boatView)
        {
            //TODO: Fetch by member id or SSN
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            string pId = boatView.FetchSsn();

            if(!InputHandler.isCorrectInputOfSsn(pId)) UpdateBoat(memberRegister, boatView);

            Member selectedMember =  memberRegister.getMemberBySsn(pId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                boatView.NoBoatsFound();
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
            }

            string idString = boatView.FetchBoatId();

            if(InputHandler.convertToInt(idString) == 0)
            {
                boatView.NotAnIntAboveZero();
                UpdateBoat(memberRegister, boatView);
            }
            else
            {
                int id = InputHandler.convertToInt(idString);

                if(boatRegister.isBoat(id))
                {
                    boatTypeMenu.DisplayMenu();
                    BoatTypes boatType = boatTypeMenu.GetInput();

                    string lengthString = boatView.FetchBoatLength();
                    if(InputHandler.convertToDouble(lengthString) == 0)
                    {
                        //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                        boatView.NotADoubleAboveZero();
                        UpdateBoat(memberRegister, boatView);
                    }

                    boatRegister.updateBoat(id, boatType, InputHandler.convertToDouble(lengthString));
                    boatView.ActionSuccess();
                }
                else
                {
                    boatView.ActionFail();
                }
            }
        }
    }
}