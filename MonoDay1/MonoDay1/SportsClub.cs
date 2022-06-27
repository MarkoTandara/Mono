using System;
using System.Collections.Generic;
using System.Text;

namespace MonoDay1
{
    abstract class SportsClub
    {
        protected int Name;
        protected int Location;
        public abstract void SetName(string clubName);
        public abstract void SetLocation(string clubLocation);
        public abstract string GetName();
        public abstract string GetLocation();
        public abstract void Score();
    }
}
