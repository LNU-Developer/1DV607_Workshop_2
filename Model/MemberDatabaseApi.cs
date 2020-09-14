using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Model
{
    class MemberDatabaseApi
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

        public async Task<bool> memberExist(string personalId)
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

        public async Task<bool> memberIdExist(int memberId)
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

        public async Task<Member> fetchMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Member member = snapshot.ConvertTo<Member>();
            return member;
        }

        public async Task<Member> fetchMemberById(int id)
        {

            CollectionReference  colRef = _db.Collection("members");
            Query query = colRef.WhereEqualTo("MemberId", id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                return documentSnapshot.ConvertTo<Member>();
            }

            DocumentReference docRef = _db.Collection("members").Document();
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Member member = snapshot.ConvertTo<Member>();
            return member;
        }

        public async Task<List<Member>> fetchAllMembers()
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

        public async Task addMember(Member member)
        {
            DocumentReference docRef = _db.Collection("members").Document(member.PersonalId);
            await docRef.SetAsync(member);
        }

        public async Task removeMemberBySsn(string personalId)
        {
            DocumentReference docRef = _db.Collection("members").Document(personalId);
            await docRef.DeleteAsync();
        }

        public async Task removeMemberById(int id)
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

        public MemberDatabaseApi(string projectId, string serviceAccountPath)
        {
            ServiceAccountPath = serviceAccountPath;
            ProjectId = projectId;
        }
    }
}