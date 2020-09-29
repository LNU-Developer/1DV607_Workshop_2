using System;
using MembersHandler;
using BoatHandler;
using EnumBoatTypes;

namespace workshop_2
{
    class SubMenu
    {
        //TODO: Kolla ska vissa metoder finnas i MemberRegister i model istället? (doesPIdExistInRegister)
        public MemberRegister Register { get; }

        public void registerMember()
        {
            //TODO: Handling wrong inputs from user

            string firstName;
            string lastName;
            string pId;

            Console.WriteLine("\nPlease enter your personal id number in 10 digits:");
            pId = Console.ReadLine();

            if(!InputHandler.isCorrectInputOfSsn(pId, true)) registerMember();

            Console.WriteLine("\nPlease enter first name:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nPlease enter last name:");
            lastName = Console.ReadLine();

            Register.addMember(firstName, lastName, pId);

            if(InputHandler.doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member registered successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again:");
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

        public void deleteMember()
        {
            //TODO: Delete by pid or memberid
            string pId;
            Console.Clear();

            Console.WriteLine("Please enter personal id number on the member you want to delete:");
            pId = Console.ReadLine();

            if(!InputHandler.isCorrectInputOfSsn(pId)) deleteMember();

            //TODO: Are you sure you want to delete this member: Visa memberInfo.
            Register.deleteMemberBySsn(pId);

            if(!InputHandler.doesPIdExistInRegister(pId))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member deleted successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again:");
            }

        }

        public void addBoatToMember()
        {
            //TODO: Fetch by member id or SSN
            string pId;

            Console.WriteLine("Please enter personal id number on the member you want to add a boat to:");
            pId = Console.ReadLine();

            if(!InputHandler.isCorrectInputOfSsn(pId)) addBoatToMember();

            Member selectedMember =  Register.getMemberBySsn(pId);
            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            Console.WriteLine("Please pick from the selected boat types:");
            Console.WriteLine("1. Sailboat");
            Console.WriteLine("2. Motorsailer");
            Console.WriteLine("3. Kayak");
            Console.WriteLine("4. Other");

            string boatTypeString = Console.ReadLine();
            if(!InputHandler.isCorrectMenuInput(boatTypeString, 1, 4)) addBoatToMember();

            BoatTypes boatType = (BoatTypes)Int32.Parse(boatTypeString);
            Console.WriteLine("Please type in the length of the boat:");
            string lengthString = Console.ReadLine();
            if(InputHandler.convertToDouble(lengthString) == 0)
            {
                //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                Console.WriteLine("Wrong input provided. Please enter a decimal number above zero.");
                addBoatToMember();
            }
            boatRegister.addBoat(boatType, InputHandler.convertToDouble(lengthString));
            Console.WriteLine("Successfully added the " + boatType + " to the selected member.");
        }

        public void deleteBoatFromMember()
        {
            //TODO: Fetch by member id or SSN
            string pId;

            Console.WriteLine("Please enter personal id number on the member you want to remove a boat from:");
            pId = Console.ReadLine();

            if(!InputHandler.isCorrectInputOfSsn(pId)) deleteBoatFromMember();

            Member selectedMember =  Register.getMemberBySsn(pId);
            BoatRegister boatRegister = new BoatRegister(selectedMember.PersonalId);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
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
            Console.WriteLine("Please enter the ID of the boat you want to remove.");
            string idString = Console.ReadLine();
            if(InputHandler.convertToInt(idString) == 0)
            {
                //TODO: Fix bug, when user first enter a wrong value it gets added as zero when user enters a correct value
                Console.WriteLine("Wrong input provided. Please enter a number above zero.");
                deleteBoatFromMember();
            }
            int id = InputHandler.convertToInt(idString);
            if(boatRegister.isBoat(id))
            {
                boatRegister.deleteBoat(id);
                Console.WriteLine("Successfully deleted the boat with the id " + idString + " from the selected member.");
            }
            else
            {
                Console.WriteLine("Error when trying to delete. The boat with the id " + idString + " doesn't exist.");
            }

        }

        public int showMemberList()
        {
            Console.Clear();
            Console.WriteLine("Choose which type of list you want to view:");
            Console.WriteLine("8. Compact list");
            Console.WriteLine("9. Verbose list");
            string input = Console.ReadLine();
            if(!InputHandler.isCorrectMenuInput(input, 8, 9))
            {
                return 10;
            }
            else
            {
                return Int32.Parse(input);
            }
        }

        public void showCompactList()
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

        public void showVerboseList()
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
        public SubMenu(MemberRegister register)
        {
            Register = register;
        }
    }

}