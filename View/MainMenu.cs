using System;
using MembersHandler;

namespace workshop_2
{
 class MainMenu
    {

        //TODO: Kolla ska vissa metoder finnas i MemberRegister i model istället? (doesPIdExistInRegister)
        public MemberRegister Register { get; }
        public int startProgram()
        {
            Console.WriteLine("Welcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (1-4):");
            Console.WriteLine("1. Register member");
            Console.WriteLine("2. Delete member");

            string input = Console.ReadLine();
            //TODO: Handling wrong inputs from user before return
            return Int32.Parse(input);
        }

        public void handleInput(int input)
        {
            if(input == 1)
            {
                registerMember();
            } 
            else if(input == 2)
            {
                deleteMember();
            } 
        }

        private void registerMember()
        {
            //TODO: Handling wrong inputs from user

            string firstName;
            string lastName;
            string pId;

            Console.WriteLine("Please enter your personal id number in 10 digits:");
            pId = Console.ReadLine();

            // IsSwedishSsn?
            if(!Register.IsSwedishSsn(pId))
            {
                Console.Clear();
                Console.WriteLine("This is not a correct personal number.");
                startProgram();
            }

            if(doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.WriteLine("A member with this personal number allready exists in the register.");
                startProgram();
            }

            Console.WriteLine("Please enter first name:");
            firstName = Console.ReadLine();

            Console.WriteLine("Please enter last name:");
            lastName = Console.ReadLine();

            Register.addMember(firstName, lastName, pId);

            if(doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.WriteLine("Member registered successfully");
                startProgram();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again:");
                startProgram();
            }


            // string input;
            // Console.WriteLine("Are these credentials correct:");
            // Console.WriteLine("First name: " + firstName);
            // Console.WriteLine("Last name: " + lastName);
            // Console.WriteLine("Personal id number: " + pId);
            // Console.WriteLine("y/n");
            // input = Console.ReadLine();
            // Member member = Register.getMemberBySsn(pId);
            // Console.WriteLine("Your member id is: " + member.MemberId);

        }

        private bool doesPIdExistInRegister(string pId)
        {
            int count = 0;
            foreach (Member member in Register.Members)
            {
                if(member.PersonalId == pId)
                {
                    count++;
                }
            }

            if(count == 1) {
                return true;
            }
            else
            {
                return false;
            } 
        }

        private void deleteMember()
        {
            //TODO: Handling wrong inputs from user
            //TODO: Delete by pid or memberid
            string pId;
            Console.Clear();

            Console.WriteLine("Please enter personal id number on the member you want to delete:");
            pId = Console.ReadLine();

            //TODO: Are you sure you want to delete this member: Visa memberInfo.
            Register.deleteMemberBySsn(pId);

            if(!doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.WriteLine("Member deleted successfully");
                startProgram();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again:");
                startProgram();
            }

        }

        public MainMenu(MemberRegister register)
        {
            Register = register;
        }

    }
}
