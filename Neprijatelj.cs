using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seminar
{
    public class Neprijatelj:Svi
        
    {
        public Neprijatelj(string path,int cox,int coy):base(path,cox,coy)
        {
            this.Speed = 5;
        }

        protected int damage;
        protected int speed;

        public int Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }
    }

    public class Flee:Neprijatelj
    {
        public Flee(string path, int cox, int coy):base(path,cox,coy)
        {
            this.Speed = 10;
            this.Damage = 2;

        }
    }

    public class PilotBird:Neprijatelj
    {
        public PilotBird(string path, int cox, int coy):base(path,cox,coy)
        {
            this.Speed = 12;
            this.Damage = 4;
        }
    }

    public class BigBird:Neprijatelj
    {
        public BigBird(string path, int cox, int coy):base(path,cox,coy)
        {
            this.Speed = 11;
            this.Damage = 6;
        }
    }
}
