using System;

namespace Model
{
    abstract class Register
    {
     private DatabaseApi database = new DatabaseApi(Environment.GetEnvironmentVariable("projectId"), Environment.GetEnvironmentVariable("serviceAccountPath"));

     public DatabaseApi Database
     {
         get { return database; }
     }
        public abstract int GenerateId();

        public abstract void DeleteById(int id);
    }
}