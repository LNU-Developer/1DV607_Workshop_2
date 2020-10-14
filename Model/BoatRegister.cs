using System;
using System.Collections.Generic;
using Enum.boat.type;

namespace Model
{
    class BoatRegister : Register
    {
        private string _ownerPersonalId;
        public IReadOnlyList<Boat> Boats
        {
            get { return Database.FetchAllBoatsForMember(_ownerPersonalId).Result.AsReadOnly(); }
        }
        public void AddBoat(BoatType boatType, double length)
        {
            Boat newBoat = new Boat()
            {
                Type = boatType,
                Length = length,
                BoatId = GenerateId()
            };
            Database.AddBoat(newBoat, _ownerPersonalId).Wait();
        }
        public override void DeleteById(int id)
        {
            if(Database.BoatIdExist(id, _ownerPersonalId).Result)
            {
                Database.RemoveBoatById(id, _ownerPersonalId).Wait();
            }
        }
        public void UpdateBoat(int id, BoatType boatType, double length)
        {
            if(Database.BoatIdExist(id, _ownerPersonalId).Result)
            {
                Boat newBoat = new Boat()
                {
                    Type = boatType,
                    Length = length,
                    BoatId = id
                };
                Database.AddBoat(newBoat, _ownerPersonalId).Wait();
            }
        }
        public bool IsBoat(int id)
        {
            if(Database.BoatIdExist(id, _ownerPersonalId).Result) { return true; }
            else { return false; }
        }
        public override int GenerateId()
        {
            Random a = new Random();

            int newBoatId;
  	        newBoatId = a.Next(0, 100000000);

            while(Database.BoatIdExist(newBoatId, _ownerPersonalId).Result)
            {
                newBoatId = a.Next(0, 100000000);
            }

            return newBoatId;
        }
        public BoatRegister(string PersonalId)
        {
            _ownerPersonalId=PersonalId;
        }
    }
}