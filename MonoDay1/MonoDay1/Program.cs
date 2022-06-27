using System;
using System.Collections.Generic;

namespace MonoDay1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FootballClub> Clubs = new List<FootballClub>();
            Clubs.Add(new FootballClub("Manchester United", "England"));
            Clubs.Add(new FootballClub("Bayern Munchen", "Germany"));
            Clubs.Add(new FootballClub("Barcelona", "Spain"));
            Clubs.Add(new FootballClub("Juventus", "Italy"));

            foreach (FootballClub club in Clubs)
            {
                Console.WriteLine("Club " + club.GetName() + " is from " + club.GetLocation());
            }
            BasketballClub ManchesterEagles = new BasketballClub("Manchester Eagles", "England");

            ManchesterEagles.Score();
            ManchesterEagles.AnnounceGame();
        }
    }
}
