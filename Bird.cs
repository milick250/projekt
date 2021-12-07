using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seminar
{
    public class Bird:Svi
    {
        public Bird(string path,int cox,int coy=0):base(path,cox,coy)
        {
            this.Health = 15;
            this.Energy = 35;
            this.Speed = 20;
            this.Score = 0;
        }

        private bool signal;

        protected int health;
        protected int energy;
        protected int speed;
        protected int score;


        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if(value<0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
                
               
            }
        }

        public int Energy
        {
            get
            {
                return energy;
            }

            set
            {
                if (value >0)
                {
                    energy = value;
                }
                if(value<=0)
                    
                {
                    value = 0;
                }
                energy = value;
        
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

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                if(value<0)
                {
                    score = 0;
                }
                else
                {
                    score = value;
                }
               
            }
        }

        public void Leti()
        {
           this.Y -= this.Speed;
            signal = false;
        }

        public void Padaj()
        {
            this.Y += 10;
            signal = true;
            
        }

        public bool Je_li_padala()
        {
            return signal;
        }
    }
}
