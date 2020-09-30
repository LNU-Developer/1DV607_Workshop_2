using System;
using Model;

namespace Controller
{
    abstract class Register
    {

        public DatabaseApi database = new DatabaseApi(Environment.GetEnvironmentVariable("projectId"), Environment.GetEnvironmentVariable("serviceAccountPath"));

        public abstract int generateId();

        public abstract void deleteById(int id);
    }
}