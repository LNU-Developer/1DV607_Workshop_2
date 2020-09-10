using System;
using System.Collections.Generic;

namespace workshop_2
{
    class BoatRegister
    {
        private List<Boat> _boats = new List<Boat>();
        private List<int> _boatIds = new List<int>();
        public IReadOnlyList<Boat> Boats
        {
            get
            {
                return _boats.AsReadOnly();
            }
        }

        public void addBoat(BoatTypes boatType, double length)
           {
            Boat newBoat = new Boat(boatType, length, generateBoatId());
            _boats.Add(newBoat);
        }

        public void deleteBoat()
        {

        }

        public void updateBoat()
        {
        }

        private int generateBoatId()
        {
            Random a = new Random();

            int newBoatId;
  	        newBoatId = a.Next(0, 100000000);

            while(_boatIds.Contains(newBoatId))
    	        newBoatId = a.Next(0, 100000000);

            _boatIds.Add(newBoatId);

            return newBoatId;
        }
    }
}