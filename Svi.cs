using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Seminar
{
    public class Svi
    {        
        //private
        private int x, y;
        private int sirina, visina; //svojstvo

        // (set) osigurava da lik ne izlazi van ruba
        public int X
        {
            get { return x; }
            set
            {
                if(value<Postavke.Lijevi_rub)
                {
                    this.x = Postavke.Lijevi_rub;
                }
                else if(value>Postavke.Desni_rub-this.Sirina/2)
                {
                    this.x = Postavke.Desni_rub-this.Sirina;
                }
                this.x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (value < Postavke.Gornji_rub)
                {
                    this.y= Postavke.Gornji_rub;
                }

                else if (value >= Postavke.Donji_rub-this.visina)
                {
                    this.y = Postavke.Donji_rub-this.visina;
                }
                else
                {
                    this.y = value;
                }
            }
        }

        public int Sirina
        {
            get { return sirina; }
            set
            {
                if (value <= 0)
                    sirina = Postavke.Sirina_lika;
                else
                    sirina = value;
            }
        }
        public int Visina
        {
            get { return visina; }
            set
            {
                if (value <= 0)
                    visina = Postavke.Visina_lika;
                else
                    visina = value;
            }
        }

        public bool Show; //show
        public List<Bitmap> Costumes = new List<Bitmap>();
        public Bitmap CurrentCostume;
        public int CostumeIndex;
        public string CostumeName;
        public int Size;
        public int SpriteIndex;

        private int originalWidth, originalHeight;

        //konstruktor
        public Svi(string spriteImage, int posX, int posY)
        {
            CurrentCostume = new Bitmap(spriteImage);
            CostumeName = spriteImage;
            X = posX;
            Y = posY;
            Sirina = CurrentCostume.Width;
            Visina = CurrentCostume.Height;
            Show = true;
            SpriteIndex = -1;
            Costumes.Add(new Bitmap(spriteImage));
            CostumeIndex = 0;
            Size = 100;
            originalWidth = sirina;
            originalHeight = visina;

        }       

        //funkcije

        public int Srediste_lika_x()
        {
            return X + Sirina / 2;
        }

        public int Srediste_lika_y()
        {
            return Y + Visina / 2;
        }        
        
        public void Postavi_na_xy(int posX, int posY)
        {
            this.X = posX;
            this.Y = posY;
        }

        private bool CheckEdgeX(int posX)
        {
            if (posX + Sirina >= Postavke.Desni_rub || posX <= Postavke.Lijevi_rub)
                return false;
            else
                return true;
        }

        private bool CheckEdgeY(int posY)
        {
            if (posY + Visina >= Postavke.Donji_rub || posY <= Postavke.Gornji_rub)
                return false;
            else
                return true;
        }        
        
        public void SetTransparentColor(Color color)
        {            
            this.CurrentCostume.MakeTransparent(color);
        }
        
        public void Postavi_velicinu(int size)
        {
            Size = size;

            float sx = float.Parse(this.Sirina.ToString());
            float sy = float.Parse(this.Visina.ToString());
            float nsx = ((sx / 100) * size);
            float nsy = ((sy / 100) * size);

            if (Size == 100)
            {
                nsx = originalWidth;
                nsy = originalHeight;
            }

            this.Sirina = Convert.ToInt32(nsx);
            this.Visina = Convert.ToInt32(nsy);
        }

        public void Prikazi_lika(bool value)
        {
            this.Show = value;
        }

        private bool Dodirnuo_desni_rub()
        {
            if (this.X + this.Sirina >= Postavke.Desni_rub)
                return true;
            else
                return false;
        }
        private bool Dodirnuo_lijevi_rub()
        {
            if (this.X <= 0)
                return true;
            else
                return false;
        }
        private bool Dodirnuo_donji_rub()
        {
            if (this.Y + this.Visina >= Postavke.Donji_rub)
                return true;
            else
                return false;
        }
        private bool Dodirnuo_gornji_rub()
        {
            if (this.Y <= 0)
                return true;
            else
                return false;
        }

        public bool Dodirnuo_rub(out string edge)
        {
            if (Dodirnuo_lijevi_rub())
            {
                edge = "left";
                return true;
            }
            if (Dodirnuo_desni_rub())
            {
                edge = "right";
                return true;
            }
            if (Dodirnuo_donji_rub())
            {
                edge = "bottom";
                return true;
            }
            if (Dodirnuo_gornji_rub())
            {
                edge = "top";
                return true;
            }

            edge = "";
            return false;
        }

        public bool Dodirnuo_lika(Svi b)
        {
            Svi a = this;

            Rectangle A = new Rectangle(a.X, a.Y, a.Sirina, a.Visina);
            Rectangle B = new Rectangle(b.X, b.Y, b.Sirina, b.Visina);

            if (A.IntersectsWith(B))
                return true;
            else
                return false;
        }
    }
}
