using Enum.boat.type;
using Model;
using View.menu;
using View.boat;
using System;

namespace Controller.boat
{
    class BoatInputController : MainInputController
    {
        private BoatView _boatView;
        private BoatViewWrongInputMessages _boatViewWrongInputMessages;
        private MemberRegister _memberRegister;
        public void AddBoat()
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            
            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) AddBoat();

            BoatRegister boatRegister = new BoatRegister(_memberRegister.GetMemberBySsn(pId).PersonalId);
            boatTypeMenu.DisplayMenu();
            BoatType boatType = boatTypeMenu.GetInput();

            string lengthString = _boatView.InputBoatLength();
            if(ConvertToDouble(lengthString) == 0)
            {
                _boatViewWrongInputMessages.PrintNotADoubleAboveZero();
                AddBoat();
            } else {
                boatRegister.AddBoat(boatType, ConvertToDouble(lengthString));
                _boatView.PrintActionSuccess();
            }
        }
        public void RemoveBoat()
        {
            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) RemoveBoat();
            BoatRegister boatRegister = new BoatRegister(_memberRegister.GetMemberBySsn(pId).PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                _boatViewWrongInputMessages.PrintNoBoatsFound();
                return;
            }
            else
            {
                int count = 0;
                foreach (Boat boat in boatRegister.Boats)
                {
                    count += 1;
                    _boatView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
                }
                _boatView.PrintEndOfInformation();
            }

            string idString = _boatView.InputBoatId();

            if(ConvertToInt(idString) == 0)
            {
                _boatViewWrongInputMessages.PrintNotADoubleAboveZero();
                RemoveBoat();
            } else {
                int id = ConvertToInt(idString);
                if(boatRegister.IsBoat(id))
                {
                    boatRegister.DeleteById(id);
                    _boatView.PrintActionSuccess();
                }
                else
                {
                    _boatView.PrintActionFail();
                }
            }
        }
        public void UpdateBoat()
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) UpdateBoat();

            Member selectedMember =  _memberRegister.GetMemberBySsn(pId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                _boatViewWrongInputMessages.PrintNoBoatsFound();
                return;
            }
            else
            {
                int count = 0;
                foreach (Boat boat in boatRegister.Boats)
                {
                    count += 1;
                    _boatView.PrintBoatInformation(count, boat.Type, boat.Length, boat.BoatId);
                }
                _boatView.PrintEndOfInformation();
            }

            string idString = _boatView.InputBoatId();

            if(ConvertToInt(idString) == 0)
            {
                _boatViewWrongInputMessages.PrintNotAnIntAboveZero();
                UpdateBoat();
            }
            else
            {
                int id = ConvertToInt(idString);

                if(boatRegister.IsBoat(id))
                {
                    boatTypeMenu.DisplayMenu();
                    BoatType boatType = boatTypeMenu.GetInput();

                    string lengthString = _boatView.InputBoatLength();
                    if(ConvertToDouble(lengthString) == 0)
                    {
                        //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                        _boatViewWrongInputMessages.PrintNotADoubleAboveZero();
                        UpdateBoat();
                    }

                    boatRegister.UpdateBoat(id, boatType, ConvertToDouble(lengthString));
                    _boatView.PrintActionSuccess();
                }
                else
                {
                    _boatView.PrintActionFail();
                }
            }
        }

         private int ConvertToInt(string input)
        {
            try
            {
                int number = Convert.ToInt32(input);
                if(number > 0)
                {
                   return number;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        private double ConvertToDouble(string input)
        {
            try
            {
                double length = Convert.ToDouble(input);
                if(length > 0)
                {
                    return length;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public BoatInputController (MemberRegister memberRegister)
        {
            _boatView = new BoatView();
            _boatViewWrongInputMessages = new BoatViewWrongInputMessages();
            _memberRegister = memberRegister;
        }
    }
}