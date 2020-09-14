using System;
using System.Collections.Generic;

namespace workshop_2
{
    class BoatRegister
    {
        private string _ownerPersonalId;
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
            Boat newBoat = new Boat()
            {
                Type = boatType,
                Length = length,
                BoatId = generateBoatId()
            };
            _boats.Add(newBoat);
        }

        public void deleteBoat(int id)
        {
            Boat foundBoat = getBoatById(id);
            _boatIds.RemoveAll(boatId => boatId == foundBoat.BoatId);
            _boats.RemoveAll(boat => boat.BoatId == id);
        }

        public Boat getBoatById (int id) {
            Boat foundBoat = _boats.Find(boat => boat.BoatId == id);
            return foundBoat;
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

        public BoatRegister(string PersonalId)
        {
            _ownerPersonalId=PersonalId;
        }
    }
}