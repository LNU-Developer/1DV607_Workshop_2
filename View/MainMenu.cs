using System;
using MembersHandler;
using BoatHandler;
using EnumBoatTypes;

namespace workshop_2
{
 class MainMenu
{

        //TODO: Kolla ska vissa metoder finnas i MemberRegister i model istället? (doesPIdExistInRegister)
        public MemberRegister Register { get; }

        public void startProgram()
        {
            Console.WriteLine("\nWelcome to the Boat Club!");
            Console.WriteLine("Please choose what you want to do next (0-4):");
            Console.WriteLine("1. Register member");
            Console.WriteLine("2. Delete member");
            Console.WriteLine("3. Show member list");
            Console.WriteLine("4. Update member information");
            Console.WriteLine("5. Add boat to member");
            Console.WriteLine("6. Remove boat from member");
            Console.WriteLine("7. Update boat information");
            Console.WriteLine("0. Exit program");

            string input = Console.ReadLine();
            if(!InputHandler.isCorrectMenuInput(input, 0, 9))
            {
                handleInput(10);
            }
            else
            {
                handleInput(Int32.Parse(input));
            }

        }

        private void handleInput(int input)
        {
            switch(input)
            {
                case 1:
                    registerMember();
                    startProgram();
                break;
                case 2:
                    deleteMember();
                    startProgram();
                break;
                case 3:
                    showMemberList();
                    startProgram();
                break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Update member");
                    startProgram();
                break;
                case 5:
                    addBoatToMember();
                    startProgram();
                break;
                case 6:
                    deleteBoatFromMember();
                    startProgram();
                break;
                case 7:
                    startProgram();
                break;
                case 8:
                    showCompactList();
                    startProgram();
                break;
                case 9:
                    showVerboseList();
                    startProgram();
                break;
                case 0:
                    Environment.Exit(0);
                break;
                default:
                    startProgram();
                break;
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

            if(!isCorrectInputOfSsn(pId, true)) registerMember();

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
            //TODO: Delete by pid or memberid
            string pId;
            Console.Clear();

            Console.WriteLine("Please enter personal id number on the member you want to delete:");
            pId = Console.ReadLine();

            if(!isCorrectInputOfSsn(pId)) deleteMember();

            //TODO: Are you sure you want to delete this member: Visa memberInfo.
            Register.deleteMemberBySsn(pId);

            if(!doesPIdExistInRegister(pId))
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

        private void addBoatToMember()
        {
            //TODO: Fetch by member id or SSN
            string pId;

            Console.WriteLine("Please enter personal id number on the member you want to add a boat to:");
            pId = Console.ReadLine();

            if(!isCorrectInputOfSsn(pId)) addBoatToMember();

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

        private void deleteBoatFromMember()
        {
            //TODO: Fetch by member id or SSN
            string pId;

            Console.WriteLine("Please enter personal id number on the member you want to remove a boat from:");
            pId = Console.ReadLine();

            if(!isCorrectInputOfSsn(pId)) deleteBoatFromMember();

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

        private bool isCorrectInputOfSsn (string id, bool idExists = false)
        {
             //TODO: Fix Personal ID wrong input handling
            if(!Register.IsSwedishSsn(id))
            {
                Console.Clear();
                Console.WriteLine("\nThis is not a correct personal number.");
                return false;
            }

            if(!doesPIdExistInRegister(id) && !idExists)
            {
                Console.Clear();
                Console.WriteLine("\nA member with this personal id doesn't exist in the register.");
                return false;
            }
            else if(doesPIdExistInRegister(id) && idExists)
            {
                Console.Clear();
                Console.WriteLine("\nA member with this personal number already exists in the register.");
                return false;
            }

            return true;
        }

        private void showMemberList()
        {
            Console.Clear();
            Console.WriteLine("Choose which type of list you want to view:");
            Console.WriteLine("8. Compact list");
            Console.WriteLine("9. Verbose list");
            string input = Console.ReadLine();
            if(!InputHandler.isCorrectMenuInput(input, 8, 9))
            {
                handleInput(10);
            }
            else
            {
                handleInput(Int32.Parse(input));
            }
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
