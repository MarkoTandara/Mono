using System;
using System.Collections.Generic;
using System.Text;

namespace MonoDay1
{
    class BasketballClub : SportsClub, ISportsCLub
    {
        private string Name;
        private string Location;
        public BasketballClub(string name, string location)
        {
            Name = name;
            Location = location;
        }
        public override void SetName(string clubName)
        {
            Name = clubName;
        }
        public override void SetLocation(string clubLocation)
        {
            Location = clubLocation;
        }
        public override string GetName()
        {
            return Name;
        }
        public override string GetLocation()
        {
            return Location;
        }

        public void Cheer()
        {
            Console.WriteLine("Make that shot!");
        }
        public void AnnounceGame()
        {
            Console.WriteLine("Come join us at indoor gym!");
        }
        public override void Score()
        {
            Console.WriteLine("Shoot a ball through a net!");
        }
    }
}
