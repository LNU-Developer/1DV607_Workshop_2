using System;

namespace Model
{
    abstract class Register
    {
        public DatabaseApi database = new DatabaseApi(Environment.GetEnvironmentVariable("projectId"), Environment.GetEnvironmentVariable("serviceAccountPath"));

        public abstract int GenerateId();

        public abstract void DeleteById(int id);
    }
}