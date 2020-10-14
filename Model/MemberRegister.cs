using System;
using System.Collections.Generic;

namespace Model
{

    /// <summary>
    /// Class to handle members in a register.
    /// </summary>
    class MemberRegister : Register
    {
        public IReadOnlyList<Member> Members
        {
            get { return Database.FetchAllMembers().Result.AsReadOnly(); }
        }

        /// <summary>
        /// Creates a member and calls the database class to add it to the database.
        /// </summary>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="personalId">Social security number of the member.</param>
        public void AddMember(string firstName, string lastName, string personalId)
        {
            if(!MemberExist(personalId))
            {
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GenerateId()
                };
                Database.AddMember(newMember).Wait();
            }
            else
            {
                throw new ArgumentException($"{nameof(personalId)} already exists. Unable to register new member.");
            }
        }

        /// <summary>
        /// Fethes a member by calling the database class and fetch it from the database.
        /// </summary>
        /// <returns>
        /// A member oject
        /// </returns>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="personalId">Social security number of the member.</param>
        public Member GetMemberBySsn(string id)
        {
            id = id.Replace("-", "");
            id = id.Replace("+", "");

            if (id.Length == 12)
                id = id.Substring(2, 10);

            if(MemberExist(id))
            {
                return Database.FetchMemberBySsn(id).Result;
            }
            else
            {
                throw new ArgumentException($"{nameof(id)} member doesn't exists.");
            }
        }

        /// <summary>
        /// Deletes a member by social security number calling the database class.
        /// </summary>
        /// <param name="personalId">Social security number of the member.</param>
        public void DeleteMemberBySsn(string id)
        {
            if(MemberExist(id))
            {
                Database.RemoveMemberBySsn(id).Wait();
            }
        }

        /// <summary>
        /// Deletes a member by member id by calling the database class.
        /// </summary>
        /// <param name="id">Member id of the member.</param>
        public override void DeleteById(int id)
        {
            if(Database.MemberIdExist(id).Result)
            {
                Database.RemoveMemberById(id).Wait();
            }
        }

        /// <summary>
        /// Updates an existing member by calling the database class and updating the database.
        /// </summary>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="personalId">Social security number of the member.</param>
        public void UpdateMember(string firstName, string lastName, string personalId)
        {
            if(MemberExist(personalId))
            {
                Member newMember = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalId = personalId,
                    MemberId = GetMemberBySsn(personalId).MemberId
                };
                Database.AddMember(newMember).Wait();
            }
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

            int newMemberId;
  	        newMemberId = a.Next(0, 100000000);

            while(Database.MemberIdExist(newMemberId).Result)
    	        newMemberId = a.Next(0, 100000000);

            return newMemberId;
        }
    }
}