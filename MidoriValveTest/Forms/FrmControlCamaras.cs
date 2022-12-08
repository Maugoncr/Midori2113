using AForge.Video.DirectShow;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class FrmControlCamaras : Form
    {

        private Midori_PV Intermediario;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FrmControlCamaras(Midori_PV intermediario)
        {
            InitializeComponent();
            Intermediario = intermediario;
        }

        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IconIniciarCam_Click(object sender, EventArgs e)
        {
            Intermediario.ActivarCam1(cbCamaraSelect.SelectedIndex);
        }

        private void IconIniciarCam2_Click(object sender, EventArgs e)
        {
            Intermediario.ActivarCam2(cbCamaraSelect2.SelectedIndex);
        }

        private void IconIniciarCam3_Click(object sender, EventArgs e)
        {
            Intermediario.ActivarCam3(cbCamaraSelect3.SelectedIndex);
        }

        private void IconIniciarCam4_Click(object sender, EventArgs e)
        {
            Intermediario.ActivarCam4(cbCamaraSelect4.SelectedIndex);
        }

        private bool HayDispositivos;
        private FilterInfoCollection MisDispositivos;

        public void CargaDiapositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivos.Count > 0)
            {
                HayDispositivos = true;
                cbCamaraSelect.Items.Clear();
                cbCamaraSelect2.Items.Clear();
                cbCamaraSelect3.Items.Clear();
                cbCamaraSelect4.Items.Clear();
                for (int i = 0; i < MisDispositivos.Count; i++)
                {
                    cbCamaraSelect.Items.Add(MisDispositivos[i].Name.ToString());
                    cbCamaraSelect.Text = MisDispositivos[0].Name.ToString();

                    cbCamaraSelect2.Items.Add(MisDispositivos[i].Name.ToString());
                    cbCamaraSelect2.Text = MisDispositivos[0].Name.ToString();

                    cbCamaraSelect3.Items.Add(MisDispositivos[i].Name.ToString());
                    cbCamaraSelect3.Text = MisDispositivos[0].Name.ToString();

                    cbCamaraSelect4.Items.Add(MisDispositivos[i].Name.ToString());
                    cbCamaraSelect4.Text = MisDispositivos[0].Name.ToString();
                }
            }
            else
            {
                HayDispositivos = false;
            }
        }

        private void iconRefresh_Click(object sender, EventArgs e)
        {
            CargaDiapositivos();
            Intermediario.CargaDiapositivosInter();
        }

        private void FrmControlCamaras_Load(object sender, EventArgs e)
        {
            CargaDiapositivos();
            Intermediario.CargaDiapositivosInter();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
