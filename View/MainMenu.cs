using System;
using MembersHandler;

namespace workshop_2
{
 class MainMenu
    {
        public MemberRegister Register {get;}
         public string startProgram()
        {
            Console.WriteLine("Welcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (1-4):");
            Console.WriteLine("1. Register member");
            Console.WriteLine("2. Delete member");
            string choice = Console.ReadLine();
            return choice;
        }

         public void readInput(string input)
        {
            if(Int32.Parse(input) == 1)
            {
                registerMember();
            } 
            else if(Int32.Parse(input) == 2)
            {
                deleteMember();
            } 
            else 
            {
                Console.WriteLine("Not a valid option");
            }
        }

        private void registerMember()
        {
            string firstName;
            string lastName;
            string pId;
            // string input;

            Console.WriteLine("You choose to register member");

            Console.WriteLine("Please enter first name:");
            firstName = Console.ReadLine();

            Console.WriteLine("Please enter last name:");
            lastName = Console.ReadLine();

            Console.WriteLine("Please enter your personal id number in 10 digits:");
            pId = Console.ReadLine();

            Register.addMember(firstName, lastName, pId);

            Member member = Register.getMemberBySsn(pId);
            Console.WriteLine(member.FirstName);
            // Console.WriteLine("Are these credentials correct:");
            // Console.WriteLine("First name: " + firstName);
            // Console.WriteLine("Last name: " + lastName);
            // Console.WriteLine("Personal id number: " + pId);
            // Console.WriteLine("y/n");
            // input = Console.ReadLine();
            
            // if (input = "y")
            // {

            // }

        }

        private void deleteMember()
        {
            string pId;
            // string input;

            Console.WriteLine("You choose to delete a member");

            Console.WriteLine("Please enter personal id number on the member you want to delete:");
            pId = Console.ReadLine();

            Register.deleteMemberBySsn(pId);

        }

        public MainMenu(MemberRegister register)
        {
            Register = register;
        }

    }
}
