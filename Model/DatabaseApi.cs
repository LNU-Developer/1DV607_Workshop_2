using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Model
{
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

        public async Task<Member> FetchMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Member member = snapshot.ConvertTo<Member>();
            return member;
        }

        public async Task<Member> FetchMemberById(int id)
        {
            CollectionReference  colRef = _db.Collection("members");
            Query query = colRef.WhereEqualTo("MemberId", id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            Member member = null;

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                member = documentSnapshot.ConvertTo<Member>();
            }
            return member;
        }

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

        public async Task AddMember(Member member)
        {
            DocumentReference docRef = _db.Collection("members").Document(member.PersonalId);
            await docRef.SetAsync(member);
        }

        public async Task RemoveMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            await docRef.DeleteAsync();
        }

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

        public async Task<Boat> FetchBoatById(int id)
        {
            CollectionReference  colRef = _db.Collection("boats");
            Query query = colRef.WhereEqualTo("BoatId", id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            Boat boat = null;
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                boat = documentSnapshot.ConvertTo<Boat>();
            }
            return boat;
        }

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

        public async Task AddBoat(Boat boat, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(boat.BoatId.ToString());
            await docRef.SetAsync(boat);
        }

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