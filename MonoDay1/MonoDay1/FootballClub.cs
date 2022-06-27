using System;
using System.Collections.Generic;
using System.Text;

namespace MonoDay1
{
    class FootballClub : SportsClub, ISportsCLub
    {
        private string Name;
        private string Location;
        public FootballClub(string name, string location)
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
            Console.WriteLine("Alee Alee");
        }
        public void AnnounceGame()
        {
            Console.WriteLine("Come join us on football pitch");
        }
        public override void Score()
        {
            Console.WriteLine("Kick a ball in goal!");
        }
    }
}
