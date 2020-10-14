using Enum.boat.type;
using Model;
using View.menu;
using View.boat;
using System;

namespace Controller.boat
{
    /// <summary>
    ///  This class inherits from the MainInputController and handles and validates inputs related to the boatcommands.
    /// </summary>
    class BoatInputController : MainInputController
    {
        private BoatView _boatView;
        private BoatViewWrongInputMessages _boatViewWrongInputMessages;

        /// <summary>
        /// Adds boat to a member
        /// </summary>
        public void AddBoat()
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();

            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId))
            {
                AddBoat();
            }
            else
            {
                BoatRegister boatRegister = MemberRegister.GetMemberBySsn(pId).BoatRegister;
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
        }

        /// <summary>
        /// Removes boat from a member
        /// </summary>
        public void RemoveBoat()
        {
            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) RemoveBoat();
            BoatRegister boatRegister = MemberRegister.GetMemberBySsn(pId).BoatRegister;

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

        /// <summary>
        /// Updates boat on a member
        /// </summary>
        public void UpdateBoat()
        {
            BoatTypeMenu boatTypeMenu = new BoatTypeMenu();
            string pId = _boatView.InputSsn();

            if(!IsCorrectInputOfSsn(pId)) UpdateBoat();

            BoatRegister boatRegister;
            if(MemberRegister.MemberExist(pId))
            {
                boatRegister = MemberRegister.GetMemberBySsn(pId).BoatRegister;
            }
            else
            {
                _boatViewWrongInputMessages.PrintSsnNotExisting();
                return;
            }


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

        /// <summary>
        /// Convert string to integer
        /// </summary>
        /// <returns>
        /// An integer if all went successfully, and zero if something went wrong.
        /// </returns>
        /// <param name="input">a number in the string format.</param>
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

        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <returns>
        /// An double if all went successfully, and zero if something went wrong.
        /// </returns>
        /// <param name="input">a number in the string format.</param>
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

        /// <summary>
        /// Check if the input is correct in a social security number
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        /// <param name="id">the social security number in string format.</param>
        /// <param name="idExists">Flag to show if the social security number exists in the register or not.</param>
        public override bool IsCorrectInputOfSsn (string id, bool idExists = false)
        {
            if(!ValidatePidInput(id))
            {
                _boatViewWrongInputMessages.NotCorrectPId();
                return false;
            }

            if(!DoesPIdExistInRegister(id) && !idExists)
            {
                _boatViewWrongInputMessages.PrintSsnNotExisting();
                return false;
            }
            return true;
        }

        public BoatInputController (MemberRegister memberRegister) : base(memberRegister)
        {
            _boatView = new BoatView();
            _boatViewWrongInputMessages = new BoatViewWrongInputMessages();
        }
    }
}