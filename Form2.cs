using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Seminar
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private string prekid;

        private int rezultat;

        public int Rezultat
        {
            get
            {
                return rezultat;
            }

            set
            {
                rezultat = value;
            }
        }

        public string Prekid
        {
            get
            {
                return prekid;
            }

            set
            {
                prekid = value;
            }
        }

        private void btnKraj_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text += rezultat.ToString();
            label3.Text += prekid;
            using (StreamWriter sw = File.AppendText("dat.txt"))
            {
                sw.WriteLine(rezultat.ToString());
            }
        }
    }
}
