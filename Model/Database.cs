using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace workshop_2
{
    class Database
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

        public async Task addData()
        {

            DocumentReference docRef = _db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
              { "First", "Ada" },
              { "Last", "Lovelace" },
              { "Born", 1815 }
            };
            await docRef.SetAsync(user);
        }

        private static void InitializeProjectId(string project)
        {

            Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            FirestoreDb db = FirestoreDb.Create("uml-1dv607-workshop2");
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", "workshop-2-1dv607-289311");
        }

        public Database(string projectId, string serviceAccountPath)
        {
            ServiceAccountPath = serviceAccountPath;
            ProjectId = projectId;

        }
    }
}