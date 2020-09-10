using System;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
           MemberRegister register = new MemberRegister();
            register.addMember("Musse", "Pigg", "9510101349");
            Member member1 = register.getMemberBySsn("9510101349");

            //Adding boats to member
            member1.BoatRegister.addBoat(BoatTypes.Sailboat, 4.5);
            member1.BoatRegister.addBoat(BoatTypes.Kayak, 7.3);

            foreach (object boat in member1.BoatRegister.Boats)
            {
                Console.WriteLine(boat.ToString());
            }

            // Deleting boat from BoatRegister
            int id = member1.BoatRegister.Boats[0].BoatId;
            member1.BoatRegister.deleteBoat(id);

            Console.WriteLine("-------------------------------------");

            foreach (object boat in member1.BoatRegister.Boats)
            {
                Console.WriteLine(boat.ToString());
            }

            // TODO: Make sure you cant create an instance of Boatregister without member
            // BoatRegister boatreg = new BoatRegister();
            
            // register.deleteMemberBySsn("9510101349");
            // Console.WriteLine(register.getMemberBySsn("9510101349").MemberId);

            // Boat two = new Boat(BoatTypes.Sailboat, 1.0);
            // Console.WriteLine(two.Type);

        }
    }
}
