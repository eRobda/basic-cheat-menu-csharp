using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace our_first_menu
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        static bool showing = true;

        public Form1()
        {
            InitializeComponent();

            //make the windows transparent
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;

            //make the windows borderless
            this.FormBorderStyle = FormBorderStyle.None;

            //make the windows start in top left corner
            this.StartPosition = FormStartPosition.Manual;

            //make the windows topmost
            this.TopMost = true;

            CheckForIllegalCrossThreadCalls = false;

            Thread shm = new Thread(ShowHideMenu);
            shm.Start();
        }

        void ShowHideMenu()
        {
            while (true)
            {
                if(GetAsyncKeyState(Keys.Insert) < 0 && showing == true) //than hide it
                {
                    this.Hide();
                    showing = false;
                    Thread.Sleep(20);
                }
                else if (GetAsyncKeyState(Keys.Insert) < 0 && showing == false) // than show it
                {
                    this.Show();
                    showing = true;
                    Thread.Sleep(20);
                }
                else if (GetAsyncKeyState(Keys.Delete) < 0)
                {
                    Environment.Exit(0);
                    Application.Exit();
                }

                Thread.Sleep(70);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //end all created threads
            Environment.Exit(0);
            Application.Exit();
        }
    }
}
