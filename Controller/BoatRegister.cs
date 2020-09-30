using System;
using System.Collections.Generic;
using EnumBoatTypes;
using Model;

namespace Controller
{
    class BoatRegister : Register
    {
        private string _ownerPersonalId;
        public IReadOnlyList<Boat> Boats
        {
            get
            {
                return database.fetchAllBoatsForMember(_ownerPersonalId).Result.AsReadOnly();
            }
        }

        public void addBoat(BoatTypes boatType, double length)
           {
            Boat newBoat = new Boat()
            {
                Type = boatType,
                Length = length,
                BoatId = generateId()
            };
            database.addBoat(newBoat, _ownerPersonalId).Wait();
        }

        public void deleteBoat(int id)
        {
            if(database.boatIdExist(id, _ownerPersonalId).Result)
            {
                database.removeBoatById(id, _ownerPersonalId).Wait();
            }
        }

        public Boat getBoatById (int id) {
            if(database.boatIdExist(id, _ownerPersonalId).Result)
            {
                return database.fetchBoatById(id).Result;
            }
            else
            {
                throw new ArgumentException(
                        $"{nameof(id)} boat doesn't exists.");
            }
        }

        public void updateBoat(int id, BoatTypes boatType, double length)
        {
            if(database.boatIdExist(id, _ownerPersonalId).Result)
            {
                Boat newBoat = new Boat()
                {
                    Type = boatType,
                    Length = length,
                    BoatId = id
                };
                database.addBoat(newBoat, _ownerPersonalId).Wait();
            }
        }

        public bool isBoat(int id)
        {
            if(database.boatIdExist(id, _ownerPersonalId).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int generateId()
        {
            Random a = new Random();

            int newBoatId;
  	        newBoatId = a.Next(0, 100000000);

            while(database.boatIdExist(newBoatId, _ownerPersonalId).Result)
    	        newBoatId = a.Next(0, 100000000);

            return newBoatId;
        }

        public BoatRegister(string PersonalId)
        {
            _ownerPersonalId=PersonalId;
        }
    }
}