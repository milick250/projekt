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
using System.Diagnostics;

namespace Seminar
{
    public partial class BGL : Form
    {
        public static bool START = true;
        public static int spriteCount = 0;
        public static ListaLikova<Svi> svi_likovi = new ListaLikova<Svi>();
        Dogadaj dogadaj = new Dogadaj();
        string ISPIS = "";
       
        //crta likove
        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            try
            {                
                foreach (Svi sprite in svi_likovi)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Sirina, sprite.Visina));
                        }
                    if (svi_likovi.Change)
                        break;
                }
                if (svi_likovi.Change)
                    svi_likovi.Change = false;
            }
            catch
            {
                //ako se doda lik dok crta onda se mijenja allSprites
                //MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            Init();
        }

        //crta tekst
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            dogadaj.Key = e.KeyCode.ToString();
            dogadaj.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            dogadaj.Key = "";
            dogadaj.KeyPressedTest = false;
        }
        
        //osvjezava ekran
        private void Update(object sender, EventArgs e)
        {
            if (dogadaj.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        //konstruktor
        public BGL()
        {
            InitializeComponent();
        }

        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        public void Init()
        {
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        Bird flappy;
        PilotBird pilot;
        Flee muha;
        BigBird big;
        Coin novac;
        Food hrana;

        bool FlappyPada=true;
        bool HranaPrikaz = false;
        bool Pilotkreni = true;
        bool Bigkreni = true;

        int brojacklikova = 0;//zbog energije...5 koraka

        private void SetupGame()
        {        
            flappy = new Bird("sprites\\ptica.png", 1, 150);  //x i y koordinate
            Game.AddSprite(flappy);
            flappy.SetTransparentColor(Color.Transparent);
            flappy.Postavi_velicinu(25);

            //NEPRIJATELJ
            muha = new Flee("sprites\\flee1.png", 350, 250);
            Game.AddSprite(muha);
            muha.SetTransparentColor(Color.Transparent);
            muha.Postavi_velicinu(25);
            

            pilot = new PilotBird("sprites\\pilotbird.png", 500, 100);
            pilot.SetTransparentColor(Color.Transparent);
            Game.AddSprite(pilot);
            pilot.Postavi_velicinu(50);
            pilot.Prikazi_lika(false);

            big = new BigBird("sprites\\pigeon.gif", 450, 350);
            big.SetTransparentColor(Color.Transparent);
            Game.AddSprite(big);
            big.Postavi_velicinu(80);
            big.Prikazi_lika(false);

            //BONUS
            novac = new Coin("sprites\\coins.png", 350, 150);
            novac.SetTransparentColor(Color.Transparent);
            Game.AddSprite(novac);
            novac.Postavi_velicinu(40);
            novac.Prikazi_lika(false);

            hrana = new Food("sprites\\food.png", 380, 110);
            pilot.SetTransparentColor(Color.Transparent);
            Game.AddSprite(hrana);
            hrana.Postavi_velicinu(40);
            hrana.Prikazi_lika(false);


            _gameover += GameOver;

            _muhadodir += MuhaDodir;
            _hranadodir += HranaDodir;
            
            _novacdodir += NovacDodir;
            _pilotdodir += PilotDodir;
            _bigdodir += BigDodir;
           

            //ISPIS

            ISPIS = "Clicks:" + brojacklikova + "\tHealth:" + flappy.Health + "\tEnergy:" + flappy.Energy + "\tScore: " + flappy.Score;

            //ulazna forma
            Form1 frm = new Form1();
            frm.ShowDialog();//!!!ne moze show

            //Pozivamo dogadaje
            Game.StartScript(BirdKretanje);
            Game.StartScript(FleeKretanje);
            Game.StartScript(HranaKretanje);
            Game.StartScript(NovacKretanje);
        }


        // moji dogadaji
       
        public delegate void MojDelegat();
        public delegate void GameOverDelegat(string poruka);

        public static event GameOverDelegat _gameover;
        public static event MojDelegat _hranadodir;
        public static event MojDelegat _novacdodir;
        public static event MojDelegat _muhadodir;
        public static event MojDelegat _pilotdodir;
        public static event MojDelegat _bigdodir;



        public void GameOver(string poruka)
        {
            FlappyPada = false;
            //MuhaKretanje = false;
            Pilotkreni = false;
            START = false;
            Form2 frm2 = new Form2();
            frm2.Rezultat = flappy.Score;
            frm2.Prekid = poruka;
            frm2.ShowDialog();
        }

        public void HranaDodir()
        {
            hrana.Prikazi_lika(false);
            hrana.Postavi_na_xy(Postavke.Desni_rub, 0);
            flappy.Energy += hrana.Vrijednost;
            if (flappy.Energy > 0)
            {
                Game.StartScript(HranaKretanje);
            }

        }
        public void NovacDodir()
        {
            novac.Prikazi_lika(false);
            flappy.Score += novac.Vrijednost;

            ISPIS = "Clicks:" + brojacklikova + "\tHealth:" + flappy.Health + "\tEnergy:" + flappy.Energy + "\tScore: " + flappy.Score;
            Wait(3.0);
            Game.StartScript(NovacKretanje);
        }

        private void MuhaDodir()
        {
            muha.Prikazi_lika(false);
            muha.Postavi_na_xy(Postavke.Desni_rub, 0);
            flappy.Health -= muha.Damage;
            if (flappy.Health > 0)
            {
                Wait(0.1);
                Game.StartScript(FleeKretanje);
            }

        }
        public void PilotDodir()
        {
            pilot.Prikazi_lika(false);
            flappy.Health -= pilot.Damage;
            flappy.Score -= 50;
            Wait(0.3);
            pilot.Postavi_na_xy(Postavke.Desni_rub, 0);
            if (flappy.Health > 0)
            {
                Wait(0.5);
                Game.StartScript(PilotKretanje);
            }

        }
        public void BigDodir()
        {
            big.Prikazi_lika(false);
            flappy.Health -= big.Damage;
            flappy.Score -= 100;
            Wait(0.3);
            big.Postavi_na_xy(Postavke.Desni_rub, 0);
            if (flappy.Health > 0)
            {
                Wait(0.5);
                Game.StartScript(BigKretanje);
            }
        }

        //kretanje
        private int BirdKretanje()
        {
            while (FlappyPada)
            {
                if (dogadaj.KeyPressed(Keys.Space))
                {
                    if (flappy.Je_li_padala())
                        brojacklikova++;
                    flappy.Leti();
                    Wait(0.01);
                    ISPIS = "Clicks:" + brojacklikova + "\tHealth:" + flappy.Health + "\tEnergy:" + flappy.Energy + "\tScore: " + flappy.Score;

                    if (brojacklikova % 5 == 0)
                    {
                        flappy.Energy -= 2;
                        ISPIS = "Clicks:" + brojacklikova + "\tHealth:" + flappy.Health + "\tEnergy:" + flappy.Energy + "\tScore: " + flappy.Score;
                    }
                }

                else
                {
                    flappy.Padaj();
                    Wait(0.08);
                    string rub;
                    if (flappy.Dodirnuo_rub(out rub))
                    {
                        if (rub == "bottom")
                        {
                            START = false;
                            string poruka = "FLAPPY JE PALA! :(";
                            _gameover.Invoke(poruka);
                            break;
                        }
                    }
                }

                if (flappy.Score >= 200 && Pilotkreni)
                {
                    Pilotkreni = false;

                    Game.StartScript(PilotKretanje);
                }
                if (flappy.Score >= 400 && Bigkreni)
                {
                    Bigkreni = false;

                    Game.StartScript(BigKretanje);
                }


                if (flappy.Health == 0)
                {
                    string poruka = "HEALTH 0! Enemy wins :P";
                    _gameover.Invoke(poruka);
                }
                else if (flappy.Energy == 0)
                {

                    string poruka = "ENERGY 0!";
                    _gameover.Invoke(poruka);
                }

                
            }
            return 0;
        }

        private int HranaKretanje()
        {
            Random r = new Random();
            int y = r.Next(0, Postavke.Donji_rub - hrana.Visina);
            int x = Postavke.Desni_rub - hrana.Sirina;
            hrana.Postavi_na_xy(x, y);
            
            if(flappy.Energy>0)
            {
                HranaPrikaz = true;
            }
            
            while (HranaPrikaz)
            {
                hrana.Prikazi_lika(true);
                hrana.X -= 10;//brzina
                Wait(0.06);
                string rub;
                if (hrana.Dodirnuo_rub(out rub))
                {
                    if (rub == "left")
                    {
                        hrana.Prikazi_lika(false);
                        Game.StartScript(HranaKretanje);
                        break;
                    }
                }
                
                else if (hrana.Dodirnuo_lika(flappy))
                {
                    _hranadodir.Invoke();
                    break;
                }
                
            }
            return 0;
        }
        private int NovacKretanje()
        {
            Random r = new Random();
            int y = r.Next(0, Postavke.Donji_rub - novac.Visina);
            int x = r.Next(Postavke.Lijevi_rub + hrana.Sirina, Postavke.Desni_rub - novac.Sirina);
            novac.Postavi_na_xy(x, y);

            while (START)
            {
                novac.Prikazi_lika(true);
                novac.X-=6;
                Wait(0.03);
                string rub;
                if (novac.Dodirnuo_rub(out rub))
                {
                    if (rub == "left" || rub == "right")
                    {
                        novac.Prikazi_lika(false);
                        Wait(0.4);
                        Game.StartScript(NovacKretanje);
                        break;
                    }
                }
                else if (novac.Dodirnuo_lika(flappy))
                {
                    _novacdodir.Invoke();
                    break;
                }
                
            }
            return 0;
        }

        private int FleeKretanje()
        {
            Random r = new Random();
            int y = r.Next(0, Postavke.Donji_rub - muha.Visina);
            muha.Postavi_na_xy(Postavke.Desni_rub, y);
            muha.Prikazi_lika(true);

            while (brojacklikova < 120)
            {
                muha.X -= muha.Speed;
                Wait(0.03);
                string rub;
                if (muha.Dodirnuo_rub(out rub))
                {
                    if (rub == "left")
                    {
                        muha.Prikazi_lika(false);
                        Wait(0.5);
                        Game.StartScript(FleeKretanje);
                        break;
                    }
                }
                else if (muha.Dodirnuo_lika(flappy))
                {
                    //!!!Beep();
                    _muhadodir.Invoke();
                    break;
                }
            }

            if (brojacklikova == 120)
            {
                muha.Prikazi_lika(false);
                muha.Postavi_na_xy(1000, 1000);

                svi_likovi.Remove(muha);

            }
            return 0;
        }
        private int PilotKretanje()
        {
            Random r = new Random();
            int y = r.Next(0, Postavke.Donji_rub - pilot.Visina);
            int x = Postavke.Desni_rub - pilot.Sirina;
            pilot.Postavi_na_xy(x, y);

            while (START)
            {
                pilot.Prikazi_lika(true);
                pilot.X -= pilot.Speed;
                Wait(0.03);
                string rub;
                if (pilot.Dodirnuo_rub(out rub))
                {
                    if (rub == "left")
                    {
                        pilot.Prikazi_lika(false);
                        Wait(0.15);
                        Pilotkreni = true;
                        break;
                    }

                }
                else if (pilot.Dodirnuo_lika(flappy))
                {
                    //!!!Beep();
                    _pilotdodir.Invoke();
                    break;
                }
               
            }
            return 0;
        }
        private int BigKretanje()
        {
            Random r = new Random();
            int y = r.Next(0, Postavke.Donji_rub - big.Visina);
            int x = r.Next(Postavke.Lijevi_rub + big.Sirina, Postavke.Desni_rub - big.Sirina);
            big.Postavi_na_xy(x, y);

            while (START)
            {
                big.Prikazi_lika(true);
                big.X -= big.Speed;
                Wait(0.07);
                string rub;
                if (big.Dodirnuo_rub(out rub))
                {
                    if (rub == "left")
                    {
                        big.Prikazi_lika(false);
                        Wait(0.15);
                        Bigkreni = true;
                        break;
                    }

                }
                else if (big.Dodirnuo_lika(flappy))
                {
                    //!!!Beep();
                    _bigdodir.Invoke();
                    break;
                }

            }
            return 0;
        }


        /*public void Beep()
        {
            SystemSounds.Beep.Play();
        }*/
    }
}
