using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Model
{
    class BoatDatabaseApi
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

        public async Task<bool> boatIdExist(int boatId, string personalId)
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

        public async Task<Boat> fetchBoatById(int id)
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

        public async Task<List<Boat>> fetchAllBoatsForMember(string personalId)
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

        public async Task addBoat(Boat boat, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(boat.BoatId.ToString());
            await docRef.SetAsync(boat);
        }

        public async Task removeBoatById(int id, string personalId)
        {
            DocumentReference docRef = _db.Collection("boats").Document(personalId).Collection("boats").Document(id.ToString());
            await docRef.DeleteAsync();
        }

        public BoatDatabaseApi(string projectId, string serviceAccountPath)
        {
            ServiceAccountPath = serviceAccountPath;
            ProjectId = projectId;
        }
    }
}