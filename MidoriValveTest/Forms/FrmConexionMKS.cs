using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class FrmConexionMKS : Form
    {
        public Midori_PV mensajero;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FrmConexionMKS()
        {
            InitializeComponent();
        }



        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconRefresh_Click(object sender, EventArgs e)
        {
            CargaDiapositivos();
        }

        private void CargaDiapositivos() 
        {
            string[] ports = SerialPort.GetPortNames();

            cbMKS1.Enabled = true;
            cbMKS1.Items.Clear();
            cbMKS1.Items.AddRange(ports);

            cbMKS2.Enabled = true;
            cbMKS2.Items.Clear();
            cbMKS2.Items.AddRange(ports);

            btnConnectMKS1.Enabled = false;
            btnConnectMKS2.Enabled = false;

        }

        private void FrmConexionMKS_Load(object sender, EventArgs e)
        {
            CargaDiapositivos();

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnConnectMKS2_Click(object sender, EventArgs e)
        {
            if (btnConnectMKS2.IconChar == FontAwesome.Sharp.IconChar.ToggleOff)
            {
                btnConnectMKS2.IconChar = FontAwesome.Sharp.IconChar.ToggleOn;
                mensajero.IniciarConexionMKS2(cbMKS2.SelectedItem.ToString());
                iconRefresh.Enabled = false;

            }
            else
            {
                btnConnectMKS2.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
                iconRefresh.Enabled = true;
            }
          
        }

        private void btnConnectMKS1_Click(object sender, EventArgs e)
        {
            if (btnConnectMKS1.IconChar == FontAwesome.Sharp.IconChar.ToggleOff)
            {
                btnConnectMKS1.IconChar = FontAwesome.Sharp.IconChar.ToggleOn;
                mensajero.IniciarConexionMKS1(cbMKS1.SelectedItem.ToString());
                iconRefresh.Enabled = false;

            }
            else
            {
                btnConnectMKS1.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
                iconRefresh.Enabled = true;

            }
        }

        private void cbMKS1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMKS1.SelectedIndex >= 0)
            {
                btnConnectMKS1.Enabled = true;
            }
            else
            {
                btnConnectMKS1.Enabled = false;
            }
        }
    }
}
