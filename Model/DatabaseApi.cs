using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Model
{

    /// <summary>
    /// Class that handles the database calls
    /// </summary>
    class DatabaseApi
    {
        private FirestoreDb _db;
        private string ProjectId
        {
            set
            {
                _db=FirestoreDb.Create(value);
            }
        }
        private string ServiceAccountPath
        {
           set
           {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", value);
           }
        }

        /// <summary>
        /// Async method that checks if a member exists in the database.
        /// </summary>
        /// <return>
        /// True or false
        ///</returns>
        /// <param name="personalId">social security number of the person to be checked.</param>
        public async Task<bool> MemberExist(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async method that checks if a member id exists in the database.
        /// </summary>
        /// <return>
        /// True or false
        ///</returns>
        /// <param name="memberId">member id of the person to be checked.</param>
        public async Task<bool> MemberIdExist(int memberId)
        {
            CollectionReference  colRef = _db.Collection("members");
            Query query = colRef.WhereEqualTo("MemberId", memberId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Async method that fetches a member from the database.
        /// </summary>
        /// <return>
        /// A Member object
        ///</returns>
        /// <param name="personalId">Social security number of the person to be fetched.</param>
        public async Task<Member> FetchMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Member member = snapshot.ConvertTo<Member>();
            return member;
        }

        /// <summary>
        /// Async method that fetches all members from the database.
        /// </summary>
        /// <return>
        /// A list of Member object
        ///</returns>
        public async Task<List<Member>> FetchAllMembers()
        {
            Query allMembersQuery = _db.Collection("members");
            QuerySnapshot allMemberQuerySnapshot = await allMembersQuery.GetSnapshotAsync();
            List<Member> members = new List<Member>();

            foreach (DocumentSnapshot documentSnapshot in allMemberQuerySnapshot.Documents)
            {
                Dictionary<string, object> snapshotMember = documentSnapshot.ToDictionary();
                Member member = new Member
                {
                    FirstName = snapshotMember["FirstName"].ToString(),
                    LastName = snapshotMember["LastName"].ToString(),
                    PersonalId = snapshotMember["PersonalId"].ToString(),
                    MemberId = Convert.ToInt32(snapshotMember["MemberId"].ToString())
                };
                members.Add(member);
            }

            return members;
        }

        /// <summary>
        /// Async method that adds a member to the database.
        /// </summary>
        /// <param name="member">a member object to be added.</param>
        public async Task AddMember(Member member)
        {
            DocumentReference docRef = _db.Collection("members").Document(member.PersonalId);
            await docRef.SetAsync(member);
        }

        /// <summary>
        /// Async method that remove a member based on social security number from the database.
        /// </summary>
        /// <param name="personalId">The social security number of the member to be removed.</param>
        public async Task RemoveMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            await docRef.DeleteAsync();
        }

        /// <summary>
        /// Async method that remove a member based on id from the database.
        /// </summary>
        /// <param name="personalId">The social security number of the member to be removed.</param>
        public async Task RemoveMemberById(int id)
        {

            CollectionReference  colRef = _db.Collection("members");
            Query query = colRef.WhereEqualTo("MemberId", id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                DocumentReference docRef = colRef.Document(documentSnapshot.Id);
                await docRef.DeleteAsync();
            }
        }

        /// <summary>
        /// Async method that checks if a boat id exists in the database.
        /// </summary>
        /// <return>
        /// True or false
        ///</returns>
        /// <param name="boatId">boat id of the boat to be checked.</param>
        /// <param name="personalId">social security number of the person that owns the boat.</param>
        public async Task<bool> BoatIdExist(int boatId, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(boatId.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Async method that fetches all boats owned by a member from the database.
        /// </summary>
        /// <return>
        /// A list of boat objects
        ///</returns>
        /// <param name="personalId">social security number of the person that owns the boats.</param>
        public async Task<List<Boat>> FetchAllBoatsForMember(string personalId)
        {
            Query allMembersBoatsQuery = _db.Collection("boats").Document(personalId).Collection("boats");
            QuerySnapshot snapshot = await allMembersBoatsQuery.GetSnapshotAsync();
            List<Boat> boats = new List<Boat>();
            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                boats.Add(documentSnapshot.ConvertTo<Boat>());
            }
            return boats;
        }

        /// <summary>
        /// Async method that adds a boat to a specific member to the database.
        /// </summary>
        /// <param name="boat">Boat object to be added.</param>
        /// <param name="personalId">social security number of the person that owns the boats.</param>
        public async Task AddBoat(Boat boat, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(boat.BoatId.ToString());
            await docRef.SetAsync(boat);
        }

        /// <summary>
        /// Async method that removes a boat based on id from to the database.
        /// </summary>
        /// <param name="id">Boat id of the boat to be removed.</param>
        /// <param name="personalId">social security number of the person that owns the boats.</param>
        public async Task RemoveBoatById(int id, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(id.ToString());
            await docRef.DeleteAsync();
        }
        public DatabaseApi(string projectId, string serviceAccountPath)
        {
            ServiceAccountPath = serviceAccountPath;
            ProjectId = projectId;
        }
    }
}