using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class FrmDontTouch : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmDontTouch()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmDontTouch_Load(object sender, EventArgs e)
        {
            GifMin.Visible = false;
            lbMin.Visible = false;
            this.Opacity = 0;
            timer1.Start();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 1517;
            this.Top = 875;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            GifMin.Visible = false;
            lbMin.Visible = false;
            lbBig.Visible = true;
            GifBig.Visible = true;
            this.Opacity = 0; // establecer la opacidad a 0 (completamente transparente)
            this.Left = 1517; // mover el formulario a la nueva posición
            this.Top = 875;
            timer1.Start(); // iniciar el temporizador
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            GifMin.Visible = true;
            lbMin.Visible = true;
            lbBig.Visible = false;
            GifBig.Visible = false;
            this.Opacity = 0; // establecer la opacidad a 0 (completamente transparente)
            this.Left = 1517; // mover el formulario a la nueva posición
            this.Top = 986;
            timer1.Start(); // iniciar el temporizador

            //int left = this.Left;
            //int top = this.Top;
            //MessageBox.Show($"Left: {left}, Top: {top}");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05; // aumentar gradualmente la opacidad
            }
            else
            {
                timer1.Stop(); // detener el temporizador cuando la opacidad sea 1
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (Opacity > 0.5) // Si la opacidad es mayor a la mitad
            {
                Opacity -= 0.1; // Disminuir en 10%
            }
            else // Si la opacidad ya está en la mitad o menos
            {
                Opacity = 0.5; // Mantener la opacidad en el 50%
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (this.Opacity >= 0.9)
            {
                this.Opacity = 1.0; // Ya se alcanzó la máxima opacidad permitida
            }
            else
            {
                this.Opacity += 0.1; // Se aumenta la opacidad en un 10%
            }
        }

        private void FrmDontTouch_Activated(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        public bool permitirCerrar = false;
        private void FrmDontTouch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!permitirCerrar)
            {
                e.Cancel = true;
            }
        }
    }
}
