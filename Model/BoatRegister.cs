using System;
using System.Collections.Generic;

namespace workshop_2
{
    class BoatRegister
    {
        private List<Boat> _boats = new List<Boat>();

        public IReadOnlyList<Boat> Boats
        {
            get
            {
                return _boats.AsReadOnly();
            }
        }

        public void addBoat(BoatTypes boatType, double length)
           {
            Boat newBoat = new Boat(boatType, length);
            _boats.Add(newBoat);
        }

        public void deleteBoat()
        {

        }

        public void updateBoat()
        {
        }

    }
}