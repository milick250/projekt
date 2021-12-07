using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Seminar
{
    public class Dogadaj
    {
        private string key;

        private bool keyPressedTest;

        public bool KeyPressedTest
        {
            get { return keyPressedTest; }
            set { keyPressedTest = value; }
        }


        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        //konstruktor
        public Dogadaj()
        {
            //!!!MouseDown = false;
            keyPressedTest = false;
            //!!!Mouse = new Point(0, 0);
        }

        
        public bool KeyPressed(string keyName)
        {
            if (KeyPressedTest && Key == keyName)
            {
                Game.WaitMS(20);
                return true;
            }
            else
                return false;
        }

        
        public bool KeyPressed(Keys key)
        {
            if (KeyPressedTest && Key == key.ToString())
            {
                Game.WaitMS(20);
                return true;
            }
            else
                return false;
        }

        
        public bool KeyPressed()
        {
            return KeyPressedTest;
        }

        //tipka pustena
        public void KeyUp()
        {
            keyPressedTest = false;
            key = "";
        }
    } 
}
