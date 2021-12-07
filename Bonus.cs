using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seminar
{
    public class Bonus:Svi
    {
        //konstruktor
        public Bonus(string path,int corx,int cory):base(path,corx,cory)    
        {

        }

        private int vrijednost;

        public int Vrijednost
        {
            get
            {
                return vrijednost;
            }

            set
            {
                vrijednost = value;
            }
        }
    }

    public class Food:Bonus
    {
        public Food(string path,int corx,int cory) : base(path, corx, cory)
        {
            this.Vrijednost = 7;
        }
    }

    public class Coin:Bonus
    {
        public Coin(string path, int corx, int cory) : base(path, corx, cory)
        {
            this.Vrijednost = 100;
        }
    }
}
