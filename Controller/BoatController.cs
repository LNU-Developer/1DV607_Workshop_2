using Enum.boat.type;
using Model;
using View.menu;
using View.boat;
namespace Controller.boat
{
    class BoatController
    {
        private BoatView _boatView;
        private MemberRegister _memberRegister;
        private InputChecker _inputChecker;
        public void AddBoat()
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            
            string pId = _boatView.InputSsn();

            if(!_inputChecker.IsCorrectInputOfSsn(pId)) AddBoat();

            BoatRegister boatRegister = new BoatRegister(_memberRegister.GetMemberBySsn(pId).PersonalId);
            boatTypeMenu.DisplayMenu();
            BoatType boatType = boatTypeMenu.GetInput();

            string lengthString = _boatView.InputBoatLength();
            if(_inputChecker.ConvertToDouble(lengthString) == 0)
            {
                _boatView.PrintNotADoubleAboveZero();
                AddBoat();
            } else {
                boatRegister.AddBoat(boatType, _inputChecker.ConvertToDouble(lengthString));
                _boatView.PrintActionSuccess();
            }
        }
        public void RemoveBoat()
        {
            string pId = _boatView.InputSsn();

            if(!_inputChecker.IsCorrectInputOfSsn(pId)) RemoveBoat();
            BoatRegister boatRegister = new BoatRegister(_memberRegister.GetMemberBySsn(pId).PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                _boatView.PrintNoBoatsFound();
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

            if(_inputChecker.ConvertToInt(idString) == 0)
            {
                _boatView.PrintNotADoubleAboveZero();
                RemoveBoat();
            } else {
                int id = _inputChecker.ConvertToInt(idString);
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

            if(!_inputChecker.IsCorrectInputOfSsn(pId)) UpdateBoat();

            Member selectedMember =  _memberRegister.GetMemberBySsn(pId);

            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            if(boatRegister.Boats.Count == 0)
            {
                _boatView.PrintNoBoatsFound();
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

            if(_inputChecker.ConvertToInt(idString) == 0)
            {
                _boatView.PrintNotAnIntAboveZero();
                UpdateBoat();
            }
            else
            {
                int id = _inputChecker.ConvertToInt(idString);

                if(boatRegister.IsBoat(id))
                {
                    boatTypeMenu.DisplayMenu();
                    BoatType boatType = boatTypeMenu.GetInput();

                    string lengthString = _boatView.InputBoatLength();
                    if(_inputChecker.ConvertToDouble(lengthString) == 0)
                    {
                        //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                        _boatView.PrintNotADoubleAboveZero();
                        UpdateBoat();
                    }

                    boatRegister.UpdateBoat(id, boatType, _inputChecker.ConvertToDouble(lengthString));
                    _boatView.PrintActionSuccess();
                }
                else
                {
                    _boatView.PrintActionFail();
                }
            }
        }

        public BoatController (MemberRegister memberRegister, InputChecker inputChecker)
        {
            _boatView = new BoatView();
            _memberRegister = memberRegister;
            _inputChecker = inputChecker;
        }
    }
}