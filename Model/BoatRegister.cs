using System;
using System.Collections.Generic;
using Enum.boat.type;

namespace Model
{
    /// <summary>
    /// Class to handle boats in a register.
    /// </summary>
    class BoatRegister : Register
    {
        private string _ownerPersonalId;
        public IReadOnlyList<Boat> Boats
        {
            get { return Database.FetchAllBoatsForMember(_ownerPersonalId).Result.AsReadOnly(); }
        }

        /// <summary>
        /// Creates a new boat and calls the database class to add it to the database.
        /// </summary>
        /// <param name="boatType">A BoatType enum.</param>
        /// <param name="length">The boat length.</param>
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

        /// <summary>
        /// Deletes a new boat and calls the database class to remove it from the database.
        /// </summary>
        /// <param name="id">The boat id.</param>
        public override void DeleteById(int id)
        {
            if(Database.BoatIdExist(id, _ownerPersonalId).Result)
            {
                Database.RemoveBoatById(id, _ownerPersonalId).Wait();
            }
        }

        /// <summary>
        /// Updates a boat and calls the database class to add changes to the database.
        /// </summary>
        /// <param name="boatType">A BoatType enum.</param>
        /// <param name="length">The boat length.</param>
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

        /// <summary>
        /// Method that checks if a boat exist based on the inputted id.
        /// </summary>
        /// <return>
        /// True or false
        ///</returns>
        /// <param name="id">A Boat id to be checked.</param>
        public bool IsBoat(int id)
        {
            if(Database.BoatIdExist(id, _ownerPersonalId).Result) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Method that generates a new unique id.
        /// </summary>
        /// <return>
        /// The unique ID
        ///</returns>
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