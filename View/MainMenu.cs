using System;
using MembersHandler;
using BoatHandler;

namespace workshop_2
{
 class MainMenu
    {

        //TODO: Kolla ska vissa metoder finnas i MemberRegister i model istället? (doesPIdExistInRegister)
        public MemberRegister Register { get; }

        public int startProgram()
        {
            Console.WriteLine("\nWelcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (1-4):");
            Console.WriteLine("1. Register member");
            Console.WriteLine("2. Delete member");
            Console.WriteLine("3. Show member list");
            Console.WriteLine("4. Update member information");

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
            else if(input == 3)
            {
                showMemberList();
            }
            else if(input == 4)
            {
                Console.Clear();
                Console.WriteLine("Update member");
            }
             else if(input == 8)
            {
                showCompactList();
            }
             else if(input == 9)
            {
                showVerboseList();
            }
        }

        private void registerMember()
        {
            //TODO: Handling wrong inputs from user

            string firstName;
            string lastName;
            string pId;

            Console.WriteLine("\nPlease enter your personal id number in 10 digits:");
            pId = Console.ReadLine();

            // IsSwedishSsn?
            if(!Register.IsSwedishSsn(pId))
            {
                Console.Clear();
                Console.WriteLine("\nThis is not a correct personal number.");
                startProgram();
            }

            if(doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.WriteLine("\nA member with this personal number already exists in the register.");
                startProgram();
            }

            Console.WriteLine("\nPlease enter first name:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nPlease enter last name:");
            lastName = Console.ReadLine();

            Register.addMember(firstName, lastName, pId);

            if(doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member registered successfully!");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member deleted successfully!");
                Console.ResetColor();
                startProgram();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again:");
                startProgram();
            }

        }
        private void showMemberList()
        {
            Console.Clear();
            Console.WriteLine("Choose which type of list you want to view:");
            Console.WriteLine("8. Compact list");
            Console.WriteLine("9. Verbose list");

            handleInput(Int32.Parse(Console.ReadLine()));
            //TODO: Handling wrong inputs from user
        
        }

        private void showCompactList()
        {
            Console.Clear();
            Console.WriteLine("Compact list\n");
            
            foreach (Member member in Register.Members)
            {
            BoatRegister boatRegister = new BoatRegister(member.PersonalId);

               Console.ForegroundColor = ConsoleColor.DarkMagenta;
               Console.WriteLine("Fullname: " + member.FullName);
               Console.WriteLine("Member id: " + member.MemberId);
               
               int count = 0;
               foreach (Boat boat in boatRegister.Boats)
               {
                   count += 1;
                   
               }
               Console.WriteLine("Number of boats: " + count);
               Console.ResetColor();
               Console.WriteLine("═══════════════════════════════════════════");
            }
        }

        private void showVerboseList()
        {
            Console.Clear();
            Console.WriteLine("Verbose list\n");
            
            foreach (Member member in Register.Members)
            {
               BoatRegister boatRegister = new BoatRegister(member.PersonalId);

               Console.ForegroundColor = ConsoleColor.DarkMagenta;
               Console.WriteLine("Fullname: " + member.FullName);
               Console.WriteLine("Personal id: " + member.PersonalId);
               Console.WriteLine("Member id: " + member.MemberId);
               Console.WriteLine("Boatinformation:");

               int count = 0;
               foreach (Boat boat in boatRegister.Boats)
               {
                   count += 1;
                   Console.WriteLine();
                   Console.WriteLine(count + ". Boat type: " + boat.Type);
                   Console.WriteLine("   Boat length: " + boat.Length);
                   Console.WriteLine("   Boat id: " + boat.BoatId);
                   Console.WriteLine("__________");
               }
          
               Console.ResetColor();
               Console.WriteLine("═══════════════════════════════════════════");
            }               
        }
       

        public MainMenu(MemberRegister register)
        {
            Register = register;
        }

    }
}
