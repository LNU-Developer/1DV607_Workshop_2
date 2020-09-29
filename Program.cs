using System;
using EnumBoatTypes;
using MembersHandler;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            DotNetEnv.Env.Load();

            MemberRegister register = new MemberRegister();
            MainMenu menu = new MainMenu(register);
            menu.startProgram();

            // Member member1 = register.getMemberBySsn("9510101349");
            // Console.WriteLine(member1.FirstName);
            // register.addMember("Musse", "Pigg", "9510101349");
            // register.addMember("Kalle", "Anka", "8710134050");
            // Member member1 = register.getMemberBySsn("9510101349");
            // Member member2 = register.getMemberBySsn("8710134050");

            // foreach (Member i in register.Members)
            // {
            //     Console.WriteLine(i.FullName);
            // }

            //Adding boats to member
            // member1.BoatRegister.addBoat(BoatTypes.Kayak, 10);
            // member1.BoatRegister.addBoat(BoatTypes.Other, 5);
            // member2.BoatRegister.addBoat(BoatTypes.Motorsailer, 4.5);
            // member2.BoatRegister.addBoat(BoatTypes.Sailboat, 2);
            // member1.BoatRegister.addBoat(BoatTypes.Kayak, 1);


            // foreach (object boat in member1.BoatRegister.Boats)
            // {
            //     Console.WriteLine(boat.ToString());
            // }

            // Deleting boat from BoatRegister
            // int id = member1.BoatRegister.Boats[0].BoatId;
            // member1.BoatRegister.deleteBoat(id);

            Console.WriteLine("-------------------------------------");

            // foreach (object boat in member1.BoatRegister.Boats)
            // {
            //     Console.WriteLine(boat.ToString());
            // }

            // TODO: Make sure you cant create an instance of Boatregister without member
            // BoatRegister boatreg = new BoatRegister();

            // register.deleteMemberBySsn("9510101349");
            // Console.WriteLine(register.getMemberBySsn("9510101349").MemberId);

            // Boat two = new Boat(BoatTypes.Sailboat, 1.0);
            // Console.WriteLine(two.Type);

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
