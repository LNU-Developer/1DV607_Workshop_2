using System;

namespace Model
{

    /// <summary>
    ///  This class that handles both members and boats through a register.
    /// </summary>
    abstract class Register
    {
        private DatabaseApi _database = new DatabaseApi(Environment.GetEnvironmentVariable("projectId"), Environment.GetEnvironmentVariable("serviceAccountPath"));
        public DatabaseApi Database
        {
             get { return _database; }
        }
        public abstract int GenerateId();
        public abstract void DeleteById(int id);

        public bool MemberExist(string personalId)
        {
            if(Database.MemberExist(personalId).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}